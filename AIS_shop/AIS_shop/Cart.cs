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
    public partial class Cart : Form
    {
        public Cart()
        {
            InitializeComponent();
        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        
        private async void buttonCheckout_Click(object sender, EventArgs e)
        {
            if (User.GetUser().Status == UserStatus.Guest)
            {
                MessageBox.Show("Вы не вошли в систему. Оформлять заказы могут только зарегистрированные пользователи.", "Некорректное действие",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите товар", "Некорректное действие", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            var id = dgv.SelectedCells[0].Value;
            int cost = (int)dgv.SelectedCells[2].Value;


            string text = $@"INSERT INTO Orders (Customer_id, Product_id, Date, Amount, Status) 
                        VALUES ({User.GetUser().Id},{id},@date,{cost},@status)";
            var connection = new SqlConnection(Common.StrSQLConnection);
            var query = new SqlCommand(text, connection);
            query.Parameters.AddWithValue("date", DateTime.Now.ToString());
            query.Parameters.AddWithValue("status", @"Обработка");
            try
            {
                await connection.OpenAsync();
                if (await query.ExecuteNonQueryAsync() != 0)
                    MessageBox.Show("Заказ успешно оформлен.", "Оформление заказа", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Заказ не был оформлен.", "Оформление заказа",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count != 0) loadData();
            else Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void loadData()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlCommand query = new SqlCommand(@"SELECT Id, CONCAT(Производитель, ' ', Модель) AS Название, Цена FROM Products WHERE", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query);
            DataSet ds = new DataSet();
            try
            {
                await connection.OpenAsync();
                int count = 0;
                foreach (var it in Common.ProductsInCart)
                {
                    if (count != 0) query.CommandText += @" OR";
                    query.CommandText += $@" Id={it.Product}";
                    count++;
                }
                adapter.Fill(ds);
                dgv.DataSource = ds.Tables[0];
                dgv.Columns[0].Visible = false;
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
    }
}
