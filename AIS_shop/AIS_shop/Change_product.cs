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
    public partial class Change_product : Form
    {
        string tableName = null;

        public Change_product()
        {
            InitializeComponent();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {

        }

        private void Change_product_Load(object sender, EventArgs e)
        {
            loadCategories();
        }

        private async void loadCategories()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string str = "SELECT Name FROM Products";
                SqlCommand command = new SqlCommand(str, connection);
                await connection.OpenAsync();
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        comboBoxCategory.Items.Add(reader.GetValue(0).ToString());
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

        private async void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableName = comboBoxCategory.SelectedItem.ToString();
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string str = string.Format(@"SELECT * FROM [v{0}]", tableName);
                SqlCommand command = new SqlCommand(str, connection);
                await connection.OpenAsync();
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                            listViewProducts.Columns.Add(reader.GetName(i).ToString());
                       
                    }
                        
                    while (await reader.ReadAsync())
                    {
                        //for (int i = 0; i < reader.FieldCount; i++)
                            //listViewProducts.Columns[i].ListView.Items.Add(reader.GetValue(i).ToString());
                            //listViewProducts.Items.Add(reader.GetValue(i).ToString());
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
    }
}
