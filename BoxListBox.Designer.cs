namespace VerTrans
{
    partial class BoxListBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbc01 = new DevExpress.XtraEditors.ListBoxControl();
            this.tp05_NO = new DevExpress.XtraEditors.SimpleButton();
            this.tp05_OK = new DevExpress.XtraEditors.SimpleButton();
            this.lb01 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lbc01)).BeginInit();
            this.SuspendLayout();
            // 
            // lbc01
            // 
            this.lbc01.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbc01.Appearance.Options.UseFont = true;
            this.lbc01.Location = new System.Drawing.Point(12, 53);
            this.lbc01.Name = "lbc01";
            this.lbc01.Size = new System.Drawing.Size(528, 237);
            this.lbc01.TabIndex = 0;
            // 
            // tp05_NO
            // 
            this.tp05_NO.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_NO.Appearance.Options.UseFont = true;
            this.tp05_NO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tp05_NO.Location = new System.Drawing.Point(283, 296);
            this.tp05_NO.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_NO.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_NO.Name = "tp05_NO";
            this.tp05_NO.Size = new System.Drawing.Size(93, 43);
            this.tp05_NO.TabIndex = 23;
            this.tp05_NO.Text = "取消";
            // 
            // tp05_OK
            // 
            this.tp05_OK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_OK.Appearance.Options.UseFont = true;
            this.tp05_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tp05_OK.Location = new System.Drawing.Point(172, 296);
            this.tp05_OK.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_OK.Name = "tp05_OK";
            this.tp05_OK.Size = new System.Drawing.Size(93, 43);
            this.tp05_OK.TabIndex = 22;
            this.tp05_OK.Text = "確定";
            this.tp05_OK.Click += new System.EventHandler(this.tp05_OK_Click);
            // 
            // lb01
            // 
            this.lb01.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb01.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb01.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb01.Location = new System.Drawing.Point(12, 12);
            this.lb01.Name = "lb01";
            this.lb01.Size = new System.Drawing.Size(501, 35);
            this.lb01.TabIndex = 24;
            this.lb01.Text = "以下檔案被使用中，是否自動關閉?";
            // 
            // BoxListBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 346);
            this.Controls.Add(this.lb01);
            this.Controls.Add(this.tp05_NO);
            this.Controls.Add(this.tp05_OK);
            this.Controls.Add(this.lbc01);
            this.Name = "BoxListBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BoxListBox";
            this.Load += new System.EventHandler(this.BoxListBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbc01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbc01;
        private DevExpress.XtraEditors.SimpleButton tp05_NO;
        private DevExpress.XtraEditors.SimpleButton tp05_OK;
        private DevExpress.XtraEditors.LabelControl lb01;
    }
}