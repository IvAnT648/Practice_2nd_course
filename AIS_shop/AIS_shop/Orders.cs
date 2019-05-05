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
    public partial class Orders : Form
    {
        private SqlConnection Connection { set; get; } = null;
        private SqlCommand Query { set; get; } = null;
        private SqlDataAdapter Adapter { set; get; } = null;
        private DataSet Data { set; get; } = null;

        public Orders()
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            updateDataGridView(@"SELECT * FROM Orders");

            dataGridView.Columns["Id"].HeaderText = "Номер заказа";
            dataGridView.Columns["Id"].ReadOnly = true;
            dataGridView.Columns["Customer_id"].HeaderText = "Id покупателя";
            dataGridView.Columns["Product_id"].HeaderText = "Id товара";
            dataGridView.Columns["Date"].HeaderText = "Дата заказа";
            dataGridView.Columns["Amount"].HeaderText = "Сумма заказа";
            dataGridView.Columns["Status"].HeaderText = "Статус заказа";
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    updateDataGridView(@"SELECT * FROM Orders");
                    break;
                case 1:
                    updateDataGridView(@"SELECT * FROM Orders WHERE Status<2");
                    break;
            }
        }

        private async void updateDataGridView(string command_text)
        {
            Connection = new SqlConnection(Common.StrSQLConnection);
            Query = new SqlCommand(command_text, Connection);
            Adapter = new SqlDataAdapter(Query);
            Data = new DataSet();
            try
            {
                await Connection.OpenAsync();
                Data.Clear();
                Adapter.Fill(Data);
                dataGridView.DataSource = Data.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            if (Adapter == null) return;

            Connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(Adapter);
                Adapter.Update(Data.Tables[0]);
                Data.Clear();
                Adapter.Fill(Data);
                dataGridView.DataSource = Data.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Проверьте корректность введенных данных. Все поля, кроме даты, имеют числовой формат. Дата записывается в виде чисел с разделителями.", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}