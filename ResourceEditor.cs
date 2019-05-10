using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;

namespace VerTrans
{
    public partial class ResourceEditor : Form
    {
        //RI RI2 ;//= new RI();
        DataTable dt;
        bool isDebug = false;
        Dictionary<int, Form1.ClassNo> ClassNo2 = new Dictionary<int, Form1.ClassNo>();
        int NumSelectIndex = -1;
        int NewFuncIndex = -1;
        
        public int GetNumSelectIndex
        {
            get { return NumSelectIndex; }
        }
        public int GetNewFuncIndex
        {
            get { return NewFuncIndex; }
        }
        public ComboBoxEdit Setcbo
        {
            set
            {
                tp04_cboClass.Properties.Items.Clear();
                ComboBoxEdit cbo = value;
                for (int i = 0; i < cbo.Properties.Items.Count;i++ )
                {
                    tp04_cboClass.Properties.Items.Add(cbo.Properties.Items[i]);
                }
            }
        }
        public ResourceEditor(bool xisDebug, string xCaption, ComboBoxEdit cbo)
        {
            InitializeComponent();
            SetCaption = xCaption;
            isDebug = xisDebug;
            Setcbo = cbo;
            Initcbo();
            lbMSG.Text = "";
            if (!isDebug)
            {
                lb00.Visible = false;
                RE_td00.Visible = false;
            }
        }
        private void ResourceEditor_Load(object sender, EventArgs e)
        {
            //Initcbo();
            lbMSG.Text = "";
            if (!isDebug)
            {
                lb00.Visible = false;
                RE_td00.Visible = false;
            }
        }
        private void Initcbo()
        {
            for (int i = 0; i < tp04_cboClass.Properties.Items.Count; i++)
            {
                string[] mt = fc.Split("[", tp04_cboClass.Properties.Items[i].ToString());
                string[] mt2 = fc.Split("]", mt[0]);
                string mt3 = fc.Split("]", mt[1])[0].Trim();
                string[] mt4 = fc.Split("~", mt3);
                if (mt4.Length > 1)
                    ClassNo2.Add(i, new Form1.ClassNo(mt2[0], mt4[0].Trim(), mt4[1].Trim()));
                else
                    ClassNo2.Add(i, new Form1.ClassNo(mt2[0], mt4[0].Trim() + "001", mt4[0].Trim() + "999"));
            }
            if (groupControl6.Text == "新增")
            {
                tp04_cboClass.Enabled = true;
                groupControl6.AppearanceCaption.ForeColor = Color.FromArgb(192, 192, 255);
                lb00.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb01.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb02.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb03.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb04.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb05.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
                lb06.Appearance.BackColor2 = Color.FromArgb(128, 128, 255);
            }
            else
            {
                RE_td03.Enabled = false;
                tp04_cboClass.Enabled = false;
                RE_td01.Enabled = false;
                gridControl1.Visible = false;
                groupControl6.Height = 457;
                this.Height = 515;
                RE_td04.Focus();
            }
        }
        public string SetCaption
        {
            set
            {
                groupControl6.Text = value;
            }
        }
        public string[] SetEdit
        {
            set
            {
                RE_td01.Text = value[0];
                RE_td02.Text = value[1];
                RE_td03.Text = value[2];
                RE_td04.Text = value[3];
                RE_td05.Text = "";// value[4];
                RE_td00.Text = value[5];
                RE_td07.Text = value[4];
            }
        }
        public string[] GetEdit
        {
            get
            {
                string[] s = new string[7];
                s[0] = RE_td01.Text;
                s[1] = RE_td02.Text;
                s[2] = RE_td03.Text;
                s[3] = RE_td04.Text;
                s[4] = RE_td05.Text;
                s[5] = RE_td00.Text;
                s[6] = RE_td07.Text;
                return s;
            }
        }
        public DataTable SetDataTable
        {
            set
            {
                dt = value;
                gridControl1.DataSource = dt;
            }
        }
 
