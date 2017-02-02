namespace ebookCutter
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.picFrm = new System.Windows.Forms.PictureBox();
            this.picPanel = new System.Windows.Forms.Panel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMultiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markBreaksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.make4x4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.print144ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.print300ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prinImagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picFrm)).BeginInit();
            this.picPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
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
            // picPanel
            // 
            this.picPanel.AutoScroll = true;
            this.picPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPanel.Controls.Add(this.picFrm);
            this.picPanel.Location = new System.Drawing.Point(12, 45);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(788, 436);
            this.picPanel.TabIndex = 1;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.printPDFToolStripMenuItem,
            this.batchToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(845, 24);
            this.mainMenu.TabIndex = 12;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.loadMultiToolStripMenuItem,
            this.scaleWidthToolStripMenuItem,
            this.pageBreakToolStripMenuItem,
            this.grayScaleToolStripMenuItem,
            this.markBreaksToolStripMenuItem,
            this.make4x4ToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.loadImageToolStripMenuItem.Text = "L&oad Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // loadMultiToolStripMenuItem
            // 
            this.loadMultiToolStripMenuItem.Name = "loadMultiToolStripMenuItem";
            this.loadMultiToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.loadMultiToolStripMenuItem.Text = "Load &Multi";
            this.loadMultiToolStripMenuItem.Click += new System.EventHandler(this.loadMultiToolStripMenuItem_Click);
            // 
            // scaleWidthToolStripMenuItem
            // 
            this.scaleWidthToolStripMenuItem.Name = "scaleWidthToolStripMenuItem";
            this.scaleWidthToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.scaleWidthToolStripMenuItem.Text = "Scale &Width";
            this.scaleWidthToolStripMenuItem.Click += new System.EventHandler(this.scaleWidthToolStripMenuItem_Click);
            // 
            // pageBreakToolStripMenuItem
            // 
            this.pageBreakToolStripMenuItem.Name = "pageBreakToolStripMenuItem";
            this.pageBreakToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.pageBreakToolStripMenuItem.Text = "Page &Break";
            this.pageBreakToolStripMenuItem.Click += new System.EventHandler(this.pageBreakToolStripMenuItem_Click);
            // 
            // grayScaleToolStripMenuItem
            // 
            this.grayScaleToolStripMenuItem.Name = "grayScaleToolStripMenuItem";
            this.grayScaleToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.grayScaleToolStripMenuItem.Text = "Gra&y Scale";
            this.grayScaleToolStripMenuItem.Click += new System.EventHandler(this.grayScaleToolStripMenuItem_Click);
            // 
            // markBreaksToolStripMenuItem
            // 
            this.markBreaksToolStripMenuItem.Name = "markBreaksToolStripMenuItem";
            this.markBreaksToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.markBreaksToolStripMenuItem.Text = "Mar&k Breaks";
            this.markBreaksToolStripMenuItem.Click += new System.EventHandler(this.markBreaksToolStripMenuItem_Click);
            // 
            // make4x4ToolStripMenuItem
            // 
            this.make4x4ToolStripMenuItem.Name = "make4x4ToolStripMenuItem";
            this.make4x4ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.make4x4ToolStripMenuItem.Text = "Make &4x4";
            this.make4x4ToolStripMenuItem.Click += new System.EventHandler(this.make4x4ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // printPDFToolStripMenuItem
            // 
            this.printPDFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.print144ToolStripMenuItem,
            this.print300ToolStripMenuItem,
            this.viewPrinterToolStripMenuItem});
            this.printPDFToolStripMenuItem.Name = "printPDFToolStripMenuItem";
            this.printPDFToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.printPDFToolStripMenuItem.Text = "Print PDF";
            // 
            // print144ToolStripMenuItem
            // 
            this.print144ToolStripMenuItem.Name = "print144ToolStripMenuItem";
            this.print144ToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.print144ToolStripMenuItem.Text = "Print 144 x 144";
            this.print144ToolStripMenuItem.Click += new System.EventHandler(this.print144ToolStripMenuItem_Click);
            // 
            // print300ToolStripMenuItem
            // 
            this.print300ToolStripMenuItem.Name = "print300ToolStripMenuItem";
            this.print300ToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.print300ToolStripMenuItem.Text = "Print 300 x 300";
            this.print300ToolStripMenuItem.Click += new System.EventHandler(this.print300ToolStripMenuItem_Click);
            // 
            // viewPrinterToolStripMenuItem
            // 
            this.viewPrinterToolStripMenuItem.Name = "viewPrinterToolStripMenuItem";
            this.viewPrinterToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.viewPrinterToolStripMenuItem.Text = "View Printer";
            // 
            // batchToolStripMenuItem
            // 
            this.batchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchProcessMenuItem,
            this.prinImagesMenuItem});
            this.batchToolStripMenuItem.Name = "batchToolStripMenuItem";
            this.batchToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.batchToolStripMenuItem.Text = "&Batch";
            // 
            // batchProcessMenuItem
            // 
            this.batchProcessMenuItem.Name = "batchProcessMenuItem";
            this.batchProcessMenuItem.Size = new System.Drawing.Size(166, 22);
            this.batchProcessMenuItem.Text = "&Load and Process";
            this.batchProcessMenuItem.Click += new System.EventHandler(this.batchProcessMenuItem_Click);
            // 
            // prinImagesMenuItem
            // 
            this.prinImagesMenuItem.Name = "prinImagesMenuItem";
            this.prinImagesMenuItem.Size = new System.Drawing.Size(166, 22);
            this.prinImagesMenuItem.Text = "&Print Split Images";
            this.prinImagesMenuItem.Click += new System.EventHandler(this.prinImagesMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 497);
            this.Controls.Add(this.picPanel);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmMain";
            this.Text = "eBook Cutter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picFrm)).EndInit();
            this.picPanel.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picFrm;
        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMultiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleWidthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageBreakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markBreaksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem make4x4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem print144ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem print300ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewPrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchProcessMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prinImagesMenuItem;
    }
}

