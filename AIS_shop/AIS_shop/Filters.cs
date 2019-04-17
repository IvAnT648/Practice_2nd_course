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
        // контейнер, где ключ - поле таблицы, 
        // а значение - команда для получения уникальных записей (для формирования фильров)
        private List<KeyValuePair<TableFields, string>> sqlCommands;

        public Filters()
        {
            InitializeComponent();
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            //connection = new SqlConnection(Common.StrSQLConnection);
            //connection.Open();
            //sqlCommands = new List<string[]>(10);
            //sqlCommands.Add();
            sqlCommands.Find()
        }

        void formSqlCommands()
        {
            foreach (var table in Common.fieldsForFilters)
                foreach (var field in table.field)
                    sqlCommands.Add(new KeyValuePair<TableFields, string>
                        (table, "SELECT DISTINCT [" + field + "] FROM [" + table + "]"));
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
