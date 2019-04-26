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
    public partial class Registration : Form
    {
        SqlConnection connection;

        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            if (MainForm.UserInSystem?.Status == UserStatus.Admin)
                regAsAdmin.Visible = true;
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Close();
        }

        private async void bRegistration_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                string status;
                if (regAsAdmin.Checked)
                    status = "ADMIN";
                else
                    status = "USER";
                try
                {
                    connection = new SqlConnection(MainForm.StrSQLConnection);
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO " +
                        "Users ([Surname], [Name], [Patronymic], [E-mail], [Nick], [Password], [Status]) VALUES (" +
                        "\'" + textBoxSurname.Text + "\', " +
                        "\'" + textBoxName.Text + "\', " +
                        "\'" + textBoxPatronymic.Text + "\', " +
                        "\'" + textBoxEmail.Text + "\', " +
                        "\'" + textBoxNick.Text + "\', " +
                        "\'" + textBoxPassword.Text + "\', " +
                        "\'" + status + "\')";

                    await command.ExecuteNonQueryAsync();

                    MessageBox.Show("Пользователь зарегистрирован", "Сообщение", MessageBoxButtons.OK);
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
                Close();
            }
        }

        private bool validation()
        {
            try
            {
                connection = new SqlConnection(MainForm.StrSQLConnection);
                connection.Open();

                // если не введены необходимые данные
                if (textBoxName.Text == "" ||
                    textBoxSurname.Text == "" ||
                    textBoxEmail.Text == "" ||
                    textBoxNick.Text == "" ||
                    textBoxPassword.Text == "")
                {
                    MessageBox.Show("Все поля, кроме отчества, обязательны для заполнения", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!textBoxName.Text.All(char.IsLetter))
                {
                    MessageBox.Show("Имя может состоять только из букв", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!textBoxSurname.Text.All(char.IsLetter))
                {
                    MessageBox.Show("Фамилия может состоять только из букв", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!textBoxPatronymic.Text.All(char.IsLetter))
                {
                    MessageBox.Show("Отчество может состоять только из букв", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!textBoxEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Некорректный e-mail адрес", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!textBoxName.Text.All(char.IsLetterOrDigit))
                {
                    MessageBox.Show("Пароль может состоять только из букв и цифр", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // проверка на совпадение введенного e-mail с e-mail других пользователей
                SqlCommand query = new SqlCommand("SELECT COUNT(Id) FROM Users WHERE [E-mail]=\'" + textBoxEmail.Text + "\'", connection);
                int answer = -1;
                answer = Convert.ToInt32(query.ExecuteScalar());
                if (answer > 0 || answer == -1)
                {
                    if (answer == -1)
                        MessageBox.Show("Произошла ошибка при сверке e-mail с базой.", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Пользователь с таким e-mail уже зарегистриррован!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // проверка на совпадение введенного ника с никами другими пользователями
                query = new SqlCommand("SELECT COUNT(Id) FROM Users WHERE [Nick]=\'" + textBoxNick.Text + "\'", connection);
                answer = -1;
                answer = Convert.ToInt32(query.ExecuteScalar());
                if (answer > 0 || answer == -1)
                {
                    if (answer == -1)
                        MessageBox.Show("Произошла ошибка при сверке ника с базой.", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Пользователь с таким ником уже зарегистриррован!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
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
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
    }
}