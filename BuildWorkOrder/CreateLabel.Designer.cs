namespace MaterialPlan
{
    partial class CreateLabel
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbx_workshop = new System.Windows.Forms.ComboBox();
            this.cbx_workorder = new System.Windows.Forms.ComboBox();
            this.tbx_count_remain = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbx_plan_count = new System.Windows.Forms.TextBox();
            this.tbx_wip_count = new System.Windows.Forms.TextBox();
            this.groupBox_woInfo = new System.Windows.Forms.GroupBox();
            this.btn_preview = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbx_label_count = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_module_id = new System.Windows.Forms.DataGridView();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox_woInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_module_id)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车间编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "工单编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "未下线量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "当前操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "投产总量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(288, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "下线数量";
            // 
            // cbx_workshop
            // 
            this.cbx_workshop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_workshop.FormattingEnabled = true;
            this.cbx_workshop.Location = new System.Drawing.Point(102, 32);
            this.cbx_workshop.Name = "cbx_workshop";
            this.cbx_workshop.Size = new System.Drawing.Size(115, 20);
            this.cbx_workshop.TabIndex = 1;
            // 
            // cbx_workorder
            // 
            this.cbx_workorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_workorder.FormattingEnabled = true;
            this.cbx_workorder.Location = new System.Drawing.Point(102, 73);
            this.cbx_workorder.Name = "cbx_workorder";
            this.cbx_workorder.Size = new System.Drawing.Size(115, 20);
            this.cbx_workorder.TabIndex = 2;
            // 
            // tbx_count_remain
            // 
            this.tbx_count_remain.Location = new System.Drawing.Point(102, 111);
            this.tbx_count_remain.Name = "tbx_count_remain";
            this.tbx_count_remain.ReadOnly = true;
            this.tbx_count_remain.Size = new System.Drawing.Size(115, 21);
            this.tbx_count_remain.TabIndex = 3;
            this.tbx_count_remain.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Cyan;
            this.textBox2.Location = new System.Drawing.Point(356, 31);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(115, 21);
            this.textBox2.TabIndex = 4;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "生码";
            // 
            // tbx_plan_count
            // 
            this.tbx_plan_count.Location = new System.Drawing.Point(356, 66);
            this.tbx_plan_count.Name = "tbx_plan_count";
            this.tbx_plan_count.ReadOnly = true;
            this.tbx_plan_count.Size = new System.Drawing.Size(115, 21);
            this.tbx_plan_count.TabIndex = 5;
            this.tbx_plan_count.TabStop = false;
            // 
            // tbx_wip_count
            // 
            this.tbx_wip_count.Location = new System.Drawing.Point(356, 110);
            this.tbx_wip_count.Name = "tbx_wip_count";
            this.tbx_wip_count.ReadOnly = true;
            this.tbx_wip_count.Size = new System.Drawing.Size(115, 21);
            this.tbx_wip_count.TabIndex = 6;
            this.tbx_wip_count.TabStop = false;
            // 
            // groupBox_woInfo
            // 
            this.groupBox_woInfo.Controls.Add(this.tbx_wip_count);
            this.groupBox_woInfo.Controls.Add(this.tbx_plan_count);
            this.groupBox_woInfo.Controls.Add(this.textBox2);
            this.groupBox_woInfo.Controls.Add(this.tbx_count_remain);
            this.groupBox_woInfo.Controls.Add(this.cbx_workorder);
            this.groupBox_woInfo.Controls.Add(this.cbx_workshop);
            this.groupBox_woInfo.Controls.Add(this.label6);
            this.groupBox_woInfo.Controls.Add(this.label5);
            this.groupBox_woInfo.Controls.Add(this.label4);
            this.groupBox_woInfo.Controls.Add(this.label3);
            this.groupBox_woInfo.Controls.Add(this.label2);
            this.groupBox_woInfo.Controls.Add(this.label1);
            this.groupBox_woInfo.Location = new System.Drawing.Point(18, 16);
            this.groupBox_woInfo.Name = "groupBox_woInfo";
            this.groupBox_woInfo.Size = new System.Drawing.Size(505, 154);
            this.groupBox_woInfo.TabIndex = 3;
            this.groupBox_woInfo.TabStop = false;
            this.groupBox_woInfo.Text = "作业站信息";
            // 
            // btn_preview
            // 
            this.btn_preview.Location = new System.Drawing.Point(16, 49);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(62, 26);
            this.btn_preview.TabIndex = 4;
            this.btn_preview.TabStop = false;
            this.btn_preview.Text = "展开";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "生码总数";
            // 
            // tbx_label_count
            // 
            this.tbx_label_count.Location = new System.Drawing.Point(174, 53);
            this.tbx_label_count.Name = "tbx_label_count";
            this.tbx_label_count.Size = new System.Drawing.Size(58, 21);
            this.tbx_label_count.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_module_id);
            this.groupBox2.Controls.Add(this.btn_preview);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbx_label_count);
            this.groupBox2.Location = new System.Drawing.Point(23, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 281);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生码序列号明细";
            // 
            // dgv_module_id
            // 
            this.dgv_module_id.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_module_id.Location = new System.Drawing.Point(16, 99);
            this.dgv_module_id.Name = "dgv_module_id";
            this.dgv_module_id.RowTemplate.Height = 23;
            this.dgv_module_id.Size = new System.Drawing.Size(216, 152);
            this.dgv_module_id.TabIndex = 7;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(354, 418);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 36);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.TabStop = false;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(448, 418);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 36);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.TabStop = false;
            this.btn_ok.Text = "确定(打印)";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // CreateLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 479);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox_woInfo);
            this.Name = "CreateLabel";
            this.Text = "生码";
            this.Load += new System.EventHandler(this.CreateLabel_Load);
            this.groupBox_woInfo.ResumeLayout(false);
            this.groupBox_woInfo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_module_id)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbx_workshop;
        private System.Windows.Forms.ComboBox cbx_workorder;
        private System.Windows.Forms.TextBox tbx_count_remain;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbx_plan_count;
        private System.Windows.Forms.TextBox tbx_wip_count;
        private System.Windows.Forms.GroupBox groupBox_woInfo;
        private System.Windows.Forms.Button btn_preview;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbx_label_count;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.DataGridView dgv_module_id;
    }
}