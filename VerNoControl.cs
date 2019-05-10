using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nini.Config;

namespace VerTrans
{
    public partial class VerNoControl : Form
    {
        DataTable Fdt = null;
        int VerIndex = 0;
        int NodeIndex = 0;
        string FIniPath = "";
        string FIniPathTmp = "";
        public VerNoControl()
        {
            InitializeComponent();          
        }


        private void VerNoControl_Load(object sender, EventArgs e)
        {
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            DataTable dt = new DataTable("VerInfo");

            dt.Columns.Add("Ver", typeof(string));
            string[] mVer = source.Configs["VerNo"].GetString("No").Split('|');
            for (int i = 0; i < mVer.Length; i++)
            {
                dt.Rows.Add(new object[] { mVer[i] });
            }
            gridControl1.DataSource = dt;  
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            btnNew(1, 1);
        }
        private void btnNew(int xtype, int xTalbeNo)
        {
            RecVer_Editor re = new RecVer_Editor();
            DataTable dt = null;

            re.SetControl = "1";
            re.SetType = xtype;
            re.SetTableNo = xTalbeNo;
            string[] mValue = null;
            switch (xTalbeNo)
            {
                case 1:
                    re.SetCaption = "現行版本";
                    dt = (gridControl1.DataSource as DataTable);
                    if (xtype == 2)
                    {
                        if (dt.Rows.Count <= 0)
                            return;
                        mValue = new string[] { VerIndex.ToString(), dt.Rows[VerIndex][0].ToString() };
                    }
                    break;
                    
            }
            re.SetTable = dt;
            if (xtype == 2) re.SetValue = mValue;
            if (re.ShowDialog() == DialogResult.OK)
            {
                dt = re.GetTable;
                if (xTalbeNo == 1)
                    gridControl1.DataSource = dt;
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            btnNew(2, 1);
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            btnDel((gridControl1.DataSource as DataTable), VerIndex);
        }
        private void btnDel(DataTable dt, int idx)
        {
            if (fc.ShowConfirm("確定要刪除該筆資料?") == DialogResult.OK)
            {
                dt.Rows.RemoveAt(idx);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataTable dt = (gridControl1.DataSource as DataTable);
            string mver = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mver += dt.Rows[i][0].ToString() + "|";
            }
            mver = mver.Substring(0, mver.Length - 1);

            string filename = @fc.ConfigPath;
            string FType = "VerNo";
            IniConfigSource source = new IniConfigSource(filename);
            try
            {
                if (source.Configs[FType] == null)
                {
                    source.Configs.Add(FType);
                }
                if (File.Exists(filename))
                {
                    source.Configs[FType].Set("No", mver);
                    source.Save();
                    fc.ShowBoxMessage("已儲存!");
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            VerIndex = e.FocusedRowHandle;
        }
    }
}
