namespace VerTrans
{
    partial class BoxInput
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
            this.lb01 = new DevExpress.XtraEditors.LabelControl();
            this.tp05_OK = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tp05_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.tb01 = new DevExpress.XtraEditors.TextEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb01.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lb01
            // 
            this.lb01.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.lb01.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb01.Appearance.ForeColor = System.Drawing.Color.White;
            this.lb01.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb01.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb01.Location = new System.Drawing.Point(0, 0);
            this.lb01.Name = "lb01";
            this.lb01.Padding = new System.Windows.Forms.Padding(10);
            this.lb01.Size = new System.Drawing.Size(569, 55);
            this.lb01.TabIndex = 19;
            this.lb01.Text = "大於10以上請按[數量]處理!!";
            // 
            // tp05_OK
            // 
            this.tp05_OK.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_OK.Appearance.Options.UseFont = true;
            this.tp05_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tp05_OK.Location = new System.Drawing.Point(176, 10);
            this.tp05_OK.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_OK.Name = "tp05_OK";
            this.tp05_OK.Size = new System.Drawing.Size(93, 43);
            this.tp05_OK.TabIndex = 17;
            this.tp05_OK.Text = "確定";
            this.tp05_OK.Click += new System.EventHandler(this.tp05_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tp05_Cancel);
            this.panel1.Controls.Add(this.tp05_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 65);
            this.panel1.TabIndex = 20;
            // 
            // tp05_Cancel
            // 
            this.tp05_Cancel.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.tp05_Cancel.Appearance.Options.UseFont = true;
            this.tp05_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tp05_Cancel.Location = new System.Drawing.Point(309, 10);
            this.tp05_Cancel.LookAndFeel.SkinName = "Seven Classic";
            this.tp05_Cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tp05_Cancel.Name = "tp05_Cancel";
            this.tp05_Cancel.Size = new System.Drawing.Size(93, 43);
            this.tp05_Cancel.TabIndex = 18;
            this.tp05_Cancel.Text = "取消";
            // 
            // tb01
            // 
            this.tb01.Location = new System.Drawing.Point(12, 112);
            this.tb01.Name = "tb01";
            this.tb01.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb01.Properties.Appearance.Options.UseFont = true;
            this.tb01.Size = new System.Drawing.Size(545, 42);
            this.tb01.TabIndex = 21;
            // 
            // BoxInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(569, 233);
            this.Controls.Add(this.tb01);
            this.Controls.Add(this.lb01);
            this.Controls.Add(this.panel1);
            this.Name = "BoxInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "輸入";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb01.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lb01;
        private DevExpress.XtraEditors.SimpleButton tp05_OK;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton tp05_Cancel;
        private DevExpress.XtraEditors.TextEdit tb01;
    }
}