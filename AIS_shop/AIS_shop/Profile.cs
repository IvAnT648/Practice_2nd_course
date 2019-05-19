using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Profile : Form
    {
        byte[] dataImage { get; set; } = null;
        bool Unsaved = false;

        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadInfo();
            LoadOrders();
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Unsaved)
            {
                DialogResult result = 
                    MessageBox.Show("Сохранить изменение изображения?", "Подтверждение действия",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    if (dataImage != null)
                        if (FileTools.PutBytesToDB(dataImage, Common.StrSQLConnection, @"Users", @"Picture", User.GetUser().Id))
                            MessageBox.Show("Файл успешно загружен в базу данных.", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else MessageBox.Show("Файл не был загружен в базу данных.", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else DeleteImage();

                if (result == DialogResult.Cancel) e.Cancel = true;
            }
        }

        private void LoadInfo()
        {
            User user = User.GetUser();
            if (user.Status == UserStatus.Guest)
            {
                MessageBox.Show("Вы не аторизованы", "Вход не выполнен",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            textBoxSurname.Text = user.Surname;
            textBoxName.Text = user.Name;
            if (!string.IsNullOrWhiteSpace(user.Patronymic))
                textBoxPatronymic.Text = user.Patronymic;
            else textBoxPatronymic.Text = "(не указано)";
            textBoxEmail.Text = user.Email;
            textBoxNick.Text = user.Nick;
            pictureBox.Image = ImageTools.GetImageFromDB(Common.StrSQLConnection, "Users", "Picture", user.Id);
            MemoryStream ms = new MemoryStream();
            if (pictureBox.Image == null)
            {
                pictureBox.Image = Properties.Resources.nofoto;
                buttonDelImage.Visible = false;
            }
            else pictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private async void LoadOrders()
        {
            string text = $@"
                SELECT CONCAT(pr.Производитель, ' ', pr.Модель), ord.Date, ord.Amount, ord.Status 
                FROM Products AS pr, Orders AS ord 
                WHERE (pr.Id=ord.Product_id) AND (ord.Customer_id={User.GetUser().Id})";
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(text, connection);
            DataSet ds = new DataSet();
            try
            {
                await connection.OpenAsync();
                adapter.Fill(ds);
                foreach (DataRow i in ds.Tables[0].Rows)
                    dgv.Rows.Add(i.ItemArray);
                    
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

        private void linkNewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openNewImage.Filter = "Изображения (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openNewImage.ShowDialog() == DialogResult.Cancel) return;
            // получаем файл в виде байтов
            dataImage = FileTools.FileInBytes(openNewImage.FileName);
            // выводим его в pictureBox
            pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            if (dataImage != null)
            {
                Unsaved = true;
                buttonDelImage.Visible = true;
            }
        }

        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            dataImage = null;
            pictureBox.Image = Properties.Resources.nofoto;
            Unsaved = true;
            buttonDelImage.Visible = false;
        }

        private void DeleteImage()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlCommand sqlCommand = new SqlCommand($@"UPDATE Users SET Picture=NULL WHERE Id={User.GetUser().Id}", connection);
            try
            {
                connection.Open();
                if (sqlCommand.ExecuteNonQuery() == 1)
                    MessageBox.Show("Изображение удалено", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}