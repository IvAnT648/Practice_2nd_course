using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace AIS_shop
{
    enum UserStatus { Guest, UsualUser, Admin }
    enum RequiredFilter { NotRequired, CheckedList, FromTo }

    class User
    {
        public string Surname { get; }
        public string Name { get; }
        public string Patronymic { get; }
        public string Email { get; }
        public string Nick { get; }
        public UserStatus Status { get; }
        public Image Picture { get; set; }

        public User(string surname, string name, string patronymic, string email, string nick, UserStatus status)
        {
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Nick = nick ?? throw new ArgumentNullException(nameof(nick));
            Status = status;
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
        // 0 - не использовать при фильтрации, 1 - checkBox'ы, 2 - 'от и до'
        public RequiredFilter filter { set; get; } = RequiredFilter.NotRequired;
        // команда для получения списка возможных значений (для checkbox'а)
        public string sqlCommand { set; get; }

        public Field(string field_name)
        {
            this.name = field_name;
        }
        public Field(string field_name, RequiredFilter requiredFilter)
        {
            this.name = field_name;
            this.filter = requiredFilter;
        }
        public Field(string field_name, RequiredFilter requiredFilter, string SQLCommand)
        {
            this.name = field_name;
            this.filter = requiredFilter;
            this.sqlCommand = SQLCommand;
        }
    }

    class Table
    {
        public string name { set; get; }
        public List<Field> fields;
    }

    static class Common
    {
        // строка соед. с БД
        public static string StrSQLConnection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        // контейнер полей, для которых нужны фильтры
        // кол-во объектов в контенере = кол-во таблиц с товарами;
        // т.е для каждого стобца таблицы есть описательная структура
        // ... заполняется при созданиии таблиц
        public static List<Table> fieldsForFilters = null;

        public static void Crutch()
        {
            // этот код должен выполняться при создании категории товаров (новой таблицы)
            string table_name = "Computers";
            if (fieldsForFilters == null)
                fieldsForFilters = new List<Table>();
            fieldsForFilters.Add(
                new Table
                {
                    name = table_name,
                    fields = new List<Field>()
                });

            fieldsForFilters[0].fields.Add(new Field("Type", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Brand", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Brand CPU", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Count of cores", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Brand GPU", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Type RAM", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Capacity RAM", RequiredFilter.FromTo));
            fieldsForFilters[0].fields.Add(new Field("Capacity HDD", RequiredFilter.FromTo));
            fieldsForFilters[0].fields.Add(new Field("Capacity SSD", RequiredFilter.FromTo));
            fieldsForFilters[0].fields.Add(new Field("Operating system", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Power PSU", RequiredFilter.CheckedList));
            fieldsForFilters[0].fields.Add(new Field("Cost", RequiredFilter.FromTo));

            foreach (var table in Common.fieldsForFilters)
                foreach (var field in table.fields)
                    if (field.filter == RequiredFilter.CheckedList)
                        field.sqlCommand = 
                            "SELECT DISTINCT [" + field.name + "] FROM [" + table.name + "]";

            //Add_product.PutImageInDB(@"C:\Dexp.jpg", "Computers", 2);
            /////////////////////////////////////////////////////////////////////////////
        }

        public async static void PutImageInDB(string path_to_file, string name_of_table, int record_id)
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

            // запись изображения в БД
            SqlConnection sqlConnection = new SqlConnection(Common.StrSQLConnection);
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = string.Format("UPDATE [{0}] SET [Picture]=@image WHERE [Id]={1}", name_of_table, record_id);
            command.Parameters.AddWithValue("@image", (object)imageData);
            try
            {
                sqlConnection.Open();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
        }

        public static Image GetImageFromDB(string name_of_table, int record_id)
        {
            byte[] imageData = null;
            SqlConnection sqlConnection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = string.Format(@"SELECT [Picture] FROM [{0}] WHERE [Id]={1}", name_of_table, record_id);
                object answer = sqlCommand.ExecuteScalar();
                imageData = sqlCommand.ExecuteScalar() as byte[];
                if (imageData != null)
                {
                    MemoryStream ms = new MemoryStream(imageData);
                    return Image.FromStream(ms);
                }
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
}