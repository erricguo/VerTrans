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
    public partial class LoginSelect : DevExpress.XtraEditors.XtraForm
    {
        public LoginSelect()
        {
            InitializeComponent();
        }
        string type = "";
        public string GetType
        {
            get
            {
                return type;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            type = "POS";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            type = "ERP";
            /*ERPVerSelect EVS = new ERPVerSelect();
            EVS.SetBtnCount = 14;
            if (EVS.ShowDialog() == DialogResult.OK)
            {
                type = "ERP";
            }*/
            

        }
    }
}