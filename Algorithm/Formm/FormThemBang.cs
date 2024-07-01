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

namespace Algorithm.Formm
{
    public partial class FormThemBang : Form
    {
        List<List<string>> listData = new List<List<string>>();
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";
        string delcontent;
        public FormThemBang()
        {
            InitializeComponent();
        }

        private void btnthemCot_Click(object sender, EventArgs e)
        {
            string propertyName = txtColumnName.Text;
            string dataType = comboBox1.SelectedItem.ToString();

            ListViewItem item = new ListViewItem(new[] { propertyName, dataType });
            listView1.Items.Add(item);

            txtColumnName.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tableName = txttenbang.Text;

            string createTableQuery = $"CREATE TABLE {tableName} (ID INT IDENTITY(1,1), ";

            foreach (ListViewItem item in listView1.Items)
            {
                string columnName = item.SubItems[0].Text;
                string dataType = item.SubItems[1].Text;
                createTableQuery += $"{columnName} {dataType}, ";
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(createTableQuery, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            txtColumnName.Clear();
            txttenbang.Clear();
            listView1.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }
    }
}
