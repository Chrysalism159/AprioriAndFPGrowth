using Guna.UI.WinForms;
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

namespace Algorithm
{
    public partial class FormTESTcs : Form
    {
        public Form activeForm;
        List<List<string>> listData = new List<List<string>>();
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";
        string delcontent;
        public FormTESTcs()
        {
            InitializeComponent();
            btnCloseChildForm.Visible = false;
            customizeDesing();
        }
        private void customizeDesing()
        {
            panelSua.Visible = false;
        }
        private void hideSua()
        {
            if (panelSua.Visible == true)
                panelSua.Visible = false;
        }
        private void showSua(Panel Sua)
        {
            if (Sua.Visible == false)
            {
                hideSua();
                Sua.Visible = true;
            }
            else
                Sua.Visible = false;
        }
        public void Reset()
        {
            lblTitle.Text = "QUẢN LÝ CƠ SỞ DỮ LIỆU";
            btnCloseChildForm.Visible = false;
        }
        private void RefreshData(string tableName)
        {
            tableName = comboBox2.SelectedItem.ToString();
            dataGridView1.DataSource =
            Data.GetTable($"select * from {tableName}");
            if (tableName != "Du_Lieu_Weka_Breast_Cancer" && tableName != "Du_Lieu_Hoa_Don_Ban_Hang_2" && tableName != "Du_Lieu_Hoa_Don_Sieu_Thi")
                dataGridView1.Columns[1].Width = 600;
            List<List<string>> dataList = new List<List<string>>();
            dataList = Data.GetDataFromDatabase(tableName);
            listData = dataList;
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            btnCloseChildForm.Visible = true;
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            this.panelDestop.Controls.Add(childForm);
            this.panelDestop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void btnThemBang_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            gunaLabel1.Visible = true;
            txttenbang.Visible = true;
            txttenbang.Focus();
            btnHuy.Visible = true;
            btnThem.Visible = true;
            btnthemCot.Visible = true;
            txtColumnName.Visible = true;
            comboBox1.Visible = true;
            listView1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            btnSualon.Enabled = false;
            btnXoalon.Enabled = false;
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                Reset();
            }
        }

