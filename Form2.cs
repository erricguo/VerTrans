using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using Nini.Config;

namespace VerTrans
{
    public partial class Form2 : Form
    {
        ListBoxControl lb = new ListBoxControl();
        private string Verno = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cbo_ver.SelectedIndex = 0;
            cbo_ProgPath.SelectedIndex = -1;
            //LoadConfigSetup(fc.GetIni(fc.ConfigPath));
            LoadConfigSetup("SetupPath");
            LoadVerNoForCbo("VerNo");
        }
        public string SetSetupName
        {
            set { tb_Setup.Text = value; }
        }
        public string SetVerNo
        {
            set 
            {
                Verno = value;
            }
        }
        public string Setcode
        {
            set
            {
                tb_code.Text = value;
            }
        }
        public string[] SetEdit
        {
            set
            {
                tb_Setup.Text = value[0];
                tb_Setup.Enabled = false;
                cbo_ver.SelectedIndex = Int32.Parse(value[1]);
                tb_code.Text = value[2];
                cbo_ProgPath.Text = value[3];
                cbo_VerNo.Text = value[4];
            }
        }
        public ListBoxControl SetList
        {

            set
            {
                lb = value;
                cbo_ver.SelectedIndex = 0;
            }
            
        }
        public string[] GetNew
        {
            get
            {
                string[] s = new string[5];
                s[0] = tb_Setup.Text;
                s[1] = cbo_ver.SelectedIndex.ToString();
                s[2] = tb_code.Text;
                s[3] = cbo_ProgPath.Text;
                s[4] = cbo_VerNo.Text;
                return s;
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (tb_Setup.Text == "")
            {
                fc.ShowBoxMessage("設定檔名稱不可空白", "錯誤訊息");
                tb_Setup.Focus();
                return;
            }
            if (cbo_ver.SelectedIndex < 0 || cbo_ver.SelectedIndex > 2)
            {
                fc.ShowBoxMessage("版本輸入錯誤", "錯誤訊息");
                cbo_ver.Focus();
                return;
            }
            if (cbo_VerNo.Text.Trim()=="")
            {
                fc.ShowBoxMessage("版號不可空白", "錯誤訊息");
                cbo_VerNo.Focus();
                return;
            }

            for (int i = 0; i < lb.Items.Count; i++)
            {
                if (tb_Setup.Text == lb.Items[i].ToString())
                {
                    fc.ShowBoxMessage("設定檔名稱重複，請重新輸入!!", "錯誤訊息");
                    tb_Setup.Focus();
                    return;
                }
            }

            tb_Setup.Enabled = true;
            this.DialogResult=DialogResult.OK;
        }

        private void tb_Setup_Leave(object sender, EventArgs e)
        {
            /*if (tb_Setup.Text == "") return;
            for (int i = 0; i < lb.Items.Count; i++)
            {
                if (tb_Setup.Text == lb.Items[i].ToString())
                {
                    fc.ShowBoxMessage("設定檔名稱重複，請重新輸入!!", "錯誤訊息");
                    tb_Setup.Focus();
                }
            }*/
        }

        private void btn_F2_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(cbo_ProgPath.Text))
            {
                folderBrowserDialog1.SelectedPath = cbo_ProgPath.Text;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = "C:\\";
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                cbo_ProgPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void LoadConfigSetup(string setupname)
        {
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            string[] tmp = source.Configs[setupname].GetValues();
            for (int i = 0; i < tmp.Length; i++)
            {
                cbo_ProgPath.Items.Add(tmp[i]);
            }
            if (cbo_ProgPath.Items.Count != 0)
            {
                cbo_ProgPath.SelectedIndex = 0;
            }
           /* string[] spread = new string[4];
            bool StartPath = false;
            cbo_ProgPath.Items.Clear();
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i] == "--SetupPath")
                {
                    StartPath = true;
                    continue;
                }
                else if (tmp[i] != "--SetupPath" && !StartPath)
                {
                    continue;
                }
                else if (tmp[i].StartsWith("--") && StartPath)
                {
                    break;
                }
                else if (tmp[i] == "")
                {
                    continue;
                }
                spread = fc.Split("=", tmp[i]);
                string s_tmp = spread[1];
                string s;
                s = (fc.iif(s_tmp == "^^", "", s_tmp)).ToString();
                cbo_ProgPath.Items.Add(s);
            }
            if (cbo_ProgPath.Items.Count != 0)
            {
                cbo_ProgPath.SelectedIndex = 0;
            }*/
        }
        private void LoadVerNoForCbo(string ConfigName)
        {
            IConfigSource source = new IniConfigSource(fc.ConfigPath);
            string[] Tmp = source.Configs[ConfigName].GetString("No").Split('|');
            //string Tmp1 = source.Configs[ConfigName].GetString("Default", "");


            //cbo_VerNo.Text = Tmp1;
            //NowSetupName = s;

            cbo_VerNo.Items.Clear();

            for (int i = 0; i < Tmp.Length; i++)
                if (Tmp[i] == "ALL")
                    //cbo_VerNo.Items.Add("");
                    continue;
                else
                    cbo_VerNo.Items.Add(Tmp[i]);

            for (int i = 0; i < cbo_VerNo.Items.Count; i++)
            {
                if (cbo_VerNo.Items[i].ToString() == Verno)
                {
                    cbo_VerNo.SelectedIndex = i;
                    return;
                }
            } 
           /* if (Tmp1.Trim() != "")
            {
                for (int i = 0; i < cbo_VerNo.Items.Count; i++)
                {
                    if (cbo_VerNo.Items[i].ToString() == Tmp1)
                    {
                        cbo_VerNo.SelectedIndex = i;
                        break;
                    }
                }
            }*/

        }
    }
}
