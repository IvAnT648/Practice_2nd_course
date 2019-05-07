using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Authorization : Form
    {
        DataSet usersData = null;
        bool UsersDataLoaded = false;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            LoadUsersData();
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void bToRegistration_Click(object sender, EventArgs e)
        {
            Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
            Close();
        }

        private async void bEnter_Click(object sender, EventArgs e)
        {
            
            string nick = maskedTextBox1.Text, password = maskedTextBox2.Text;
            if (!string.IsNullOrWhiteSpace(nick) && !string.IsNullOrWhiteSpace(password))
            {
                if (!UsersDataLoaded)
                {
                    MessageBox.Show("Данные о пользователях не загружены. Повторите попытку позднее.", "Сообщение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int id = 0;
                for (int it = 0; it < usersData.Tables[0].Rows.Count; it++)
                {
                    string currentNick = usersData.Tables[0].Rows[it].ItemArray[1].ToString();
                    string currentEmail = usersData.Tables[0].Rows[it].ItemArray[2].ToString();
                    string currentPassword = usersData.Tables[0].Rows[it].ItemArray[3].ToString();

                    if ((nick == currentNick || nick == currentNick) && password == currentPassword)
                    {
                        id = (int)usersData.Tables[0].Rows[it].ItemArray[0];
                        break;
                    }
                }
                if (id == 0)
                {
                    MessageBox.Show("Некорректные данные для входа. Проверьте правильность введенных данных", "Пользователь не найден",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // далее, выполняем вход
                SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
                SqlCommand query = new SqlCommand(@"SELECT Surname, Name, Patronymic, [E-mail], Nick, Status FROM Users WHERE Id=" + id, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await query.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        if (await reader.ReadAsync())
                        {
                            UserStatus status = UserStatus.Guest;
                            switch ((int)reader.GetValue(5))
                            {
                                case 1:
                                    status = UserStatus.Normal;
                                    break;
                                case 2:
                                    status = UserStatus.Admin;
                                    break;
                                default:
                                    MessageBox.Show("Ошибка чтения данных о пользователе из БД.\n" +
                                        "Вход будет выполненен как гость", "Ошибка", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                            // авторизация пользователя
                            User user = User.Login(
                                id,
                                reader.GetValue(0)?.ToString(),
                                reader.GetValue(1)?.ToString(),
                                reader.GetValue(2)?.ToString(),
                                reader.GetValue(3)?.ToString(),
                                reader.GetValue(4)?.ToString(),
                                status
                            );

                        }
                    }
                    if (!reader.IsClosed)
                        reader.Close();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                
            }
            else MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // загрузка всех данных для входа, т.к. sql - регистронезависимый
        private async void LoadUsersData()
        {
            usersData = new DataSet();
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT Id, Nick, [E-mail], Password FROM Users", connection);
                adapter.Fill(usersData);
                UsersDataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UsersDataLoaded = false;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
    }
}