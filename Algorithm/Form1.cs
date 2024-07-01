using Algorithm.Code;
using Algorithm.FPGrowthAlgorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Algorithm
{
    public partial class Form1 : Form
    {
        List<List<string>> listData = new List<List<string>>();
        ItemSets aprioriData = new ItemSets();
        string path = @"D:\Study\KLTN";
        FPGrowth fPGrowth = new FPGrowth();
        int conf, sup;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void LoadDatabases()
        {
            // Danh sách tên các cơ sở dữ liệu "Data_1", "Data_2", "Data_3", "Data_test", "Data_Weka"
            string[] databaseName = { "Du_Lieu_Hoa_Don_Sieu_Thi", "Du_Lieu_Hoa_Don_Ban_Hang_2", "Du_Lieu_Ma_Hoa", "Du_Lieu_Ma_Hoa_2", "Du_Lieu_Weka_Breast_Cancer" };
            string[] confList = { "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" };
            cbConf.Items.AddRange(confList);
            cbData.Items.AddRange(databaseName);
            // Thêm tên các cơ sở dữ liệu vào ComboBox
        }

        
        private ItemSets ConvertToAprioriData()
        {
            ItemSets aprioriData = new ItemSets();
            int i = 1;
            foreach(var item in listData)
            {
                ItemSet itemSet = new ItemSet();
                itemSet.TID = i;
                itemSet.Items = item;
                aprioriData.CreateFrequentItemSet(itemSet);
                i++;
            }
            return aprioriData;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDatabases();
            
        }
        private void RefreshData(string tableName)
        {
            tableName = cbData.SelectedItem.ToString();
            dataGridView1.DataSource =
            Data.GetTable($"select * from {tableName}");
            if(tableName != "Du_Lieu_Weka_Breast_Cancer" && tableName != "Du_Lieu_Hoa_Don_Ban_Hang_2" && tableName != "Du_Lieu_Hoa_Don_Sieu_Thi")
                dataGridView1.Columns[1].Width = 600;
            List<List<string>> dataList = new List<List<string>>();
            dataList = Data.GetDataFromDatabase(tableName);
            listData = dataList;
        }
        private void cbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDatabase = cbData.SelectedItem.ToString();
            //selectedDatabase = ChangeDatabaseName(selectedDatabase);
            RefreshData(selectedDatabase);


        }

        private void AprioriButton_Click(object sender, EventArgs e)
        {
            AprioriAlgorithm();
        }

        
        private void FPGrowthButton_Click(object sender, EventArgs e)
        {
            FPGrowthAlgorithm();
        }

        private void cbConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectConf = cbConf.SelectedItem.ToString();
            selectConf = selectConf.Replace("%", "");
            conf = Convert.ToInt32(selectConf);
        }
        private int CountConfTextbox()
        {
            if (cbConf.Text != null && int.TryParse(cbConf.Text, out conf))
                return conf;
            return conf;
        }
        
        private void btnDraw_Click(object sender, EventArgs e)
        {
            FPTreeForm form = new FPTreeForm(fPGrowth);
            form.Show();
        }


        private void txtminConf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CountConfTextbox();
        }

        private void AprioriButton_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AprioriAlgorithm();
            }
        }



        private void AprioriAlgorithm()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            richTextBox1.Clear();
            int min_sup, min_conf;
            min_sup = sup;
            min_conf = conf;
            ItemSets aprioriData = new ItemSets();
            aprioriData = ConvertToAprioriData();
            Apriori apriori = new Apriori(aprioriData, min_sup, min_conf);
            apriori.RunAlgorithm(path);
            string selectedFileName = path + "_" + "AprioriAlgorithm.txt";
            bool split = false;
            richTextBox2.Clear();
            richTextBox2.Text = "Thuật toán Apriori" + "\n";
            try
            {
                // Đọc nội dung của tệp và gán vào RichTextBox
                //string fileContent = File.ReadAllText(selectedFileName);
                //richTextBox1.Text = fileContent;
                using (StreamReader reader = new StreamReader(selectedFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Thêm nội dung của dòng đọc được vào biến tạm thời
                        if(!split)
                        {
                            richTextBox1.Text += line + "\n";
                        }
                        else
                            richTextBox2.Text += line + "\n";
                        // Kiểm tra xem dòng đọc có chứa nội dung chỉ định không
                        if (line.Contains("**************************************"))
                        {
                            // Nếu tìm thấy nội dung chỉ định, thoát khỏi vòng lặp
                            split = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            watch.Stop();
            label6.Text = "Hiển thị dữ liệu ----- Thời gian thực hiện chương trình: " + "\t" + watch.ElapsedMilliseconds.ToString() + "ms";
        }

        private void FPGrowthButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                FPGrowthAlgorithm();
        }

        private void cbConf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountConfTextbox();
                txtminSup.Focus();
            }
        }

        private void gunaGradientPanel3_Click(object sender, EventArgs e)
        {

        }

        private void AprioriButton_Click_1(object sender, EventArgs e)
        {
            GetSupportNumber();
            if(sup != 0)
            {
                AprioriAlgorithm();
            }
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            FPTreeForm form = new FPTreeForm(fPGrowth);
            form.Show();
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            GetSupportNumber();
            if(sup != 0)
                FPGrowthAlgorithm();
        }

        private void cbData_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedDatabase = cbData.SelectedItem.ToString();
            RefreshData(selectedDatabase);
        }

        private void cbConf_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectConf = cbConf.SelectedItem.ToString();
            selectConf = selectConf.Replace("%", "");
            conf = Convert.ToInt32(selectConf);
        }
        private void GetSupportNumber()
        {
            try
            {
                sup = Convert.ToInt32(txtminSup.Text);
            }
            catch
            {
                MessageBox.Show("Dữ liệu nhập vào không phải dạng số!");
                txtminSup.Clear();
                txtminSup.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FPGrowthAlgorithm()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int min_sup, min_conf;
            min_sup = sup;
            min_conf = conf;

            fPGrowth.RunAlgorithm(listData, min_sup, path, min_conf);
            string selectedFileName = path + "_" + "FPGrowth.txt";
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox2.Text = "Thuật toán FP-Growth" + "\n";
            bool split = false;
            try
            {
                // Đọc nội dung của tệp và gán vào RichTextBox
                //string fileContent = File.ReadAllText(selectedFileName);
                //richTextBox1.Text = fileContent;
                using (StreamReader reader = new StreamReader(selectedFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Thêm nội dung của dòng đọc được vào biến tạm thời
                        if (!split)
                        {
                            richTextBox1.Text += line + "\n";
                        }
                        else
                            richTextBox2.Text += line + "\n";
                        // Kiểm tra xem dòng đọc có chứa nội dung chỉ định không
                        if (line.Contains("**************************************"))
                        {
                            // Nếu tìm thấy nội dung chỉ định, thoát khỏi vòng lặp
                            split = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            watch.Stop();
            label6.Text = "Hiển thị dữ liệu ----- Thời gian thực hiện chương trình: " + "\t" + watch.ElapsedMilliseconds.ToString() + "ms";
        }

    }
}
