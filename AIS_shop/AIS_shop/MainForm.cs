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
using System.Configuration;


namespace AIS_shop
{
    

    public partial class MainForm : Form
    {
        // строка соед. с БД
        public static string StrSQLConnection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        // для взаимодействия других форм с главной
        //--- SQL-запрос для обовления данных в dataGridView
        public static string QueryToUpdate { set; get; }
        //--- пользователь в системе (сделать Singleton)
        internal static User UserInSystem{ set; get; } = null;

        public MainForm()
        {
            InitializeComponent();
            buttonFilters.Enabled = false;
            QueryToUpdate = "";
            администрированиеToolStripMenuItem.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show();
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            UserStateChange();
            loadDataToGridView(@"SELECT * FROM vComputers ORDER BY Brand");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private async void loadDataToGridView(string sqlCommand)
        {
            SqlConnection connection = new SqlConnection(StrSQLConnection);
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
                if (dataGridView.DataSource != null) buttonFilters.Enabled = true;
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

        private void UserStateChange()
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
            UserStateChange();
        }

        private void выйтиИзУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInSystem = null;
            UserStateChange();
        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChange();
        }

        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChange();
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Left)
            {
                Product product = new Product(dataGridView.SelectedRows[0]);
                product.ShowDialog();
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            // команда получения таблицы-представления
            string sqlCommand = @"SELECT * FROM vComputers ORDER BY Brand";
            loadDataToGridView(sqlCommand);
        }

        private void buttonFilters_Click(object sender, EventArgs e)
        {
            QueryToUpdate = null;
            Filters filters = new Filters();
            filters.ShowDialog();
            if (!string.IsNullOrWhiteSpace(QueryToUpdate))
                loadDataToGridView(QueryToUpdate);
        }

        private void управлениеТоварамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductManager change = new ProductManager();
            change.ShowDialog();
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
