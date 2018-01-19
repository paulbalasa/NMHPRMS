namespace NMHLBOPDRMS
{
    partial class frmScheduling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScheduling));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.lblDaysAc = new System.Windows.Forms.Label();
            this.btnRemove = new MetroFramework.Controls.MetroButton();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnSaveDays = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDays = new System.Windows.Forms.ComboBox();
            this.lstDays = new System.Windows.Forms.ListBox();
            this.label172 = new System.Windows.Forms.Label();
            this.cboIDNum = new System.Windows.Forms.ComboBox();
            this.tmPckrTo = new System.Windows.Forms.DateTimePicker();
            this.tmPckrFrom = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new MetroFramework.Controls.MetroButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstTime = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
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
            this.panel1.Size = new System.Drawing.Size(853, 43);
            this.panel1.TabIndex = 358;
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.ForeColor = System.Drawing.Color.White;
            this.lblMinimize.Location = new System.Drawing.Point(803, 7);
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
            this.lblClose.Location = new System.Drawing.Point(824, 10);
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
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Scheduling";
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
            // lblDaysAc
            // 
            this.lblDaysAc.AutoEllipsis = true;
            this.lblDaysAc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDaysAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDaysAc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDaysAc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysAc.Location = new System.Drawing.Point(245, 87);
            this.lblDaysAc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDaysAc.Name = "lblDaysAc";
            this.lblDaysAc.Size = new System.Drawing.Size(169, 23);
            this.lblDaysAc.TabIndex = 446;
            this.lblDaysAc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(551, 441);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(138, 27);
            this.btnRemove.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnRemove.TabIndex = 445;
            this.btnRemove.Text = "REMOVE";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoEllipsis = true;
            this.lblPosition.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPosition.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Location = new System.Drawing.Point(544, 52);
            this.lblPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(169, 23);
            this.lblPosition.TabIndex = 444;
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(474, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 21);
            this.label5.TabIndex = 443;
            this.label5.Text = "Position:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(28, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 442;
            this.label2.Text = "ID Number:";
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(152, 52);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(321, 23);
            this.lblName.TabIndex = 441;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveDays
            // 
            this.btnSaveDays.Location = new System.Drawing.Point(697, 441);
            this.btnSaveDays.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDays.Name = "btnSaveDays";
            this.btnSaveDays.Size = new System.Drawing.Size(138, 27);
            this.btnSaveDays.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSaveDays.TabIndex = 440;
            this.btnSaveDays.Text = "SAVE SCHEDULE";
            this.btnSaveDays.Click += new System.EventHandler(this.btnSaveDays_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(28, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 21);
            this.label1.TabIndex = 439;
            this.label1.Text = "Days:";
            // 
            // cboDays
            // 
            this.cboDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDays.Enabled = false;
            this.cboDays.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cboDays.FormattingEnabled = true;
            this.cboDays.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.cboDays.Location = new System.Drawing.Point(78, 86);
            this.cboDays.Margin = new System.Windows.Forms.Padding(4);
            this.cboDays.Name = "cboDays";
            this.cboDays.Size = new System.Drawing.Size(159, 25);
            this.cboDays.TabIndex = 438;
            this.cboDays.SelectedIndexChanged += new System.EventHandler(this.cboDays_SelectedIndexChanged);
            // 
            // lstDays
            // 
            this.lstDays.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDays.FormattingEnabled = true;
            this.lstDays.ItemHeight = 21;
            this.lstDays.Location = new System.Drawing.Point(17, 201);
            this.lstDays.Name = "lstDays";
            this.lstDays.Size = new System.Drawing.Size(400, 235);
            this.lstDays.TabIndex = 437;
            this.lstDays.SelectedIndexChanged += new System.EventHandler(this.lstDays_SelectedIndexChanged);
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.BackColor = System.Drawing.Color.Transparent;
            this.label172.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label172.ForeColor = System.Drawing.Color.Black;
            this.label172.Location = new System.Drawing.Point(28, 52);
            this.label172.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(124, 21);
            this.label172.TabIndex = 436;
            this.label172.Text = "Name of Doctor:";
            // 
            // cboIDNum
            // 
            this.cboIDNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIDNum.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cboIDNum.Items.AddRange(new object[] {
            "sdfgsdfg"});
            this.cboIDNum.Location = new System.Drawing.Point(152, 18);
            this.cboIDNum.Margin = new System.Windows.Forms.Padding(4);
            this.cboIDNum.Name = "cboIDNum";
            this.cboIDNum.Size = new System.Drawing.Size(181, 25);
            this.cboIDNum.TabIndex = 435;
            this.cboIDNum.SelectedIndexChanged += new System.EventHandler(this.cboIDNum_SelectedIndexChanged);
            // 
            // tmPckrTo
            // 
            this.tmPckrTo.CustomFormat = "hh\':\'mm tt";
            this.tmPckrTo.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.tmPckrTo.Enabled = false;
            this.tmPckrTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmPckrTo.Location = new System.Drawing.Point(651, 88);
            this.tmPckrTo.Name = "tmPckrTo";
            this.tmPckrTo.ShowUpDown = true;
            this.tmPckrTo.Size = new System.Drawing.Size(90, 20);
            this.tmPckrTo.TabIndex = 458;
            this.tmPckrTo.Value = new System.DateTime(2017, 10, 27, 20, 0, 0, 0);
            this.tmPckrTo.MouseLeave += new System.EventHandler(this.tmPckrTo_MouseLeave);
            // 
            // tmPckrFrom
            // 
            this.tmPckrFrom.CustomFormat = "hh\':\'mm tt";
            this.tmPckrFrom.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.tmPckrFrom.Enabled = false;
            this.tmPckrFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmPckrFrom.Location = new System.Drawing.Point(529, 88);
            this.tmPckrFrom.Name = "tmPckrFrom";
            this.tmPckrFrom.ShowUpDown = true;
            this.tmPckrFrom.Size = new System.Drawing.Size(90, 20);
            this.tmPckrFrom.TabIndex = 1;
            this.tmPckrFrom.Value = new System.DateTime(2017, 10, 27, 8, 0, 0, 0);
            this.tmPckrFrom.MouseLeave += new System.EventHandler(this.tmPckrFrom_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(746, 88);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(34, 20);
            this.btnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnAdd.TabIndex = 450;
            this.btnAdd.Text = "ADD";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(75, 115);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(145, 18);
            this.lblMessage.TabIndex = 457;
            this.lblMessage.Text = "Please select Time...";
            this.lblMessage.Visible = false;
            // 
            // lblTo
            // 
            this.lblTo.AutoEllipsis = true;
            this.lblTo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(625, 88);
            this.lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 20);
            this.lblTo.TabIndex = 456;
            this.lblTo.Text = "-";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(475, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 21);
            this.label4.TabIndex = 448;
            this.label4.Text = "Time:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 480);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(853, 22);
            this.panel4.TabIndex = 448;
            // 
            // lstTime
            // 
            this.lstTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTime.FormattingEnabled = true;
            this.lstTime.ItemHeight = 21;
            this.lstTime.Location = new System.Drawing.Point(411, 201);
            this.lstTime.Name = "lstTime";
            this.lstTime.Size = new System.Drawing.Size(423, 235);
            this.lstTime.TabIndex = 449;
            this.lstTime.SelectedIndexChanged += new System.EventHandler(this.lstTime_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tmPckrTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tmPckrFrom);
            this.groupBox1.Controls.Add(this.label172);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.cboDays);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.cboIDNum);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDaysAc);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblPosition);
            this.groupBox1.Location = new System.Drawing.Point(17, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 146);
            this.groupBox1.TabIndex = 450;
            this.groupBox1.TabStop = false;
            // 
            // frmScheduling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(853, 502);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstTime);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSaveDays);
            this.Controls.Add(this.lstDays);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScheduling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctor\'s Scheduling";
            this.Load += new System.EventHandler(this.frnScheduling_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        public MetroFramework.Controls.MetroButton btnRemove;
        public MetroFramework.Controls.MetroButton btnSaveDays;
        private System.Windows.Forms.ListBox lstDays;
        public System.Windows.Forms.Label lblDaysAc;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblPosition;
        public System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cboIDNum;
        public System.Windows.Forms.ComboBox cboDays;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lstTime;
        public System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMessage;
        public MetroFramework.Controls.MetroButton btnAdd;
        private System.Windows.Forms.DateTimePicker tmPckrFrom;
        private System.Windows.Forms.DateTimePicker tmPckrTo;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}