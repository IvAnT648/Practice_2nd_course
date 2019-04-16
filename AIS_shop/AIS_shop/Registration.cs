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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // if (все поля корректно заполнены)
            // добавляем нового пользователь в бд
            // и выполняем вход в новый акк
            Close();
        }
        // кнопка перехода на авторизацию
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Authorization auth = new Authorization();
            auth.ShowDialog();
            Close();
        }
    }
}
