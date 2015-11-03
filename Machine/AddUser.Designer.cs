namespace SysConfig
{
    partial class AddUser
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
            this.comboBox_workshop = new System.Windows.Forms.ComboBox();
            this.comboBox_dept = new System.Windows.Forms.ComboBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox_userID = new System.Windows.Forms.TextBox();
            this.textBox_remark = new System.Windows.Forms.TextBox();
            this.groupBox_addUser = new System.Windows.Forms.GroupBox();
            this.btn_query = new System.Windows.Forms.Button();
            this.button_del = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_insert = new System.Windows.Forms.Button();
            this.groupBox_addUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车间编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "所属部门";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "人员姓名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "当前操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "工号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "备注";
            // 
            // comboBox_workshop
            // 
            this.comboBox_workshop.FormattingEnabled = true;
            this.comboBox_workshop.Location = new System.Drawing.Point(76, 21);
            this.comboBox_workshop.Name = "comboBox_workshop";
            this.comboBox_workshop.Size = new System.Drawing.Size(182, 20);
            this.comboBox_workshop.TabIndex = 1;
            // 
            // comboBox_dept
            // 
            this.comboBox_dept.FormattingEnabled = true;
            this.comboBox_dept.Location = new System.Drawing.Point(76, 71);
            this.comboBox_dept.Name = "comboBox_dept";
            this.comboBox_dept.Size = new System.Drawing.Size(182, 20);
            this.comboBox_dept.TabIndex = 1;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(76, 120);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(182, 21);
            this.textBox_username.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Cyan;
            this.textBox1.Location = new System.Drawing.Point(338, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(186, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "增加用户";
            // 
            // textBox_userID
            // 
            this.textBox_userID.Location = new System.Drawing.Point(338, 71);
            this.textBox_userID.Name = "textBox_userID";
            this.textBox_userID.Size = new System.Drawing.Size(186, 21);
            this.textBox_userID.TabIndex = 2;
            // 
            // textBox_remark
            // 
            this.textBox_remark.Location = new System.Drawing.Point(338, 114);
            this.textBox_remark.Name = "textBox_remark";
            this.textBox_remark.Size = new System.Drawing.Size(186, 21);
            this.textBox_remark.TabIndex = 2;
            // 
            // groupBox_addUser
            // 
            this.groupBox_addUser.Controls.Add(this.textBox1);
            this.groupBox_addUser.Controls.Add(this.textBox_remark);
            this.groupBox_addUser.Controls.Add(this.textBox_userID);
            this.groupBox_addUser.Controls.Add(this.textBox_username);
            this.groupBox_addUser.Controls.Add(this.comboBox_dept);
            this.groupBox_addUser.Controls.Add(this.comboBox_workshop);
            this.groupBox_addUser.Controls.Add(this.label6);
            this.groupBox_addUser.Controls.Add(this.label5);
            this.groupBox_addUser.Controls.Add(this.label4);
            this.groupBox_addUser.Controls.Add(this.label3);
            this.groupBox_addUser.Controls.Add(this.label2);
            this.groupBox_addUser.Controls.Add(this.label1);
            this.groupBox_addUser.Location = new System.Drawing.Point(12, 18);
            this.groupBox_addUser.Name = "groupBox_addUser";
            this.groupBox_addUser.Size = new System.Drawing.Size(530, 159);
            this.groupBox_addUser.TabIndex = 3;
            this.groupBox_addUser.TabStop = false;
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(36, 214);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(76, 33);
            this.btn_query.TabIndex = 4;
            this.btn_query.TabStop = false;
            this.btn_query.Text = "信息查询";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // button_del
            // 
            this.button_del.Location = new System.Drawing.Point(183, 214);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(76, 33);
            this.button_del.TabIndex = 4;
            this.button_del.TabStop = false;
            this.button_del.Text = "删除用户";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(327, 214);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(76, 33);
            this.button_reset.TabIndex = 4;
            this.button_reset.TabStop = false;
            this.button_reset.Text = "取消";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_insert
            // 
            this.button_insert.Location = new System.Drawing.Point(460, 214);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(76, 33);
            this.button_insert.TabIndex = 4;
            this.button_insert.TabStop = false;
            this.button_insert.Text = "存储";
            this.button_insert.UseVisualStyleBackColor = true;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 352);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.btn_query);
            this.Controls.Add(this.groupBox_addUser);
            this.Name = "AddUser";
            this.Text = "增加用户";
            this.Load += new System.EventHandler(this.AddUser_Load);
            this.groupBox_addUser.ResumeLayout(false);
            this.groupBox_addUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_workshop;
        private System.Windows.Forms.ComboBox comboBox_dept;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox_userID;
        private System.Windows.Forms.TextBox textBox_remark;
        private System.Windows.Forms.GroupBox groupBox_addUser;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button button_insert;
    }
}