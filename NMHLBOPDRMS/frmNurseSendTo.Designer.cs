namespace NMHLBOPDRMS
{
    partial class frmNurseSendTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNurseSendTo));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtRequested = new System.Windows.Forms.DateTimePicker();
            this.lblDaysAc = new System.Windows.Forms.Label();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new MetroFramework.Controls.MetroButton();
            this.cboDoctorsName = new System.Windows.Forms.ComboBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimename = new System.Windows.Forms.Label();
            this.label200 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            this.panel4.Controls.Add(this.dtRequested);
            this.panel4.Controls.Add(this.lblDaysAc);
            this.panel4.Controls.Add(this.dtPicker);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 205);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(322, 25);
            this.panel4.TabIndex = 359;
            // 
            // dtRequested
            // 
            this.dtRequested.Location = new System.Drawing.Point(3, 14);
            this.dtRequested.Name = "dtRequested";
            this.dtRequested.Size = new System.Drawing.Size(200, 20);
            this.dtRequested.TabIndex = 1;
            this.dtRequested.Value = new System.DateTime(2017, 8, 10, 0, 0, 0, 0);
            this.dtRequested.Visible = false;
            // 
            // lblDaysAc
            // 
            this.lblDaysAc.AutoEllipsis = true;
            this.lblDaysAc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDaysAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDaysAc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDaysAc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysAc.Location = new System.Drawing.Point(245, 2);
            this.lblDaysAc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDaysAc.Name = "lblDaysAc";
            this.lblDaysAc.Size = new System.Drawing.Size(91, 23);
            this.lblDaysAc.TabIndex = 436;
            this.lblDaysAc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDaysAc.Visible = false;
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(105, 14);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(200, 20);
            this.dtPicker.TabIndex = 0;
            this.dtPicker.Value = new System.DateTime(2017, 8, 10, 0, 0, 0, 0);
            this.dtPicker.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.lblMinimize);
            this.panel1.Controls.Add(this.lblClose);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 43);
            this.panel1.TabIndex = 358;
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.ForeColor = System.Drawing.Color.White;
            this.lblMinimize.Location = new System.Drawing.Point(274, 9);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Size = new System.Drawing.Size(18, 20);
            this.lblMinimize.TabIndex = 2;
            this.lblMinimize.Text = "_";
            this.lblMinimize.Click += new System.EventHandler(this.lblMinimize_Click);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(298, 11);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(20, 20);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Send To";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(223, 164);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 29);
            this.btnSend.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSend.TabIndex = 357;
            this.btnSend.Text = "SEND";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cboDoctorsName
            // 
            this.cboDoctorsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoctorsName.FormattingEnabled = true;
            this.cboDoctorsName.Location = new System.Drawing.Point(17, 47);
            this.cboDoctorsName.Name = "cboDoctorsName";
            this.cboDoctorsName.Size = new System.Drawing.Size(265, 21);
            this.cboDoctorsName.TabIndex = 351;
            this.cboDoctorsName.SelectedIndexChanged += new System.EventHandler(this.cboDoctorsName_SelectedIndexChanged);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(58, 73);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 17);
            this.lblTime.TabIndex = 354;
            // 
            // lblTimename
            // 
            this.lblTimename.AutoSize = true;
            this.lblTimename.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimename.Location = new System.Drawing.Point(14, 73);
            this.lblTimename.Name = "lblTimename";
            this.lblTimename.Size = new System.Drawing.Size(41, 17);
            this.lblTimename.TabIndex = 353;
            this.lblTimename.Text = "Time:";
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label200.Location = new System.Drawing.Point(13, 19);
            this.label200.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(64, 20);
            this.label200.TabIndex = 352;
            this.label200.Text = "Doctor:";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel1;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.label3;
            this.bunifuDragControl2.Vertical = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.label200);
            this.groupBox1.Controls.Add(this.lblTimename);
            this.groupBox1.Controls.Add(this.cboDoctorsName);
            this.groupBox1.Location = new System.Drawing.Point(11, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 104);
            this.groupBox1.TabIndex = 437;
            this.groupBox1.TabStop = false;
            // 
            // frmNurseSendTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(322, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNurseSendTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nurse Send To..";
            this.Load += new System.EventHandler(this.frmNurseSendTo_Load);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.ComboBox cboDoctorsName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroButton btnSend;
        private System.Windows.Forms.Label label200;
        private System.Windows.Forms.DateTimePicker dtPicker;
        public System.Windows.Forms.DateTimePicker dtRequested;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        public System.Windows.Forms.Label lblDaysAc;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimename;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}