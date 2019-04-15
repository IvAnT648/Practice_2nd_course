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
        SqlConnection sqlConnection;

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
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
            // TO DO: msgbox подтверждение
            if (MessageBox.Show("Выход из программы", "Закрыть программу?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\git\Practice_2nd_course\AIS_shop\AIS_shop\DataBaseDET.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Computers]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Product_id"]) 
                        + "     " + Convert.ToString(sqlReader["Brand"]) 
                        + "     " + Convert.ToString(sqlReader["Model"]) 
                        + "     " + Convert.ToString(sqlReader["Model_CPU"])
                        //+ "     " + Convert.ToString(sqlReader["Speed_CPU_GHz"])
                        + "     " + Convert.ToString(sqlReader["Count_core"]) 
                        + "     " + Convert.ToString(sqlReader["GPU"])
                        //+ "     " + Convert.ToString(sqlReader["Capacity_mem_GPU"])
                        + "     " + Convert.ToString(sqlReader["Capacity_RAM_Gb"])
                        + "     " + Convert.ToString(sqlReader["Type_RAM"])
                        //+ "     " + Convert.ToString(sqlReader["Frequency_RAM_GHz"])
                        + "     " + Convert.ToString(sqlReader["Capacity_HDD_Gb"])
                        + "     " + Convert.ToString(sqlReader["Capacity_SSD_Gb"])
                        + "     " + Convert.ToString(sqlReader["OS"])
                        + "     " + Convert.ToString(sqlReader["PSU"])
                        + "     " + Convert.ToString(sqlReader["Cost"])
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "! " + ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
