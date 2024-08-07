﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Algorithm
{
    public partial class TSXdb : Form
    {
        List<List<string>> listData = new List<List<string>>();
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";
        string delcontent;
        public TSXdb()
        {
            InitializeComponent();
        }

        private void gunaImageButton1_Click(object sender, EventArgs e)
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txtColumnName.Clear();
            txttenbang.Clear();
            listView1.Clear();
            comboBox1.SelectedIndex = -1;
            dataGridView1.DataSource = null;

            gunaLabel1.Visible = false;
            txttenbang.Visible = false;
            btnHuy.Visible = false;
            btnThem.Visible = false;
            btnSua.Visible = false;
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
            groupBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void gunaImageButton3_Click(object sender, EventArgs e)
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

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void gunaImageButton2_Click(object sender, EventArgs e)
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
            panel1.Visible = true;
            gunaLabel2.Text = "Chọn tên bảng muốn sửa";
            gunaLabel2.Visible = true;
            comboBox2.Visible = true;
            comboBox2.SelectedIndex = -1;
            panel2.Visible = true;
            btnHuy.Visible = true;
            btnXoalon.Enabled = false;
            btnThemlon.Enabled = false;
            groupBox1.Visible = true;
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

        private void TSXdb_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox1.Text = "Lựa chọn giá trị thêm vào bảng";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string selectedTableName = comboBox2.SelectedItem.ToString();
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            int primaryKeyValue = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            string updateQuery = $"UPDATE {selectedTableName} SET {columnName} = '{newValue}' WHERE TID = '{primaryKeyValue}'";
            Data.AddEditDelete(updateQuery);
            MessageBox.Show("Cập nhật dữ liệu thành công!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData(selectedTableName);
        }

        private void btnXoanho_Click(object sender, EventArgs e)
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
        private string InsertQuerry(string nameTable)
        {
            btnInsert.Visible = true;
            string sql = $"insert into {nameTable} values ('";
            if(nameTable == "Data_supermarket")
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
                    sql +=  "yes',";
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
            else if (nameTable == "Data_test")
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
            else if (nameTable == "Data_test_2")
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
            else if (nameTable == "Data_1")
            {
                checkBox1.Text = "Bread";
                checkBox2.Text = "Milk";
                checkBox3.Text = "Snacks";
                checkBox4.Text = "Egg";
                checkBox5.Text = "Coca";
                checkBox6.Text = "Candy";
                checkBox2.Visible = true;
                checkBox4.Visible = true;
                checkBox6.Visible = true;
                checkBox7.Visible = false;
                checkBox8.Visible = false;
                checkBox9.Visible = false;
                checkBox10.Visible = false;
                if (checkBox1.Checked)
                    sql += $"{checkBox1.Text},";
                if (checkBox2.Checked)
                    sql += $"{checkBox2.Text},";
                if (checkBox3.Checked)
                    sql += $"{checkBox3.Text},";
                if (checkBox5.Checked)
                    sql += $"{checkBox5.Text},";
                if (checkBox4.Checked)
                    sql += $"{checkBox4.Text},";
                if (checkBox6.Checked)
                    sql += $"{checkBox6.Text}'";
            }
            else if (nameTable == "Data_2")
            {
                checkBox1.Text = "Book";
                checkBox2.Text = "Pen";
                checkBox3.Text = "Ruler";
                checkBox4.Text = "Pencil";
                checkBox5.Text = "Eraser";
                checkBox6.Text = "NoteBook";
                checkBox7.Visible = false;
                checkBox8.Visible = false;
                checkBox9.Visible = false;
                checkBox10.Visible = false;
                if (checkBox1.Checked)
                    sql += $"{checkBox1.Text},";
                if (checkBox2.Checked)
                    sql += $"{checkBox2.Text},";
                if (checkBox3.Checked)
                    sql += $"{checkBox3.Text},";
                if (checkBox5.Checked)
                    sql += $"{checkBox5.Text},";
                if (checkBox4.Checked)
                    sql += $"{checkBox4.Text},";
                if (checkBox6.Checked)
                    sql += $"{checkBox6.Text}'";
            }
            if (sql.EndsWith(","))
                sql=sql.Remove(sql.Length - 1);
            sql += "')";
            return sql;
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                delcontent = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }
        private void btnThemnho_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            string selectedTableName = comboBox2.SelectedItem.ToString();
            string sql = InsertQuerry(selectedTableName);


            //LoadListView(selectedTableName);
            //RefreshData(selectedTableName);
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

        private void btnSuanho_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


