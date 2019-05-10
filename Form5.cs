using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace VerTrans
{
    public partial class Form5 : Form
    {
        List<string[]> mValue = fc.ReadVerTransIni(fc.path);
        List<Button> btnList = new List<Button>();
        private enum Statu { None, Add, Edit,Start, Max };

        Dictionary<string, string[]> Dic = new Dictionary<string, string[]>();
        Dictionary<string, string[]> DicHotkey = new Dictionary<string, string[]>();
        List<Button> btnList1 = new List<Button>();
        Statu CurrentStatu = Statu.None;
        Dictionary<Button, object> MixList = new Dictionary<Button, object>();
        Dictionary<string, object> ApplyList = new Dictionary<string, object>();
        Dictionary<int, string[]> RegList = new Dictionary<int, string[]>();
        int CurrentKeyNum = 0;
        string[] DefInfo = new string[2];
        List<string> Editor = fc.LoadConfigIni("Editor");
        List<string> user = fc.LoadConfigIni("User");
        List<string> Account = fc.LoadConfigIni("Account");
        bool StartHotKey = false;
        public static IntPtr form5Handle = (IntPtr)0;
        Font f = new System.Drawing.Font("YaHei Consolas Hybrid", 9F);
        private enum MyKeys
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            Win = 8
        }

        const int WM_CHAR = 0X102;
        const int WM_HOTKEY = 0x312;
        const int WM_SETTEXT = 0x0c;
        const int WM_GETTEXT = 0x0d;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public Form5()
        {
            InitializeComponent();
        }
        //副程式
        private void Init()
        {
            dtp01.Value = DateTime.Today;
            for (int i = 0; i < mValue.Count;i++ )
            {
                cbo_CustName.Items.Add(mValue[i][0]);
                cbo_CustID.Items.Add(fc.iif(mValue[i][2]=="",mValue[i][0],mValue[i][2]));
            }
            btnList.Add(btn01);
            btnList.Add(btn02);
            btnList.Add(btn03);
            btnList.Add(btn04);
            btnList.Add(btn05);
            btnList.Add(btn06);
            btnList.Add(btn07);
            btnList.Add(btn08);
            btnList.Add(btn09);

            btnList1.Add(btnAdd);
            btnList1.Add(btnDel);
            btnList1.Add(btnEdit);
            btnList1.Add(btnExit);
            btnList1.Add(btnGO);
            btnList1.Add(btnSetup);
            btnList1.Add(btnApply);
            btnList1.Add(btnClear);
            //Dic = fc.LoadCodeIni(fc.CodePath); //20131223 mark Code 拿掉
            foreach (KeyValuePair<string, string[]> item in Dic)
            {
                LB01.Items.Add(item.Key);
            }

            MixList.Add(btnDate, dtp01);
            MixList.Add(btnEditer, cbo_Editer);
            MixList.Add(btnSeriesNo, tb_SeriesNo);
            MixList.Add(btnCustName, cbo_CustName);
            MixList.Add(btnCustID, cbo_CustID);
            ApplyList.Add("[日期]", dtp01);
            ApplyList.Add("[修改人員]", cbo_Editer);
            ApplyList.Add("[單號]", tb_SeriesNo);
            ApplyList.Add("[客戶簡稱]", cbo_CustName);
            ApplyList.Add("[客服代號]", cbo_CustID);
            //DicHotkey = fc.LoadHotkeyIni(fc.HotKeyPath); //20131223 mark Hotkey 拿掉

            int idx = -1;
            idx = cbo_CustName.FindStringExact(DefInfo[0]);
            if (idx != -1)
            {
                cbo_CustName.SelectedIndex = idx;
                idx = -1;
            }
            idx = cbo_CustID.FindStringExact(DefInfo[1]);
            if (idx != -1)
            {
                cbo_CustID.SelectedIndex = idx;
            }
            cbo_HotKey.SelectedIndex = 0;

            for (int i = 0; i < Editor.Count;i++ )
            {
                cbo_Editer.Items.Add(Editor[i]);
            }
            if (cbo_Editer.Items.Count <= 0)
                cbo_Editer.SelectedIndex = -1;
            else
            {
                if (cbo_Editer.FindStringExact(user[0]) != -1)
                    cbo_Editer.SelectedIndex = cbo_Editer.FindStringExact(user[0]);
                else
                    cbo_Editer.SelectedIndex = 0;
            }
                
        }
        private void StartEdit()
        {
            if ((CurrentStatu == Statu.Edit) && (LB01.SelectedItem == null)) return;
            if (CurrentStatu == Statu.Add)
            {
                tb_Name.Text = "";
                rtb01.Text = "";
            }
            foreach (Button b in btnList)
            {
                b.Enabled = false;
                b.BackColor = Color.FromName("control");
            }
            foreach (Button b in btnList1)
            {
                b.Enabled = false;
            }
            if (CurrentStatu == Statu.Add || CurrentStatu == Statu.Edit)
            {
                btnTick.Enabled = true;
                btnCross.Enabled = true;
            }
            if (CurrentStatu == Statu.Start)
            {
                btnGO.Enabled = true;
                btnGO.Text = "停  止";
            }
            
            LB01.Enabled = false;
        }
        private void EndEdit()
        {
            foreach (Button b in btnList)
            {
                b.Enabled = true;
                b.BackColor = Color.FromName("control");
            }
            foreach (Button b in btnList1)
            {
                b.Enabled = true;
                b.BackColor = Color.FromName("control");
            }
            btnTick.Enabled = false;
            btnCross.Enabled = false;
            tb_Name.Enabled = true;
            LB01.Enabled = true;
            btnGO.Text = "啟  動";
        }
        private void SearchDic()
        {
            if (LB01.SelectedItem == null) return;
            string s = LB01.SelectedItem.ToString();
            if (Dic.ContainsKey(s))
            {
                rtb01.Text = "";
                rtb01.Lines = Dic[s];
                tb_Name.Text = s;
            }
        }
        private void InsertTextToRTB(string xValue)
        {
            int RX, RY;
            RX = rtb01.SelectionStart;
            rtb01.Text = rtb01.Text.Insert(RX, xValue);
            rtb01.Focus();
            RY = rtb01.GetLineFromCharIndex(rtb01.SelectionStart) + 1;
            RX = RX + xValue.Length;
            rtb01.SelectionStart = RX;          
        }
        private void HotkeyON()
        {
            int RegNum = 200;
            foreach (KeyValuePair<string, string[]> item in DicHotkey)
            {
                MyKeys mKey = MyKeys.None;
                int NumPad = 96;
                if (item.Value.Length!=0 )
                {
                    if (item.Key.StartsWith("C"))
                        mKey = MyKeys.Ctrl;
                    else if (item.Key.StartsWith("A"))
                        mKey = MyKeys.Alt;
                    else if (item.Key.StartsWith("S"))
                        mKey = MyKeys.Shift;

                    NumPad += Int32.Parse(item.Key.Substring(item.Key.Length - 1, 1));
                    Keys k = (Keys)(NumPad);
                    RegisterHotKey(this.Handle, RegNum, (int)mKey, (int)(k)); //注册热键F2
                    RegList.Add(RegNum, item.Value);
                    RegNum += 100; 
                }
            }


        }
        private void HotkeyOFF()
        {
            foreach (KeyValuePair<int, string[]> item in RegList)
            {
                if (item.Value.Length != 0)
                {
                    UnregisterHotKey(this.Handle, item.Key); //注销热键 
                    
                }
            }
            RegList.Clear();
            
        }
        [DllImportAttribute("user32.dll", EntryPoint = "RegisterHotKey")]
        public static extern bool RegisterHotKey
        (
                IntPtr hWnd,        //要注册热键的窗口句柄      
                int id,             //热键编号      
                int fsModifiers,    //特殊键如：Ctrl，Alt，Shift，Window      
                int vk              //一般键如：A B C F1，F2 等      
        );
        [DllImportAttribute("user32.dll", EntryPoint = "UnregisterHotKey")]
        public static extern bool UnregisterHotKey
        (
                IntPtr hWnd,        //注册热键的窗口句柄      
                int id              //热键编号上面注册热键的编号      
        );
        private void SendKey(char[] c)
        {
            //char[] c = tb_PW.Text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if ((byte)c[i] >= 0x41 && (byte)c[i] <= 0x60)
                {
                    byte b = (byte)c[i];
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0, 0);//鍵下shift鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0x2, 0);//鍵下shift鍵。
                    continue;
                }
                if (c[i] >= 'a' && c[i] <= 'z')
                {
                    char ch = (char)(c[i] - 0x20);
                    byte b = (byte)ch;
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    continue;
                }
                if (((byte)c[i] >= 0x30 && (byte)c[i] <= 0x39))
                {
                    byte b = (byte)c[i];
                    keybd_event(b, MapVirtualKey(b, 0), 0, 0);//鍵下f鍵。
                    keybd_event(b, MapVirtualKey(b, 0), 0x2, 0);//放開f鍵。
                    continue;
                }

                bool shift;
                byte k = CheckVirtualKey(c[i], out shift);
                if (shift)
                {
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0, 0);//鍵下shift鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0, 0);//鍵下f鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0x2, 0);//放開f鍵。
                    keybd_event(0xa0, MapVirtualKey(0xa0, 0), 0x2, 0);//鍵下shift鍵。
                }
                else
                {
                    keybd_event(k, MapVirtualKey(k, 0), 0, 0);//鍵下f鍵。
                    keybd_event(k, MapVirtualKey(k, 0), 0x2, 0);//放開f鍵。
                }
            }
            keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0, 0);//鍵下f鍵。
            keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0x2, 0);//放開f鍵。
        }
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern byte MapVirtualKey(byte wCode, int wMap);
        private byte CheckVirtualKey(char c, out bool shift)
        {
            byte y = new byte();
            shift = false;
            switch (c)
            {
                case '`':
                    y = 0xC0;
                    break;
                case '~':
                    y = 0xC0;
                    shift = true;
                    break;
                case '!':
                    y = 0x31;
                    shift = true;
                    break;
                case '@':
                    y = 0x32;
                    shift = true;
                    break;
                case '#':
                    y = 0x33;
                    shift = true;
                    break;
                case '$':
                    y = 0x34;
                    shift = true;
                    break;
                case '%':
                    y = 0x35;
                    shift = true;
                    break;
                case '^':
                    y = 0x36;
                    shift = true;
                    break;
                case '&':
                    y = 0x37;
                    shift = true;
                    break;
                case '*':
                    y = 0x38;
                    shift = true;
                    break;
                case '(':
                    y = 0x39;
                    shift = true;
                    break;
                case ')':
                    y = 0x30;
                    shift = true;
                    break;
                case '-':
                    y = 0xBD;
                    break;
                case '_':
                    y = 0xBD;
                    shift = true;
                    break;
                case '=':
                    y = 0xBB;
                    break;
                case '+':
                    y = 0xBB;
                    shift = true;
                    break;
                case '[':
                    y = 0xDB;
                    break;
                case '{':
                    y = 0xDB;
                    shift = true;
                    break;
                case ']':
                    y = 0xDD;
                    break;
                case '}':
                    y = 0xDD;
                    shift = true;
                    break;
                case '\\':
                    y = 0xDC;
                    break;
                case '|':
                    y = 0xDC;
                    shift = true;
                    break;
                case ';':
                    y = 0xBA;
                    break;
                case ':':
                    y = 0xBA;
                    shift = true;
                    break;
                case '\'':
                    y = 0xDE;
                    break;
                case '"':
                    y = 0xDE;
                    shift = true;
                    break;
                case ',':
                    y = 0xBC;
                    break;
                case '<':
                    y = 0xBC;
                    shift = true;
                    break;
                case '.':
                    y = 0xBE;
                    break;
                case '>':
                    y = 0xBE;
                    shift = true;
                    break;
                case '/':
                    y = 0xBF;
                    break;
                case '?':
                    y = 0xBF;
                    shift = true;
                    break;
            }
            return y;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("User32.dll",EntryPoint="FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
        //根据坐标获取窗口句柄
        [DllImport("user32")]
        private static extern IntPtr WindowFromPoint(
        Point Point  //坐标
        );
        //試著讓指定視窗控制代碼的視窗取得焦點。
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //用於取得滑鼠游標在螢幕中的座標位置。
        [DllImport("User32")]
        public extern static bool GetCursorPos(out Point p);

        private IntPtr GetHwndOnClick()
        {
            Point p;
            if (GetCursorPos(out p))
            {
                return WindowFromPoint(p);
            }
            return (IntPtr)0;
        }
        [DllImport("user32.dll")]
        public static extern long SendMessage(IntPtr hWnd, uint msg, uint wparam, int text);

        //EX: 傳送字串： SendMessage(輸入欄位的Handle, WM_SETTEXT, 0, "你要送的字串" );
        //----------------------------------------------------------------------------------
        //SET-------------------------------------------------------------------------------
        public string[] SetEdit
        {
            set
            {
                DefInfo = value;
            }
        }
        //----------------------------------------------------------------------------------
        private void Form5_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < mValue.Count; i++)
            {
                if ((sender as ComboBox).SelectedItem.ToString() == mValue[i][0])
                {
                    cbo_CustID.SelectedItem = (mValue[i][2]);
                    return;
                }
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            foreach (Button b in btnList)
            {
                b.BackColor = Color.FromName("control");
            }
            (sender as Button).BackColor = Color.FromArgb(255, 255, 192);

            CurrentKeyNum = Int32.Parse((sender as Button).Text);
            if (cbo_HotKey.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                string hk = cbo_HotKey.Text+"-"+(sender as Button).Text;
                if (DicHotkey.ContainsKey(hk))
                {
                    rtb01.Text = "";
                    rtb01.Lines = DicHotkey[hk];
                }
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentStatu = Statu.Add;
            StartEdit();
            (sender as Button).BackColor = Color.FromArgb(192, 255, 192);
            tb_Name.Focus();
        }

        private void LB01_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchDic();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (fc.ShowConfirm("是否刪除<<" + tb_Name.Text + ">> ?", "詢問") == DialogResult.OK)
            {
                int oldIndex = LB01.SelectedIndex;
                Dic.Remove(LB01.SelectedItem.ToString());
                LB01.Items.RemoveAt(LB01.SelectedIndex);
                if (oldIndex >= LB01.Items.Count)
                {
                    LB01.SelectedIndex = oldIndex - 1;
                }
                else if (LB01.Items.Count == 0)
                {
                    LB01.SelectedIndex = -1;
                }
                else
                {
                    LB01.SelectedIndex = oldIndex;
                }
                //fc.WriteCodeIni(Dic); //20131223 mark Code 拿掉
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            CurrentStatu = Statu.Edit;
            StartEdit();
            (sender as Button).BackColor = Color.FromArgb(192, 192, 255);
            tb_Name.Enabled = false;
            rtb01.Focus();
        }

        private void btnTick_Click(object sender, EventArgs e)
        {
            if (CurrentStatu == Statu.Add)
            {
                bool isExist = false;
                foreach (KeyValuePair<string, string[]> item in Dic)
                {
                    if (item.Key == tb_Name.Text)
                    {
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    Dic.Add(tb_Name.Text, rtb01.Lines);
                    LB01.Items.Add(tb_Name.Text);
                    tb_Name.Text = "";
                    rtb01.Text = "";
                    //fc.WriteCodeIni(Dic); //20131223 mark Code 拿掉
                    EndEdit();
                }
                else
                {
                    fc.ShowBoxMessage("已存在名為<<" + tb_Name.Text + ">>的設定檔，請更改名稱!");
                    return;
                }
            }
            else if (CurrentStatu == Statu.Edit)
            {
                if (Dic.ContainsKey(tb_Name.Text))
                {
                    Dic[tb_Name.Text] = rtb01.Lines;
                    //fc.WriteCodeIni(Dic); //20131223 mark Code 拿掉
                    EndEdit();
                    return;
                }
            }
            CurrentStatu = Statu.None;
        }

        private void btnCross_Click(object sender, EventArgs e)
        {
            EndEdit();
            SearchDic();
        }
        private void InfoClick(object sender, EventArgs e)
        {
            InsertTextToRTB("["+(sender as Button).Text+"]");
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            if (cbo_HotKey.SelectedIndex == -1)
            {
                fc.ShowBoxMessage("沒有選取熱鍵!","警告");
                return;
            }
            else
            {
                bool isSelectBtn = false;
                foreach (Button b in btnList)
                {
                    if (b.BackColor == Color.FromArgb(255, 255, 192))
                    {
                        isSelectBtn = true;
                    }
                }
                if (!isSelectBtn)
                {
                    fc.ShowBoxMessage("沒有選取數字鍵!", "警告");
                    return;
                }
                string hk = cbo_HotKey.Text + "-" + CurrentKeyNum;
                if (DicHotkey.ContainsKey(hk))
                {
                    btnApply.PerformClick();
                    DicHotkey[hk] = rtb01.Lines;
                    //fc.WriteHotkeyIni(DicHotkey);//20131223 mark Hotkey 拿掉
                    fc.ShowBoxMessage(hk+"  設定完成!", "訊息");
                }
            }       
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            fc.user = cbo_Editer.Text;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rtb01.Lines.Length;i++ )
            {
                //string[] tmp = fc.Split(" ", rtb01.Lines[i]);
                //byte[] ch = (UnicodeEncoding.Default.GetBytes(rtb01.Lines[i]));
                char[] ch = rtb01.Lines[i].ToCharArray();
                List<string> tmp = new List<string>();
                string s = "";
                bool strCatch = false;
                for (int k = 0; k < ch.Length; k++)
                {
                    if (strCatch)
                    {
                        s += ch[k].ToString();
                        if (ch[k] == ']')
                        {
                            strCatch = false;
                            if (s != "")
                            {
                                tmp.Add(s);
                                s = "";
                            }
                        }
                    } 
                    else
                    {
                        if (ch[k] == '[')
                        {
                            strCatch = true;
                            if (s!="")
                            {
                                tmp.Add(s);
                            }
                            s = ch[k].ToString();
                            continue;
                        }
                        s += ch[k].ToString();
                    }         
                }

                for (int j = 0; j < tmp.Count;j++ )
                {
                    if (ApplyList.ContainsKey(tmp[j]))
                    {
                        if (ApplyList[tmp[j]] is DateTimePicker)
                        {
                            rtb01.Text = rtb01.Text.Replace(tmp[j], (ApplyList[tmp[j]] as DateTimePicker).Value.ToString("yyyyMMdd"));
                        }
                        else if (ApplyList[tmp[j]] is TextBox)
                        {
                            rtb01.Text = rtb01.Text.Replace(tmp[j], (ApplyList[tmp[j]] as TextBox).Text);
                        }
                        else if (ApplyList[tmp[j]] is ComboBox)
                        {
                            rtb01.Text = rtb01.Text.Replace(tmp[j], (ApplyList[tmp[j]] as ComboBox).Text);
                        }
                    }
                }
                
            }
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            if (StartHotKey)
            {
                CurrentStatu = Statu.None;
                HotkeyOFF();
                StartHotKey = false;
                EndEdit();
            }
            else
            {
                CurrentStatu = Statu.Start;
                btnGO.BackColor = Color.FromArgb(192, 192, 255);
                form5Handle = this.Handle;
                HotkeyON();
                StartHotKey = true;
                StartEdit();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {/*
                switch (m.WParam.ToInt32())
                {
                      
                    case 200:
                        Clipboard.SetData(DataFormats.Text, rtb01.Text);
                        SendKeys.Send("+V");
                        //SendKeys.Send("20121025 [修改人員] 123456 7987984");
                        break;
                    case 300:
                        SendKey(rtb01.Text.ToCharArray());
                        break;
                }*/
                if (RegList.ContainsKey(m.WParam.ToInt32()))
                {
                    string[] tmp = RegList[m.WParam.ToInt32()];
                    IntPtr p = GetHwndOnClick();
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        byte[] ch = (UnicodeEncoding.Default.GetBytes(tmp[i]));
                        for (int j = 0; j < ch.Length; j++)
                        {
                            SendMessage(p, WM_CHAR, ch[j], 0); 
                        }
                        
                        keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0, 0);//鍵下f鍵。
                        keybd_event(0x0D, MapVirtualKey(0x0D, 0), 0x2, 0);//放開f鍵。
                        Thread.Sleep(100);
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void Form5_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                form5Handle = this.Handle;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtb01_TextChanged(object sender, EventArgs e)
        {
            rtb01.Font = f;
        }

        private void cbo_HotKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_Name.Text = "";
            rtb01.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tb_Name.Text = "";
            rtb01.Text = "";
        }
    }
}
