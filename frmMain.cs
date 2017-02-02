/*
  Credits 
  GrayScale() and Pixel Access: Original code by Christian Graus https://www.codeproject.com/Articles/1989/Image-Processing-for-Dummies-with-C-and-GDI-Part
  print images : (Damien_The_Unbeliever) http://stackoverflow.com/questions/8442361/print-multiple-images-on-click-of-one-button-print-all-in-winforms
  ScaleWidth : Wendy Zang https://social.msdn.microsoft.com/Forums/expression/en-US/3e14a438-74d1-4b11-9631-9a71f467e08c/what-is-the-best-way-to-keep-images-data-when-modifying?forum=csharpgeneral
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace ebookCutter
{
    public partial class frmMain : Form
    {
        enum RgbTrims : byte { RgbTrim4 = 0xF0, RgbTrim3 = 0xF8, RgbTrim2 = 0xFC, RgbTrim1 = 0xFE };
        const byte TrimRgb = (byte)RgbTrims.RgbTrim4;
        int lastHeight;
        int currentPage;
        Bitmap masterImage;
        private string imageFilePath = "";
        private System.Collections.ArrayList Pages = new System.Collections.ArrayList();

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            picFrm.SizeMode = PictureBoxSizeMode.AutoSize;
            frmMain_Resize(sender, e);
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            picPanel.Width = this.ClientRectangle.Width - (picPanel.Left * 2);
            picPanel.Height = this.ClientRectangle.Height - (picPanel.Top + picPanel.Left);
        }
        private void print144ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPdf(144);
        }
        private void print300ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPdf(300);
        }
        private void PrintPdf(int DPI)
        {
            GC.Collect();
            //GC.WaitForPendingFinalizers();
            string DocName = "Document";
            if (imageFilePath.Length > 0)
            {
                int i = imageFilePath.LastIndexOf('\\');
                if (i > 0) DocName = imageFilePath.Substring(i + 1);
                i = DocName.LastIndexOf('.');
                if (i > 0) DocName = DocName.Substring(0, i);
            }
            DocName += "_" + DPI;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PrinterSettings.PrinterName = "CutePDF Writer";
            pd.DefaultPageSettings.Landscape = false;
            pd.DocumentName = DocName;

            if (viewPrinterToolStripMenuItem.Checked)
            {
                PrintDialog pdi = new PrintDialog();
                pdi.Document = pd;
                pdi.ShowDialog();
            }

            for (int i = 0; i < pd.PrinterSettings.PrinterResolutions.Count; i++)
            {
                if (pd.PrinterSettings.PrinterResolutions[i].X == DPI)
                {
                    pd.PrinterSettings.DefaultPageSettings.PrinterResolution = pd.PrinterSettings.PrinterResolutions[i];
                    pd.DefaultPageSettings.PrinterResolution = pd.PrinterSettings.PrinterResolutions[i];
                }
            }
            pd.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            currentPage = 0;
            lastHeight = 0;
            masterImage = (Bitmap)(picFrm.Image);
            try
            {
                pd.PrintPage += new PrintPageEventHandler(PrintImageHandler);
                pd.EndPrint += new PrintEventHandler(PrintEndHandler);
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static bool MarkBreaks(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    byte* q = p;
                    bool isBlank = true;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0]; green = p[1]; red = p[2];
                        if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                        p += 3;
                    }
                    if (isBlank == true)
                    {
                        for (int x = 0; x < b.Width; ++x) { q[0] = 0; q[1] = 0; q[2] = 255; q += 3; }
                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);
            return true;
        }

        public static bool GrayScale(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red
                            + .587 * green
                            + .114 * blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }
        private void PrintEndHandler(object sender, PrintEventArgs e)
        {
            MessageBox.Show("Done", "Print to PDF");
        }
        private void PrintImageHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            int h = (int)(Pages[currentPage]) - lastHeight;
            Bitmap tmp = new Bitmap(800, h);
            Graphics graphics = Graphics.FromImage(tmp);
            graphics.DrawImage(masterImage, new Rectangle(0, 0, 800, h), new Rectangle(0, lastHeight, 800, h), GraphicsUnit.Pixel);
            graphics.Dispose();
            lastHeight = (int)(Pages[currentPage]) + 1;
            currentPage++;

            tmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Rectangle m = ppeArgs.MarginBounds;

            Graphics g = ppeArgs.Graphics;
            g.DrawImage(tmp, m);
            ppeArgs.HasMorePages = currentPage < Pages.Count;
        }
        public static Point TrimImageHor(Bitmap b, bool head, bool tail)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            Point pt = new Point(0, b.Height);
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                byte red, green, blue;
                int mid = (b.Height / 2);

                //------------------------------------------------ Downward
                if(head) for (int f = 0; f < (mid-4); f++)
                {
                    byte* q = p;
                    q += (b.Width * 3 * f) + (nOffset * f);
                    bool isBlank = true;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = q[0]; green = q[1]; red = q[2];
                        if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                        q += 3;
                    }
                    if (isBlank == false) f = mid;//break the loop
                    else pt.X++;
                }
                //------------------------------------------------ Upward
                if(tail) for (int f = b.Height -1 ; f > (mid+4); f--)
                {
                    byte* q = p;
                    q += (b.Width * 3 * f) + (nOffset * f);
                    bool isBlank = true;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = q[0]; green = q[1]; red = q[2];
                        if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                        q += 3;
                    }
                    if (isBlank == false) f = mid;//break the loop
                    else pt.Y--;
                }
                //--------------------------
            }
            b.UnlockBits(bmData);
            return pt;
        }

        public static bool PageBreak(Bitmap b, ref System.Collections.ArrayList PagePos)
        {
            PagePos.Clear();
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                byte red, green, blue;

                for (int y = 580; y < b.Height; ++y)
                {
                    int min = int.MaxValue; int max = 0;
                    bool found = false;
                    //------------------------------------------------ Downward
                    for (int f = 0; f < 20 && (y+f) < b.Height; f++)
                    {
                        byte* q = p;
                        q += (b.Width * 3 * (y + f)) + (nOffset * (y + f));
                        bool isBlank = true;
                        for (int x = 0; x < b.Width; ++x)
                        {
                            blue = q[0]; green = q[1]; red = q[2];
                            if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                            q += 3;
                        }
                        if (found == true && isBlank == false) f = 20;//break the loop
                        else if (isBlank == true)
                        {
                            int y2 = (y + f);
                            if (y2 > max) max = y2;
                            if (y2 < min) min = y2;
                            found = true;
                        }
                    }
                    //------------------------------------------------ Upward
                    if (found == true) y = min;
                    for (int f = -1; f > -200; f--)
                    {
                        byte* q = p;
                        q += (b.Width * 3 * (y + f)) + (nOffset * (y + f));
                        bool isBlank = true;
                        for (int x = 0; x < b.Width; ++x)
                        {
                            blue = q[0]; green = q[1]; red = q[2];
                            if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                            q += 3;
                        }
                        if (found == true && isBlank == false) f = -300;//break the loop
                        else if (isBlank == true)
                        {
                            int y2 = (y + f);
                            if (y2 > max) max = y2;
                            if (y2 < min) min = y2;
                            found = true;
                        }
                    }
                    //--------------------------
                    if (min < int.MaxValue && max > 0)
                    {
                        byte* q = p;
                        int f = ((max + min) / 2);
                        q += (b.Width * 3 * f) + (nOffset * f);
                        PagePos.Add(f);
                        for (int x = 0; x < b.Width; ++x) { q[0] = 0; q[1] = 0; q[2] = 255; q += 3; }
                        y = f;
                    }
                    y += 579;
                }
            }
            PagePos.Add(b.Height);
            b.UnlockBits(bmData);
            return true;
        }

        public static bool BifurcatePage(Bitmap b, ref System.Collections.ArrayList PagePos,int forward,int backword)
        {
            PagePos.Clear();
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            backword = backword * -1;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                byte red, green, blue;
                int y = (b.Height / 2);

                int min = int.MaxValue; int max = 0;
                bool found = false;
                //------------------------------------------------ Downward
                for (int f = 0; f < forward && (y + f) < b.Height; f++)
                {
                    byte* q = p;
                    q += (b.Width * 3 * (y + f)) + (nOffset * (y + f));
                    bool isBlank = true;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = q[0]; green = q[1]; red = q[2];
                        if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                        q += 3;
                    }
                    if (found == true && isBlank == false) f = forward + 1;//break the loop
                    else if (isBlank == true)
                    {
                        int y2 = (y + f);
                        if (y2 > max) max = y2;
                        if (y2 < min) min = y2;
                        found = true;
                    }
                }
                //------------------------------------------------ Upward
                if (found == true) y = min;
                for (int f = -1; f > backword; f--)
                {
                    byte* q = p;
                    q += (b.Width * 3 * (y + f)) + (nOffset * (y + f));
                    bool isBlank = true;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = q[0]; green = q[1]; red = q[2];
                        if (((blue & TrimRgb) != TrimRgb) || ((green & TrimRgb) != TrimRgb) || ((red & TrimRgb) != TrimRgb)) isBlank = false;
                        q += 3;
                    }
                    if (found == true && isBlank == false) f = backword - 1;//break the loop
                    else if (isBlank == true)
                    {
                        int y2 = (y + f);
                        if (y2 > max) max = y2;
                        if (y2 < min) min = y2;
                        found = true;
                    }
                }
                //--------------------------
                if (min < int.MaxValue && max > 0)
                {
                    byte* q = p;
                    int f = ((max + min) / 2);
                    q += (b.Width * 3 * f) + (nOffset * f);
                    PagePos.Add(f);
                    for (int x = 0; x < b.Width; ++x) { q[0] = 0; q[1] = 0; q[2] = 255; q += 3; }
                    y = f;
                }
            }
            PagePos.Add(b.Height);
            b.UnlockBits(bmData);
            return true;
        }

        private void make4x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(picFrm.Image);

            int newWidth = bmp.Width / 2;
            int newHeight = bmp.Height / 2;

            Bitmap bmp1 = new Bitmap(newWidth, newHeight);
            Graphics graphics = Graphics.FromImage(bmp1);
            graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, newWidth, newHeight), GraphicsUnit.Pixel);
            graphics.Dispose();

            Bitmap bmp2 = new Bitmap(newWidth, newHeight);
            graphics = Graphics.FromImage(bmp2);
            graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(newWidth + 1, 0, newWidth, newHeight), GraphicsUnit.Pixel);
            graphics.Dispose();

            Bitmap bmp3 = new Bitmap(newWidth, newHeight);
            graphics = Graphics.FromImage(bmp3);
            graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, newHeight + 1, newWidth, newHeight), GraphicsUnit.Pixel);
            graphics.Dispose();

            Bitmap bmp4 = new Bitmap(newWidth, newHeight);
            graphics = Graphics.FromImage(bmp4);
            graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(newWidth + 1, newHeight + 1, newWidth, newHeight), GraphicsUnit.Pixel);
            graphics.Dispose();

            string DocName = imageFilePath;
            if (imageFilePath.Length > 0)
            {
                int i = imageFilePath.LastIndexOf('\\');
                if (i > 0) DocName = imageFilePath.Substring(0, i + 1) + "..\\" + imageFilePath.Substring(i + 1);
                i = DocName.LastIndexOf('.');
                if (i > 0) DocName = DocName.Substring(0, i);
            }
            bmp1.Save(DocName + "_A.jpg", ImageFormat.Jpeg);
            bmp2.Save(DocName + "_B.jpg", ImageFormat.Jpeg);
            bmp3.Save(DocName + "_C.jpg", ImageFormat.Jpeg);
            bmp4.Save(DocName + "_D.jpg", ImageFormat.Jpeg);

            MessageBox.Show("Done");
        }



        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageFilePath = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "jpg";
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.png";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picFrm.ImageLocation = dlg.FileName;
                imageFilePath = dlg.FileName;
                this.Text = "ebookCutter: " + dlg.FileName;
            }
        }

        private void loadMultiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageFilePath = "";
            frmCrop dlg = new frmCrop();
            dlg.hostImage = picFrm;
            dlg.ShowDialog();
        }

        private Bitmap ScaleWidth(Bitmap bmp)
        {
            double ratio = 800.0 / ((double)(bmp.Width));

            int newWidth = 800; // (int)(bmp.Width * ratio);
            int newHeight = (int)((double)(bmp.Height) * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        private void scaleWidthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFrm.Image = ScaleWidth(new Bitmap(picFrm.Image));
        }

        private void pageBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(picFrm.Image);
            PageBreak(bmp, ref Pages);
            picFrm.Image = bmp;

        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(picFrm.Image);
            GrayScale(bmp);
            picFrm.Image = bmp;

        }

        private void markBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(picFrm.Image);
            MarkBreaks(bmp);
            picFrm.Image = bmp;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void batchProcessMenuItem_Click(object sender, EventArgs e)
        {
            imageFilePath = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "ini";
            dlg.Filter = "Batch Configuration|*.ini";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            frmBatch frm = new frmBatch();
            frm.iniFile = dlg.FileName;
            frm.ShowDialog();
        }

        private void prinImagesMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "Image File|*.png";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            frmBatch frm = new frmBatch();
            frm.pngFile = dlg.FileName;
            frm.ShowDialog();
        }
    }
}
