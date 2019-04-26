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

namespace AIS_shop
{
    public partial class AddNewProduct : Form
    {
        bool Saved = false;
        byte[] dataImage = null;
        public AddNewProduct()
        {
            InitializeComponent();
        }

        private void AddNewProduct_Load(object sender, EventArgs e)
        {
            labelFileName.Visible = false;
            pictureBox.Visible = false;
            buttonDelImage.Visible = false;
            // Type
            dgv.Rows.Add("Тип ПК", "Да", "Текст");
            // Brand
            dgv.Rows.Add("Производитель", "Да", "Текст");
            // Model
            dgv.Rows.Add("Модель", "Да", "Текст");
            // Brand CPU
            dgv.Rows.Add("Процессор", "Да", "Текст");
            // Count of cores
            dgv.Rows.Add("Кол-во ядер", "Да", "Целое число");
            // Brand GPU
            dgv.Rows.Add("Видеокарта", "Да", "Текст");
            // Type RAM
            dgv.Rows.Add("Тип оперативной памяти", "Да", "Текст");
            // Capacity RAM
            dgv.Rows.Add("Объем оперативной памяти (Гб)", "Да", "Целое число");
            // HDD                            
            dgv.Rows.Add("Объем жесткого диска (Гб)", "Нет", "Целое число");
            // SSD                            
            dgv.Rows.Add("Объем SSD (Гб)", "Нет", "Целое число");
            // Operating system                        
            dgv.Rows.Add("Операционная система", "Нет", "Текст");
            // PSU                               
            dgv.Rows.Add("Мощность блока питаня (Вт)", "Нет", "Целое число");
            // Stock
            dgv.Rows.Add("Кол-во на складе", "Да", "Целое число");
            // Cost
            dgv.Rows.Add("Цена", "Да", "Дробное число");
            // Description
            dgv.Rows.Add("Описание", "Нет", "Текст");
        }

        private void AddNewProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!valid())
            {
                
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите добавить этот товар?", "Добавить товар",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
        }

        private bool valid()
        {
            // row.Cells[].Value.ToString()
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                { // если строка пустая или с одними пробелами
                    if (row.Cells[1].Value.ToString() == "Да")
                    { // если обязательное поле
                        MessageBox.Show("Поле "+ row.Cells[3].Value.ToString()+" должно быть заполнено", "Некорректный ввод!", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    continue;
                }
                switch (row.Cells[2].Value.ToString())
                {
                    case "Текст":
                        if (!row.Cells[3].Value.ToString().All(_____________))
                        {
                            MessageBox.Show("Некорректный ввод в поле \"" + row.Cells[3].Value.ToString()+"\"", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        break;
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
            openImageFile.Filter = "Изображения (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openImageFile.ShowDialog() == DialogResult.Cancel) return;
            
            // получаем файл в виде байтов
            dataImage = ImageTools.GetFilesBytes(openImageFile.FileName);
            // выводим его в pictureBox
            pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            pictureBox.Visible = true;
            labelFileName.Text = Path.GetFileName(openImageFile.FileName);
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
