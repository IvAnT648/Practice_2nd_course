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
    public partial class Form5 : Form
    {
        private Car car;
        private int userID;
        public Form5(Car _car, int _userID)
        {
            car = _car;
            userID = _userID;
            InitializeComponent();
        }

        public Form5(Car _car)
        {
            car = _car;
            InitializeComponent();
            button2.Enabled = false;
        }

        private async void Form5_Load(object sender, EventArgs e)
        {
            if (car.state_Buy == 1)
            {
                SqlDataReader sqlReader = null;
                SqlCommand comand = new SqlCommand("SELECT [Date], Buyer_Sure_Name = [Users].Sure_Name, Buyer_Name = [Users].Name FROM [Transactions], [Users] WHERE [Transactions].ID_Car = @ID_Car AND [Transactions].ID_Buyer = [Users].ID", LoginForm.sqlConnection);
                comand.Parameters.AddWithValue("ID_Car", car.id);

                try
                {
                    sqlReader = await comand.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();

                    label19.Text = Convert.ToString(sqlReader["Buyer_Sure_Name"]) + " " + Convert.ToString(sqlReader["Buyer_Name"]);
                    label21.Text = Convert.ToString(sqlReader["Date"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                    car.name += " (Продан)";
                    label19.Visible = true;
                    label20.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;

                    button2.Enabled = false;

                }
            }

            label1.Text = car.name;
            label9.Text = car.transmission;
            label10.Text = car.mileage.ToString();
            label11.Text = car.year.ToString();
            label12.Text = car.engine_Type;
            label13.Text = car.state_Broken;
            label14.Text = car.state_New;
            label15.Text = car.price.ToString();
            label17.Text = car.seller;

            textBox1.Text = car.descryption;
            
            if(car.image == "Фото не выбрано")
            {
                label23.Visible = true;
            } else
            {
                pictureBox1.ImageLocation = car.image;
            }           
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (car.state_Buy == 1)
            {
                MessageBox.Show("Вы не можете купить этот автомобиль, он уже продан.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (car.seller_id == userID)
            {
                MessageBox.Show("Вы не можете купить этот автомобиль, т. к. вы уже являетесь его владельцем.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlDataReader sqlReader = null;
            SqlCommand comand = new SqlCommand("UPDATE [Cars] SET [Cars].State_Buy = 1 WHERE [Cars].ID = @ID", LoginForm.sqlConnection);
            comand.Parameters.AddWithValue("ID", car.id);
            try
            {
                sqlReader = await comand.ExecuteReaderAsync();
                if (sqlReader != null)
                    sqlReader.Close();
                comand = new SqlCommand("INSERT INTO [Transactions] ([ID_Car], [ID_Seller], [ID_Buyer], [Date]) VALUES (@ID_Car, @ID_Seller, @ID_Buyer, @Date)", LoginForm.sqlConnection);
                comand.Parameters.AddWithValue("ID_Car", car.id);
                comand.Parameters.AddWithValue("ID_Seller", car.seller_id);
                comand.Parameters.AddWithValue("ID_Buyer", userID);
                comand.Parameters.AddWithValue("Date", DateTime.Now);
                sqlReader = await comand.ExecuteReaderAsync();
                                
                car.state_Buy = 1;
                label19.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                label22.Visible = true;

                button2.Enabled = false;

                MessageBox.Show("Поздравляем! Вы купили автомобиль " + car.name + " за " + car.price + " рублей.", "Отчет о покупке автомобиля", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (sqlReader != null)
                    sqlReader.Close();
                comand = new SqlCommand("SELECT [Date], Sure_Name = [Users].Sure_Name, Name = [Users].Name FROM [Transactions], [Users] WHERE [Transactions].ID_Car = @ID_Car AND [Transactions].ID_Buyer = [Users].ID", LoginForm.sqlConnection);
                comand.Parameters.AddWithValue("ID_Car", car.id);
                sqlReader = await comand.ExecuteReaderAsync();
                await sqlReader.ReadAsync();

                label19.Text = Convert.ToString(sqlReader["Sure_Name"]) + " " + Convert.ToString(sqlReader["Name"]);
                label21.Text = Convert.ToString(sqlReader["Date"]);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (sqlReader != null)
                    sqlReader.Close();
                return;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
    }
}
