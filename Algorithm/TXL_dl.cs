using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm
{
    public partial class TXL_dl : Form
    {
        public TXL_dl()
        {
            InitializeComponent();
        }
        List<string> attributeNames = new List<string>();
        List<Dictionary<string, int>> attributeCounts = new List<Dictionary<string, int>>();
        List<Dictionary<string, int>> attributeValues = new List<Dictionary<string, int>>();
        RichTextBox rtb = new RichTextBox();
        
        private int GetAttributeCount(string attributeName)
        {
            foreach (Dictionary<string, int> attributeInfo in attributeCounts)
            {
                foreach (KeyValuePair<string, int> kvp in attributeInfo)
                {
                    if (kvp.Key.Replace(" ", "").Equals(attributeName.Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        return kvp.Value;
                    }
                }
            }
            return 0;
        }
        public List<Dictionary<string, int>> GetAttributeCounts(string input)
        {
            // List<Dictionary<string, int>> attributeCounts = new List<Dictionary<string, int>>();
            MatchCollection matches = Regex.Matches(input, @"@attribute\s+([\w\s]+)\s+\{([^}]*)\}");
            foreach (Match match in matches)
            {
                string attributeName = match.Groups[1].Value.Trim();
                string values = match.Groups[2].Value;
                int count = values.Split(',').Length;
                Dictionary<string, int> attributeInfo = new Dictionary<string, int>();
                attributeInfo.Add(attributeName, count);
                attributeCounts.Add(attributeInfo);
            }

            return attributeCounts;
        }

        public List<Dictionary<string, int>> GetAttributeValues(string attributeName, string input)
        {
            string pattern = "@attribute\\s+" + Regex.Escape(attributeName) + "\\s+\\{([^}]*)\\}";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string valuesStr = match.Groups[1].Value;
                string[] values = valuesStr.Split(',');
                foreach (string value in values)
                {
                    string trimmedValue = value.Trim();
                    if (!string.IsNullOrEmpty(trimmedValue))
                    {
                        int valueCount = Regex.Matches(input, @"\b" + Regex.Escape(trimmedValue) + @"\b").Count;
                        Dictionary<string, int> attributeInfo = new Dictionary<string, int>();
                        attributeInfo.Add(trimmedValue, valueCount);
                        attributeValues.Add(attributeInfo);
                    }
                }
            }
            return attributeValues;
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


        private void nutOpen_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ARFF Files (*.arff)|*.arff|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    attributeNames.Clear();
                    attributeCounts.Clear();

                    List<List<string>> data = new List<List<string>>();
                    int transactionCount = 0;
                    string relationName = "";

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        bool isDataSection = false;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!isDataSection)
                            {
                                if (line.StartsWith("@attribute"))
                                {
                                    int startIndex = line.IndexOf(" ") + 1;
                                    int endIndex = line.LastIndexOf(" ");
                                    string attributeName = line.Substring(startIndex, endIndex - startIndex);
                                    attributeNames.Add(attributeName);
                                }
                                else if (line.StartsWith("@data"))
                                {
                                    isDataSection = true;
                                }
                                else if (line.StartsWith("@relation"))
                                {
                                    relationName = GetRelationName(line);
                                }
                            }
                            else
                            {
                                string[] values = line.Split(',');
                                data.Add(new List<string>(values));
                                transactionCount++;
                            }
                        }
                    }
                    string fileContent = File.ReadAllText(filePath);

                    attributeCounts = GetAttributeCounts(fileContent);


                    dataGridView1.Columns.Clear();


                    DataGridViewColumn sttColumn = new DataGridViewTextBoxColumn();
                    sttColumn.Name = "Number";
                    sttColumn.HeaderText = "STT";
                    sttColumn.Width = 50;
                    dataGridView1.Columns.Add(sttColumn);


                    DataGridViewColumn attributeNameColumn = new DataGridViewTextBoxColumn();
                    attributeNameColumn.Name = "AttributeName";
                    attributeNameColumn.HeaderText = "Tên thuộc tính";
                    attributeNameColumn.Width = 600;
                    dataGridView1.Columns.Add(attributeNameColumn);


                    for (int i = 0; i < attributeNames.Count; i++)
                    {
                        dataGridView1.Rows.Add((i + 1).ToString(), attributeNames[i]);
                    }
                    lbNameDB.Text = "Tên tập dữ liệu: " + relationName;
                    lbSLGiaoDich.Text = "Số lượng giao dịch: " + transactionCount.ToString();
                    lbSLthuoctinh.Text = "Số lượng thuộc tính: " + attributeNames.Count.ToString();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không thể mở tệp tin: " + ex.Message);
                }
                //


                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        rtb.AppendText(line + "\n");
                    }
                }

            }

        }

        private void dataGridView1_CellClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string attributeName = selectedRow.Cells["AttributeName"].Value.ToString();
                int count = GetAttributeCount(attributeName);
                lbTenThuocTinh.Text = "Tên thuộc tính: " + attributeName;
                lbSLGiaTri.Text = "Số lượng giá trị: " + count.ToString();
                //string value = GetAttributeValues(attributeName, rtb.ToString());


            }
        }

        private void gunaGradientButton1_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = true;
            }
        }

        private void gunaGradientButton2_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }
        }

        private void gunaGradientButton3_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = !row.Selected;
            }
        }

        private void associateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void gunaGradientButton5_Click_1(object sender, EventArgs e)
        {
            ChuyenDoi_dl f = new ChuyenDoi_dl();
            f.Show();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
