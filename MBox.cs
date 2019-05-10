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
    public partial class MBox : DevExpress.XtraEditors.XtraForm
    {
        string MsgCaption = "";
        string MsgText = "";
        public MBox()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

        }
        public string[] SetMsg
        {
            set
            {
                labelControl1.Text = value[0];
                this.Text = value[1];
                switch (Int32.Parse(value[2]))                
                {
                    case 0://確定
                        simpleButton1.Left = 76;
                        simpleButton2.Visible = false;
                        break;
                    case 1: //確定，取消
                        break;
                    case 2:
                        break;

                }
            }
        }
    }
}