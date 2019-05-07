using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AIS_shop
{
    public partial class AddProduct : Form
    {
        List<_strToGridView> fields = new List<_strToGridView>(15);
        // изображение
        byte[] dataImage = null;
        // команда добавления в бд
        string commandText = null;

        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddNewProduct_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

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
            fields.Add(new _strToGridView("Цена", "Да", "Целое неотрицательное число"));
            fields.Add(new _strToGridView("Описание", "Нет", "Текст"));
            
            labelFileName.Visible = false;
            pictureBox.Visible = false;
            buttonDelImage.Visible = false;
            dgv.RowHeadersVisible = false;

            // вывод в DataGridView
            for (int i = 0; i < fields.Count; i++)
                dgv.Rows.Add(fields[i].name, fields[i].obligation, fields[i].type);
            commandText = @"INSERT INTO Products (";
            // добавление к запросу полей, в которые будет осуществляться вставка
            foreach (var f in fields)
            {
                if (f != fields[0]) commandText += @", ";
                commandText += $@"[{f.name}]";
            }
            commandText += @", [Изображение])";
            
            commandText += 
@" VALUES (@type, @brand, @model, @cpu, @cores, @gpu, @ram, @typeram, @hdd, @ssd, @os, @psu, @stock, @cost, @descripton, @image)";
        }

        private void AddNewProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (valid())
            { 
                if (MessageBox.Show("Вы уверены, что хотите добавить этот товар?", "Добавить товар",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // добавление введенных значений столбцов в команду
                    SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
                    SqlCommand query = new SqlCommand(commandText, connection);

                    query.Parameters.AddWithValue("@type", fields[0].value);
                    query.Parameters.AddWithValue("@brand", fields[1].value);
                    query.Parameters.AddWithValue("@model", fields[2].value);
                    query.Parameters.AddWithValue("@cpu", fields[3].value);
                    query.Parameters.AddWithValue("@cores", fields[4].value);
                    query.Parameters.AddWithValue("@gpu", fields[5].value);
                    query.Parameters.AddWithValue("@ram", fields[6].value);
                    query.Parameters.AddWithValue("@typeram", fields[7].value);
                    query.Parameters.AddWithValue("@hdd", fields[8].value);
                    query.Parameters.AddWithValue("@ssd", fields[9].value);
                    query.Parameters.AddWithValue("@os", fields[10].value);
                    query.Parameters.AddWithValue("@psu", fields[11].value);
                    query.Parameters.AddWithValue("@stock", fields[12].value);
                    query.Parameters.AddWithValue("@cost", fields[13].value);
                    query.Parameters.AddWithValue("@descripton", fields[14].value);

                    if (dataImage != null) query.Parameters.AddWithValue("@image", dataImage);
                    else query.CommandText = query.CommandText.Replace("@image", "'NULL'");
                    // выполнение команды
                    try
                    {
                        connection.Open();
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
            }
        }

        private bool valid()
        {
            string intPattern = @"^\d+$";

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
                    fields.Find(f => f.name == sChar).value = DBNull.Value;
                }
                else
                {
                    switch (sType)
                    {
                        case "Целое неотрицательное число":
                            Regex regex = new Regex(intPattern);
                            if (regex.IsMatch(sValue))
                            {
                                int val = int.Parse(sValue);
                                fields.Find(f => f.name == sChar).value = val;
                            }
                            else
                            {
                                MessageBox.Show($"Поле \"{sChar}\" заполнено не корректно", "Некорректный ввод!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                        case "Текст":
                            fields.Find(f => f.name == sChar).value = sValue;
                            break;
                        default:
                            MessageBox.Show("Произошла ошибка при распозновании типа значения. См. код \'AddNewProduct.valid()\'", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                    }
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
