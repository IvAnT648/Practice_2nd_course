using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text;

namespace AIS_shop
{
    enum OrderStatus { Registered, Perfomed, Completed }
    enum RequiredFilter { NotRequired, CheckedList, FromTo }
    enum UserStatus { Guest, Customer, Admin }
    
    // используется при добавлении/изменении товара
    class _strToGridView
    {
        public string name { get; }
        public string obligation { get; }
        public string type { get; }
        public object value { get; set; } = null;

        public _strToGridView(string table_field, string obligation, string type)
        {
            this.name = table_field;
            this.obligation = obligation;
            this.type = type;
        }
    }

    class User
    {
        public int Id { get; private set; }
        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Patronymic { get; private set; }
        public string Email { get; private set; }
        public string Nick { get; private set; }
        public UserStatus Status { get; private set; }
        public Image Picture { get; private set; }

        private static User instance = null;

        private User() { }

        public static User GetUser()
        {
            if (instance == null)
            {
                instance = new User();
                instance.Id = 0;
                instance.Surname = null;
                instance.Name = null;
                instance.Patronymic = null;
                instance.Email = null;
                instance.Nick = null;
                instance.Status = UserStatus.Guest;
            }
            return instance;
        }

        public static User Login(int id, string surname, string name, string patronymic, string email, string nick, UserStatus status)
        {
            if (instance == null)
                instance = new User();
            instance.Id = id;
            instance.Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            instance.Name = name ?? throw new ArgumentNullException(nameof(name));
            instance.Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
            instance.Email = email ?? throw new ArgumentNullException(nameof(email));
            instance.Nick = nick ?? throw new ArgumentNullException(nameof(nick));
            instance.Status = status;

            string message = null;
            if (instance.Status == UserStatus.Customer)
                message = $"Добро пожаловать {instance.Surname} {instance.Name} {instance.Patronymic}!";
            if (instance.Status == UserStatus.Admin)
                message = $"Добро пожаловать {instance.Surname} {instance.Name} {instance.Patronymic}! Вы вошли как администратор.";
            if (instance.Status == UserStatus.Guest)
                message = $"Добро пожаловать! Вы вошли как гость.";
            MessageBox.Show(message, "Вход в систему выполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return instance;
        }

        public static void Logout()
        {
            instance.Id = 0;
            instance.Surname = null;
            instance.Name = null;
            instance.Patronymic = null;
            instance.Email = null;
            instance.Nick = null;
            instance.Status = UserStatus.Guest;
        }
    }

    class FilterChecked
    {
        public GroupBox groupBox { set; get; }
        public CheckedListBox checkedList { set; get; }

