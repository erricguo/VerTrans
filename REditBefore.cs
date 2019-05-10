using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace VerTrans
{
    public partial class REditBefore : Form
    {
        public REditBefore()
        {
            InitializeComponent();
        }
        int Findex = 0;
        string FType = "";
        int FHeight = 190;
        const int SetHeight = 440;
        string FMark = "";
        public string SetType
        {
            set
            {
                FType = value;
            }
        }
        public int GetReturn
        {
            get
            {
                return Findex;
            }
        }
        public string GetMark
        {            
            get
            {
                FMark = @" " + FMark;
                return FMark;
            }
        }
        private void tp04_btnEdit_Click(object sender, EventArgs e)
        {
            Findex = Int32.Parse((sender as DevExpress.XtraEditors.SimpleButton).Tag.ToString());
            switch (Findex)
            {
                case 0:
                case 1:
                    this.Height = SetHeight;
                    groupControl1.Enabled = false;
                    break;

            }
        }

        private void REditBefore_Load(object sender, EventArgs e)
        {
            if (FType.EndsWith("M"))
            {
                btn_Mark.Enabled = false;
                btn_Modi.Enabled = false;
            }
            else
            {
                btn_UnMark.Enabled = false;
            }
           this.Height= FHeight ;
        }

        private void RE_td05_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            FMark = RE_td05.Text;
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.Height = FHeight;
            groupControl1.Enabled = true;
        }

        private void RE_td05_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btn_OK.PerformClick();
            }
            else if (e.KeyValue == 27)
            {
                btn_No.PerformClick();
            }
        }

        private void REditBefore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                simpleButton3.PerformClick();
            }
        }
    }
}
