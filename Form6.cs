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
    public partial class Form6 : Form
    {
        private enum Statu { None, Add, Edit, Max };
        Dictionary<string, string[]> Dic = new Dictionary<string, string[]>();
        List<Button> btnList = new List<Button>();
        Statu CurrentStatu = Statu.None;
        public Form6()
        {
            InitializeComponent();
        }
        //副程式--------------------------------------------------------------
        private void Init()
        {
            btnList.Add(btnAdd);
            btnList.Add(btnDel);
            btnList.Add(btnEdit);
            btnList.Add(btnExit);
            //Dic = fc.LoadCodeIni(fc.CodePath); //20131223 mark Code 拿掉
            foreach (KeyValuePair<string, string[]> item in Dic)
            {
                LB01.Items.Add(item.Key);
            }
        }
        private void StartEdit()
        {
            if ((CurrentStatu==Statu.Edit) && (LB01.SelectedItem == null)) return;
            if (CurrentStatu ==Statu.Add)
            {
                tb_Name.Text = "";
                rtb01.Text = "";
            }
            foreach (Button b in btnList)
            {
                b.Enabled = false;
            }
            btnTick.Enabled = true;
            btnCross.Enabled = true;
            LB01.Enabled = false;
        }
        private void EndEdit()
        {
            foreach (Button b in btnList)
            {
                b.Enabled = true;
                b.BackColor = Color.FromName("control");
            }
            btnTick.Enabled = false;
            btnCross.Enabled = false;
            tb_Name.Enabled = true;
            LB01.Enabled = true;
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
        //---------------------------------------------------------------------
        private void Form6_Load(object sender, EventArgs e)
        {
            Init();
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
            if(fc.ShowConfirm("是否刪除<<" + tb_Name.Text + ">> ?","詢問")==DialogResult.OK)
            {
                int oldIndex = LB01.SelectedIndex;
                Dic.Remove(LB01.SelectedItem.ToString());
                LB01.Items.RemoveAt(LB01.SelectedIndex);
                if (oldIndex >= LB01.Items.Count )
                {
                    LB01.SelectedIndex = oldIndex-1;
                }
                else if(LB01.Items.Count==0)
                {
                    LB01.SelectedIndex =  - 1;
                }
                else
                {
                    LB01.SelectedIndex = oldIndex;
                }
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
    }
}
