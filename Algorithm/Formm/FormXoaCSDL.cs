using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm.Formm
{
    public partial class FormXoaCSDL : Form
    {
        List<List<string>> listData = new List<List<string>>();
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";
        string delcontent;
        public FormXoaCSDL()
        {
            InitializeComponent();
        }
        private void RefreshData(string tableName)
        {

            dataGridView1.DataSource =
            Data.GetTable($"select * from {tableName}");
            if (tableName != "Data_Weka" && tableName != "Data_supermarket")
                dataGridView1.Columns[1].Width = 600;
            List<List<string>> dataList = new List<List<string>>();
            dataList = Data.GetDataFromDatabase(tableName);
            listData = dataList;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string selectedTableName = comboBox2.SelectedItem.ToString();
            string sql = $"delete from {selectedTableName} where TID='" +
               delcontent + "'";
            if (delcontent != null)
            {
                Data.AddEditDelete(sql);
                MessageBox.Show("Xóa dữ liệu thành công!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(selectedTableName);
            }
            else
            {
                MessageBox.Show("Bạn chưa lựa chọn dữ liệu cần xóa!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
