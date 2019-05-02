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
        
        private void buttonCheckout_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Count != 0) loadData();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void loadData()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlCommand query = new SqlCommand(@"SELECT CONCAT(Производитель, ' ', Модель) AS Название, Цена FROM Products WHERE", connection);
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
