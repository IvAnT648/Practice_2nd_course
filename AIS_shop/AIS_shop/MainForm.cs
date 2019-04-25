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
        // для взаимодействия других форм с главной
        public static bool flagClose { set; get; } = false;
        public static string QueryToUpdate { set; get; }
        public static string CurrentTable { set; get; }
        internal static User UserInSystem{ get; set; } = null;

        public MainForm()
        {
            InitializeComponent();
            button1.Enabled = false;
            flagClose = false;
            QueryToUpdate = "";
            администрированиеToolStripMenuItem.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            if (flagClose)
                Close();
            Common.Crutch();
            UserStateChanged();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public async void loadDataToGridView(string sqlCommand)
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                await connection.OpenAsync();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand, connection);
                DataSet dataSet = new DataSet();
                sqlAdapter.Fill(dataSet);
                dataGridView.DataSource = dataSet.Tables[0];
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
                if (dataGridView.DataSource != null) button1.Enabled = true;
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
                QueryToUpdate = "";
                Filters filters = new Filters();
                filters.ShowDialog();
                if (QueryToUpdate != "")
                    loadDataToGridView(QueryToUpdate);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTable = comboBox1.SelectedItem.ToString();
            // команда получения таблицы-представления
            string sqlCommand = "SELECT * FROM [v" + CurrentTable + "]";
            loadDataToGridView(sqlCommand);
        }

        private void UserStateChanged()
        {
            if (UserInSystem == null)
            {
                войтиToolStripMenuItem.Visible = true;
                зарегистрироватьсяToolStripMenuItem.Visible = true;
                перейтиВЛичныйКабинетToolStripMenuItem.Visible = false;
                выйтиИзУчетнойЗаписиToolStripMenuItem.Visible = false;
                администрированиеToolStripMenuItem.Visible = false;
            }
            else
            {
                if (UserInSystem.Status == UserStatus.Admin)
                    администрированиеToolStripMenuItem.Visible = true;
                войтиToolStripMenuItem.Visible = false;
                зарегистрироватьсяToolStripMenuItem.Visible = false;
                перейтиВЛичныйКабинетToolStripMenuItem.Visible = true;
                выйтиИзУчетнойЗаписиToolStripMenuItem.Visible = true;
            }
        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            auth.ShowDialog();
            UserStateChanged();
        }

        private void выйтиИзУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInSystem = null;
            UserStateChanged();
        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChanged();
        }

        private void добавитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChanged();
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Left)
            {
                Product product = new Product();
                product.row = dataGridView.SelectedRows[0];
                product.ShowDialog();
            }
        }
    }
}



/*
    
    try
    {
        connection = new SqlConnection(Common.StrSQLConnection);
        connection.Open();
    
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

*/
