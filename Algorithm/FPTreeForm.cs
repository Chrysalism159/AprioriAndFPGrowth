using Algorithm.FPGrowthAlgorithm;
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
    public partial class FPTreeForm : Form
    {
        FPGrowth _fPGrowth = new FPGrowth();
        public FPTreeForm(FPGrowth fPGrowth)
        {
            this._fPGrowth = fPGrowth;
            InitializeComponent();

        }

        private void FPTreeForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Node root = new Node("");
            //if (_fPGrowth.fpTree != null)
            //{
            //    _fPGrowth.DrawTree(_fPGrowth.fpTree.root, 0, pictureBox1.Width, 50, e.Graphics);
            //}
            //_fPGrowth.DrawTree(root, 0, pictureBox1.Width, 50, e.Graphics);
        }

        private void FPTreeForm_SizeChanged(object sender, EventArgs e)
        {
            //this.pictureBox1.Width = this.Width;
            //this.pictureBox1.Height = this.Height;
        }

        private void FPTreeForm_Paint(object sender, PaintEventArgs e)
        {
            Node root = new Node("");
            if (_fPGrowth.fpTree != null)
            {
                _fPGrowth.DrawTree(_fPGrowth.fpTree.root, 0, this.Width, 50, e.Graphics);
            }
            _fPGrowth.DrawTree(root, 0, this.Width, 50, e.Graphics);
        }
    }
}
