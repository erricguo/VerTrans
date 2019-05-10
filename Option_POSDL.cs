using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nini.Config;
using Nini.Ini;
using System.IO;

namespace VerTrans
{
    public partial class Option_POSDL : Form
    {
        string[] FSetting = new string[2];
        public Option_POSDL()
        {
            InitializeComponent();
        }

        public string[] GetSettingInfo
        {
            get
            {
                return FSetting;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(tb_Path.Text))
            {
                fbd01.SelectedPath = tb_Path.Text;
            }
            else
            {
                fbd01.SelectedPath = "C:\\";
            }

            if (fbd01.ShowDialog() == DialogResult.OK)
            {
                tb_Path.Text = fbd01.SelectedPath + "\\";
            }
        }

        private void Option_POSDL_Load(object sender, EventArgs e)
        {
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
                    string mbool = source.Configs[FType].Get("UseDownLoadPath", "F");
                    if (mbool == "T")
                    {
                        tb_Path.ReadOnly = false;
                        chkUse.Checked = true;
                        tb_Path.Text = source.Configs[FType].Get("DownLoadPath", "");
                        btnPath.Enabled = true;
                    }
                    else
                    {
                        tb_Path.ReadOnly = true;
                        chkUse.Checked = false;
                        tb_Path.Text = "";
                        btnPath.Enabled = false;
                    }                                       
                }
            }
            catch (Exception ex)
            {
                fc.WriteLog(ex.Message, true);
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tb_Path_Leave(object sender, EventArgs e)
        {
            if ((tb_Path.TextLength > 0) && (tb_Path.Text.Trim() != ""))
            {
                if (!fc.isDirectory(tb_Path.Text))
                {
                    fc.ShowMsg("此目錄不存在!", "錯誤", "0");
                    tb_Path.Focus();
                }
            }
        }

        private void chkUse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUse.Checked)
            {
                tb_Path.ReadOnly = false;
                btnPath.Enabled = true;
            }
            else
            {
                tb_Path.ReadOnly = true;
                btnPath.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FSetting[0] = fc.iif(chkUse.Checked, "T", "F").ToString();
            FSetting[1] = tb_Path.Text;
        }

        private void tb_Path_TextChanged(object sender, EventArgs e)
        {
            lbOutPut.Text = tb_Path.Text + "GP32\\歐萊德_20150706";
        }
    }
}
