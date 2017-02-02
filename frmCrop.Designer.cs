namespace ebookCutter
{
    partial class frmCrop
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
            this.picPanel = new System.Windows.Forms.Panel();
            this.picFrm = new System.Windows.Forms.PictureBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.picPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFrm)).BeginInit();
            this.SuspendLayout();
            // 
            // picPanel
            // 
            this.picPanel.AutoScroll = true;
            this.picPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPanel.Controls.Add(this.picFrm);
            this.picPanel.Location = new System.Drawing.Point(2, 55);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(788, 436);
            this.picPanel.TabIndex = 2;
            // 
            // picFrm
            // 
            this.picFrm.ErrorImage = null;
            this.picFrm.InitialImage = null;
            this.picFrm.Location = new System.Drawing.Point(-2, -2);
            this.picFrm.Name = "picFrm";
            this.picFrm.Size = new System.Drawing.Size(269, 281);
            this.picFrm.TabIndex = 0;
            this.picFrm.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(85, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select Images";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmCrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 494);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.picPanel);
            this.Name = "frmCrop";
            this.Text = "Crop Images";
            this.Load += new System.EventHandler(this.frmCrop_Load);
            this.Resize += new System.EventHandler(this.frmCrop_Resize);
            this.picPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFrm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.PictureBox picFrm;
        private System.Windows.Forms.Button btnSelect;
    }
}