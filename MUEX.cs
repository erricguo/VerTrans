using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace VerTrans
{
    public partial class MUEX : DevExpress.XtraEditors.XtraForm
    {
        public MUEX()
        {
            InitializeComponent();
        }
        string[] DBInfo = new string[6];
        string[] POSAccount = new string[2];
        public string[] SetDBInfo
        {
            set { DBInfo = value; }            
        }
        public string[] GetPOSACCOUNT
        {
            get
            {
                return POSAccount;
            }
        }

        private void MUEX_Load(object sender, EventArgs e)
        {
            ShowInfo(DBInfo[4]);
            Text = "["+DBInfo[4]+"] 勇者無懼 ~ 勇往直前 之 闖空門小幫手!";
        }
        public static string makeConnectString(bool xFSecurity_info, string xFIntegrated_Security, string xFServer, string xFDataBase,
            string xFUserID, string xFPassword)
        {
            string mB = "";
            if (xFSecurity_info)
                mB = "true";
            else
                mB = "False";

            string mRes = "";
            mRes = /*"Provider=" + FProvider +
                      ";*/
                          "Persist Security Info=" + xFSecurity_info +
                      ";Integrated Security=" + xFIntegrated_Security +
                      ";Data Source=" + xFServer +
                      ";Initial Catalog=" + xFDataBase +
                      ";User ID=" + xFUserID +
                      ";Password=" + xFPassword;
            return mRes;
        }
        private void AddColumn(string xCaption,string xFieldName,int xWidth)
        {//DevExpress.XtraGrid.Columns.GridColumn
            DevExpress.XtraGrid.Columns.GridColumn g = new DevExpress.XtraGrid.Columns.GridColumn();
            g.AppearanceCell.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            g.AppearanceCell.Options.UseFont = true;
            g.AppearanceHeader.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            g.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            g.AppearanceHeader.Options.UseFont = true;
            g.AppearanceHeader.Options.UseForeColor = true;
            g.AppearanceHeader.Options.UseTextOptions = true;
            g.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            if (xFieldName =="Index")
            {
                g.AppearanceCell.Options.UseTextOptions = true;
                g.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            g.Caption = xCaption;
            g.FieldName = xFieldName;
            g.Name = xFieldName;
            g.OptionsColumn.AllowEdit = false;
            g.Visible = true;
            g.VisibleIndex = 0;
            g.Width = xWidth;
            gridView2.Columns.Add(g);
            //return g;
        }
        private void ShowInfo(string type)
        {
            List<string> FList = null;// new List<string>();
            List<string> SList = null;// new List<string>();
            List<int> WList = null;// new List<string>();
            /*string id = "";
            string pw = "";*/
            string SQL = "";
            string DBType = ""; string ServerIP = ""; string DBName = ""; string UserID = ""; string UserPass = ""; string DSCSYS = "";
            string StoreNo = "";
            DataTable dt = new DataTable("456");
            bool IsHasPOSNU = true;
            SqlConnection conn = null;
            this.StartPosition = FormStartPosition.CenterParent;
            bool xFSecurity_info = false; string xFIntegrated_Security_def = "";
            //dt.Columns.Add("Index", typeof(string));
            //dt.Columns.Add(id, typeof(string));
            //dt.Columns.Add(pw, typeof(string));

            {
                ServerIP = DBInfo[0];
                DBName = DBInfo[1];
                UserID = DBInfo[2];
                UserPass = DBInfo[3];
                StoreNo = DBInfo[5];
            }
            if (UserID == "")
            { xFSecurity_info = false; xFIntegrated_Security_def = "SSPI"; }
            else
            { xFSecurity_info = true; xFIntegrated_Security_def = ""; }

            if (type=="POS") //POS
            {
                FList = new List<string>() { "Index","MU001", "MU003"};//, "MU015", "MU016", "MU017", "MU018" };
                SList = new List<string>() { "序號","收銀員帳號", "收銀員密碼"};//, "超級使用者", "MU016", "MU017", "MU018" };
                //WList = new List<int>() { 50,110,110};//,120,120,120,120 };
                WList = new List<int>() { 50, 140, 140 };
                for (int i = 0;i<FList.Count ;i++)
                {
                    dt.Columns.Add(FList[i],typeof(string));
                    AddColumn(SList[i], FList[i], WList[i]);
                    
                    //gridView2.Columns[i].BestFit();
                }
                //SQL = " Select MU001, MU003,MU015,MU016,MU017,MU018 from " + DBName + ".dbo.POSMU ";
                SQL = "select * from "+DBName+".sys.tables where name='POSNU'";
                conn = new SqlConnection(makeConnectString(xFSecurity_info, xFIntegrated_Security_def, ServerIP, DBName, UserID, UserPass));
                try
                {
                    conn.Open();
                    //if ((sender as SimpleButton).Name == "gbtnERPTest")
                    {
                        SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                        //SQL = " Select MU001, MU003 from " + DBName + ".dbo.POSMU ";
                        myCommand = new SqlCommand(SQL, conn);
                        myDataReader = myCommand.ExecuteReader();

                        if (!myDataReader.HasRows)
                        {
                            IsHasPOSNU = false;
                        }
                        conn.Close();
                        myCommand.Cancel();
                        myDataReader.Close();
                    }
                    
                }
                catch(Exception ex)
                {
                    fc.ShowBoxMessage(ex.Message);
                }

                SQL = " Select MU001, MU003 from " + DBName + ".dbo.POSMU " ;
                if (StoreNo.Trim() != "" && IsHasPOSNU)
                {
                    SQL +=  "LEFT JOIN " + DBName + ".dbo.POSNU ON NU001=MU001 AND NU002=" + "'"+StoreNo+"'"+
                            "WHERE MU018 IS NULL OR MU018 = '' OR MU018 ='N' OR (MU018='Y' AND NU001 IS NOT NULL) ";
                }
                    
               // this.Width = 340;
                
            }
            else if (type =="ERP") //ERP
            {
                //FList = new List<string>() { "Index", "MA001", "MA003", "MA004", "MA005", "MA008" };
                FList = new List<string>() { "Index", "MA001", "MA003"};
                //SList = new List<string>() { "序號", "登入者帳號", "登入者密碼", "可登入公司", "有效截止日期", "帳號鎖定碼" };
                SList = new List<string>() { "序號", "登入者帳號", "登入者密碼" };
                //WList = new List<int>() { 50, 110, 110};
                WList = new List<int>() { 50, 140, 140 };
                for (int i = 0; i < FList.Count; i++)
                {
                    dt.Columns.Add(FList[i], typeof(string));
                    AddColumn(SList[i], FList[i], WList[i]);
                }
                //SQL = " Select MA001, MA003,MA004,MA005,MA008 from " + DBName + ".dbo.DSCMA ";
                SQL = " Select MA001, MA003 from " + DBName + ".dbo.DSCMA ";
               // this.Width = 680;
                //this.Left = this.Left - 170;
                
            }

            conn = null;
            conn = new SqlConnection(makeConnectString(xFSecurity_info, xFIntegrated_Security_def, ServerIP, DBName, UserID, UserPass));
            try
            {
                conn.Open();
                //if ((sender as SimpleButton).Name == "gbtnERPTest")
                {
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    //SQL = " Select MU001, MU003 from " + DBName + ".dbo.POSMU ";
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();
                    int count = 1;

                    while (myDataReader.Read())
                    {
                        if (type == "POS") //POS
                        {
                            dt.Rows.Add(new object[] { (count).ToString(),
                                    myDataReader[FList[1]].ToString(), 
                                    fc.ERPDecrypt(myDataReader[FList[2]].ToString())/*,
                                    myDataReader[FList[3]].ToString(), 
                                    myDataReader[FList[4]].ToString(), 
                                    myDataReader[FList[5]].ToString(), 
                                    myDataReader[FList[6]].ToString() */                       
                            });
                        }
                        else if(type == "ERP")
                        {
                            dt.Rows.Add(new object[] { (count).ToString(),
                                    myDataReader[FList[1]].ToString(), 
                                    fc.ERPDecrypt(myDataReader[FList[2]].ToString())/*,
                                    myDataReader[FList[3]].ToString(), 
                                    myDataReader[FList[4]].ToString(), 
                                    myDataReader[FList[5]].ToString() */                     
                            });
                        }
                        count++;
                        //MUDic.Add(count++, new POSMU(myDataReader["MU001"].ToString(), ERPDecrypt(myDataReader["MU001"].ToString())));
                    }
                    myCommand.Cancel();
                    myDataReader.Close();
                    gridControl2.DataSource = dt;
                    gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
                    //gridView2.Columns["MU001"].BestFit();
                    //gridView2.Columns["MU003"].BestFit();
                }
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage(ex.Message.ToString());
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            POSAccount[0] = gridView2.GetFocusedRowCellValue("MU001").ToString().Trim();
            POSAccount[1] = gridView2.GetFocusedRowCellValue("MU003").ToString().Trim();
        }
    }
}