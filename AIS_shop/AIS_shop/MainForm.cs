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
        public MainForm()
        {
            InitializeComponent();
        }

        private void loadDataToGridView(string table_name)
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                // команда получения таблицы представления
                string sqlCommand = "SELECT * FROM [v" + table_name + "]";
                
                connection.Open();
                
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand, connection);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                sqlAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
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
            if (comboBox1.SelectedItem != null)
            {
                Filters filters = new Filters(Convert.ToString(comboBox1.SelectedItem));
                filters.ShowDialog();
            }
            else MessageBox.Show("Выберите категорию товаров", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (MessageBox.Show("Закрыть программу?", "Выход из программы",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            Common.Crutch();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataToGridView(Convert.ToString(comboBox1.SelectedItem));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}