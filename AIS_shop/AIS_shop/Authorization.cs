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
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }
        // Кнопка "Войти"
        private void button1_Click(object sender, EventArgs e)
        {
            // if (найден пользователь)

            // отобразить изменения в MainForm
            // else вывести msgBox
            Close();
        }
        // кнопка перехода на форму регистрации
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
            Close();
        }
    }
}
