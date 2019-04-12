using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_shop
{


    public static class forms
    {
        //public static Authorization auth = null;
        public static Cart cart = null;
        public static Filters filters = null;
        public static MainForm main = null;
        public static Order order = null;
        public static Product product = null;
        public static Profile profile = null;
        public static Registration reg = null;
        public static Welcome welcome = null;
    }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

       
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            forms.main = new MainForm();
            
            Application.Run(forms.main);
        }
    }
}

/*
Form qwe=Application.OpenForms[0];
qwe.Show();

Main_form.Show();
*/
