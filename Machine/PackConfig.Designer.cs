namespace SysConfig
{
    partial class PackConfig
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
            this.cbx_workshop = new System.Windows.Forms.ComboBox();
            this.cbx_wo = new System.Windows.Forms.ComboBox();
            this.cbx_pack_count = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbx_sale_order = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.dgv_power_set = new System.Windows.Forms.DataGridView();
            this.dgv_imp_set = new System.Windows.Forms.DataGridView();
            this.cbx_template = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_power_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_imp_set)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车间代码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "工单编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "拖数数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(486, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "当前操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(486, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "订单编号";
            // 
            // cbx_workshop
            // 
            this.cbx_workshop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_workshop.FormattingEnabled = true;
            this.cbx_workshop.Location = new System.Drawing.Point(83, 23);
            this.cbx_workshop.Name = "cbx_workshop";
            this.cbx_workshop.Size = new System.Drawing.Size(140, 20);
            this.cbx_workshop.TabIndex = 1;
            // 
            // cbx_wo
            // 
            this.cbx_wo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_wo.FormattingEnabled = true;
            this.cbx_wo.Location = new System.Drawing.Point(83, 70);
            this.cbx_wo.Name = "cbx_wo";
            this.cbx_wo.Size = new System.Drawing.Size(140, 20);
            this.cbx_wo.TabIndex = 2;
            // 
            // cbx_pack_count
            // 
            this.cbx_pack_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_pack_count.FormattingEnabled = true;
            this.cbx_pack_count.Location = new System.Drawing.Point(83, 117);
            this.cbx_pack_count.Name = "cbx_pack_count";
            this.cbx_pack_count.Size = new System.Drawing.Size(140, 20);
            this.cbx_pack_count.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(559, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(140, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "包装规则设定";
            // 
            // tbx_sale_order
            // 
            this.tbx_sale_order.Location = new System.Drawing.Point(559, 70);
            this.tbx_sale_order.Name = "tbx_sale_order";
            this.tbx_sale_order.Size = new System.Drawing.Size(140, 21);
            this.tbx_sale_order.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbx_sale_order);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.cbx_pack_count);
            this.groupBox1.Controls.Add(this.cbx_wo);
            this.groupBox1.Controls.Add(this.cbx_workshop);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 151);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作业站信息";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(639, 410);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(77, 36);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.TabStop = false;
            this.btn_ok.Text = "存储";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(556, 410);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(77, 36);
            this.btn_del.TabIndex = 4;
            this.btn_del.TabStop = false;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            // 
            // dgv_power_set
            // 
            this.dgv_power_set.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_power_set.Location = new System.Drawing.Point(25, 214);
            this.dgv_power_set.Name = "dgv_power_set";
            this.dgv_power_set.RowTemplate.Height = 23;
            this.dgv_power_set.Size = new System.Drawing.Size(291, 170);
            this.dgv_power_set.TabIndex = 5;
            this.dgv_power_set.TabStop = false;
            // 
            // dgv_imp_set
            // 
            this.dgv_imp_set.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_imp_set.Location = new System.Drawing.Point(342, 214);
            this.dgv_imp_set.Name = "dgv_imp_set";
            this.dgv_imp_set.RowTemplate.Height = 23;
            this.dgv_imp_set.Size = new System.Drawing.Size(374, 170);
            this.dgv_imp_set.TabIndex = 5;
            this.dgv_imp_set.TabStop = false;
            // 
            // cbx_template
            // 
            this.cbx_template.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_template.FormattingEnabled = true;
            this.cbx_template.Location = new System.Drawing.Point(100, 188);
            this.cbx_template.Name = "cbx_template";
            this.cbx_template.Size = new System.Drawing.Size(140, 20);
            this.cbx_template.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "选择模板";
            // 
            // PackConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 464);
            this.Controls.Add(this.cbx_template);
            this.Controls.Add(this.dgv_imp_set);
            this.Controls.Add(this.dgv_power_set);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Name = "PackConfig";
            this.Text = "包装设定";
            this.Load += new System.EventHandler(this.PackConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_power_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_imp_set)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbx_workshop;
        private System.Windows.Forms.ComboBox cbx_wo;
        private System.Windows.Forms.ComboBox cbx_pack_count;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbx_sale_order;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.DataGridView dgv_power_set;
        private System.Windows.Forms.DataGridView dgv_imp_set;
        private System.Windows.Forms.ComboBox cbx_template;
        private System.Windows.Forms.Label label6;
    }
}