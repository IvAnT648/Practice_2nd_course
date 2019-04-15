using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Filters : Form
    {
        public Filters()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
    }
}
