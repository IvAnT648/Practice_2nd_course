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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            // ввод информации из бд
            //
            //
        }
        
        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            // чтобы крестик не закрывал окно корзины, а скрывал
            e.Cancel = true;
            Hide();
            forms.main.Show();
        }
    }
}
