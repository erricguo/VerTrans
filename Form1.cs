using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualBasic.Devices;
using System.Threading;
using System.Collections;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;
using System.Drawing.Text;
using Nini.Config;
using Nini.Ini;
// 添加额外命名空间
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using DevExpress.XtraEditors.Controls;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace VerTrans
{
    public partial class Form1 : Form
    {

        public class DBINFO
        {
            public string DBType;
            public string ServerIP;
            public string DBName;
            public string UserID;
            public string UserPass;
            public string DSCSYS;
        }
        public class MACHINEPARAM
        {
            public string BAS_CompNo;
            public string BAS_CompShortName;
            public string BAS_StoreNo;
            public string BAS_StoreShortName;
            public string BAS_POSNO;
            public string BAS_Stamp_StoreName;
            public string BAS_Stamp_Manager;
            public string BAS_Stamp_CompName;
            public string BAS_Stamp_CompAddress;
            public string BAS_Stamp_CompPhone;
            public string BAS_Stamp_CompUniID;
            public string BAS_LoginID;
            public string BAS_LoginPW;
        }

        public static int hwnd = 0;
        Dictionary<string, String[]> LB_List = new Dictionary<string, String[]>();
        List<string> CboDList = new List<string>();
        public static Dictionary<string, string[]> AccountList = new Dictionary<string, string[]>();
        public static List<string> NoCopyList = new List<string>();
        List<string> List_ver = new List<string>();
        string[] SetEdit = new string[5];
        const int options = 2;
        bool IsLoadFile = false;
        string LoadFileName = "";
        bool IsClose = false;
        bool IsFindNet01 = false;
        int[] Node = new int[] { -1, -1, -1, -1 };
        int Space = 0;
        //DateTime dt = DateTime.Now;
        //private Form5 f5;  //20131223 mark form5 拿掉
        int filecounts = 0;
        int currentindex = 0;
        string myStr = null;
        string filename_copy = "";
        List<string[]> tmplist_copy = new List<string[]>();
        List<string> tmplist_path_copy = new List<string>();
        List<string> List_Copy = new List<string>();
        List<string> List_path_Copy = new List<string>();
        List<string> List_Fnmae_Copy = new List<string>();
        string[] Step1 = null;
        string[] Step2 = null;
        string[] Step3 = null;
        string Step4 = "";
        Dictionary<string, int> filesum = new Dictionary<string, int>();
        public static string ToolPath = "";
        public static string NowSetupName = "";
        public Dictionary<int, string> ResourceInc = new Dictionary<int, string>();
        //20130822 新增授權
        [DllImport("Authorize.dll", EntryPoint = "GOAuthorize", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string GOAuthorize(string xlbCompanyNo, string xlbStoreNo, string xlbPOSNo, string xedERPIP, string xedPort);              //delphi 中的函数

        [DllImport("Authorize.dll", EntryPoint = "DelAuthorize", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string DelAuthorize(string xlbCompanyNo, string xlbStoreNo, string xlbPOSNo, string xedERPIP, string xedPort);              //delphi 中的函数

        [DllImport("Authorize.dll", EntryPoint = "GOCheckAuthorise", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string GOCheckAuthorise();              //delphi 中的函数

        [DllImport("Authorize.dll", EntryPoint = "CDecrypt", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string CDecrypt(string S, string KeyWord);              //delphi 中的函数

        [DllImport("Authorize.dll", EntryPoint = "CEncrypt", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]     // "TestDll.dll"为dll名称,EntryPoint 为函数名
        public static extern string CEncrypt(string S, string KeyWord);              //delphi 中的函数
                                    
        
        public static string FProvider = "SQLOLEDB.1";
        public static string FIntegrated_Security_def = "SSPI";
        public static string Def_SQLConnect = "SQLConnect_1";
        DBINFO gERPDBInfo = new DBINFO();
        DBINFO gLocalDBInfo = new DBINFO();
        MACHINEPARAM gParam = new MACHINEPARAM();
        public static string C_PrivateKey = "12375447";//此Key勿任意更改
        public static bool IsAuthor = false;
        public static string CompanyNo = "";
        public static string StoreNo = "";
        public static string POSNo = "";
        public static string ERPIP = "";
        public static string Port = "";
        //20130822 新增授權

        //20131122 新增Resource編輯器
        bool isDebug = false;
        RI RI1;
        public struct ClassNo
        {
            public string mtype;
            public string mStartNo;
            public string mEndNo;
            public ClassNo(string xtype, string xStartNo, string xEndNo) { mtype = xtype; mStartNo = xStartNo; mEndNo = xEndNo; }
            
        }
        public Dictionary<int, ClassNo> tp04_Class = new Dictionary<int, ClassNo>();
        //20131122 新增Resource編輯器

        //20131128 新增2003圖書館
        //public List<XXX2003> List2003 = new List<XXX2003>();
        XXX2003 xx;
        string[] FCodeType = {"主檔單頭","主檔單身","交易單頭","交易單身","交易記錄","月檔/統計","系統檔","其它"};
        Font FontYahei;
        string tp05_basepath = "";
        string skk = "";
        //20131128 新增2003圖書館

        //20131208 
        string FCurrStr = ""; //目前指到的設定檔INDEX
        bool tp06_IsHasModiFolder = true;
        string modiname = "";
        //20131208

        //20131219
        Dictionary<int, POSMU> MUDic = new Dictionary<int, POSMU>();
        //20131219

        //20140107
        string TP04_LoadPath = "";
        //20140107

        //20140109
        string FCopyName = "";
        //20140109

        //20140113
        Font ft1 = new Font("微軟正黑體", 13, FontStyle.Bold);
        //20140113

        //20140205
        // 服务器端口
        int port;
        // 定义变量
        private UdpClient sendUdpClient;
        private UdpClient receiveUdpClient;
        private IPEndPoint clientIPEndPoint;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private BinaryReader binaryReader;
        private string userListstring;
        private static string txtserverIP = "10.40.31.89";
        private string txtLocalIP;
        private string txtlocalport;
        private string txtusername;
        private string SendUserIP;
        private string SendUserPORT;
        private DataTable gv3DT = new DataTable("123");
        //private List<ChatFormcs> chatFormList = new List<ChatFormcs>();
        
        //20140205 
        //20140211
        private string tp7_UserName= "";
        private string tp7_NewName = "";
        private string f2Setupname = "";
        private string f2SetupVerno = "";
        private string f2Setcode = "";
        private bool btnNewSetValue = false;
        //20140211
        //20140309
        private bool IsUnCopyResetOn = false;
        List<CheckedListBoxControl> clb = null;
        //20140309
        //20140312
        List<DataTable> dtList = new List<DataTable>();
        //20140312

        //20140704 增加 程式代號查詢
        List<string> tp07_lb01_List = new List<string>();
        //NameValueCollection keylist34 = new NameValueCollection();
        //NameValueCollection keylistGP2 = new NameValueCollection();
        string[] keylist34 = null;
        string[] keylistGP2 = null;
        string[] sectionlist = null;
        string[] sectionlist2 = null;
        //20140704 增加 程式代號查詢

        //20140806
        bool IsPOSAutoLogin = true;
        //20140806

        //20150706
        string[] FDownLoadPath = new string[4]; //POS下載路徑
        //20150706

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string aaa = fc.ERPDecrypt("32DC6C1291CC");
            
            //fc.installFont(Properties.Resources.YaHei_Consolas_1_12, "YaHei Consolas Hybrid", "微軟雅黑");
            hwnd = (int)this.Handle;
            //fc.GetSingleThread();
            fc.PrevInstance();
            lbl_Cur_Code.Text = "";
            lbl_Cur_Ver.Text = "";
            lbl_Cur_Name.Text = "";
            lbl_Cur_ProgPath.Text = "";
            lbl_Cur_VerNo.Text = "";
            cbo_ver.SelectedIndex = 0;
            cbo_ProgPath.SelectedIndex = -1;

            Str07.Text = "";
            //指定使用的容器
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Text = "轉換版本代號 Ver " + fc.GetVersion() + " @ ErricGuo ";
            if (fc.GetVersion() == "開發程式階段")
            {
                isDebug = true;
            }

            fc.ChkFiles();
            fc.LoadVerInfo();
            LoadSetupForCbo("SetupPath");
            tb_VerNo.Text = LoadConfigValue("CurrenInfo","VerNo");
            cbo_VerNo.Text = LoadConfigValue("SetupVerNo", "VerNo");
            TP04_LoadPath = LoadConfigValue("TP04", "LoadPath");
            tp04_tdFileName.Text = LoadConfigValue("TP04", "LoadPath");
            tp7_UserName = LoadConfigValue("TP07", "UserName");
            barCheckItem1.Checked = LoadConfigValueBool("Options", "IsPOSAutoLogin");
            barCheckItem2.Checked = LoadConfigValueBool("Options", "IsClearPI032"); //20160301 add  by Erric for 20160301001 
            //edPOSID.Text = LoadConfigValue("AutoLogin", "LoginID");
            //edPOSPW.Text = LoadConfigValue("AutoLogin", "LoginPW");

            if ((bool)LoadConfigValueO("UpdatePI", "AutoUpdate"))
                chkUpdatePI.Checked = true;
            else
                chkUpdatePI.Checked = false;
            if (tp7_UserName == "") tp7_UserName = System.Windows.Forms.SystemInformation.ComputerName;            
            
            //20160718
            string tmpVerno = LoadConfigValue("VerNo", "No");
            if (tmpVerno != "")
            {
                string[] sp = fc.Split("|", tmpVerno);
                for (int i = 0; i < sp.Length;i++ )
                {
                    cbo_VerNo.Properties.Items.Add(sp[i].Trim());
                }        
            }
            //20160718

            WriteConfigIni();
            ReadIni();
            cbo_VerNo.Text = LoadConfigValue("SetupVerNo", "VerNo");
            LoadSetupForCbo("SetupPath");
            LoadVerNoForCbo("VerNo");
            LoadCurrentInfo();
            LoadAccount();
            lbl_PCName.Text = System.Windows.Forms.SystemInformation.ComputerName;
            lbl_LocalIP.Text = fc.GetLocalIP();

            //MODICHECK
            tb_min.Text = "10";
            lb_msg.Text = "";
            lb_sav.Text = "";
            //---------

            //20130822 新增授權
            lbStoreName.Text = "";
            lbCompanyName.Text = "";
            //gbtnAuth.Visible = false;
            //gbtnDelAuth.Visible = false;

            LoadIni();
            VarInit();
            SetUserIP("", 2);
            CheckAuth();
            SaveTmpAuthInfo();
            //SaveSetupInfo();
            //20130822 新增授權

            //20131122 新增Resource編輯器
            tp04_cboClass.Enabled = tp04_chkClass.Checked;
            if (!isDebug)
            {
                tp04_lb11.Visible = false;
                tp04_td11.Visible = false;
                gridView1.Columns[0].Visible = false;
            }

           /* for (int i = 0; i < tp04_cboClass.Properties.Items.Count; i++)
            {
                string[] mt = fc.Split("[", tp04_cboClass.Properties.Items[i].ToString());
                string[] mt2 = fc.Split("]", mt[0]);
                string mt3 = fc.Split("]", mt[1])[0].Trim();
                string[] mt4 = fc.Split("~", mt3);
                if (mt4.Length > 1)
                    tp04_Class.Add(i, new ClassNo(mt2[0], mt4[0].Trim(), mt4[1].Trim()));
                else
                    tp04_Class.Add(i, new ClassNo(mt2[0], mt4[0].Trim() + "001", mt4[0].Trim() + "999"));
            }*/
            //20131122 新增Resource編輯器

            //20131128 新增2003圖書館
            if (fc.isFontExist("YaHei Consolas Hybrid"))
            {
                FontYahei = new Font("YaHei Consolas Hybrid", 13, FontStyle.Bold);
            }
            else
            {
                PrivateFontCollection p_Font = new PrivateFontCollection();
                byte[] b_Font = new byte[VerTrans.Properties.Resources.YaHei_Consolas_1_12.Length];
                IntPtr MeAdd = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * b_Font.Length);

                Marshal.Copy(VerTrans.Properties.Resources.YaHei_Consolas_1_12, 0, MeAdd, VerTrans.Properties.Resources.YaHei_Consolas_1_12.Length);
                p_Font.AddMemoryFont(MeAdd, b_Font.Length);
                FontYahei = new Font(p_Font.Families[0].Name, 13, FontStyle.Bold);
            }
            //fc.ShowBoxMessage("字體資訊:" + FontYahei.Name);
            //20131128 新增2003圖書館

            //20131207 新增POS下載
            tp06_chklist01.Items.Clear();
            tp06_chklist02.Items.Clear();
            tp06_chklist03.Items.Clear();
            tp06_chklist04.Items.Clear();
            string tmppwc = LoadConfigValue("TP06", "PWChar");
            tp06_PW.PasswordChar = tmppwc == "" ? char.MinValue : tmppwc[0];
            //tp06_chklist02.Items.Clear();
            //20131207 新增POS下載    
        
            //TP07-----------------------------------------------------------------------------------------------
            LoadPGSections();

            //20140205
            //IPAddress[] localIP = Dns.GetHostAddresses("");
            //txtserverIP.Text = localIP[1].ToString();
            //txtLocalIP = localIP[1].ToString();
            txtLocalIP = lbl_LocalIP.Text;
            // 随机指定本地端口
            Random random = new Random();
            port = random.Next(1024, 65500);
            txtlocalport = port.ToString();

            // 随机生成用户名
            //Random random2 = new Random((int)DateTime.Now.Ticks);
            //txtusername = lbl_PCName.Text;
            txtusername = tp7_UserName;
            lstviewOnlineUser.Items.Clear();
            gv3DT.Columns.Add("Read");
            gv3DT.Columns.Add("Time");
            gv3DT.Columns.Add("Send");
            gv3DT.Columns.Add("Main");
            gv3DT.Columns.Add("VerNo");
            gv3DT.Columns.Add("Setup");            
            LoginToServer();
            //20140205

            if (!fc.Isx64())
            {
                barButtonItem10.Caption = "System32";
            }
            else
            {
                barButtonItem10.Caption = "SysWOW64";
            }
            SelectVerNo();

            //20140309
            clb = new List<CheckedListBoxControl>() { tp06_chklist01, tp06_chklist02, tp06_chklist03, tp06_chklist04 };            
            //20140309

            for (int i = 0; i < FDownLoadPath.Length;i++ )
            {
                FDownLoadPath[i] = "";
            }
        }

        public void SetAutoLoginGroup()
        {
            if (cbo_VerNo.Text == "21" || cbo_VerNo.Text == "31" ||
                cbo_VerNo.Text == "33" || cbo_VerNo.Text == "34" ||
                cbo_VerNo.Text == "GP1")
            {
                btnedPOSSave.Visible = false;
                lb_setup.Height = 505; 
            }
            else
            {
                btnedPOSSave.Visible = true;
                lb_setup.Height = 365;
            }
        }
        public Dictionary<string, string[]> GetAccountInfo
        {
            get
            {
                return AccountList;
            }
        }
        //*********recursiveCopy***********
        private string[] recursiveCopy(string sourcePath, string targetPath)
        {
            if (System.IO.Directory.Exists(sourcePath))
            {
                //copy directories
                if (!System.IO.Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                    //UpdateMsg("",targetPath, true);
                }
                /* 20130108 不需要再source裡面下載其他dir
                string[] dirs = System.IO.Directory.GetDirectories(sourcePath);
                foreach (string d in dirs)
                {
                    string nextSource = d + "\\";
                    string[] directName = d.Split("\\".ToCharArray());
                    string nextTarget = targetPath + directName[directName.Length - 1] + "\\";
                    recursiveCopy(nextSource, nextTarget);
                } 20130108 */

                //copy files
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                return files;
            }
            return null;
        }//End recursiveCopy

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (cbo_ver.Text == "")
            {
                fc.ShowBoxMessage("版本不可空白!!");
                cbo_ver.Focus();
                return;
            }
            if (!fc.isDirectory(cbo_ProgPath.Text))
            {
                fc.ShowBoxMessage("程式路徑不存在!!");
                cbo_ProgPath.Focus();
                return;
            }
            RegistryKey start = Registry.LocalMachine;
            RegistryKey programName = start.CreateSubKey(fc.keyPathString);
            try
            {
                string TypeName = "";
                Color c = new Color();
                TypeName = GetPosTypeName(cbo_ver.SelectedIndex, out c);
                programName.SetValue("MODULE_ID", cbo_ver.SelectedIndex, RegistryValueKind.DWord);
                programName.SetValue("CTICustNo", tb_code.Text);
                programName.SetValue("ProgPath", cbo_ProgPath.Text);
                programName.SetValue("MODULE_NAME", TypeName);
            }
            catch (System.Exception ex)
            {                
                fc.ShowBoxMessage("寫入失敗!!\r\n" + ex.Message);
                fc.WriteLog(ex.Message, true);
            }
            programName.Close();
            NowSetupName = tb_SetupName.Text;
            WriteConfigIni();
            LoadCurrentInfo();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int lb_setup_CurrIndex = -1;
        int lb_setup_OldIndex = -1;
        private void lb_setup_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_setup_CurrIndex = lb_setup.SelectedIndex;
            cbStoreNo.Properties.Items.Clear();
            ListBoxControl lb = (ListBoxControl)sender;
            int idx = lb.SelectedIndex;
            if (idx == -1)
            {
                return;
            }
            else
            {
                foreach (string s in LB_List.Keys)
                {
                    if (lb.Items[idx].ToString() == s)
                    {
                        tb_SetupName.Text = LB_List[s][0];
                        cbo_ver.SelectedIndex = Int32.Parse(LB_List[s][1]);
                        tb_code.Text = LB_List[s][2];
                        cbo_ProgPath.Text = LB_List[s][3];
                        tb_VerNo.Text = LB_List[s][4];
                        if (cbo_ProgPath.Text=="")
                        {
                            cbo_ProgPath.Text = @"C:\COSMOS_POS";
                        }
                        FCurrStr = s;
                        break;
                    }
                }
            }
            if (LoadSetupInfo(tb_SetupName.Text))
                VarInit();
            else
                DefaultVarInit();

            SetAutoLoginGroup();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            int idx = lb_setup.SelectedIndex;
            if (idx == -1)
            {
                return;
            }
            else
            {
                if (fc.ShowConfirm("是否刪除設定檔 <<" + lb_setup.Items[idx].ToString() + ">> ? ", "詢問") == DialogResult.OK)
                {
                    string filename = fc.CustPath;
                    string setupname = lb_setup.Items[idx].ToString();
                    /*                lb_setup.Items.Add(s[0]);
                        cbo_ver.SelectedIndex = Int32.Parse(s[1]);
                        tb_code.Text = s[2];
                        cbo_ProgPath.Text = s[3];
                        tb_VerNo.Text = s[4];*/
                    IniConfigSource source = new IniConfigSource(filename);
                    if (source.Configs[setupname] != null)
                    {
                        source.Configs.Remove(source.Configs[setupname]);
                    }
                    source.Save();

                    LB_List.Remove(lb_setup.Items[idx].ToString());
                    lb_setup.Items.Remove(lb_setup.Items[idx].ToString());
                    //lb_setup.Items.RemoveAt(idx);
                    lb_setup.SelectedIndex = lb_setup.Items.Count - 1;
                }
            }
            WriteIni();


        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            int idx = lb_setup.SelectedIndex;
            if (idx == -1)
            {
                return;
            }
            SetEdit[0] = lb_setup.Items[idx].ToString();
            SetEdit[1] = cbo_ver.SelectedIndex.ToString();
            SetEdit[2] = tb_code.Text;
            SetEdit[3] = cbo_ProgPath.Text;
            SetEdit[4] = tb_VerNo.Text;
            Form2 f2 = new Form2();
            f2.SetEdit = SetEdit;
            f2.Text = "修改";
            if (f2.ShowDialog() == DialogResult.OK)
            {
                string[] s = new string[5];
                s = f2.GetNew;
                cbo_ver.SelectedIndex = Int32.Parse(s[1]);
                tb_code.Text = s[2];
                cbo_ProgPath.Text = s[3];
                tb_VerNo.Text = s[4];
                lb_setup.SelectedIndex = idx;
                LB_List[lb_setup.Items[idx].ToString()][1] = s[1];
                LB_List[lb_setup.Items[idx].ToString()][2] = s[2];
                LB_List[lb_setup.Items[idx].ToString()][3] = s[3];
                LB_List[lb_setup.Items[idx].ToString()][4] = s[4];
                WriteIni();
            }
            f2.Dispose();
            SelectVerNo();
        }
        private void WriteIni(string xNode, string xKey, object xName)
        {
            int i = 0;
            fc.SaveTemp(fc.ConfigPath, fc.ConfigTmpPath);//備份前存檔
            try
            {
                IConfigSource source = new IniConfigSource(fc.ConfigPath);
                if (source.Configs[xNode] == null)
                {
                    source.Configs.Add(xNode);
                }
                source.Configs[xNode].Set(xKey, xName);
                source.Save();
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.WriteLog(ex.Message, true);
                fc.ReturnTemp(fc.ConfigPath, fc.ConfigTmpPath);
            }
            finally
            {
                fc.DelTemp(fc.ConfigTmpPath);
            }
        }
        private void WriteIni()
        {
            int i = 0;
            fc.SaveTemp(fc.path, fc.TmpPath);//備份前存檔
            try
            {
                IConfigSource source = new IniConfigSource(fc.path);
                if (source.Configs["CustNo"] == null)
                {
                    source.Configs.Add("CustNo");
                }
                else
                {
                    source.Configs.Remove(source.Configs["CustNo"]);
                    source.Configs.Add("CustNo");
                }
                
                foreach (string k in LB_List.Keys)
                {
                    source.Configs["CustNo"].Set("Item" + string.Format("{0:00}", i),
                        fc.iif(LB_List[k][0] == "", "^^", LB_List[k][0]) + "|" +
                    fc.iif(LB_List[k][1] == "", "^^", LB_List[k][1]) + "|" +
                    fc.iif(LB_List[k][2] == "", "^^", LB_List[k][2]) + "|" +
                    fc.iif(LB_List[k][3] == "", "^^", LB_List[k][3]) + "|" +
                    fc.iif(LB_List[k][4] == "", "^^", LB_List[k][4]) );
                    i++;
                }
                source.Save();
                
                /*StreamWriter w = new StreamWriter(@fc.path, false,Encoding.UTF8);
                w.Write("[CustNo]\r\n");
                foreach (string k in LB_List.Keys)
                {
                    w.Write("Item" + string.Format("{0:00}", i) + "=");
                    w.Write(fc.iif(LB_List[k][0] == "", "^^", LB_List[k][0]) + "|");
                    w.Write(fc.iif(LB_List[k][1] == "", "^^", LB_List[k][1]) + "|");
                    w.Write(fc.iif(LB_List[k][2] == "", "^^", LB_List[k][2]) + "|");
                    w.Write(fc.iif(LB_List[k][3] == "", "^^", LB_List[k][3]) + "|");
                    w.Write(fc.iif(LB_List[k][4] == "", "^^", LB_List[k][4]) + "|\r\n");
                    i++;
                }
                w.Close();
                System.IO.File.Delete(fc.TmpPath);
                timer1.Enabled = true;
                Str07.Text = "存檔完成!";*/
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.WriteLog(ex.Message, true);
                fc.ReturnTemp(fc.path, fc.TmpPath);
            }
            finally
            {
                fc.DelTemp(fc.TmpPath);
            }
        }
        /*  private void ReadIni()
          {
              Str07.Text = "檔案讀取中..";
              StreamReader r ;
              if (IsLoadFile)
              {
                  r = new StreamReader(LoadFileName);
              }
              else
              {
                  r = new StreamReader(@fc.path);
              }
              LB_List.Clear();
              lb_setup.Items.Clear();
                       
              List<string> tmp = new List<string>();
              while (!r.EndOfStream)
              {
                  tmp.Add(r.ReadLine());
              }
              r.Close();
              for (int i = 0; i < tmp.Count; i++)
              {
                  string[] spread = fc.Split("=", tmp[i]);
                  string[] s_tmp = fc.Split(";", spread[1]);
                  string[] s = new string[4];
                  for (int j=0; j < s.Length; j++)
                  {
                      if (j < s_tmp.Length)
                      {
                          s[j] = (fc.iif(s_tmp[j] == "^^", "", s_tmp[j])).ToString();
                      }
                      else
                      {
                          s[j] = "";
                      }    
                  }
                  LB_List.Add(s[0],s);
                  lb_setup.Items.Add(s[0]);
              }
              timer1.Enabled = true;
              Str07.Text = "檔案讀取完成!";
              IsLoadFile = false;
          }*/
        private void ReadIni()
        {
            Str07.Text = "檔案讀取中..";
            List<string[]> mValue = new List<string[]>();
 
            if (IsLoadFile)
                mValue = fc.ReadVerTransIni(LoadFileName);
            else
                mValue = fc.ReadVerTransIni(fc.path);

            LB_List.Clear();
            lb_setup.Items.Clear();

            for (int i = 0; i < mValue.Count; i++)
            {
                LB_List.Add(mValue[i][0], mValue[i]);
                lb_setup.Items.Add(mValue[i][0]);
            }            

            timer1.Enabled = true;
            Str07.Text = "檔案讀取完成!";
            IsLoadFile = false;

        }
        private void LoadCurrentInfo()
        {
            RegistryKey start = Registry.LocalMachine;
            RegistryKey programName = start.OpenSubKey(fc.keyPathString, true);
            if (programName != null)
            {
                try
                {
                    lbl_Cur_Code.Text = programName.GetValue("CTICustNo").ToString();
                    tb_code.Text = lbl_Cur_Code.Text;
                }
                catch (System.Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message);
                }
                try
                {
                    Color c = new Color();
                    int m = Int32.Parse(programName.GetValue("MODULE_ID").ToString());
                    lbl_Cur_Ver.Text = GetPosTypeName(m, out c);
                    lbl_Cur_Ver.ForeColor = c;
                    cbo_ver.SelectedIndex = m;
                }
                catch (System.Exception ex)
                {
                    cbo_ver.SelectedIndex = 0;
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage("機碼所登錄的值不在0~2之間，請重新設定!!\r\n" + ex.Message);
                }
                try
                {
                    lbl_Cur_ProgPath.Text = programName.GetValue("ProgPath").ToString();
                    cbo_ProgPath.Text = lbl_Cur_ProgPath.Text;
                }
                catch (System.Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message);
                }
            }
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            lbl_Cur_VerNo.Text = source.Configs["CurrenInfo"].GetString("VerNo", "");
            
            
            bool FindName = false;
            foreach (string s in LB_List.Keys)
            {
                if (NowSetupName == LB_List[s][0])
                {
                    lbl_Cur_Name.Text = LB_List[s][0];
                    FindName = true;
                    break;
                }
            }

            if (!FindName)
            {
                lbl_Cur_Name.Text = "";
            }
            for (int i = 0; i < lb_setup.Items.Count; i++)
            {
                if (lb_setup.Items[i].ToString() == lbl_Cur_Name.Text)
                {
                    lb_setup.SelectedIndex = i;
                    break;
                }
            }
            timer1.Enabled = true;
            if (lb_setup.SelectedIndex != -1)
            {
                Str07.Text = "已載入登錄檔資訊<<" + lb_setup.GetItemText(lb_setup.SelectedIndex) + ">>!";
            }
            else
                Str07.Text = "已載入登錄檔資訊!";
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            string[] s = new string[5];
            Form2 f2 = new Form2();
            f2.SetList = lb_setup;
            if (btnNewSetValue)
            {
                f2.SetSetupName = f2Setupname;
                f2.SetVerNo = f2SetupVerno;
                f2.Setcode = f2Setcode;
            }
            f2.Text = "新增";
            if (f2.ShowDialog() == DialogResult.OK)
            {
                s = f2.GetNew;
                lb_setup.Items.Add(s[0]);
                cbo_ver.SelectedIndex = Int32.Parse(s[1]);
                tb_code.Text = s[2];
                cbo_ProgPath.Text = s[3];
                tb_VerNo.Text = s[4];
                LB_List.Add(s[0], s);
                FCopyName = s[0];
                //lb_setup.SelectedIndex = lb_setup.Items.Count - 1;
                WriteIni();
            }
            f2.Dispose();
            SelectVerNo();
        }
        private void btn_F2_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(cbo_ProgPath.Text))
            {
                fbd01.SelectedPath = cbo_ProgPath.Text;
            }
            else
            {
                fbd01.SelectedPath = "C:\\";
            }

            if (fbd01.ShowDialog() == DialogResult.OK)
            {
                cbo_ProgPath.Text = fbd01.SelectedPath;
            }
        }
        private void 版本說明ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*fc.ShowBoxMessage("1.0.0.9".PadRight(13)+"修改目前版本資訊的標準版顏色為深藍色\n"+
                            "".PadRight(20) + "修改版本為空白時寫入REG錯誤的BUG，增加寫入前判斷版本不可空白\n" +
                            "1.0.0.10".PadRight(12) + "新增判別 32/64 位元PC 機碼路徑\n" +
                            "1.0.0.11".PadRight(12) + "新增ProgPath程式路徑，可自由變更路徑及設定各代號使用路徑\n" +
                            "1.0.0.12".PadRight(12) + "預設版本及路徑的DEF值\n" +
                            "1.0.0.13".PadRight(12) + "增加MENUBAR，增加資訊->版本說明\n" +
                            "1.0.0.14".PadRight(12) + "設定檔INI位置變更為:我的文件\\VerTrans\\VerTrans.ini\n" +
                            "".PadRight(20) + "若程式在該路徑找不到VerTrans.ini，則會詢問是否新增\n"+
                            "".PadRight(20) + "修正個案代號為空值時，資料帶入的錯誤\n"
                            
                            ,"版本更新說明");*/
            //fc.ShowBoxMessage(Properties.Resources.VerInfo, "版本更新說明");
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void 離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 讀取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (fc.isDirectory(fc.DirVerTrans))
                ofd01.InitialDirectory = fc.DirVerTrans;
            else
                ofd01.InitialDirectory = fc.Mydocument;

            ofd01.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";

            if (ofd01.ShowDialog() == DialogResult.OK)
            {
                IsLoadFile = true;
                LoadFileName = ofd01.InitialDirectory + "\\" + ofd01.SafeFileName;
            }
            if (ofd01.SafeFileName != "VerTrans.ini")
            {
                fc.ShowBoxMessage("請選擇 <<VerTrans.ini>> !", "錯誤");
                return;
            }
            ReadIni();
            LoadCurrentInfo();*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Str07.Text = "";
            timer1.Enabled = false;
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void LoadVerNoForCbo(string ConfigName)
        {
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            string[] Tmp = source.Configs[ConfigName].GetString("No").Split('|');
            string Tmp1 = source.Configs[ConfigName].GetString("Default", "");

            if (Tmp1 != "ALL")                        
                tb_VerNo.Text = Tmp1;
            //NowSetupName = s;

            cbo_VerNo.Properties.Items.Clear();

            for (int i = 0; i < Tmp.Length; i++)
            {
               /* if(Tmp[i]=="^^")
                    cbo_VerNo.Properties.Items.Add("");
                else*/
                    cbo_VerNo.Properties.Items.Add(Tmp[i]);
            }

            if (Tmp1.Trim() != "")
            {
                for (int i = 0; i < cbo_VerNo.Properties.Items.Count; i++)
                {
                    if (cbo_VerNo.Properties.Items[i].ToString() == Tmp1)
                    {
                        cbo_VerNo.SelectedIndex = i;
                        break;
                    }
                }   
            }
           
        }
        private void LoadSetupForCbo(string ConfigName)
        {
            cbo_ProgPath.Items.Clear();
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            if (source.Configs[ConfigName]==null)
            {
                return;
            }
            string[] Tmp = source.Configs[ConfigName].GetValues();
            string[] Tmp1 = source.Configs["SetupName"].GetValues();
            string s = "";
            
            for (int i = 0; i < Tmp.Length; i++)
                cbo_ProgPath.Items.Add(Tmp[i]);

            if (Tmp1.Length != 0)
                s = Tmp1[0];

            tb_SetupName.Text = s;
            NowSetupName = s;           
        }
        private void LoadAccount()
        {
            List<string> tmplist = fc.LoadConfigIni("Account");
            string ip = "";
            string id = "";
            string pw = "";
            for (int i = 0; i < tmplist.Count; i++)
            {
                /*   if (i%2==0)
                   {
                       ip = tmplist[i];
                   }
                   else
                   {*/
                string[] tmp0 = fc.Split("|", tmplist[i]);
                ip = tmp0[0];
                id = tmp0[1];
                pw = fc.desDecryptBase64(tmp0[2]);
                string[] tmp = new string[] { ip + "|" + id + "|" + pw };
                //string[] tmp = new string[]{tmplist[i]};
                AccountList.Add(ip, tmp);
                //  }
            }
        }
        private void WriteConfigIni()
        {
            //IConfigSource source = new IniConfigSource(fc.ConfigPath);
            string[] xValue = new string[cbo_ProgPath.Items.Count];
            for (int i = 0; i < cbo_ProgPath.Items.Count; i++)
            {
                xValue[i] = cbo_ProgPath.Items[i].ToString();
            }
            string xVerNo = "";
            for (int i = 0; i < cbo_VerNo.Properties.Items.Count; i++)
            {
                /*if (cbo_VerNo.Properties.Items[i].ToString() == "")                
                    xVerNo += "^^";
                else*/
                    xVerNo += cbo_VerNo.Properties.Items[i].ToString() ;

                if (i + 1 < cbo_VerNo.Properties.Items.Count)
                    xVerNo += "|";                
            }
            if (fc.IsOldVersion())
            {
                版本說明ToolStripMenuItem1.PerformClick();
            }            
            fc.WriteConfigIni1(tb_SetupName.Text, xValue,
                               new string[]{tp06_ID.Text,tp06_PW.Text},
                                xVerNo,
                                tb_VerNo.Text,
                                tb_VerNo.Text,
                                //lbl_Cur_VerNo.Text,
                                cbo_VerNo.Text,
                                TP04_LoadPath,
                                tp7_UserName,
                                tp06_PW.PasswordChar.ToString(),
                                chkUpdatePI.Checked,
                                new string[]{edPOSID.Text,edPOSPW.Text},
                                barCheckItem1.Checked,
                                barCheckItem2.Checked); //20160301 add  by Erric for 20160301001 
        }
        /*
        private void WriteConfigIni()
        {
            //dim
            List<string> mList = fc.ConfigIni();

            fc.SaveTemp(fc.ConfigPath, fc.ConfigTmpPath);//備份前存檔
            try
            {
                //dim
                StreamWriter w = new StreamWriter(@fc.ConfigPath, false);
                bool StartWriteVersion = false;
                bool OldVersion = false;
                foreach (string k in mList)
                {
                    if (StartWriteVersion)
                    {
                        if (k=="")
                        {
                            OldVersion = true;
                            continue;
                        }
                        string[] s = fc.Split("=", k);
                        if (k != fc.GetVersion())
                        {
                            string[] v1 = fc.Split(".", s[1]);
                            string[] v2 = fc.Split(".", fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());
                            for (int i = 0; i < v1.Length;i++ )
                            {
                                if (Int32.Parse(v1[i]) < Int32.Parse(v2[i]))
                                {
                                    OldVersion = true;
                                    break;
                                }
                            }
                        }
                        StartWriteVersion = false;
                        break;
                    }

                    if (k == "--ProductVersion")
                    {
                        StartWriteVersion = true;
                        continue;
                    }
                }
                w.WriteLine("--ProductVersion");
                w.WriteLine("Version=" + fc.iif(fc.GetVersion() == "開發程式階段", "1.0.0.1", fc.GetVersion()).ToString());
                w.WriteLine("");
                w.WriteLine("--SetupPath");
                for (int i = 0; i < cbo_ProgPath.Items.Count;i++ )
                {
                    w.Write("Path" + string.Format("{0:00}", i) + "=");
                    w.Write(cbo_ProgPath.Items[i].ToString() + "\r\n");
                }
                if (cbo_ProgPath.Items.Count==0)
                {
                    w.WriteLine(@"Path00=C:\COSMOS_POS");
                }
                w.WriteLine("");
                w.Close();
                System.IO.File.Delete(fc.ConfigTmpPath);

                if (OldVersion)
                {
                    版本說明ToolStripMenuItem1.PerformClick();
                }
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage("存檔失敗!!\r\n" + ex.Message);
                fc.ReturnTemp(fc.ConfigPath, fc.ConfigTmpPath);
            }
        }*/

        private void Form1_SizeChanged(object sender, EventArgs e)
        {/*
            if (this.WindowState == FormWindowState.Minimized)
            {
                hwnd = (int)this.Handle;
                this.notifyIcon1.Visible = true;
                this.Hide();
            }*/
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //讓Form再度顯示，並寫狀態設為Normal
            this.Visible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.TopMost = false;
        }

        private void Net01Code_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string dd = string.Format("{0:dd}", dt);
            string d = "";
            if (dd.Length > 1)
                d = dd.Substring(1, 1);
            string Lyy = string.Format("{0:yyyy}", dt).Substring(0, 2);
            string Ryy = string.Format("{0:yyyy}", dt).Substring(2, 2);
            string mm = string.Format("{0:MM}", dt);

            string s = d + Lyy + (Int32.Parse(Ryy) + Int32.Parse(mm) + Int32.Parse(dd)).ToString() + d;

            IntPtr ParenthWnd = fc.FindWindow(null, "請輸入");
            Clipboard.SetData(DataFormats.Text, s);
            if (ParenthWnd != IntPtr.Zero)
            {
                fc.SetForegroundWindow(ParenthWnd);
                IntPtr TEdit = fc.FindWindowEx(ParenthWnd, 0, "TEdit", "");
                if (TEdit != IntPtr.Zero)
                {
                    byte[] ch = (UnicodeEncoding.Default.GetBytes(s));
                    char[] cChar = Encoding.ASCII.GetChars(ch);
                    fc.SendKey(cChar);
                    IsFindNet01 = false;
                    //SendKeys.Send("Enter");
                }
            }
            this.notifyIcon1.Visible = true;
            this.Hide();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Tag.ToString() == "4")
            {
                Str07.Text = "將指定的設定檔寫入REG註冊!";
            }
            else if (b.Tag.ToString() == "5")
            {
                Str07.Text = "神秘的小工具~到底是做什麼的呢!?";
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Str07.Text = "";
        }

        private void 登錄編輯程式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("regedit.exe");

        }
        private string GetPosTypeName(int xm, out Color c)
        {
            string s = "";
            c = new Color();
            if (xm == 0)
            {
                c = Color.PaleTurquoise;
                s = "標準版";
            }
            else if (xm == 1)
            {
                c = Color.LightGreen;
                s = "服飾版";
            }
            else if (xm == 2)
            {
                c = Color.LightGoldenrodYellow;
                s = "餐飲版";
            }
            return s;
        }

        private void btn_CopyCur_Code_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, tb_code.Text);
        }

        private void 常用片語設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //20131223 mark form5 拿掉
            /*if (f5 == null)
            {
                f5 = new Form5();
                string[] tmp = new string[] { lbl_Cur_Name.Text, lbl_Cur_Code.Text };
                f5.SetEdit = tmp;
                f5.Show();
                WriteConfigIni();
            }
            else
            {
                if (f5.IsDisposed)
                {
                    f5 = new Form5();
                    string[] tmp = new string[] { lbl_Cur_Name.Text, lbl_Cur_Code.Text };
                    f5.SetEdit = tmp;
                    f5.Show();
                    WriteConfigIni();
                }
                else
                {
                    f5.WindowState = FormWindowState.Normal;
                    //f5.TopMost = true;
                }
            }*/
        }

        private void 常用路徑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            if (f3.ShowDialog() == DialogResult.OK)
            {
                string[] tmp = f3.PathList;
                cbo_ProgPath.Items.Clear();
                for (int i = 0; i < tmp.Length; i++)
                {
                    cbo_ProgPath.Items.Add(tmp[i]);
                }
                WriteConfigIni();
            }
        }

        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsClose = true;
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsClose)
            {
                e.Cancel = true;
                this.components = new System.ComponentModel.Container();
                notifyIcon1.Visible = true;
                this.Visible = false;
            }
            else
            {
                LogOutToServer();
                this.Visible = false;
                Thread.Sleep(2000);    
            }
        }
        /*  protected override void WndProc(ref Message m)
           {
               //双击鼠标
              // IntPtr ParenthWnd = fc.FindWindow(null, "請輸入");
              if (m.HWnd != IntPtr.Zero)
               {
                   if (!IsFindNet01)
                   {
                       IntPtr ParenthWnd = fc.FindWindow(null, "請輸入");
                       if (ParenthWnd != IntPtr.Zero)
                       {
                           IsFindNet01 = true;
                           Net01Code.PerformClick();
                       }
                   }
               }
               if (m.Msg == 0x00A1)
               {
                   if (!IsFindNet01)
                   {
                       IntPtr ParenthWnd = fc.FindWindow(null, "請輸入");
                       if (ParenthWnd != IntPtr.Zero)
                       {
                           IsFindNet01 = true;
                           Net01Code.PerformClick();
                       }
                   }
               }
               else
                   base.WndProc(ref m);
           }*/

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isDebug)
            {
                //timer2.Enabled = false;
            }
            IntPtr NET01 = fc.FindWindow("TfrmCheckPass", "請輸入");
            IntPtr B01 = fc.FindWindow("TfrmDataSyncEF", "輸入密碼");
            if (NET01 != IntPtr.Zero)
            {
                string s = fc.GetPassWord();
                if (NET01 != IntPtr.Zero)
                {
                    fc.SetForegroundWindow(NET01);
                    timer2.Enabled = false;
                    if (fc.GetTEditWin_SetPassword(NET01, s, false) == 0)
                        fc.ShowBoxMessage("沒有找到視窗!", "錯誤");
                    timer2.Enabled = true;
                }
            }
            if (B01 != IntPtr.Zero)
            {
                string s = fc.GetPassWord();
                if (B01 != IntPtr.Zero)
                {
                    timer2.Enabled = false;
                    int Result = fc.GetTEditWin_SetPassword(B01, s, true);
                    if (Result == 0)
                        fc.ShowBoxMessage("沒有找到視窗!", "錯誤");
                    timer2.Enabled = true;
                }
            }
        }
        private void btn_NoLoad_Click(object sender, EventArgs e)
        {
            if (fc.ShowConfirm("是否讀取Config設定檔?", "詢問") == DialogResult.OK)
            {

            }
        }
        private void tb_SetupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            return;
        }

        private void tb_SetupName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            List_ver.Clear();
            lb_msg.Text = "";
            lb_sav.Text = "";
            gb01.BackColor = Color.FromName("Control");
            if (tb_min.Text == "")
            {
                tb_min.Text = "10";
            }
            for (int i = 0; i < lv_ver.Items.Count; i++)
            {
                if (lv_ver.Items[i].Checked)
                {
                    List_ver.Add(lv_ver.Items[i].Text);
                }
            }
            if (List_ver.Count <= 0)
            {
                fc.ShowBoxMessage("沒有選擇版本!!", "錯誤");
            }

            ProView PV = new ProView();
            PV.SetListVer = List_ver;
            PV.SetMinute = tb_min.Text;
            PV.ShowDialog();
            int[] rm = PV.GetResult;
            string filename = PV.GetFilename;
            lb_msg.Text = "檢查完成!\r\r時間異常:" + rm[0] + "\r\n日期異常:" + rm[1];
            lb_sav.Text = "已儲存 我的文件\\Modicheck\\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + filename + ".xls";
            if (rm[0] == 0 && rm[1] == 0)
            {
                gb01.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                gb01.BackColor = Color.FromArgb(255, 192, 192);
                if (fc.ShowConfirm("資料異常!! 是否開啟EXCEL檢視?", "警告") == DialogResult.OK)
                {
                    string savefilename = PV.GetSaveFilename;
                    System.Diagnostics.Process.Start(savefilename);
                }
            }
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_ver.Items.Count; i++)
                lv_ver.Items[i].Checked = true;
        }

        private void btn_Noall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_ver.Items.Count; i++)
                lv_ver.Items[i].Checked = false;
        }

        private string SpaceString(int sum)
        {
            string mStr = "";
            for (int i = 0; i < sum; i++)
            {
                mStr += " ";
            }
            return mStr;
        }
        //20130822 新增 授權
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

        List<string> cbStoreNoList = new List<string>();
        private void gbtnERPTest_Click(object sender, EventArgs e)
        {
            bool xFSecurity_info = false; string xFIntegrated_Security_def = "";
            SqlConnection conn = null;
            string SQL = "";
            string DBType = ""; string ServerIP = ""; string DBName = ""; string UserID = ""; string UserPass = ""; string DSCSYS = "";
            if ((sender as SimpleButton).Name == "gbtnERPTest")
            {
                ServerIP = edERPIP.Text;
                DBName = edERPDBName.Text;
                UserID = edERPDBid.Text;
                UserPass = edERPDBpass.Text;
                DSCSYS = edDSCSys_db.Text;
            }
            else
            {
                ServerIP = edLocalDBIP.Text;
                DBName = edLocalDBName.Text;
                UserID = edLocalDBid.Text;
                UserPass = edLocalDBpass.Text;
            }
            if (UserID == "")
            { xFSecurity_info = false; xFIntegrated_Security_def = FIntegrated_Security_def; }
            else
            { xFSecurity_info = true; xFIntegrated_Security_def = ""; }

            conn = new SqlConnection(makeConnectString(xFSecurity_info, xFIntegrated_Security_def, ServerIP, DBName, UserID, UserPass));
            try
            {
                conn.Open();
                if ((sender as SimpleButton).Name == "gbtnERPTest")
                {
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    SQL = " Select MB001, MB002 from " + edDSCSys_db.Text + ".dbo.DSCMB where MB003 = '" + edERPDBName.Text + "'";
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        edCompanyNo.Text = myDataReader["MB001"].ToString();
                        lbCompanyName.Text = myDataReader["MB002"].ToString();
                        break;
                    }
                    myCommand.Cancel();
                    myDataReader.Close();
                    SQL = "Select MA001, MA002 from " + edERPDBName.Text + ".dbo.WSCMA order by MA001";
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();
                    cbStoreNoList.Clear();
                    cbStoreNo.Properties.Items.Clear();
                    while (myDataReader.Read())
                    {
                        cbStoreNoList.Add(myDataReader["MA002"].ToString().Trim());
                        //cbStoreNo.Properties.Items.Add(myDataReader["MA001"].ToString().Trim());
                        cbStoreNo.Properties.Items.Add(cbStoreNoSetUser(myDataReader["MA001"].ToString().Trim(), myDataReader["MA002"].ToString().Trim()));
                    }
                    if (cbStoreNo.Properties.Items.Count > 0 && cbStoreNo.Text.Trim() == "")
                        cbStoreNo.SelectedIndex = 0;
                    myCommand.Cancel();
                    myDataReader.Close();
                    GetcbStoreNo();
                }


                fc.ShowBoxMessage("連線成功", "訊息");
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString(), "錯誤");
            }
        }
        private string cbStoreNoSetUser(string xID,string xName)
        {
           return FormatUserIP(xID, xName);
        }
        private void cbStoreNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetcbStoreNo();
        }
        private void GetcbStoreNo()
        {
            if (lb_setup_OldIndex != lb_setup_CurrIndex)
            {
                lb_setup_OldIndex = lb_setup_CurrIndex;
                cbStoreNoPOSNo();
                return;
            }
            if (cbStoreNo.SelectedIndex == -1) return;
            cbStoreNo.SelectedIndex = cbStoreNo.Properties.Items.IndexOf(cbStoreNo.Text);
            if (cbStoreNo.SelectedIndex == -1) return;            
            lbStoreName.Text = cbStoreNoList[cbStoreNo.SelectedIndex].ToString();

            string[] s1 = fc.Split("】",cbStoreNo.Text);
            string[] s2 = fc.Split("【", s1[0]);
            cbStoreNo.Text = s2[0];

            cbStoreNoPOSNo();
        }
        private void cbStoreNoPOSNo()
        {
            if(cbStoreNo.Text == "")
            {
                return;
            }
            bool xFSecurity_info = false; string xFIntegrated_Security_def = "";
            SqlConnection conn = null;
            string SQL = "";
            string DBType = ""; string ServerIP = ""; string DBName = ""; string UserID = ""; string UserPass = ""; string DSCSYS = "";
            {
                ServerIP = edERPIP.Text;
                DBName = edERPDBName.Text;
                UserID = edERPDBid.Text;
                UserPass = edERPDBpass.Text;
                DSCSYS = edDSCSys_db.Text;
            }
            if (UserID == "")
            { xFSecurity_info = false; xFIntegrated_Security_def = FIntegrated_Security_def; }
            else
            { xFSecurity_info = true; xFIntegrated_Security_def = ""; }

            conn = new SqlConnection(makeConnectString(xFSecurity_info, xFIntegrated_Security_def, ServerIP, DBName, UserID, UserPass));
            try
            {
                conn.Open();
                //if ((sender as SimpleButton).Name == "gbtnERPTest")
                {
                    edPOSNo.Properties.Items.Clear();
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    SQL = " Select PI002 from POSPI where PI001 = '" + cbStoreNo.Text + "'";
                    myCommand = new SqlCommand(SQL, conn);
                    myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        edPOSNo.Properties.Items.Add(myDataReader["PI002"].ToString());
                    }
                    myCommand.Cancel();
                    myDataReader.Close();

                    if (edPOSNo.Properties.Items.Count > 0 && edPOSNo.Text.Trim() == "")
                        edPOSNo.SelectedIndex = 0;
                }
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString(), "錯誤");
            }
        }

        private void gbtnApply_Click(object sender, EventArgs e)
        {
            if (!SaveIni()) return;
            LoadIni();
            VarInit();
        }

        private bool SaveIni()
        {

            //string filename = @"C:\COSMOS_POS\DSCPOSSetup.ini";
            string filename = @cbo_ProgPath.Text;
            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            if (!fc.isDirectory(filename))
            {
                if (fc.ShowMsg("指定的目錄不存在", "錯誤", "0") == DialogResult.OK)
                    return false;
            }
            filename += @"\DSCPOSSetup.ini";
            StreamReader r = new StreamReader(filename, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                if (File.Exists(filename))
                {
                    //ini.IniWriteValue("lng", "lang1", edERPDBid.Text, filename);
                    //ini.WriteInteger("DB", "ERPDBType", cbERPDBType.Items.IndexOf(cbERPDBType.Text));
                    source.Configs["DB"].Set("ERPDBIP", edERPIP.Text);
                    source.Configs["DB"].Set("ERPDBIP", edERPIP.Text);
                    source.Configs["DB"].Set("ERPDBName", edERPDBName.Text);
                    source.Configs["DB"].Set("ERPDBid", edERPDBid.Text);
                    source.Configs["DB"].Set("ERPDBpass", CEncrypt(edERPDBpass.Text, C_PrivateKey));
                    //UserPass := Decrypt(mPos.ReadString('DB', 'ERPDBpass', Encrypt('', C_PrivateKey)), C_PrivateKey);
                    source.Configs["DB"].Set("DSCSYS_DB", edDSCSys_db.Text);
                    source.Configs["DB"].Set("PortNO", edPort.Text);

                    //ini.WriteInteger("DB", "LocalDBType", cbLocalDBType.Items.IndexOf(cbLocalDBType.Text));
                    source.Configs["DB"].Set("LocalIP", edLocalIP.Text);
                    source.Configs["DB"].Set("LocalDBIP", edLocalDBIP.Text);
                    source.Configs["DB"].Set("LocalDBName", edLocalDBName.Text);
                    source.Configs["DB"].Set("LocalDBid", edLocalDBid.Text);
                    source.Configs["DB"].Set("LocalDBpass", CEncrypt(edLocalDBpass.Text, C_PrivateKey));

                    source.Configs["DB"].Set("CompanyNo", edCompanyNo.Text);
                    source.Configs["DB"].Set("CompanyName", lbCompanyName.Text);
                    source.Configs["DB"].Set("StoreNo", cbStoreNo.Text);
                    source.Configs["DB"].Set("StoreName", lbStoreName.Text);
                    source.Configs["DB"].Set("POSNo", edPOSNo.Text);

                    source.Configs["DB"].Set("SelfDefStamp", fc.GetStrFromBool(ckSelfDefStamp.Checked));                    
                    source.Configs["DB"].Set("StampStoreName", edStampStoreName.Text);
                    source.Configs["DB"].Set("StampManager", edStampManager.Text);
                    source.Configs["DB"].Set("StampCompanyName", edStampCompanyName.Text);
                    source.Configs["DB"].Set("StampCompanyAdd", edStampCompanyAdd.Text);
                    source.Configs["DB"].Set("StampCompanyPhone", edStampCompanyPhone.Text);
                    source.Configs["DB"].Set("StampUniID", edStampUniID.Text);

                    if (barCheckItem1.Checked)
                    {
                        source.Configs["POS"].Set("ShowTAXMB", "1");
                        source.Configs["POS"].Set("AutoLogin", "1");
                    }
                    source.Configs["POS"].Set("LoginID", edPOSID.Text);
                    source.Configs["POS"].Set("LoginPW", edPOSPW.Text);
                    
                    /*source.Configs["SCANNER_A"].Set("BaudRate", 6);
                    source.Configs["SCANNER_A"].Set("DataBits", 3);
                    source.Configs["SCANNER_A"].Set("StopBits", 0);
                    source.Configs["SCANNER_A"].Set("ParityCheck", 0);

                    ini.WriteInteger("SCANNER_B", "BaudRate", 6);
                    ini.WriteInteger("SCANNER_B", "DataBits", 3);
                    ini.WriteInteger("SCANNER_B", "StopBits", 0);
                    ini.WriteInteger("SCANNER_B", "ParityCheck", 0);
                    
                    ini.WriteBool("NetUpdate", "UseFTP", rbFTP.Checked);
                    ini.WriteString("NetUpdate", "FTPIP", edFTPIP.Text + ":" + edFTPPort.Text);
                    ini.WriteString("NetUpdate", "FTPUserName", edFTPUserName.Text);
                    ini.WriteString("NetUpdate", "FTPPwd", Encrypt(edFTPPwd.Text, C_PrivateKey));
                    ini.WriteBool("NetUpdate", "UseNetPath", rbNetPath.Checked);
                    ini.WriteString("NetUpdate", "NetPath", edNetPath.Text);
                    ini.WriteString("NetUpdate", "NetDrive", edNetDrive.Text);
                    ini.WriteString("NetUpdate", "NetUserName", edNetUserName.Text);
                    ini.WriteString("NetUpdate", "NetPwd", Encrypt(edNetPwd.Text, C_PrivateKey));

                    ini.WriteInteger("NetUpdate", "POSType", rgPOSType.ItemIndex);
                    */
                    StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
                    ((IniConfigSource)source).Save(writer);
                }
                r.Close();
                r.Dispose();
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            return true;
        }

        private void CreateDSCPOSSETUP_INI(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
                try
                {
                    StreamWriter w = new StreamWriter(filename, false, Encoding.Default);
                    string[] sr = fc.Split("\r\n", Properties.Resources.DSCPOSSETIP_INI);
                    for (int i = 0; i < sr.Length; i++)
                    {
                        w.WriteLine(sr[i]);
                    }
                    w.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("存檔失敗!!\r\n" + ex.Message);
                }
            }
        }
        private bool LoadIni()
        {
            //string filename = @"C:\COSMOS_POS\DSCPOSSetup.ini";
            string filename = @cbo_ProgPath.Text;
            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            if (!fc.isDirectory(filename))
            {
                fc.ShowMsg("指定的目錄不存在", "錯誤", "0");
                return false;
            }
            filename += @"\DSCPOSSetup.ini";

            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            CreateDSCPOSSETUP_INI(filename);
            StreamReader r = new StreamReader(filename, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            fc.SetAliasForNini(source);
            try
            {
                if (source.Configs["DB"] != null)
                {
                    //with Pub.gERPDBInfo do
                    {
                        //SetPubValue(dpnRec,["gERPDBInfo","DBType"]  ,TDBType(ini.ReadInteger("DB", "ERPDBType", 0)));
                        //gERPDBInfo.DBType=ini.ReadInteger("DB", "ERPDBType", 0)));

                        gERPDBInfo.ServerIP = source.Configs["DB"].GetString("ERPDBIP", "");
                        gERPDBInfo.DBName = source.Configs["DB"].GetString("ERPDBName", "");
                        gERPDBInfo.UserID = source.Configs["DB"].GetString("ERPDBid", "");
                        gERPDBInfo.UserPass = CDecrypt(source.Configs["DB"].GetString("ERPDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                        gERPDBInfo.DSCSYS = source.Configs["DB"].GetString("DSCSYS_DB", "DSCSYS");
                    }
                    edPort.Text = source.Configs["DB"].GetString("PortNO", "211");

                    //with Pub.gLocalDBInfo do
                    {
                        //SetPubValue(dpnRec,["gLocalDBInfo.DBType"]  ,TDBType(ini.ReadInteger("DB", "LocalDBType", 0)));
                        //gLocalDBInfo.DBType=ini.ReadInteger("DB", "LocalDBType", 0));
                        gLocalDBInfo.ServerIP = source.Configs["DB"].GetString("LocalDBIP", fc.GetLocalIP());
                        gLocalDBInfo.DBName = source.Configs["DB"].GetString("LocalDBName", "COSMOS_POS");
                        gLocalDBInfo.UserID = source.Configs["DB"].GetString("LocalDBid", "");
                        gLocalDBInfo.UserPass = CDecrypt(source.Configs["DB"].GetString("LocalDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                    }
                    edLocalIP.Text = source.Configs["DB"].GetString("LocalIP", fc.GetLocalIP());

                    //with Pub.gParam do
                    {
                        gParam.BAS_CompNo = source.Configs["DB"].GetString("CompanyNo", "");
                        gParam.BAS_CompShortName = source.Configs["DB"].GetString("CompanyName", "");
                        gParam.BAS_StoreNo = source.Configs["DB"].GetString("StoreNo", "");
                        gParam.BAS_StoreShortName = source.Configs["DB"].GetString("StoreName", "");
                        gParam.BAS_POSNO = source.Configs["DB"].GetString("POSNo", "");
                        gParam.BAS_Stamp_StoreName = source.Configs["DB"].GetString("StampStoreName", "");
                        gParam.BAS_Stamp_Manager = source.Configs["DB"].GetString("StampManager", "");
                        gParam.BAS_Stamp_CompName = source.Configs["DB"].GetString("StampCompanyName", "");
                        gParam.BAS_Stamp_CompAddress = source.Configs["DB"].GetString("StampCompanyAdd", "");
                        gParam.BAS_Stamp_CompPhone = source.Configs["DB"].GetString("StampCompanyPhone", "");
                        gParam.BAS_Stamp_CompUniID = source.Configs["DB"].GetString("StampUniID", "");
                    }
                    bool mBool = ckSelfDefStamp.Checked;
                    bool mBool1 = source.Configs["DB"].GetBoolean("SelfDefStamp", false);
                    if (mBool != mBool1) ckSelfDefStamp_CheckedChanged(ckSelfDefStamp, null);
                }

                if (source.Configs["POS"] != null)
                {
                    gParam.BAS_LoginID = source.Configs["POS"].GetString("LoginID", "");
                    gParam.BAS_LoginPW = source.Configs["POS"].GetString("LoginPW", "");
                }

                /*
                rbFTP.Checked      = ini.ReadBool("NetUpdate", "UseFTP", true);
                edFTPIP.Text       = ini.ReadString("NetUpdate", "FTPIP", "");
                edFTPPort.Text     = copy(edFTPIP.Text, pos(":", edFTPIP.Text) + 1, length(edFTPIP.Text));
                edFTPIP.Text       = copy(edFTPIP.Text, 1, pos(":", edFTPIP.Text) - 1);
                edFTPUserName.Text = ini.ReadString("NetUpdate", "FTPUserName", "");
                edFTPPwd.Text      = Decrypt(ini.ReadString("NetUpdate", "FTPPwd", Encrypt("", C_PrivateKey)), C_PrivateKey);

                rbNetPath.Checked  = ini.ReadBool("NetUpdate", "UseNetPath", false);
                edNetDrive.Text    = ini.ReadString("NetUpdate", "NetDrive", "");
                edNetUserName.Text = ini.ReadString("NetUpdate", "NetUserName", "");
                edNetPwd.Text      = Decrypt(ini.ReadString("NetUpdate", "NetPwd", Encrypt("", C_PrivateKey)), C_PrivateKey);
                edNetPath.Text     = ini.ReadString("NetUpdate", "NetPath", "");
               
                UpdateModeCheck;

                rgPOSType.ItemIndex = ini.ReadInteger("NetUpdate", "POSType", 0);
                */
                r.Close();
                r.Dispose();
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            return true;
        }

        private void ckSelfDefStamp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSelfDefStamp.Checked)
            {
                edStampStoreName.Text = lbStoreName.Text;
                edStampCompanyName.Text = lbCompanyName.Text;
            }

            edStampStoreName.Enabled = ckSelfDefStamp.Checked;
            edStampManager.Enabled = ckSelfDefStamp.Checked;
            edStampCompanyName.Enabled = ckSelfDefStamp.Checked;
            edStampCompanyAdd.Enabled = ckSelfDefStamp.Checked;
            edStampCompanyPhone.Enabled = ckSelfDefStamp.Checked;
            edStampUniID.Enabled = ckSelfDefStamp.Checked;
        }

        private void VarInit()
        {
            //cbERPDBType.Text = cbERPDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gDownLoadDBInfo.DBType'))];
            //cbERPDBType.Text = cbERPDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gERPDBInfo.DBType'))];            
            edERPIP.Text = gERPDBInfo.ServerIP;
            edERPDBName.Text = gERPDBInfo.DBName;
            edERPDBid.Text = gERPDBInfo.UserID;
            edERPDBpass.Text = gERPDBInfo.UserPass;
            edDSCSys_db.Text = gERPDBInfo.DSCSYS;

            //cbLocalDBType.Text = cbLocalDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gLocalDBInfo.DBType'))];
            edLocalDBIP.Text = gLocalDBInfo.ServerIP;
            edLocalDBName.Text = gLocalDBInfo.DBName;
            edLocalDBid.Text = gLocalDBInfo.UserID;
            edLocalDBpass.Text = gLocalDBInfo.UserPass;

            edCompanyNo.Text = gParam.BAS_CompNo;
            lbCompanyName.Text = gParam.BAS_CompShortName;
            cbStoreNo.Text = gParam.BAS_StoreNo;
            lbStoreName.Text = gParam.BAS_StoreShortName;
            edPOSNo.Text = gParam.BAS_POSNO;

            edStampStoreName.Text = gParam.BAS_Stamp_StoreName;
            edStampManager.Text = gParam.BAS_Stamp_Manager;
            edStampCompanyName.Text = gParam.BAS_Stamp_CompName;
            edStampCompanyAdd.Text = gParam.BAS_Stamp_CompAddress;
            edStampCompanyPhone.Text = gParam.BAS_Stamp_CompPhone;
            edStampUniID.Text = gParam.BAS_Stamp_CompUniID;

            //edPOSID.Text = gParam.BAS_LoginID;
            //edPOSPW.Text = gParam.BAS_LoginPW;
            //gbtnApply.Enabled= false;
        }
        private void DefaultVarInit()
        {
            //cbERPDBType.Text = cbERPDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gDownLoadDBInfo.DBType'))];
            //cbERPDBType.Text = cbERPDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gERPDBInfo.DBType'))];
            edERPIP.Text = "127.0.0.1";
            edERPDBName.Text = "Leader";
            edERPDBid.Text = "sa";
            edERPDBpass.Text = "123";
            edDSCSys_db.Text = "DSCSYS";
            
            //cbLocalDBType.Text = cbLocalDBType.Items.Strings[Integer(GetPub_I(dpnRec,'gLocalDBInfo.DBType'))];
            edLocalDBIP.Text = "127.0.0.1";
            edLocalDBName.Text = "COSMOS_POS";
            edLocalDBid.Text = "sa";
            edLocalDBpass.Text = "123";
            edLocalIP.Text = "127.0.0.1";

            edCompanyNo.Text = "";
            lbCompanyName.Text = "";
            cbStoreNo.Text = "";
            lbStoreName.Text = "";
            edPOSNo.Text = "";

            edStampStoreName.Text = "";
            edStampManager.Text = "";
            edStampCompanyName.Text = "";
            edStampCompanyAdd.Text = "";
            edStampCompanyPhone.Text = "";
            edStampUniID.Text ="";

            edPOSID.Text = "";
            edPOSPW.Text = "";
        }



        private void gbtnDelAuth_Click(object sender, EventArgs e)
        {
           /* if (fc.ShowConfirm("確定", "刪除授權") != DialogResult.OK)
            {
                return;
            }
            else*/
            {
                try
                {                    
                    LoadCurrAuthInfo();
                    //gbtnApply.PerformClick();
                    //DelAuthorize(edCompanyNo.Text, cbStoreNo.Text, edPOSNo.Text, edERPIP.Text, edPort.Text);                    
                    DelAuthorize(CompanyNo, StoreNo, POSNo, ERPIP, Port);
                    CheckAuth();
                    //SaveTmpAuthInfo();
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
                //gbtnApply.PerformClick();
                /*DelAuthorise(SocketConnection,
                             VarArrayof([edERPIP.Text, edPort.Text]),
                             VarArrayof([GetPub_S(dpnRef,'BAS','BAS_CompNo'), GetPub_S(dpnRef,'BAS','BAS_StoreNo'), GetPub_S(dpnRef,'BAS','BAS_POSNo'), 0]));
                CheckAuth;*/
            }
        }

        private void gbtnAuth_Click(object sender, EventArgs e)
        {
            //gbtnApply.PerformClick();
            try
            {
                fc.CheckB01();//授權之前先關閉B01
                SaveDSCPOSSetupINI();
                //SaveSetupInfo();
                string ms = GOAuthorize(edCompanyNo.Text.Trim(), cbStoreNo.Text, edPOSNo.Text, edERPIP.Text, edPort.Text);
                fc.WriteLog("GOAuthorize:"+ms, true);
                //GOAuthorize(CompanyNo, StoreNo, POSNo, ERPIP, Port);
                CheckAuth();
                //SaveTmpAuthInfo(); 
                try
                {
                    if (File.Exists(cbo_ProgPath.Text + @"\1028.LNG"))
                    {
                        File.Delete(cbo_ProgPath.Text + @"\1028.LNG");
                        tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "1028.LNG 已刪除!\r\n";
                    }
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message);
                }

                WriteLoginAccount(tb_VerNo.Text);                    
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }
        private void CheckAuth()
        {
            try
            {
                string ms = GOCheckAuthorise();
                bool b = false;
                fc.WriteLog("CheckAuth:" + ms, true);
                if (ms.ToUpper() == "TRUE") b = true; else b = false;
                IsAuthor = b;
                picAuth.Visible = IsAuthor;
                gbtnAuth.Visible = !b;


                gbtnDelAuth.Visible = !gbtnAuth.Visible;
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveSetupInfo();
        }
        public bool SaveDSCPOSSetupINI()
        {
            string filename = lbl_Cur_ProgPath.Text + @"\DSCPOSSetup.ini";
            string setupname = "DB";

            CreateDSCPOSSETUP_INI(filename);
            StreamReader r = new StreamReader(filename, Encoding.Default);
            IConfigSource source = new IniConfigSource(r);
            string s = "";

            //IniConfigSource source = new IniConfigSource(r);
            fc.SetAliasForNini(source);
            if (source.Configs[setupname] == null) source.Configs.Add(setupname);
            if (source.Configs["POS"] == null) source.Configs.Add("POS");
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                {
                    
                    source.Configs[setupname].Set("ERPDBIP", edERPIP.Text);
                    source.Configs[setupname].Set("ERPDBName", edERPDBName.Text);
                    source.Configs[setupname].Set("ERPDBid", edERPDBid.Text);
                    source.Configs[setupname].Set("ERPDBpass", CEncrypt(edERPDBpass.Text, C_PrivateKey));
                    source.Configs[setupname].Set("DSCSYS_DB", edDSCSys_db.Text);
                    source.Configs[setupname].Set("PortNO", edPort.Text);

                    source.Configs[setupname].Set("LocalIP", edLocalIP.Text);
                    source.Configs[setupname].Set("LocalDBIP", edLocalDBIP.Text);
                    source.Configs[setupname].Set("LocalDBName", edLocalDBName.Text);
                    source.Configs[setupname].Set("LocalDBid", edLocalDBid.Text);
                    source.Configs[setupname].Set("LocalDBpass", CEncrypt(edLocalDBpass.Text, C_PrivateKey));

                    source.Configs[setupname].Set("CompanyNo", edCompanyNo.Text);
                    source.Configs[setupname].Set("CompanyName", lbCompanyName.Text);
                    source.Configs[setupname].Set("StoreNo", cbStoreNo.Text);
                    source.Configs[setupname].Set("StoreName", lbStoreName.Text);
                    source.Configs[setupname].Set("POSNo", edPOSNo.Text);

                    source.Configs[setupname].Set("SelfDefStamp", fc.GetStrFromBool(ckSelfDefStamp.Checked));
                    source.Configs[setupname].Set("StampStoreName", edStampStoreName.Text);
                    source.Configs[setupname].Set("StampManager", edStampManager.Text);
                    source.Configs[setupname].Set("StampCompanyName", edStampCompanyName.Text);
                    source.Configs[setupname].Set("StampCompanyAdd", edStampCompanyAdd.Text);
                    source.Configs[setupname].Set("StampCompanyPhone", edStampCompanyPhone.Text);
                    source.Configs[setupname].Set("StampUniID", edStampUniID.Text);

                    if (barCheckItem1.Checked)
                    {
                        source.Configs["POS"].Set("ShowTAXMB", "1");
                        source.Configs["POS"].Set("AutoLogin", "1");
                    }
                    source.Configs["POS"].Set("LoginID", edPOSID.Text);
                    source.Configs["POS"].Set("LoginPW", edPOSPW.Text);

                    StreamWriter writer = new StreamWriter(filename, false,Encoding.Default);
                    ((IniConfigSource)source).Save(writer);
                }
                r.Close();
                r.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                return false;
            }
        }
        public bool SaveSetupInfo()
        {
            string filename = fc.CustPath;
            string setupname = tb_SetupName.Text;
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            if (source.Configs[setupname] == null) source.Configs.Add(setupname);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                //if (!File.Exists(filename))
                {
                    source.Configs[setupname].Set("ERPDBIP", edERPIP.Text);
                    source.Configs[setupname].Set("ERPDBName", edERPDBName.Text);
                    source.Configs[setupname].Set("ERPDBid", edERPDBid.Text);
                    source.Configs[setupname].Set("ERPDBpass", CEncrypt(edERPDBpass.Text, C_PrivateKey));
                    source.Configs[setupname].Set("DSCSYS_DB", edDSCSys_db.Text);
                    source.Configs[setupname].Set("PortNO", edPort.Text);

                    source.Configs[setupname].Set("LocalIP", edLocalIP.Text);
                    source.Configs[setupname].Set("LocalDBIP", edLocalDBIP.Text);
                    source.Configs[setupname].Set("LocalDBName", edLocalDBName.Text);
                    source.Configs[setupname].Set("LocalDBid", edLocalDBid.Text);
                    source.Configs[setupname].Set("LocalDBpass", CEncrypt(edLocalDBpass.Text, C_PrivateKey));

                    source.Configs[setupname].Set("CompanyNo", edCompanyNo.Text);
                    source.Configs[setupname].Set("CompanyName", lbCompanyName.Text);
                    source.Configs[setupname].Set("StoreNo", cbStoreNo.Text);
                    source.Configs[setupname].Set("StoreName", lbStoreName.Text);
                    source.Configs[setupname].Set("POSNo", edPOSNo.Text);

                    source.Configs[setupname].Set("SelfDefStamp", fc.GetStrFromBool(ckSelfDefStamp.Checked));
                    source.Configs[setupname].Set("StampStoreName", edStampStoreName.Text);
                    source.Configs[setupname].Set("StampManager", edStampManager.Text);
                    source.Configs[setupname].Set("StampCompanyName", edStampCompanyName.Text);
                    source.Configs[setupname].Set("StampCompanyAdd", edStampCompanyAdd.Text);
                    source.Configs[setupname].Set("StampCompanyPhone", edStampCompanyPhone.Text);
                    source.Configs[setupname].Set("StampUniID", edStampUniID.Text);

                    source.Configs[setupname].Set("LoginID", edPOSID.Text);
                    source.Configs[setupname].Set("LoginPW", edPOSPW.Text);

                    source.Save();
                    /* ini.WriteInteger("SCANNER_A", "BaudRate", 6);
                     ini.WriteInteger("SCANNER_A", "DataBits", 3);
                     ini.WriteInteger("SCANNER_A", "StopBits", 0);
                     ini.WriteInteger("SCANNER_A", "ParityCheck", 0);

                     ini.WriteInteger("SCANNER_B", "BaudRate", 6);
                     ini.WriteInteger("SCANNER_B", "DataBits", 3);
                     ini.WriteInteger("SCANNER_B", "StopBits", 0);
                     ini.WriteInteger("SCANNER_B", "ParityCheck", 0);*/
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            LB_List[FCurrStr][0] = tb_SetupName.Text;
            LB_List[FCurrStr][1] = cbo_ver.SelectedIndex.ToString();
            LB_List[FCurrStr][2] = tb_code.Text;
            LB_List[FCurrStr][3] = cbo_ProgPath.Text;
            LB_List[FCurrStr][4] = tb_VerNo.Text;
            WriteIni();
            return true;
        }
        public bool LoadSetupInfo(string xSetupName)
        {
            string filename = fc.CustPath;
            string setupname = xSetupName;
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            if (source.Configs[setupname] == null)
            {
                return false;
            }
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                //with Pub.gERPDBInfo do
                {
                    gERPDBInfo.ServerIP = source.Configs[setupname].GetString( "ERPDBIP", "");
                    gERPDBInfo.DBName = source.Configs[setupname].GetString( "ERPDBName", "");
                    gERPDBInfo.UserID = source.Configs[setupname].GetString( "ERPDBid", "");
                    gERPDBInfo.UserPass = CDecrypt(source.Configs[setupname].GetString( "ERPDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                    gERPDBInfo.DSCSYS = source.Configs[setupname].GetString( "DSCSYS_DB", "DSCSYS");
                }
                edPort.Text = source.Configs[setupname].GetString( "PortNO", "211");

                //with Pub.gLocalDBInfo do
                {
                    gLocalDBInfo.ServerIP = source.Configs[setupname].GetString( "LocalDBIP", fc.GetLocalIP());
                    gLocalDBInfo.DBName = source.Configs[setupname].GetString( "LocalDBName", "COSMOS_POS");
                    gLocalDBInfo.UserID = source.Configs[setupname].GetString( "LocalDBid", "");
                    gLocalDBInfo.UserPass = CDecrypt(source.Configs[setupname].GetString( "LocalDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                }
                edLocalIP.Text = source.Configs[setupname].GetString( "LocalIP", fc.GetLocalIP());

                //with Pub.gParam do
                {
                    gParam.BAS_CompNo = source.Configs[setupname].GetString( "CompanyNo", "");
                    gParam.BAS_CompShortName = source.Configs[setupname].GetString( "CompanyName", "");
                    gParam.BAS_StoreNo = source.Configs[setupname].GetString( "StoreNo", "");
                    gParam.BAS_StoreShortName = source.Configs[setupname].GetString( "StoreName", "");
                    gParam.BAS_POSNO = source.Configs[setupname].GetString( "POSNo", "");
                    gParam.BAS_Stamp_StoreName = source.Configs[setupname].GetString( "StampStoreName", "");
                    gParam.BAS_Stamp_Manager = source.Configs[setupname].GetString( "StampManager", "");
                    gParam.BAS_Stamp_CompName = source.Configs[setupname].GetString( "StampCompanyName", "");
                    gParam.BAS_Stamp_CompAddress = source.Configs[setupname].GetString( "StampCompanyAdd", "");
                    gParam.BAS_Stamp_CompPhone = source.Configs[setupname].GetString( "StampCompanyPhone", "");
                    gParam.BAS_Stamp_CompUniID = source.Configs[setupname].GetString( "StampUniID", "");
                }
                ckSelfDefStamp.Checked = source.Configs[setupname].GetBoolean("SelfDefStamp", false);
                edPOSID.Text = source.Configs[setupname].GetString("LoginID", "");
                edPOSPW.Text = source.Configs[setupname].GetString("LoginPW", "");
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
            return true;
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (IsAuthor)
            {
                try
                {
                    gbtnDelAuth.PerformClick();
                    btn_Ok.PerformClick();
                    gbtnAuth.PerformClick();
                    if (chkUpdatePI.Checked)
                        GOUpdatePOSPI();
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
            }
            else
            {
                try
                {
                    btn_Ok.PerformClick();
                    gbtnAuth.PerformClick();
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
            }
        }
        private void SaveTmpAuthInfo()
        {
            CompanyNo = edCompanyNo.Text;
            StoreNo = cbStoreNo.Text;
            POSNo = edPOSNo.Text;
            ERPIP = edERPIP.Text;
            Port = edPort.Text;
        }
        private bool LoadCurrAuthInfo()
        {
            //string filename = @cbo_ProgPath.Text;
            string filename = @lbl_Cur_ProgPath.Text;
            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            if (!fc.isDirectory(filename))
            {
                fc.ShowMsg("指定的目錄不存在", "錯誤", "0");
                return false;
            }
            filename += @"\DSCPOSSetup.ini";
            StreamReader r = new StreamReader(filename, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                
                CompanyNo = source.Configs["DB"].GetString("CompanyNo", "");
                StoreNo = source.Configs["DB"].GetString("StoreNo", "");
                POSNo = source.Configs["DB"].GetString("POSNo", "");
                ERPIP = source.Configs["DB"].GetString("ERPDBIP", "");
                Port = source.Configs["DB"].GetString("PortNO", "211");
                r.Close();
                r.Dispose();
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            return true;
        }

        private void lb_LocalIP_DoubleClick(object sender, EventArgs e)
        {
            edLocalIP.Text = "127.0.0.1";
        }

        private void lbLocalDBid_DoubleClick(object sender, EventArgs e)
        {
            edLocalDBid.Text = "sa";
            edLocalDBpass.Text = "123";
            edLocalDBIP.Text = "127.0.0.1";
            edLocalDBName.Text = "COSMOS_POS";
            edLocalIP.Text = "127.0.0.1";
        }

        private void lbERPDBid_DoubleClick(object sender, EventArgs e)
        {
            edERPDBid.Text = "sa";
            edERPDBpass.Text = "123";
            edERPIP.Text = "127.0.0.1";
            edERPDBName.Text = "Leader";
            edDSCSys_db.Text = "DSCSYS";
            edPort.Text = "211";
        }

        private void cboA2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void tp04_Class_add(Dictionary<int, ClassNo> xClass, ComboBoxEdit cbo, string[] xStr, string xType)
        {            
            foreach (string v in xStr)
            {
                cbo.Properties.Items.Add("[" + xType + "]" + v);
                string[] mt = fc.Split("]", v);
                string[] mt2 = fc.Split("[", mt[0]);
                string[] mt3 = fc.Split("~", mt2[0]);
                if (mt3.Length > 1)
                    xClass.Add(xClass.Count, new ClassNo(xType, mt3[0].Trim(), mt3[1].Trim()));
            }
        }
        private void tp04_btnLoad_Click(object sender, EventArgs e)
        {
            string VerNodePath = tp04_tdFileName.Text + "\\POSVerNode.ini";            
            if (!File.Exists(VerNodePath))
            {
                fc.ShowBoxMessage("Resource設定檔不存在!!");
                fc.WriteLog("Resource設定檔不存在!!",true);
                return;
            }
            StreamReader r = new StreamReader(VerNodePath, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            string[] mVer = source.Configs["Ver"].GetString("VerInfo").Split('|');
            tp04_cbo00.Properties.Items.Clear();            
            foreach(string v in mVer)
            {
                tp04_cbo00.Properties.Items.Add(v);
            }
            if (tp04_cbo00.Properties.Items.Count <= 0) return;
            tp04_cbo00.SelectedIndex = 0;
            tp04_cboClass.Properties.Items.Clear();
            tp04_Class.Clear();
            dtList.Clear();
            string[] mS = source.Configs["Node"].GetString("System").Split('|');
            string[] mI = source.Configs["Node"].GetString("Infomation").Split('|');
            string[] mE = source.Configs["Node"].GetString("Error").Split('|');
            tp04_Class_add(tp04_Class, tp04_cboClass, mS, "S");
            tp04_Class_add(tp04_Class, tp04_cboClass, mI, "I");
            tp04_Class_add(tp04_Class, tp04_cboClass, mE, "E");
            r.Close();
            r.Dispose();
            /*foreach (string v in mS)
            {
                tp04_cboClass.Properties.Items.Add("[S]" + v);
                string[] mt = fc.Split("]", v);
                string[] mt2 = fc.Split("[", mt[0]);
                string[] mt3 = fc.Split("~", mt2[0]);
                if(mt3.Length > 1)
                tp04_Class.Add(kk++, new ClassNo("S", mt3[0].Trim(), mt3[1].Trim()));
            }
            tp04_cboClass.Properties.Items.Add("[I]" + mI);
            tp04_Class.Add(kk++, new ClassNo("I", "000001","999999"));
            tp04_cboClass.Properties.Items.Add("[E]" + mE);
            tp04_Class.Add(kk++, new ClassNo("E", "000001","999999"));*/

            /*for (int i = 0; i < tp04_cboClass.Properties.Items.Count; i++)
            {
                string[] mt = fc.Split("[", tp04_cboClass.Properties.Items[i].ToString());
                string[] mt2 = fc.Split("]", mt[0]);
                string mt3 = fc.Split("]", mt[1])[0].Trim();
                string[] mt4 = fc.Split("~", mt3);
                if (mt4.Length > 1)
                    tp04_Class.Add(i, new ClassNo(mt2[0], mt4[0].Trim(), mt4[1].Trim()));
                else
                    tp04_Class.Add(i, new ClassNo(mt2[0], mt4[0].Trim() + "001", mt4[0].Trim() + "999"));
            }*/


            for (int i = 0; i < tp04_cbo00.Properties.Items.Count; i++)
            {
                string path = tp04_tdFileName.Text + "\\Resourceconst" + tp04_cbo00.Properties.Items[i].ToString() + ".inc";
                DataTable dt = new DataTable(tp04_cbo00.Properties.Items[i].ToString());
                /*string path = "";
                if (TP04_LoadPath == "") TP04_LoadPath = @"C:\";
                tp04_Ofd01.InitialDirectory = TP04_LoadPath;

                if (tp04_Ofd01.ShowDialog() == DialogResult.OK)
                    tp04_tdFileName.Text = tp04_Ofd01.FileName;
                else
                    return;
            
                path = tp04_tdFileName.Text;
                TP04_LoadPath = Path.GetDirectoryName(path);
                WriteIni("TP04", "LoadPath", TP04_LoadPath);*/
                //File.SetAttributes(path, FileAttributes.Normal);

                RI1 = new RI();
                int Stype = -1;
                string TypeKind = "X";
                bool SBOut = false;
                //第一步：引用FileStream類別
                //引用類別
                FileStream myFile = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                // 第二步：引用StreamReader類別
                //引用StringReader類別
                StreamReader myReader = new StreamReader(myFile, Encoding.Default);
                string tmp = "";
                string tmp2 = "";
                string BaseLine = "";
                string mLine = "";

                int idx = 1;
                int Readeridx = 0;
                ResourceInc.Clear();
                try
                {
                    while (myReader.Peek() >= 0)
                    {
                        BaseLine = myReader.ReadLine();
                        mLine = BaseLine.TrimStart();
                        Readeridx++;
                        string tmpID = "";
                        string tmpStr = "";
                        string markstr = "";
                        bool isMark = false;
                        if (mLine.StartsWith(@"//"))
                        {
                            isMark = true;
                        }

                        string[] ss = fc.Split(@"//", mLine);

                        if (ss.Length <= 0) continue;
                        for (int j = 0; j < ss.Length; j++)
                        {
                            tmp = ss[j].TrimStart();
                            if (tmp.StartsWith("gSLangStr"))// "TGSystemLangCode")
                            {
                                Stype = 1;
                                TypeKind = "S";
                                continue;
                            }
                            else if (tmp.StartsWith("gILangStr"))
                            {
                                Stype = 2;
                                TypeKind = "I";
                                continue;
                            }
                            else if (tmp.StartsWith("gELangStr"))
                            {
                                Stype = 3;
                                TypeKind = "E";
                                continue;
                            }
                            else if (tmp.StartsWith(");"))// == "TGInfoLangCode")
                            {
                                Stype = -1;
                                TypeKind = "X";
                                continue;
                            }

                            if (isMark)
                            {
                                switch (Stype)
                                {
                                    case 1: TypeKind = "SM"; break;
                                    case 2: TypeKind = "IM"; break;
                                    case 3: TypeKind = "EM"; break;
                                    default: TypeKind = "M"; break;
                                }
                            }
                            else
                            {
                                switch (Stype)
                                {
                                    case 1: TypeKind = "S"; break;
                                    case 2: TypeKind = "I"; break;
                                    case 3: TypeKind = "E"; break;
                                    default: TypeKind = "X"; break;
                                }
                            }

                            if (tmp.StartsWith("(LangID"))
                            {
                                tmp = tmp.Trim();
                                tmp = tmp.Substring(1, tmp.Length - 3);

                                string[] tmps = fc.Split(@";", tmp);
                                string tmps2 = fc.Split(@"LangStr:", tmps[1].Trim())[0].TrimStart();
                                string[] tmps3 = fc.Split(@"'", tmps2.Trim());
                                tmpID = fc.Split(@"LangID:", tmps[0].Trim())[0].Trim();
                                if (tmps3.Length > 0)
                                {
                                    for (int k = 0; k < tmps3.Length;k++ )
                                    {
                                        tmpStr += tmps3[k] + "'";
                                    }
                                    tmpStr = tmpStr.Substring(0, tmpStr.Length - 1);
                                    //tmpStr = tmps3[0];
                                    //tmpStr = tmps2.Trim().Substring(1, tmps2.Trim().Length-2);
                                }
                                else
                                    tmpStr = "";

                                if ((j == ss.Length - 1))
                                {
                                    RI1.gELangStr.Add(new RI.TTypeXLine(Readeridx, TypeKind), new RI.TPOSLangStr(tmpID, tmpStr, ""));
                                }
                            }
                            else if (j > 0 && tmpID != "")
                            {
                                markstr += @"//" + tmp;
                                if (j == ss.Length - 1)
                                {
                                    RI1.gELangStr.Add(new RI.TTypeXLine(Readeridx, TypeKind), new RI.TPOSLangStr(tmpID, tmpStr, markstr));
                                    idx += 1;
                                }
                            }
                        }
                    }
                    myReader.Close();
                    myReader.Dispose();
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    return;
                }

                dt.Columns.Add("Index", typeof(string));
                dt.Columns.Add("Line", typeof(int));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("LangID", typeof(string));
                dt.Columns.Add("LangStr", typeof(string));
                dt.Columns.Add("MarkInfo", typeof(string));
                dt.Columns.Add("EditStatu", typeof(string));

                foreach (KeyValuePair<RI.TTypeXLine, RI.TPOSLangStr> k in RI1.gSLangStr)
                {
                    AddRow(dt, k.Key.Lines, k.Key.Type, k.Value.LangID, k.Value.LangStr, k.Value.MarkStr);
                }
                foreach (KeyValuePair<RI.TTypeXLine, RI.TPOSLangStr> k in RI1.gILangStr)
                {
                    AddRow(dt, k.Key.Lines, k.Key.Type, k.Value.LangID, k.Value.LangStr, k.Value.MarkStr);
                }
                foreach (KeyValuePair<RI.TTypeXLine, RI.TPOSLangStr> k in RI1.gELangStr)
                {
                    AddRow(dt, k.Key.Lines, k.Key.Type, k.Value.LangID, k.Value.LangStr, k.Value.MarkStr);
                }
                dtList.Add(dt);
            }

            gridControl1.DataSource = dtList[0];
            if (dtList[0].Rows.Count > 0)
            {
                tp04_btnAdd.Enabled = true;
                tp04_btnEdit.Enabled = true;
                tp04_btnSave.Enabled = true;
            }
            /* for (int i = 0; i < gridView1.Columns.Count-3 ;i++ )
             {
                 gridView1.Columns[i].BestFit();
             }*/
            /*this.gridView1.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            this.gridView1.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效*/
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            tp04_td01.Focus();
        }
        private void AddRow(DataTable xdt, int xLine, string xtype, string id, string str, string mark)
        {
            xdt.Rows.Add(new object[] { xdt.Rows.Count.ToString(), xLine, xtype, id, str, mark });
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            /*  if (e.RowHandle < 0) return;

              tp04_td06.Text = gridView1.GetRowCellValue(e.RowHandle, "Line").ToString();
              tp04_td07.Text = gridView1.GetRowCellValue(e.RowHandle, "Type").ToString();
              tp04_td08.Text = gridView1.GetRowCellValue(e.RowHandle, "LangID").ToString();
              tp04_td09.Text = gridView1.GetRowCellValue(e.RowHandle, "LangStr").ToString();
              tp04_td10.Text = gridView1.GetRowCellValue(e.RowHandle, "MarkInfo").ToString();*/
        }

        private void tp04_td01_EditValueChanged(object sender, EventArgs e)
        {
            string m1 = "", m2 = "", m3 = "", m4 = "", m5 = "";
            if (tp04_td01.Text != "")
            {
                m1 = string.Format("Contains([Line],{0})", "'" + tp04_td01.Text + "'");
            }
            if (tp04_td02.Text != "")
            {
                if (m1 != "") m2 = m2 + " and ";
                m2 += string.Format("Contains([Type],{0})", "'" + tp04_td02.Text + "'");
                m1 += m2;
            }
            if (tp04_td03.Text != "")
            {
                if (m1 != "") m3 = m3 + " and ";
                m3 += string.Format("Contains([LangID],{0})", "'" + tp04_td03.Text + "'");
                m1 += m3;
            }
            if (tp04_td04.Text != "")
            {
                if (m1 != "") m4 = m4 + " and ";
                m4 += string.Format("Contains([LangStr],{0})", "'" + tp04_td04.Text + "'");
                m1 += m4;
            }
            if (tp04_td05.Text != "")
            {
                if (m1 != "") m5 = m5 + " and ";
                m5 += string.Format("Contains([MarkInfo],{0})", "'" + tp04_td05.Text + "'");
                m1 += m5;
            }
            gridView1.ActiveFilterString = m1;
        }

        private void tp04_btnClear_Click(object sender, EventArgs e)
        {
            tp04_td01.Text = "";
            tp04_td02.Text = "";
            tp04_td03.Text = "";
            tp04_td04.Text = "";
            tp04_td05.Text = "";
            tp04_cboClass.SelectedIndex = -1;
            tp04_chkClass.Checked = false;
            gridView1.ActiveFilterString = "";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            tp04_ShowDataInfo(e.FocusedRowHandle);
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                gridView1.FocusedRowHandle = 0;
                tp04_ShowDataInfo(gridView1.FocusedRowHandle);
            }
            else
                gridView1.FocusedRowHandle = -1;


        }
        private void tp04_ShowDataInfo(int mhandel)
        {

            if (mhandel < 0 || mhandel >= gridView1.RowCount) return;
            tp04_td06.Text = gridView1.GetRowCellValue(mhandel, "Line").ToString();
            tp04_td07.Text = gridView1.GetRowCellValue(mhandel, "Type").ToString();
            tp04_td08.Text = gridView1.GetRowCellValue(mhandel, "LangID").ToString();
            tp04_td09.Text = gridView1.GetRowCellValue(mhandel, "LangStr").ToString();
            tp04_td10.Text = gridView1.GetRowCellValue(mhandel, "MarkInfo").ToString();
            tp04_td11.Text = gridView1.GetRowCellValue(mhandel, "Index").ToString();
        }

        private void tp04_chkClass_CheckedChanged(object sender, EventArgs e)
        {
            tp04_cboClass.Enabled = tp04_chkClass.Checked;
            tp04_td01.Enabled = !tp04_chkClass.Checked;
            tp04_td02.Enabled = !tp04_chkClass.Checked;
            tp04_td03.Enabled = !tp04_chkClass.Checked;
            tp04_td04.Enabled = !tp04_chkClass.Checked;
            tp04_td05.Enabled = !tp04_chkClass.Checked;
        }

        private void tp04_lbClass_Click(object sender, EventArgs e)
        {
            tp04_chkClass.Checked = !tp04_chkClass.Checked;
        }

        private void tp04_cboClass_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.State != DrawItemState.Selected)
            {
                //e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(88, 88, 88), LinearGradientMode.Horizontal), e.Bounds);
                e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);

                if (e.Item.ToString().StartsWith("[S]"))
                {
                    e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.FromArgb(128, 128, 255)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else if (e.Item.ToString().StartsWith("[I]"))
                {
                    e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.FromArgb(255, 166, 77)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else if (e.Item.ToString().StartsWith("[E]"))
                {
                    e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.FromArgb(255, 128, 128)), e.Bounds, e.Appearance.GetStringFormat());
                }
            }
            else
            {
                if (e.Item.ToString().StartsWith("[S]"))
                {
                    e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(128, 128, 255), LinearGradientMode.Horizontal), e.Bounds);
                }
                else if (e.Item.ToString().StartsWith("[I]"))
                {
                    e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(255, 166, 77), LinearGradientMode.Horizontal), e.Bounds);
                }
                else if (e.Item.ToString().StartsWith("[E]"))
                {
                    e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(255, 128, 128), LinearGradientMode.Horizontal), e.Bounds);
                }
                e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.White),
                    e.Bounds, e.Appearance.GetStringFormat());
            }
            e.Handled = true;
        }

        private void tp04_cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tp04_cboClass.SelectedIndex < 0) return;
            string m1 = "";
            {
                m1 = string.Format("[LangID] >= {0} and [LangID] <= {1} and ([Type] = {2} or [Type] = {3} ) ",
                    "'" + tp04_Class[tp04_cboClass.SelectedIndex].mStartNo + "'",
                    "'" + tp04_Class[tp04_cboClass.SelectedIndex].mEndNo + "'",
                    "'" + tp04_Class[tp04_cboClass.SelectedIndex].mtype + "'",
                    "'" + tp04_Class[tp04_cboClass.SelectedIndex].mtype + "M'"
                    );
            }

            gridView1.ActiveFilterString = m1;
        }

        private void tp04_btnEdit_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                fc.ShowBoxMessage("沒有選擇資料列!");
                return;
            }
            REditBefore REB = new REditBefore();
            REB.SetType = tp04_td07.Text;
            if (REB.ShowDialog() == DialogResult.OK)
            {
                int mType = REB.GetReturn;
                switch (mType)
                {
                    case 0:
                        //檢查是否有同類型的ID存在
                        if (CheckIsIDExist(tp04_td07.Text.Substring(0,1), tp04_td08.Text))
                        {
                            fc.ShowBoxMessage("不可取消註解，已有相同的LangID存在!!");
                            return;
                        }
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MarkInfo", tp04_td10.Text + REB.GetMark);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Type", tp04_td07.Text.Substring(0, 1));
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "EditStatu", "UnMark");
                        break;
                    case 1:
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MarkInfo", tp04_td10.Text + REB.GetMark);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Type", tp04_td07.Text + "M");
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "EditStatu", "Mark");
                        break;
                    case 2:
                        {
                            ShowRSEditor("EDIT", "修改");
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        private bool CheckIsIDExist(string xType,string xID)
        {
            for (int i = 0; i < gridView1.RowCount;i++ )
            {
                if (gridView1.GetRowCellValue(i, "Type").ToString() == xType &&
                    gridView1.GetRowCellValue(i, "LangID").ToString() == xID)
                {
                    return true;
                }
            }
            return false;
        }
        private void ShowRSEditor(string xtype, string xCaption)
        {
            try
            {

            ResourceEditor RE1 = new ResourceEditor(isDebug, xCaption,tp04_cboClass);
            string oldType = tp04_td07.Text;
            string oldLangID = tp04_td08.Text;
            int NumSelectIndex = -1;
            int NewFuncIndex = -1;
            int mFocusedRowHandle = -1;
            bool IsFunc = false;
            if (gridView1.RowCount > 0)
            {
                mFocusedRowHandle = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Index").ToString());
                IsFunc = mFocusedRowHandle < 100;
            }
                
           
            RE1.SetDataTable = (gridControl1.DataSource as DataTable);
            if (xtype == "EDIT")
            {
                if (IsFunc)//如果修改功能鍵
                {
                    RE1.SearchPlace = 0;
                }
                else
                {
                    RE1.SetEdit = new string[] { tp04_td06.Text,
                                         tp04_td07.Text,
                                         tp04_td08.Text,
                                         tp04_td09.Text,
                                         tp04_td10.Text,
                                         tp04_td11.Text
                                         };
                }                
            }            
            if (RE1.ShowDialog() == DialogResult.OK)
            {
                NumSelectIndex = RE1.GetNumSelectIndex;
                if (xtype == "ADD")
                {
                    IsFunc = NumSelectIndex == 0;
                }
                NewFuncIndex   = RE1.GetNewFuncIndex;
                if (NumSelectIndex == -1 && xtype == "ADD")
                {
                    fc.ShowBoxMessage("新增失敗","錯誤");
                    return ;
                }
                string[] s = RE1.GetEdit;
                if (xtype == "EDIT")
                {
                    if (IsFunc) //修改時若不是功能鍵則全版版跟修
                    {
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MarkInfo", s[6]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "EditStatu", "Mark");
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Type", oldType + "M");
                    }
                    else //修改功能鍵時，只修改單一版本
                    {
                        for (int i = 0; i < dtList.Count; i++)
                        {
                            dtList[i].Rows[mFocusedRowHandle][5] = s[6];
                            dtList[i].Rows[mFocusedRowHandle][6] = "Mark";
                            dtList[i].Rows[mFocusedRowHandle][2] = oldType + "M";
                            dtList[i].AcceptChanges();
                        }
                    }
                }

                if (!IsFunc) //新增功能鍵以外的ID，使用新增
                {                    
                    for (int i = 0; i < dtList.Count; i++)
                    {
                        string[] stmp = (string[])s.Clone();
                        DataRow dr = dtList[i].NewRow();
                        dr = SetDataRow(dr, stmp, oldLangID);
                        int idx = Int32.Parse(stmp[5]);
                        dtList[i].Rows.InsertAt(dr, idx);
                        ReSortLineNo(dtList[i], idx, stmp);
                        dtList[i].AcceptChanges();
                    }                    
                }
                else //新增 1~100 功能鍵的時候 因為有預留欄位 所以不新增，使用修改的方式
                {
                    if (NumSelectIndex == -1 && xtype == "EDIT")
                    {
                        string[] stmp = (string[])s.Clone();                       
                        NumSelectIndex = gridView1.FocusedRowHandle;
                        //NumSelectIndex = mFocusedRowHandle;
                        DataRow dr = (gridControl1.DataSource as DataTable).NewRow();
                        dr = SetDataRow(dr, stmp, oldLangID);
                         int idx = Int32.Parse(stmp[5]);
                        (gridControl1.DataSource as DataTable).Rows.InsertAt(dr, idx);
                        ReSortLineNo((gridControl1.DataSource as DataTable), idx, s);
                    }
                    else
                    {
                        tp04_cboClass.SelectedIndex = NumSelectIndex;
                        /*DataRow dr = (gridControl1.DataSource as DataTable).NewRow();
                         (gridControl1.DataSource as DataTable).Rows.InsertAt(SetDataRow(dr, s, oldLangID), idx);
                         ReSortLineNo((gridControl1.DataSource as DataTable), idx, s);*/
                        //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LangID", s[2]);
                        gridView1.SetRowCellValue(NewFuncIndex, "LangStr", s[3]);
                        gridView1.SetRowCellValue(NewFuncIndex, "MarkInfo", s[4]);
                        gridView1.SetRowCellValue(NewFuncIndex, "EditStatu", "Add");
                        gridView1.SetRowCellValue(NewFuncIndex, "Type", "S");
                    } 
                }               
            }

            gridView1.RefreshData();
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
            //gridControl1.DataSource = dtList[tp04_cbo00.SelectedIndex];
        }
        private DataRow SetDataRow(DataRow dr, string[] s, string oldLangID)
        {
            dr[0] = s[5];
            dr[1] = s[0];
            dr[2] = s[1];
            dr[3] = s[2];
            dr[4] = s[3];
            dr[5] = s[4];
            if (s[2] == oldLangID)
            {
                dr[0] = (Int32.Parse(dr[0].ToString()) + 1).ToString();
                dr[1] = (Int32.Parse(dr[1].ToString()) + 1).ToString();
                dr[6] = "Modi";
                s[5] = dr[0].ToString();
            }
            else
                dr[6] = "Add";

            return dr;
        }
        private void ReSortLineNo(DataTable dt,int idx,string[] s)
        {
            bool isStart = false;
            for (int i = idx + 1; i < dt.Rows.Count; i++)
            {
                /*if (!isStart)
                {
                    if (dt.Rows[i][2].ToString().StartsWith(s[1]))
                    {
                        isStart = true;
                    }
                }
                if (isStart)*/
                {
                    dt.Rows[i][0] = i;
                    dt.Rows[i][1] = (int)(dt.Rows[i][1]) + 1;
                }
            }
        }

        private void tp04_btnAdd_Click(object sender, EventArgs e)
        {
            if ((gridControl1.DataSource as DataTable).Rows.Count <= 0)
            {
                fc.ShowBoxMessage("載入的INC不存在任何資料!");
                return;
            }
            ShowRSEditor("ADD", "新增");
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            tp04_ShowDataInfo(e.RowHandle);
        }

        private void tp04_btnSave_Click(object sender, EventArgs e)
        {
            if ((gridControl1.DataSource as DataTable).Rows.Count <= 0)
            {
                fc.ShowBoxMessage("載入的INC不存在任何資料!");
                return;
            }
            string RCDic = tp04_tdFileName.Text;
            /*string path = "";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Inc Image|*.inc";
            saveFileDialog1.Title = "Save an Inc File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                @path = saveFileDialog1.FileName.ToString();
            }
            else
            {
                return;
            }
            if (RI.SaveToInc((gridControl1.DataSource as DataTable), tp04_cboClass, tp04_cbo00))
            {
                //tp04_tdFileName.Text = @path;
            }*/
            string  yyyy = DateTime.Now.ToString("yyyy");
            string  MM = DateTime.Now.ToString("MM");
            string  dd = DateTime.Now.ToString("dd");
            string[] mfiles = null;
            if (!fc.isDirectory(RCDic + "\\" + yyyy))
            {
                Directory.CreateDirectory((RCDic + "\\" + yyyy));
            }
            if (!fc.isDirectory(RCDic + "\\" + yyyy + "\\" + MM))
            {
                Directory.CreateDirectory((RCDic + "\\" + yyyy + "\\" + MM));
            }
            mfiles = Directory.GetFiles(RCDic + "\\" + yyyy + "\\" + MM);
            for (int j = 0; j < mfiles.Length;j++ )
            {
                mfiles[j] = Path.GetFileName(mfiles[j]);
            }
            int k = 1;
            string mCName = null;
            while (true)            
            {
                mCName = "ResourceConst" + tp04_cbo00.Properties.Items[0].ToString() + "_" + yyyy + MM + dd + string.Format("{0:000}", k) + ".inc";
                if (mfiles.Contains(mCName))
                {
                    k++;
                }
                else
                {
                    //mCName = "\\ResourceConst" + tp04_cbo00.Properties.Items[0].ToString() + "_" + yyyy + MM + dd + string.Format("{0:000}", k) + ".inc";
                    break;
                }
            }
            List<string> RCIniPath = new List<string>();
            List<string> RCIniTmpPath = new List<string>();
            for (int i = 0; i < dtList.Count;i++ )
            {
                string tmppath = RCDic + "\\" + yyyy + "\\" + MM + "\\ResourceConst" + tp04_cbo00.Properties.Items[i].ToString() + "_" + yyyy + MM + dd + string.Format("{0:000}", k) + ".inc";
                string path = RCDic + "\\" + "ResourceConst" + tp04_cbo00.Properties.Items[i].ToString() + ".inc";
                try
                {
                    if (CheckReadonly(path, RCIniTmpPath, RCIniPath))
                        return;                    
                    RCIniPath.Add(path);
                    RCIniTmpPath.Add(tmppath);
                    File.Copy(path, tmppath,true);                    
                    /*FileCopyEx fe = new FileCopyEx();
                    fe.SetType = 2;
                    fe.SetFile = new List<string[]>() { new string[] { path }, new string[] { tmppath } };
                    fe.ShowDialog();*/
                   if (!RI.SaveToInc(dtList[i], tp04_cboClass, path))
                   {
                       File.Copy(tmppath, path, true);                                              
                   }                    
                    //tp04_tdFileName.Text = @path;                    
                }
                catch (System.Exception ex)
                {
                    fc.WriteLog(ex.Message,true);
                    File.Copy(tmppath,path,true);
                }                
            }
            //20150331 mark 每個版本的行數不一定會相同
            /*if (!CheckRCIniCorrect(RCIniPath))
            {
                for (int j = 0; j < RCIniPath.Count; j++)
                {
                    fc.WriteLog("版本資料行數不一，將還原舊資料到INC檔", true);
                    File.Copy(RCIniTmpPath[j], RCIniPath[j], true);
                }
            }
            else*/
            //20150331 mark 每個版本的行數不一定會相同
                fc.ShowBoxMessage("儲存成功!", 600);
        }
        private bool CheckRCIniCorrect(List<string> RCIniPath)
        {
            int totalLines = 0;
            for (int i = 0; i < RCIniPath.Count;i++ )//檢查每個版本的行數
            {
                StreamReader txtRe = new StreamReader(RCIniPath[i],Encoding.Default);//打开当前文件
                string[] txtlist = txtRe.ReadToEnd().Split('\n');//txtlist.Length就是行数                
                txtRe.Close();
                txtRe.Dispose();
                if (i==0)
                {
                    totalLines = txtlist.Length;
                }
                else
                {
                    if (totalLines != txtlist.Length)
                    {
                        fc.ShowBoxMessage("版本資料行數不一，將還原舊資料到INC檔");
                        return false;
                    }
                }                
            }
            return true;
        }
        private bool CheckReadonly(string path ,List<string> RCIniTmpPath, List<string> RCIniPath)
        {
            if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                string str = Path.GetFileName(path);
                fc.ShowBoxMessage(str + "\r\n檔案唯讀!請確認有CHECK OUT再做修改");
                for (int j = 0; j < RCIniPath.Count; j++)//將已編輯過的INC檔還原
                {
                    fc.WriteLog("檔案唯讀，將還原舊資料到INC檔", true);
                    File.Copy(RCIniTmpPath[j], RCIniPath[j], true);
                }
                return true;
            }
            return false;
        }
        public bool SaveDSCPOSSetupInfo()
        {
            string filename = fc.CustPath;
            string setupname = tb_SetupName.Text;
            StreamReader r = new StreamReader(filename, Encoding.Default);
            IniConfigSource source = new IniConfigSource(r);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                //if (!File.Exists(filename))
                {
                    source.Configs[setupname].Set("ERPDBIP", edERPIP.Text);
                    source.Configs[setupname].Set("ERPDBName", edERPDBName.Text);
                    source.Configs[setupname].Set("ERPDBid", edERPDBid.Text);
                    source.Configs[setupname].Set("ERPDBpass", CEncrypt(edERPDBpass.Text, C_PrivateKey));
                    source.Configs[setupname].Set("DSCSYS_DB", edDSCSys_db.Text);
                    source.Configs[setupname].Set("PortNO", edPort.Text);

                    source.Configs[setupname].Set("LocalIP", edLocalIP.Text);
                    source.Configs[setupname].Set("LocalDBIP", edLocalDBIP.Text);
                    source.Configs[setupname].Set("LocalDBName", edLocalDBName.Text);
                    source.Configs[setupname].Set("LocalDBid", edLocalDBid.Text);
                    source.Configs[setupname].Set("LocalDBpass", CEncrypt(edLocalDBpass.Text, C_PrivateKey));

                    source.Configs[setupname].Set("CompanyNo", edCompanyNo.Text);
                    source.Configs[setupname].Set("CompanyName", lbCompanyName.Text);
                    source.Configs[setupname].Set("StoreNo", cbStoreNo.Text);
                    source.Configs[setupname].Set("StoreName", lbStoreName.Text);
                    source.Configs[setupname].Set("POSNo", edPOSNo.Text);

                    source.Configs[setupname].Set("SelfDefStamp", fc.GetStrFromBool(ckSelfDefStamp.Checked));
                    source.Configs[setupname].Set("StampStoreName", edStampStoreName.Text);
                    source.Configs[setupname].Set("StampManager", edStampManager.Text);
                    source.Configs[setupname].Set("StampCompanyName", edStampCompanyName.Text);
                    source.Configs[setupname].Set("StampCompanyAdd", edStampCompanyAdd.Text);
                    source.Configs[setupname].Set("StampCompanyPhone", edStampCompanyPhone.Text);
                    source.Configs[setupname].Set("StampUniID", edStampUniID.Text);

                    /*source.Configs[setupname].Set("ShowTAXMB", "1");
                    source.Configs[setupname.Set("AutoLogin", "1");*/
                    if (barCheckItem1.Checked)
                    {
                        source.Configs[setupname].Set("LoginID", edPOSID.Text);
                        source.Configs[setupname].Set("LoginPW", edPOSPW.Text);
                    }
                    StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
                    ((IniConfigSource)source).Save(writer);
                    /* ini.WriteInteger("SCANNER_A", "BaudRate", 6);
                     ini.WriteInteger("SCANNER_A", "DataBits", 3);
                     ini.WriteInteger("SCANNER_A", "StopBits", 0);
                     ini.WriteInteger("SCANNER_A", "ParityCheck", 0);

                     ini.WriteInteger("SCANNER_B", "BaudRate", 6);
                     ini.WriteInteger("SCANNER_B", "DataBits", 3);
                     ini.WriteInteger("SCANNER_B", "StopBits", 0);
                     ini.WriteInteger("SCANNER_B", "ParityCheck", 0);*/
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            return true;
        }
        public bool LoadDSCPOSSetupInfo(string xSetupName)
        {
            string filename = @"C:\COSMOS_POS\DSCPOSSetup.ini";
            string setupname = xSetupName;
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                //with Pub.gERPDBInfo do
                {
                    gERPDBInfo.ServerIP = source.Configs[setupname].GetString( "ERPDBIP", "");
                    gERPDBInfo.DBName = source.Configs[setupname].GetString( "ERPDBName", "");
                    gERPDBInfo.UserID = source.Configs[setupname].GetString( "ERPDBid", "");
                    gERPDBInfo.UserPass = CDecrypt(source.Configs[setupname].GetString( "ERPDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                    gERPDBInfo.DSCSYS = source.Configs[setupname].GetString( "DSCSYS_DB", "DSCSYS");
                }
                edPort.Text = source.Configs[setupname].GetString( "PortNO", "211");

                //with Pub.gLocalDBInfo do
                {
                    gLocalDBInfo.ServerIP = source.Configs[setupname].GetString( "LocalDBIP", fc.GetLocalIP());
                    gLocalDBInfo.DBName = source.Configs[setupname].GetString( "LocalDBName", "COSMOS_POS");
                    gLocalDBInfo.UserID = source.Configs[setupname].GetString( "LocalDBid", "");
                    gLocalDBInfo.UserPass = CDecrypt(source.Configs[setupname].GetString( "LocalDBpass", CEncrypt("", C_PrivateKey)), C_PrivateKey);
                }
                edLocalIP.Text = source.Configs[setupname].GetString( "LocalIP", fc.GetLocalIP());

                //with Pub.gParam do
                {
                    gParam.BAS_CompNo = source.Configs[setupname].GetString( "CompanyNo", "");
                    gParam.BAS_CompShortName = source.Configs[setupname].GetString( "CompanyName", "");
                    gParam.BAS_StoreNo = source.Configs[setupname].GetString( "StoreNo", "");
                    gParam.BAS_StoreShortName = source.Configs[setupname].GetString( "StoreName", "");
                    gParam.BAS_POSNO = source.Configs[setupname].GetString( "POSNo", "");
                    gParam.BAS_Stamp_StoreName = source.Configs[setupname].GetString( "StampStoreName", "");
                    gParam.BAS_Stamp_Manager = source.Configs[setupname].GetString( "StampManager", "");
                    gParam.BAS_Stamp_CompName = source.Configs[setupname].GetString( "StampCompanyName", "");
                    gParam.BAS_Stamp_CompAddress = source.Configs[setupname].GetString( "StampCompanyAdd", "");
                    gParam.BAS_Stamp_CompPhone = source.Configs[setupname].GetString( "StampCompanyPhone", "");
                    gParam.BAS_Stamp_CompUniID = source.Configs[setupname].GetString( "StampUniID", "");
                }
                ckSelfDefStamp.Checked = source.Configs[setupname].GetBoolean("SelfDefStamp", false);
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            return true;
        }
        private void DELDSCPOSSetupInfo()
        {

        }
        private void tp05_Load127_Click(object sender, EventArgs e)
        {
            //string filename = @fc.Library2003Path;
            string filename = @fc.ConfigPath;
            //string FDefaultPath = "DefaultPath";
            string FDefaultPath = "Library2003";
            bool mUseDefault = true;
            string mCusPath = "";
            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
            tp05_Close.PerformClick();
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                mUseDefault = source.Configs[FDefaultPath].GetBoolean("UseDefault", true);
                mCusPath = source.Configs[FDefaultPath].GetString( "CusPath", "");
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }

            try
            {                
                if (!mUseDefault)
                {
                    tp05_basepath = mCusPath+"\\";// +@"\VerTrans\Cosmos_patch\Cosmos安裝片\";
                }
                else
                {
                    tp05_basepath = @"\\10.40.40.127\Cosmos_patch\Cosmos安裝片\";
                }
                string path = tp05_basepath;
                string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/

                foreach (string item in dirs)
                {
                    if (!item.EndsWith("POS"))
                    {
                        string[] s = fc.Split(@"\", item);
                        tp05_cbo01.Properties.Items.Add(s[s.Length - 1]);//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                    }
                }

                if (tp05_cbo01.Properties.Items.Count > 0)
                {
                    tp05_cbo01.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                return;
            }            
        }

        private void tp05_cbo01_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp05_cbo01.SelectedIndex >= 0)
            {
                tp05_cbo02.Properties.Items.Clear();
                tp05_cbo02.Text = "";
                tp05_cbo03.Properties.Items.Clear();
                tp05_cbo03.Text = "";

                string path = tp05_basepath + tp05_cbo01.Text + @"\Tools\TransDB\";
                string[] dirs = null;
                if (fc.isDirectory(path))
                    dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
                else
                    return;                
                
                if (dirs.Length > 0) //若底下有業態
                {
                    bool IshaveSTD = false;
                    tp05_cbo02.Enabled = true;
                    foreach (string item in dirs)
                    {
                        tp05_cbo02.Properties.Items.Add(Path.GetFileNameWithoutExtension(item).ToUpper());//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中                 
                    }
                    for (int i = 0; i < tp05_cbo02.Properties.Items.Count; i++)
                    {
                        if (tp05_cbo02.Properties.Items[i].ToString().ToUpper() == "STD")
                        {
                            tp05_cbo02.SelectedIndex = i;
                            IshaveSTD = true;
                        }
                    }
                    if (!IshaveSTD)
                        tp05_cbo02.SelectedIndex = 0;
                }
                else //若底下沒有業態
                {
                    tp05_cbo02.Enabled = false;
                    Get2003(path);
                }
                tp05_td18.Focus();
            }
        }

        private void tp05_cbo02_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp05_cbo02.SelectedIndex >= 0)
            {
                tp05_cbo03.Properties.Items.Clear();
                tp05_cbo03.Text = "";
                string path = tp05_basepath + tp05_cbo01.Text + @"\Tools\TransDB\" + tp05_cbo02.Text;
                Get2003(path);
            }
        }
        private void Get2003(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                string[] files = System.IO.Directory.GetFiles(path, "*.sdd");
                foreach (string s in files)
                {
                    string tmp = Path.GetFileNameWithoutExtension(s).ToUpper();
                    if (tmp.EndsWith("2-003"))
                    {
                        tmp = tmp.Substring(0, 3);
                        tp05_cbo03.Properties.Items.Add(tmp);
                    }
                }
            }
            if (tp05_cbo03.Properties.Items.Count > 0)
            {
                tp05_cbo03.SelectedIndex = 0;
            }
        }

        private void tp05_cbo04_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp05_cbo04.SelectedIndex == -1)
            {
                return;
            }
            int j = tp05_cbo04.SelectedIndex; 
            DataTable dt = new DataTable("456");
            dt.Columns.Add("PK", typeof(bool));
            dt.Columns.Add("INDEX", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("LENGTH", typeof(float));
            dt.Columns.Add("INFO", typeof(string));

            for (int i = 0; i < xx.GetRoot()[j].GetRecord().Count; i++)
            {
                dt.Rows.Add(new object[] { 
                    xx.GetRoot()[j].GetRecord()[i].XPK,
                    xx.GetRoot()[j].GetRecord()[i].XIndex,
                    xx.GetRoot()[j].GetRecord()[i].XID,
                    xx.GetRoot()[j].GetRecord()[i].XName,
                    xx.GetRoot()[j].GetRecord()[i].XType,
                    xx.GetRoot()[j].GetRecord()[i].XLength,
                    xx.GetRoot()[j].GetRecord()[i].XInfo});
            }
            gridControl2.DataSource = dt;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.Columns["NAME"].BestFit();

            tp05_td11.Text = xx.GetRoot()[j].XCodeName;
            tp05_td12.Text = xx.GetRoot()[j].XCodeType.Substring(0,1);
            tp05_td12C.Text = FCodeType[Int32.Parse(tp05_td12.Text)-1];
            tp05_chk13.Checked = xx.GetRoot()[j].XIsTransed;
            tp05_td14.Text = xx.GetRoot()[j].XPrimary;
            tp05_td15.Text = xx.GetRoot()[j].XIndex01 == "" ? "" : xx.GetRoot()[j].XIndex01;
            tp05_td16.Text = xx.GetRoot()[j].XIndex02 == "" ? "" : xx.GetRoot()[j].XIndex02;
            tp05_td17.Text = xx.GetRoot()[j].XIndex03 == "" ? "" : xx.GetRoot()[j].XIndex03;
            tp05_Clear.PerformClick();
            tp05_td18.Focus();
        }

        private void tp05_cbo03_SelectedIndexChanged(object sender, EventArgs e)
        {
            tp05_Load2003(false, "");
        }
        private void tp05_Load2003(bool isModi,string xpath)
        {
            try
            {
                string path = "";
                if (!isModi)
                {
                    if (tp05_cbo03.SelectedIndex == -1)
                    {
                        return;
                    }
                    tp05_cbo04.Properties.Items.Clear();
                    xx = new XXX2003();
                    path = tp05_basepath + tp05_cbo01.Text + @"\Tools\TransDB\";
                    if (tp05_cbo02.Enabled)
                    {
                        path += tp05_cbo02.Text + "\\";
                    }
                    path += tp05_cbo03.Text + "2-003.SDD";
                    tp05_cbo01.Enabled = true;
                    tp05_cbo02.Enabled = true;
                    tp05_cbo03.Enabled = true;
                }
                else
                {
                    tp05_cbo04.Properties.Items.Clear();
                    xx = new XXX2003();
                    path = xpath;
                    tp05_cbo01.Enabled = false;
                    tp05_cbo02.Enabled = false;
                    tp05_cbo03.Enabled = false;
                }
                FileStream myFile = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                StreamReader myReader = new StreamReader(myFile, Encoding.Default);
                //List<string> asd = new List<string>();

                string XCode = "";
                string XCodeName = "";
                string XCodeType = "";
                bool XIsTransed = false;
                string XPrimary = "";
                string XIndex01 = "";
                string XIndex02 = "";
                string XIndex03 = "";
                bool XIsPK = false;
                List<string> PK = new List<string>();
                int IsStart = 0;
                int mindex = 0;
                int nindex = 0;
                string[] ms;
                while (myReader.Peek() >= 0)
                {
                    string s = myReader.ReadLine();
                    if (s.StartsWith("檔檔案代碼"))
                    {
                        skk = s;
                    }
                    string[] tt = fc.Split("\t", s);
                    if (tt.Length > 1)
                    {
                        s = "";
                        for (int m = 0; m < tt.Length; m++)
                        {
                            s += tt[m];
                        }
                    }
                    /* if (s.StartsWith("\f"))
                     {
                         XCode = "";
                         XCodeName = "";
                         XCodeType = "";
                         XIsTransed = false;
                         XPrimary = "";
                         XIndex01 = "";
                         XIndex02 = "";
                         XIndex03 = "";
                         IsStart = 0;
                         mindex = mindex + 1;
                         continue;
                     }
                     else*/
                    if (s.StartsWith(@"檔案代碼"))
                    {
                        XCode = "";
                        XCodeName = "";
                        XCodeType = "";
                        XIsTransed = false;
                        XPrimary = "";
                        XIndex01 = "";
                        XIndex02 = "";
                        XIndex03 = "";
                        XIsPK = false;
                        PK.Clear();
                        if (xx.GetRoot().Count > 0)
                        {
                            mindex = mindex + 1;
                        }
                        ms = fc.Split(":", s);
                        if (ms.Length > 1)
                            XCode = ms[1];
                        IsStart = 0;
                    }
                    else if (s.Trim() == "" || s.StartsWith(@"//"))
                    {
                        continue;
                    }
                    if (IsStart == 0)
                    {
                        /* if (s.StartsWith(@"檔案代碼"))
                         {
                             ms = fc.Split(":", s);
                             if (ms.Length > 1)
                                 XCode = ms[1];
                         }
                         else */
                        if (s.StartsWith(@"檔案名稱"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XCodeName = ms[1];
                            XCodeName = fc.Split(":", s)[1];
                        }
                        else if (s.StartsWith(@"類    型"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XCodeType = ms[1];
                        }
                        else if (s.StartsWith(@"轉檔完成"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XIsTransed = ms[1] == "Y";
                        }
                        else if (s.StartsWith(@"PRIMARY"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XPrimary = ms[1];
                            //string[] mpk = fc.Split("+", XPrimary);
                            string[] mpka = fc.Split(" ", XPrimary);
                            string[] mpk = new string[0];
                            if (mpka.Length > 1)
                              mpk = fc.Split("+", mpka[0]);
                            if (XCode=="POSTK")
                            {
                                string a = "";
                                a = "123";

                            }
                            for (int n = 0; n < mpk.Length; n++)
                            {
                                PK.Add(mpk[n]);
                            }
                        }
                        else if (s.StartsWith(@"INDEX01"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XIndex01 = ms[1];
                        }
                        else if (s.StartsWith(@"INDEX02"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XIndex02 = ms[1];
                        }
                        else if (s.StartsWith(@"INDEX03"))
                        {
                            ms = fc.Split(":", s);
                            if (ms.Length > 1)
                                XIndex03 = ms[1];
                        }
                        else if (s.StartsWith(@"0"))
                        {
                            IsStart = 1;
                        }
                        else
                        {
                            continue;
                        }
                    }


                    if (IsStart == 1)
                    {
                        xx.Add(mindex, XCode, XCodeName, XCodeType, XIsTransed, XPrimary, XIndex01, XIndex02, XIndex03);
                        tp05_cbo04.Properties.Items.Add(XCode.ToUpper());//+" ["+XCodeName+"]");
                        XCode = "";
                        XCodeName = "";
                        XCodeType = "";
                        XIsTransed = false;
                        XPrimary = "";
                        XIndex01 = "";
                        XIndex02 = "";
                        XIndex03 = "";

                        IsStart = 2;
                    }
                    if (IsStart == 2)
                    {
                        string[] tmp = fc.Split(" ", s);
                        string s2 = "";
                        if (tmp.Length < 5)
                        {
                            continue;
                        }
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (i == 5)
                            {
                                s2 = tmp[i];
                            }
                            else if (i > 5)
                            {
                                s2 += " " + tmp[i];
                            }
                        }
                        foreach (string mss in PK)
                        {
                            if (tmp[1].Trim() == mss.Trim())
                            {
                                XIsPK = true;
                                break;
                            }
                        }
                        float tmp_4 = 0;
                        bool bb = float.TryParse(tmp[4],out tmp_4);
                        if (!bb)
                        {
                            tmp[4] = "0";
                        }
                        xx.GetRoot()[mindex].Add(tmp[0], tmp[1], tmp[2], tmp[3], float.Parse(tmp[4]), s2, XIsPK);
                        XIsPK = false;
                    }

                }
                myReader.Close();
                myReader.Dispose();
                if (tp05_cbo04.Properties.Items.Count > 0)
                {
                    tp05_cbo04.SelectedIndex = 0;
                }
            }
            catch (System.Exception ex)
            {
                
                fc.ShowBoxMessage(ex.Message.ToString()+ "SKK:"+skk);
                fc.WriteLog(skk, true);
            }

        }

        private void tp05_cbo04_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            try
            {
                if (e.State != DrawItemState.Selected)
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString() + " [" + xx.GetRoot()[e.Index].XCodeName + "]", FontYahei, new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    //e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(220, 232, 166), LinearGradientMode.Horizontal), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString() + " [" + xx.GetRoot()[e.Index].XCodeName + "]", FontYahei, new SolidBrush(Color.White),
                        e.Bounds, e.Appearance.GetStringFormat());
                }
                e.Handled = true;
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

            //Font f = new Font("YaHei Consolas Hybrid",e.Appearance.Font.Size, FontStyle.Bold);

        }
        /*
                     dt.Columns.Add("INDEX", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("LENGTH", typeof(float));
            dt.Columns.Add("INFO", typeof(string));
         */
        private void tp05_td05_EditValueChanged(object sender, EventArgs e)
        {
            string m1 = "", m2 = "", m3 = "", m4 = "", m5 = "", m6 = "";
            if (tp05_td05.Text != "")
            {
                m1 = string.Format("Contains([INDEX],{0})", "'" + tp05_td05.Text + "'");
            }
            if (tp05_td06.Text != "")
            {
                if (m1 != "") m2 = m2 + " and ";
                m2 += string.Format("Contains([TYPE],{0})", "'" + tp05_td06.Text + "'");
                m1 += m2;
            }
            if (tp05_td07.Text != "")
            {
                if (m1 != "") m3 = m3 + " and ";
                string[] tmp = fc.Split(",",tp05_td07.Text);
                for (int i = 0; i < tmp.Length;i++ )
                {
                    m3 += string.Format("Contains([ID],{0})", "'" + tmp[i] + "'") + " or ";
                }
                //m3 += string.Format("Contains([ID],{0})", "'" + tp05_td07.Text + "'");
                if (m3.EndsWith("or "))
                {
                    m3 = m3.Substring(0, m3.Length - 4);
                }
                m1 += m3;
            }
            if (tp05_td08.Text != "")
            {
                if (m1 != "") m4 = m4 + " and ";
                m4 += string.Format("Contains([LENGTH],{0})", "'" + tp05_td08.Text + "'");
                m1 += m4;
            }
            if (tp05_td09.Text != "")
            {
                if (m1 != "") m5 = m5 + " and ";
                m5 += string.Format("Contains([NAME],{0})", "'" + tp05_td09.Text + "'");
                m1 += m5;
            }
            if (tp05_td10.Text != "")
            {
                if (m1 != "") m6 = m6 + " and ";
                m6 += string.Format("Contains([INFO],{0})", "'" + tp05_td10.Text + "'");
                m1 += m6;
            }
            gridView2.ActiveFilterString = m1;
        }

        private void tp05_Clear_Click(object sender, EventArgs e)
        {
            tp05_td05.Text = "";
            tp05_td06.Text = "";
            tp05_td07.Text = "";
            tp05_td08.Text = "";
            tp05_td09.Text = "";
            tp05_td10.Text = "";
            tp05_td18.Text = "";
            gridView2.ActiveFilterString = "";
        }

        private void tp05_Close_Click(object sender, EventArgs e)
        {
            tp05_cbo04.Properties.Items.Clear();
            tp05_cbo03.Properties.Items.Clear();
            tp05_cbo02.Properties.Items.Clear();
            tp05_cbo01.Properties.Items.Clear();
            tp05_cbo04.Text = "";
            tp05_cbo03.Text = "";
            tp05_cbo02.Text = "";
            tp05_cbo01.Text = "";
            tp05_Clear.PerformClick();
            tp05_td11.Text = "";
            tp05_td12.Text = "";
            tp05_chk13.Checked = false;
            tp05_td14.Text = "";
            tp05_td15.Text = "";
            tp05_td16.Text = "";
            tp05_td17.Text = "";
            tp05_td12C.Text = "";
            tp05_td18.Text = "";
            gridControl2.DataSource = null;
            if (xx != null)
            xx.Clear();
        }
        private void tp05_LoadModi_Click(object sender, EventArgs e)
        {
            string path = "";//@"C:\ResourceConst.inc";
            if (tp05_Ofd01.ShowDialog() == DialogResult.OK)
            {
                path = tp05_Ofd01.FileName;
                tp05_Load2003(true, path);
            }
            else
                return;          
        }

        private void tp05_Clear_MouseEnter(object sender, EventArgs e)
        {
            tp05_td05.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td06.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td07.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td08.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td09.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td10.BackColor = Color.FromArgb(255, 128, 128);
            tp05_td18.BackColor = Color.FromArgb(255, 128, 128);        
        }

        private void tp05_Clear_MouseLeave(object sender, EventArgs e)
        {
            tp05_td05.BackColor = Color.White;
            tp05_td06.BackColor = Color.White;
            tp05_td07.BackColor = Color.White;
            tp05_td08.BackColor = Color.White;
            tp05_td09.BackColor = Color.White;
            tp05_td10.BackColor = Color.White;
            tp05_td18.BackColor = Color.White;  
        }

        private void 載入資訊設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TP05_LoadSetup x1 = new TP05_LoadSetup();
            x1.ShowDialog();
            /*            if (cboA.SelectedIndex == -1) return;

            cboB.Visible = true;
            cboB.Text = "";
            cboC.Text = "";
            cboD.Text = "";
            cboC.Visible = false;
            cboD.Visible = false;
            if (cboA.SelectedIndex == 0)
                tbA2.Text = "10.40.40.127";
            else if (cboA.SelectedIndex == 1)
                tbA2.Text = "10.40.140.211";


            if (cboA.SelectedIndex == 0)
            {
                cboB.Items.Clear();
                for (int i = 0; i < fc.cboB1.Length; i++)
                    cboB.Items.Add(fc.cboB1[i]);
                Space = 0;
                //if (AccountList.Count>0)
                if (AccountList.ContainsKey("10.40.40.127"))
                {
                    tb_ID.Text = fc.Split(";", AccountList["10.40.40.127"][0])[1];
                    tb_PW.Text = fc.Split(";", AccountList["10.40.40.127"][0])[2];
                    //tb_PW.Text = fc.desDecryptBase64(fc.Split(";", AccountList["10.40.40.127"][0])[1]);
                }
                else
                {
                    tb_ID.Text = "";
                    tb_PW.Text = "";
                }
                cbo_SavePath.SelectedIndex = 0;
                gb05.Enabled = true;
            }
            else if (cboA.SelectedIndex == 1)
            {
                cboB.Items.Clear();
                for (int i = 0; i < fc.cboB2.Length; i++)
                    cboB.Items.Add(fc.cboB2[i]);
                Space = 1;
                //if (AccountList.Count > 1)
                if (AccountList.ContainsKey("10.40.140.211"))
                {
                    tb_ID.Text = fc.Split(";", AccountList["10.40.140.211"][0])[1];
                    tb_PW.Text = fc.Split(";", AccountList["10.40.140.211"][0])[2];
                }
                else
                {
                    tb_ID.Text = "";
                    tb_PW.Text = "";
                }
                cbo_SavePath.SelectedIndex = -1;
                gb05.Enabled = false;
            }
            cboB.Focus();*/       
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                /*if (!File.Exists(@fc.Config2Path))
                {
                    File.Create(@fc.Config2Path).Close();
                    fc.tp06_WriteDefLoadInfo(@fc.Config2Path);
                }*/
                IniConfigSource source = new IniConfigSource(fc.ConfigPath);

                //IniConfigSource source = new IniConfigSource(@fc.Config2Path);
                //SetupIni ini = new SetupIni();
                //ini.SetFileName(@fc.Config2Path);
                try
                {
                    tp06_ID.Text = source.Configs["TP06"].GetString("ID", "");
                    tp06_PW.Text = fc.desDecryptBase64(source.Configs["TP06"].GetString("PW", ""));
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
            }
        }

        private void tp06_Conn_Click(object sender, EventArgs e)
        {
            tp06_chklist01.Items.Clear();
            tp06_chklist02.Items.Clear();
            tp06_chklist03.Items.Clear();
            tp06_chklist04.Items.Clear();
            tp06_list05.Items.Clear();
            // if (tp06_cboB.SelectedIndex == -1) return;
            string mIP = "10.40.40.127";

            if (tp06_ID.Text == "" || tp06_PW.Text == "")
            {
                fc.ShowBoxMessage("工號及密碼不可空白!!", "錯誤");
               // tp06_cboC.SelectedIndex = -1;
                return;
            }

            try
            {
                fc.ConnNetUse(mIP, tp06_ID.Text, tp06_PW.Text);
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message);
                return;
                //return false;
            }
            string filename = @fc.ConfigPath;
            string FType = "TP06";
            IniConfigSource source = new IniConfigSource(filename);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            string mFileNames = "";
            try
            {
                if (File.Exists(filename))
                {
                    for (int i = 0; i < tp06_list05.Items.Count; i++)
                    {
                        mFileNames += tp06_list05.Items[i] + "|";
                    }
                    source.Configs[FType].Set("ID", tp06_ID.Text);
                    source.Configs[FType].Set("PW", fc.desEncryptBase64(tp06_PW.Text));
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            tp06_cboB.Visible = true;
            tp06_cboC.Visible = false;
            tp06_cboC.Text = "";
            tp06_cboD.Text = "";
            tp06_cboD.Visible = false;


            tp06_cboB.Items.Clear();
            string path = fc.cboA[0];
            string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
            List<string> sss = new List<string>();
            List<string> sss2 = new List<string>();

            foreach (string item in dirs)
            {
                if (item.Contains("POS"))
                {
                    //sss.Add(item);
                    //tp06_cboB.Items.Add(item);
                    string[] xx = fc.Split(@"\", item);
                    sss2.Add(xx[xx.Length - 1]);//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                    tp06_cboB.Items.Add(xx[xx.Length - 1]);
                }
            }

            //tp06_cbo_SavePath.SelectedIndex = 0;            
            //gb05.Enabled = true;
            tp06_cboC.Focus();
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(@fc.Config2Path);
            try
            {
                string[] s = source.Configs["TP06"].GetString("DownLoadPath", "").Split('|');
                if (s.Length > 0) tp06_cbo_SavePath.Items.Clear();
                foreach (string t in s)
                {
                    if(t != "")
                        tp06_cbo_SavePath.Items.Add(t);
                }
                if (tp06_cbo_SavePath.Items.Count > 0)
                {
                    tp06_cbo_SavePath.SelectedIndex = 0;
                }
                if (tp06_cboB.Items.Count > 0)
                {
                    tp06_cboB.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        

        }
        private void tp06_cboB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp06_cboB.SelectedIndex == -1) return;
            tp06_cboC.Visible = true;
            tp06_cboC.Text = "";
            tp06_cboD.Text = "";
            tp06_cboD.Visible = false;

            tp06_cboC.Items.Clear();
            tp06_cboC.Items.Add("PKG");
            tp06_cboC.Items.Add("MODI");
            tp06_cboC.Items.Add("PKG+MODI");
            tp06_cboC.Focus();
            tp06_chklist01.Items.Clear();
            tp06_chklist02.Items.Clear();
            tp06_chklist03.Items.Clear();
            tp06_chklist04.Items.Clear();
            tp06_GetNoCopyList();
            if (tp06_cboC.Items.Count > 0)
            {
                tp06_cboC.SelectedIndex = 0;
            }

            IniConfigSource source = new IniConfigSource(fc.ConfigPath);
            try
            {
                if (source.Configs["Options_POSDL"] != null)
                {
                    if (source.Configs["Options_POSDL"].GetString("UseDownLoadPath", "F") == "T")
                    {
                        FDownLoadPath[0] = source.Configs["Options_POSDL"].GetString("DownLoadPath", "C:\\COSMOS_POS\\");
                    }
                    else
                    {
                        FDownLoadPath[0] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }

            if (FDownLoadPath[0] != "")
            {
                FDownLoadPath[1] = "";
                FDownLoadPath[2] = "";
                FDownLoadPath[3] = "";
                string mTempPath = tp06_cbo_SavePath.Text;
                tp06_cbo_SavePath.Text = "";
                try
                {
                    string[] mPath = fc.Split("對外_", tp06_cboB.Text);
                    mPath = fc.Split("POS", mPath[0]);
                    if (mPath.Length > 1) //GPPOS1.0 -> [0]GP [1]1.0
                    {
                        mPath[1] = mPath[1].Replace(".", "");
                        FDownLoadPath[1] = mPath[0] + mPath[1];
                    }
                    else
                    {
                        FDownLoadPath[1] = mPath[0];
                    }
                    if (tp06_cboC.Text != "")
                    {
                        if (tp06_cboC.SelectedIndex == 0)
                        {
                            FDownLoadPath[2] = "\\" + tp06_cboC.Text + "_" + DateTime.Now.ToString("yyyyMMdd");
                            FDownLoadPath[3] = "";
                        }
                        /*else
                        {
                            FDownLoadPath[2] = "_";
                            if (tp06_cboD.Text != "" && tp06_cboD.Visible)
                            {
                                string[] mPath3 = fc.Split("-", tp06_cboD.Text);
                                FDownLoadPath[3] = mPath3[1] + "_" + DateTime.Now.ToString("yyyyMMdd");
                            }
                        }*/
                        
                    }
                    
                    foreach (string s in FDownLoadPath)
                    {
                        tp06_cbo_SavePath.Text += s;
                    }

                }
                catch (System.Exception ex)
                {
                    fc.ShowMsg(ex.Message, "錯誤", "0");
                    tp06_cbo_SavePath.Text = mTempPath;
                }
            }

            
        }

        private void tp06_CheckIsUnCopyFile(CheckedListBoxControl clb,string path)
        {
            string[] s = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < s.Length; i++)
            {
                clb.Items.Add(Path.GetFileName(s[i]));
                if (!tp06_list05.Items.Contains(Path.GetFileName(s[i])))
                {
                    clb.Items[clb.Items.Count - 1].CheckState = CheckState.Checked;
                    //return true;
                }
            }
            //return false;
        }
        private void tp06_cboC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp06_cboC.SelectedIndex == -1) return;
            tp06_chklist01.Items.Clear();
            tp06_chklist02.Items.Clear();
            tp06_chklist03.Items.Clear();
            tp06_chklist04.Items.Clear();
            if (tp06_cboC.SelectedItem.ToString() != "MODI")
            {
                
                //string path1 = fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString();

                tp06_CheckIsUnCopyFile(tp06_chklist01, fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString());
                tp06_CheckIsUnCopyFile(tp06_chklist02, fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString() + @"\PKG");
                tp06_CheckIsUnCopyFile(tp06_chklist04, fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString() + @"\System");

                /*string[] s =  System.IO.Directory.GetFiles(path1);
                for (int i = 0; i < s.Length;i++ )
                {
                    
                    tp06_chklist01.Items.Add(Path.GetFileName(s[i]));
                    if (!tp06_CheckIsUnCopyFile(Path.GetFileName(s[i])))
                        tp06_chklist01.Items[tp06_chklist01.Items.Count - 1].CheckState = CheckState.Checked;
                }

                path1 = fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString()+@"\PKG";
                string[] s2 = System.IO.Directory.GetFiles(path1);
                for (int i = 0; i < s2.Length; i++)
                {
                    tp06_chklist02.Items.Add(Path.GetFileName(s2[i]));
                }

                path1 = fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString() + @"\System";
                string[] s3 = System.IO.Directory.GetFiles(path1);
                for (int i = 0; i < s3.Length; i++)
                {
                    tp06_chklist04.Items.Add(Path.GetFileName(s3[i]));
                }*/
            }

            if (FDownLoadPath[0] != "")
            {
                FDownLoadPath[2] = "";
                FDownLoadPath[3] = "";
                string mTempPath = tp06_cbo_SavePath.Text;
                tp06_cbo_SavePath.Text = "";
                try
                {
                    if (tp06_cboC.SelectedIndex == 0)
                    {
                        FDownLoadPath[2] = "\\" + tp06_cboC.Text + "_" + DateTime.Now.ToString("yyyyMMdd");
                        FDownLoadPath[3] = "";
                    }
                    else
                    {
                        FDownLoadPath[2] = "\\";
                        if (tp06_cboD.Text != "" && tp06_cboD.Visible)
                        {
                            string[] mPath3 = fc.Split("-", tp06_cboD.Text);
                            FDownLoadPath[3] = mPath3[1] + "_" + DateTime.Now.ToString("yyyyMMdd");
                        }
                    }

                    foreach (string s in FDownLoadPath)
                    {
                        tp06_cbo_SavePath.Text += s;
                    }
                }
                catch (System.Exception ex)
                {
                    fc.ShowMsg(ex.Message, "錯誤", "0");
                    tp06_cbo_SavePath.Text = mTempPath;
                }
            }

            if (tp06_cboC.SelectedItem.ToString() == "PKG")
            {
                tp06_cboD.Visible = false;
                tp06_cboD.Text = "";
                return;
            }

            tp06_cboD.Visible = true;
            string xs = "";
            string path = fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString() + @"\FOR客製客戶";
            tp06_cboD.Items.Clear();
            CboDList.Clear();
            string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
            foreach (string item in dirs)
            {
                string[] num = fc.Split("-", Path.GetFileNameWithoutExtension(item));
                if (!fc.CheckNum(fc.ASC(num[0][0])))
                    continue;
                CboDList.Add(Path.GetFileNameWithoutExtension(item));
                xs +=  fc.ZeroAtFirst(num[0], 4) + "-" + num[1]+"\r\n";
                tp06_cboD.Items.Add(fc.ZeroAtFirst(num[0], 4) + "-" + num[1]);                    
                //tp06_cboD.Items.Add(Path.GetFileNameWithoutExtension(item));                    
            }
            tp06_Msg.Text = xs + "";
            tp06_cboD.Focus();
            if (tp06_cboD.Items.Count > 0)
            {
                tp06_cboD.SelectedIndex = 0;
            }
        }
        private void tp06_cboD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp06_cboD.SelectedIndex == -1) return;
            tp06_chklist03.Items.Clear();
            tp06_IsHasModiFolder = false;
            modiname = "";
            for (int i = 0; i < CboDList.Count; i++)
            {
                string[] mName = CboDList[i].Split('-');
                string[] mName2 = tp06_cboD.Text.Split('-');
                if (mName.Length > 1)
                {
                    //if (CboDList[i].EndsWith(tp06_cboD.Text.Substring(tp06_cboD.Text.Length - 2, 2)))
                    if (mName[1] == mName2[1])
                    {
                        modiname = CboDList[i];
                        break;
                    }
                }
                else
                {
                    fc.WriteLog("沒有符合的資料", true);
                }
            }
            string path = fc.cboA[0] + @"\" + tp06_cboB.SelectedItem.ToString() + @"\FOR客製客戶\" + modiname;// tp06_cboD.SelectedItem.ToString();


            string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
            /*
            string[] s = null;
            if (dirs.Length > 0)//有MODI資料夾
                s = System.IO.Directory.GetFiles(path+@"\MODI");
            else
                s = System.IO.Directory.GetFiles(path);
            foreach (string item in s)
            {
                tp06_chklist03.Items.Add(Path.GetFileName(item));                    
            }*/
            if (dirs.Length > 0)//有MODI資料夾
            {
                path += @"\MODI";
                tp06_IsHasModiFolder = true;
            }

            tp06_CheckIsUnCopyFile(tp06_chklist03, path);

            if (FDownLoadPath[0] != "")
            {
                FDownLoadPath[3] = "";
                string mTempPath = tp06_cbo_SavePath.Text;
                tp06_cbo_SavePath.Text = "";
                try
                {
                    string[] mPath = fc.Split("-", tp06_cboD.Text);
                    FDownLoadPath[3] = mPath[1] + "_" + DateTime.Now.ToString("yyyyMMdd");
                    foreach (string s in FDownLoadPath)
                    {
                        tp06_cbo_SavePath.Text += s;
                    }
                }
                catch (System.Exception ex)
                {
                    fc.ShowMsg(ex.Message, "錯誤", "0");
                    tp06_cbo_SavePath.Text = mTempPath;
                }
            }

        }  
        private void tp06_chklist01_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if ((sender as CheckedListBoxControl).Items[e.Index].CheckState == CheckState.Checked)
            {
                e.Appearance.ForeColor = Color.FromArgb(255, 232, 166);
                e.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
            }
            else
                e.Appearance.ForeColor = Color.White;
            
        }

        private int tp06_GetCheckedCount(CheckedListBoxControl clb)
        {
            int mcount = 0;
            for (int i = 0; i < clb.Items.Count;i++ )
            {
                if (clb.Items[i].CheckState == CheckState.Checked)
                {
                    mcount++;
                }
            }
            return mcount;
        }

        private void tp06_chklist01_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
          /*  if ((sender as CheckedListBoxControl).Name == "tp06_chklist01")
            {
                tp06_gc01.Text = "安裝資料夾 " + tp06_GetCheckedCount((sender as CheckedListBoxControl)).ToString() + "/" + (sender as CheckedListBoxControl).Items.Count.ToString();
            }*/
            if (IsUnCopyResetOn)
            {
                return;
            }
            string name = (sender as CheckedListBoxControl).Items[e.Index].Value.ToString();
            if (e.State == CheckState.Checked)
            {
                if (tp06_list05.Items.Contains(name))
                    tp06_list05.Items.Remove(name);
            }
            else
            {
                if (!tp06_list05.Items.Contains(name))
                {
                    tp06_list05.Items.Add(name);
                }
                    
            }
        }

        private void tp06_DownLoad_Click(object sender, EventArgs e)
        {
            if (tp06_cboB.Text == "" || tp06_cboC.Text == "" || (tp06_cboC.Text == "PKG+MODI" && tp06_cboD.Text == "")) //20130904 ADD 新增只下載MODI
            {
                tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "選擇檔案欄位不可空白!!\r\n";
                return;
            }

            if (tp06_ID.Text == "" || tp06_PW.Text == "")
            {
                tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "帳號&密碼不可空白!!\r\n";
                return;
            }
            if (tp06_cbo_SavePath.Text == "")
            {
                tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "儲存路徑不可空白!!\r\n";
                return;
            }
            bool isAutoOpenDir = false;
            if (!fc.isDirectory(tp06_cbo_SavePath.Text))
            {
                isAutoOpenDir = true;
                if (fc.ShowConfirm("儲存路徑不存在!!\r\n是否建立資料夾?", "詢問") == DialogResult.Cancel)
                    return;
            }
            fc.AddItemToCbo(tp06_cbo_SavePath);

            string filename = @fc.ConfigPath;
            string FType = "TP06";
            string mkey1 = tp06_cboB.Text;
            IniConfigSource source = new IniConfigSource(filename);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            string mFileNames = "";
            string SavePath = "";
            for (int i = 0; i < tp06_cbo_SavePath.Items.Count;i++ )
            {
                SavePath += tp06_cbo_SavePath.Items[i].ToString() +"|";
            }
            try
            {
                if (File.Exists(filename))
                {
                   /* for (int i = 0; i < tp06_list05.Items.Count; i++)
                    {
                        mFileNames += tp06_list05.Items[i]+"|";
                    }*/
                    //source.Configs[FType].Set(mkey1, mFileNames);
                    //source.Configs[FType].Set("DownLoadPath", tp06_cbo_SavePath.Text);
                    source.Configs[FType].Set("DownLoadPath", SavePath);
                    source.Configs[FType].Set("ID", tp06_ID.Text);
                    source.Configs[FType].Set("PW", fc.desEncryptBase64(tp06_PW.Text));
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
            string modipath = "";
            if (tp06_IsHasModiFolder) modipath = @"\MODI"; 
            List<string[]> P1 = new List<string[]>();
            List<string[]> P2 = new List<string[]>();
            P1.Add(new string[] { fc.cboA[0] + @"\" + tp06_cboB.Text, tp06_cbo_SavePath.Text});
            P1.Add(new string[] { fc.cboA[0] + @"\" + tp06_cboB.Text + @"\PKG", tp06_cbo_SavePath.Text +  @"\PKG" });
            P1.Add(new string[] { fc.cboA[0] + @"\" + tp06_cboB.Text + @"\System", tp06_cbo_SavePath.Text +  @"\System" });
            //P1.Add(new string[] { fc.cboA[0] + @"\" + tp06_cboB.Text + @"\FOR客製客戶\" + tp06_cboD.Text + modipath, tp06_cbo_SavePath.Text + @"\MODI" });
            P1.Add(new string[] { fc.cboA[0] + @"\" + tp06_cboB.Text + @"\FOR客製客戶\" + modiname + modipath, tp06_cbo_SavePath.Text + @"\MODI" });
            tp06_SetFileNameForFileCopyEx(P2, tp06_chklist01);
            tp06_SetFileNameForFileCopyEx(P2, tp06_chklist02);
            tp06_SetFileNameForFileCopyEx(P2, tp06_chklist04);
            tp06_SetFileNameForFileCopyEx(P2, tp06_chklist03);
            FileCopyEx fex = new FileCopyEx();
            fex.SetType = 1;//COPY對外區資料到本機
            fex.SetP1 = P1;
            fex.SetP2 = P2;
            fex.ShowDialog();
            tp06_Msg.Text = fex.GetMsgInfo;
            if (isAutoOpenDir) System.Diagnostics.Process.Start(tp06_cbo_SavePath.Text);
        }
        private void tp06_SetFileNameForFileCopyEx(List<string[]> P,CheckedListBoxControl C)
        {
            int k = 0;
            for (int j = 0; j < C.Items.Count;j++ )
            {
                if (C.Items[j].CheckState == CheckState.Checked)
                {
                    k++;
                }
            }
            int y = 0;
            string[] s = new string[k];
            for (int i = 0; i < C.Items.Count; i++)
            {
                if (C.Items[i].CheckState == CheckState.Checked)
                {
                    s[y] = C.Items[i].Value.ToString();
                    y++;
                }              
            }
            P.Add(s);
        }

        private void tp06_Del1028_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(cbo_ProgPath.Text + @"\1028.LNG"))
                {
                    File.Delete(cbo_ProgPath.Text + @"\1028.LNG");
                    tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "1028.LNG 已刪除!\r\n";
                }
                else
                    tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "1028.LNG 不存在!\r\n";
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message);
            }
        }

        private void tp06_DelModi_Click(object sender, EventArgs e)
        {
            try
            {
                if (fc.isDirectory(cbo_ProgPath.Text + @"\MODI"))
                {
                    string[] files = Directory.GetFiles(cbo_ProgPath.Text + @"\MODI");
                    if (files.Length <= 0)
                    {
                        tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "MODI已無檔案存在!\r\n";
                    }
                    else
                    {
                        foreach (string s in files)
                        {
                            File.Delete(s);
                        }
                        tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "MODI檔案已刪除!\r\n";
                    }
                }
                else
                    tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "目錄不存在!\r\n";
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message);
            }
        }

        private void tp06_DelAuth_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(cbo_ProgPath.Text + @"\COSMOS_POS_Authorise"))
                {
                    File.Delete(cbo_ProgPath.Text + @"\COSMOS_POS_Authorise");
                    tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "授權檔案已刪除!\r\n";
                }
                else
                    tp06_Msg.Text += "[" + DateTime.Now.ToString() + "] " + "授權檔案不存在!\r\n";
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message);
            }
        }

        private void tp06_cboB_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as System.Windows.Forms.ComboBox).Name == "tp06_cboB")
                tp06_ver.Visible = tp06_cboB.Visible;
            else if ((sender as System.Windows.Forms.ComboBox).Name == "tp06_cboC")
                tp06_Type.Visible = tp06_cboC.Visible;
            else if ((sender as System.Windows.Forms.ComboBox).Name == "tp06_cboD")
                tp06_Modi.Visible = tp06_cboD.Visible;
        }

        private void tp06_F2_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(tp06_cbo_SavePath.Text))
            {
                fbd01.SelectedPath = tp06_cbo_SavePath.Text;
            }
            else
            {
                fc.ShowBoxMessage("目的資料夾不存在!");
                fbd01.SelectedPath = "C:\\";
            }

            if (fbd01.ShowDialog() == DialogResult.OK)
            {
                tp06_cbo_SavePath.Text = fbd01.SelectedPath;
            }

        }

        private void 主程式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (lbl_Cur_ProgPath.Text == "")
            {
                fc.ShowBoxMessage("[目前資訊]中的[程式路徑]不在存!!");
                return;
            }
            else
                filename = @lbl_Cur_ProgPath.Text;

            int stag = Int32.Parse((sender as ToolStripMenuItem).Tag.ToString());
            switch (stag)
            {
                case 0: filename += @"\PKG\DSCPOSI01.exe";                    
                break;
                case 1: filename += @"\PKG\PosMainGP.exe";
                break;
                case 2: filename += @"\DSCPOSNet01.exe";
                break;
                case 3: filename += @"\PKG\DSCPOSB01.exe";
                break;
                case 4: filename += @"\PKG\CHECKCONNECTION.EXE";
                break;
                case 5: filename += @"\DSCPOSDEVICE.EXE";
                break;
                case 6: filename += @"\DataSet.exe";
                break;
                case 7: filename += @"\DSCPOSSetup.exe";
                break;
                case 8: filename += @"\DSCPOSSetup.ini";
                break;
                case 9: filename += @"\1028.LNG";
                break;
                default:
                break;
            }
            if (filename !="")
            {
                fc.TryOpenFile(filename);
            }
            else
            {
                fc.ShowBoxMessage("選擇錯誤!");
            }
            
            
        }

        private void BBILoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fc.isDirectory(fc.DirVerTrans))
                ofd01.InitialDirectory = fc.DirVerTrans;
            else
                ofd01.InitialDirectory = fc.Mydocument;

            ofd01.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";

            if (ofd01.ShowDialog() == DialogResult.OK)
            {
                IsLoadFile = true;
                LoadFileName = ofd01.InitialDirectory + "\\" + ofd01.SafeFileName;
            }
            if (ofd01.SafeFileName != "VerTrans.ini")
            {
                fc.ShowBoxMessage("請選擇 <<VerTrans.ini>> !", "錯誤");
                return;
            }
            ReadIni();
            LoadCurrentInfo();
        }

        private void BBISetup_0_0_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TP05_LoadSetup x1 = new TP05_LoadSetup();
            x1.ShowDialog();
        }

        private void BBIExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void BBIRegedit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("regedit.exe");
        }

        private void BBIIInfo_0_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*fc.ShowBoxMessage("1.0.0.9".PadRight(13)+"修改目前版本資訊的標準版顏色為深藍色\n"+
                            "".PadRight(20) + "修改版本為空白時寫入REG錯誤的BUG，增加寫入前判斷版本不可空白\n" +
                            "1.0.0.10".PadRight(12) + "新增判別 32/64 位元PC 機碼路徑\n" +
                            "1.0.0.11".PadRight(12) + "新增ProgPath程式路徑，可自由變更路徑及設定各代號使用路徑\n" +
                            "1.0.0.12".PadRight(12) + "預設版本及路徑的DEF值\n" +
                            "1.0.0.13".PadRight(12) + "增加MENUBAR，增加資訊->版本說明\n" +
                            "1.0.0.14".PadRight(12) + "設定檔INI位置變更為:我的文件\\VerTrans\\VerTrans.ini\n" +
                            "".PadRight(20) + "若程式在該路徑找不到VerTrans.ini，則會詢問是否新增\n"+
                            "".PadRight(20) + "修正個案代號為空值時，資料帶入的錯誤\n"
                            
                            ,"版本更新說明");*/
            //fc.ShowBoxMessage(Properties.Resources.VerInfo, "版本更新說明");
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void BBIEnd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {        
            IsClose = true;
            this.Close();
        }

        private void BBIGP1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string filename = "";
            if (lbl_Cur_ProgPath.Text == "")
            {
                fc.ShowBoxMessage("[目前資訊]中的[程式路徑]不在存!!");
                return;
            }
            else
                filename = @lbl_Cur_ProgPath.Text;

            int stag = Int32.Parse(e.Item.Tag.ToString());
            switch (stag)
            {
                case 0: filename += @"\PKG\DSCPOSI01.exe";
                    break;
                case 1: filename += @"\PKG\PosMainGP.exe";
                    break;
                case 2: filename += @"\DSCPOSNet01.exe";
                    break;
                case 3: filename += @"\PKG\DSCPOSB01.exe";
                    break;
                case 4: filename += @"\PKG\CHECKCONNECTION.EXE";
                    break;
                case 5: filename += @"\DSCPOSDEVICE.EXE";
                    break;
                case 6: filename += @"\DataSet.exe";
                    break;
                case 7: filename += @"\DSCPOSSetup.exe";
                    break;
                case 8: filename += @"\DSCPOSSetup.ini";
                    break;
                case 9: filename += @"\1028.LNG";
                    break;
                default:
                    break;
            }
            if (filename != "")
            {
                fc.TryOpenFile(filename);
            }
            else
            {
                fc.ShowBoxMessage("選擇錯誤!");
            }
        }

        private void picPOS_Click(object sender, EventArgs e)
        {
            LoginSelect ls = new LoginSelect();
            string xIP = "";
            string xDBName = "";
            string xID = "";
            string xPW = "";
            string type = "";
            string xStoreNo = "";
            if (ls.ShowDialog() == DialogResult.OK)
            {
                type = ls.GetType;
            }
            
            if (type=="")
            {
                return;
            }
            else if (type == "POS")
            {
                xIP = edLocalDBIP.Text;
                xDBName = edLocalDBName.Text;
                xID = edLocalDBid.Text;
                xPW = edLocalDBpass.Text;
                xStoreNo = cbStoreNo.Text;
            }
            else if (type =="ERP")
            {
                xIP = edERPIP.Text;
                xDBName = edDSCSys_db.Text;
                xID = edERPDBid.Text;
                xPW = edERPDBpass.Text;
                xStoreNo = ""; 
            }

            try
            {
                if (xIP == "" || xDBName == "" || xID == "" || xPW == "")
                {
                    fc.ShowBoxMessage("進擊前請先準備好你的資料庫!!");
                }
                else
                {
                    MUEX mu = new MUEX();
                    mu.SetDBInfo = new string[] { xIP, xDBName, xID, xPW, type, xStoreNo };
                    if (mu.ShowDialog() == DialogResult.OK)
                    {
                        string[] PA = mu.GetPOSACCOUNT;
                        edPOSID.Text = PA[0];
                        edPOSPW.Text = PA[1];
                        btnedPOSSave.PerformClick();
                    }                    
                }
                
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void tb_VerNo_TextChanged(object sender, EventArgs e)
        {

        }
        private string LoadConfigValue(string section, string key)
        {
            string s = "";
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            if (source.Configs[section]!=null)
            {
                s = source.Configs[section].GetString(key, "");
            }
            return s;
        }
        private object LoadConfigValueO(string section, string key)
        {
            bool s = false;
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            if (source.Configs[section] != null)
            {
                s = source.Configs[section].GetBoolean(key, false);
            }
            return  s;
        }
        private bool LoadConfigValueBool(string section, string key)
        {
            bool s = true;
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            fc.SetAliasForNini(source);
            if (source.Configs[section] != null)
            {
                s = source.Configs[section].GetBoolean(key, true);
            }
            return s;
        }
        private void cbo_VerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectVerNo();
            SetAutoLoginGroup();
        }
        private void SelectVerNo()
        {
            if (cbo_VerNo.SelectedIndex == -1)
            {
                return;
            }
            lb_setup.Items.Clear();


            foreach (string s in LB_List.Keys)
            {
                if (cbo_VerNo.Text == "ALL")
                {
                    lb_setup.Items.Add(s);
                }
                else
                {
                    if (LB_List[s][4] == cbo_VerNo.Text)
                        lb_setup.Items.Add(s);
                }
            }
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            source.Configs["SetupVerNo"].Set("VerNo", cbo_VerNo.Text);
            source.Save();

            if (lb_setup.Items.Count ==0)
            {
                tb_SetupName.Text = "";
                tb_VerNo.Text = "";
                cbo_ver.Text = "";
                tb_code.Text = "";
                cbo_ProgPath.Text = "";
                DefaultVarInit();
            }
            else
            {
                /*            for (int i = 0; i < lb_setup.Items.Count; i++)
            {
                if (lb_setup.Items[i].ToString() == lbl_Cur_Name.Text)
                {
                    lb_setup.SelectedIndex = i;
                    break;
                }
            }
                 * */
                if (lb_setup.Items.Contains(lbl_Cur_Name.Text))
                {
                    lb_setup.SelectedValue = lbl_Cur_Name.Text;
                }
            }
            lb_setup.Focus();
        }

        private void gc_PC_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, fc.GetPassWord());
            if (!IsClose)
            {
                this.components = new System.ComponentModel.Container();
                notifyIcon1.Visible = true;
                this.Visible = false;
            }
        }

        private void BBICmd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe"); 
        }

        private void BBIClac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe"); 
        }

        private void BBICom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("devmgmt.msc"); 
        }

        private void BBIControl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("control printers");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            gc_PC.Text = "本機資訊   " + fc.GetPassWord();
            groupControl1.Text = "設定檔資訊 " + DateTime.Now.ToString();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(lbl_Cur_ProgPath.Text);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string t = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string s = Path.GetDirectoryName(t) + "\\" + barButtonItem10.Caption;
            System.Diagnostics.Process.Start(s);
        }

        private void 產生新複本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNewSetValue = true;
            f2Setupname = tb_SetupName.Text;
            f2SetupVerno = tb_VerNo.Text;
            f2Setcode = tb_code.Text;
            string mSetupName = tb_SetupName.Text;

            btn_New.PerformClick();
            if (lb_setup.Items.Contains(mSetupName))
            {
                lb_setup.SelectedItem = mSetupName;
                string filename = fc.CustPath;
                if (FCopyName.Trim() == "")
                {
                    return;
                }
                string setupname = FCopyName;
                /*                lb_setup.Items.Add(s[0]);
                    cbo_ver.SelectedIndex = Int32.Parse(s[1]);
                    tb_code.Text = s[2];
                    cbo_ProgPath.Text = s[3];
                    tb_VerNo.Text = s[4];*/
                IniConfigSource source = new IniConfigSource(filename);
                fc.SetAliasForNini(source);
                if (source.Configs[setupname] == null) source.Configs.Add(setupname);
                //SetupIni ini = new SetupIni();
                //ini.SetFileName(filename);
                try
                {
                    //if (!File.Exists(filename))
                    {
                        source.Configs[setupname].Set("ERPDBIP", edERPIP.Text);
                        source.Configs[setupname].Set("ERPDBName", edERPDBName.Text);
                        source.Configs[setupname].Set("ERPDBid", edERPDBid.Text);
                        source.Configs[setupname].Set("ERPDBpass", CEncrypt(edERPDBpass.Text, C_PrivateKey));
                        source.Configs[setupname].Set("DSCSYS_DB", edDSCSys_db.Text);
                        source.Configs[setupname].Set("PortNO", edPort.Text);

                        source.Configs[setupname].Set("LocalIP", edLocalIP.Text);
                        source.Configs[setupname].Set("LocalDBIP", edLocalDBIP.Text);
                        source.Configs[setupname].Set("LocalDBName", edLocalDBName.Text);
                        source.Configs[setupname].Set("LocalDBid", edLocalDBid.Text);
                        source.Configs[setupname].Set("LocalDBpass", CEncrypt(edLocalDBpass.Text, C_PrivateKey));

                        source.Configs[setupname].Set("CompanyNo", edCompanyNo.Text);
                        source.Configs[setupname].Set("CompanyName", lbCompanyName.Text);
                        source.Configs[setupname].Set("StoreNo", cbStoreNo.Text);
                        source.Configs[setupname].Set("StoreName", lbStoreName.Text);
                        source.Configs[setupname].Set("POSNo", edPOSNo.Text);

                        source.Configs[setupname].Set("SelfDefStamp", fc.GetStrFromBool(ckSelfDefStamp.Checked));
                        source.Configs[setupname].Set("StampStoreName", edStampStoreName.Text);
                        source.Configs[setupname].Set("StampManager", edStampManager.Text);
                        source.Configs[setupname].Set("StampCompanyName", edStampCompanyName.Text);
                        source.Configs[setupname].Set("StampCompanyAdd", edStampCompanyAdd.Text);
                        source.Configs[setupname].Set("StampCompanyPhone", edStampCompanyPhone.Text);
                        source.Configs[setupname].Set("StampUniID", edStampUniID.Text);

                        source.Configs[setupname].Set("LoginID", edPOSID.Text);
                        source.Configs[setupname].Set("LoginPW", edPOSPW.Text);
                        source.Save();
                        /* ini.WriteInteger("SCANNER_A", "BaudRate", 6);
                         ini.WriteInteger("SCANNER_A", "DataBits", 3);
                         ini.WriteInteger("SCANNER_A", "StopBits", 0);
                         ini.WriteInteger("SCANNER_A", "ParityCheck", 0);

                         ini.WriteInteger("SCANNER_B", "BaudRate", 6);
                         ini.WriteInteger("SCANNER_B", "DataBits", 3);
                         ini.WriteInteger("SCANNER_B", "StopBits", 0);
                         ini.WriteInteger("SCANNER_B", "ParityCheck", 0);*/
                    }
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string tag1 = (sender as SimpleButton).Tag.ToString() ;
            if (tag1 == "POS") 
                //GetDBNames(tag1, edLocalDBid.Text.Trim(), edLocalDBpass.Text.Trim(), edLocalIP.Text.Trim());  //20140417 mark 應該抓LocalDBIP
                GetDBNames(tag1, edLocalDBid.Text.Trim(), edLocalDBpass.Text.Trim(), edLocalDBIP.Text.Trim());//20140417 modi 應該抓edLocalDBIP
            else
                GetDBNames(tag1, edERPDBid.Text.Trim(), edERPDBpass.Text.Trim(), edERPIP.Text.Trim());            
        }
        private void GetDBNames(string objname,string id,string pw, string ip)
        {
            List<string> tmp = new List<string>();
            if (id.Trim() != "" && pw.Trim() != "" && ip.Trim() != "")
            {
                string sql = " select name FROM sys.databases";
                //tmp = fc.GetSQL(edERPDBid.Text.Trim(), edERPDBpass.Text.Trim(), edERPIP.Text.Trim(), sql);
                tmp = fc.GetSQL(id, pw, ip, sql);
                tmp.Sort();
            }
            else
            {
                fc.ShowBoxMessage("資料庫ID，PASSWORD，IP不可空白!!");
                return;
            }

            //ComboBoxEdit cbo = null;
            if (objname == "POS")
            {
                //cbo = edLocalDBName;
                SetDBName(edLocalDBName, tmp);
            }
            else
            {
                SetDBName(edDSCSys_db,tmp);
                SetDBName(edERPDBName,tmp);
               /* if (objname == "DSCSYS")
                {
                    cbo = edDSCSys_db;
                }
                else if (objname == "ERP")
                {
                    cbo = edERPDBName;
                }*/
            }


           /* cbo.Properties.Items.Clear();
            foreach (string s in tmp)
            {
                cbo.Properties.Items.Add(s);
            }            
            cbo = null;*/
        }
        private void SetDBName(ComboBoxEdit cbo, List<string> xDBName)
        {
            cbo.Properties.Items.Clear();
            foreach (string s in xDBName)
            {
                cbo.Properties.Items.Add(s);
            }  
        }

        private void edDSCSys_db_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            try
            {                
                if (e.State != DrawItemState.Selected)
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString(), ft1, new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    //e.Cache.FillRectangle(new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString(), ft1, new SolidBrush(Color.White), e.Bounds, e.Appearance.GetStringFormat());
                }
                e.Handled = true;
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
        }

        //20140205
        private void LoginToServer()
        {
            if (receiveUdpClient != null)
            {
                receiveUdpClient.Close();
            }
            Random random = new Random();
            port = random.Next(1024, 65500);
            txtlocalport = port.ToString();
            // 创建接受套接字
            IPAddress clientIP = IPAddress.Parse(txtLocalIP);
            clientIPEndPoint = new IPEndPoint(clientIP, int.Parse(txtlocalport));
            receiveUdpClient = new UdpClient(clientIPEndPoint);
            // 启动接收线程      
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
            // 匿名发送
            sendUdpClient = new UdpClient(0);
            // 启动发送线程
            Thread sendThread = new Thread(SendMessage);
            sendThread.Start(string.Format("login,{0},{1}", txtusername, clientIPEndPoint));
        }

        // 客户端接受服务器回应消息 
        private void ReceiveMessage()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {                    
                    byte[] receiveBytes = receiveUdpClient.Receive(ref remoteIPEndPoint);
                    // 关闭receiveUdpClient时会产生异常
                    if (remoteIPEndPoint.Address.ToString() == "0.0.0.0")
                    {
                        fc.ShowBoxMessage("伺服器關閉中");
                    }
                    string message = Encoding.Unicode.GetString(receiveBytes, 0, receiveBytes.Length);

                    // 处理消息
                    string[] splitstring = message.Split(',');

                    switch (splitstring[0])
                    {
                        case "Accept":
                            try
                            {
                                tcpClient = new TcpClient();
                                tcpClient.Connect(remoteIPEndPoint.Address, int.Parse(splitstring[1]));
                                if (tcpClient != null)
                                {
                                    // 表示连接成功
                                    networkStream = tcpClient.GetStream();
                                    binaryReader = new BinaryReader(networkStream);
                                    AcceptLogin();
                                }
                            }
                            catch (System.Exception ex)
                            {
                                fc.WriteLog(ex.Message, true);
                                fc.ShowBoxMessage("連接失敗\r\n" + ex.Message, "錯誤");
                            }

                            Thread getUserListThread = new Thread(GetUserList);
                            getUserListThread.Start();
                            break;
                        case "login":
                            string userItem = splitstring[1] + "," + splitstring[2];
                            AddItemToListView(userItem);
                            break;
                        case "logout":
                            RemoveItemFromListView(splitstring[1]);
                            break;
                       /* case "talk":
                            for (int i = 0; i < chatFormList.Count; i++)
                            {
                                if (chatFormList[i].Text == splitstring[2])
                                {
                                    chatFormList[i].ShowTalkInfo(splitstring[2], splitstring[1], splitstring[3]);
                                }
                            }

                            break;*/
                        case "kickall":
                            {
                                ClearItemFromListView();
                            }
                            break;
                        case "kick":
                            KickItemFromListView(splitstring[1]);
                            break;
                        case "sendsetup":
                            string xItem = splitstring[1] + "," + splitstring[2] + "," + splitstring[3] + "," + splitstring[4] + "," + splitstring[5];
                            AddRowFromgridView(xItem);
                            break;
                        case "ChangeName":
                            ChangeUserNameToList(splitstring[1], splitstring[3]);
                            break;
                    }
                }
                catch
                {
                    //fc.ShowBoxMessage(ex.Message, "錯誤");
                    //fc.WriteLog(ex.Message, true);
                    receiveUdpClient.Close();
                    break;
                }
            }
        }
        // 从服务器获取在线用户列表
        private void GetUserList()
        {
            while (true)
            {
                userListstring = null;
                try
                {
                    userListstring = binaryReader.ReadString();
                    if (userListstring.EndsWith("end"))
                    {
                        string[] splitstring = userListstring.Split(';');
                        for (int i = 0; i < splitstring.Length - 1; i++)
                        {
                            AddItemToListView(splitstring[i]);
                        }

                        binaryReader.Close();
                        tcpClient.Close();
                        break;
                    }
                }
                catch
                {
                    //fc.WriteLog(ex.Message, true);
                    break;
                }
            }
        }

        // 用委托机制来操作界面上控件
        private delegate void AddItemToListViewDelegate(string str);

        /// <summary>
        /// 在ListView中追加用户信息
        /// </summary>
        /// <param name="userinfo">要追加的信息</param>
        private void AddItemToListView(string userinfo)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                AddItemToListViewDelegate adddelegate = AddItemToListView;
                lstviewOnlineUser.Invoke(adddelegate, userinfo);
            }
            else
            {
                string[] splitstring = userinfo.Split(',');
                string[] ip = splitstring[1].Split(':');
                if (ip.Length > 0)
                {
                    for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
                    {
                        string[] connip =lstviewOnlineUser.Items[i].SubItems[2].Text.Split(':') ;
                        if (connip[0]==ip[0])
                        {
                            lstviewOnlineUser.Items.RemoveAt(i);
                            SetUserIP(FormatUserIP(ip[0], splitstring[0]), 1);
                            break;
                        }
                    }
                }
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(splitstring[0]);
                //item.SubItems.Add(splitstring[1]);
                string[] ff = fc.Split(":",splitstring[1]);
                int j=0;
                while (ff.Length > j)
                {
                    if (j >=2 )
                    {
                        break;
                    }
                    item.SubItems.Add(ff[j]);
                    j++;
                }                
                lstviewOnlineUser.Items.Add(item);
                SetUserIP(FormatUserIP(ip[0], splitstring[0]), 0);
                if (txtusername == splitstring[0])
                    return;
                else
                {
                   // AddToListControl("使用者登入：<Color=RoyalBlue>" + splitstring[0] + "</Color>", "black");
                    AddToListControl("[<Color=RoyalBlue>" + splitstring[0] + "</Color>]登入", "black");
                }
            }
        }
        // 用委托机制来操作界面上控件
        private delegate void AddToListControlDelegate(string xstr,string xcolor);

        /// <summary>
        /// 在ListView中追加用户信息
        /// </summary>
        /// <param name="userinfo">要追加的信息</param>
        private void AddToListControl(string xstr, string xcolor)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                AddToListControlDelegate adddelegate = AddToListControl;
                lstviewOnlineUser.Invoke(adddelegate, xstr, xcolor);
            }
            else
            {
                tp7_rtb.Items.Add("[" + DateTime.Now.ToString() + "] <Color="+xcolor+">"+ xstr + "</Color>");
                tp7_rtb.SelectedIndex = tp7_rtb.Items.Count - 1;
            }
        }
        
        private delegate void ChangeUserNameToListDelegate(string oldname,string newname);
        private void ChangeUserNameToList(string oldname, string newname)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                ChangeUserNameToListDelegate adddelegate = ChangeUserNameToList;
                lstviewOnlineUser.Invoke(adddelegate, oldname, newname);
            }
            else
            {
                for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
                {
                    if (lstviewOnlineUser.Items[i].SubItems[1].Text == oldname)
                    {
                        SetUserIP(FormatUserIP(lstviewOnlineUser.Items[i].SubItems[2].Text, oldname), 1);
                        SetUserIP(FormatUserIP(lstviewOnlineUser.Items[i].SubItems[2].Text, newname), 0);
                        lstviewOnlineUser.Items[i].SubItems[1].Text = newname;
                        AddToListControl("[<Color=Tomato>" + oldname + "</Color>]" +
                                         " 變更新名稱[" + "<Color=Green>" + newname + "</Color>]", "black");

                        if (txtusername == oldname)
                        {
                            txtusername = newname;
                            tp7_UserName = newname;

                        }
                    }
                }
                string filename = @fc.ConfigPath;
                string FType = "TP07";
                IniConfigSource source = new IniConfigSource(filename);
                try
                {
                    if (File.Exists(filename))
                    {
                        source.Configs[FType].Set("UserName", tp7_UserName);
                        source.Save();
                    }
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
            }
        }
        // 用委托机制来操作界面上控件
        private delegate void AcceptLogintDelegate();

        /// <summary>
        /// 在ListView中追加用户信息
        /// </summary>
        /// <param name="userinfo">要追加的信息</param>
        private void AcceptLogin()
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                AcceptLogintDelegate adddelegate = AcceptLogin;
                lstviewOnlineUser.Invoke(adddelegate);
            }
            else
            {
                AddToListControl("登入伺服器成功!", "blue");
            }
        }
        private delegate void RemoveItemFromListViewDelegate(string str);

        /// <summary>
        /// 从ListView中删除用户信息
        /// </summary>
        /// <param name="str">要删除的信息</param>
        private void RemoveItemFromListView(string str)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                RemoveItemFromListViewDelegate removedelegate = RemoveItemFromListView;
                lstviewOnlineUser.Invoke(removedelegate, str);
            }
            else
            {
                for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
                {
                    if (lstviewOnlineUser.Items[i].SubItems[1].Text == str)
                    {
                        {
                            AddToListControl("[<Color=Tomato>" + str + "</Color>]登出", "black");
                        }
                        SetUserIP(FormatUserIP(lstviewOnlineUser.Items[i].SubItems[2].Text, str), 1);
                        lstviewOnlineUser.Items[i].Remove();                        
                    }
                }   
            }
        }
        private delegate void KickItemFromListViewDelegate(string str);

        /// <summary>
        /// 从ListView中删除用户信息
        /// </summary>
        /// <param name="str">要删除的信息</param>
        private void KickItemFromListView(string str)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                KickItemFromListViewDelegate removedelegate = KickItemFromListView;
                lstviewOnlineUser.Invoke(removedelegate, str);
            }
            else
            {
                for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
                {
                    if (lstviewOnlineUser.Items[i].SubItems[1].Text == str)
                    {
                        if (txtusername == str)
                        {
                            AddToListControl("你被伺服器排擠囉~", "red");
                            lstviewOnlineUser.Items.Clear();
                            receiveUdpClient.Close();
                            SetUserIP("",2);
                        }
                        else
                        {
                            AddToListControl("<Color=Tomato>" + str + " </Color>遭受到無情的飛踢~", "black");
                            SetUserIP(FormatUserIP(lstviewOnlineUser.Items[i].SubItems[2].Text, str), 1);
                        }
                        lstviewOnlineUser.Items[i].Remove();
                    }
                }
            }
        }
        // 发送登录请求
        private void SendMessage(object obj)
        {
            string message = (string)obj;
            byte[] sendbytes = Encoding.Unicode.GetBytes(message);
            IPAddress remoteIp = IPAddress.Parse(txtserverIP);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(remoteIp, 2953);
            sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
            sendUdpClient.Close();
        }

        private void 傳送設定檔至ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsnUserList m = new MsnUserList();
            List<string> UserList = new List<string>();

            for (int i = 0; i < lstviewOnlineUser.Items.Count;i++ )
            {
                UserList.Add(lstviewOnlineUser.Items[i].SubItems[1].Text);
            }

            m.SetUserList = UserList;
            if (m.ShowDialog()!=DialogResult.OK)
            {
                return;
            }
            
            string user = m.GetUser;
            for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
            {
                if (lstviewOnlineUser.Items[i].SubItems[1].Text==user)
                {
                    SendUserIP = lstviewOnlineUser.Items[i].SubItems[2].Text;
                    SendUserPORT = lstviewOnlineUser.Items[i].SubItems[3].Text;
                    break;
                }
            }
            SendSetup();
        }

        private delegate void ClearItemFromListViewDelegate();
        private void ClearItemFromListView()
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                ClearItemFromListViewDelegate removedelegate = ClearItemFromListView;
                lstviewOnlineUser.Invoke(removedelegate);
            }
            else
            {
                lstviewOnlineUser.Items.Clear();
                receiveUdpClient.Close();
                SetUserIP("", 2);
                AddToListControl("伺服器關閉~", "red");
            }

        }
        private delegate void AddRowFromgridViewDelegate(string xStr);
        private void AddRowFromgridView(string xStr)
        {
            if (lstviewOnlineUser.InvokeRequired)
            {
                AddRowFromgridViewDelegate removedelegate = AddRowFromgridView;
                gridControl3.Invoke(removedelegate, xStr);
            }
            else
            {
                //if (chatFormList[i].Text == splitstring[2])
                {
                    string[] splitstring = xStr.Split(',');
                    DataRow dr = gv3DT.NewRow();
                    dr[1] = splitstring[0];
                    dr[2] = splitstring[1];
                    dr[3] = splitstring[2];
                    dr[4] = splitstring[3];
                    dr[5] = splitstring[4];
                    gv3DT.Rows.Add(dr);
                    gridControl3.DataSource = gv3DT;
                    fc.ShowBoxMessage("收到新的設定檔!!\r\n來自：" + splitstring[1] + "\r\n名稱：" + splitstring[2] + "\r\n版本：" + splitstring[3], "通知");
                }
            }

        }
        // 退出
        private void LogOutToServer()
        {
            // 匿名发送
            sendUdpClient = new UdpClient();
            //启动发送线程
            Thread sendThread = new Thread(SendMessage);
            sendThread.Start(string.Format("logout,{0},{1}", txtusername, clientIPEndPoint));
            receiveUdpClient.Close();
            lstviewOnlineUser.Items.Clear();
            this.Text = "Client";
            AddToListControl("登出!", "BlueViolet");
        }
        //變更使用者名稱
        private void ChangeNameToServer(string s)
        {
            // 匿名发送
            sendUdpClient = new UdpClient();
            //启动发送线程
            Thread sendThread = new Thread(SendMessage);
            
            sendThread.Start(string.Format("ChangeName,{0},{1},{2}", txtusername, clientIPEndPoint,s));            
        }

        private void SendSetup()
        {
            // 匿名发送
            sendUdpClient = new UdpClient();
            // 启动发送线程
            Thread sendThread = new Thread(SendMessageToUser);
            string s = lb_setup.SelectedItem.ToString();
            string v = tb_VerNo.Text;
            string SetupInfo = GetSetupInfo();
            string ver = tb_code.Text;
            if (ver == "")
                SetupInfo += "|;";
            else
                SetupInfo += ver + ";";
            
            sendThread.Start(string.Format("sendsetup,{0},{1},{2},{3},{4}", DateTime.Now.ToString(), txtusername, s, v, SetupInfo));
            fc.ShowBoxMessage("設定檔已傳送!", 1000);
        }
        private void SendMessageToUser(object obj)
        {
            string message = (string)obj;
            byte[] sendbytes = Encoding.Unicode.GetBytes(message);
            //string[] Sip = fc.Split(":", SendUserIP);
            //string[] Sip = new string[] { SendUserIP, SendUserIP };
            IPAddress clientIP = IPAddress.Parse(SendUserIP);
            IPEndPoint serverIPEndPoint = new IPEndPoint(clientIP, Int32.Parse(SendUserPORT));
            sendUdpClient.Send(sendbytes, sendbytes.Length, serverIPEndPoint);
            sendUdpClient.Close();
        }

        private string GetSetupInfo()
        {
            string S1 = "";
            if (ckSelfDefStamp.Checked)
                S1 = "T";
            else
                S1 = "F";
            List<string> ss = new List<string>();
            if (edERPIP.Text.Trim() == "127.0.0.1")
                ss.Add(lbl_LocalIP.Text);
            else
                ss.Add(edERPIP.Text);
            
            ss.Add(edERPDBName.Text);
            ss.Add(edERPDBid.Text);
            ss.Add(CEncrypt(edERPDBpass.Text, C_PrivateKey));
            ss.Add(edDSCSys_db.Text);
            ss.Add(edPort.Text);
            ss.Add(edLocalIP.Text);
            if (edLocalDBIP.Text.Trim() == "127.0.0.1")
                ss.Add(lbl_LocalIP.Text);
            else
                ss.Add(edLocalDBIP.Text);

            ss.Add(edLocalDBName.Text);
            ss.Add(edLocalDBid.Text);
            ss.Add(CEncrypt(edLocalDBpass.Text, C_PrivateKey));
            ss.Add(edCompanyNo.Text);
            ss.Add(lbCompanyName.Text);
            ss.Add(cbStoreNo.Text);
            ss.Add(lbStoreName.Text);
            ss.Add(edPOSNo.Text);
            ss.Add(S1);
            ss.Add(edStampStoreName.Text);
            ss.Add(edStampManager.Text);
            ss.Add(edStampCompanyName.Text);
            ss.Add(edStampCompanyAdd.Text);
            ss.Add(edStampCompanyPhone.Text);
            ss.Add(edStampUniID.Text);
            string rt = "";
            for (int i = 0; i < ss.Count;i++ )
            {
                if (ss[i] == "")
                    rt += "|;";
                else
                    rt += ss[i] + ";";
                
            }
            return rt;
        }
        private void SetSetupInfo(string setupInfo)
        {
            string[] s = fc.Split(";",setupInfo);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i]=="|")
                {
                    s[i] = "";
                }
            }
            edERPIP.Text = s[0];
            edERPDBName.Text = s[1];
            edERPDBid.Text = s[2];
            edERPDBpass.Text = CDecrypt(s[3], C_PrivateKey);
            edDSCSys_db.Text = s[4];
            edPort.Text = s[5];
            edLocalIP.Text = s[6];
            edLocalDBIP.Text = s[7];
            edLocalDBName.Text = s[8];
            edLocalDBid.Text = s[9];
            edLocalDBpass.Text = CDecrypt(s[10], C_PrivateKey);
            edCompanyNo.Text = s[11];
            lbCompanyName.Text = s[12];
            cbStoreNo.Text = s[13];
            lbStoreName.Text = s[14];
            edPOSNo.Text = s[15];
            if (s[16]=="T")
                ckSelfDefStamp.Checked = true;
            else
                ckSelfDefStamp.Checked = false;                        
            edStampStoreName.Text = s[17];
            edStampManager.Text = s[18];
            edStampCompanyName.Text = s[19];
            edStampCompanyAdd.Text = s[20];
            edStampCompanyPhone.Text = s[21];
            edStampUniID.Text = s[22];
            tb_code.Text = s[23];
        }

        private void 套用此設定檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView3.FocusedRowHandle <0)
            {
                return;
            }
            if (fc.ShowConfirm("<<" + gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Main") + ">>確定套用此設定檔?") == DialogResult.OK)
            {
                SetSetupInfo(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Setup").ToString());
            }
        }

        private void 另存新設定檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView3.FocusedRowHandle < 0)
            {
                return;
            }
            tabControl1.SelectedIndex = 0;
            btnNewSetValue = true;
            f2Setupname = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Main").ToString();
            f2SetupVerno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "VerNo").ToString();
            string tmp = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Setup").ToString();
            string[] s = fc.Split(";", tmp);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == "|")
                {
                    s[i] = "";
                }
            }
            f2Setcode = s[23];
            btn_New.PerformClick();
            string filename = fc.CustPath;
            if (FCopyName.Trim() == "")
            {
                return;
            }
            string setupname = FCopyName;
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            if (source.Configs[setupname] == null) source.Configs.Add(setupname);
            try
            {
                {
                    source.Configs[setupname].Set("ERPDBIP", s[0]);
                    source.Configs[setupname].Set("ERPDBName", s[1]);
                    source.Configs[setupname].Set("ERPDBid", s[2]);
                    source.Configs[setupname].Set("ERPDBpass", s[3]);
                    source.Configs[setupname].Set("DSCSYS_DB", s[4]);
                    source.Configs[setupname].Set("PortNO", s[5]);

                    source.Configs[setupname].Set("LocalIP", s[6]);
                    source.Configs[setupname].Set("LocalDBIP", s[7]);
                    source.Configs[setupname].Set("LocalDBName", s[8]);
                    source.Configs[setupname].Set("LocalDBid", s[9]);
                    source.Configs[setupname].Set("LocalDBpass", s[10]);

                    source.Configs[setupname].Set("CompanyNo", s[11]);
                    source.Configs[setupname].Set("CompanyName", s[12]);
                    source.Configs[setupname].Set("StoreNo", s[13]);
                    source.Configs[setupname].Set("StoreName", s[14]);
                    source.Configs[setupname].Set("POSNo", s[15]);

                    bool ds = false;
                    if (s[16]=="T")
                    {
                        ds = true;
                    }
                    source.Configs[setupname].Set("SelfDefStamp", fc.GetStrFromBool(ds));
                    source.Configs[setupname].Set("StampStoreName", s[17]);
                    source.Configs[setupname].Set("StampManager", s[18]);
                    source.Configs[setupname].Set("StampCompanyName", s[19]);
                    source.Configs[setupname].Set("StampCompanyAdd", s[20]);
                    source.Configs[setupname].Set("StampCompanyPhone", s[21]);
                    source.Configs[setupname].Set("StampUniID", s[22]);

                    source.Configs[setupname].Set("LoginID", edPOSID.Text);
                    source.Configs[setupname].Set("LoginPW", edPOSPW.Text);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
        }
        //程式碼區塊
        private int lastPosition = 0;
        private void tp7_btnLogin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
            {
                if (lstviewOnlineUser.Items[i].SubItems[1].Text==txtusername)
                {
                    fc.ShowBoxMessage("已登入","資訊");
                    return;
                }
            }
            LoginToServer();
        }

        private void tp7_btnLogOut_Click(object sender, EventArgs e)
        {
            SetUserIP("", 2);
            for (int i = 0; i < lstviewOnlineUser.Items.Count; i++)
            {
                if (lstviewOnlineUser.Items[i].SubItems[1].Text == txtusername)
                {
                    LogOutToServer();
                    return;                    
                }
            }            
        }

        private void tp7_btnChangeName_Click(object sender, EventArgs e)
        {
            string s = "";
            if(fc.ShowBoxInput("請輸入要變更的名稱",out s))
            ChangeNameToServer(s);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RecVerControl r = new RecVerControl(tp04_tdFileName.Text);
            r.ShowDialog();
        }

        private void tp06_lbl_SavePath_Click(object sender, EventArgs e)
        {
            tp06_cbo_SavePath.Items.Clear();
            tp06_cbo_SavePath.Items.Add(@"C:\COSMOS_POS");
            tp06_cbo_SavePath.SelectedIndex = 0;

            string filename = @fc.ConfigPath;
            string FType = "TP06";
            IniConfigSource source = new IniConfigSource(filename);
            try
            {
                if (File.Exists(filename))
                {
                    source.Configs[FType].Set("DownLoadPath", tp06_cbo_SavePath.Text);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }

        private void tp06_OpenDir_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(tp06_cbo_SavePath.Text))
            {
                System.Diagnostics.Process.Start(tp06_cbo_SavePath.Text);
            }
            else
            {
                fc.ShowBoxMessage("路徑錯誤或此資料夾不存在!!");
            }                        
        }

        private void tp06_btnCopySave_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = @fc.ConfigPath;
                string FType = "TP06";
                string mkey1 = tp06_cboB.Text;
                IniConfigSource source = new IniConfigSource(filename);
                string mFileNames = "";
                string SavePath = "";
                if (File.Exists(filename))
                {
                    for (int i = 0; i < tp06_list05.Items.Count; i++)
                    {
                        mFileNames += tp06_list05.Items[i] + "|";
                    }
                    source.Configs[FType].Set(mkey1, mFileNames);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }

        private void tp06_btnCopyRestore_Click(object sender, EventArgs e)
        {
            tp06_GetNoCopyList();
            tp06_GetCopyList(tp06_chklist01);
            tp06_GetCopyList(tp06_chklist02);
            tp06_GetCopyList(tp06_chklist03);
            tp06_GetCopyList(tp06_chklist04);
        }
        private void tp06_GetCopyList(CheckedListBoxControl clb)
        {
            if (clb.Items.Count <=0)
            {
                return;
            }
            ListBox lb = new ListBox();
            IsUnCopyResetOn = true;
            for (int i = 0; i < clb.Items.Count;i++ )
            {
                clb.Items[i].CheckState = CheckState.Checked;
            }
            IsUnCopyResetOn = false;
            for (int i = 0; i < tp06_list05.Items.Count; i++)
            {   
                for (int j = 0; j < clb.Items.Count;j++ )
                {
                    if (clb.Items[j].Value.ToString() == tp06_list05.Items[i].ToString())
                    {
                        clb.Items[j].CheckState = CheckState.Unchecked;
                        break;
                    }
                }
            }
        }
        private void tp06_GetNoCopyList()
        {
            /*tp06_chklist01.Items.Clear();
            tp06_chklist02.Items.Clear();
            tp06_chklist03.Items.Clear();
            tp06_chklist04.Items.Clear();*/
            tp06_list05.Items.Clear();

            string filename = @fc.ConfigPath;
            string FType = "TP06";
            string mkey1 = tp06_cboB.Text;
            string[] mUnCopyFilestr = null;

            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            IniConfigSource source = new IniConfigSource(filename);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {

                mUnCopyFilestr = fc.Split("|", source.Configs[FType].GetString(mkey1, ""));
                foreach (string item in mUnCopyFilestr)
                {
                    tp06_list05.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }
        CheckState tp06_UnCopySelectAllSW = CheckState.Checked;
        
        private void tp06_btnCopyAll_Click(object sender, EventArgs e)
        {            
            for (int j=0;j<clb.Count;j++)
            {            
                for (int i = 0; i < clb[j].Items.Count; i++)
                {
                    clb[j].Items[i].CheckState = tp06_UnCopySelectAllSW;
                }
            }
            if (tp06_UnCopySelectAllSW == CheckState.Checked)
            {
                tp06_UnCopySelectAllSW = CheckState.Unchecked;
            }
            else
                tp06_UnCopySelectAllSW = CheckState.Checked;
            
        }
        private void tp05_td18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tp05_Clear.PerformClick();
            }
        }
        private void tp05_td18_KeyUp(object sender, KeyEventArgs e)
        {           
            
            if (tp05_td18.Text.Length <= 0)  return;

            if (e.Control && e.KeyCode == Keys.Back)
            {
                tp05_td05.Text = "";
                tp05_td06.Text = "";
                tp05_td07.Text = "";
                tp05_td08.Text = "";
                tp05_td09.Text = "";
                tp05_td10.Text = "";
                tp05_td18.Text = "";
                return;
            }


            int num = -1;
            bool Ism2003 = false;
            Ism2003 = !Int32.TryParse(tp05_td18.Text.Substring(0, 1), out num);
            if (/*tp05_td18.Text.Length > 5 ||*/ tp05_cbo01.Text == "")
            {
                //if (Ism2003)
                {
                    tp05_td18.Text = "";
                    return;
                }                
            }
            string m2003 = "";
            string mLast = "";
            string schema = "";
            //if (tp05_td18.Text.Length < 5)
            if (!Ism2003)
            {
                m2003 = tp05_cbo03.Text;
                mLast = tp05_cbo04.Text.Substring(3, 2);
            }
            else 
            {
                if (tp05_td18.Text.Length == 5)
                {
                    m2003 = tp05_td18.Text.Substring(0, 3).ToUpper();
                    mLast = tp05_td18.Text.Substring(3, 2).ToUpper();
                }                
            }
            schema = tp05_td18.Text.ToUpper();
            
   
            //bool Ism2003 = !Int32.TryParse(schema, out num);
            if (Ism2003)
            {
                if (schema.Length >= 5)
                {
                    int idx = -1;
                    idx = tp05_cbo03.Properties.Items.IndexOf(m2003);
                    if (idx != -1)
                    {
                        tp05_cbo03.SelectedIndex = idx;
                        //tp05_td18.Text = "";
                    }
                    else//找不到標準SCHEMA 接著找SYS TABLE 
                    {
                        idx = tp05_cbo03.Properties.Items.IndexOf("SYS");
                        tp05_cbo03.SelectedIndex = idx;
                        idx = tp05_cbo04.Properties.Items.IndexOf(schema);
                        if (idx != -1)
                        {
                            tp05_cbo04.SelectedIndex = idx;
                        }
                       /* for (int i = 0; i < tp05_cbo04.Properties.Items.Count; i++)
                        {
                            if (tp05_cbo04.Properties.Items[i].ToString().ToUpper().StartsWith(schema))
                            {
                                tp05_cbo04.SelectedIndex = i;
                                //tp05_td18.Text = "";
                            }
                        }*/
                        return;
                    }
                    idx = tp05_cbo04.Properties.Items.IndexOf(schema);
                    if (idx != -1)
                    {
                        tp05_cbo04.SelectedIndex = idx;
                       // tp05_td18.Text = "";
                    }
                    else//找標準SCHEMA 但是找不到對應TABLE 表示此TABLE在SYS 例如ADMMA 
                    {
                        idx = tp05_cbo03.Properties.Items.IndexOf("SYS");
                        tp05_cbo03.SelectedIndex = idx;
                        idx = tp05_cbo04.Properties.Items.IndexOf(schema);
                        if (idx != -1)
                        {
                            tp05_cbo04.SelectedIndex = idx;
                        }
                       /* for (int i = 0; i < tp05_cbo04.Properties.Items.Count; i++)
                        {
                            if (tp05_cbo04.Properties.Items[i].ToString().ToUpper().StartsWith(schema))
                            {
                                tp05_cbo04.SelectedIndex = i;
                            }
                        }*/
                        return;
                    }
                }
            }
            else
            {
                bool Isnum = Int32.TryParse(schema, out num);
                if (Isnum)
                {
                    tp05_td07.Text = mLast + string.Format("{0:000}", num);// schema;     
                }
                else
                {
                    string[] tmp = null;
                    List<string> atmp = new List<string>();
                    string tmp1 = "";
                    if (schema.Contains(','))
                    {
                        string[] tmp2 = fc.Split(",", schema);
                        for (int i = 0; i < tmp2.Length;i++ )
                        {
                            if (tmp2[i].Contains('-'))
                            {
                                string[] tmp3 = fc.Split("-", tmp2[i]);
                                if (tmp3.Length == 2)
                                {
                                    int a = Int32.Parse(tmp3[0]);
                                    int b = Int32.Parse(tmp3[1]);
                                    if (a > b)
                                    {
                                        //break;
                                    }
                                    else if (a == b)
                                    {
                                        atmp.Add(a.ToString());
                                    }
                                    else
                                    {                                                                        
                                        for (; a <= b; a++)
                                        {
                                            atmp.Add(a.ToString());                                            
                                        }
                                    }
                                }
                            }
                            else
                            {
                                atmp.Add(tmp2[i]);
                            }
                        }
                        //tmp = fc.Split(",", schema);
                        tmp = atmp.ToArray();
                    }
                    else if (schema.Contains('-'))
                    {
                        string[] tmp2 =  fc.Split("-", schema);
                        if (tmp2.Length == 2)
                        {
                            int a = Int32.Parse(tmp2[0]);
                            int b = Int32.Parse(tmp2[1]);
                            if (a > b)
                            {
                                return;
                            }
                            else if (a==b)
                            {
                                tmp = new string[] { a.ToString() };
                            }
                            else
                            {
                                int j = 0;
                                tmp = new string[b - a + 1];
                                for (; a <= b ; a++, j++)
                                {
                                    tmp[j] = a.ToString();
                                }
                            }
                            
                        }
                        else
                            return;
                    }
                    if (tmp != null)
                    {
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i].Trim() != "")
                            {
                                tmp1 += mLast + string.Format("{0:000}", Int32.Parse(tmp[i])) + ",";
                            }
                        }
                        if (tmp1.EndsWith(","))
                        {
                            tp05_td07.Text = tmp1.Substring(0, tmp1.Length - 1);
                        }
                    }
                }                                    
            }            
        }

        private void tp06_PWLbl_Click(object sender, EventArgs e)
        {
            if (tp06_PW.PasswordChar==char.MinValue)
            {
                tp06_PW.PasswordChar='●';
            }
            else
            {
                tp06_PW.PasswordChar = char.MinValue;
            }
            WriteIni("TP06", "PWChar", tp06_PW.PasswordChar.ToString());
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void tp04_cbo00_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtList.Count <= 0)
            {
                return;
            }
            else
            {
                gridControl1.DataSource = dtList[tp04_cbo00.SelectedIndex];
            }
        }

        private void tp04_btnRCIniPath_Click(object sender, EventArgs e)
        {
           string path = "";
           if (tp04_tdFileName.Text == "") //TP04_LoadPath = @"C:\";
           tp04_fbd01.RootFolder = Environment.SpecialFolder.DesktopDirectory;           
           tp04_fbd01.Description = "請選擇Resource設定檔的資料夾";

           if (tp04_fbd01.ShowDialog() == DialogResult.OK)
               tp04_tdFileName.Text = tp04_fbd01.SelectedPath;
           else
               return;
        
           path = tp04_tdFileName.Text;
           //TP04_LoadPath = Path.GetDirectoryName(path);
           TP04_LoadPath = tp04_tdFileName.Text;
           WriteIni("TP04", "LoadPath", TP04_LoadPath);
        }

        private void btn_UpdatePOSPI_Click(object sender, EventArgs e)
        {

        }
        private void GOUpdatePOSPI()
        {
            string mSQL1 = String.Format("SELECT TOP 1 * FROM POSPI WHERE PI001='{0}' AND PI002='{1}'", cbStoreNo.Text, edPOSNo.Text);
            string mSQL2 = String.Format("SELECT TOP 1 * FROM POSPI WHERE PI001='{0}' ", edCompanyNo.Text);
            bool IsPIExist = false;
            IsPIExist = CheckPOSPIIsExist(mSQL1); //POS機台 PI
            if (IsPIExist) //UPDATE
            {
                UPDATEPOSPI("0");
            }
            else //INSERT
            {
                INSERTPOSPI("0");
            }
            IsPIExist = CheckPOSPIIsExist(mSQL2); //ERP機台 PI
            if (IsPIExist) //UPDATE
            {
                UPDATEPOSPI("2");
            }
            else //INSERT
            {
                INSERTPOSPI("2");
            }
        }
        public bool CheckPOSPIIsExist(string xsql)
        {
            string connstr = fc.makeConnectString(edERPIP.Text, edERPDBName.Text, edERPDBid.Text, edERPDBpass.Text); 
            SqlConnection conn = null;
            conn = new SqlConnection(connstr);
            bool IsPIExist = false;
            try
            {
                conn.Open();
                {
                    SqlCommand myCommand = null; SqlDataReader myDataReader = null;
                    myCommand = new SqlCommand(xsql, conn);
                    myDataReader = myCommand.ExecuteReader();
                    IsPIExist = myDataReader.HasRows;
                    conn.Close();
                    myCommand.Cancel();
                    myDataReader.Close();
                }
                return IsPIExist;
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
            return false;
        }
        public void WriteLoginAccount(string Verno)
        {
            string connstr = fc.makeConnectString(edLocalDBIP.Text, edLocalDBName.Text, edLocalDBid.Text, edLocalDBpass.Text);
            SqlConnection conn = new SqlConnection(connstr);
            string xsql = "SELECT MU001 FROM POSMU WHERE MU001='001' OR MU001='123' ";
            int j = 0;
            try
            {
                conn.Open();
                {
                    SqlCommand myCommand = null; 
                    myCommand = new SqlCommand(xsql, conn);
                    SqlDataAdapter data = new SqlDataAdapter(myCommand);
                    DataSet ds = new DataSet();
                    data.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                    {
                        if (ds.Tables[0].Rows[i]["MU001"].ToString().Trim() == "001" )
                        {
                            j += 1;
                        }
                        else if (ds.Tables[0].Rows[i]["MU001"].ToString().Trim() == "123")
                        {
                            j += 2;
                        }
                    }

                    myCommand.Cancel();
                    string a = "";
                    string b = "";

                    if ((Verno == "34") || (Verno == "GP2") || (Verno=="GP3"))
                    {
                        a = " ,MU015,MU016,MU017,MU018";
                        b = " ,'N','Y','N','N'";
                    }                    

                    if (j == 0) //兩個帳號都不存在
                    {
                        xsql = " INSERT INTO POSMU (MU001,MU002,MU003,MU005,MU007" + a + ") " +
                               " VALUES ('001','001','" + fc.ERPEncrypt("001") + "','20000101','1'" + b + "); " +
                               " INSERT INTO POSMU (MU001,MU002,MU003,MU005,MU007" + a + ") " +
                               " VALUES ('123','123','" + fc.ERPEncrypt("123") + "','20000101','1'" + b + "); ";
                    }
                    else if (j==1) //001存在
                    {
                        xsql = " UPDATE POSMU SET MU003='" + fc.ERPEncrypt("001") + "' WHERE MU001='001'; " +
                               " INSERT INTO POSMU (MU001,MU002,MU003,MU005,MU007" + a + ") " +
                               " VALUES ('123','123','" + fc.ERPEncrypt("123") + "','20000101','1'" + b + "); ";
                    }
                    else if (j==2)//123存在
                    {
                        xsql = " INSERT INTO POSMU (MU001,MU002,MU003,MU005,MU007" + a + ") " +
                               " VALUES ('001','001','" + fc.ERPEncrypt("001") + "','20000101','1'" + b + "); " +
                               " UPDATE POSMU SET MU003='" + fc.ERPEncrypt("123") + "' WHERE MU001='123'; ";
                    }
                    else if (j==3)//兩個都存在
                    {
                        xsql = " UPDATE POSMU SET MU003='" + fc.ERPEncrypt("001") + "' WHERE MU001='001'; " +
                               " UPDATE POSMU SET MU003='" + fc.ERPEncrypt("123") + "' WHERE MU001='123'; ";
                    }

                    myCommand = new SqlCommand(xsql, conn);
                    myCommand.ExecuteNonQuery();

                    conn.Close();
                    myCommand.Cancel();
                }
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
        }
        public void INSERTPOSPI(string xtype)
        {
            try
            {
                string connstr = fc.makeConnectString(edERPIP.Text, edERPDBName.Text, edERPDBid.Text, edERPDBpass.Text); 
                SqlConnection conn = null;
                conn = new SqlConnection(connstr);
                var sqlQuery = "SELECT * FROM POSPI WHERE 0 = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                DataRow newRow = dataSet.Tables[0].NewRow();
                for (int i = 0; i < newRow.Table.Columns.Count;i++ )
                {
                    
                    if(newRow.Table.Columns[i].DataType ==  System.Type.GetType("System.String"))
                        newRow[i] = "";
                    else if (newRow.Table.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                        newRow[i] = 0;                
                }
                newRow["COMPANY"] = edERPDBName.Text;
                newRow["CREATE_DATE"] = DateTime.Now.ToString("yyyyMMdd");
                newRow["FLAG"] = 0;
                newRow["CREATE_TIME"] = DateTime.Now.ToString("HH:mm:ss");
                if (newRow.Table.Columns.Contains("sync_count"))
                    newRow["sync_count"] = 0;
                if (xtype == "2") //ERP
                {
                    newRow["PI001"] = edCompanyNo.Text;
                    newRow["PI002"] = "000";
                }
                else
                {
                    newRow["PI001"] = cbStoreNo.Text;
                    newRow["PI002"] = edPOSNo.Text;
                }
                
                newRow["PI003"] = edLocalIP.Text;
                newRow["PI004"] = "";
                newRow["PI005"] = "";
                newRow["PI006"] = "0";
                newRow["PI007"] = "";
                newRow["PI008"] = "";
                newRow["PI009"] = "";
                newRow["PI010"] = edERPIP.Text;
                if (xtype == "2") //ERP 不需要這些
                {
                    newRow["PI011"] = "";
                    newRow["PI012"] = "";
                    newRow["PI013"] = "";
                    newRow["PI014"] = "";
                    newRow["PI015"] = "";
                    newRow["PI016"] = "";
                    newRow["PI017"] = "";
                    newRow["PI018"] = "";
                }
                else
                {
                    newRow["PI011"] = edERPIP.Text;
                    newRow["PI012"] = edERPDBName.Text;
                    newRow["PI013"] = edERPDBid.Text;
                    newRow["PI014"] = CEncrypt(edERPDBpass.Text, C_PrivateKey);
                    newRow["PI015"] = edERPIP.Text;
                    newRow["PI016"] = edERPDBName.Text;
                    newRow["PI017"] = edERPDBid.Text;
                    newRow["PI018"] = CEncrypt(edERPDBpass.Text, C_PrivateKey);
                }
                newRow["PI019"] = "0";
                newRow["PI020"] = "";
                newRow["PI021"] = "";
                newRow["PI022"] = "";
                newRow["PI023"] = "";
                newRow["PI024"] = "0";
                newRow["PI025"] = xtype;
                newRow["PI026"] = "N";
                if (xtype=="2")            
                    newRow["PI027"] = edERPDBName.Text;            
                else
                    newRow["PI027"] = "";
                newRow["PI028"] = edLocalDBName.Text;
                dataSet.Tables[0].Rows.Add(newRow);
                new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet);
                conn.Close();
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
        }
        public void UPDATEPOSPI(string xtype)
        {
            try
            {
                string mSQL1 = String.Format("SELECT TOP 1 * FROM POSPI WHERE PI001='{0}' AND PI002='{1}'", cbStoreNo.Text, edPOSNo.Text);
                string mSQL2 = String.Format("SELECT TOP 1 * FROM POSPI WHERE PI001='{0}' ", edCompanyNo.Text);  
                string connstr = fc.makeConnectString(edERPIP.Text, edERPDBName.Text, edERPDBid.Text, edERPDBpass.Text);
                SqlConnection conn = null;
                conn = new SqlConnection(connstr);
                SqlDataAdapter dataAdapter = null;

                if (xtype == "2")
                    dataAdapter = new SqlDataAdapter(mSQL2, conn);
                else
                    dataAdapter = new SqlDataAdapter(mSQL1, conn);
                
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataSet.Tables[0].Rows[0]["MODI_DATE"] = DateTime.Now.ToString("yyyyMMdd");
                dataSet.Tables[0].Rows[0]["MODI_TIME"] = DateTime.Now.ToString("HH:mm:ss");

                if (xtype == "2") //ERP 
                    dataSet.Tables[0].Rows[0]["PI003"] = edERPIP.Text;
                else
                    dataSet.Tables[0].Rows[0]["PI003"] = edLocalIP.Text;

                //dataSet.Tables[0].Rows[0]["PI004"] = "";
                //dataSet.Tables[0].Rows[0]["PI005"] = "";
                //dataSet.Tables[0].Rows[0]["PI006"] = "0"; //20140417 mark 如果UPDATE 0 會使原本有選擇範本的變成0 所以UPDATE不更新此項
                //dataSet.Tables[0].Rows[0]["PI007"] = "";
                //dataSet.Tables[0].Rows[0]["PI008"] = "";
                //dataSet.Tables[0].Rows[0]["PI009"] = "";

                if (xtype == "2") //ERP 
                    dataSet.Tables[0].Rows[0]["PI010"] = edERPIP.Text;
                else
                    dataSet.Tables[0].Rows[0]["PI010"] = edLocalDBIP.Text;

                if (xtype == "2") //ERP 不需要這些
                {
                    dataSet.Tables[0].Rows[0]["PI011"] = "";
                    dataSet.Tables[0].Rows[0]["PI012"] = "";
                    dataSet.Tables[0].Rows[0]["PI013"] = "";
                    dataSet.Tables[0].Rows[0]["PI014"] = "";
                    dataSet.Tables[0].Rows[0]["PI015"] = "";
                    dataSet.Tables[0].Rows[0]["PI016"] = "";
                    dataSet.Tables[0].Rows[0]["PI017"] = "";
                    dataSet.Tables[0].Rows[0]["PI018"] = "";
                }
                else
                {
                    dataSet.Tables[0].Rows[0]["PI011"] = edERPIP.Text;
                    dataSet.Tables[0].Rows[0]["PI012"] = edERPDBName.Text;
                    dataSet.Tables[0].Rows[0]["PI013"] = edERPDBid.Text;
                    dataSet.Tables[0].Rows[0]["PI014"] = CEncrypt(edERPDBpass.Text, C_PrivateKey);
                    dataSet.Tables[0].Rows[0]["PI015"] = edERPIP.Text;
                    dataSet.Tables[0].Rows[0]["PI016"] = edERPDBName.Text;
                    dataSet.Tables[0].Rows[0]["PI017"] = edERPDBid.Text;
                    dataSet.Tables[0].Rows[0]["PI018"] = CEncrypt(edERPDBpass.Text, C_PrivateKey);
                }
                //dataSet.Tables[0].Rows[0]["PI019"] = "0";
                //dataSet.Tables[0].Rows[0]["PI020"] = "";
                //dataSet.Tables[0].Rows[0]["PI021"] = "";
                //dataSet.Tables[0].Rows[0]["PI022"] = "";
                //dataSet.Tables[0].Rows[0]["PI023"] = "";
                //dataSet.Tables[0].Rows[0]["PI024"] = "0";
                dataSet.Tables[0].Rows[0]["PI025"] = xtype;
                dataSet.Tables[0].Rows[0]["PI026"] = "N";
                if (xtype == "2")
                    dataSet.Tables[0].Rows[0]["PI027"] = edERPDBName.Text;
                else
                    dataSet.Tables[0].Rows[0]["PI027"] = "";
                dataSet.Tables[0].Rows[0]["PI028"] = edLocalDBName.Text;

                new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet);
                conn.Close();
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage(ex.Message);
            }
        }

        private void chkUpdatePI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUpdatePI.CheckState == CheckState.Checked)            
                WriteIni("UpdatePI", "AutoUpdate", true);
            else
                WriteIni("UpdatePI", "AutoUpdate", false);                        
        }
        private void SetUserIP(string ip,int xtype)
        {
            switch (xtype)
            {
                case 0://ADD
                    if (!edERPIP.Properties.Items.Contains(ip))
                    {
                        edERPIP.Properties.Items.Add(ip);
                        edLocalDBIP.Properties.Items.Add(ip);
                    }
                    break;
                case 1://DEL
                    if (edERPIP.Properties.Items.Contains(ip))
                    {
                        edERPIP.Properties.Items.Remove(ip);
                        edLocalDBIP.Properties.Items.Remove(ip);
                    }
                    break;
                case 2:
                    edERPIP.Properties.Items.Clear();
                    edLocalDBIP.Properties.Items.Clear();
                    edERPIP.Properties.Items.Add("【127.0.0.1】 Local IP");
                    edERPIP.Properties.Items.Add("【" + lbl_LocalIP.Text + "】 " + lbl_PCName.Text);
                    edLocalDBIP.Properties.Items.Add("【127.0.0.1】 Local IP");
                    edLocalDBIP.Properties.Items.Add("【" + lbl_LocalIP.Text + "】 " + lbl_PCName.Text);   
                    break;
            }
        }
        private string FormatUserIP(string ip, string name)
        {
            string[] ips = fc.Split(".", ip);
            string tmp = "";
            for (int i = 0; i < ips.Length; i++)
            {
                tmp += fc.EmptyAtFirst(ips[i], 3)+".";
            }
            tmp = tmp.Substring(0, tmp.Length - 1);
            return "【" + ip + "】 " + name;
            //return "【" + tmp + "】 " + name;
            //return fc.EmptyAtEnd(name,20) + " " + "【" + ip + "】";
        }

        private void edERPIP_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            try
            {
                if (e.State != DrawItemState.Selected)
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString() , e.Appearance.Font, new SolidBrush(Color.FromArgb(255, 232, 166)), e.Bounds, e.Appearance.GetStringFormat());
                }
                else
                {
                    e.Cache.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                    //e.Cache.FillRectangle(new LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(220, 232, 166), LinearGradientMode.Horizontal), e.Bounds);
                    e.Cache.DrawString(e.Item.ToString(), e.Appearance.Font, new SolidBrush(Color.White),
                        e.Bounds, e.Appearance.GetStringFormat());
                }
                e.Handled = true;
            }
            catch (System.Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

            //Font f = new Font("YaHei Consolas Hybrid",e.Appearance.Font.Size, FontStyle.Bold);

        }

        private void edERPIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBoxEdit).SelectedIndex < 0)
            {
                return;
            }
            string s = (sender as ComboBoxEdit).Text;
            string[] s1 = fc.Split("】", s);
            string[] s2 = fc.Split("【", s1[0]);
            (sender as ComboBoxEdit).Text = s2[0];
        }

        private void tp06_PW_TextChanged(object sender, EventArgs e)
        {

        }

        private void BBIProgram_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnedPOSSave_Click(object sender, EventArgs e)
        {
            string filename = @fc.ConfigPath;
            string FType = "AutoLogin";
            IniConfigSource source = new IniConfigSource(filename);
            try
            {
                if (File.Exists(filename))
                {
                    source.Configs[FType].Set("ID", edPOSID.Text);
                    source.Configs[FType].Set("PW", edPOSPW.Text);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

            filename = fc.CustPath;
            string setupname = tb_SetupName.Text;
            source = new IniConfigSource(filename);
            try
            {
                {
                    source.Configs[setupname].Set("LoginID", edPOSID.Text);
                    source.Configs[setupname].Set("LoginPW", edPOSPW.Text);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

            filename = lbl_Cur_ProgPath.Text + @"\DSCPOSSetup.ini";
            setupname = "POS";
            StreamReader r = new StreamReader(filename, Encoding.Default);
            
            source = new IniConfigSource(r);
            //source = new IniConfigSource(filename);
            try
            {
                if (source.Configs[setupname]==null)                                
                {
                    source.Configs.Add(setupname);
                }
                if (barCheckItem1.Checked)
                {
                    source.Configs[setupname].Set("ShowTAXMB", "1");
                    source.Configs[setupname].Set("AutoLogin", "1");
                }
                source.Configs[setupname].Set("LoginID", edPOSID.Text);
                source.Configs[setupname].Set("LoginPW", edPOSPW.Text);
                StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
                ((IniConfigSource)source).Save(writer);
                r.Close();
                r.Dispose();
                //source.Save();

            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }
        }
        //TP07----------------------------------------------------------------------------------------------------
        public void LoadPGSections()
        {
            LoadPGNameInfo();
        }
        public bool LoadPGNameInfo()
        {
            string filename = fc.PGNamePath;
            if (!File.Exists(filename))
            {
                WriteDefaultPGName(filename);
            }
            IniConfigSource source = new IniConfigSource(filename);

            keylist34 = source.Configs["34"].GetValues();
            keylistGP2 = source.Configs["GP2"].GetValues();

            sectionlist = source.Configs["34"].GetKeys();
            sectionlist2 = source.Configs["GP2"].GetKeys();
            
            tp07_lb01.Items.Clear();
            try
            {
                for (int i = 0; i < sectionlist.Length; i++)
                {
                    tp07_lb01.Items.Add(sectionlist[i]);
                    tp07_lb01_List.Add(sectionlist[i]);
                }
                tp07_lb01.Font = new System.Drawing.Font("微軟正黑體", 14, FontStyle.Bold);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        private void WriteDefaultPGName(string fileName)
        {
            File.Create(fileName).Close();
            try
            {
                StreamWriter w = new StreamWriter(@fileName, false, Encoding.UTF8);
                string[] sr = fc.Split("\r\n", Properties.Resources.PGName);
                for (int i = 0; i < sr.Length; i++)
                {
                    w.WriteLine(sr[i]);
                }
                w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("存檔失敗!!\r\n" + ex.Message);
            }
        }
        private void tp07_tb01_EditValueChanged(object sender, EventArgs e)
        {
            tp07_lb01.Items.Clear();
            if (tp07_tb01.Text == "")
            {
                for (int i = 0; i < tp07_lb01_List.Count; i++)
                {
                    tp07_lb01.Items.Add(tp07_lb01_List[i]);
                }
                tp07_lb01.SelectedIndex = -1;
                return;
            }
            for (int i = 0; i < tp07_lb01_List.Count; i++)
            {
                int j = tp07_lb01_List[i].ToString().IndexOf(tp07_tb01.Text);
                if (j >= 0)
                {
                    tp07_lb01.Items.Add(tp07_lb01_List[i]);
                }
            }
            for (int i = 0; i < keylist34.Length; i++)
            {
                int j = keylist34[i].ToString().IndexOf(tp07_tb01.Text);
                if (j >= 0)
                {
                    tp07_lb01.Items.Add(keylist34[i]);
                }
            }
            for (int i = 0; i < keylistGP2.Length; i++)
            {
                int j = keylistGP2[i].ToString().IndexOf(tp07_tb01.Text);
                if (j >= 0)
                {
                    tp07_lb01.Items.Add(keylistGP2[i]);
                }
            }
        }
        private void tp07_lb01_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp07_lb01.SelectedIndex == -1)
            {
                tp07_tb02.Text = "";
                tp07_tb04.Text = "";
                tp07_tb03.Text = "";
                tp07_tb05.Text = "";

                return;
            }
            string filename = fc.PGNamePath;
            IniConfigSource source = new IniConfigSource(filename);
            tp07_tb02.Text = source.Configs["34"].GetString(tp07_lb01.Text, "");
            tp07_tb04.Text = tp07_lb01.Text;
            tp07_tb03.Text = source.Configs["GP2"].GetString(tp07_lb01.Text, "");
            tp07_tb05.Text = tp07_lb01.Text;

            if (tp07_tb02.Text == "")
            {
                for (int i = 0; i < keylist34.Length; i++)
                {
                    if (keylist34[i] == tp07_tb04.Text)
                    {
                        //tp07_tb02.Text = keylist34.Keys[i];
                        tp07_tb02.Text = sectionlist[i];
                        tp07_tb04.Text = keylist34[i];//ini.ReadString("34", tp07_tb02.Text, ""); ;
                    }
                }
            }

            if (tp07_tb03.Text == "")
            {
                for (int i = 0; i < keylistGP2.Length; i++)
                {
                    //if (keylistGP2.GetValues(i)[0] == tp07_tb05.Text)
                    if (keylistGP2[i] == tp07_tb05.Text)
                    {
                        tp07_tb03.Text = sectionlist2[i];
                        tp07_tb05.Text = keylistGP2[i];// ini.ReadString("GP2", tp07_tb03.Text, ""); ;
                    }
                }
            }

            if (tp07_tb02.Text == "" && tp07_tb03.Text != "")
            {
                tp07_tb04.Text = source.Configs["34"].GetString(tp07_tb03.Text, "");
                if (tp07_tb04.Text != "")
                {
                    for (int i = 0; i < keylist34.Length; i++)
                    {
                        if (keylist34[i] == tp07_tb04.Text)
                        {
                            tp07_tb02.Text = sectionlist[i];
                        }
                    }
                }
            }

            if (tp07_tb03.Text == "" && tp07_tb02.Text != "")
            {
                tp07_tb05.Text = source.Configs["GP2"].GetString(tp07_tb02.Text, "");
                if (tp07_tb05.Text != "")
                {
                    for (int i = 0; i < keylistGP2.Length; i++)
                    {
                        if (keylistGP2[i] == tp07_tb05.Text)
                        {
                            tp07_tb03.Text = sectionlist2[i];
                        }
                    }
                }
            }
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string filename = @fc.ConfigPath;
            string FType = "Options";
            IniConfigSource source = new IniConfigSource(filename);
            try
            {
                if (File.Exists(filename))
                {
                    source.Configs[FType].Set("IsPOSAutoLogin", barCheckItem1.Checked);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

            if (lbl_Cur_ProgPath.Text.Trim() != "")
            {
                filename = lbl_Cur_ProgPath.Text + @"\DSCPOSSetup.ini";
                StreamReader r = new StreamReader(filename, Encoding.Default);
                source = new IniConfigSource(r);
                string s = "";

                fc.SetAliasForNini(source);
                if (source.Configs["POS"] == null) source.Configs.Add("POS");
                try
                {
                    if (barCheckItem1.Checked)
                    {
                        source.Configs["POS"].Set("ShowTAXMB", "1");
                        source.Configs["POS"].Set("AutoLogin", "1");
                        if (edPOSID.Text == "" && edPOSPW.Text=="")
                        {
                            source.Configs["POS"].Set("LoginID", s);
                            source.Configs["POS"].Set("LoginPW", s); 
                        }
                        else
                        {
                            source.Configs["POS"].Set("LoginID", edPOSID.Text);
                            source.Configs["POS"].Set("LoginPW", edPOSPW.Text);
                        }
                    }
                    else
                    {
                        source.Configs["POS"].Set("ShowTAXMB", "0");
                        source.Configs["POS"].Set("AutoLogin", "0");
                    }
                    
                    StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
                    ((IniConfigSource)source).Save(writer);
                    r.Close();
                    r.Dispose();
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                }
            }
        }
        //選項-POS下載
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Option_POSDL op = new Option_POSDL();
            if (op.ShowDialog() == DialogResult.OK)
            {
                string[] FSetting = op.GetSettingInfo;
                string filename = @fc.ConfigPath;
                string FType = "Options_POSDL";
                IniConfigSource source = new IniConfigSource(filename);
                try
                {
                    if (source.Configs[FType] == null)
                    {
                        source.Configs.Add(FType);
                    }
                    if (File.Exists(filename))
                    {
                        source.Configs[FType].Set("UseDownLoadPath", FSetting[0]);
                        source.Configs[FType].Set("DownLoadPath", FSetting[1]);
                        source.Save();
                    }
                }
                catch (Exception ex)
                {
                    fc.WriteLog(ex.Message, true);
                    fc.ShowBoxMessage(ex.Message.ToString());
                    //throw;
                }
                //GetSettingInfo
            }
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string filename = @fc.ConfigPath;
            string FType = "Options";
            IniConfigSource source = new IniConfigSource(filename);
            try
            {
                if (File.Exists(filename))
                {
                    source.Configs[FType].Set("IsClearPI032", barCheckItem2.Checked);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
            }

        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerNoControl r = new VerNoControl();
            if (r.ShowDialog() == DialogResult.OK)
            {
                //20160718
                
                string tmpVerno = LoadConfigValue("VerNo", "No");
                if (tmpVerno != "")
                {
                    cbo_VerNo.Properties.Items.Clear();
                    string[] sp = fc.Split("|", tmpVerno);
                    for (int i = 0; i < sp.Length; i++)
                    {
                        cbo_VerNo.Properties.Items.Add(sp[i].Trim());
                    }
                    if (cbo_VerNo.Properties.Items.Count > 0)
                    {
                        cbo_VerNo.SelectedIndex = 0;
                    }
                }
                //20160718
            }
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            tp05_td05.Text = "";
            tp05_td06.Text = "";
            tp05_td07.Text = "";
            tp05_td08.Text = "";
            tp05_td09.Text = "";
            tp05_td10.Text = "";
            tp05_td18.Text = "";
        }
        //TP07----------------------------------------------------------------------------------------------------
    }//------------------------------------------------------------------------------------
}
