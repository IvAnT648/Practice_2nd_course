using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (User.GetUser()?.Status == UserStatus.Admin)
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
                    connection = new SqlConnection(Common.StrSQLConnection);
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = $@"INSERT INTO 
                        Users (Surname, Name, Patronymic, [E-mail], Nick, Password, Status) 
                        VALUES (@surname, @name, @patronymic, @email, @nick, @password, @status)";

                    command.Parameters.AddWithValue("surname", textBoxSurname.Text);
                    command.Parameters.AddWithValue("name", textBoxName.Text);
                    command.Parameters.AddWithValue("patronymic", textBoxPatronymic.Text);
                    command.Parameters.AddWithValue("email", textBoxEmail.Text);
                    command.Parameters.AddWithValue("nick", textBoxNick.Text);
                    command.Parameters.AddWithValue("password", textBoxPassword.Text);
                    command.Parameters.AddWithValue("status", status);

                    if (await command.ExecuteNonQueryAsync() != 1)
                    {
                        MessageBox.Show("Пользователь не был зарегистрирован", "Ошибка при регистрации",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else 
                    MessageBox.Show("Пользователь зарегистрирован", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SqlCommand query = new SqlCommand($@"SELECT [Id], [Surname], [Name], [Patronymic], [E-mail], [Nick], UPPER([Status]) FROM [Users] WHERE [Nick]='{textBoxNick.Text}'", connection);
                    SqlDataReader reader = await query.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        if (await reader.ReadAsync())
                        {
                            UserStatus userStatus = UserStatus.Guest;
                            switch (reader.GetValue(6)?.ToString())
                            {
                                case "USER":
                                    userStatus = UserStatus.Normal;
                                    break;
                                case "ADMIN":
                                    userStatus = UserStatus.Admin;
                                    break;
                                default:
                                    MessageBox.Show("Ошибка чтения данных о пользователе из БД.\nВход будет выполненен как гость", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                            // авторизация пользователя
                            User user = User.Login(
                                (int)reader.GetValue(0),
                                reader.GetValue(1)?.ToString(),
                                reader.GetValue(2)?.ToString(),
                                reader.GetValue(3)?.ToString(),
                                reader.GetValue(4)?.ToString(),
                                reader.GetValue(5)?.ToString(),
                                userStatus
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
                Close();
            }
        }

        private bool validation()
        {
            try
            {
                connection = new SqlConnection(Common.StrSQLConnection);;
                connection.Open();
                Regex regex = null;
                // если не введены необходимые данные
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(textBoxNick.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPassword.Text))
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
                regex = new Regex(@"^([a-z0-9]|_){4,}$");
                if (!regex.IsMatch(textBoxNick.Text))
                {
                    MessageBox.Show("Никнейм может состоять только из букв, цифр и нижнего подчеркивания и при этом иметь не менее 4 символов", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //regex = new Regex(@"^(\w|_|\.)+(@)(\w+)(\.)(\w)+$");
                if (!IsValidEmail(textBoxEmail.Text))
                {
                    MessageBox.Show("Некорректный e-mail адрес", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                regex = new Regex(@"^([a-z0-9]|_){4,}$");
                if (!regex.IsMatch(textBoxPassword.Text))
                {
                    MessageBox.Show("Пароль может состоять только из букв, цифр и нижнего подчеркивания и при этом иметь не менее 4 символов", "Некорректный ввод",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // проверка на совпадение введенного e-mail с e-mail других пользователей
                SqlCommand query = new SqlCommand($@"SELECT COUNT(Id) FROM Users WHERE [E-mail]='{textBoxEmail.Text}'", connection);
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
                query = new SqlCommand($@"SELECT COUNT(Id) FROM Users WHERE [Nick]='{textBoxNick.Text}'", connection);
                answer = -1;
                answer = Convert.ToInt32(query.ExecuteScalar());
                if (answer > 0 || answer == -1)
                {
                    if (answer == -1)
                        MessageBox.Show("Произошла ошибка при сверке ника с базой.", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Пользователь с таким ником уже зарегистрирован!", "Ошибка!",
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
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}