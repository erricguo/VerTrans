namespace VerTrans
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Setup = new System.Windows.Forms.TextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tb_code = new System.Windows.Forms.TextBox();
            this.cbo_ver = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_F2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbo_ProgPath = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_VerNo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "設定檔名稱";
            // 
            // tb_Setup
            // 
            this.tb_Setup.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Setup.ForeColor = System.Drawing.Color.Black;
            this.tb_Setup.Location = new System.Drawing.Point(129, 14);
            this.tb_Setup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tb_Setup.Multiline = true;
            this.tb_Setup.Name = "tb_Setup";
            this.tb_Setup.Size = new System.Drawing.Size(437, 34);
            this.tb_Setup.TabIndex = 1;
            this.tb_Setup.Leave += new System.EventHandler(this.tb_Setup_Leave);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Ok.ForeColor = System.Drawing.Color.Black;
            this.btn_Ok.Location = new System.Drawing.Point(346, 185);
            this.btn_Ok.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(104, 56);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "確定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_Cancel.Location = new System.Drawing.Point(462, 185);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(104, 56);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // tb_code
            // 
            this.tb_code.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_code.ForeColor = System.Drawing.Color.Black;
            this.tb_code.Location = new System.Drawing.Point(129, 101);
            this.tb_code.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tb_code.Multiline = true;
            this.tb_code.Name = "tb_code";
            this.tb_code.Size = new System.Drawing.Size(189, 34);
            this.tb_code.TabIndex = 15;
            // 
            // cbo_ver
            // 
            this.cbo_ver.BackColor = System.Drawing.Color.PowderBlue;
            this.cbo_ver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_ver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_ver.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_ver.ForeColor = System.Drawing.Color.Black;
            this.cbo_ver.FormattingEnabled = true;
            this.cbo_ver.Items.AddRange(new object[] {
            "標準版",
            "服飾版",
            "餐飲版"});
            this.cbo_ver.Location = new System.Drawing.Point(377, 58);
            this.cbo_ver.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbo_ver.MaxLength = 2;
            this.cbo_ver.Name = "cbo_ver";
            this.cbo_ver.Size = new System.Drawing.Size(189, 34);
            this.cbo_ver.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 26);
            this.label2.TabIndex = 13;
            this.label2.Text = "個案代號";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(320, 61);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 26);
            this.label4.TabIndex = 12;
            this.label4.Text = "業態";
            // 
            // btn_F2
            // 
            this.btn_F2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_F2.ForeColor = System.Drawing.Color.Black;
            this.btn_F2.Location = new System.Drawing.Point(531, 144);
            this.btn_F2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_F2.Name = "btn_F2";
            this.btn_F2.Size = new System.Drawing.Size(35, 34);
            this.btn_F2.TabIndex = 21;
            this.btn_F2.Text = "...";
            this.btn_F2.UseVisualStyleBackColor = true;
            this.btn_F2.Click += new System.EventHandler(this.btn_F2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(29, 148);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 26);
            this.label5.TabIndex = 19;
            this.label5.Text = "程式路徑";
            // 
            // cbo_ProgPath
            // 
            this.cbo_ProgPath.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_ProgPath.ForeColor = System.Drawing.Color.Black;
            this.cbo_ProgPath.FormattingEnabled = true;
            this.cbo_ProgPath.Location = new System.Drawing.Point(129, 144);
            this.cbo_ProgPath.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbo_ProgPath.Name = "cbo_ProgPath";
            this.cbo_ProgPath.Size = new System.Drawing.Size(398, 34);
            this.cbo_ProgPath.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(71, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 26);
            this.label3.TabIndex = 23;
            this.label3.Text = "版號";
            // 
            // cbo_VerNo
            // 
            this.cbo_VerNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_VerNo.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_VerNo.ForeColor = System.Drawing.Color.Black;
            this.cbo_VerNo.FormattingEnabled = true;
            this.cbo_VerNo.Location = new System.Drawing.Point(129, 58);
            this.cbo_VerNo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbo_VerNo.Name = "cbo_VerNo";
            this.cbo_VerNo.Size = new System.Drawing.Size(189, 34);
            this.cbo_VerNo.TabIndex = 24;
            // 
            // Form2
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(573, 248);
            this.Controls.Add(this.cbo_VerNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo_ProgPath);
            this.Controls.Add(this.btn_F2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_code);
            this.Controls.Add(this.cbo_ver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.tb_Setup);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Setup;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox tb_code;
        private System.Windows.Forms.ComboBox cbo_ver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_F2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cbo_ProgPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_VerNo;

    }
}