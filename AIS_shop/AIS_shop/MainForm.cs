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
        public static string CurrentTable { private set; get; }
        internal static User UserInSystem{ set; get; } = null;

        public MainForm()
        {
            InitializeComponent();
            buttonFilters.Enabled = false;
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

        private void bRefresh_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedIndex >= 0)
            {
                CurrentTable = comboBoxCategory.SelectedItem.ToString();
                // команда получения таблицы-представления
                string sqlCommand = "SELECT * FROM [v" + CurrentTable + "] ORDER BY [Brand]";
                loadDataToGridView(sqlCommand);
            }
            else MessageBox.Show("Выберите категорию товаров", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonFilters_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem != null)
            {
                QueryToUpdate = "";
                Filters filters = new Filters();
                filters.ShowDialog();
                if (QueryToUpdate != "")
                    loadDataToGridView(QueryToUpdate);
            }
            else MessageBox.Show("Выберите категорию товаров", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void управлениеТоварамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_product change = new Change_product();
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
