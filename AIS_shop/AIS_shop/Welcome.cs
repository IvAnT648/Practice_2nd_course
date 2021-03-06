﻿using System;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        // Кнопка "Перейти в каталог"
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Кнопка "Войти"
        private void button2_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            auth.ShowDialog();
            Close();
        }
        // Кнопка "Регистрация"
        private void button3_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
            Close();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }
    }
}