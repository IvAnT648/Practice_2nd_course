using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class ChangeProduct : Form
    {
        DataGridViewRow Row { set; get; } = null;
        // стобцы в dataGridView
        List<_strToGridView> fields = null;
        // флаги изменения
        bool[] Changeflags = null;
        // словарь "поле таблицы"-"значение"
        Dictionary<string, object> FieldValue = null;
        // "старое" изображение
        byte[] oldImage = null;
        // изображение
        byte[] dataImage = null;
        // команда добавления в бд
        string sqlCommand = @"UPDATE Products SET ";

        public ChangeProduct(DataGridViewRow row)
        {
            InitializeComponent();
            Row = row;
        }

        private void ChangeProduct_Load(object sender, EventArgs e)
        {
            FieldValue = new Dictionary<string, object>(15);
            fields = new List<_strToGridView>(15);
            Changeflags = new bool[16];

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

            foreach (var f in fields)
            {
                f.value = Row.Cells[f.name].Value.ToString();
                FieldValue.Add(f.name, f.value);
            }

            // вывод в DataGridView
            foreach (var f in fields)
                dgv.Rows.Add(f.name, f.obligation, f.type, f.value);

            // загрузка изображения из БД

            oldImage = FileTools.GetFileFromDB(MainForm.StrSQLConnection, @"Products", @"Изображение", (int)Row.Cells[0].Value);
            dataImage = oldImage;
            if (dataImage != null) pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));

            if (pictureBox.Image == null) buttonDelImage.Visible = false;
            else buttonDelImage.Visible = true;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

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
                    checkChange();
                    // добавление введенных значений столбцов в команду
                    int count = 0; // счетчик изменений
                    int i = 0;
                    foreach (var field in FieldValue)
                    {
                        if (Changeflags[i])
                        {
                            if (count != 0)
                                sqlCommand += @", ";
                            sqlCommand += $@"[{FieldValue.Keys.ElementAt(i)}]=";
                            if (field.Value == null)
                            {
                                sqlCommand += $@"NULL";
                                i++;
                                continue;
                            }
                            if (dgv.Rows[i].Cells[3].Value is string)
                                sqlCommand += $@"'{dgv.Rows[i].Cells[3].Value.ToString()}'";
                            else sqlCommand += $@"{dgv.Rows[i].Cells[3].Value.ToString()}";
                            count++;
                        }
                        i++;
                    }
                    if (Changeflags.Last())
                    {
                        if (count != 0) sqlCommand += ", ";
                        sqlCommand += @"[Изображение]=@image";
                        count++;
                    }
                    sqlCommand += $@" WHERE Id={Row.Cells[0].Value}";

                    if (count == 0) return;
                    // выполнение команды
                    SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
                    try
                    {
                        connection.Open();
                        SqlCommand query = new SqlCommand(this.sqlCommand, connection);
                        if (sqlCommand.Contains("@image")) query.Parameters.AddWithValue("@image", (object)dataImage);
                        if (await query.ExecuteNonQueryAsync() == 1)
                        {
                            MessageBox.Show("Запись была успешно обновлена", "Сообщение",
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
                Close();
            }
        }

        private bool valid()
        {
            string intPattern = @"^\d+$";
            string floatPattern = @"^\d+(\.|,)?\d+$";

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
                Regex regex = null;
                switch (sType)
                {
                    case "Целое неотрицательное число":
                        regex = new Regex(intPattern);
                        if (!regex.IsMatch(sValue))
                        {
                            MessageBox.Show($"Поле \"{sChar}\" заполнено не корректно", "Некорректный ввод!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                            
                        break;
                    case "Дробное неотрицательное число":
                        regex = new Regex(floatPattern);
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
                        MessageBox.Show("Произошла ошибка при распозновании типа значения. См. код \'AddNewProduct.valid()\'", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }
            return true;
        }

        private void checkChange()
        {
            int i = 0;
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                if (dgvRow.Cells["Value"].Value.ToString() == Row.Cells[i+1].Value.ToString())
                    Changeflags[i] = false;
                else Changeflags[i] = true;
                i++;
            }
            
            if (dataImage == oldImage)
                Changeflags[i] = false;
            else Changeflags[i] = true;
        }

        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            dataImage = null;
            buttonDelImage.Visible = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
