using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm
{
    public partial class OpenF : Form
    {
        public OpenF()
        {
            InitializeComponent();
            lbRun.Text += "         ";
            timer2.Start();
        }
        int x = 0;
        private void OpenF_Load(object sender, EventArgs e)
        {
            lbRun.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            TXL_dl f = new TXL_dl();
            f.Show();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FormTESTcs f = new FormTESTcs();
            f.Show();
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            FormTESTcs f = new FormTESTcs();
            f.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lbRun.Text = lbRun.Text.Substring(1, lbRun.Text.Length - 1) + lbRun.Text.Substring(0, 1);
        }
    }
}
