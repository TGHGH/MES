namespace Producing
{
    partial class ModuleClean
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
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbx_eqp = new System.Windows.Forms.ComboBox();
            this.tbx_module_id = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_workshop = new System.Windows.Forms.ComboBox();
            this.tbx_current_site = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_scan_count = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "组件编号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "设备机台";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(285, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "当前操作";
            // 
            // cbx_eqp
            // 
            this.cbx_eqp.AccessibleDescription = "";
            this.cbx_eqp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_eqp.FormattingEnabled = true;
            this.cbx_eqp.Location = new System.Drawing.Point(360, 76);
            this.cbx_eqp.Name = "cbx_eqp";
            this.cbx_eqp.Size = new System.Drawing.Size(139, 20);
            this.cbx_eqp.TabIndex = 1;
            this.cbx_eqp.TabStop = false;
            // 
            // tbx_module_id
            // 
            this.tbx_module_id.Location = new System.Drawing.Point(92, 76);
            this.tbx_module_id.Name = "tbx_module_id";
            this.tbx_module_id.Size = new System.Drawing.Size(135, 21);
            this.tbx_module_id.TabIndex = 2;
            this.tbx_module_id.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Cyan;
            this.textBox2.Location = new System.Drawing.Point(401, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(94, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "清洗";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_eqp);
            this.groupBox1.Controls.Add(this.cbx_workshop);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbx_current_site);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.tbx_module_id);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 119);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作业站信息";
            // 
            // cbx_workshop
            // 
            this.cbx_workshop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_workshop.FormattingEnabled = true;
            this.cbx_workshop.Location = new System.Drawing.Point(92, 33);
            this.cbx_workshop.Name = "cbx_workshop";
            this.cbx_workshop.Size = new System.Drawing.Size(134, 20);
            this.cbx_workshop.TabIndex = 1;
            // 
            // tbx_current_site
            // 
            this.tbx_current_site.BackColor = System.Drawing.Color.Cyan;
            this.tbx_current_site.Location = new System.Drawing.Point(359, 28);
            this.tbx_current_site.Name = "tbx_current_site";
            this.tbx_current_site.ReadOnly = true;
            this.tbx_current_site.Size = new System.Drawing.Size(36, 21);
            this.tbx_current_site.TabIndex = 2;
            this.tbx_current_site.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "车间";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_scan_count);
            this.groupBox3.Location = new System.Drawing.Point(334, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 145);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "过站总数";
            // 
            // btn_scan_count
            // 
            this.btn_scan_count.BackColor = System.Drawing.Color.White;
            this.btn_scan_count.Font = new System.Drawing.Font("SimSun", 50F);
            this.btn_scan_count.Location = new System.Drawing.Point(6, 20);
            this.btn_scan_count.Name = "btn_scan_count";
            this.btn_scan_count.Size = new System.Drawing.Size(178, 109);
            this.btn_scan_count.TabIndex = 0;
            this.btn_scan_count.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 6;
            this.button1.TabStop = false;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(445, 343);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(80, 30);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.TabStop = false;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(537, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(522, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabelRight
            // 
            this.toolStripStatusLabelRight.Font = new System.Drawing.Font("Microsoft YaHei", 18F);
            this.toolStripStatusLabelRight.Name = "toolStripStatusLabelRight";
            this.toolStripStatusLabelRight.Size = new System.Drawing.Size(0, 17);
            // 
            // ModuleClean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 558);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "ModuleClean";
            this.Text = "清洗站点";
            this.Load += new System.EventHandler(this.ModuleClean_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbx_eqp;
        private System.Windows.Forms.TextBox tbx_module_id;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox tbx_current_site;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRight;
        private System.Windows.Forms.ComboBox cbx_workshop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_scan_count;
    }
}