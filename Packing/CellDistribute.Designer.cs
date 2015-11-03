namespace Producing
{
    partial class CellDistribute
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.cbx_workshop = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_cell_info = new System.Windows.Forms.GroupBox();
            this.cbx_cell_color = new System.Windows.Forms.ComboBox();
            this.tbx_eff_up = new System.Windows.Forms.TextBox();
            this.cbx_cell_comp = new System.Windows.Forms.ComboBox();
            this.tbx_eff_low = new System.Windows.Forms.TextBox();
            this.tbx_cell_power = new System.Windows.Forms.TextBox();
            this.tbx_cell_grade = new System.Windows.Forms.TextBox();
            this.tbx_cellbatch_count = new System.Windows.Forms.TextBox();
            this.tbx_cell_erplot = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbx_obatch_count = new System.Windows.Forms.TextBox();
            this.tb_cell_obatch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox_cell_info.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 37);
            this.button1.TabIndex = 6;
            this.button1.TabStop = false;
            this.button1.Text = "原物料批号报废";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(360, 407);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 37);
            this.button2.TabIndex = 11;
            this.button2.TabStop = false;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(464, 407);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(64, 37);
            this.btn_ok.TabIndex = 12;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(544, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(498, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabelRight
            // 
            this.toolStripStatusLabelRight.Font = new System.Drawing.Font("Microsoft YaHei", 18F);
            this.toolStripStatusLabelRight.Name = "toolStripStatusLabelRight";
            this.toolStripStatusLabelRight.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.cbx_workshop);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作业站信息";
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.Cyan;
            this.textBox10.Location = new System.Drawing.Point(350, 26);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(137, 21);
            this.textBox10.TabIndex = 2;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "创建电池片批号";
            // 
            // cbx_workshop
            // 
            this.cbx_workshop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_workshop.FormattingEnabled = true;
            this.cbx_workshop.Location = new System.Drawing.Point(73, 26);
            this.cbx_workshop.Name = "cbx_workshop";
            this.cbx_workshop.Size = new System.Drawing.Size(140, 20);
            this.cbx_workshop.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "当前操作";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车间编码";
            // 
            // groupBox_cell_info
            // 
            this.groupBox_cell_info.Controls.Add(this.cbx_cell_color);
            this.groupBox_cell_info.Controls.Add(this.tbx_eff_up);
            this.groupBox_cell_info.Controls.Add(this.cbx_cell_comp);
            this.groupBox_cell_info.Controls.Add(this.tbx_eff_low);
            this.groupBox_cell_info.Controls.Add(this.tbx_cell_power);
            this.groupBox_cell_info.Controls.Add(this.tbx_cell_grade);
            this.groupBox_cell_info.Controls.Add(this.tbx_cellbatch_count);
            this.groupBox_cell_info.Controls.Add(this.tbx_cell_erplot);
            this.groupBox_cell_info.Controls.Add(this.label13);
            this.groupBox_cell_info.Controls.Add(this.label8);
            this.groupBox_cell_info.Controls.Add(this.label7);
            this.groupBox_cell_info.Controls.Add(this.label6);
            this.groupBox_cell_info.Controls.Add(this.label12);
            this.groupBox_cell_info.Controls.Add(this.label11);
            this.groupBox_cell_info.Controls.Add(this.label5);
            this.groupBox_cell_info.Controls.Add(this.label4);
            this.groupBox_cell_info.Controls.Add(this.label3);
            this.groupBox_cell_info.Location = new System.Drawing.Point(12, 97);
            this.groupBox_cell_info.Name = "groupBox_cell_info";
            this.groupBox_cell_info.Size = new System.Drawing.Size(518, 194);
            this.groupBox_cell_info.TabIndex = 4;
            this.groupBox_cell_info.TabStop = false;
            this.groupBox_cell_info.Text = "新物料信息";
            // 
            // cbx_cell_color
            // 
            this.cbx_cell_color.FormattingEnabled = true;
            this.cbx_cell_color.Location = new System.Drawing.Point(77, 72);
            this.cbx_cell_color.Name = "cbx_cell_color";
            this.cbx_cell_color.Size = new System.Drawing.Size(133, 20);
            this.cbx_cell_color.TabIndex = 3;
            // 
            // tbx_eff_up
            // 
            this.tbx_eff_up.Location = new System.Drawing.Point(149, 111);
            this.tbx_eff_up.Name = "tbx_eff_up";
            this.tbx_eff_up.Size = new System.Drawing.Size(47, 21);
            this.tbx_eff_up.TabIndex = 5;
            // 
            // cbx_cell_comp
            // 
            this.cbx_cell_comp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_cell_comp.FormattingEnabled = true;
            this.cbx_cell_comp.Location = new System.Drawing.Point(71, 154);
            this.cbx_cell_comp.Name = "cbx_cell_comp";
            this.cbx_cell_comp.Size = new System.Drawing.Size(140, 20);
            this.cbx_cell_comp.TabIndex = 6;
            // 
            // tbx_eff_low
            // 
            this.tbx_eff_low.Location = new System.Drawing.Point(75, 111);
            this.tbx_eff_low.Name = "tbx_eff_low";
            this.tbx_eff_low.Size = new System.Drawing.Size(47, 21);
            this.tbx_eff_low.TabIndex = 4;
            // 
            // tbx_cell_power
            // 
            this.tbx_cell_power.Location = new System.Drawing.Point(350, 26);
            this.tbx_cell_power.Name = "tbx_cell_power";
            this.tbx_cell_power.Size = new System.Drawing.Size(137, 21);
            this.tbx_cell_power.TabIndex = 7;
            // 
            // tbx_cell_grade
            // 
            this.tbx_cell_grade.Location = new System.Drawing.Point(350, 68);
            this.tbx_cell_grade.Name = "tbx_cell_grade";
            this.tbx_cell_grade.Size = new System.Drawing.Size(137, 21);
            this.tbx_cell_grade.TabIndex = 8;
            // 
            // tbx_cellbatch_count
            // 
            this.tbx_cellbatch_count.Location = new System.Drawing.Point(350, 111);
            this.tbx_cellbatch_count.Name = "tbx_cellbatch_count";
            this.tbx_cellbatch_count.Size = new System.Drawing.Size(137, 21);
            this.tbx_cellbatch_count.TabIndex = 9;
            // 
            // tbx_cell_erplot
            // 
            this.tbx_cell_erplot.Location = new System.Drawing.Point(75, 26);
            this.tbx_cell_erplot.Name = "tbx_cell_erplot";
            this.tbx_cell_erplot.Size = new System.Drawing.Size(137, 21);
            this.tbx_cell_erplot.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "电池厂商";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(276, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "片源数量";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(276, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "片源等级";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(276, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "单片功率";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(199, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(128, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "%-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "片源效率";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "片源颜色";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "片源批号";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbx_obatch_count);
            this.groupBox3.Controls.Add(this.tb_cell_obatch);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(13, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 77);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "原物料批号";
            // 
            // tbx_obatch_count
            // 
            this.tbx_obatch_count.Location = new System.Drawing.Point(349, 32);
            this.tbx_obatch_count.Name = "tbx_obatch_count";
            this.tbx_obatch_count.Size = new System.Drawing.Size(137, 21);
            this.tbx_obatch_count.TabIndex = 10;
            // 
            // tb_cell_obatch
            // 
            this.tb_cell_obatch.Location = new System.Drawing.Point(75, 32);
            this.tb_cell_obatch.Name = "tb_cell_obatch";
            this.tb_cell_obatch.Size = new System.Drawing.Size(137, 21);
            this.tb_cell_obatch.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "片源数量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "物料批号";
            // 
            // CellDistribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 561);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox_cell_info);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "CellDistribute";
            this.Text = "创建电池片批号";
            this.Load += new System.EventHandler(this.CellDistribute_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_cell_info.ResumeLayout(false);
            this.groupBox_cell_info.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.ComboBox cbx_workshop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_cell_info;
        private System.Windows.Forms.TextBox tbx_eff_up;
        private System.Windows.Forms.TextBox tbx_eff_low;
        private System.Windows.Forms.TextBox tbx_cell_power;
        private System.Windows.Forms.TextBox tbx_cell_grade;
        private System.Windows.Forms.TextBox tbx_cellbatch_count;
        private System.Windows.Forms.TextBox tbx_cell_erplot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbx_obatch_count;
        private System.Windows.Forms.TextBox tb_cell_obatch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbx_cell_comp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbx_cell_color;
    }
}