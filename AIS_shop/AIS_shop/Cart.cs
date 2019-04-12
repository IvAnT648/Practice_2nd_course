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
    public partial class Cart : Form
    {
        public Cart()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            
        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            // чтобы крестик не закрывал окно корзины, а скрывал
            e.Cancel = true;
            Hide();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // записываем заказ в бд
            if (forms.order == null)
                forms.order = new Order();
            forms.order.ShowDialog();
        }
    }
}
