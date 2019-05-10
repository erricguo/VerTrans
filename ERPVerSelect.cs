using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VerTrans
{ 
    public partial class ERPVerSelect : DevExpress.XtraEditors.XtraForm
    {
        public ERPVerSelect()
        {
            InitializeComponent();
        }
        int StartX = 12;
        int StartY = 42;
        int offsetX = 6;
        int offsetY = 6;
        int FBtnCount = 0;
        int PositionX = 0;
        int PositionY = 0;
        int ColumnsCount = 3;
        public int SetBtnCount
        {
            set { FBtnCount = value + 1; }
        }
        private void CreateBtn()
        {
            for (int i = 0; i < FBtnCount; i++)
            {
                if (i > 0 && i % ColumnsCount == 0)
                {
                    PositionY += 1;
                }
                PositionX = i % ColumnsCount;
               
                DevExpress.XtraEditors.SimpleButton g = new DevExpress.XtraEditors.SimpleButton();                
                g.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                g.Appearance.Options.UseFont = true;
                g.DialogResult = System.Windows.Forms.DialogResult.OK;
                g.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
                g.Location = new System.Drawing.Point(StartX + PositionX*(82+offsetX), StartY+PositionY*(52+offsetY));
                g.LookAndFeel.SkinName = "Office 2010 Silver";
                g.LookAndFeel.UseDefaultLookAndFeel = false;
                g.LookAndFeel.UseWindowsXPTheme = true;
                g.Name = i.ToString();
                g.Size = new System.Drawing.Size(82, 52);
                g.TabIndex = i;
                g.Text = i.ToString();
                if (i==FBtnCount-1)
                {
                    if (i % ColumnsCount!=0)
                    {
                        PositionY++;
                        
                    }
                    g.Location = new System.Drawing.Point(StartX + 1 * (82 + offsetX), StartY + (PositionY) * (52 + offsetY));

                    g.LookAndFeel.UseWindowsXPTheme = false;
                    g.LookAndFeel.SkinName = "DevExpress Dark Style";
                    g.Image = global::VerTrans.Properties.Resources._123;
                    g.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
                    g.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                this.Controls.Add(g);
                this.Height = g.Location.Y + g.Height + offsetY;// +StartY * 3;
            }
        }

        private void ERPVerSelect_Load(object sender, EventArgs e)
        {
            
            CreateBtn();
        }
    }
}