namespace ebookCutter
{
    partial class frmBatch
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
            this.txtCon = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCon
            // 
            this.txtCon.BackColor = System.Drawing.Color.Navy;
            this.txtCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCon.ForeColor = System.Drawing.Color.Yellow;
            this.txtCon.Location = new System.Drawing.Point(0, 0);
            this.txtCon.Multiline = true;
            this.txtCon.Name = "txtCon";
            this.txtCon.ReadOnly = true;
            this.txtCon.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCon.Size = new System.Drawing.Size(650, 403);
            this.txtCon.TabIndex = 0;
            // 
            // frmBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(650, 403);
            this.Controls.Add(this.txtCon);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBatch";
            this.Text = "Batch Processing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBatch_FormClosing);
            this.Shown += new System.EventHandler(this.frmBatch_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCon;
    }
}