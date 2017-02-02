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
    public class pngFile : System.Collections.IComparer
    {
        public string fileName = "";
        public int pageNum = -1;
        public int splitNum = -1;

        public pngFile()
        { }
        public pngFile(string pngFileName)
        {
            char[] deli = { '_','.' };
            fileName = pngFileName;
            string tmp = fileName;
            int i = tmp.LastIndexOf('\\');
            tmp = tmp.Substring(i + 1);

            string[] arr = tmp.Split(deli);
            if( arr.Length == 4 )
            {
                if( int.TryParse(arr[1],out pageNum) == false ) pageNum = -1;
                if (int.TryParse(arr[2], out splitNum) == false) splitNum = -1;
            }
        }
        int System.Collections.IComparer.Compare(Object x, Object y)
        {
            pngFile xo = (pngFile)x;
            pngFile yo = (pngFile)y;

            if (xo.pageNum > yo.pageNum) return 1;
            if (xo.pageNum < yo.pageNum) return -1;
            //both are same
            if (xo.splitNum > yo.splitNum) return 1;
            if (xo.splitNum < yo.splitNum) return -1;
            //both are same
            return 0;
        }
        public override string ToString()
        {
            return fileName;
        }
    }
    public partial class frmBatch : Form
    {
        private const int kindleWidth = (1440 / 4)  * 3;

        public string iniFile = "";
        public string pngFile = "";

        private System.Collections.ArrayList Pages = new System.Collections.ArrayList();
        private System.Collections.ArrayList splitFiles = new System.Collections.ArrayList();
        private bool canClose = true;
        public frmBatch()
        {
            InitializeComponent();
        }

        private void frmBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.CloseReason == CloseReason.
            if (canClose == false) e.Cancel = true;
            else e.Cancel = false;
        }
        private void Log(string msg)
        {
            msg += "\r\n";
            txtCon.Text += msg;
            if (txtCon.TextLength > 30000)
            {
                txtCon.Text = txtCon.Text.Substring(10000);
            }

            txtCon.SelectionStart = txtCon.Text.Length - 1;
            txtCon.ScrollToCaret();
            txtCon.Refresh();

            Application.DoEvents();
        }

        private void frmBatch_Shown(object sender, EventArgs e)
        {
            if (iniFile.Length > 2) ProcessBatch();
            else if (pngFile.Length > 2) PrintImages();
        }
        private void PrintImages()
        {
            canClose = false;

            string folder = pngFile;
            int i = folder.LastIndexOf('\\');
            folder = folder.Substring(0, i + 1);

            Log("Generating list of files from folder: " + folder);

            splitFiles.Clear();

            string[] files = System.IO.Directory.GetFiles(folder, "page_*.png");
            for (int x = 0; x < files.Length ; x++)
            {
                splitFiles.Add(new pngFile(files[x]));
                //Log("Image File at [" + x + "] is " + files[x]);
            }
            splitFiles.Sort(new pngFile());

            /*
            for (int x = 0; x < splitFiles.Count; x++)
                Log("Image File at [" + x + "] is " + splitFiles[x]);
            */
            string title = "Image Document";
            frmDocTitle dlg = new frmDocTitle();
            if (dlg.ShowDialog() == DialogResult.OK)
                title = dlg.DocTile;

            PrintPdf(300, title);
        }
        private void ProcessBatch()
        {

            canClose = false;

            string folder = iniFile;
            int i = folder.LastIndexOf('\\');
            folder = folder.Substring(0, i + 1);

            Log("Opening Batch file : " + iniFile);
            System.IO.StreamReader sr = new System.IO.StreamReader(iniFile);
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            char[] sep = { '=' };
            while (!sr.EndOfStream)
            {
                string a = sr.ReadLine();
                if (a.Length > 3 && !a.StartsWith("#"))
                {
                    string[] b = a.Split(sep);
                    if (b.Length == 2)
                    {
                        ht.Add(b[0], b[1]);
                    }
                }
            }
            sr.Close();

            string title = (string)ht["title"];
            string crop = (string)ht["crop"];
            bool grey = string.Compare(ht["grey"].ToString(), "yes") == 0 ? true : false;
            bool doPrint = string.Compare(ht["print"].ToString(), "yes") == 0 ? true : false;
            string outdir = (string)ht["outdir"];
            string pgbfwd = (string)ht["page-break-forward"];
            string pgbbwd = (string)ht["page-break-backword"];
            //
            bool hdt0 = string.Compare(ht["page0-head-trim"].ToString(), "yes") == 0 ? true : false;
            bool tlt0 = string.Compare(ht["page0-tail-trim"].ToString(), "yes") == 0 ? true : false;
            bool hdt1 = string.Compare(ht["page1-head-trim"].ToString(), "yes") == 0 ? true : false;
            bool tlt1 = string.Compare(ht["page1-tail-trim"].ToString(), "yes") == 0 ? true : false;
            bool hdt2 = string.Compare(ht["page2-head-trim"].ToString(), "yes") == 0 ? true : false;
            bool tlt2 = string.Compare(ht["page2-tail-trim"].ToString(), "yes") == 0 ? true : false;
            //

            Log("Crop values are : " + crop);
            Log("Perform Grey Scale : " + grey);
            Log("page-break-forward : " + pgbfwd);
            Log("page-break-backword : " + pgbbwd);
            Log("Output Directory : " + folder + outdir + "\\");

            char[] cm = { ',' };
            string[] pts = crop.Split(cm);
            int cleft = int.Parse(pts[0]);
            int ctop = int.Parse(pts[1]);
            int cright = int.Parse(pts[2]);
            int cbottom = int.Parse(pts[3]);
            int pbf = int.Parse(pgbfwd);
            int pbb = int.Parse(pgbbwd);

            int fid = 1;
            splitFiles.Clear();
            while (ht.Contains("input" + fid))
            {
                string fname = (string)ht["input" + fid];
                Log("Loading image file : " + fname);
                Bitmap fileBmp = new Bitmap(folder + fname);

                Log("Doing Grey Scale if specified");
                if (grey) frmMain.GrayScale(fileBmp);

                Log("Doing "+ kindleWidth+" width scaling");
                int cropWidth = cright - cleft;
                int cropHeight = cbottom - ctop;

                double ratio = ((double)kindleWidth) / ((double)(cropWidth));
                int newWidth = kindleWidth;
                int newHeight = (int)((double)(cropHeight) * ratio);
                if ((newHeight % 4) != 0)
                {
                    newHeight = ((newHeight / 4)*4)+4;
                }

                Bitmap bmp = new Bitmap(newWidth, newHeight);
                // Draws the image in the specified size with quality mode set to HighQuality
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(fileBmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(cleft, ctop, cropWidth, cropHeight), GraphicsUnit.Pixel);
                }
                fileBmp.Dispose();
                fileBmp = null;
                if (hdt0 || tlt0)
                {
                    Point pt = frmMain.TrimImageHor(bmp, hdt0, tlt0);
                    Bitmap tmp2 = new Bitmap(newWidth, pt.Y - pt.X);
                    Graphics graphics = Graphics.FromImage(tmp2);
                    graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, (pt.Y - pt.X)), new Rectangle(0, pt.X, newWidth, (pt.Y - pt.X)), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    bmp = tmp2;
                    newHeight = (pt.Y - pt.X);
                }


                Log("Doing Page Breaking");
                //frmMain.PageBreak(bmp, ref Pages);
                frmMain.BifurcatePage(bmp, ref Pages,pbf,pbb);
                int p75 = ((newHeight / 4) * 3);
                if (Pages.Count == 1 || (int)Pages[0] > p75)
                {
                    Pages.Clear();
                    Pages.Add(newHeight / 2);
                    Pages.Add(newHeight);
                }


                int top = 0;
                for (i = 0; i < 2; i++)
                {
                    Log("Saving split page : " + fid + ":" + i);
                    int h = (int)Pages[i] - top;
                    Bitmap tmp = new Bitmap(newWidth, h);
                    Graphics graphics = Graphics.FromImage(tmp);
                    graphics.DrawImage(bmp, new Rectangle(0, 0, newWidth, h), new Rectangle(0, top, newWidth, h), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    //---------triming start-------
                    if (i == 0 && (hdt1 || tlt1))
                    {
                        Point pt = frmMain.TrimImageHor(tmp, hdt1, tlt1);
                        Bitmap tmp2 = new Bitmap(newWidth, pt.Y - pt.X);
                        graphics = Graphics.FromImage(tmp2);
                        graphics.DrawImage(tmp, new Rectangle(0, 0, newWidth, (pt.Y - pt.X) ), new Rectangle(0, pt.X, newWidth, (pt.Y - pt.X)), GraphicsUnit.Pixel);
                        graphics.Dispose();
                        tmp = tmp2;
                    }
                    if (i == 1 && (hdt2 || tlt2))
                    {
                        Point pt = frmMain.TrimImageHor(tmp, hdt2, tlt2);
                        Bitmap tmp2 = new Bitmap(newWidth, pt.Y - pt.X);
                        graphics = Graphics.FromImage(tmp2);
                        graphics.DrawImage(tmp, new Rectangle(0, 0, newWidth, (pt.Y - pt.X)), new Rectangle(0, pt.X, newWidth, (pt.Y - pt.X)), GraphicsUnit.Pixel);
                        graphics.Dispose();
                        tmp = tmp2;
                    }
                    //---------triming ends-------
                    string name = folder + outdir + "\\page_" + fid + "_" + i + ".png";
                    tmp.Save(name, ImageFormat.Png);
                    splitFiles.Add(name);
                    top = (int)Pages[i] + 1;
                }
                bmp.Dispose();
                bmp = null;

                Log("Done saving split pages");
                fid++;
            }
            Log("###  Finished Splitting all Pages.  ###");
            if(doPrint == true )
                PrintPdf(300, title);
            else
                canClose = true;
        }
        private void PrintEndHandler(object sender, PrintEventArgs e)
        {
            Log("### Printing Finished ###");
            canClose = true;
        }
        Pen myPen = null;
        private int splitIndex = 0;
        private void PrintImageHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            if (myPen == null)
            {
                myPen = new Pen(Color.Gray, 1); 
            }

            int DPI = 300;
            Log("Printing : " + (splitIndex+1) + " / " + splitFiles.Count);
            Bitmap tmp = new Bitmap(splitFiles[splitIndex].ToString());
            splitIndex++;
            tmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Rectangle m = ppeArgs.MarginBounds;

            double ratio =  ((double)(m.Height * DPI)) / ((double)(tmp.Height));
            int newHeight = m.Height * DPI;
            int newWidth = (int)((double)(tmp.Width) * ratio);
            if ((newWidth % 4) != 0)
            {
                newWidth = ((newWidth / 4) * 4) + 4;
            }
            if (newWidth > (m.Width * DPI) ) newWidth = (m.Width * DPI);
            int left = ((m.Width * DPI) - newWidth) / 2;
            if (left < 0) left = 0;

            Graphics g = ppeArgs.Graphics;
            g.DrawRectangle(myPen,m);
            g.DrawImage(tmp, new Rectangle(left/DPI,0, newWidth/DPI,m.Height) );
            ppeArgs.HasMorePages = splitIndex < splitFiles.Count;
        }

        private void PrintPdf(int DPI,string title)
        {
            GC.Collect();
            //GC.WaitForPendingFinalizers();
            string DocName = title;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PrinterSettings.PrinterName = "CutePDF Writer";
            pd.DefaultPageSettings.Landscape = false;
            pd.DocumentName = DocName;

            /*
            if (viewPrinterToolStripMenuItem.Checked)
            {
                PrintDialog pdi = new PrintDialog();
                pdi.Document = pd;
                pdi.ShowDialog();
            }*/

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

            pd.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Kindle", 335, 449);
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Kindle", 335, 449);

            splitIndex = 0;
            try
            {
                pd.PrintPage += new PrintPageEventHandler(PrintImageHandler);
                pd.EndPrint += new PrintEventHandler(PrintEndHandler);
                pd.Print();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
            }
        }

    }
}
