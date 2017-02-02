using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ebookCutter
{
    public partial class frmCrop : Form
    {
        public frmCrop()
        {
            InitializeComponent();
        }

        public System.Windows.Forms.PictureBox hostImage;

        private void frmCrop_Load(object sender, EventArgs e)
        {
            picFrm.SizeMode = PictureBoxSizeMode.AutoSize;
            frmCrop_Resize(sender, e);

        }

        private void frmCrop_Resize(object sender, EventArgs e)
        {
            picPanel.Width = this.ClientRectangle.Width - (picPanel.Left * 2);
            picPanel.Height = this.ClientRectangle.Height - (picPanel.Top + picPanel.Left);

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "jpg";
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.png";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap[] bmps = new Bitmap[dlg.FileNames.Length];
                int h = 0;
                int w = 0;
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    bmps[i] = new Bitmap(dlg.FileNames[i]);
                    h += bmps[i].Height;
                    if (bmps[i].Width > w) w = bmps[i].Width;
                }
                Bitmap bmp = new Bitmap(w, h);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(System.Drawing.Color.White);

                h = 0;
                for (int i = 0; i < bmps.Length; i++)
                {
                    g.DrawImage(bmps[i], 0, h, bmps[i].Width, bmps[i].Height);
                    h += bmps[i].Height;
                }

                hostImage.Image = bmp;
                //picFrm.Image = bmp;
                //imageFilePath = dlg.FileName;
            }


        }
    }
}
