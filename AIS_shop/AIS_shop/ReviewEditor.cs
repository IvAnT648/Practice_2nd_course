using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIS_shop
{
    public partial class ReviewEditor : Form
    {

        User user = User.GetUser();
        int Product_id { set; get; } = 0;
        int Mark { set; get; } = 0;
        string Advantages { set; get; } = null;
        string Disadvantages { set; get; } = null;
        string Comment { set; get; } = null;
        bool fCreateReview = false;

        public ReviewEditor(int product_id)
        {
            InitializeComponent();
            Product_id = product_id;
        }

        private void ReviewEditor_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            var connection = new SqlConnection(Common.StrSQLConnection);
            string text = $@"SELECT CONCAT(Производитель, ' ', Модель) AS Name FROM Products WHERE Id={Product_id}";
            SqlCommand query = new SqlCommand(text, connection);
            SqlDataReader reader = null;
            try
            {
                await connection.OpenAsync();
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                        labelProductName.Text = reader.GetValue(0).ToString();
                }
                reader.Close();
                reader = null;

                query.CommandText = $@"SELECT Mark, Advantages, Disadvantages, Comment FROM Reviews WHERE Product_id={Product_id} AND User_id={user.Id}";
                reader = await query.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        // загрузка из БД
                        if ((int)reader.GetValue(0) > 5)
                            Mark = 5;
                        else
                        if ((int)reader.GetValue(0) < 1)
                            Mark = 1;
                        else Mark = (int)reader.GetValue(0);
                        Advantages = reader.GetValue(1).ToString();
                        Disadvantages = reader.GetValue(2).ToString();
                        Comment = reader.GetValue(3).ToString();
                        // загрузка в форму
                        numericUpDownMark.Value = Mark;
                        richTextBoxAdvantages.Text = Advantages;
                        richTextBoxDisadvantages.Text = Disadvantages;
                        richTextBoxComment.Text = Comment;
                    }
                }
                else fCreateReview = true;
                reader.Close();
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

        private async void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection(Common.StrSQLConnection);;
            string text = null;

            if (fCreateReview)
                text = $@"INSERT INTO Reviews (Product_id, User_id, Mark, Advantages, Disadvantages, Comment) 
                            VALUES ({Product_id}, {user.Id}, {numericUpDownMark.Value}, @adv, @disadv, @comm)";
            else
                text = $@"UPDATE Reviews SET Mark={numericUpDownMark.Value}, Advantages=@adv, Disadvantages=@disadv, Comment=@comm WHERE Product_id={Product_id} AND User_id={user.Id}";

            SqlCommand query = new SqlCommand(text, connection);
            query.Parameters.AddWithValue("@adv", richTextBoxAdvantages.Text.Replace("'", "''"));
            query.Parameters.AddWithValue("@disadv", richTextBoxDisadvantages.Text.Replace("'", "''"));
            query.Parameters.AddWithValue("@comm", richTextBoxComment.Text.Replace("'", "''"));
            try
            {
                await connection.OpenAsync();
                if (await query.ExecuteNonQueryAsync() == 1)
                {
                    MessageBox.Show("Операция успешно выполнена.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
