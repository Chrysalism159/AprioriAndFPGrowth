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
    public partial class FormXoaBang : Form
    {
        List<List<string>> listData = new List<List<string>>();
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";
        string delcontent;
        public FormXoaBang()
        {
            InitializeComponent();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT name FROM sys.tables", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string tableName = reader["name"].ToString();
                    comboBox2.Items.Add(tableName);
                }
            }
            gunaLabel2.Text = "Chọn tên bảng muốn xóa";
            gunaLabel2.Visible = true;
        }
    }
}
