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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Кнопка "Перейти в каталог"
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm main = new MainForm();
            main.ShowDialog();
            Close();
        }
        // Кнопка "Войти"
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Authorization auth = new Authorization();
            auth.ShowDialog();
            if (auth.IsDisposed)
                Close();
            else Show();
        }
        // Кнопка "Регистрация"
        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
            if (reg.IsDisposed)
                Close();
            else Show();
        }
    }
}
