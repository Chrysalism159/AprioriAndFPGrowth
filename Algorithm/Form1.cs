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

namespace Algorithm
{
    public partial class Form1 : Form
    {
        List<List<string>> listData = new List<List<string>>();
        ItemSets aprioriData = new ItemSets();
        string path = @"D:\Study\KLTN";
        FPGrowth fPGrowth = new FPGrowth();
        private bool isDragging = false;
        private Node draggedNode;
        private Point dragOffset;
        int conf;

        public Form1()
        {
            InitializeComponent();
        }
        private void LoadDatabases()
        {
            // Danh sách tên các cơ sở dữ liệu
            string[] databaseNames = { "Data_1", "Data_2", "Data_3", "Data_test" };
            string[] confList = { "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" };
            cbConf.Items.AddRange(confList);
            // Thêm tên các cơ sở dữ liệu vào ComboBox
            cbData.Items.AddRange(databaseNames);
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
        //private void ConvertToFPTreeData()
        //{
        //    int i = 1;
        //    foreach (var item in listData)
        //    {
        //        ItemSet itemSet = new ItemSet();
        //        itemSet.TID = i;
        //        itemSet.Items = item;
        //        aprioriData.CreateFrequentItemSet(itemSet);
        //        i++;
        //    }
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDatabases();
            txtminConf.Visible = false;
            rtbAddData.Visible = false;
        }
        private void RefreshData(string tableName)
        {
            
            dataGridView1.DataSource =
            Data.GetTable($"select * from {tableName}");
            dataGridView1.Columns[0].Width = 600;
            List<List<string>> dataList = new List<List<string>>();
            dataList = Data.GetDataFromDatabase(tableName);
            listData = dataList;
        }
        private void cbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbAddData.Clear();
            string selectedDatabase = cbData.SelectedItem.ToString();
            RefreshData(selectedDatabase);


        }

        private void AprioriButton_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            richTextBox1.Clear();
            int min_sup, min_conf;
            min_sup = Convert.ToInt32(txtminSup.Text);
            min_conf = conf;
            ItemSets aprioriData = new ItemSets();
            aprioriData = ConvertToAprioriData();
            Apriori apriori = new Apriori(aprioriData, min_sup, min_conf);
            apriori.RunAlgorithm(path);
            string selectedFileName = path + "_" + "AprioriAlgorithm.txt";
            try
            {
                // Đọc nội dung của tệp và gán vào RichTextBox
                string fileContent = File.ReadAllText(selectedFileName);
                richTextBox1.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            watch.Stop();
            label6.Text = "Hiển thị dữ liệu -----" + "\t" + watch.ElapsedMilliseconds.ToString() + "ms";
        }

        
        private void FPGrowthButton_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int min_sup, min_conf;
            min_sup = Convert.ToInt32(txtminSup.Text);
            min_conf = conf;

            fPGrowth.RunAlgorithm(listData, min_sup, path, min_conf);
            string selectedFileName = path + "_" + "FPGrowth.txt";
            richTextBox1.Clear();
            try
            {
                // Đọc nội dung của tệp và gán vào RichTextBox
                string fileContent = File.ReadAllText(selectedFileName);
                richTextBox1.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            watch.Stop();
            label6.Text = "Hiển thị dữ liệu ----- " + "\t" + watch.ElapsedMilliseconds.ToString() + "ms";
        }

        private void cbConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectConf = cbConf.SelectedItem.ToString();
            selectConf = selectConf.Replace("%", "");
            conf = Convert.ToInt32(selectConf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!txtminConf.Visible)
            {
                cbConf.Visible = false;
                txtminConf.Visible = true;
                txtminConf.Focus();
            }
            else
            {
                txtminConf.Visible = false;
                cbConf.Visible = true;
            }
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            rtbAddData.Visible = true;
            rtbAddData.Focus();
        }

        private void rtbAddData_KeyDown(object sender, KeyEventArgs e)
        {
            string tableName = cbData.SelectedItem.ToString();
            if(e.KeyCode == Keys.Enter)
            {
                string sql = "insert into " + tableName + " values ('" + rtbAddData.Text + "')";
                Data.AddEditDelete(sql);
                RefreshData(tableName);
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                rtbAddData.Visible = false;

            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            FPTreeForm form = new FPTreeForm(fPGrowth);
            form.Show();
        }



        //private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        //{
        //    Node root = new Node("");
        //    fPGrowth.fpTree.DrawTree(root, 0, pictureBox1.Width, 50, e.Graphics);
        //}
    }
}
