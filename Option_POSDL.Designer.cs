namespace VerTrans
{
    partial class Option_POSDL
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
            this.chkUse = new DevExpress.XtraEditors.CheckEdit();
            this.gc01 = new DevExpress.XtraEditors.GroupControl();
            this.lbOutPut = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.tb_Path = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnNO = new DevExpress.XtraEditors.SimpleButton();
            this.fbd01 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.chkUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc01)).BeginInit();
            this.gc01.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkUse
            // 
            this.chkUse.EditValue = true;
            this.chkUse.Location = new System.Drawing.Point(12, 6);
            this.chkUse.Name = "chkUse";
            this.chkUse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.chkUse.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold);
            this.chkUse.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.chkUse.Properties.Appearance.Options.UseBackColor = true;
            this.chkUse.Properties.Appearance.Options.UseFont = true;
            this.chkUse.Properties.Appearance.Options.UseForeColor = true;
            this.chkUse.Properties.Caption = "是否啟用";
            this.chkUse.Size = new System.Drawing.Size(119, 27);
            this.chkUse.TabIndex = 213;
            this.chkUse.CheckedChanged += new System.EventHandler(this.chkUse_CheckedChanged);
            // 
            // gc01
            // 
            this.gc01.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.gc01.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold);
            this.gc01.Appearance.Options.UseBackColor = true;
            this.gc01.Appearance.Options.UseFont = true;
            this.gc01.AppearanceCaption.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold);
            this.gc01.AppearanceCaption.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.gc01.AppearanceCaption.Options.UseFont = true;
            this.gc01.AppearanceCaption.Options.UseForeColor = true;
            this.gc01.Controls.Add(this.lbOutPut);
            this.gc01.Controls.Add(this.labelControl1);
            this.gc01.Controls.Add(this.labelControl19);
            this.gc01.Controls.Add(this.tb_Path);
            this.gc01.Controls.Add(this.btnPath);
            this.gc01.Location = new System.Drawing.Point(12, 39);
            this.gc01.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.gc01.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gc01.LookAndFeel.UseWindowsXPTheme = true;
            this.gc01.Name = "gc01";
            this.gc01.Size = new System.Drawing.Size(731, 129);
            this.gc01.TabIndex = 214;
            this.gc01.Text = "下載設定";
            this.gc01.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // lbOutPut
            // 
            this.lbOutPut.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbOutPut.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbOutPut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lbOutPut.Location = new System.Drawing.Point(135, 88);
            this.lbOutPut.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.lbOutPut.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lbOutPut.Name = "lbOutPut";
            this.lbOutPut.Size = new System.Drawing.Size(614, 22);
            this.lbOutPut.TabIndex = 114;
            this.lbOutPut.Text = "C:\\COSMOS_POS";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.SteelBlue;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Location = new System.Drawing.Point(19, 46);
            this.labelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.labelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(110, 22);
            this.labelControl1.TabIndex = 113;
            this.labelControl1.Text = "下載預設路徑";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl19.Appearance.BackColor2 = System.Drawing.Color.SteelBlue;
            this.labelControl19.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl19.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl19.Location = new System.Drawing.Point(59, 88);
            this.labelControl19.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.labelControl19.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(70, 22);
            this.labelControl19.TabIndex = 112;
            this.labelControl19.Text = "輸出範例";
            // 
            // tb_Path
            // 
            this.tb_Path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tb_Path.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Path.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.tb_Path.Location = new System.Drawing.Point(135, 43);
            this.tb_Path.Name = "tb_Path";
            this.tb_Path.Size = new System.Drawing.Size(539, 30);
            this.tb_Path.TabIndex = 4;
            this.tb_Path.TextChanged += new System.EventHandler(this.tb_Path_TextChanged);
            this.tb_Path.Leave += new System.EventHandler(this.tb_Path_Leave);
            // 
            // btnPath
            // 
            this.btnPath.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPath.ForeColor = System.Drawing.Color.Black;
            this.btnPath.Location = new System.Drawing.Point(680, 41);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(29, 32);
            this.btnPath.TabIndex = 6;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(537, 183);
            this.btnOK.LookAndFeel.SkinName = "Seven Classic";
            this.btnOK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 43);
            this.btnOK.TabIndex = 216;
            this.btnOK.Text = "確定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnNO
            // 
            this.btnNO.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btnNO.Appearance.Options.UseFont = true;
            this.btnNO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNO.Location = new System.Drawing.Point(643, 183);
            this.btnNO.LookAndFeel.SkinName = "Seven Classic";
            this.btnNO.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(100, 43);
            this.btnNO.TabIndex = 215;
            this.btnNO.Text = "取消";
            // 
            // Option_POSDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(755, 240);
            this.Controls.Add(this.chkUse);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnNO);
            this.Controls.Add(this.gc01);
            this.Name = "Option_POSDL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POS下載-設定";
            this.Load += new System.EventHandler(this.Option_POSDL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc01)).EndInit();
            this.gc01.ResumeLayout(false);
            this.gc01.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkUse;
        private DevExpress.XtraEditors.GroupControl gc01;
        private DevExpress.XtraEditors.LabelControl lbOutPut;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private System.Windows.Forms.TextBox tb_Path;
        private System.Windows.Forms.Button btnPath;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnNO;
        private System.Windows.Forms.FolderBrowserDialog fbd01;
    }
}