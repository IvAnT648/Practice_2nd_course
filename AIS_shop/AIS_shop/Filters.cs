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
        private SqlConnection connection;

        private List<string[]> sqlCommands;

        public Filters()
        {
            InitializeComponent();
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(Constants.connectionStringToDB);
            connection.Open();
            sqlCommands = new List<string[]>(10);
            sqlCommands.Add();
            "SELECT DISTINCT [Тип ПК] FROM [Computers]";
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
