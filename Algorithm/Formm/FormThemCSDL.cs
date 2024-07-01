using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Algorithm.Formm
{
    public partial class FormThemCSDL : Form
    {
        public FormThemCSDL()
        {
            InitializeComponent();
        }

        private void FormThemCSDL_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox1.Text = "Lựa chọn giá trị thêm vào bảng";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            string selectedTableName = comboBox2.SelectedItem.ToString();
            string sql = InsertQuerry(selectedTableName);
        }
        private string InsertQuerry(string nameTable)
        {
            btnInsert.Visible = true;
            string sql = $"insert into {nameTable} values ('";
            if (nameTable == "Data_supermarket")
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
                sql = sql.Remove(sql.Length - 1);
            sql += "')";
            return sql;
        }
    }
}
