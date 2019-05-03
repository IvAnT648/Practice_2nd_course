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
        // Первая инициализация пользователя - как гость
        User user = User.GetUser();
        // для взаимодействия других форм с главной
        //--- SQL-запрос для обовления данных в dataGridView
        public static string QueryToUpdate { set; get; } = null;
        
        public MainForm()
        {
            InitializeComponent();
            buttonFilters.Enabled = false;
            //администрированиеToolStripMenuItem.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show();
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            UserStateChange();
            dataGridView.RowHeadersVisible = false;
            администрированиеToolStripMenuItem.Visible = true;
            QueryToUpdate = @"SELECT * FROM Products";
            updateDataInGridView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть программу?", "Выход из программы",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private async void updateDataInGridView()
        {
            if (string.IsNullOrWhiteSpace(QueryToUpdate))
            {
                MessageBox.Show("Неверная команда! Будет выполнен запрос по-умолчанию.", "Ошибка!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                QueryToUpdate = @"SELECT * FROM Products";
            }
            string WhereInStock = @" WHERE Склад>0";
            string AndInStock = @" AND Склад>0";
            string Sort = @" ORDER BY Производитель";
            if (QueryToUpdate.Contains("ORDER BY")) QueryToUpdate = QueryToUpdate.Replace(Sort, "");
            if (radioButtonStock.Checked)
            {
                // показать товары в наличии
                if (!QueryToUpdate.Contains("WHERE"))
                    QueryToUpdate += WhereInStock;
                else QueryToUpdate += AndInStock;
            }
            else 
            if (radioButtonAllProduct.Checked)
            {
                // показать все товары
                if (QueryToUpdate.Contains(WhereInStock))
                    QueryToUpdate = QueryToUpdate.Replace(WhereInStock, "");
                else
                if (QueryToUpdate.Contains(AndInStock))
                    QueryToUpdate = QueryToUpdate.Replace(AndInStock, "");
            }

            // сортировка
            if (!QueryToUpdate.Contains("ORDER BY")) QueryToUpdate += Sort;

            Cursor = Cursors.WaitCursor;
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                await connection.OpenAsync();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryToUpdate, connection);
                DataSet dataSet = new DataSet();
                sqlAdapter.Fill(dataSet);
                dataGridView.DataSource = dataSet.Tables[0];
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[15].Visible = false;
                dataGridView.Columns[16].Visible = false;
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
                Cursor = Cursors.Default;
            }
        }

        private void перейтиВЛичныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram about = new AboutProgram();
            about.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            auth.ShowDialog();
            UserStateChange();
        }

        private void выйтиИзУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.Logout();
            UserStateChange();
        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChange();
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.WaitCursor;
                Product product = new Product(dataGridView.SelectedRows[0]);
                product.ShowDialog();
                Cursor = Cursors.Default;
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            // очистка dataGridView
            dataGridView.DataSource = null;
            // загрузка данных из БД
            if (!string.IsNullOrWhiteSpace(QueryToUpdate))
                updateDataInGridView();
        }

        private void buttonFilters_Click(object sender, EventArgs e)
        {
            QueryToUpdate = null;
            Filters filters = new Filters();
            filters.ShowDialog();
            if (!string.IsNullOrWhiteSpace(QueryToUpdate))
                updateDataInGridView();
        }

        private void управлениеТоварамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductManagement change = new ProductManagement();
            change.ShowDialog();
        }

        private void управлениеУчетнымиЗаписямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
        }

        private void buttonCart_Click(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Cart cart = new Cart();
                cart.ShowDialog();
            }
        }

        private void UserStateChange()
        {
            if (user.Status == UserStatus.Guest)
            {
                войтиToolStripMenuItem.Visible = true;
                зарегистрироватьсяToolStripMenuItem.Visible = true;
                перейтиВЛичныйКабинетToolStripMenuItem.Visible = false;
                выйтиИзУчетнойЗаписиToolStripMenuItem.Visible = false;
                администрированиеToolStripMenuItem.Visible = false;
            }
            else
            {
                if (user.Status == UserStatus.Admin)
                    администрированиеToolStripMenuItem.Visible = true;
                войтиToolStripMenuItem.Visible = false;
                зарегистрироватьсяToolStripMenuItem.Visible = false;
                перейтиВЛичныйКабинетToolStripMenuItem.Visible = true;
                выйтиИзУчетнойЗаписиToolStripMenuItem.Visible = true;
            }
        }
    }
}



/*
    var connection = new SqlConnection(Common.StrSQLConnection);
    try
    {
        await connection.OpenAsync();
    
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
