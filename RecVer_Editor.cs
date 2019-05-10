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
    public partial class RecVer_Editor : Form
    {
        public RecVer_Editor()
        {
            InitializeComponent();
        }
        int FType = 0; //0:NONE 1:ADD 2:EDIT
        int FTable = 1; //1:Ver 2:NODE
        int FFocusedHendle = 0;
        string NowControl = "";
        string[] FStr = null;
        DataTable Fdt = null;
        public string SetCaption
        {
            set
            {
                this.Text = value;
            }

        }
        public string SetControl
        {
            set
            {
                NowControl = value;
            }
        }
        public int SetType
        {
            set
            {
                FType = value;
            }
        }
        public int SetTableNo
        {
            set
            {
                FTable = value;
                if (FTable == 1)
                {
                    this.Size = new System.Drawing.Size(274, 151);
                    btnOK.Location = new Point(52, 69);
                    btnNO.Location = new Point(131, 69);
                    lb01.Text = "版本編號";
                    cbo04.Location = new Point(116, 123);
                    SetControlVisible(false);
                    
                }
                else if (FTable == 2)
                {
                    this.Size = new System.Drawing.Size(274, 291);
                    btnOK.Location = new Point(52, 205);
                    btnNO.Location = new Point(131, 205);
                    lb01.Text = "編號類別";
                    cbo04.Location = new Point(116, 18);
                    SetControlVisible(true);
                    tb02.Focus();
                }
            }
        }
        public string[] SetValue
        {
            set
            {
                FStr = value;
                if (FType==2)
                {
                    if (FTable==1)
                    {
                        FFocusedHendle = Int32.Parse(value[0]);
                        tb01.Text = value[1];
                    }
                    else if (FTable == 2)
                    {
                        FFocusedHendle = Int32.Parse(value[0]);
                        cbo04.Text = value[1];
                        tb02.Text = value[2];
                        tb03.Text = value[3];
                        tb05.Text = value[4];                        
                    }
                }
            }
        }
        public DataTable SetTable
        {
            set
            {
                Fdt = value;
            }
        }
        public DataTable GetTable
        {
            get
            {
                return Fdt;
            }
        }
        private void SetControlVisible(bool mbool) 
        {
            cbo04.SelectedIndex = 0;
            lb02.Visible = mbool;
            lb03.Visible = mbool;
            lb04.Visible = mbool;
            tb01.Visible = !mbool;
            tb02.Visible = mbool;
            tb03.Visible = mbool;
            cbo04.Visible = mbool;
            tb05.Visible = mbool;
        }
        private void RecVer_Editor_Load(object sender, EventArgs e)
        {
            if (FType==1)
            {
                this.ActiveControl = tb01;
                tb01.Focus();
            }
            else if (FType==2)
            {
                this.ActiveControl = tb02;
                tb02.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (CheckValue())
            {
                if (FType==1)//新增
                {
                    if (FTable==1)
                    {
                        Fdt.Rows.Add(tb01.Text);
                        DataView dv = Fdt.DefaultView;
                        if (NowControl =="2")
                        {
                            dv.Sort = "Ver ASC";
                        }
                        
                        Fdt = dv.ToTable();
                    }
                    else if (FTable==2)
                    {
                        Fdt.Rows.Add(cbo04.Text,tb02.Text,tb03.Text,tb05.Text);
                        DataView dv = Fdt.DefaultView;
                        dv.Sort = "Type DESC,StartNo ASC,EndNo ASC";
                        Fdt = dv.ToTable();
                    }
                }
                else if (FType==2)
                {
                    if (FTable == 1)
                    {
                        Fdt.Rows[FFocusedHendle][0] = tb01.Text;
                        DataView dv = Fdt.DefaultView;
                        if (NowControl == "2")
                        {
                            dv.Sort = "Ver ASC";
                        }
                        Fdt = dv.ToTable();
                    }
                    else if (FTable == 2)
                    {
                        Fdt.Rows[FFocusedHendle][0] = cbo04.Text;
                        Fdt.Rows[FFocusedHendle][1] = tb02.Text;
                        Fdt.Rows[FFocusedHendle][2] = tb03.Text;
                        Fdt.Rows[FFocusedHendle][3] = tb05.Text;                        
                        DataView dv = Fdt.DefaultView;
                        dv.Sort = "Type DESC,StartNo ASC,EndNo ASC";
                        Fdt = dv.ToTable();
                    }                    
                }
                this.DialogResult = DialogResult.OK;
                Close();
            }            
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool CheckValue()
        {
            if (FType==1)//新增
            {
                if (FTable==1)
                {
                    for (int i = 0; i < Fdt.Rows.Count;i++ )
                    {
                        
                    }
                }
                else if (FTable == 1)
                {

                }
            }
            else if (FType==2)//修改
            {
            }
            return true;
        }
    }
}
