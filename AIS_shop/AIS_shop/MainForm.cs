using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

namespace AIS_shop
{
    public partial class MainForm : Form
    {
        // Первая инициализация пользователя - как гость
        User user = User.GetUser();
        // SQL-запрос для обовления данных в dataGridView
        public static string QueryToUpdate { set; get; } = null;
        public static bool flagToRefresh = false;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            administrationToolStripMenuItem.Visible = false;
            buttonFilters.Enabled = false;
            Show();
            Welcome welcome = new Welcome();
            welcome.ShowDialog();
            UpdateControls();
            QueryToUpdate = @"SELECT * FROM Products";
            UpdateDataGridView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть программу?", "Выход из программы",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private async void UpdateDataGridView()
        {
            Cursor = Cursors.WaitCursor;
            progressBar.Value = 0;
            if (string.IsNullOrWhiteSpace(QueryToUpdate))
                QueryToUpdate = @"SELECT * FROM Products";

            string WhereInStock = @" WHERE Склад>0";
            string AndInStock = @" AND Склад>0";
            string Sort = @" ORDER BY Производитель";
            progressBar.Value = 10;
            if (QueryToUpdate.Contains(Sort)) QueryToUpdate = QueryToUpdate.Replace(Sort, "");
            if (radioButtonStock.Checked)
            {
                // показать товары в наличии
                if (!QueryToUpdate.Contains("WHERE"))
                    QueryToUpdate += WhereInStock;
                else
                if (!QueryToUpdate.Contains("Склад>0"))
                    QueryToUpdate += AndInStock;
            }
            else
            if (radioButtonAllProduct.Checked)
            {
                // показать все товары
                if (QueryToUpdate.Contains(WhereInStock))
                    QueryToUpdate = QueryToUpdate.Replace(WhereInStock, "");
                if (QueryToUpdate.Contains(AndInStock))
                    QueryToUpdate = QueryToUpdate.Replace(AndInStock, "");
            }

            // сортировка
            if (!QueryToUpdate.Contains("ORDER BY")) QueryToUpdate += Sort;
            progressBar.Value = 30;

            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                progressBar.Value = 35;
                await connection.OpenAsync();
                progressBar.Value = 60;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryToUpdate, connection);
                DataSet dataSet = new DataSet();
                sqlAdapter.Fill(dataSet);
                progressBar.Value = 95;
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

