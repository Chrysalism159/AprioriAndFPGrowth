namespace Algorithm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gunaGradientPanel1 = new Guna.UI.WinForms.GunaGradientPanel();
            this.cbConf = new Guna.UI.WinForms.GunaComboBox();
            this.cbData = new Guna.UI.WinForms.GunaComboBox();
            this.txtminSup = new Guna.UI.WinForms.GunaTextBox();
            this.gunaGradientButton1 = new Guna.UI.WinForms.GunaGradientButton();
            this.AprioriButton = new Guna.UI.WinForms.GunaGradientButton();
            this.gunaGradientPanel2 = new Guna.UI.WinForms.GunaGradientPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gunaGradientPanel3 = new Guna.UI.WinForms.GunaGradientPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.gunaGradientPanel1.SuspendLayout();
            this.gunaGradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gunaGradientPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Độ hỗ trợ tối thiểu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Độ tin cậy tối thiểu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lựa chọn database";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(14, 61);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(545, 309);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(46, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 22);
            this.label5.TabIndex = 16;
            this.label5.Text = "Hiển thị dữ liệu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(275, 22);
            this.label6.TabIndex = 19;
            this.label6.Text = "Danh sách các tập thường xuyên";
            // 
            // gunaGradientPanel1
            // 
            this.gunaGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel1.BackgroundImage")));
            this.gunaGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel1.Controls.Add(this.cbConf);
            this.gunaGradientPanel1.Controls.Add(this.cbData);
            this.gunaGradientPanel1.Controls.Add(this.txtminSup);
            this.gunaGradientPanel1.Controls.Add(this.gunaGradientButton1);
            this.gunaGradientPanel1.Controls.Add(this.AprioriButton);
            this.gunaGradientPanel1.Controls.Add(this.label1);
            this.gunaGradientPanel1.Controls.Add(this.label2);
            this.gunaGradientPanel1.Controls.Add(this.label3);
            this.gunaGradientPanel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gunaGradientPanel1.GradientColor1 = System.Drawing.Color.IndianRed;
            this.gunaGradientPanel1.GradientColor2 = System.Drawing.Color.White;
            this.gunaGradientPanel1.GradientColor3 = System.Drawing.Color.White;
            this.gunaGradientPanel1.GradientColor4 = System.Drawing.Color.White;
            this.gunaGradientPanel1.Location = new System.Drawing.Point(0, 26);
            this.gunaGradientPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.gunaGradientPanel1.Name = "gunaGradientPanel1";
            this.gunaGradientPanel1.Size = new System.Drawing.Size(590, 349);
            this.gunaGradientPanel1.TabIndex = 22;
            this.gunaGradientPanel1.Text = "gunaGradientPanel1";
            // 
            // cbConf
            // 
            this.cbConf.BackColor = System.Drawing.Color.Transparent;
            this.cbConf.BaseColor = System.Drawing.Color.White;
            this.cbConf.BorderColor = System.Drawing.Color.Silver;
            this.cbConf.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbConf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConf.FocusedColor = System.Drawing.Color.Empty;
            this.cbConf.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbConf.ForeColor = System.Drawing.Color.Black;
            this.cbConf.FormattingEnabled = true;
            this.cbConf.Location = new System.Drawing.Point(222, 84);
            this.cbConf.Margin = new System.Windows.Forms.Padding(2);
            this.cbConf.Name = "cbConf";
            this.cbConf.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cbConf.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cbConf.Radius = 10;
            this.cbConf.Size = new System.Drawing.Size(323, 26);
            this.cbConf.TabIndex = 25;
            this.cbConf.SelectedIndexChanged += new System.EventHandler(this.cbConf_SelectedIndexChanged_1);
            // 
            // cbData
            // 
            this.cbData.BackColor = System.Drawing.Color.Transparent;
            this.cbData.BaseColor = System.Drawing.Color.White;
            this.cbData.BorderColor = System.Drawing.Color.Silver;
            this.cbData.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbData.FocusedColor = System.Drawing.Color.Empty;
            this.cbData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbData.ForeColor = System.Drawing.Color.Black;
            this.cbData.FormattingEnabled = true;
            this.cbData.Location = new System.Drawing.Point(222, 23);
            this.cbData.Margin = new System.Windows.Forms.Padding(2);
            this.cbData.Name = "cbData";
            this.cbData.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cbData.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cbData.Radius = 10;
            this.cbData.Size = new System.Drawing.Size(323, 26);
            this.cbData.TabIndex = 25;
            this.cbData.SelectedIndexChanged += new System.EventHandler(this.cbData_SelectedIndexChanged_1);
            // 
            // txtminSup
            // 
            this.txtminSup.BackColor = System.Drawing.Color.Transparent;
            this.txtminSup.BaseColor = System.Drawing.Color.White;
            this.txtminSup.BorderColor = System.Drawing.Color.Silver;
            this.txtminSup.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtminSup.FocusedBaseColor = System.Drawing.Color.White;
            this.txtminSup.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtminSup.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtminSup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtminSup.Location = new System.Drawing.Point(222, 141);
            this.txtminSup.Margin = new System.Windows.Forms.Padding(2);
            this.txtminSup.Name = "txtminSup";
            this.txtminSup.PasswordChar = '\0';
            this.txtminSup.Radius = 15;
            this.txtminSup.SelectedText = "";
            this.txtminSup.Size = new System.Drawing.Size(323, 33);
            this.txtminSup.TabIndex = 24;
            // 
            // gunaGradientButton1
            // 
            this.gunaGradientButton1.AnimationHoverSpeed = 0.07F;
            this.gunaGradientButton1.AnimationSpeed = 0.03F;
            this.gunaGradientButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradientButton1.BaseColor1 = System.Drawing.Color.Violet;
            this.gunaGradientButton1.BaseColor2 = System.Drawing.Color.LightSteelBlue;
            this.gunaGradientButton1.BorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaGradientButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaGradientButton1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gunaGradientButton1.ForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.Image = ((System.Drawing.Image)(resources.GetObject("gunaGradientButton1.Image")));
            this.gunaGradientButton1.ImageSize = new System.Drawing.Size(60, 60);
            this.gunaGradientButton1.Location = new System.Drawing.Point(303, 235);
            this.gunaGradientButton1.Margin = new System.Windows.Forms.Padding(2);
            this.gunaGradientButton1.Name = "gunaGradientButton1";
            this.gunaGradientButton1.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gunaGradientButton1.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gunaGradientButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.OnHoverImage = null;
            this.gunaGradientButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.Radius = 25;
            this.gunaGradientButton1.Size = new System.Drawing.Size(242, 59);
            this.gunaGradientButton1.TabIndex = 22;
            this.gunaGradientButton1.Text = "Thuật toán FP Growth";
            this.gunaGradientButton1.Click += new System.EventHandler(this.gunaGradientButton1_Click);
            // 
            // AprioriButton
            // 
            this.AprioriButton.AnimationHoverSpeed = 0.07F;
            this.AprioriButton.AnimationSpeed = 0.03F;
            this.AprioriButton.BackColor = System.Drawing.Color.Transparent;
            this.AprioriButton.BaseColor1 = System.Drawing.Color.Violet;
            this.AprioriButton.BaseColor2 = System.Drawing.Color.LightSteelBlue;
            this.AprioriButton.BorderColor = System.Drawing.Color.Black;
            this.AprioriButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.AprioriButton.FocusedColor = System.Drawing.Color.Empty;
            this.AprioriButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.AprioriButton.ForeColor = System.Drawing.Color.White;
            this.AprioriButton.Image = ((System.Drawing.Image)(resources.GetObject("AprioriButton.Image")));
            this.AprioriButton.ImageSize = new System.Drawing.Size(60, 60);
            this.AprioriButton.Location = new System.Drawing.Point(49, 235);
            this.AprioriButton.Margin = new System.Windows.Forms.Padding(2);
            this.AprioriButton.Name = "AprioriButton";
            this.AprioriButton.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.AprioriButton.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.AprioriButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.AprioriButton.OnHoverForeColor = System.Drawing.Color.White;
            this.AprioriButton.OnHoverImage = null;
            this.AprioriButton.OnPressedColor = System.Drawing.Color.Black;
            this.AprioriButton.Radius = 25;
            this.AprioriButton.Size = new System.Drawing.Size(215, 59);
            this.AprioriButton.TabIndex = 21;
            this.AprioriButton.Text = "Thuật toán Apriori";
            this.AprioriButton.Click += new System.EventHandler(this.AprioriButton_Click_1);
            // 
            // gunaGradientPanel2
            // 
            this.gunaGradientPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaGradientPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel2.BackgroundImage")));
            this.gunaGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel2.Controls.Add(this.dataGridView1);
            this.gunaGradientPanel2.Controls.Add(this.label5);
            this.gunaGradientPanel2.GradientColor1 = System.Drawing.Color.White;
            this.gunaGradientPanel2.GradientColor2 = System.Drawing.Color.White;
            this.gunaGradientPanel2.GradientColor3 = System.Drawing.Color.White;
            this.gunaGradientPanel2.GradientColor4 = System.Drawing.Color.IndianRed;
            this.gunaGradientPanel2.Location = new System.Drawing.Point(608, 26);
            this.gunaGradientPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.gunaGradientPanel2.Name = "gunaGradientPanel2";
            this.gunaGradientPanel2.Size = new System.Drawing.Size(568, 349);
            this.gunaGradientPanel2.TabIndex = 23;
            this.gunaGradientPanel2.Text = "gunaGradientPanel2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 62);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(490, 254);
            this.dataGridView1.TabIndex = 22;
            // 
            // gunaGradientPanel3
            // 
            this.gunaGradientPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunaGradientPanel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel3.BackgroundImage")));
            this.gunaGradientPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel3.Controls.Add(this.label4);
            this.gunaGradientPanel3.Controls.Add(this.richTextBox2);
            this.gunaGradientPanel3.Controls.Add(this.label6);
            this.gunaGradientPanel3.Controls.Add(this.richTextBox1);
            this.gunaGradientPanel3.GradientColor1 = System.Drawing.Color.White;
            this.gunaGradientPanel3.GradientColor2 = System.Drawing.Color.IndianRed;
            this.gunaGradientPanel3.GradientColor3 = System.Drawing.Color.White;
            this.gunaGradientPanel3.GradientColor4 = System.Drawing.Color.White;
            this.gunaGradientPanel3.Location = new System.Drawing.Point(0, 391);
            this.gunaGradientPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.gunaGradientPanel3.Name = "gunaGradientPanel3";
            this.gunaGradientPanel3.Size = new System.Drawing.Size(1163, 444);
            this.gunaGradientPanel3.TabIndex = 24;
            this.gunaGradientPanel3.Text = "gunaGradientPanel3";
            this.gunaGradientPanel3.Click += new System.EventHandler(this.gunaGradientPanel3_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.gunaGradientPanel1);
            this.panel1.Controls.Add(this.gunaGradientPanel3);
            this.panel1.Controls.Add(this.gunaGradientPanel2);
            this.panel1.Location = new System.Drawing.Point(2, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1163, 838);
            this.panel1.TabIndex = 20;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(604, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 22);
            this.label4.TabIndex = 21;
            this.label4.Text = "Hiển thị kết quả dự đoán";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(604, 61);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(545, 309);
            this.richTextBox2.TabIndex = 20;
            this.richTextBox2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1174, 857);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Phân tích dữ liệu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gunaGradientPanel1.ResumeLayout(false);
            this.gunaGradientPanel1.PerformLayout();
            this.gunaGradientPanel2.ResumeLayout(false);
            this.gunaGradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gunaGradientPanel3.ResumeLayout(false);
            this.gunaGradientPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel1;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel2;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel3;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI.WinForms.GunaGradientButton AprioriButton;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton1;
        private Guna.UI.WinForms.GunaComboBox cbConf;
        private Guna.UI.WinForms.GunaComboBox cbData;
        private Guna.UI.WinForms.GunaTextBox txtminSup;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}

