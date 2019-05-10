namespace VerTrans
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.dtp01 = new System.Windows.Forms.DateTimePicker();
            this.tb_SeriesNo = new System.Windows.Forms.TextBox();
            this.cbo_Editer = new System.Windows.Forms.ComboBox();
            this.cbo_CustName = new System.Windows.Forms.ComboBox();
            this.cbo_CustID = new System.Windows.Forms.ComboBox();
            this.btn07 = new System.Windows.Forms.Button();
            this.btn08 = new System.Windows.Forms.Button();
            this.btn09 = new System.Windows.Forms.Button();
            this.btn04 = new System.Windows.Forms.Button();
            this.btn05 = new System.Windows.Forms.Button();
            this.btn06 = new System.Windows.Forms.Button();
            this.btn01 = new System.Windows.Forms.Button();
            this.btn02 = new System.Windows.Forms.Button();
            this.btn03 = new System.Windows.Forms.Button();
            this.cbo_HotKey = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCross = new System.Windows.Forms.Button();
            this.btnTick = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rtb01 = new System.Windows.Forms.RichTextBox();
            this.LB01 = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.btnDate = new System.Windows.Forms.Button();
            this.btnEditer = new System.Windows.Forms.Button();
            this.btnSeriesNo = new System.Windows.Forms.Button();
            this.btnCustName = new System.Windows.Forms.Button();
            this.btnCustID = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp01
            // 
            this.dtp01.CustomFormat = "yyyy/MM/dd";
            this.dtp01.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtp01.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp01.Location = new System.Drawing.Point(284, 8);
            this.dtp01.Name = "dtp01";
            this.dtp01.Size = new System.Drawing.Size(122, 27);
            this.dtp01.TabIndex = 6;
            // 
            // tb_SeriesNo
            // 
            this.tb_SeriesNo.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_SeriesNo.Location = new System.Drawing.Point(284, 78);
            this.tb_SeriesNo.Name = "tb_SeriesNo";
            this.tb_SeriesNo.Size = new System.Drawing.Size(122, 27);
            this.tb_SeriesNo.TabIndex = 8;
            // 
            // cbo_Editer
            // 
            this.cbo_Editer.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_Editer.FormattingEnabled = true;
            this.cbo_Editer.Location = new System.Drawing.Point(284, 44);
            this.cbo_Editer.Name = "cbo_Editer";
            this.cbo_Editer.Size = new System.Drawing.Size(84, 24);
            this.cbo_Editer.TabIndex = 12;
            // 
            // cbo_CustName
            // 
            this.cbo_CustName.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_CustName.FormattingEnabled = true;
            this.cbo_CustName.Location = new System.Drawing.Point(284, 114);
            this.cbo_CustName.Name = "cbo_CustName";
            this.cbo_CustName.Size = new System.Drawing.Size(122, 24);
            this.cbo_CustName.TabIndex = 13;
            this.cbo_CustName.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // cbo_CustID
            // 
            this.cbo_CustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_CustID.FormattingEnabled = true;
            this.cbo_CustID.Location = new System.Drawing.Point(284, 149);
            this.cbo_CustID.Name = "cbo_CustID";
            this.cbo_CustID.Size = new System.Drawing.Size(122, 24);
            this.cbo_CustID.TabIndex = 14;
            // 
            // btn07
            // 
            this.btn07.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn07.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn07.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn07.Location = new System.Drawing.Point(27, 37);
            this.btn07.Name = "btn07";
            this.btn07.Size = new System.Drawing.Size(45, 39);
            this.btn07.TabIndex = 15;
            this.btn07.Text = "7";
            this.btn07.UseVisualStyleBackColor = true;
            this.btn07.Click += new System.EventHandler(this.btnClick);
            // 
            // btn08
            // 
            this.btn08.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn08.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn08.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn08.Location = new System.Drawing.Point(78, 37);
            this.btn08.Name = "btn08";
            this.btn08.Size = new System.Drawing.Size(45, 39);
            this.btn08.TabIndex = 16;
            this.btn08.Text = "8";
            this.btn08.UseVisualStyleBackColor = true;
            this.btn08.Click += new System.EventHandler(this.btnClick);
            // 
            // btn09
            // 
            this.btn09.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn09.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn09.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn09.Location = new System.Drawing.Point(129, 37);
            this.btn09.Name = "btn09";
            this.btn09.Size = new System.Drawing.Size(45, 39);
            this.btn09.TabIndex = 17;
            this.btn09.Text = "9";
            this.btn09.UseVisualStyleBackColor = true;
            this.btn09.Click += new System.EventHandler(this.btnClick);
            // 
            // btn04
            // 
            this.btn04.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn04.Location = new System.Drawing.Point(27, 84);
            this.btn04.Name = "btn04";
            this.btn04.Size = new System.Drawing.Size(45, 39);
            this.btn04.TabIndex = 20;
            this.btn04.Text = "4";
            this.btn04.UseVisualStyleBackColor = true;
            this.btn04.Click += new System.EventHandler(this.btnClick);
            // 
            // btn05
            // 
            this.btn05.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn05.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn05.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn05.Location = new System.Drawing.Point(78, 84);
            this.btn05.Name = "btn05";
            this.btn05.Size = new System.Drawing.Size(45, 39);
            this.btn05.TabIndex = 19;
            this.btn05.Text = "5";
            this.btn05.UseVisualStyleBackColor = true;
            this.btn05.Click += new System.EventHandler(this.btnClick);
            // 
            // btn06
            // 
            this.btn06.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn06.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn06.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn06.Location = new System.Drawing.Point(129, 84);
            this.btn06.Name = "btn06";
            this.btn06.Size = new System.Drawing.Size(45, 39);
            this.btn06.TabIndex = 18;
            this.btn06.Text = "6";
            this.btn06.UseVisualStyleBackColor = true;
            this.btn06.Click += new System.EventHandler(this.btnClick);
            // 
            // btn01
            // 
            this.btn01.BackColor = System.Drawing.SystemColors.Control;
            this.btn01.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn01.ForeColor = System.Drawing.Color.Black;
            this.btn01.Location = new System.Drawing.Point(27, 131);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(45, 39);
            this.btn01.TabIndex = 23;
            this.btn01.Text = "1";
            this.btn01.UseVisualStyleBackColor = false;
            this.btn01.Click += new System.EventHandler(this.btnClick);
            // 
            // btn02
            // 
            this.btn02.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn02.Location = new System.Drawing.Point(78, 131);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(45, 39);
            this.btn02.TabIndex = 22;
            this.btn02.Text = "2";
            this.btn02.UseVisualStyleBackColor = true;
            this.btn02.Click += new System.EventHandler(this.btnClick);
            // 
            // btn03
            // 
            this.btn03.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn03.Location = new System.Drawing.Point(129, 131);
            this.btn03.Name = "btn03";
            this.btn03.Size = new System.Drawing.Size(45, 39);
            this.btn03.TabIndex = 21;
            this.btn03.Text = "3";
            this.btn03.UseVisualStyleBackColor = true;
            this.btn03.Click += new System.EventHandler(this.btnClick);
            // 
            // cbo_HotKey
            // 
            this.cbo_HotKey.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbo_HotKey.FormattingEnabled = true;
            this.cbo_HotKey.Items.AddRange(new object[] {
            "CTRL",
            "ALT"});
            this.cbo_HotKey.Location = new System.Drawing.Point(284, 184);
            this.cbo_HotKey.Name = "cbo_HotKey";
            this.cbo_HotKey.Size = new System.Drawing.Size(122, 24);
            this.cbo_HotKey.TabIndex = 24;
            this.cbo_HotKey.SelectedIndexChanged += new System.EventHandler(this.cbo_HotKey_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(240, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 25;
            this.label6.Text = "熱鍵";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn08);
            this.groupBox1.Controls.Add(this.btn07);
            this.groupBox1.Controls.Add(this.btn09);
            this.groupBox1.Controls.Add(this.btn01);
            this.groupBox1.Controls.Add(this.btn06);
            this.groupBox1.Controls.Add(this.btn02);
            this.groupBox1.Controls.Add(this.btn05);
            this.groupBox1.Controls.Add(this.btn03);
            this.groupBox1.Controls.Add(this.btn04);
            this.groupBox1.Location = new System.Drawing.Point(427, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 200);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "數字鍵盤";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(209, 410);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 28;
            this.label7.Text = "字串設定";
            // 
            // btnGO
            // 
            this.btnGO.BackColor = System.Drawing.SystemColors.Control;
            this.btnGO.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGO.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGO.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGO.Location = new System.Drawing.Point(644, 15);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(116, 55);
            this.btnGO.TabIndex = 29;
            this.btnGO.Text = "啟  動";
            this.btnGO.UseVisualStyleBackColor = false;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // btnSetup
            // 
            this.btnSetup.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetup.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSetup.Location = new System.Drawing.Point(644, 85);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(116, 55);
            this.btnSetup.TabIndex = 31;
            this.btnSetup.Text = "設  定";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(209, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 33;
            this.label8.Text = "片語名稱";
            // 
            // btnCross
            // 
            this.btnCross.Enabled = false;
            this.btnCross.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCross.FlatAppearance.BorderSize = 0;
            this.btnCross.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCross.Image = global::VerTrans.Properties.Resources._1351127915_button_cross_red_副本1;
            this.btnCross.Location = new System.Drawing.Point(317, 238);
            this.btnCross.Name = "btnCross";
            this.btnCross.Size = new System.Drawing.Size(28, 28);
            this.btnCross.TabIndex = 39;
            this.btnCross.UseVisualStyleBackColor = true;
            this.btnCross.Click += new System.EventHandler(this.btnCross_Click);
            // 
            // btnTick
            // 
            this.btnTick.Enabled = false;
            this.btnTick.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTick.FlatAppearance.BorderSize = 0;
            this.btnTick.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnTick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTick.Image = global::VerTrans.Properties.Resources._1351127998_accepted_48_副本;
            this.btnTick.Location = new System.Drawing.Point(285, 238);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(28, 28);
            this.btnTick.TabIndex = 38;
            this.btnTick.UseVisualStyleBackColor = true;
            this.btnTick.Click += new System.EventHandler(this.btnTick_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Location = new System.Drawing.Point(644, 153);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(116, 55);
            this.btnExit.TabIndex = 37;
            this.btnExit.Text = "離  開";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(465, 238);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 28);
            this.btnEdit.TabIndex = 36;
            this.btnEdit.Text = "修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(412, 238);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(47, 28);
            this.btnDel.TabIndex = 35;
            this.btnDel.Text = "刪除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(359, 238);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(47, 28);
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Text = "加入";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // rtb01
            // 
            this.rtb01.ContextMenuStrip = this.contextMenuStrip1;
            this.rtb01.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtb01.Location = new System.Drawing.Point(284, 304);
            this.rtb01.Name = "rtb01";
            this.rtb01.Size = new System.Drawing.Size(476, 228);
            this.rtb01.TabIndex = 40;
            this.rtb01.Text = "";
            this.rtb01.WordWrap = false;
            this.rtb01.TextChanged += new System.EventHandler(this.rtb01_TextChanged);
            // 
            // LB01
            // 
            this.LB01.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB01.FormattingEnabled = true;
            this.LB01.ItemHeight = 16;
            this.LB01.Location = new System.Drawing.Point(12, 32);
            this.LB01.Name = "LB01";
            this.LB01.Size = new System.Drawing.Size(175, 500);
            this.LB01.TabIndex = 41;
            this.LB01.SelectedIndexChanged += new System.EventHandler(this.LB01_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(12, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "片語設定檔";
            // 
            // tb_Name
            // 
            this.tb_Name.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Name.Location = new System.Drawing.Point(284, 272);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(476, 27);
            this.tb_Name.TabIndex = 43;
            // 
            // btnDate
            // 
            this.btnDate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDate.Location = new System.Drawing.Point(220, 7);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(60, 27);
            this.btnDate.TabIndex = 44;
            this.btnDate.Text = "日期";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.InfoClick);
            // 
            // btnEditer
            // 
            this.btnEditer.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEditer.Location = new System.Drawing.Point(200, 43);
            this.btnEditer.Name = "btnEditer";
            this.btnEditer.Size = new System.Drawing.Size(80, 27);
            this.btnEditer.TabIndex = 45;
            this.btnEditer.Text = "修改人員";
            this.btnEditer.UseVisualStyleBackColor = true;
            this.btnEditer.Click += new System.EventHandler(this.InfoClick);
            // 
            // btnSeriesNo
            // 
            this.btnSeriesNo.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSeriesNo.Location = new System.Drawing.Point(220, 78);
            this.btnSeriesNo.Name = "btnSeriesNo";
            this.btnSeriesNo.Size = new System.Drawing.Size(60, 27);
            this.btnSeriesNo.TabIndex = 46;
            this.btnSeriesNo.Text = "單號";
            this.btnSeriesNo.UseVisualStyleBackColor = true;
            this.btnSeriesNo.Click += new System.EventHandler(this.InfoClick);
            // 
            // btnCustName
            // 
            this.btnCustName.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCustName.Location = new System.Drawing.Point(200, 113);
            this.btnCustName.Name = "btnCustName";
            this.btnCustName.Size = new System.Drawing.Size(80, 27);
            this.btnCustName.TabIndex = 47;
            this.btnCustName.Text = "客戶簡稱";
            this.btnCustName.UseVisualStyleBackColor = true;
            this.btnCustName.Click += new System.EventHandler(this.InfoClick);
            // 
            // btnCustID
            // 
            this.btnCustID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCustID.Location = new System.Drawing.Point(200, 148);
            this.btnCustID.Name = "btnCustID";
            this.btnCustID.Size = new System.Drawing.Size(80, 27);
            this.btnCustID.TabIndex = 48;
            this.btnCustID.Text = "客服代號";
            this.btnCustID.UseVisualStyleBackColor = true;
            this.btnCustID.Click += new System.EventHandler(this.InfoClick);
            // 
            // btnApply
            // 
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(518, 238);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(47, 28);
            this.btnApply.TabIndex = 49;
            this.btnApply.Text = "套用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(571, 238);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(47, 28);
            this.btnClear.TabIndex = 50;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 540);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCustID);
            this.Controls.Add(this.btnCustName);
            this.Controls.Add(this.btnSeriesNo);
            this.Controls.Add(this.btnEditer);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.tb_Name);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.LB01);
            this.Controls.Add(this.rtb01);
            this.Controls.Add(this.btnCross);
            this.Controls.Add(this.btnTick);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbo_HotKey);
            this.Controls.Add(this.cbo_CustID);
            this.Controls.Add(this.cbo_CustName);
            this.Controls.Add(this.cbo_Editer);
            this.Controls.Add(this.tb_SeriesNo);
            this.Controls.Add(this.dtp01);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(784, 576);
            this.MinimumSize = new System.Drawing.Size(784, 576);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "常用片語設定";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.SizeChanged += new System.EventHandler(this.Form5_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form5_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp01;
        private System.Windows.Forms.TextBox tb_SeriesNo;
        private System.Windows.Forms.ComboBox cbo_Editer;
        private System.Windows.Forms.ComboBox cbo_CustName;
        private System.Windows.Forms.ComboBox cbo_CustID;
        private System.Windows.Forms.Button btn07;
        private System.Windows.Forms.Button btn08;
        private System.Windows.Forms.Button btn09;
        private System.Windows.Forms.Button btn04;
        private System.Windows.Forms.Button btn05;
        private System.Windows.Forms.Button btn06;
        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Button btn02;
        private System.Windows.Forms.Button btn03;
        private System.Windows.Forms.ComboBox cbo_HotKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCross;
        private System.Windows.Forms.Button btnTick;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.RichTextBox rtb01;
        private System.Windows.Forms.ListBox LB01;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_Name;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.Button btnEditer;
        private System.Windows.Forms.Button btnSeriesNo;
        private System.Windows.Forms.Button btnCustName;
        private System.Windows.Forms.Button btnCustID;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}