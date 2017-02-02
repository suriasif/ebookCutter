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
    public partial class frmDocTitle : Form
    {
        public frmDocTitle()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "ini";
            dlg.Filter = "Batch Configuration|*.ini";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            System.IO.StreamReader sr = new System.IO.StreamReader(dlg.FileName);
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
            txtDocTitle.Text = (string)ht["title"];
        }
        public string DocTile = "";
        private void btnOK_Click(object sender, EventArgs e)
        {
            DocTile = txtDocTitle.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
