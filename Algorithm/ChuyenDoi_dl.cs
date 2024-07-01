using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm
{
    public partial class ChuyenDoi_dl : Form
    {
        public ChuyenDoi_dl()
        {
            InitializeComponent();
        }
        public const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True";

        List<string> attributeNames = new List<string>();
        List<string> dataRows = new List<string>();
        string relationName = "";
        private void btnMoTep_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ARFF Files|*.arff";
            openFileDialog1.Title = "Select an ARFF File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string arffFilePath = openFileDialog1.FileName;


                try
                {
                    ReadARFFFile(arffFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi mở tệp ARFF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string ConvertARFFtoSQL(string arffFilePath)
        {
            List<string> attributeNames = new List<string>();
            List<string> dataRows = new List<string>();

            using (StreamReader reader = new StreamReader(arffFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("@attribute"))
                    {
                        string attributeName = Regex.Match(line, "@attribute\\s+(\\w+)").Groups[1].Value;
                        attributeNames.Add(attributeName);
                    }
                    else if (line.StartsWith("@data"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {

                            string[] values = line.Split(',');
                            List<string> formattedValues = new List<string>();
                            for (int i = 0; i < values.Length; i++)
                            {

                                formattedValues.Add($"'{values[i]}'");
                            }

                            dataRows.Add($"({string.Join(",", formattedValues)})");
                        }
                    }
                }
            }

            string sql = $"CREATE TABLE tableName (";

            for (int i = 0; i < attributeNames.Count; i++)
            {
                sql += $"{attributeNames[i]} VARCHAR(255)";
                if (i < attributeNames.Count - 1)
                {
                    sql += ", ";
                }
            }

            sql += ");\n";

            if (dataRows.Count > 0)
            {
                sql += $"INSERT INTO tableName VALUES {string.Join(", ", dataRows)};";
            }

            return sql;
        }

        private void btnChuyenDoi_Click(object sender, EventArgs e)
        {
            try
            {
                ConvertARFFtoSQL();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi chuyển đổi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuVaoCSDL_Click(object sender, EventArgs e)
        {
            try
            {

                RunSQLQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi chạy câu lệnh SQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReadARFFFile(string arffFilePath)
        {
            attributeNames.Clear();
            dataRows.Clear();
            richTextBoxArff.Clear();
            richTextBoxSql.Clear();

            using (StreamReader reader = new StreamReader(arffFilePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    richTextBoxArff.AppendText(line + "\n");
                    if (line.StartsWith("@attribute"))
                    {
                        string attributeName = Regex.Match(line, "@attribute\\s+(\\w+)").Groups[1].Value;
                        attributeNames.Add(attributeName);
                    }
                    else if (line.StartsWith("@data"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            richTextBoxArff.AppendText(line + "\n");
                            string[] values = line.Split(',');
                            List<string> formattedValues = new List<string>();
                            for (int i = 0; i < values.Length; i++)
                            {
                                if (values[i] == "t")
                                    formattedValues.Add($"'yes'");
                                else if (values[i] == "?")
                                    formattedValues.Add($"'no'");
                                else
                                    formattedValues.Add($"'{values[i]}'");
                            }
                            dataRows.Add($"({string.Join(",", formattedValues)})");
                        }
                    }
                    else if (line.StartsWith("@relation"))
                    {
                        relationName = GetRelationName(line);
                    }
                }
            }
        }
        private string GetRelationName(string line)
        {
            int startIndex = line.IndexOf(" ") + 1;
            if (startIndex >= 0)
            {
                return line.Substring(startIndex);
            }
            else
            {
                return "";
            }
        }

        private void ConvertARFFtoSQL()
        {

            richTextBoxSql.Clear();

            string sql = $"CREATE TABLE " + relationName + " (";

            for (int i = 0; i < attributeNames.Count; i++)
            {
                sql += $"{attributeNames[i]} VARCHAR(255)";
                if (i < attributeNames.Count - 1)
                {
                    sql += ", ";
                }
            }

            sql += ");\n";

            if (dataRows.Count > 0)
            {
                sql += $"INSERT INTO {relationName} VALUES {string.Join(", ", dataRows)};";
            }

            richTextBoxSql.AppendText(sql + "\n");
        }
        private void RunSQLQuery()
        {

            string createTableQuery = richTextBoxSql.Text;

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
        }

    }
}
