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
        Font font = new Font("Arial", 12, FontStyle.Bold);
        private const int Radius = 20;
        private const int Gap = 50;
        private Point treePosition = new Point(0, 0);
        private Point lastMousePosition = Point.Empty;
        private Node selectedNode;
        private Dictionary<string, string> nodeDisplay;
        public FPTreeForm(FPGrowth fPGrowth)
        {
            this._fPGrowth = fPGrowth;
            InitializeComponent();

        }

        private void FPTreeForm_Load(object sender, EventArgs e)
        {
            nodeDisplay = new Dictionary<string, string>();
        }


        private void FPTreeForm_Paint(object sender, PaintEventArgs e)
        {
            Node root = new Node("");
            if (_fPGrowth.fpTree != null)
            {
                DrawTree(_fPGrowth.fpTree.root, 0, this.Width, 50, e.Graphics);
            }
            DrawTree(root, 0, this.Width, 50, e.Graphics);
        }
        public void DrawTree(Node node, int left, int right, int top, Graphics g)
        {
            if (node == null) return;

            // Sử dụng vị trí cây hiện tại để tính toán vị trí của nút
            int x = (left + right) / 2 + treePosition.X;
            int y = top + treePosition.Y;
            // Vẽ nút hiện tại
            g.FillEllipse(Brushes.White, x - Radius, y, 2 * Radius, 2 * Radius);
            g.DrawEllipse(Pens.Black, x - Radius, y, 2 * Radius, 2 * Radius);
            g.DrawString(node.NameNode + " (" + node.FpCount + ")", font, Brushes.Black, x - 20, y + 8);

            // Tính toán vị trí của các nút con
            int childCount = node.Children != null ? node.Children.Count : 0;
            int childGap = 200; // Khoảng cách cố định giữa các nút con
            int childLeft = x - (childCount - 1) * childGap / 2;

            // Vẽ các liên kết tới nút con và gọi đệ quy cho mỗi nút con
            if (node.Children != null)
            {
                for (int i = 0; i < childCount; i++)
                {
                    int childX = childLeft + i * childGap;
                    int childY = y + Gap * 2; // Dịch chuyển xuống dưới một dòng so với nút cha

                    // Tính toán vị trí của các nút con dựa trên số lượng nút con
                    int childGapMultiplier = GetGapMultiplier(i, childCount);
                    childX += childGap * childGapMultiplier;

                    g.DrawLine(Pens.Black, x, y + Radius * 2, childX, childY);
                    DrawTree(node.Children[i], childX - Radius, childX + Radius, y + Gap * 2, g);
                }
            }
        }

        // Hàm để tính toán khoảng cách giữa các nhánh
        private int GetGapMultiplier(int index, int childCount)
        {
            return index - childCount / 2;
        }
        
        private void FPTreeForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Kiểm tra xem có đang nhấn chuột không
            if (selectedNode != null && e.Button == MouseButtons.Left)
            {
                // Cập nhật vị trí của nút được chọn
                selectedNode.PositionX += e.X - lastMousePosition.X;
                selectedNode.PositionY += e.Y - lastMousePosition.Y;

                // Lưu lại vị trí chuột
                lastMousePosition = e.Location;

                // Vẽ lại cây ở vị trí mới
                Refresh();
            }
        }
        private Node FindNodeAtLocation(Node node, Point location)
        {
            // Kiểm tra xem nút hiện tại có nằm trong vùng chuột không
            if (node != null &&
                location.X >= node.PositionX - Radius &&
                location.X <= node.PositionX + Radius &&
                location.Y >= node.PositionY &&
                location.Y <= node.PositionY + 2 * Radius)
            {
                return node;
            }

            // Nếu không, kiểm tra các nút con
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var foundNode = FindNodeAtLocation(child, location);
                    if (foundNode != null)
                        return foundNode;
                }
            }

            return null;
        }

        private void FPTreeForm_MouseDown(object sender, MouseEventArgs e)
        {
            Node root = new Node("");
            if (e.Button == MouseButtons.Left)
            {
                // Lặp qua các nút để kiểm tra xem có nút nào được nhấn không
                selectedNode = FindNodeAtLocation(root, e.Location);

                // Lưu lại vị trí chuột
                lastMousePosition = e.Location;
            }
        }

        private void FPTreeForm_MouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
        }
    }
}
