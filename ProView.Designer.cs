namespace VerTrans
{
    partial class ProView
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
            this.lb_info = new System.Windows.Forms.Label();
            this.pb_file = new System.Windows.Forms.ProgressBar();
            this.pb_main = new System.Windows.Forms.ProgressBar();
            this.lb_main = new System.Windows.Forms.Label();
            this.lb_file = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_info
            // 
            this.lb_info.AutoSize = true;
            this.lb_info.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_info.Location = new System.Drawing.Point(215, 39);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(36, 16);
            this.lb_info.TabIndex = 5;
            this.lb_info.Text = "INFO";
            // 
            // pb_file
            // 
            this.pb_file.Location = new System.Drawing.Point(12, 58);
            this.pb_file.Name = "pb_file";
            this.pb_file.Size = new System.Drawing.Size(462, 23);
            this.pb_file.Step = 1;
            this.pb_file.TabIndex = 4;
            // 
            // pb_main
            // 
            this.pb_main.Location = new System.Drawing.Point(12, 13);
            this.pb_main.Name = "pb_main";
            this.pb_main.Size = new System.Drawing.Size(462, 23);
            this.pb_main.Step = 1;
            this.pb_main.TabIndex = 3;
            // 
            // lb_main
            // 
            this.lb_main.AutoSize = true;
            this.lb_main.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_main.Location = new System.Drawing.Point(476, 16);
            this.lb_main.Name = "lb_main";
            this.lb_main.Size = new System.Drawing.Size(22, 16);
            this.lb_main.TabIndex = 6;
            this.lb_main.Text = "0%";
            // 
            // lb_file
            // 
            this.lb_file.AutoSize = true;
            this.lb_file.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_file.Location = new System.Drawing.Point(476, 61);
            this.lb_file.Name = "lb_file";
            this.lb_file.Size = new System.Drawing.Size(22, 16);
            this.lb_file.TabIndex = 7;
            this.lb_file.Text = "0%";
            // 
            // ProView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(514, 94);
            this.Controls.Add(this.lb_file);
            this.Controls.Add(this.lb_main);
            this.Controls.Add(this.lb_info);
            this.Controls.Add(this.pb_file);
            this.Controls.Add(this.pb_main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProView";
            this.Shown += new System.EventHandler(this.ProView_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_info;
        private System.Windows.Forms.ProgressBar pb_file;
        private System.Windows.Forms.ProgressBar pb_main;
        private System.Windows.Forms.Label lb_main;
        private System.Windows.Forms.Label lb_file;
    }
}