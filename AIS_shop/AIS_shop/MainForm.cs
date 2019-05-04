using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show();
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            UserStateChange();
            dataGridView.RowHeadersVisible = false;
            buttonFilters.Enabled = false;
            administrationToolStripMenuItem.Visible = true;
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
            Cursor = Cursors.WaitCursor;
            progressBar.Value = 0;
            if (string.IsNullOrWhiteSpace(QueryToUpdate))
            {
                MessageBox.Show("Неверная команда! Будет выполнен запрос по-умолчанию.", "Ошибка!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                QueryToUpdate = @"SELECT * FROM Products";
            }
            string WhereInStock = @" WHERE Склад>0";
            string AndInStock = @" AND Склад>0";
            string Sort = @" ORDER BY Производитель";
            progressBar.Value = 10;
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
            progressBar.Value = 25;
            
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                await connection.OpenAsync();
                progressBar.Value = 60;
                Thread.Sleep(1000);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryToUpdate, connection);
                DataSet dataSet = new DataSet();
                sqlAdapter.Fill(dataSet);

                progressBar.Value = 95;
                Thread.Sleep(1000);
                dataGridView.DataSource = dataSet.Tables[0];
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[15].Visible = false;
                dataGridView.Columns[16].Visible = false;
                progressBar.Value = 100;
                
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
                progressBar.Value = 0;
            }
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
                loginToolStripMenuItem.Visible = true;
                registerToolStripMenuItem.Visible = true;
                goToPersonalAreaToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = false;
                administrationToolStripMenuItem.Visible = false;
                textBoxUser.Visible = false;
            }
            else
            {
                textBoxUser.Text = $"{user.Surname} {user.Name} {user.Patronymic}";
                textBoxUser.Visible = true;
                if (user.Status == UserStatus.Admin)
                    administrationToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                registerToolStripMenuItem.Visible = false;
                goToPersonalAreaToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = true;
            }
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram about = new AboutProgram();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void goToPersonalAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.Logout();
            UserStateChange();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            auth.ShowDialog();
            UserStateChange();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChange();
        }

        private void productManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductManagement change = new ProductManagement();
            change.ShowDialog();
            updateDataInGridView();
        }

        private void registerNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UserStateChange();
        }

        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            ExcelApplication ExcelApp = new ExcelApplication();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "№п/п";
            ExcelApp.Cells[1, 2] = "Число";
            ExcelApp.Cells[1, 3] = "Название";
            ExcelApp.Cells[1, 4] = "Количество";
            ExcelApp.Cells[1, 5] = "Цена ОПТ";
            ExcelApp.Cells[1, 6] = "Цена Розница";
            ExcelApp.Cells[1, 7] = "Сумма";

            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = (dataGridView[i, j].Value).ToString();
                }
            }
            ExcelApp.Visible = true;
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
