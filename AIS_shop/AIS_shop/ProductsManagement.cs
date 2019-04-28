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
    public partial class ProductsManagement : Form
    {
        public static bool updateFlag = false;
        public ProductsManagement()
        {
            InitializeComponent();
        }

        private async void updateListViewData()
        {
            if (listView.Items.Count != 0) listView.Items.Clear();
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string str = @"SELECT * FROM Products";
                SqlCommand command = new SqlCommand(str, connection);
                await connection.OpenAsync();
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    bool printTitles = true;
                    while (await reader.ReadAsync())
                    {
                        if (printTitles)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                listView.Columns.Add(reader.GetName(i).ToString());
                            printTitles = false;
                        }
                        ListViewItem item = new ListViewItem(reader.GetValue(0).ToString());
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            item.SubItems.Add(reader.GetValue(i).ToString());
                        }
                        listView.Items.Add(item);
                    }
                }
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
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddNewProduct newProduct = new AddNewProduct();
            newProduct.ShowDialog();
            if (updateFlag)
            {
                updateListViewData();
                updateFlag = false;
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct update = new UpdateProduct();
            update.ShowDialog();
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                string title = "Подтвердите действие";
                string qst = "Вы уверены, что хотите удалить выбранную запись из таблицы?";
                string recordToDelete = null;

                foreach (ListViewItem item in listView.SelectedItems)
                {
                    qst += "\nЗапись \"";
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 0) recordToDelete += " | ";
                        recordToDelete += item.SubItems[i].Text;
                    }
                }
                recordToDelete += "\"";
                if (recordToDelete != null) qst += recordToDelete;
                if (MessageBox.Show(qst, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
                    try
                    {
                        connection.Open();
                        SqlCommand query = new SqlCommand();
                        query.CommandText = string.Format(@"DELETE FROM Products WHERE Id={0}", listView.SelectedItems[0].Text);
                        query.Connection = connection;
                        if (query.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show("Удаление записи \"" + recordToDelete + "\" успешно выполнено", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            updateListViewData();
                        }
                        else MessageBox.Show("Удаление записи \'" + recordToDelete + "\' не выполнено", "Операция невыполнена", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                else listView.SelectedItems[0].Selected = false;
            }
            else MessageBox.Show("Вы не выбрали товар для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ProductManager_Load(object sender, EventArgs e)
        {
            updateListViewData();
        }
    }
}
