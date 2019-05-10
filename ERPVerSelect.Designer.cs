namespace VerTrans
{
    partial class ERPVerSelect
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
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labelControl45
            // 
            this.labelControl45.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl45.Appearance.BackColor2 = System.Drawing.Color.SteelBlue;
            this.labelControl45.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelControl45.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl45.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl45.Location = new System.Drawing.Point(12, 12);
            this.labelControl45.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.labelControl45.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(258, 24);
            this.labelControl45.TabIndex = 112;
            this.labelControl45.Text = "選擇DSCSYS資料庫版本";
            // 
            // ERPVerSelect
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 272);
            this.Controls.Add(this.labelControl45);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ERPVerSelect";
            this.Text = "XtraForm1";
            this.Load += new System.EventHandler(this.ERPVerSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl45;


    }
}