        private void btnSuaBang_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
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
            //string[] databaseName = { "Du_Lieu_Hoa_Don_Sieu_Thi", "Du_Lieu_Ma_Hoa", "Du_Lieu_Ma_Hoa_2" };
            //comboBox2.Items.AddRange(databaseName);
            showSua(panelSua);
            gunaLabel2.Text = "Chọn tên bảng muốn sửa";
            gunaLabel2.Visible = true;
            comboBox2.Visible = true;
            comboBox2.SelectedIndex = -1;
            panel2.Visible = true;
            btnHuy.Visible = true;
            btnXoalon.Enabled = false;
            btnThemlon.Enabled = false;
            
        }


        private void btnXoaCSDL_Click(object sender, EventArgs e)
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

        private void btnXoaBang_Click(object sender, EventArgs e)
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
            comboBox2.Visible = true;
            gunaLabel2.Visible = true;
            btnHuy.Visible = true;
            btnXoa.Visible = true;
            btnSualon.Enabled = false;
            btnThemlon.Enabled = false;
        }

        private void btnSuaBang_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Formm.FormSuaCSDL(), sender);
            hideSua();
        }

        private void btnThemCSDL_Click(object sender, EventArgs e)
        {
            
            string selectedTableName = comboBox2.SelectedItem.ToString();
            groupBox1.Visible = true;
            string sql = InsertQuerry(selectedTableName);
        }
        private string InsertQuerry(string nameTable)
        {
            //"Du_Lieu_Hoa_Don_Sieu_Thi", "Du_Lieu_Hoa_Don_Ban_Hang_2", "Du_Lieu_Ma_Hoa", "Du_Lieu_Ma_Hoa_2", "Du_Lieu_Weka_Breast_Cancer"
            btnInsert.Visible = true;
            string sql = $"insert into {nameTable} values ('";
            if (nameTable == "Du_Lieu_Hoa_Don_Sieu_Thi")
            {
                checkBox1.Text = "Coca";
                checkBox2.Text = "Snacks";
                checkBox3.Text = "Apple";
                checkBox4.Text = "Noodle";
                checkBox5.Text = "Lemon";
                checkBox6.Text = "Sausage";
                checkBox7.Text = "Pepsi";
                checkBox8.Text = "Ham";
                checkBox9.Text = "Watermelon";
                checkBox10.Text = "Peper";

                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
                checkBox7.Visible = true;
                checkBox8.Visible = true;
                checkBox9.Visible = true;
                checkBox10.Visible = true;
                if (checkBox1.Checked)
                    sql += "yes',";
                else sql += "no',";
                if (checkBox2.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox3.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox4.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox5.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox6.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox7.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox8.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox9.Checked)
                    sql += "'yes',";
                else sql += "'no',";
                if (checkBox10.Checked)
                    sql += "'yes'";
                else sql += "'no'";

            }
            else if (nameTable == "Du_Lieu_Ma_Hoa")
            {
                checkBox1.Text = "A";
                checkBox2.Visible = false;
                checkBox3.Text = "B";
                checkBox4.Visible = false;
                checkBox5.Text = "C";
                checkBox6.Visible = false;
                checkBox7.Text = "D";
                checkBox8.Visible = false;
                checkBox9.Text = "E";
                checkBox10.Visible = false;
                if (checkBox1.Checked)
                    sql += $"{checkBox1.Text},";
                if (checkBox3.Checked)
                    sql += $"{checkBox3.Text},";
                if (checkBox5.Checked)
                    sql += $"{checkBox5.Text},";
                if (checkBox7.Checked)
                    sql += $"{checkBox7.Text},";
                if (checkBox9.Checked)
                    sql += $"{checkBox9.Text},";
            }
            else if (nameTable == "Du_Lieu_Ma_Hoa_2")
            {
                checkBox1.Text = "A";
                checkBox2.Visible = false;
                checkBox3.Text = "C";
                checkBox4.Visible = false;
                checkBox5.Text = "D";
                checkBox6.Visible = false;
                checkBox7.Text = "T";
                checkBox8.Visible = false;
                checkBox9.Text = "W";
                checkBox10.Visible = false;
                if (checkBox1.Checked)
                    sql += $"{checkBox1.Text},";
                if (checkBox3.Checked)
                    sql += $"{checkBox3.Text},";
                if (checkBox5.Checked)
                    sql += $"{checkBox5.Text},";
                if (checkBox7.Checked)
                    sql += $"{checkBox7.Text},";
                if (checkBox9.Checked)
                    sql += $"{checkBox9.Text}";
            }
            else
            {
                checkBox1.Text = "A";
                checkBox2.Text = "B";
                checkBox3.Text = "C";
                checkBox4.Text = "D";
                checkBox5.Text = "E";
                checkBox6.Text = "F";
                checkBox7.Text = "G";
                checkBox8.Text = "H";
                checkBox9.Text = "I";
                checkBox10.Text = "K";
                if (checkBox1.Checked)
                    sql += $"{checkBox1.Text},";
                if (checkBox3.Checked)
                    sql += $"{checkBox3.Text},";
                if (checkBox5.Checked)
                    sql += $"{checkBox5.Text},";
                if (checkBox7.Checked)
                    sql += $"{checkBox7.Text},";
                if (checkBox9.Checked)
                    sql += $"{checkBox9.Text},";
                if (checkBox2.Checked)
                    sql += $"{checkBox2.Text},";
                if (checkBox4.Checked)
                    sql += $"{checkBox4.Text},";
                if (checkBox6.Checked)
                    sql += $"{checkBox6.Text},";
                if (checkBox8.Checked)
                    sql += $"{checkBox8.Text},";
                if (checkBox10.Checked)
                    sql += $"{checkBox10.Text},";
            }
            if (sql.EndsWith(","))
                sql = sql.Remove(sql.Length - 1);
            sql += "')";
            return sql;
        }

        private void btnSuaCSDL_Click(object sender, EventArgs e)
        {
            
        }

        private void FormTESTcs_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox1.Text = "Lựa chọn giá trị thêm vào bảng";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hideSua();
            txtColumnName.Clear();
            txttenbang.Clear();
            listView1.Clear();
            comboBox1.SelectedIndex = -1;
            dataGridView1.DataSource = null;

            gunaLabel1.Visible = false;
            txttenbang.Visible = false;
            btnHuy.Visible = false;
            btnThem.Visible = false;
            btnXoa.Visible = false;
            btnthemCot.Visible = false;
            txtColumnName.Visible = false;
            comboBox1.Visible = false;
            listView1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            comboBox2.Visible = false;
            gunaLabel2.Visible = false;
            btnThemlon.Enabled = true;
            btnSualon.Enabled = true;
            btnXoalon.Enabled = true;
            groupBox1.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                delcontent = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
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

            string createTableQuery = $"CREATE TABLE {tableName} (TID INT IDENTITY(1,1), ";

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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string selectedTableName = comboBox2.SelectedItem.ToString();
            if (comboBox2.SelectedIndex != -1)
            {
                string deleteTableQuery = $"DROP TABLE {selectedTableName}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(deleteTableQuery, connection);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Bảng đã được xóa thành công.");
                        comboBox2.Items.Remove(selectedTableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa bảng: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bảng để xóa.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDatabase = comboBox2.SelectedItem.ToString();
            InsertQuerry(selectedDatabase);
            RefreshData(selectedDatabase);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string selectedTableName = comboBox2.SelectedItem.ToString();
            string sql = InsertQuerry(selectedTableName);
            Data.AddEditDelete(sql);
            MessageBox.Show("Thêm dữ liệu thành công!",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData(selectedTableName);
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string selectedTableName = comboBox2.SelectedItem.ToString();
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            int primaryKeyValue = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (primaryKeyValue != null)
            {
                string updateQuery = $"UPDATE {selectedTableName} SET {columnName} = '{newValue}' WHERE TID = '{primaryKeyValue}'";
                Data.AddEditDelete(updateQuery);
                MessageBox.Show("Cập nhật dữ liệu thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(selectedTableName);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
