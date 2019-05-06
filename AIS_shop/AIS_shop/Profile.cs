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
            loadInfo();
            loadOrders();
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Unsaved)
            {
                DialogResult result = 
                    MessageBox.Show("Сохранить иземенения?", "Подтверждение сохранения",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    if (FileTools.PutBytesToDB(dataImage, Common.StrSQLConnection, @"Users", @"Picture", User.GetUser().Id))
                        MessageBox.Show("Файл загружен в базу данных.", "Сообщение");
                if (result == DialogResult.Cancel) e.Cancel = true;
            }
        }

        private void loadInfo()
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
            dataImage = FileTools.GetFileFromDB(Common.StrSQLConnection, "Users", "Picture", user.Id);
            if (dataImage != null)
                pictureBox.Image = Image.FromStream(new MemoryStream(dataImage));
            else pictureBox.Image = Properties.Resources.nofoto;
        }

        private async void loadOrders()
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
            if (pictureBox.Image != null) Unsaved = true;
        }
    }
}