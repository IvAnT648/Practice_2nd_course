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
        private SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
        public Authorization()
        {
            InitializeComponent();
        }
        // Кнопка "Войти"
        private async void button1_Click(object sender, EventArgs e)
        {
            string nick = maskedTextBox1.Text, password = maskedTextBox2.Text;
            if (nick != "" && password != "")
            {
                SqlDataReader reader = null;
                SqlCommand query = new SqlCommand(
                    "SELECT [User_id] FROM [Users] WHERE " +
                    "([Nick]=\'" + nick + "\' OR [E-mail]=\'" + nick + "\') AND ([Password]=\'" + password + "\')", connection);
                try
                {
                    await connection.OpenAsync();
                    uint id = 0;
                    object answer = await query.ExecuteScalarAsync();
                    if (answer == null)
                    {
                        MessageBox.Show("Проверьте корректность введенных данных.", "Пользователь не обнаружен!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        id = Convert.ToUInt32(answer);
                        // далее, выполняем вход
                        query.CommandText = "SELECT [Surname], [Name], [Patronymic], [E-mail], [Nick], [Status], [Avatar] FROM [Users] WHERE [User_id]=" + id;
                        reader = await query.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            if (await reader.ReadAsync())
                                MainForm.UserInSystem = new User(
                                    reader.GetValue(0)?.ToString(),
                                    reader.GetValue(1)?.ToString(),
                                    reader.GetValue(2)?.ToString(),
                                    reader.GetValue(3)?.ToString(),
                                    reader.GetValue(4)?.ToString(),
                                    reader.GetValue(5)?.ToString(),
                                    reader.GetValue(6)?.ToString()
                                );                                
                        }
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
            else MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // кнопка перехода на форму регистрации
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
            Close();
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
