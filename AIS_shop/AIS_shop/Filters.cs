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
        private List <FilterChecked> filtersChecked = null;
        private List<FilterFromTo> filtersFromTo = null;
        private string tableName;
        private static string SQLCommandToUpdate;

        public Filters()
        {
            InitializeComponent();
        }

        public Filters(string name_of_table)
        {
            InitializeComponent();
            this.tableName = name_of_table;
        }

        private int _AddQueryFilterChecked()
        {
            if (filtersChecked != null)
            {
                // цикл по фильтрам
                foreach (var filter in filtersChecked)
                {
                    // если в каком-либо группбоксе не выбран ни один вариант
                    if (filter.checkedList.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("Необходимо выбрать как минимум один вариант из списка",
                            filter.groupBox.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                    // пропускаем фильтр, если отмечены все чекбоксы
                    if (filter.checkedList.CheckedItems.Count == filter.checkedList.Items.Count)
                        continue;
                    // если команда не была модифицирована на более ранних итерациях
                    if (SQLCommandToUpdate.Contains("WHERE"))
                        SQLCommandToUpdate += " AND";
                    else SQLCommandToUpdate += " WHERE";

                    // если отмечено более 1 чекбокса - используем команду IN
                    if (filter.checkedList.CheckedItems.Count > 1)
                    {
                        int i = 0;
                        // добавляем название столбца
                        SQLCommandToUpdate += " [" + filter.groupBox.Text + "] IN (";

                        // добавляем все возможные значения этого столбца
                        foreach (var ch in filter.checkedList.CheckedItems)
                        {
                            SQLCommandToUpdate += "\'" + filter.checkedList.GetItemText(ch) + "\'";

                            if (i++ != filter.checkedList.CheckedItems.Count - 1)
                                SQLCommandToUpdate += ", ";
                        }
                        SQLCommandToUpdate += ")";
                    }
                    else // иначе - если отмечен только один вариант - используем команду AND
                    {
                        // добавляем название столбца
                        SQLCommandToUpdate += " [" + filter.groupBox.Text + "]";
                        // добавляем значение этого столбца
                        SQLCommandToUpdate += "=\'" +
                            filter.checkedList.GetItemText(filter.checkedList.CheckedItems[0]) + "\'";
                    }
                }
                return 1;
            }
            else return 0;
        }
        private int _AddQueryFilterFromTo()
        {
            if (filtersFromTo != null)
            {
                foreach (var filter in filtersFromTo)
                {
                    int f = 0, // левая граница диапазона
                        t = 0; // правая граница диапазона

                    // пропуск фильтров с пустыми textBox
                    if (filter.from.Text == "" && filter.to.Text == "")
                        continue;
                    // если команда не была модифицирована на более ранних стадиях
                    if (SQLCommandToUpdate.Contains("WHERE"))
                        SQLCommandToUpdate += " AND";
                    else SQLCommandToUpdate += " WHERE";

                    if (filter.from.Text != "")
                    {
                        if (!Int32.TryParse(filter.from.Text, out f))
                        {
                            MessageBox.Show("Некорректный ввод в поле \'" + filter.groupBox.Text + "\'", "Ошибка ввода!",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                    }

                    if (filter.to.Text != "")
                    {
                        if (!Int32.TryParse(filter.to.Text, out t))
                        {
                            MessageBox.Show("Некорректный ввод в поле \'" + filter.groupBox.Text + "\'", "Ошибка ввода!",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                    }

                    SQLCommandToUpdate += " [" + filter.groupBox.Text + "]";
                    if (f > t)
                    {
                        if (t != 0)
                        {
                            MessageBox.Show("Некорректный ввод", filter.groupBox.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
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
                return 1;
            }
            return 0;
        }

        // применение изменений - создание sql-запроса 
        private int ApplyChanges()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                bool ok = false;
                // формирование SQL-запроса для вывода представления в главную форму
                SQLCommandToUpdate = "SELECT * FROM [v" + tableName + "]";

                // добавление в команду для обновления запрос по фильтрам 
                if (_AddQueryFilterChecked() != -1)
                    if (_AddQueryFilterFromTo() != -1)
                        ok = true;

                if (ok)
                { // если все ок - записываем команду в главной форме
                    MainForm.QueryToUpdate = SQLCommandToUpdate;
                    return 1;
                }
                else return 0;
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
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                connection.Open();
                List<string> variants = new List<string>();
                Table table = Common.fieldsForFilters.Find(item => item.name == this.tableName);
                // для каждого поля, для которого нужно сделать фильтр,
                // делаем, в зависимости от от параметра "field.num"
                foreach (Field field in table.fields)
                {
                    SqlCommand command = new SqlCommand(field.sqlCommand, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    
                    if (field.num == 1)
                    { // если для поля требуется чекбоксы
                        // загружаем
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (row.ItemArray[0].ToString() == "") continue;
                            else variants.Add(row.ItemArray[0].ToString());
                        }
                           
                        ds.Clear();
                        // создаем фильтры-чекбоксы
                        if (filtersChecked == null)
                            filtersChecked = new List<FilterChecked>();
                        filtersChecked.Add(new FilterChecked(field.name));

                        foreach (string variant in variants)
                            filtersChecked[filtersChecked.Count - 1].checkedList.Items.Add(variant, true);
                            
                        variants.Clear();
                    } // иначе - если требуется - фильтр "от и до"
                    else if (field.num == 2)
                    {
                        if (filtersFromTo == null)
                            filtersFromTo = new List<FilterFromTo>();
                        filtersFromTo.Add(new FilterFromTo(field.name));
                    }
                }
                // добавление эл-тов на форму
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
            if (ApplyChanges() != 0)
                Close();
        }
    }
}