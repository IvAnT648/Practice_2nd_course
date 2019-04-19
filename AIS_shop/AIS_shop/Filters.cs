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
    public partial class Filters : Form
    {
        private List<FilterChecked> filtersChecked = null;
        private List<FilterFromTo> filtersFromTo = null;
        private string tableName;

        public Filters()
        {
            InitializeComponent();
        }

        public Filters(string name_of_table)
        {
            InitializeComponent();
            this.tableName = name_of_table;
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            List<string> variants = new List<string>();
            try
            {
                connection.Open();

                Table table = Common.tablesForFilters.Find(item => item.name == this.tableName);

                foreach (Field field in table.fields)
                {
                    SqlCommand command = new SqlCommand(field.sqlCommand, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    if (field.num == 1)
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                           variants.Add(Convert.ToString(row.ItemArray[0]));
                        ds.Clear();
                        
                        if (filtersChecked == null)
                            filtersChecked = new List<FilterChecked>();
                        filtersChecked.Add(new FilterChecked(field.name));

                        foreach (string variant in variants)
                            filtersChecked[filtersChecked.Count-1].checkedList.Items.Add(variant);
                        
                        variants.Clear();
                    }
                    else if (field.num == 2)
                    {
                        if (filtersFromTo == null)
                            filtersFromTo = new List<FilterFromTo>();
                        filtersFromTo.Add(new FilterFromTo(field.name));
                    }
                }
                foreach (var filter in filtersChecked)
                    flowLayoutPanel1.Controls.Add(filter.groupBox);
                foreach (var filter in filtersFromTo)
                    flowLayoutPanel1.Controls.Add(filter.groupBox);
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

        private void Filters_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // применяем фильтры
            // возможно, обновляем список товаров здесь
            // сохранить состояние фильтров
            Hide();
        }

        
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}