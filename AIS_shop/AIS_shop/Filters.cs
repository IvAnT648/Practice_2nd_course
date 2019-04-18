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
        private List<sub1_ForFiltersForm> sqlCommands;

        private List<CheckedListBox> checkedListBoxes;
        private List<FilterFromTo> filtersFromTo;

        public Filters()
        {
            InitializeComponent();
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    FilterFromTo filterFromTo = new FilterFromTo("Проверка "+(i+1));
                    flowLayoutPanel1.Controls.Add(filterFromTo.groupBox);
                }
                    

                /*formSqlCommands();
                Common.SqlConnection = new SqlConnection(Common.StrSQLConnection);
                Common.SqlConnection.Open();

                foreach (var it in sqlCommands)
                {
                    if (it.field.num == 1)
                    {
                        
                        //flowLayoutPanel1.Controls.Add(new CheckedListBox()
                    }
                        
                }*/
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

        // сформировать sql команды для получения критериев фильтрации
        void formSqlCommands()
        {
            foreach (var table in Common.fieldsForFilters)
                foreach (var field in table.fields)
                    sqlCommands.Add(new sub1_ForFiltersForm(
                            table.name, field, "SELECT DISTINCT [" + field + "] FROM [" + table + "]"));
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