        private void tp04_cboClass_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
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
            NumSelectIndex = tp04_cboClass.SelectedIndex;
            SearchPlace = NumSelectIndex;

        }
        public int SearchPlace
        {
            set
            {
                if (value < 0 )
                {
                    return;
                }
                int index = value;
                tp04_cboClass.SelectedIndex = value;
                string m1 = "";
                {
                    m1 = string.Format("[LangID] >= {0} and [LangID] <= {1} and ([Type] = {2} or [Type] = {3} ) ",
                        "'" + ClassNo2[index].mStartNo + "'",
                        "'" + ClassNo2[index].mEndNo + "'",
                        "'" + ClassNo2[index].mtype + "'",
                        "'" + ClassNo2[index].mtype + "M'"
                        );
                }

                gridView1.ActiveFilterString = m1;
                if (ClassNo2[index].mtype.StartsWith("S"))
                {
                    RE_td02.SelectedIndex = 0;
                }
                else if (ClassNo2[index].mtype.StartsWith("I"))
                {
                    RE_td02.SelectedIndex = 1;
                }
                else if (ClassNo2[index].mtype.StartsWith("E"))
                {
                    RE_td02.SelectedIndex = 2;
                }

                int xrow = 0;
                if (RE_td02.Text == "I")
                {
                    xrow = gridView1.RowCount - 3;
                }
                else if (RE_td02.Text == "E")
                {
                    xrow = gridView1.RowCount - 4;
                }
                else
                {
                    xrow = gridView1.RowCount - 1;
                }

                int Idx = -1;
                int Lie = -1;
                int LID = -1;
                //
                if (xrow==-1) //沒有資料，新建NODE會沒資料，找出在哪一個節點後面
                {
                    DataTable dt = (gridControl1.DataSource as DataTable);
                    int mStart = Int32.Parse(ClassNo2[index].mStartNo);
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //Console.WriteLine(i.ToString());
                            //if (ClassNo2[index].mtype == dt.Rows[i]["Type"].ToString())
                            if (dt.Rows[i]["Type"].ToString().StartsWith(ClassNo2[index].mtype))
                            {
                                int mEnd = Int32.Parse(dt.Rows[i]["LangID"].ToString());
                                {
                                    //if (i + 1 == dt.Rows.Count)
                                    {
                                        Idx = Int32.Parse(dt.Rows[i]["Index"].ToString()) + 1;
                                        Lie = Int32.Parse(dt.Rows[i]["Line"].ToString()) + 1;
                                        //LID = Int32.Parse(dt.Rows[i]["LangID"].ToString()) + 1;
                                        LID = mStart;
                                        /*RE_td00.Text = Idx.ToString();
                                        RE_td01.Text = Lie.ToString();
                                        RE_td03.Text = string.Format("{0:D6}", LID);*/
                                    }
                                    if (mStart < mEnd)
                                    {
                                        Idx = Int32.Parse(dt.Rows[i]["Index"].ToString()) + 1;
                                        Lie = Int32.Parse(dt.Rows[i]["Line"].ToString()) + 1;
                                        LID = Int32.Parse(dt.Rows[i]["LangID"].ToString()) + 1;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        fc.ShowBoxMessage(ex.Message);
                    }
                    
                }
                else
                {
                    if (index == 0)
                    {
                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            if (gridView1.GetRowCellValue(i, "Type").ToString() == "SM" &&
                                gridView1.GetRowCellValue(i, "LangStr").ToString().Contains("Reserved"))
                            {
                                Idx = Int32.Parse(gridView1.GetRowCellValue(i, "Index").ToString());
                                Lie = Int32.Parse(gridView1.GetRowCellValue(i, "Line").ToString());
                                LID = Int32.Parse(gridView1.GetRowCellValue(i, "LangID").ToString());
                                NewFuncIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Idx = Int32.Parse(gridView1.GetRowCellValue(xrow, "Index").ToString()) + 1;
                        Lie = Int32.Parse(gridView1.GetRowCellValue(xrow, "Line").ToString()) + 1;
                        LID = Int32.Parse(gridView1.GetRowCellValue(xrow, "LangID").ToString()) + 1;
                    }      
                }
                //          
                RE_td00.Text = Idx.ToString();
                RE_td01.Text = Lie.ToString();
                RE_td03.Text = string.Format("{0:D6}", LID);
            }
        }

        private void RE_td03_EditValueChanged(object sender, EventArgs e)
        {
            string xID = "";
            bool isRepeat = false;
            if (groupControl6.Text=="新增")
            {
                if (RE_td03.Text.Length == 6)
                {
                    for (int i = 0; i < gridView1.RowCount;i++ )
                    {
                        if (gridView1.GetRowCellValue(i, "LangID").ToString()== RE_td03.Text &&
                            gridView1.GetRowCellValue(i, "Type").ToString() == RE_td02.Text)
                        {
                            isRepeat = true;
                            break;  
                        }                            
                    }
                    if (!isRepeat)
                    {
                        if (gridView1.RowCount == 0)//新增節點沒資料
                        {
                            //NOTHING
                        }
                        else
                        {
                            lbMSG.Text = "";
                            RE_td01.Text = (Int32.Parse(gridView1.GetRowCellValue(0, "Line").ToString())).ToString();
                            RE_td00.Text = (Int32.Parse(gridView1.GetRowCellValue(0, "Index").ToString())).ToString();

                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                if (Int32.Parse(RE_td03.Text) > Int32.Parse(gridView1.GetRowCellValue(i, "LangID").ToString()))
                                {
                                    RE_td01.Text = (Int32.Parse(gridView1.GetRowCellValue(i, "Line").ToString()) + 1).ToString();
                                    RE_td00.Text = (Int32.Parse(gridView1.GetRowCellValue(i, "Index").ToString()) + 1).ToString();
                                }
                            }
                        }                        
                    }
                    else
                    {
                        lbMSG.Text = "輸入的LangID已存在!!";
                    }
                }
                else
                {
                    lbMSG.Text = "";
                }
            }
        }

        private void tp04_btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void RE_td05_Leave(object sender, EventArgs e)
        {
            if (RE_td05.Text.Trim().Length > 0)
            {
                if (!RE_td05.Text.Trim().StartsWith(@"//"))
                {
                    fc.ShowBoxMessage("【MarkInfo】必須輸入雙左斜線!!");
                    RE_td05.Focus();
                }
            }
        }

        private void RE_td07_Leave(object sender, EventArgs e)
        {
            if (RE_td07.Text.Trim().Length > 0)
            {
                if (groupControl6.Text != "新增")
                {
                    if (!RE_td07.Text.Trim().StartsWith(@"//"))
                    {
                        fc.ShowBoxMessage("【原資料註解】必須輸入雙左斜線!!");
                        RE_td07.Focus();
                    }
                }
            }
        }

    }
}
