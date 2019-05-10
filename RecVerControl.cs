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
    public partial class RecVerControl : Form
    {
        DataTable Fdt = null;
        int VerIndex = 0;
        int NodeIndex = 0;
        string FIniPath = "";
        string FIniPathTmp = "";
        public RecVerControl(string xpath)
        {
            InitializeComponent();
            FIniPath = xpath + "\\POSVerNode.ini";
            FIniPathTmp = xpath + "\\POSVerNode_Tmp.ini";            
        }

        private void RecVerControl_Load(object sender, EventArgs e)
        {
            this.Width = 704;
            StreamReader r = new StreamReader(FIniPath, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            DataTable dt = new DataTable("VerInfo");
            DataTable dt2 = new DataTable("NoedeInfo");
            dt.Columns.Add("Ver", typeof(string));
            //dt2.Columns.Add("Idx", typeof(string));
            dt2.Columns.Add("Type", typeof(string));
            dt2.Columns.Add("StartNo", typeof(string));
            dt2.Columns.Add("EndNo", typeof(string));
            dt2.Columns.Add("Name", typeof(string));
            string[] mVer = source.Configs["Ver"].GetString("VerInfo").Split('|');            
            for (int i = 0; i < mVer.Length; i++)
            {
               dt.Rows.Add(new object[] { mVer [i]});                                 
            }            
            gridControl1.DataSource = dt;
            string[] mS = source.Configs["Node"].GetString("System").Split('|');
            string[] mI = source.Configs["Node"].GetString("Infomation").Split('|');
            string[] mE = source.Configs["Node"].GetString("Error").Split('|');
            NodeTalbeadd(dt2, mS, "S");
            NodeTalbeadd(dt2, mI, "I");
            NodeTalbeadd(dt2, mE, "E");
            gridControl2.DataSource = dt2;
            Fdt = (gridControl2.DataSource as DataTable);            
        }
        private void NodeTalbeadd(DataTable dt,string[] xStr, string xType)
        {
            foreach (string v in xStr)
            {
                string[] mt = fc.Split("]", v);
                string mt1 = ""; string mt3_0 = ""; string mt3_1 = "";
                if (mt.Length > 0)
                {
                    mt1 = mt[1];
                }
                string[] mt2 = fc.Split("[", mt[0]);
                string[] mt3 = fc.Split("~", mt2[0]);
                
                if (mt3.Length > 1)
                {
                    mt3_0 = mt3[0].Trim();
                    mt3_1 = mt3[1].Trim();
                }
                dt.Rows.Add(new object[] { /*dt.Rows.Count.ToString(),*/xType, mt3_0, mt3_1, mt1 });                   
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void btn_New_Click(object sender, EventArgs e)
        {            
            if ((sender as Button).Name == "btn_New")
            {
                btnNew(1,1);
            }
            else if ((sender as Button).Name == "btn_New2")
            {
                btnNew(1, 2);
            }
        }
        private void btnNew(int xtype,int xTalbeNo)
        {
            RecVer_Editor re = new RecVer_Editor();
            DataTable dt = null;

            re.SetControl = "2";
            re.SetType = xtype;
            re.SetTableNo = xTalbeNo;
            string[] mValue = null;
            switch (xTalbeNo)
            {
            case 1:
                    re.SetCaption = "現行版本";
                    dt = (gridControl1.DataSource as DataTable);
                    if (xtype==2)
                    {
                        if (dt.Rows.Count <=0)
                            return;
                        mValue = new string[] {VerIndex.ToString(), dt.Rows[VerIndex][0].ToString() };
                    }
            	break;
            case 2:
                    re.SetCaption = "節點編輯";
                    dt = (gridControl2.DataSource as DataTable);
                    if (xtype == 2)
                    {
                        if (dt.Rows.Count <= 0)
                            return;
                        mValue = new string[] { NodeIndex.ToString(),
                                                dt.Rows[NodeIndex][0].ToString(),
                                                dt.Rows[NodeIndex][1].ToString(),
                                                dt.Rows[NodeIndex][2].ToString(),
                                                dt.Rows[NodeIndex][3].ToString()};
                    }
                break;
            }
            re.SetTable = dt;
            if (xtype == 2) re.SetValue = mValue;
            if (re.ShowDialog() == DialogResult.OK)
            {
                dt = re.GetTable;
                if(xTalbeNo==1)
                    gridControl1.DataSource = dt;
                else if (xTalbeNo == 2)
                    gridControl2.DataSource = dt;
            }
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btn_Edit")
            {
                btnNew(2, 1);
            }
            else if ((sender as Button).Name == "btn_Edit2")
            {
                btnNew(2, 2);
            }
        }
        private void btnDel(DataTable dt,int idx)
        {
            if (fc.ShowConfirm("確定要刪除該筆資料?")==DialogResult.OK)
            {
                dt.Rows.RemoveAt(idx);
            }
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btn_Del")
            {
                btnDel((gridControl1.DataSource as DataTable), VerIndex);
            }
            else if ((sender as Button).Name == "btn_Del2")
            {
                btnDel((gridControl2.DataSource as DataTable), NodeIndex);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            VerIndex = e.FocusedRowHandle;
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            NodeIndex = e.FocusedRowHandle;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if ((File.GetAttributes(FIniPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                string str = Path.GetFileName(FIniPath);
                fc.ShowBoxMessage(str + "\r\n檔案唯讀!請確認有CHECK OUT再做修改");
                return;
            }

            File.Copy(FIniPath, FIniPathTmp, true);
            StreamWriter sw = new StreamWriter(FIniPath, false, Encoding.Default);
            try
            {
                DataTable dt = (gridControl1.DataSource as DataTable);
                string mver = "";
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    mver += dt.Rows[i][0].ToString() + "|";
                }
                mver = mver.Substring(0, mver.Length - 1);
                dt = (gridControl2.DataSource as DataTable);
                string mSystem = "";
                string mInfomation = "";
                string mError = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == "S")
                    {
                        mSystem += "["+dt.Rows[i][1].ToString()+" ~ "+
                                       dt.Rows[i][2].ToString()+"]"+
                                       dt.Rows[i][3].ToString()+ "|";
                    }
                    else if (dt.Rows[i][0].ToString() == "I")
                    {
                        mInfomation += "[" + dt.Rows[i][1].ToString() + " ~ " +
                                             dt.Rows[i][2].ToString() + "]" +
                                             dt.Rows[i][3].ToString() + "|";
                    }
                    else if (dt.Rows[i][0].ToString() == "E")
                    {
                        mError += "[" + dt.Rows[i][1].ToString() + " ~ " +
                                        dt.Rows[i][2].ToString() + "]" +
                                        dt.Rows[i][3].ToString() + "|";
                    }                    
                }
                mSystem = mSystem.Substring(0, mSystem.Length - 1);
                mInfomation = mInfomation.Substring(0, mInfomation.Length - 1);
                mError = mError.Substring(0, mError.Length - 1);
                sw.Write("[Ver]\r\n");
                sw.Write("VerInfo = " + mver + "\r\n");
                sw.Write("\r\n");
                sw.Write("[Node]\r\n");
                sw.Write("System = " + mSystem + "\r\n");
                sw.Write("Infomation = " + mInfomation + "\r\n");
                sw.Write("Error = " + mError + "\r\n");
                File.Delete(FIniPathTmp);
            }
            catch (System.Exception ex)
            {
                File.Copy(FIniPathTmp, FIniPath, true);
            }
            finally
            {
                sw.Close();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

    }
}
