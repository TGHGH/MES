namespace MES
{
    partial class MESShell
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
            this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripUserLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainStatusStrip
            // 
            this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripUserLabel});
            this._mainStatusStrip.Location = new System.Drawing.Point(0, 540);
            this._mainStatusStrip.Name = "_mainStatusStrip";
            this._mainStatusStrip.Size = new System.Drawing.Size(784, 22);
            this._mainStatusStrip.TabIndex = 2;
            this._mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel1.Text = "当前作业员：";
            // 
            // toolStripUserLabel
            // 
            this.toolStripUserLabel.Name = "toolStripUserLabel";
            this.toolStripUserLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MESShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._mainStatusStrip);
            this.IsMdiContainer = true;
            this.Name = "MESShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSMES";
            this.Load += new System.EventHandler(this.MESShell_Load);
            this._mainStatusStrip.ResumeLayout(false);
            this._mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripUserLabel;
    }
}

