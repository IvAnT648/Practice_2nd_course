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

namespace Test_DB
{
    public partial class LoginForm : Form
    {
        public static SqlConnection sqlConnection;
        public User userProperty { get; set; }

        public LoginForm()
        {
            OpenConnection();
            InitializeComponent();
        }

        public void OpenConnection()
        {
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\git\Practice_2nd_course\Test_DB\Test_DB\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            String login, password;
            login = textBox1.Text.ToString();
            password = textBox2.Text.ToString();
            textBox2.ResetText();

            SqlDataReader sqlReader = null;
            SqlCommand comand = new SqlCommand("SELECT * FROM [Users] WHERE Login = @login", sqlConnection);
            comand.Parameters.AddWithValue("login", login);
            try
            {
                sqlReader = await comand.ExecuteReaderAsync();
                await sqlReader.ReadAsync();
                if (Convert.ToInt32(sqlReader["Password"]) == password.GetHashCode())
                {
                    User user = new User(login, Convert.ToString(sqlReader["Name"]), Convert.ToString(sqlReader["Sure_Name"]), Convert.ToInt32(sqlReader["ID"]));                   
                    userProperty = user;
                    if (sqlReader != null)
                        sqlReader.Close();
                } else
                {
                    MessageBox.Show("Введен неверный логин или пароль! Попробуйте еще раз.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (sqlReader != null)
                        sqlReader.Close();
                    textBox2.ResetText();
                    return;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            textBox1.ResetText();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginForm_FormClosing(Object sender, FormClosedEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            userProperty = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.SetVisibleCore(false);
            form2.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userProperty = null;
            this.Hide();
        }
    }

    public class User
    {
        public String name, sureName, login;
        public int id;
        public User(String _login, String _name, String _sureName, int _id)
        {
            name = _name;
            sureName = _sureName;
            login = _login;
            id = _id;
        }
    }
}
