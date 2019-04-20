using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Filters : Form
    {
        private List<FilterChecked> filtersChecked = null;
        private List<FilterFromTo> filtersFromTo = null;
        private string tableName;
        private static string SQLCommandToUpdate;
        MainForm main = null;

        public Filters()
        {
            InitializeComponent();
        }

        public Filters(string name_of_table)
        {
            InitializeComponent();
            this.tableName = name_of_table;
        }

        // применение изменений - создание sql-запроса 
        //   и направление в функцию loadDataToGridView
        private int ApplyChanges()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                bool ok = false;
                bool skip = false;
                // формирование SQL-запроса для вывода представления в главную форму
                // SELECT * FROM [v<table_name>] WHERE <value1> = <selected_value1> AND ... [<valueN>]=[<selected_valueN>]
                SQLCommandToUpdate = "SELECT * FROM [v" + tableName + "]";
                
                if (filtersChecked != null)
                {
                    
                    for (int i = 0; i < filtersChecked.Count; i++)
                    {
                        var filt = filtersChecked[i];
                        // если в каком-либо чекбоксе не выбран ни один вариант
                        if (filt.checkedList.CheckedItems.Count == 0)
                        {
                            MessageBox.Show("Необходимо выбрать как минимум один вариант из списка", 
                                filt.groupBox.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return 0;
                        }
                        // пропускаем неизмененные чекбоксы
                        if (filt.checkedList.CheckedItems.Count == filt.checkedList.Items.Count)
                        {
                            if (filtersFromTo == null && i == filtersChecked.Count - 1)
                                ok = true;
                            continue;
                        }
                        if (SQLCommandToUpdate == "SELECT * FROM [v" + tableName + "]")
                            SQLCommandToUpdate += " WHERE";
                        else if (i != 0)
                            SQLCommandToUpdate += " AND";
                        for (int j = 0; j < filt.checkedList.CheckedItems.Count; j++)
                        {
                            var ch = filt.checkedList.CheckedItems[j];
                            // добавляем название столбца
                            SQLCommandToUpdate += " [" + filt.groupBox.Text + "]";
                            // добавляем значение этого столбца
                            SQLCommandToUpdate += "=\'" + filt.checkedList.GetItemText(ch) + "\'";
                            if (j != filt.checkedList.CheckedItems.Count - 1)
                                SQLCommandToUpdate += " AND";
                        }
                    }
                }

                if (filtersFromTo != null && !skip)
                {
                    for (int i = 0; i < filtersFromTo.Count; i++)
                    {
                        int f = 0, // левая граница диапазона
                            t = 0; // правая граница диапазона
                        var filt = filtersFromTo[i];
                        // пропуск фильтров с пустыми textBox
                        if (filt.from.Text == "" && filt.to.Text == "")
                        {
                            if (i == filtersFromTo.Count - 1)
                                ok = true; // если обошли все фильтры
                            continue;
                        }
                        if (SQLCommandToUpdate == "SELECT * FROM [v" + tableName + "]")
                            SQLCommandToUpdate += " WHERE";
                        else if (i != 0)
                            SQLCommandToUpdate += " AND";

                        if (filt.from.Text != "")
                            f = Int32.Parse(filt.from.Text);

                        if (filt.to.Text != "")
                            t = Int32.Parse(filt.to.Text);
                        SQLCommandToUpdate += " [" + filt.groupBox.Text + "]";
                        if (f > t)
                        {
                            if (t != 0)
                            {
                                MessageBox.Show("Некорректный ввод", filt.groupBox.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            SQLCommandToUpdate += ">=" + f.ToString();
                        }
                        else if (f == t)
                            SQLCommandToUpdate += "=" + f.ToString();
                        else if (f < t)
                            if (f == 0)
                                SQLCommandToUpdate += "<=" + t.ToString();
                            else SQLCommandToUpdate += " BETWEEN " + f.ToString() + " AND " + t.ToString();
                    }
                }
                if (ok)
                {
                    if (main != null)
                    {
                        main.loadDataToGridView(SQLCommandToUpdate);
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось применить фильтры", "Ошибка применения фильтров", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                return 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
                return -1;
            }
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            main = Owner as MainForm;
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            List<string> variants = new List<string>();
            try
            {
                connection.Open();

                Table table = Common.fieldsForFilters.Find(item => item.name == this.tableName);

                foreach (Field field in table.fields)
                {
                    SqlCommand command = new SqlCommand(field.sqlCommand, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    if (field.num == 1)
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                           variants.Add(Convert.ToString(row.ItemArray[0]));
                        ds.Clear();
                        
                        if (filtersChecked == null)
                            filtersChecked = new List<FilterChecked>();
                        filtersChecked.Add(new FilterChecked(field.name));

                        foreach (string variant in variants)
                            filtersChecked[filtersChecked.Count - 1].checkedList.Items.Add(variant, true);
                            
                        
                        variants.Clear();
                    }
                    else if (field.num == 2)
                    {
                        if (filtersFromTo == null)
                            filtersFromTo = new List<FilterFromTo>();
                        filtersFromTo.Add(new FilterFromTo(field.name));
                    }
                }
                foreach (var filter in filtersChecked)
                    flowLayoutPanel1.Controls.Add(filter.groupBox);
                foreach (var filter in filtersFromTo)
                    flowLayoutPanel1.Controls.Add(filter.groupBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        private void Filters_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // применяем фильтры
            int f = ApplyChanges();
            // сохранить состояние фильтров
            if (f != 0)
                Close();
        }
    }
}