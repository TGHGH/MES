namespace MaterialPlan
{
    partial class CellReturn
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_workshop = new System.Windows.Forms.ComboBox();
            this.tbx_module = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_cellreturn_detail = new System.Windows.Forms.DataGridView();
            this.btn_scanCount = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cellreturn_detail)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车间编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "组件编码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前操作";
            // 
            // cbx_workshop
            // 
            this.cbx_workshop.FormattingEnabled = true;
            this.cbx_workshop.Location = new System.Drawing.Point(73, 25);
            this.cbx_workshop.Name = "cbx_workshop";
            this.cbx_workshop.Size = new System.Drawing.Size(121, 20);
            this.cbx_workshop.TabIndex = 2;
            this.cbx_workshop.TabStop = false;
            // 
            // tbx_module
            // 
            this.tbx_module.Location = new System.Drawing.Point(73, 72);
            this.tbx_module.Name = "tbx_module";
            this.tbx_module.Size = new System.Drawing.Size(121, 21);
            this.tbx_module.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Cyan;
            this.textBox2.Location = new System.Drawing.Point(395, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(33, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "CTK";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Cyan;
            this.textBox3.Location = new System.Drawing.Point(431, 25);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(112, 21);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "片源退库";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.tbx_module);
            this.groupBox1.Controls.Add(this.cbx_workshop);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 126);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作业站信息";
            // 
            // dgv_cellreturn_detail
            // 
            this.dgv_cellreturn_detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cellreturn_detail.Location = new System.Drawing.Point(14, 25);
            this.dgv_cellreturn_detail.Name = "dgv_cellreturn_detail";
            this.dgv_cellreturn_detail.RowTemplate.Height = 23;
            this.dgv_cellreturn_detail.Size = new System.Drawing.Size(361, 150);
            this.dgv_cellreturn_detail.TabIndex = 4;
            this.dgv_cellreturn_detail.TabStop = false;
            // 
            // btn_scanCount
            // 
            this.btn_scanCount.BackColor = System.Drawing.Color.White;
            this.btn_scanCount.Font = new System.Drawing.Font("SimSun", 50F);
            this.btn_scanCount.Location = new System.Drawing.Point(447, 192);
            this.btn_scanCount.Name = "btn_scanCount";
            this.btn_scanCount.Size = new System.Drawing.Size(117, 99);
            this.btn_scanCount.TabIndex = 5;
            this.btn_scanCount.TabStop = false;
            this.btn_scanCount.UseVisualStyleBackColor = false;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(490, 351);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(74, 25);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.TabStop = false;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_cellreturn_detail);
            this.groupBox2.Location = new System.Drawing.Point(16, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 196);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "退库明细";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(538, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabelRight
            // 
            this.toolStripStatusLabelRight.Font = new System.Drawing.Font("Microsoft YaHei", 18F);
            this.toolStripStatusLabelRight.Name = "toolStripStatusLabelRight";
            this.toolStripStatusLabelRight.Size = new System.Drawing.Size(0, 17);
            // 
            // CellReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_scanCount);
            this.Controls.Add(this.groupBox1);
            this.Name = "CellReturn";
            this.Text = "片源退库";
            this.Load += new System.EventHandler(this.CellReturn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cellreturn_detail)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_workshop;
        private System.Windows.Forms.TextBox tbx_module;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_cellreturn_detail;
        private System.Windows.Forms.Button btn_scanCount;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRight;
    }
}