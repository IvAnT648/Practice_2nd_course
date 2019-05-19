using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class ChangeProduct : Form
    {
        DataGridViewRow Row { set; get; } = null;
        // стобцы в dataGridView
        List<_strToGridView> fields = null;
        // флаги изменения
        // словарь "поле таблицы"-"значение"
        Dictionary<string, object> FieldValue = null;
        // "старое" изображение
        // изображение
        byte[] dataImage = null;
        // команда добавления в бд
        string commandText = @"UPDATE Products SET ";

        public ChangeProduct()
        {
            InitializeComponent();
        }

        public ChangeProduct(DataGridViewRow row)
        {
            InitializeComponent();
            Row = row;
        }

        private void ChangeProduct_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            FieldValue = new Dictionary<string, object>(15);
            fields = new List<_strToGridView>(15);

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

            foreach (var f in fields)
            {
                f.value = Row.Cells[f.name].Value.ToString();
                FieldValue.Add(f.name, f.value);
            }

            // вывод в DataGridView
            foreach (var f in fields)
                dgv.Rows.Add(f.name, f.obligation, f.type, f.value);

            // загрузка изображения из БД
            dataImage = FileTools.GetFileFromDB(Common.StrSQLConnection, @"Products", @"Изображение", (int)Row.Cells[0].Value);
            if (dataImage != null) pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            if (pictureBox.Image == null) buttonDelImage.Visible = false;
            else buttonDelImage.Visible = true;
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            openImage.Filter = "Изображения (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openImage.ShowDialog() == DialogResult.Cancel) return;
            // получаем файл в виде байтов
            dataImage = FileTools.FileInBytes(openImage.FileName);
            // выводим его в pictureBox
            pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            if (pictureBox.Image == null) buttonDelImage.Visible = false;
            else buttonDelImage.Visible = true;
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                if (MessageBox.Show("Вы уверены, что хотите изменить товар?", "Изменение товара",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    commandText = $@"UPDATE Products SET [Тип ПК]=@type, [Производитель]=@brand, [Модель]=@model, [CPU]=@cpu, [Кол-во ядер]=@cores, [GPU]=@gpu, [Объем RAM]=@ram, [Тип RAM]=@typeram, [HDD]=@hdd, [SSD]=@ssd, [Операционная система]=@os, [Блок питания]=@psu, [Склад]=@stock, [Цена]=@cost, [Описание]=@descripton, [Изображение]=@image WHERE Id={(int)Row.Cells[0].Value}";

                    // выполнение команды
                    SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
                    SqlCommand query = new SqlCommand(commandText, connection);
                    query.Parameters.AddWithValue("@type", dgv.Rows[0].Cells[3].Value);
                    query.Parameters.AddWithValue("@brand", dgv.Rows[1].Cells[3].Value);
                    query.Parameters.AddWithValue("@model", dgv.Rows[2].Cells[3].Value);
                    query.Parameters.AddWithValue("@cpu", dgv.Rows[3].Cells[3].Value);
                    query.Parameters.AddWithValue("@cores", dgv.Rows[4].Cells[3].Value);
                    query.Parameters.AddWithValue("@gpu", dgv.Rows[5].Cells[3].Value);
                    query.Parameters.AddWithValue("@ram", dgv.Rows[6].Cells[3].Value);
                    query.Parameters.AddWithValue("@typeram", dgv.Rows[7].Cells[3].Value);
                    if (dgv.Rows[8].Cells[3].Value != null)
                        query.Parameters.AddWithValue("@hdd", dgv.Rows[8].Cells[3].Value);
                    else query.Parameters.AddWithValue("@hdd", DBNull.Value);
                    if (dgv.Rows[9].Cells[3].Value != null)
                        query.Parameters.AddWithValue("@ssd", dgv.Rows[9].Cells[3].Value);
                    else query.Parameters.AddWithValue("@ssd", DBNull.Value);
                    if (dgv.Rows[10].Cells[3].Value != null)
                        query.Parameters.AddWithValue("@os", dgv.Rows[10].Cells[3].Value);
                    else query.Parameters.AddWithValue("@os", DBNull.Value);
                    if (dgv.Rows[11].Cells[3].Value != null)
                        query.Parameters.AddWithValue("@psu", dgv.Rows[11].Cells[3].Value);
                    else query.Parameters.AddWithValue("@psu", DBNull.Value);
                    query.Parameters.AddWithValue("@stock", dgv.Rows[12].Cells[3].Value);
                    query.Parameters.AddWithValue("@cost", dgv.Rows[13].Cells[3].Value);
                    if (dgv.Rows[14].Cells[3].Value != null)
                        query.Parameters.AddWithValue("@descripton", dgv.Rows[14].Cells[3].Value);
                    else query.Parameters.AddWithValue("@descripton", DBNull.Value);
                    if (dataImage != null) query.Parameters.AddWithValue("@image", dataImage);
                    else query.Parameters.AddWithValue("@image", DBNull.Value);
                    try
                    {
                        connection.Open();
                        if (await query.ExecuteNonQueryAsync() == 1)
                        {
                            MessageBox.Show("Информация успешно обновлена", "Сообщение",
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
                string sChar = row.Cells[0]?.Value?.ToString();
                string sObligation = row.Cells[1]?.Value?.ToString();
                string sType = row.Cells[2]?.Value?.ToString();
                string sValue = null;
                if (row.Cells[3].Value == null)
                    sValue = "";
                else sValue = row.Cells[3]?.Value?.ToString();

                if (string.IsNullOrWhiteSpace(sValue))
                { // если строка пустая или с одними пробелами - пропускаем
                    if (sObligation == "Да")
                    { // если обязательное поле
                        MessageBox.Show($"Поле {sChar} должно быть заполнено", "Некорректный ввод!", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    continue;
                }
                
                switch (sType)
                {
                    case "Целое неотрицательное число":
                        Regex regex = new Regex(intPattern);
                        if (!regex.IsMatch(sValue))
                        {
                            MessageBox.Show($"Поле \"{sChar}\" заполнено не корректно", "Некорректный ввод!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        break;
                    case "Текст":
                        break;
                    default:
                        MessageBox.Show("Произошла ошибка при распозновании типа значения. См. код 'AddNewProduct.valid()'", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }
            return true;
        }

        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            dataImage = null;
            buttonDelImage.Visible = false;
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}