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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Int64 password_Hash = textBox6.Text.GetHashCode();
            if (textBox7.Text.GetHashCode() != password_Hash)
            {
                MessageBox.Show("Введенные пароли не сов подают! Пожалуйста введите еще раз.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.ResetText();
                textBox7.ResetText();
                return;
            }
            
            SqlDataReader sqlReader = null;
            SqlCommand comand = new SqlCommand("INSERT INTO [Users] ([Sure_Name], [Name], [Login], [Password]) VALUES (@Sure_Name, @Name, @Login, @Password)", LoginForm.sqlConnection);
            comand.Parameters.AddWithValue("Sure_Name", textBox3.Text);
            comand.Parameters.AddWithValue("Name", textBox4.Text);
            comand.Parameters.AddWithValue("Login", textBox5.Text);
            comand.Parameters.AddWithValue("Password", password_Hash);
            try
            {
                sqlReader = await comand.ExecuteReaderAsync();
               
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.ResetText();
                textBox7.ResetText();
                if (sqlReader != null)
                    sqlReader.Close();
                return;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                this.Close();
            }
        }
    }
}
