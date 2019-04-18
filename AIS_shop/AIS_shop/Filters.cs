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
        // контейнер структур из 3 строк (1 - назв. табл., 2 - назв. поля, 3 - sql-команда)
        // для получения уникальных записей (для формирования фильров)
        private List<sub1_ForFiltersForm> sqlCommands = null;

        private List<FilterChecked> filtersChecked = null;
        private List<FilterFromTo> filtersFromTo = null;

        public Filters()
        {
            InitializeComponent();
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            try
            {
                /*
                filtersFromTo.Add(new FilterFromTo("Ch2"));
                
                
                filtersFromTo.Add(new FilterFromTo("Ch4"));
                  
                foreach (var it in filtersChecked)
                    flowLayoutPanel1.Controls.Add(it.groupBox);
                
                foreach (var it in filtersFromTo)
                    flowLayoutPanel1.Controls.Add(it.groupBox);
                    */
                if (sqlCommands == null)
                    sqlCommands = new List<sub1_ForFiltersForm>();

                foreach (var table in Common.fieldsForFilters)
                    foreach (var field in table.fields)
                        sqlCommands.Add(new sub1_ForFiltersForm(
                                table.name, field, "SELECT DISTINCT [" + field + "] FROM [" + table + "]"));

                foreach (var it in sqlCommands)
                {
                    if (it.field.num == 1)
                    {
                        if (filtersChecked == null)
                            filtersChecked = new List<FilterChecked>(10);
                        filtersChecked.Insert(0, new FilterChecked(it.field.name));
                        flowLayoutPanel1.Controls.Add(filtersChecked[0].groupBox);
                    }
                    else 
                    if (it.field.num == 2)
                    {
                        if (filtersFromTo == null)
                            filtersFromTo = new List<FilterFromTo>(5);
                        filtersFromTo.Insert(0, new FilterFromTo(it.field.name));
                        flowLayoutPanel1.Controls.Add(filtersFromTo[0].groupBox);
                    }
                }

                Common.SqlConnection = new SqlConnection(Common.StrSQLConnection);
                Common.SqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

        }

        private void Filters_FormClosing(object sender, FormClosingEventArgs e)
        {
            // сброс установленных фильтров 
            // (установить те, которые были установлены при этом отображении формы)
            //
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // сброс установленных фильтров 
            // (установить те, которые были установлены при этом отображении формы)
            //
            Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // применяем фильтры
            // возможно, обновляем список товаров здесь
            //
            Close();

        }

        
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}