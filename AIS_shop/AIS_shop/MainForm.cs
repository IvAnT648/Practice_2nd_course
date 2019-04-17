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
    public partial class MainForm : Form
    {
        private const string sqlCommand =
            "SELECT [Бренд], [Модель], [Производитель CPU], [Модель CPU], [Кол-во ядер], [Производитель GPU], [Модель GPU], [Тип RAM], [Объем RAM], " +
                "[Объем HDD], [Объем SSD], [Операционная система], [Мощность БП], [Цена] FROM [Computers]";

        private SqlConnection sqlConnection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadDataToGridView()
        {
            sqlConnection = new SqlConnection(Constants.connectionStringToDB);
            sqlConnection.Open();
            try
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand, sqlConnection);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                sqlAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "!!! " + ex.Source.ToString(), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
        }

        private void личныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void перейтиВЛичныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }

        private void корзинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Filters filters = new Filters();
            filters.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram about = new AboutProgram();
            about.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выход из программы", "Закрыть программу?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            Show();
            loadDataToGridView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}