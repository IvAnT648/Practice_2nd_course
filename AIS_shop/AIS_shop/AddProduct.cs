using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AIS_shop
{
    public partial class AddProduct : Form
    {
        // структура для вывода в dataGridView
        List<_strToGridView> fields = new List<_strToGridView>(15);
        // словарь "поле таблицы"-"значение"
        Dictionary <string, object> FieldValue = new Dictionary<string, object>(15);
        // изображение
        byte[] dataImage = null;
        // команда добавления в бд
        string sqlCommand = @"INSERT INTO Products (";

        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddNewProduct_Load(object sender, EventArgs e)
        {
            fields.Add(new _strToGridView("Тип ПК", "Да", "Текст"));
            fields.Add(new _strToGridView("Производитель", "Да", "Текст"));
            fields.Add(new _strToGridView("Модель", "Да", "Текст"));
            fields.Add(new _strToGridView("CPU", "Да", "Текст"));
            fields.Add(new _strToGridView("Кол-во ядер", "Да", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("GPU", "Да", "Текст"));
            fields.Add(new _strToGridView("Объем RAM", "Да", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("Тип RAM", "Да", "Текст"));
            fields.Add(new _strToGridView("HDD", "Нет", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("SSD", "Нет", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("Операционная система", "Нет", "Текст"));
            fields.Add(new _strToGridView("Блок питания", "Нет", "Текст"));
            fields.Add(new _strToGridView("Склад", "Да", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("Цена", "Да", "Дробное неотрицательное число"));
            fields.Add(new _strToGridView("Описание", "Нет", "Текст"));
            
            labelFileName.Visible = false;
            pictureBox.Visible = false;
            buttonDelImage.Visible = false;
            dgv.RowHeadersVisible = false;

            // вывод в DataGridView
            for (int i = 0; i < fields.Count; i++)
                dgv.Rows.Add(fields[i].name, fields[i].obligation, fields[i].type);
            // добавление к запросу полей, в которые будет осуществляться вставка
            foreach (var f in fields)
            {
                
                FieldValue.Add(f.name, null);
                // 
                if (f != fields[0]) sqlCommand += @", ";
                sqlCommand += $@"[{f.name}]";
            }
            sqlCommand += @", [Изображение])";
        }

        private void AddNewProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (valid())
            { 
                if (MessageBox.Show("Вы уверены, что хотите добавить этот товар?", "Добавить товар",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    addProductToDB();
                }
            }
        }

        private async void addProductToDB()
        {
            // добавление введенных значений столбцов в команду
            sqlCommand += @" VALUES (";
            foreach (var field in FieldValue)
            {
                if (field.Key != FieldValue.First().Key)
                    sqlCommand += @", ";
                if (field.Value == null)
                {
                    sqlCommand += $@"NULL";
                    continue;
                }
                if (field.Value is string)
                    sqlCommand += $@"'{field.Value.ToString()}'";
                else sqlCommand += $@"{field.Value.ToString()}";
            }
            sqlCommand += @", @image)";
            // выполнение команды
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            try
            {
                connection.Open();
                SqlCommand query = new SqlCommand(this.sqlCommand, connection);
                query.Parameters.AddWithValue("@image", (object)dataImage);
                if (await query.ExecuteNonQueryAsync() == 1)
                {
                    MessageBox.Show("Запись была успешно добавлена в таблицу", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProductManagement.updateFlag = true;
                    Close();
                }
                    
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

        private bool valid()
        {
            string intPattern = @"^\d+$";
            string floatPattern = @"^\d+(\.|,)?\d+$";

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string sValue = null;
                string sChar = row.Cells[0]?.Value?.ToString();
                string sObligation = row.Cells[1]?.Value?.ToString();
                string sType = row.Cells[2]?.Value?.ToString();
                if (row.Cells[3].Value == null)
                    sValue = "";
                else sValue = row.Cells[3]?.Value?.ToString();

                if (string.IsNullOrWhiteSpace(sValue))
                { // если строка пустая или с одними пробелами
                    if (sObligation == "Да")
                    { // если обязательное поле
                        MessageBox.Show($"Поле {sChar} должно быть заполнено", "Некорректный ввод!", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    continue;
                }
                Regex regex = null;
                switch (sType)
                {
                    case "Целое неотрицательное число":
                        regex = new Regex(intPattern);
                        if (regex.IsMatch(sValue))
                        {
                            uint val = uint.Parse(sValue);
                            FieldValue[sChar] = val;
                        }
                        else
                        {
                            MessageBox.Show($"Поле \"{sChar}\" заполнено не корректно", "Некорректный ввод!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        break;
                    case "Дробное неотрицательное число":
                        regex = new Regex(floatPattern);
                        if (regex.IsMatch(sValue))
                        {
                            if (sValue.Contains("."))
                                sValue = sValue.Replace(".", ",");
                            float val = float.Parse(sValue);
                            FieldValue[sChar] = val;
                        }
                        else
                        {
                            MessageBox.Show($"Поле \"{sChar}\" заполнено не корректно", "Некорректный ввод!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        break;
                    case "Текст":
                        FieldValue[sChar] = sValue;
                        break;
                    default:
                        MessageBox.Show("Произошла ошибка при распозновании типа значения. См. код \'AddNewProduct.valid()\'", "Ошибка!", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            openImage.Filter = "Изображения (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openImage.ShowDialog() == DialogResult.Cancel) return;
            
            // получаем файл в виде байтов
            dataImage = FileTools.FileInBytes(openImage.FileName);
            // выводим его в pictureBox
            pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            pictureBox.Visible = true;
            labelFileName.Text = Path.GetFileName(openImage.FileName);
            labelFileName.Visible = true;
            buttonDelImage.Visible = true;
        }

        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            dataImage = null;
            pictureBox.Image = null;
            pictureBox.Visible = false;
            labelFileName.Text = "";
            labelFileName.Visible = false;
            buttonDelImage.Visible = false;
        }
    }
}
