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
    public partial class Product : Form
    {
        DataGridViewRow Row { set; get; } = null;

        public Product(DataGridViewRow row)
        {
            InitializeComponent();
            Row = row ?? throw new ArgumentNullException(nameof(row));
        }

        private void Product_Load(object sender, EventArgs e)
        {
            lableProductName.Text = Row.Cells["Brand"].Value.ToString() + " " + Row.Cells["Model"].Value.ToString();
            labelCost.Text = "Цена: " + Row.Cells["Cost"].Value.ToString();
            labelInStock.Text = "На складе: " + Row.Cells["Stock"].Value;
            //
            richTextBoxDescription.Text = "Описание загружается...";
            loadDescription();
            if (richTextBoxDescription.Text == "Описание загружается...")
                richTextBoxDescription.Text = "*** Описание не было загружено ***";
            //
            listBoxChars.Items.Add("Характеристики товара загружаются...");
            loadCharacteristics();
            if (listBoxChars.Items[0].ToString() == "Характеристики товара загружаются...")
                listBoxChars.Items[0] = "*** Характеристики товара не загружены ***";
            //
            richTextBoxReviews.Text = "Отзывы о товаре загружаются...";
            loadReviews();
            if (richTextBoxReviews.Text == "Отзывы о товаре загружаются...")
                richTextBoxReviews.Text = "*** Отзывы не были загружены ***";
            //
            loadPicture("Computers", (int)Row.Cells["Id"].Value);
            //
            if ((int)Row.Cells["Stock"].Value == 0)
                bAddToCart.Enabled = false;
        }

        private void _printUserNickToReview(int id)
        {
            if (id <= 0) return;
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = @"SELECT [Nick] FROM [Users] WHERE [Id]=" + id;
                connection.Open();
                SqlCommand query = new SqlCommand(commandText, connection);
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        richTextBoxReviews.Text += "=== Отзыв от пользователя " + reader.GetString(0);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить отзывы о товаре.", "Ошибка загрузки",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBoxReviews.Text = "*** Отзывы не были загружены ***"; 
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

        private async void loadReviews()
        {
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = @"SELECT [User_id], [Mark], [Advantages], [Disadvantages], [Comment] FROM [Reviews] WHERE [Product_id]=" + (int)Row.Cells[0].Value;
                connection.Open();
                SqlCommand query = new SqlCommand(commandText, connection);
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    richTextBoxReviews.Clear();
                    while (await reader.ReadAsync())
                    {
                        _printUserNickToReview((int)reader.GetValue(0));
                        richTextBoxReviews.Text += "\nОценка по 10-бальной шкале: " + reader.GetValue(1).ToString();
                        //
                        richTextBoxReviews.Text += "\n---Достоинства:\n";
                        if (reader.GetValue(2).ToString() != "")
                            richTextBoxReviews.Text += reader.GetValue(2).ToString();
                        else richTextBoxReviews.Text += " *Не указано*";
                        //
                        richTextBoxReviews.Text += "\n---Недостатки:\n";
                        if (reader.GetValue(3).ToString() != "")
                            richTextBoxReviews.Text += reader.GetValue(3).ToString();
                        else richTextBoxReviews.Text += " *Не указано*";
                        //
                        richTextBoxReviews.Text += "\n---Комментарий:\n";
                        if (reader.GetValue(4).ToString() != "")
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
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = string.Format(@"SELECT * FROM Computers WHERE Id={0}", (int)Row.Cells[0].Value);
                connection.Open();
                SqlCommand query = new SqlCommand(commandText, connection);
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        listBoxChars.Items.Clear();
                        for (int i = 1; i < reader.FieldCount-2; i++)
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
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = string.Format(@"SELECT Description FROM Computers WHERE [Id]={0}", (int)Row.Cells[0].Value);
                command.Connection = connection;
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        if (reader.GetValue(0).ToString() != "")
                            richTextBoxDescription.Text = reader.GetValue(0).ToString();
                        else richTextBoxDescription.Text = "*** Описание отсутствует ***";
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить описание товара.\n Не удалось прочитать данные из \"SqlDataReader\" ", "Ошибка загрузки",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        richTextBoxDescription.Text = "*** Описание не было загружено ***";
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить описание товара", "Ошибка загрузки", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBoxDescription.Text = "*** Описание не было загружено ***";
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

        private void loadPicture(string tableName, int id)
        {
            // загрузка из БД
            Image image = ImageTools.GetImageFromDB(MainForm.StrSQLConnection, tableName, "Picture", id);
            // загрузка изображения в pictureBox
            if (image != null)
                pictureBox.Image = image;
        }
    }
}
