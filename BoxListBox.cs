using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.IO;

namespace VerTrans
{
    public partial class BoxListBox : DevExpress.XtraEditors.XtraForm
    {
        List<string> Boxlist = new List<string>();
        string FMsg = "";
        public List<string> SetListBox
        {
            set
            {
                Boxlist = value;
            }
        }
        public string GetMsg
        {
            get { return FMsg; }
        }
        public BoxListBox()
        {
            InitializeComponent();
        }

        private void BoxListBox_Load(object sender, EventArgs e)
        {
            lbc01.Items.Clear();
            foreach (string s in Boxlist)
            {
                lbc01.Items.Add(s);
            }
        }

        private void tp05_OK_Click(object sender, EventArgs e)
        {
            string pp = "";
            try
            {
                // 取得本機端上執行中的應用程式
                foreach (Process p in Process.GetProcesses())
                {
                    foreach (string s in Boxlist)
                    {
                        if (p.ProcessName.ToUpper() == Path.GetFileNameWithoutExtension(s).ToUpper())  // 判斷 MainWindowHandle 為非零值的應用程式，表示有主視窗
                        {
                            FMsg += "[" + DateTime.Now.ToString() + "] " + "關閉程序<<" + p.ProcessName + ">>\r\n";
                            pp = p.ProcessName;
                            p.Kill();
                            fc.WriteLog("關閉程序<<" + p.ProcessName + ">>", true);
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                FMsg += "[" + DateTime.Now.ToString() + "] " + "關閉程序<<" + pp + ">> 時 出錯! " + ex.Message.ToString() + "\r\n";
                fc.ShowBoxMessage("關閉程序<<" + pp + ">> 時 出錯! " + ex.Message.ToString());
                fc.WriteLog("關閉程序<<" + pp + ">> 時 出錯! " + ex.Message.ToString(), true);
            }

        }

    }
}