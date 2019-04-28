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
    public partial class UsersManagement : Form
    {
        // словарь "Ник" - "Изображение"
        Dictionary<string, object> imgs = new Dictionary<string, object>();
        // словарь "Ник" - "флаг изменения статуса"
        Dictionary<string, bool> statusChanges = new Dictionary<string, bool>();

        public UsersManagement()
        {
            InitializeComponent();
        }

        private void UsersManagement_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private async void updateList()
        {
            if (listView.Items.Count != 0) listView.Items.Clear();
            SqlConnection connection = new SqlConnection(MainForm.StrSQLConnection);
            SqlDataReader reader = null;
            try
            {
                string str = @"SELECT * FROM Users";
                SqlCommand command = new SqlCommand(str, connection);
                await connection.OpenAsync();
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        // вывод в listView
                        ListViewItem item = new ListViewItem(reader.GetValue(1).ToString());
                        item.SubItems.Add(reader.GetValue(2).ToString());
                        item.SubItems.Add(reader.GetValue(3).ToString());
                        item.SubItems.Add(reader.GetValue(4).ToString());
                        item.SubItems.Add(reader.GetValue(5).ToString());
                        item.SubItems.Add(reader.GetValue(7).ToString());
                        listView.Items.Add(item);
                        // сохраняем изображение
                        imgs.Add(reader.GetValue(5).ToString(), reader.GetValue(8));
                        // установка флага изменения статуса
                        statusChanges.Add(reader.GetValue(5).ToString(), false);
                    }
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
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems// .SubItems[5].ToString().ToUpper() == "ADMIN")
                radioButtonAdmin.Checked = true;
            if (listView.SelectedItems[0].SubItems[5].ToString().ToUpper() == "USER")
                radioButtonUser.Checked = true;
        }

        private void radioButtonUser_CheckedChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems[0].SubItems[5].ToString().ToUpper() == "ADMIN")
                statusChanges[listView.SelectedItems[0].SubItems[4].ToString()] = true;
        }

        private void radioButtonAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems[0].SubItems[5].ToString().ToUpper() == "USER")
                statusChanges[listView.SelectedItems[0].SubItems[4].ToString()] = true;
        }
    }
}