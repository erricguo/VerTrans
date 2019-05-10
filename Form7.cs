using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VerTrans
{
    public partial class Form7 : Form
    {
        List<string> VerInfo = new List<string>();
        Dictionary<string, string> DateInfo = new Dictionary<string, string>();
        Dictionary<string, string[]> DicVerInfo = new Dictionary<string, string[]>();
        public Form7()
        {
            InitializeComponent();
        }
        //副程式
        private void Init()
        {
            VerInfo = fc.LoadVerInfo();
            SetVerInfo(VerInfo);
            GetVerInfo();
        }
        private void SetVerInfo(List<string> xValue)
        {
            string ver = "";
            List<string> tmp = new List<string>();
            for (int i = 0; i < xValue.Count;i++ )
            {
                string[] sp = fc.Split("\r\n", xValue[i]);
                for (int j = 0; j < sp.Length; )
                {
                    if (sp[j].StartsWith("##"))
                    {
                        tmp.Clear();
                        string[] spt = fc.Split("##", sp[j]);
                        cbo_Ver.Items.Add(spt[0]);
                        ver = spt[0];
                        j++;
                    }
                    else if (sp[j].StartsWith("**"))
                    {
                        string[] spt = fc.Split("**", sp[j]);
                        DateInfo.Add(ver,spt[0]);
                        j++;
                    }
                    else 
                    {
                        tmp.Add(sp[j]);
                        j++;
                        if (j==sp.Length)
                        {
                            string[] aa = tmp.ToArray();
                            DicVerInfo.Add(ver, aa);
                            ver = "";          
                        }
                    }
                }
            }
        }
        private void GetVerInfo()
        {
            if (cbo_Ver.Items.Count > 0 )
            {
                if(cbo_Ver.SelectedIndex == -1)
                {
                    cbo_Ver.SelectedIndex = cbo_Ver.Items.Count-1;
                }
                string s = cbo_Ver.SelectedItem.ToString();
                tb_Date.Text = DateInfo[s];
                rtb01.Lines = DicVerInfo[s];

            }
            rtb01.Focus();
        }
        //--------------------------------------------------
        private void Form7_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void cbo_Ver_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetVerInfo();
        }
    }
}
