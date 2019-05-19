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
    public partial class Form3 : Form
    {
        private int userID;
        private Car car;
        private bool update;
        public Form3(int _userID)
        {
            userID = _userID;
            update = false;
            InitializeComponent();
        }

        public Form3(Car _car)
        {
            car = _car;
            update = true;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            
            try
            {
                SqlCommand comand = new SqlCommand("SELECT [Transmission].Name FROM [Transmission];", LoginForm.sqlConnection);
                sqlReader = await comand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox1.Items.Add(sqlReader["Name"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();

                comand = new SqlCommand("SELECT [Engine_Type].Name FROM [Engine_Type];", LoginForm.sqlConnection);
                sqlReader = await comand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox2.Items.Add(sqlReader["Name"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();

                comand = new SqlCommand("SELECT [State_Broken].Name FROM [State_Broken];", LoginForm.sqlConnection);
                sqlReader = await comand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox3.Items.Add(sqlReader["Name"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();

                comand = new SqlCommand("SELECT [State_New].Name FROM [State_New];", LoginForm.sqlConnection);
                sqlReader = await comand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox4.Items.Add(sqlReader["Name"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

            if (update)
            {
                textBox1.Text = car.name;
                textBox2.Text = car.mileage.ToString();
                textBox3.Text = car.price.ToString();
                textBox4.Text = car.descryption;
                numericUpDown1.Value = car.year;
                label9.Text = car.image;
                comboBox1.SelectedItem = car.transmission;
                comboBox2.SelectedItem = car.engine_Type;
                comboBox3.SelectedItem = car.state_Broken;
                comboBox4.SelectedItem = car.state_New;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы jpg, jpeg, png, gif|*.jpeg;*.jpg;*.png;*.gif";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                label9.Text = OPF.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox1.TextLength < 1)
            {
                MessageBox.Show("Введите название. Название может включать в себя: марку автомобиля, модель, тип кузова и т.д.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedIndex < 0)
            {
               MessageBox.Show("Выберите тип коробки передач.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
            }
            if (textBox2.TextLength < 1)
            {
               MessageBox.Show("Введите пробег(в километрах)", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
            }
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип двигателя.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox3.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите состояние автомобиля.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox4.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип автомобиля.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox3.TextLength < 1)
            {
                MessageBox.Show("Введите цену автомобиля, за которую вы хотите продать его.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (update)
            {
                SqlDataReader sqlReader = null;
                SqlCommand comand = new SqlCommand("UPDATE [Cars] SET [Name] = @Name, [Year] = @Year, [Price] = @Price, [State_Buy] = @State_Buy, [Description] = @Description, [Engine_Type] = @Engine_Type, [Mileage] = @Mileage, [State_Broken] = @State_Broken, [State_New] = @State_New, [Transmission] = @Transmission, [Image] = @Image WHERE [cars].ID = @Car_ID ", LoginForm.sqlConnection);
                comand.Parameters.AddWithValue("Name", textBox1.Text);
                comand.Parameters.AddWithValue("Year", numericUpDown1.Value);
                comand.Parameters.AddWithValue("Price", textBox3.Text);
                comand.Parameters.AddWithValue("State_Buy", 0);
                comand.Parameters.AddWithValue("Description", textBox4.Text);
                comand.Parameters.AddWithValue("Engine_Type", comboBox2.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Mileage", textBox2.Text);
                comand.Parameters.AddWithValue("State_Broken", comboBox3.SelectedIndex + 1);
                comand.Parameters.AddWithValue("State_New", comboBox4.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Transmission", comboBox1.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Image", label9.Text);
                comand.Parameters.AddWithValue("Car_ID", car.id);
                try
                {
                    sqlReader = await comand.ExecuteReaderAsync();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            }
            else
            {
                SqlDataReader sqlReader = null;
                SqlCommand comand = new SqlCommand("INSERT INTO [Cars] ([Name], [Year], [Price], [State_Buy], [Description], [Engine_Type], [Mileage], [State_Broken], [State_New], [Transmission], [Image], [Seller_ID]) VALUES (@Name, @Year, @Price, @State_Buy, @Description, @Engine_Type, @Mileage, @State_Broken, @State_New, @Transmission, @Image, @Seller_ID)", LoginForm.sqlConnection);
                comand.Parameters.AddWithValue("Name", textBox1.Text);
                comand.Parameters.AddWithValue("Year", numericUpDown1.Value);
                comand.Parameters.AddWithValue("Price", textBox3.Text);
                comand.Parameters.AddWithValue("State_Buy", 0);
                comand.Parameters.AddWithValue("Description", textBox4.Text);
                comand.Parameters.AddWithValue("Engine_Type", comboBox2.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Mileage", textBox2.Text);
                comand.Parameters.AddWithValue("State_Broken", comboBox3.SelectedIndex + 1);
                comand.Parameters.AddWithValue("State_New", comboBox4.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Transmission", comboBox1.SelectedIndex + 1);
                comand.Parameters.AddWithValue("Image", label9.Text);
                comand.Parameters.AddWithValue("Seller_ID", userID);
                try
                {
                    sqlReader = await comand.ExecuteReaderAsync();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            }

            this.Close();
        }
    }
}