        public FilterChecked(string name)
        {
            Size size = new Size(200, 150);
            groupBox = new GroupBox()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Text = name
            };
            size = new Size(174, 109);
            checkedList = new CheckedListBox()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Location = new Point(10, 19),
                CheckOnClick = true
            };
            groupBox.Controls.Add(checkedList);
        }
    }

    class FilterFromTo
    {
        public GroupBox groupBox { set; get; }
        public TextBox from { set; get; }
        public TextBox to { set; get; }
        private Label lFrom { set; get; }
        private Label lTo { set; get; }

        public FilterFromTo(string name)
        {
            Size size = new Size(200, 100);
            this.groupBox = new GroupBox()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Text = name
            };
            size = new Size(80, 20);
            this.from = new TextBox()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Location = new Point(10, 65)
            };
            this.to = new TextBox()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Location = new Point(105, 65),
                RightToLeft = RightToLeft.Yes
            };
            
            size = new Size(27, 20);
            this.lFrom = new Label()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Location = new Point(7, 40),
                Font = new Font("Calibri", 12, FontStyle.Regular),
                Text = "От"
            };
            this.lTo = new Label()
            {
                Size = size,
                MinimumSize = size,
                MaximumSize = size,
                Location = new Point(158, 40),
                Font = new Font("Calibri", 12, FontStyle.Regular),
                Text = "До"
            };

            this.groupBox.Controls.Add(from);
            this.groupBox.Controls.Add(to);
            this.groupBox.Controls.Add(lFrom);
            this.groupBox.Controls.Add(lTo);
            
        }
    }

    class Field
    {
        // название столбца
        public string name { set; get; }
        // способ фильтрации
        
        public RequiredFilter filter { set; get; } = RequiredFilter.NotRequired;
        // команда для получения списка возможных значений (для checkbox'а)
        public string sqlCommand { set; get; }

        public Field(string field_name, RequiredFilter requiredFilter)
        {
            this.name = field_name ?? throw new ArgumentNullException(nameof(name));
            this.filter = requiredFilter;
        }

        public Field(string field_name, RequiredFilter requiredFilter, string SQLCommand)
        {
            this.name = field_name ?? throw new ArgumentNullException(nameof(name));
            this.filter = requiredFilter;
            this.sqlCommand = SQLCommand ?? throw new ArgumentNullException(nameof(SQLCommand));
        }
    }

    static class ImageTools
    {
        // загружает изображение в базу данных, а также возвращает его.
        // аргументы: путь к файлу, строка подключения к БД, название таблицы, ячейки и id записи для сохранения. 
        // Поле таблицы "field" в базе данных должно иметь тип "Image" 
        public static Image PutImageInDB(string path_to_file, string strConnectionToDB, string name_of_table, string field, int record_id)
        {
            byte[] imageData = null;
            FileInfo fInfo = new FileInfo(path_to_file);

            // получение размера изображения в байтах
            int numBytes = (int)fInfo.Length;
            // открытие файла
            FileStream fStream = new FileStream(path_to_file, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fStream);
            // конвертация изображения в байты
            imageData = binaryReader.ReadBytes(numBytes);
            if (imageData == null) return null;
            // запись изображения в БД
            SqlConnection sqlConnection = new SqlConnection(strConnectionToDB);
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = string.Format($@"UPDATE [{name_of_table}] SET [{field}]=@image WHERE [Id]={record_id}");
            command.Parameters.AddWithValue("@image", (object)imageData);
            try
            {
                // загрузка в БД
                sqlConnection.Open();
                command.ExecuteNonQuery();
                // возвращение этого изображения
                return Image.FromStream(new MemoryStream(imageData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
        }

        // загружает и возвращает изображение из базы данных.
        // аргументы: строка подключения к БД, название таблицы и id записи из которой выполняется загрузка изображения из поля "field".
        // Поле таблицы "field" должно иметь тип "Image" 
        public static Image GetImageFromDB(string strConnectionToDB, string name_of_table, string field, int record_id)
        {
            byte[] imageData = null;
            SqlConnection sqlConnection = new SqlConnection(strConnectionToDB);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = string.Format($@"SELECT [{field}] FROM [{name_of_table}] WHERE [Id]={record_id}");
                object answer = sqlCommand.ExecuteScalar();
                imageData = sqlCommand.ExecuteScalar() as byte[];
                if (imageData != null) return Image.FromStream(new MemoryStream(imageData));
                else return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
                
        }
    }

    static class FileTools
    {
        // загружает и возвращает изображение из базы данных.
        // аргументы: строка подключения к БД, название таблицы и id записи из которой выполняется загрузка изображения из поля "field".
        // Поле таблицы "field" должно иметь тип "Image" 
        public static byte[] GetFileFromDB(string strConnectionToDB, string name_of_table, string field, int record_id)
        {
            byte[] data = null;

            SqlConnection sqlConnection = new SqlConnection(strConnectionToDB);
            SqlCommand sqlCommand = new SqlCommand(string.Format($@"SELECT [{field}] FROM [{name_of_table}] WHERE [Id]={record_id}"),sqlConnection);
            SqlDataReader reader = null;
            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        var s = Encoding.ASCII.GetString((byte[])reader.GetValue(0));
                        if (s != "NULL")
                            data = reader.GetValue(0) as byte[];
                    }
                        
                }
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }

        }

        public static bool PutBytesToDB(byte[] data, string strConnectionToDB, string name_of_table, string field, int record_id)
        {
            if (data.Length == 0) return false;
            SqlConnection sqlConnection = new SqlConnection(strConnectionToDB);
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = string.Format($@"UPDATE [{name_of_table}] SET [{field}]=@image WHERE [Id]={record_id}")
            };
            command.Parameters.AddWithValue("@image", (object)data);
            try
            {
                // загрузка в БД
                sqlConnection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
        }

        public static byte[] FileInBytes(string path_to_file)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(path_to_file);
            // получение размера файла в байтах
            int numBytes = (int)fInfo.Length;
            // открытие файла
            FileStream fStream = new FileStream(path_to_file, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fStream);
            // конвертация файла в байты
            data = binaryReader.ReadBytes(numBytes);
            if (data == null) return null;
            else return data;
        }
    }

    static class Common
    {
        public static string StrSQLConnection { get; } = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public static List<int> ProductsInCart { get; } = new List<int>();
    }
}