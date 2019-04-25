using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Authorization : Form
    {
        DataSet usersData;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            loadUsersData();
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {

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
            if (nick != "" && password != "")
            {
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
                SqlCommand query = new SqlCommand("SELECT [Surname], [Name], [Patronymic], [E-mail], [Nick], UPPER([Status]) FROM [Users] WHERE [Id]=" + id, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await query.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        if (await reader.ReadAsync())
                        {
                            UserStatus status = UserStatus.Guest;
                            switch (reader.GetValue(5)?.ToString())
                            {
                                case "USER":
                                    status = UserStatus.UsualUser;
                                    break;
                                case "ADMIN":
                                    status = UserStatus.Admin;
                                    break;
                                default:
                                    MessageBox.Show("Ошибка чтения данных о пользователе из БД.\nВход будет выполненен как гость", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                            // авторизация пользователя
                            MainForm.UserInSystem = new User(
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
        private async void loadUsersData()
        {
            if (usersData == null)
                usersData = new DataSet();
            else usersData.Clear();
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [Id], [Nick], [E-mail], [Password] FROM Users", connection);
                adapter.Fill(usersData);
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
    }
}
