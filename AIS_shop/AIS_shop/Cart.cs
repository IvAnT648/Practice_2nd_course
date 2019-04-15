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
            Close();
            
        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // записываем заказ в бд
            //
            Order order = new Order();
            order.ShowDialog();
        }
    }
}
