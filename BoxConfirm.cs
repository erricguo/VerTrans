using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VerTrans
{
    public partial class BoxConfirm : DevExpress.XtraEditors.XtraForm
    {
        public string SetMsg
        {
            set
            {
                lb01.Text = value;
            }
        }
        public string SetTitle
        {
            set
            {
                this.Text = value;
            }
        }
        public BoxConfirm()
        {
            InitializeComponent();
        }

        private void tp05_OK_Click(object sender, EventArgs e)
        {
            fc.WriteLog(lb01.Text, true);
        }
    }
}