        private void UpdateControls()
        {
            if (user.Status == UserStatus.Guest)
            {
                loginToolStripMenuItem.Visible = true;
                registerToolStripMenuItem.Visible = true;
                goToPersonalAreaToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = false;
                administrationToolStripMenuItem.Visible = false;
                textBoxUser.Visible = false;
                buttonCart.Enabled = true;
            }
            else
            {
                if (user.Status == UserStatus.Admin)
                {
                    administrationToolStripMenuItem.Visible = true;
                    buttonCart.Enabled = false;
                }
                else administrationToolStripMenuItem.Visible = false;
                textBoxUser.Text = $"{user.Surname} {user.Name} {user.Patronymic}";
                textBoxUser.Visible = true;
                loginToolStripMenuItem.Visible = false;
                registerToolStripMenuItem.Visible = false;
                goToPersonalAreaToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = true;
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
            UpdateDataGridView();
        }

        private void buttonFilters_Click(object sender, EventArgs e)
        {
            QueryToUpdate = null;
            Filters filters = new Filters();
            filters.ShowDialog();
            if (flagToRefresh && !string.IsNullOrWhiteSpace(QueryToUpdate))
                UpdateDataGridView();
        }

        private void buttonCart_Click(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Сообщение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Cart cart = new Cart();
                cart.ShowDialog();
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
            switch (user.Status)
            {
                case UserStatus.Admin:
                    AdminProfile adminProfile = new AdminProfile();
                    adminProfile.ShowDialog();
                    break;
                case UserStatus.Customer:
                    Profile profile = new Profile();
                    profile.ShowDialog();
                    break;
                case UserStatus.Guest:
                    MessageBox.Show("Сначала авторизируйтесь.", "Переход в личный кабинет",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (user.Status != UserStatus.Guest)
            {
                if (MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Подтверждение действия", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    User.Logout();
                    Common.ProductsInCart.Clear();
                    UpdateControls();
                }
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            auth.ShowDialog();
            UpdateControls();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UpdateControls();
        }

        private void productManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (user.Status == UserStatus.Admin)
            {
                ProductManagement change = new ProductManagement();
                change.ShowDialog();
                UpdateDataGridView();
            }
            else MessageBox.Show("Недостаточно полномочий для завершения этого действия.", 
                "Некорректное действие!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void registerNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            UpdateControls();
        }

        private void orderManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (user.Status == UserStatus.Admin)
            {
                Orders orders = new Orders();
                orders.ShowDialog();
            }
            else MessageBox.Show("Недостаточно полномочий для завершения этого действия.", 
                "Некорректное действие!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Список товаров пуст!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Document = new PrintDocument();
            Document.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printPreviewDialog = new PrintPreviewDialog
            {
                Width = 1200,
                Height = 1200,
                Document = Document
            };
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                Document.PrinterSettings = printDialog.PrinterSettings;
                Document.DefaultPageSettings.Landscape = true;
                printPreviewDialog.ShowDialog();
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int x = 30;
            int y = 30;
            int cell_height = 0;

            int colCount = dataGridView.ColumnCount-2;
            int rowCount = dataGridView.RowCount;

            Font font = new Font("Times New Roman", 7, FontStyle.Regular, GraphicsUnit.Point);

            int[] widthC = new int[colCount];

            int current_col = 0;
            int current_row = 0;

            while (current_col < colCount)
            {
                if (g.MeasureString(dataGridView.Columns[current_col].HeaderText.ToString(), font).Width > widthC[current_col])
                {
                    widthC[current_col] = (int)g.MeasureString(dataGridView.Columns[current_col].HeaderText.ToString(), font).Width + 20;
                }
                current_col++;
            }

            while (current_row < rowCount)
            {
                while (current_col < colCount)
                {
                    if (g.MeasureString(dataGridView[current_col, current_row].Value.ToString(), font).Width > widthC[current_col])
                    {
                        widthC[current_col] = (int)g.MeasureString(dataGridView[current_col, current_row].Value.ToString(), font).Width + 20;
                    }
                    current_col++;
                }
                current_col = 0;
                current_row++;
            }

            current_col = 0;
            current_row = 0;

            string value = "";

            int width = widthC[current_col];
            int height = dataGridView[current_col, current_row].Size.Height;

            Rectangle cell_border;
            SolidBrush brush = new SolidBrush(Color.Black);


            while (current_col < colCount)
            {
                width = widthC[current_col];
                cell_height = dataGridView[current_col, current_row].Size.Height;
                cell_border = new Rectangle(x, y, width, height);
                value = dataGridView.Columns[current_col].HeaderText.ToString();
                g.DrawRectangle(new Pen(Color.Black), cell_border);
                g.DrawString(value, font, brush, x, y);
                x += widthC[current_col];
                current_col++;
            }
            while (current_row < rowCount + 1)
            {
                while (current_col < colCount)
                {
                    width = widthC[current_col];
                    cell_height = dataGridView[current_col, current_row - 1].Size.Height;
                    cell_border = new Rectangle(x, y, width, height);
                    value = dataGridView[current_col, current_row - 1].Value.ToString();

                    g.DrawRectangle(new Pen(Color.Black), cell_border);
                    g.DrawString(value, font, brush, x, y);
                    x += widthC[current_col];
                    current_col++;
                }
                current_col = 0;
                current_row++;
                x = 30;
                y += cell_height;
            }
        }

        // экспортируются все ячейки, кроме описания и изображения
        private void buttonExportToCSV_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Список товаров пуст!", "Ошибка!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (saveCsvFile.ShowDialog() == DialogResult.OK)
            {
                string fileInStr = null;
                
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count-2; j++)
                        fileInStr += dataGridView[j, i].Value.ToString() + ";";
                    fileInStr += "\n";
                    progressBar.Value += 90 / dataGridView.RowCount;
                }
                progressBar.Value = 100;
                StreamWriter streamWriter = new StreamWriter(saveCsvFile.FileName, false, 
                    Encoding.GetEncoding("Windows-1251"));
                streamWriter.Write(fileInStr);
                streamWriter.Close();
                saveCsvFile.FileName = "Products_DETShop.csv";
                progressBar.Value = 0;
            }
        }
        // экспортируются все ячейки, кроме описания и изображения
        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Список товаров пуст!", "Ошибка!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (saveExcelFile.ShowDialog() == DialogResult.OK)
            {
                ExcelApplication ExcelApp = new ExcelApplication();
                progressBar.Value = 0;
                Excel.Workbook workbook = ExcelApp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                ExcelApp.Columns.ColumnWidth = 20;
                ExcelApp.Cells[1, 1] = "Id товара";
                ExcelApp.Cells[1, 2] = "Тип ПК";
                ExcelApp.Cells[1, 3] = "Производитель";
                ExcelApp.Cells[1, 4] = "Модель";
                ExcelApp.Cells[1, 5] = "Процессор";
                ExcelApp.Cells[1, 6] = "Кол-во ядер";
                ExcelApp.Cells[1, 7] = "Видеокарта";
                ExcelApp.Cells[1, 8] = "Объем RAM";
                ExcelApp.Cells[1, 9] = "Тип RAM";
                ExcelApp.Cells[1, 10] = "Объем HDD";
                ExcelApp.Cells[1, 11] = "Объем SSD";
                ExcelApp.Cells[1, 12] = "Операционная система";
                ExcelApp.Cells[1, 13] = "Блок питания";
                ExcelApp.Cells[1, 14] = "Кол-во на складе";
                ExcelApp.Cells[1, 15] = "Цена";
                progressBar.Value += 30;
                for (int i = 0; i < dataGridView.Columns.Count - 2; i++)
                {
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        ExcelApp.Cells[j + 2, i + 1] = dataGridView[i, j].Value.ToString();
                    }
                    progressBar.Value += 66 / dataGridView.Columns.Count - 2;
                }
                progressBar.Value = 99;
                ExcelApp.AlertBeforeOverwriting = false;
                ExcelApp.DisplayAlerts = false;
                workbook.SaveAs(saveExcelFile.FileName);
                ExcelApp.Quit();
                progressBar.Value = 100;
                saveExcelFile.FileName = "Products_DETShop.xlsx";
                progressBar.Value = 0;
            }
        }
    }
}