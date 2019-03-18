namespace DocConverter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_convert = new System.Windows.Forms.Button();
            this.btn_savepath = new System.Windows.Forms.Button();
            this.txt_filepath = new System.Windows.Forms.TextBox();
            this.txt_savepath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel_pager = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.uud_startpage = new System.Windows.Forms.NumericUpDown();
            this.uud_endpage = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_filepath = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel_pager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uud_startpage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uud_endpage)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_convert
            // 
            this.btn_convert.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_convert.Location = new System.Drawing.Point(344, 12);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(84, 62);
            this.btn_convert.TabIndex = 0;
            this.btn_convert.Text = "转换";
            this.btn_convert.UseVisualStyleBackColor = true;
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // btn_savepath
            // 
            this.btn_savepath.Location = new System.Drawing.Point(246, 51);
            this.btn_savepath.Name = "btn_savepath";
            this.btn_savepath.Size = new System.Drawing.Size(75, 23);
            this.btn_savepath.TabIndex = 0;
            this.btn_savepath.Text = "保存目录";
            this.btn_savepath.UseVisualStyleBackColor = true;
            this.btn_savepath.Click += new System.EventHandler(this.btn_savepath_Click);
            // 
            // txt_filepath
            // 
            this.txt_filepath.Enabled = false;
            this.txt_filepath.Location = new System.Drawing.Point(18, 12);
            this.txt_filepath.Name = "txt_filepath";
            this.txt_filepath.Size = new System.Drawing.Size(215, 21);
            this.txt_filepath.TabIndex = 1;
            // 
            // txt_savepath
            // 
            this.txt_savepath.Enabled = false;
            this.txt_savepath.Location = new System.Drawing.Point(18, 53);
            this.txt_savepath.Name = "txt_savepath";
            this.txt_savepath.Size = new System.Drawing.Size(215, 21);
            this.txt_savepath.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.panel_pager);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_savepath);
            this.panel1.Controls.Add(this.btn_filepath);
            this.panel1.Controls.Add(this.btn_convert);
            this.panel1.Controls.Add(this.txt_filepath);
            this.panel1.Controls.Add(this.btn_savepath);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 145);
            this.panel1.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 131);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 10);
            this.progressBar1.TabIndex = 7;
            // 
            // panel_pager
            // 
            this.panel_pager.Controls.Add(this.label2);
            this.panel_pager.Controls.Add(this.uud_startpage);
            this.panel_pager.Controls.Add(this.uud_endpage);
            this.panel_pager.Controls.Add(this.label1);
            this.panel_pager.Location = new System.Drawing.Point(192, 80);
            this.panel_pager.Name = "panel_pager";
            this.panel_pager.Size = new System.Drawing.Size(252, 45);
            this.panel_pager.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "开始页：";
            // 
            // uud_startpage
            // 
            this.uud_startpage.Location = new System.Drawing.Point(72, 10);
            this.uud_startpage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uud_startpage.Name = "uud_startpage";
            this.uud_startpage.Size = new System.Drawing.Size(49, 21);
            this.uud_startpage.TabIndex = 2;
            this.uud_startpage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uud_startpage.ValueChanged += new System.EventHandler(this.uud_startpage_ValueChanged);
            // 
            // uud_endpage
            // 
            this.uud_endpage.Location = new System.Drawing.Point(187, 10);
            this.uud_endpage.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.uud_endpage.Name = "uud_endpage";
            this.uud_endpage.Size = new System.Drawing.Size(49, 21);
            this.uud_endpage.TabIndex = 2;
            this.uud_endpage.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.uud_endpage.ValueChanged += new System.EventHandler(this.uud_endpage_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "结束页：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部转换",
            "自定义页数"});
            this.comboBox1.Location = new System.Drawing.Point(82, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "全部转换";
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "转换模式：";
            // 
            // btn_filepath
            // 
            this.btn_filepath.Location = new System.Drawing.Point(246, 12);
            this.btn_filepath.Name = "btn_filepath";
            this.btn_filepath.Size = new System.Drawing.Size(75, 23);
            this.btn_filepath.TabIndex = 0;
            this.btn_filepath.Text = "选择文件";
            this.btn_filepath.UseVisualStyleBackColor = true;
            this.btn_filepath.Click += new System.EventHandler(this.btn_filepath_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 169);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档（pdf, word, excel）图片转换器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_pager.ResumeLayout(false);
            this.panel_pager.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uud_startpage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uud_endpage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_convert;
        private System.Windows.Forms.Button btn_savepath;
        private System.Windows.Forms.TextBox txt_filepath;
        private System.Windows.Forms.TextBox txt_savepath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_filepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown uud_endpage;
        private System.Windows.Forms.NumericUpDown uud_startpage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_pager;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

