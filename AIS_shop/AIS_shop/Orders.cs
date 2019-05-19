using System;
using System.Data;
using System.Data.SqlClient;
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
            UpdateDataGridView(@"SELECT * FROM Orders");

            dataGridView.Columns["Id"].HeaderText = "Номер заказа";
            dataGridView.Columns["Id"].ReadOnly = true;
            dataGridView.Columns["Customer_id"].HeaderText = "Id покупателя";
            dataGridView.Columns["Customer_id"].ReadOnly = true;
            dataGridView.Columns["Product_id"].HeaderText = "Id товара";
            dataGridView.Columns["Product_id"].ReadOnly = true;
            dataGridView.Columns["Date"].HeaderText = "Дата заказа";
            dataGridView.Columns["Date"].ReadOnly = true;
            dataGridView.Columns["Amount"].HeaderText = "Сумма заказа";
            dataGridView.Columns["Amount"].ReadOnly = true;
            dataGridView.Columns["Status"].HeaderText = "Статус заказа";
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    UpdateDataGridView(@"SELECT * FROM Orders");
                    break;
                case 1:
                    UpdateDataGridView(@"SELECT * FROM Orders WHERE Status<2");
                    break;
            }
        }

        private async void UpdateDataGridView(string command_text)
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
            if (MessageBox.Show("Вы уверены, что хотите применить изменения?", "Подтверждение действия", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}