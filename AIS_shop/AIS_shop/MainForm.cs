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
        private string sqlCommand = "SELECT * FROM [vComputers]";

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadDataToGridView()
        {
            try
            {
                // TO DO: в зависимости от выбранной категории товаров
                // поместить в sqlCommand соответствующую команду на вывод представления
                //


                Common.SqlConnection = new SqlConnection(Common.StrSQLConnection);
                Common.SqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand, Common.SqlConnection);
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
                if (Common.SqlConnection != null && Common.SqlConnection.State != ConnectionState.Closed)
                    Common.SqlConnection.Close();
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
                if (Common.SqlConnection != null && Common.SqlConnection.State != ConnectionState.Closed)
                    Common.SqlConnection.Close();
                Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            loadDataToGridView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Common.SqlConnection != null && Common.SqlConnection.State != ConnectionState.Closed)
                Common.SqlConnection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}