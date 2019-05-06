using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class Product : Form
    {
        DataGridViewRow Row { set; get; } = null;
        User user = User.GetUser();
        int product_id { set; get; }

        public Product(DataGridViewRow row)
        {
            InitializeComponent();
            Row = row ?? throw new ArgumentNullException(nameof(row));
            product_id = (int)Row.Cells[0].Value;
        }

        private void Product_Load(object sender, EventArgs e)
        {
            lableProductName.Text = Row.Cells["Производитель"].Value.ToString() + " " + Row.Cells["Модель"].Value.ToString();
            labelCost.Text = "Цена: " + Row.Cells["Цена"].Value.ToString();
            labelInStock.Text = "На складе: " + Row.Cells["Склад"].Value.ToString();
            //
            richTextBoxDescription.Text = "*** Описание не было загружено ***";
            loadDescription();
            //
            listBoxChars.Items.Add("*** Характеристики товара не загружены ***");
            loadCharacteristics();
            //
            richTextBoxReviews.Text = "*** Отзывы не были загружены ***";
            loadReviews();                
            //
            loadPicture("Products", product_id);
            //
            if ((int)Row.Cells["Склад"].Value == 0)
                bAddToCart.Enabled = false;
        }

        private void _printUserNickToReview(int id)
        {
            if (id <= 0) return;
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            try
            {
                string commandText = @"SELECT [Nick] FROM [Users] WHERE [Id]=" + id;
                connection.Open();
                SqlCommand query = new SqlCommand(commandText, connection);
                object result = query.ExecuteScalar();
                if (result != null)
                    richTextBoxReviews.Text += "=== Отзыв от пользователя " + result.ToString();
                else
                    richTextBoxReviews.Text += "=== Отзыв от неизвестного пользователя";
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

        private async void loadReviews()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);;
            SqlDataReader reader = null;
            try
            {
                string commandText = @"SELECT User_id, Mark, Advantages, Disadvantages, Comment FROM Reviews WHERE Product_id=" + product_id;
                connection.Open();
                SqlCommand query = new SqlCommand(commandText, connection);
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    richTextBoxReviews.Clear();
                    while (await reader.ReadAsync())
                    {
                        _printUserNickToReview((int)reader.GetValue(0));
                        richTextBoxReviews.Text += "\nОценка по 5-бальной шкале: " + reader.GetValue(1).ToString();
                        //
                        richTextBoxReviews.Text += "\n---Достоинства:\n";
                        if (!string.IsNullOrWhiteSpace(reader.GetValue(2).ToString()))
                            richTextBoxReviews.Text += reader.GetValue(2).ToString();
                        else richTextBoxReviews.Text += " *Не указано*";
                        //
                        richTextBoxReviews.Text += "\n---Недостатки:\n";
                        if (!string.IsNullOrWhiteSpace(reader.GetValue(3).ToString()))
                            richTextBoxReviews.Text += reader.GetValue(3).ToString();
                        else richTextBoxReviews.Text += " *Не указано*";
                        //
                        richTextBoxReviews.Text += "\n---Комментарий:\n";
                        if (!string.IsNullOrWhiteSpace(reader.GetValue(4).ToString()))
                            richTextBoxReviews.Text += reader.GetValue(4).ToString();
                        else richTextBoxReviews.Text += " *Не указано*";
                        richTextBoxReviews.Text += "\n--------------------------------------------------------------------------------------------------------------------------\n";

                    }
                }
                else
                {
                    richTextBoxReviews.Text = "*** Отзывов нет ***";
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
                if (reader != null && !reader.IsClosed) reader.Close();
            }
        }

        private async void loadCharacteristics()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);;
            SqlDataReader reader = null;
            try
            {
                string commandText = string.Format($@"SELECT * FROM Products WHERE Id={product_id}");
                await connection.OpenAsync();
                SqlCommand query = new SqlCommand(commandText, connection);
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        listBoxChars.Items.Clear();
                        for (int i = 1; i < reader.FieldCount-4; i++)
                        {
                            if (string.IsNullOrWhiteSpace(reader.GetValue(i).ToString())) continue;
                            string text = reader.GetName(i).ToString() + ": " + reader.GetValue(i).ToString();
                            listBoxChars.Items.Add(text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить характеристики товара.\n Не удалось прочитать данные из \"SqlDataReader\" ", "Ошибка загрузки",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listBoxChars.Items.Clear();
                        listBoxChars.Items.Add("*** Характеристики товара не загружены ***");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить характеристики товара.", "Ошибка загрузки",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxChars.Items.Clear();
                    listBoxChars.Items.Add("*** Характеристики товара не загружены ***");
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
                if (reader != null && !reader.IsClosed) reader.Close();
            }
        }

        private async void loadDescription()
        {
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);;
            try
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = string.Format($@"SELECT Описание FROM Products WHERE [Id]={(int)Row.Cells[0].Value}");
                command.Connection = connection;
                object result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    richTextBoxDescription.Text = result.ToString();
                }
                else
                {
                    richTextBoxDescription.Text = "*** Описание отсутствует ***";
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

        private void loadPicture(string tableName, int id)
        {
            // загрузка из БД
            Image image = ImageTools.GetImageFromDB(Common.StrSQLConnection, tableName, "Изображение", id);
            // загрузка изображения в pictureBox
            if (image != null)
                pictureBox.Image = image;
            else pictureBox.Image = Properties.Resources.nofoto;
        }

        private void buttonAddReview_Click(object sender, EventArgs e)
        {
            if (user.Status == UserStatus.Guest)
            {
                MessageBox.Show("Оставлять отзывы могут только авторизованные пользователи. Войдите или зарегистрируйтесь.", "Некорректное действие", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var connection = new SqlConnection(Common.StrSQLConnection);;
            var query = new SqlCommand($@"SELECT COUNT(Id) FROM Reviews WHERE Product_id={product_id} AND User_id={user.Id}", connection);
            try
            {
                connection.Open();
                int result = (int)query.ExecuteScalar();
                if (result > 0)
                {
                    if (MessageBox.Show("Вы уже добавляли отзыв об этом товаре. Хотите отредактировать его?", "Сообщение", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ReviewEditor editReview = new ReviewEditor(product_id);
                        editReview.ShowDialog();
                    }
                }
                else
                {
                    ReviewEditor createReview = new ReviewEditor(product_id);
                    createReview.ShowDialog();
                }
                loadReviews();
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

        private async void buttonAddToCart_Click(object sender, EventArgs e)
        {
            if (Common.ProductsInCart.Find(x => x == product_id) > 0)
            {
                MessageBox.Show("Товар уже в корзине", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);;
            SqlCommand query = new SqlCommand($@"SELECT * FROM Products WHERE Id={product_id}", connection);
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                    if (await reader.ReadAsync())
                    {
                        Common.ProductsInCart.Add(product_id);
                        MessageBox.Show("Товар успешно добавлен в корзину", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
