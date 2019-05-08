using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Cart : Form
    {
        public Cart()
        {
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count == 0) Close();
            LoadData();
        }

        private async void LoadData()
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
                    query.CommandText += $@" Id={it}";
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

        private async void buttonCheckout_Click(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count == 0)
            {
                MessageBox.Show("В корзине нет товаров", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар!", "Товар не выбран",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (User.GetUser().Status == UserStatus.Guest)
            {
                MessageBox.Show("Вы не вошли в систему. Оформлять заказы могут только зарегистрированные пользователи.", "Некорректное действие",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите оформить этот заказ?", "Подтверждение действия", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            
            int id = (int)dgv.SelectedCells[0].Value;
            int cost = (int)dgv.SelectedCells[2].Value;

            string text = $@"INSERT INTO Orders (Customer_id, Product_id, Date, Amount, Status) 
                        VALUES ({User.GetUser().Id},{id},@date,{cost},0)";
            var connection = new SqlConnection(Common.StrSQLConnection);
            var query = new SqlCommand(text, connection);
            query.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            try
            {
                await connection.OpenAsync();
                if (await query.ExecuteNonQueryAsync() != 0)
                {
                    MessageBox.Show("Заказ успешно оформлен.", "Оформление заказа",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = (int)dgv.SelectedCells[0].Value;

                    if (Common.ProductsInCart.Count != 0)
                    {
                        Common.ProductsInCart.Remove(
                            Common.ProductsInCart.Find(f => f == id));
                        dgv.Rows.Remove(dgv.SelectedRows[0]);
                    }
                }
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

        private void buttonDeleteFromCart_Click(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count == 0)
            {
                MessageBox.Show("В корзине нет товаров", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Вы уверены что хотите удалить товар из корзины?", "Подтверждение действия",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int id = (int)dgv.SelectedCells[0].Value;

            if (Common.ProductsInCart.Count != 0)
            {
                Common.ProductsInCart.Remove(
                    Common.ProductsInCart.Find(f => f == id));
                MessageBox.Show("Товар удален из корзины", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgv.Rows.Remove(dgv.SelectedRows[0]);
            }
        }
    }
}