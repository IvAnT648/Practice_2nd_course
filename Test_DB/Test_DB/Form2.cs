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

    public partial class Form2 : Form
    {
        private User user_Logined;
        private bool displaySoldCars = true;
        private Int16 orderedBy = 2;
        private int count;
        private List<Car> cars = new List<Car>();
       
        public Form2()
        {
            InitializeComponent();
        }

        public void LoginUser()
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
            user_Logined = login.userProperty;
            login.Close();
            if (user_Logined == null)
            {
                label1.Text = "Войдите или зарегистрируйтесь";
                разместитьОбъявлениеToolStripMenuItem.Enabled = false;
                отображатьПроданныеАвтомобилиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
                displaySoldCars = false;
                отображатьПроданныеАвтомобилиToolStripMenuItem.Enabled = false;
                редактироватьToolStripMenuItem.Enabled = false;
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
            } else
            {
                label1.Text = "Здравствуйте " + user_Logined.sureName + " " + user_Logined.name;
                разместитьОбъявлениеToolStripMenuItem.Enabled = true;
                отображатьПроданныеАвтомобилиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                displaySoldCars = true;
                отображатьПроданныеАвтомобилиToolStripMenuItem.Enabled = true;
                редактироватьToolStripMenuItem.Enabled = true;
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            LoginUser();
            updateList();
        }

        private void Form2_FormClosing(Object sender, FormClosedEventArgs e)
        {
 //           this.Parent.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void разместитьОбъявлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addCar();
        }

        private void addCar()
        {
            Form3 form3 = new Form3(user_Logined.id);
            form3.ShowDialog();
            updateList();
        }

        private async void updateList()
        {
            SqlDataReader sqlReader = null;
            String sqlComandString = "SELECT [Cars].Name, [Cars].Description, [Cars].Image, [Cars].ID, [Cars].Year, [Cars].Price, [Cars].State_Buy, Engine_Type = [Engine_Type].Name, [Cars].Mileage, State_Broken = [State_Broken].Name, State_New = [State_New].Name, Transmission = [Transmission].Name, [Seller_ID], Seller_Sure_Name = [Users].Sure_Name, Seller_Name = [Users].Name FROM[Cars], [Transmission], [Engine_Type], [State_Broken], [State_New], [Users] WHERE[Cars].Engine_Type = [Engine_Type].ID AND [Cars].State_Broken = [State_Broken].ID AND [Cars].State_New = [State_New].ID AND [Cars].Transmission = [Transmission].ID AND [Cars].Seller_ID = [Users].ID";
            if (!displaySoldCars)
            {
                sqlComandString += " AND [Cars].State_Buy = 0";
            }
            sqlComandString += " ORDER BY ";
            switch (orderedBy)
            {
                case 1:
                    sqlComandString += "[Year]";
                    break;
                case 2:
                    sqlComandString += "[Price]";
                    break;
                case 3:
                    sqlComandString += "[Mileage]";
                    break;
                case 4:
                    sqlComandString += "[Engine_Type]";
                    break;
                case 5:
                    sqlComandString += "[Name]";
                    break;
            }
            SqlCommand comand = new SqlCommand(sqlComandString, LoginForm.sqlConnection);

            try
            {
                sqlReader = await comand.ExecuteReaderAsync();
                listView1.Items.Clear();
                cars.Clear();
                while (await sqlReader.ReadAsync())
                {
                    Car car = new Car(Convert.ToString(sqlReader["Name"]), Convert.ToString(sqlReader["Description"]), Convert.ToString(sqlReader["Image"]), Convert.ToInt32(sqlReader["ID"]), Convert.ToInt32(sqlReader["Year"]), Convert.ToInt32(sqlReader["Price"]), Convert.ToInt32(sqlReader["State_Buy"]), Convert.ToString(sqlReader["Engine_Type"]), Convert.ToInt32(sqlReader["Mileage"]), Convert.ToString(sqlReader["State_Broken"]), Convert.ToString(sqlReader["State_New"]), Convert.ToString(sqlReader["Transmission"]), Convert.ToInt32(sqlReader["Seller_ID"]), (Convert.ToString(sqlReader["Seller_Sure_Name"]) + " " + Convert.ToString(sqlReader["Seller_Name"])));
                    cars.Add(car);
                    ListViewItem item = new ListViewItem(car.name);
                    item.SubItems.Add(car.transmission);
                    item.SubItems.Add(car.mileage.ToString());
                    item.SubItems.Add(car.year.ToString());
                    item.SubItems.Add(car.engine_Type);
                    item.SubItems.Add(car.state_Broken);
                    item.SubItems.Add(car.state_New);
                    item.SubItems.Add(car.price.ToString());
                    item.SubItems.Add(car.state_Buy_Str);
                    listView1.Items.Add(item);
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
        }

        private void поТипуАвтомобиляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поТипуАвтомобиляToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поПробегуToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поТипуДвигателяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСостояниюToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            orderedBy = 1;
            updateList();
        }

        private void поСтоимостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поТипуАвтомобиляToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            поПробегуToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поТипуДвигателяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСостояниюToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            orderedBy = 2;
            updateList();
        }

        private void поПробегуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поТипуАвтомобиляToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поПробегуToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            поТипуДвигателяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСостояниюToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            orderedBy = 3;
            updateList();
        }

        private void поТипуДвигателяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поТипуАвтомобиляToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поПробегуToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поТипуДвигателяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            поСостояниюToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            orderedBy = 4;
            updateList();
        }

        private void поСостояниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поТипуАвтомобиляToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поПробегуToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поТипуДвигателяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked;
            поСостояниюToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            orderedBy = 5;
            updateList();
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginUser();
            updateList();
        }

        private void отображатьПроданныеАвтомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (отображатьПроданныеАвтомобилиToolStripMenuItem.CheckState == System.Windows.Forms.CheckState.Checked)
            {
                displaySoldCars = true;
            } else
            {
                displaySoldCars = false;
            }
            updateList();
        }

        private void подробнееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5;
            if (user_Logined != null)
            {
                form5 = new Form5(cars[Convert.ToInt32(listView1.SelectedIndices[0])], user_Logined.id);
            } else
            {
                form5 = new Form5(cars[Convert.ToInt32(listView1.SelectedIndices[0])]);
            }
            form5.ShowDialog();
            updateList();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            int H = 0;
            H = 50;
            e.Graphics.DrawString("Каталог автомобилей торговой площадки Car Market", new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, 50, H);
            H += 50;

            e.Graphics.DrawString(columnHeader1.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 80, H);
            e.Graphics.DrawString(columnHeader2.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 420, H);
            e.Graphics.DrawString(columnHeader3.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 525, H);
            e.Graphics.DrawString(columnHeader4.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 580, H);
            e.Graphics.DrawString(columnHeader5.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 660, H);
            e.Graphics.DrawString(columnHeader6.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 750, H);
            e.Graphics.DrawString(columnHeader7.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 820, H);
            e.Graphics.DrawString(columnHeader8.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 920, H);
            e.Graphics.DrawString(columnHeader9.Text, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 990, H);
            H += 30;

            e.Graphics.DrawString("Страница " + (count / 34 + 1), new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Black, 990, 800);

            int i = 1;
            foreach (ListViewItem item in listView1.Items)
            {
                if (i < count)
                {
                    i++;
                    continue;
                }
                e.Graphics.DrawString(count.ToString() + ".", new Font("Times New Roman", 10), Brushes.Black, 50, H);
                e.Graphics.DrawString(item.Text, new Font("Times New Roman", 10), Brushes.Black, 80, H);
                e.Graphics.DrawString(item.SubItems[1].Text, new Font("Times New Roman", 9), Brushes.Black, 420, H);
                e.Graphics.DrawString(item.SubItems[2].Text, new Font("Times New Roman", 9), Brushes.Black, 525, H);
                e.Graphics.DrawString(item.SubItems[3].Text, new Font("Times New Roman", 9), Brushes.Black, 580, H);
                e.Graphics.DrawString(item.SubItems[4].Text, new Font("Times New Roman", 9), Brushes.Black, 660, H);
                e.Graphics.DrawString(item.SubItems[5].Text, new Font("Times New Roman", 9), Brushes.Black, 750, H);
                e.Graphics.DrawString(item.SubItems[6].Text, new Font("Times New Roman", 9), Brushes.Black, 820, H);
                e.Graphics.DrawString(item.SubItems[7].Text, new Font("Times New Roman", 9), Brushes.Black, 920, H);
                e.Graphics.DrawString(item.SubItems[8].Text, new Font("Times New Roman", 9), Brushes.Black, 990, H);
                H += 20;
                i++;
                count++;

                if (i % 34 == 0)
                {
                    e.HasMorePages = true;
                    break;
                }
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            count = 1;
            printDialog1.Document = printDocument1;
            Nullable<System.Windows.Forms.DialogResult> print = printDialog1.ShowDialog();
            if (print == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.PrinterSettings.DefaultPageSettings.Landscape = true;
                printDocument1.Print();
            }            
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            LoginUser();
            updateList();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cars[Convert.ToInt32(listView1.SelectedIndices[0])].seller_id == user_Logined.id)
            {
                Form3 form3 = new Form3(cars[Convert.ToInt32(listView1.SelectedIndices[0])]);
                form3.ShowDialog();
                updateList();
            } else
            {
                MessageBox.Show("Вы не имеете права редактировать данное объявление, т. к. вы не являетесь его владельцем.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void поToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void поСтоимостиToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void поПробегуToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void поТипуДвигателяToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void поНазваниюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            addCar();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (cars[Convert.ToInt32(listView1.SelectedIndices[0])].seller_id == user_Logined.id)
            {
                Form3 form3 = new Form3(cars[Convert.ToInt32(listView1.SelectedIndices[0])]);
                form3.ShowDialog();
                updateList();
            }
            else
            {
                MessageBox.Show("Вы не имеете права редактировать данное объявление, т. к. вы не являетесь его владельцем.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
            Form5 form5;
            if (user_Logined != null)
            {
                form5 = new Form5(cars[Convert.ToInt32(listView1.SelectedIndices[0])], user_Logined.id);
            }
            else
            {
                form5 = new Form5(cars[Convert.ToInt32(listView1.SelectedIndices[0])]);
            }
            form5.ShowDialog();
            updateList();
        }
    }
    public class Car
    {
        public String name, descryption, image, engine_Type, state_Broken, state_New, transmission, state_Buy_Str, seller;
        public int id, year, price, mileage, state_Buy, seller_id;
        public Car(String _name, String _descryption, String _image, int _id, int _year, int _price, int _state_Buy, String _engine_Type, int _mileage, String _state_Broken, String _state_New, String _transmission, int _seller_id, String _seller)
        {
            name = _name;
            descryption = _descryption;
            image = _image;
            id = _id;
            year = _year;
            price = _price;
            state_Buy = _state_Buy;
            state_Broken = _state_Broken;
            state_New = _state_New;
            transmission = _transmission;
            mileage = _mileage;
            engine_Type = _engine_Type;
            seller_id = _seller_id;
            seller = _seller;
            if (state_Buy == 0)
                state_Buy_Str = "Не продан";
            else
                state_Buy_Str = "Продан";
        }
    }
}
