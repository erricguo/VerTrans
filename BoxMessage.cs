using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace VerTrans
{
    public partial class BoxMessage : DevExpress.XtraEditors.XtraForm
    {
        private int Seconds = 0;
        public int SetSeconds
        {
            set
            {
                Seconds = value;
            }
        }
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
        public BoxMessage()
        {
            InitializeComponent();
        }

        private void BoxMessage_Load(object sender, EventArgs e)
        {
            if (Seconds > 0)
            {
                timer1.Interval = Seconds;
                timer1.Enabled = true;
            }
        }

        private void tp05_OK_Click(object sender, EventArgs e)
        {
            fc.WriteLog(lb01.Text,true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tp05_OK.PerformClick();
        }

    }
}