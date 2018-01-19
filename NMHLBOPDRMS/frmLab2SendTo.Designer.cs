namespace NMHLBOPDRMS
{
    partial class frmLab2SendTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLab2SendTo));
            this.btnSend = new MetroFramework.Controls.MetroButton();
            this.lblNumber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtRequested = new System.Windows.Forms.DateTimePicker();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimename = new System.Windows.Forms.Label();
            this.cboNurseName = new System.Windows.Forms.ComboBox();
            this.cboDoctorsName = new System.Windows.Forms.ComboBox();
            this.rdoDoctor = new System.Windows.Forms.RadioButton();
            this.rdoNurse = new System.Windows.Forms.RadioButton();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.lblDaysAc = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(233, 207);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 29);
            this.btnSend.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSend.TabIndex = 354;
            this.btnSend.Text = "SEND";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(12, 7);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(35, 13);
            this.lblNumber.TabIndex = 355;
            this.lblNumber.Text = "label1";
            this.lblNumber.Visible = false;
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
            this.panel1.Size = new System.Drawing.Size(336, 43);
            this.panel1.TabIndex = 357;
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.ForeColor = System.Drawing.Color.White;
            this.lblMinimize.Location = new System.Drawing.Point(269, 8);
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
            this.lblClose.Location = new System.Drawing.Point(294, 10);
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
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Send To";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            this.panel4.Controls.Add(this.dtRequested);
            this.panel4.Controls.Add(this.dtPicker);
            this.panel4.Controls.Add(this.lblName);
            this.panel4.Controls.Add(this.lblNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 242);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(336, 25);
            this.panel4.TabIndex = 359;
            // 
            // dtRequested
            // 
            this.dtRequested.Location = new System.Drawing.Point(248, 9);
            this.dtRequested.Name = "dtRequested";
            this.dtRequested.Size = new System.Drawing.Size(200, 20);
            this.dtRequested.TabIndex = 358;
            this.dtRequested.Value = new System.DateTime(2017, 8, 10, 0, 0, 0, 0);
            this.dtRequested.Visible = false;
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(42, 4);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(200, 20);
            this.dtPicker.TabIndex = 357;
            this.dtPicker.Value = new System.DateTime(2017, 8, 10, 0, 0, 0, 0);
            this.dtPicker.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(125, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 356;
            this.lblName.Text = "label1";
            this.lblName.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(70, 67);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 17);
            this.lblTime.TabIndex = 357;
            // 
            // lblTimename
            // 
            this.lblTimename.AutoSize = true;
            this.lblTimename.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimename.Location = new System.Drawing.Point(30, 67);
            this.lblTimename.Name = "lblTimename";
            this.lblTimename.Size = new System.Drawing.Size(41, 17);
            this.lblTimename.TabIndex = 356;
            this.lblTimename.Text = "Time:";
            // 
            // cboNurseName
            // 
            this.cboNurseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNurseName.Enabled = false;
            this.cboNurseName.FormattingEnabled = true;
            this.cboNurseName.Location = new System.Drawing.Point(33, 119);
            this.cboNurseName.Name = "cboNurseName";
            this.cboNurseName.Size = new System.Drawing.Size(254, 21);
            this.cboNurseName.TabIndex = 352;
            // 
            // cboDoctorsName
            // 
            this.cboDoctorsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoctorsName.Enabled = false;
            this.cboDoctorsName.FormattingEnabled = true;
            this.cboDoctorsName.Location = new System.Drawing.Point(33, 44);
            this.cboDoctorsName.Name = "cboDoctorsName";
            this.cboDoctorsName.Size = new System.Drawing.Size(254, 21);
            this.cboDoctorsName.TabIndex = 351;
            this.cboDoctorsName.SelectedIndexChanged += new System.EventHandler(this.cboDoctorsName_SelectedIndexChanged);
            // 
            // rdoDoctor
            // 
            this.rdoDoctor.AutoSize = true;
            this.rdoDoctor.BackColor = System.Drawing.Color.Transparent;
            this.rdoDoctor.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDoctor.ForeColor = System.Drawing.Color.Black;
            this.rdoDoctor.Location = new System.Drawing.Point(15, 13);
            this.rdoDoctor.Name = "rdoDoctor";
            this.rdoDoctor.Size = new System.Drawing.Size(81, 25);
            this.rdoDoctor.TabIndex = 349;
            this.rdoDoctor.Text = "Doctor";
            this.rdoDoctor.UseVisualStyleBackColor = false;
            this.rdoDoctor.CheckedChanged += new System.EventHandler(this.rdoDoctor_CheckedChanged);
            // 
            // rdoNurse
            // 
            this.rdoNurse.AutoSize = true;
            this.rdoNurse.BackColor = System.Drawing.Color.Transparent;
            this.rdoNurse.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNurse.ForeColor = System.Drawing.Color.Black;
            this.rdoNurse.Location = new System.Drawing.Point(15, 88);
            this.rdoNurse.Name = "rdoNurse";
            this.rdoNurse.Size = new System.Drawing.Size(75, 25);
            this.rdoNurse.TabIndex = 350;
            this.rdoNurse.Text = "Nurse";
            this.rdoNurse.UseVisualStyleBackColor = false;
            this.rdoNurse.CheckedChanged += new System.EventHandler(this.rdoNurse_CheckedChanged);
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
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // lblDaysAc
            // 
            this.lblDaysAc.AutoEllipsis = true;
            this.lblDaysAc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDaysAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDaysAc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDaysAc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysAc.Location = new System.Drawing.Point(0, 245);
            this.lblDaysAc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDaysAc.Name = "lblDaysAc";
            this.lblDaysAc.Size = new System.Drawing.Size(91, 23);
            this.lblDaysAc.TabIndex = 437;
            this.lblDaysAc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDaysAc.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.rdoDoctor);
            this.groupBox1.Controls.Add(this.lblTimename);
            this.groupBox1.Controls.Add(this.rdoNurse);
            this.groupBox1.Controls.Add(this.cboNurseName);
            this.groupBox1.Controls.Add(this.cboDoctorsName);
            this.groupBox1.Location = new System.Drawing.Point(11, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 154);
            this.groupBox1.TabIndex = 438;
            this.groupBox1.TabStop = false;
            // 
            // frmLab2SendTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(336, 267);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDaysAc);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLab2SendTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laboratory 2 Send To..";
            this.Load += new System.EventHandler(this.frmLab2SendTo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnSend;
        public System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cboNurseName;
        private System.Windows.Forms.ComboBox cboDoctorsName;
        private System.Windows.Forms.RadioButton rdoDoctor;
        private System.Windows.Forms.RadioButton rdoNurse;
        public System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        public System.Windows.Forms.DateTimePicker dtRequested;
        public System.Windows.Forms.Label lblDaysAc;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimename;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}