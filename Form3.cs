using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.Devices;

namespace VerTrans
{
    public partial class Form3 : Form
    {
        Computer myComputer = new Computer();
        string ConfigPath = "";
        string TmpPath = "";
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            ConfigPath = myComputer.FileSystem.SpecialDirectories.MyDocuments + @"\VerTrans\Config.ini";
            TmpPath = myComputer.FileSystem.SpecialDirectories.MyDocuments + @"\VerTrans\Config_Temp.ini";
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath).Close();
            }
            LB01.SelectedIndex = -1;
            LoadConfigIni("SetupPath");
        }
        public string[] PathList
        {
            get
            {
                string[] xValue = new string[LB01.Items.Count];
                for (int i = 0; i < LB01.Items.Count; i++)
                {
                    xValue[i] = LB01.Items[i].ToString();
                }
                return xValue;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "C:\\";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                LB01.Items.Add(folderBrowserDialog1.SelectedPath);
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            LB01.Items.Remove(LB01.SelectedItem);
        }

        private void LoadConfigIni(string ConfigName)
        {
            List<string> Tmp = fc.LoadConfigIni(ConfigName);
            for (int i = 0; i < Tmp.Count; i++)
            {
                LB01.Items.Add(Tmp[i]);
            }
        }
    }
}
