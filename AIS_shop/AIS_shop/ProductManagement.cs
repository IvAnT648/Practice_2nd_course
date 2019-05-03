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
    public partial class ProductManagement : Form
    {
        public static bool updateFlag = false;
        public ProductManagement()
        {
            InitializeComponent();
        }

        private void ProductManager_Load(object sender, EventArgs e)
        {
            dataGridView.RowHeadersVisible = false;
            updateData();
        }

        private void updateData()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT * FROM Products", connection);
            DataSet data = new DataSet();
            try
            {
                connection.Open();
                adapter.Fill(data);
                dataGridView.DataSource = data.Tables[0];
                dataGridView.Columns[0].Visible = false;
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
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddProduct newProduct = new AddProduct();
            newProduct.ShowDialog();
            if (updateFlag)
            {
                updateData();
                updateFlag = false;
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                ChangeProduct change = new ChangeProduct(dataGridView.SelectedRows[0]);
                change.ShowDialog();
                if (updateFlag)
                {
                    updateData();
                    updateFlag = false;
                }
            }
            else MessageBox.Show("Вы не выбрали товар для изменения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                string title = "Подтвердите действие";
                string qst = "Вы уверены, что хотите удалить выбранный товар из базы данных?";
                string recordToDelete = null;
                qst += "\nЗапись \"";
                for (int i = 0; i < 4; i++)
                {
                    if (i != 0) recordToDelete += " | ";
                    recordToDelete += dataGridView.SelectedCells[i].Value.ToString();
                }
                recordToDelete += "\"";
                if (recordToDelete != null) qst += recordToDelete;
                if (MessageBox.Show(qst, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
                    try
                    {
                        connection.Open();
                        SqlCommand query = new SqlCommand();
                        query.CommandText = string.Format($@"DELETE FROM Products WHERE Id={dataGridView.SelectedCells[0].Value}");
                        query.Connection = connection;
                        if (query.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show($"Удаление записи \"{recordToDelete}\" успешно выполнено", "Операция выполнена", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            updateData();
                        }
                        else MessageBox.Show($"Удаление записи \"{recordToDelete}\" не выполнено", "Операция невыполнена", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                else dataGridView.ClearSelection();
            }
            else MessageBox.Show("Вы не выбрали товар для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
    }
}
