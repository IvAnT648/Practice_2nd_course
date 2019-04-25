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
    public partial class Product : Form
    {
        public DataGridViewRow row { set; get; } = null;

        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            if (row != null)
            {
                lableProductName.Text = row.Cells["Brand"].Value.ToString() + " " + row.Cells["Model"].Value.ToString();
                labelCost.Text = "Цена: " + row.Cells["Cost"].Value.ToString();
                labelInStock.Text = "На складе: " + row.Cells["In stock"].Value;
                //
                richTextBoxDescription.Text = "Описание загружается...";
                loadDescription();
                //
                listBoxChars.Items.Add("Характеристики товара загружаются...");
                loadCharacteristics();
                //
                richTextBoxReviews.Text = "Отзывы о товаре загружаются...";
                loadReviews();
            }
            else
            {
                MessageBox.Show("Окно не получило данные о товаре и поэтому будет закрыто.", "Ошибка загрузки", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void _printUserNickToReview(int id)
        {
            if (id <= 0) return;
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = "SELECT [Nick] FROM [Users] WHERE [User_id]=" + id;
                connection.OpenAsync();
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
                    richTextBoxReviews.Text = "***Отзывы не были загружены***"; 
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
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = "SELECT [User_id], [Mark], [Advantages], [Disadvantages], [Comment] FROM [Reviews] WHERE [Product_id]=" + (int)row.Cells[0].Value;
                await connection.OpenAsync();
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
                    richTextBoxReviews.Text = "***Отзывов нет***";
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
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string commandText = "SELECT * FROM [" + MainForm.CurrentTable + "] WHERE [Id]=" + (int)row.Cells[0].Value;
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
                            if (reader.GetValue(i).ToString() == "") continue;
                            string text = reader.GetName(i).ToString() + ": " + reader.GetValue(i).ToString();
                            listBoxChars.Items.Add(text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить характеристики товара.\n Не удалось прочитать данные из \"SqlDataReader\" ", "Ошибка загрузки",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listBoxChars.Items.Clear();
                        listBoxChars.Items.Add("***Характеристики товара не загружены***");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить характеристики товара.", "Ошибка загрузки",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxChars.Items.Clear();
                    listBoxChars.Items.Add("***Характеристики товара не загружены***");
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
            SqlConnection connection = new SqlConnection(Common.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT [Description] FROM [" + MainForm.CurrentTable + "] WHERE [Id]=" + (int)row.Cells[0].Value;
                command.Connection = connection;
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        if (reader.GetValue(0).ToString() != "")
                            richTextBoxDescription.Text = reader.GetValue(0).ToString();
                        else richTextBoxDescription.Text = "***Описание отсутствует***";
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить описание товара.\n Не удалось прочитать данные из \"SqlDataReader\" ", "Ошибка загрузки",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        richTextBoxDescription.Text = "***Описание не было загружено***";
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить описание товара", "Ошибка загрузки", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBoxDescription.Text = "***Описание не было загружено***";
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
    }
}
