namespace VerTrans
{
    partial class MsnUserList
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
            this.lb_List = new DevExpress.XtraEditors.ListBoxControl();
            this.label4 = new System.Windows.Forms.Label();
            this.tp05_NO = new DevExpress.XtraEditors.SimpleButton();
            this.tp05_OK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lb_List)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_List
            // 
            this.lb_List.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.lb_List.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_List.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb_List.Appearance.Options.UseBackColor = true;
            this.lb_List.Appearance.Options.UseFont = true;
            this.lb_List.Appearance.Options.UseForeColor = true;
            this.lb_List.Location = new System.Drawing.Point(12, 33);
            this.lb_List.LookAndFeel.SkinName = "VS2010";
            this.lb_List.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lb_List.Name = "lb_List";
            this.lb_List.Size = new System.Drawing.Size(200, 300);
            this.lb_List.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 40;
            this.label4.Text = "在線人員清單";
            // 
            // tp05_NO
            // 
            this.tp05_NO.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_NO.Appearance.Options.UseFont = true;
            this.tp05_NO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tp05_NO.Location = new System.Drawing.Point(125, 339);
            this.tp05_NO.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_NO.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_NO.Name = "tp05_NO";
            this.tp05_NO.Size = new System.Drawing.Size(82, 33);
            this.tp05_NO.TabIndex = 42;
            this.tp05_NO.Text = "取消";
            this.tp05_NO.Click += new System.EventHandler(this.tp05_NO_Click);
            // 
            // tp05_OK
            // 
            this.tp05_OK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_OK.Appearance.Options.UseFont = true;
            this.tp05_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tp05_OK.Location = new System.Drawing.Point(16, 339);
            this.tp05_OK.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_OK.Name = "tp05_OK";
            this.tp05_OK.Size = new System.Drawing.Size(82, 33);
            this.tp05_OK.TabIndex = 41;
            this.tp05_OK.Text = "確定";
            this.tp05_OK.Click += new System.EventHandler(this.tp05_OK_Click);
            // 
            // MsnUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 380);
            this.Controls.Add(this.tp05_NO);
            this.Controls.Add(this.tp05_OK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_List);
            this.Name = "MsnUserList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "傳送設定檔";
            this.Load += new System.EventHandler(this.MsnUserList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lb_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lb_List;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton tp05_NO;
        private DevExpress.XtraEditors.SimpleButton tp05_OK;

    }
}