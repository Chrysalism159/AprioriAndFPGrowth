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
            this.txtminConf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtminSup = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbData = new System.Windows.Forms.ComboBox();
            this.AprioriButton = new System.Windows.Forms.Button();
            this.FPGrowthButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbConf = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddData = new System.Windows.Forms.Button();
            this.rtbAddData = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtminConf
            // 
            this.txtminConf.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtminConf.Location = new System.Drawing.Point(245, 229);
            this.txtminConf.Name = "txtminConf";
            this.txtminConf.Size = new System.Drawing.Size(186, 29);
            this.txtminConf.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(196, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Độ hỗ trợ tối thiểu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Độ tin cậy tối thiểu";
            // 
            // txtminSup
            // 
            this.txtminSup.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtminSup.Location = new System.Drawing.Point(245, 299);
            this.txtminSup.Name = "txtminSup";
            this.txtminSup.Size = new System.Drawing.Size(186, 29);
            this.txtminSup.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(198, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lựa chọn database";
            // 
            // cbData
            // 
            this.cbData.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbData.FormattingEnabled = true;
            this.cbData.Location = new System.Drawing.Point(245, 152);
            this.cbData.Name = "cbData";
            this.cbData.Size = new System.Drawing.Size(186, 30);
            this.cbData.TabIndex = 5;
            this.cbData.SelectedIndexChanged += new System.EventHandler(this.cbData_SelectedIndexChanged);
            // 
            // AprioriButton
            // 
            this.AprioriButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AprioriButton.Location = new System.Drawing.Point(195, 350);
            this.AprioriButton.Name = "AprioriButton";
            this.AprioriButton.Size = new System.Drawing.Size(168, 51);
            this.AprioriButton.TabIndex = 6;
            this.AprioriButton.Text = "Thuật toán Apriori";
            this.AprioriButton.UseVisualStyleBackColor = true;
            this.AprioriButton.Click += new System.EventHandler(this.AprioriButton_Click);
            // 
            // FPGrowthButton
            // 
            this.FPGrowthButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPGrowthButton.Location = new System.Drawing.Point(381, 350);
            this.FPGrowthButton.Name = "FPGrowthButton";
            this.FPGrowthButton.Size = new System.Drawing.Size(188, 51);
            this.FPGrowthButton.TabIndex = 8;
            this.FPGrowthButton.Text = "Thuật toán FP Growth";
            this.FPGrowthButton.UseVisualStyleBackColor = true;
            this.FPGrowthButton.Click += new System.EventHandler(this.FPGrowthButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView1.Location = new System.Drawing.Point(623, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(625, 250);
            this.dataGridView1.TabIndex = 10;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(156, 566);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1026, 292);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // cbConf
            // 
            this.cbConf.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConf.FormattingEnabled = true;
            this.cbConf.Location = new System.Drawing.Point(245, 229);
            this.cbConf.Name = "cbConf";
            this.cbConf.Size = new System.Drawing.Size(186, 30);
            this.cbConf.TabIndex = 13;
            this.cbConf.SelectedIndexChanged += new System.EventHandler(this.cbConf_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.button1.Location = new System.Drawing.Point(464, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 42);
            this.button1.TabIndex = 14;
            this.button1.Text = "Nhập số khác";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1171, 38);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ứng dụng khai phá dữ liệu trong phân tích nhu cầu của mua hàng khách hàng ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(619, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 22);
            this.label5.TabIndex = 16;
            this.label5.Text = "Hiển thị dữ liệu";
            // 
            // btnAddData
            // 
            this.btnAddData.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddData.Location = new System.Drawing.Point(623, 429);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(231, 40);
            this.btnAddData.TabIndex = 17;
            this.btnAddData.Text = "+ Cập nhật thêm dữ liệu";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // rtbAddData
            // 
            this.rtbAddData.Location = new System.Drawing.Point(897, 429);
            this.rtbAddData.Name = "rtbAddData";
            this.rtbAddData.Size = new System.Drawing.Size(351, 40);
            this.rtbAddData.TabIndex = 18;
            this.rtbAddData.Text = "";
            this.rtbAddData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbAddData_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(152, 527);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 22);
            this.label6.TabIndex = 19;
            this.label6.Text = "Hiển thị dữ liệu";
            // 
            // btnDraw
            // 
            this.btnDraw.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDraw.Location = new System.Drawing.Point(195, 418);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(374, 51);
            this.btnDraw.TabIndex = 20;
            this.btnDraw.Text = "Vẽ cây FPTree";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 887);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtbAddData);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbConf);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.FPGrowthButton);
            this.Controls.Add(this.AprioriButton);
            this.Controls.Add(this.cbData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtminSup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtminConf);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtminConf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtminSup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbData;
        private System.Windows.Forms.Button AprioriButton;
        private System.Windows.Forms.Button FPGrowthButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cbConf;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.RichTextBox rtbAddData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDraw;
    }
}

