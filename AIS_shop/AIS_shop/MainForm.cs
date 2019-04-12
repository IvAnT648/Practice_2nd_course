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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Show();
            forms.welcome = new Welcome();
            forms.welcome.ShowDialog();
        }

        private void личныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void перейтиВЛичныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            if (forms.profile == null)
                forms.profile = new Profile();
            forms.profile.ShowDialog();
            //Show();
        }

        private void корзинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (forms.cart == null)
                forms.cart = new Cart();
            forms.cart.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (forms.filters == null)
                forms.filters = new Filters();
            forms.filters.ShowDialog();
            //((MainForm)Owner) - родитель
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AboutProgram about = new AboutProgram();
            about.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // msgbox подтверждение
            Close();
        }
    }
}
