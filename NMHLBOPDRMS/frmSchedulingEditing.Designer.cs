namespace NMHLBOPDRMS
{
    partial class frmSchedulingEditing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedulingEditing));
            this.btnUpdateSchedule = new MetroFramework.Controls.MetroButton();
            this.lblIDNum = new System.Windows.Forms.Label();
            this.lstDays = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btnClearAll = new MetroFramework.Controls.MetroButton();
            this.label172 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDaysAc = new System.Windows.Forms.Label();
            this.btnRemove = new MetroFramework.Controls.MetroButton();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cboDays = new System.Windows.Forms.ComboBox();
            this.tmPckrTo = new System.Windows.Forms.DateTimePicker();
            this.tmPckrFrom = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new MetroFramework.Controls.MetroButton();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.lstTime = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdateSchedule
            // 
            this.btnUpdateSchedule.Location = new System.Drawing.Point(696, 446);
            this.btnUpdateSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateSchedule.Name = "btnUpdateSchedule";
            this.btnUpdateSchedule.Size = new System.Drawing.Size(138, 27);
            this.btnUpdateSchedule.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnUpdateSchedule.TabIndex = 464;
            this.btnUpdateSchedule.Text = "UPDATE SCHEDULE";
            this.btnUpdateSchedule.Click += new System.EventHandler(this.btnUpdateSchedule_Click);
            // 
            // lblIDNum
            // 
            this.lblIDNum.AutoEllipsis = true;
            this.lblIDNum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblIDNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIDNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIDNum.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDNum.Location = new System.Drawing.Point(153, 25);
            this.lblIDNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIDNum.Name = "lblIDNum";
            this.lblIDNum.Size = new System.Drawing.Size(169, 23);
            this.lblIDNum.TabIndex = 462;
            this.lblIDNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstDays
            // 
            this.lstDays.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDays.FormattingEnabled = true;
            this.lstDays.ItemHeight = 21;
            this.lstDays.Location = new System.Drawing.Point(17, 204);
            this.lstDays.Name = "lstDays";
            this.lstDays.Size = new System.Drawing.Size(400, 235);
            this.lstDays.TabIndex = 453;
            this.lstDays.SelectedIndexChanged += new System.EventHandler(this.lstDays_SelectedIndexChanged);
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
            this.panel1.TabIndex = 451;
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.ForeColor = System.Drawing.Color.White;
            this.lblMinimize.Location = new System.Drawing.Point(804, 8);
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
            this.lblClose.Location = new System.Drawing.Point(826, 11);
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
            this.label3.Size = new System.Drawing.Size(184, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Editing of Schedule";
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(472, 446);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(104, 27);
            this.btnClearAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnClearAll.TabIndex = 463;
            this.btnClearAll.Text = "CLEAR ALL";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.BackColor = System.Drawing.Color.Transparent;
            this.label172.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label172.ForeColor = System.Drawing.Color.Black;
            this.label172.Location = new System.Drawing.Point(27, 59);
            this.label172.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(124, 21);
            this.label172.TabIndex = 452;
            this.label172.Text = "Name of Doctor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(27, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 21);
            this.label1.TabIndex = 455;
            this.label1.Text = "Days:";
            // 
            // lblDaysAc
            // 
            this.lblDaysAc.AutoEllipsis = true;
            this.lblDaysAc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDaysAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDaysAc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDaysAc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysAc.Location = new System.Drawing.Point(259, 94);
            this.lblDaysAc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDaysAc.Name = "lblDaysAc";
            this.lblDaysAc.Size = new System.Drawing.Size(169, 26);
            this.lblDaysAc.TabIndex = 461;
            this.lblDaysAc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(584, 446);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(104, 27);
            this.btnRemove.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnRemove.TabIndex = 460;
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
            this.lblPosition.Location = new System.Drawing.Point(551, 57);
            this.lblPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(169, 23);
            this.lblPosition.TabIndex = 459;
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(478, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 21);
            this.label5.TabIndex = 458;
            this.label5.Text = "Position:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(27, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 457;
            this.label2.Text = "ID Number:";
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(153, 59);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(315, 23);
            this.lblName.TabIndex = 456;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboDays
            // 
            this.cboDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cboDays.Location = new System.Drawing.Point(82, 95);
            this.cboDays.Margin = new System.Windows.Forms.Padding(4);
            this.cboDays.Name = "cboDays";
            this.cboDays.Size = new System.Drawing.Size(159, 25);
            this.cboDays.TabIndex = 454;
            this.cboDays.SelectedIndexChanged += new System.EventHandler(this.cboDays_SelectedIndexChanged);
            // 
            // tmPckrTo
            // 
            this.tmPckrTo.CustomFormat = "hh\':\'mm tt";
            this.tmPckrTo.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.tmPckrTo.Enabled = false;
            this.tmPckrTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmPckrTo.Location = new System.Drawing.Point(649, 97);
            this.tmPckrTo.Name = "tmPckrTo";
            this.tmPckrTo.ShowUpDown = true;
            this.tmPckrTo.Size = new System.Drawing.Size(90, 20);
            this.tmPckrTo.TabIndex = 477;
            this.tmPckrTo.Value = new System.DateTime(2017, 10, 27, 20, 0, 0, 0);
            this.tmPckrTo.MouseLeave += new System.EventHandler(this.tmPckrTo_MouseLeave);
            // 
            // tmPckrFrom
            // 
            this.tmPckrFrom.CustomFormat = "hh\':\'mm tt";
            this.tmPckrFrom.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.tmPckrFrom.Enabled = false;
            this.tmPckrFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmPckrFrom.Location = new System.Drawing.Point(527, 97);
            this.tmPckrFrom.Name = "tmPckrFrom";
            this.tmPckrFrom.ShowUpDown = true;
            this.tmPckrFrom.Size = new System.Drawing.Size(90, 20);
            this.tmPckrFrom.TabIndex = 474;
            this.tmPckrFrom.Value = new System.DateTime(2017, 10, 27, 20, 0, 0, 0);
            this.tmPckrFrom.MouseLeave += new System.EventHandler(this.tmPckrFrom_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(744, 97);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(34, 20);
            this.btnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnAdd.TabIndex = 475;
            this.btnAdd.Text = "ADD";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(623, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 20);
            this.label6.TabIndex = 476;
            this.label6.Text = "-";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(79, 122);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(145, 18);
            this.lblMessage.TabIndex = 473;
            this.lblMessage.Text = "Please select Time...";
            this.lblMessage.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(477, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 21);
            this.label4.TabIndex = 464;
            this.label4.Text = "Time:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 480);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(853, 22);
            this.panel4.TabIndex = 466;
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
            // lstTime
            // 
            this.lstTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTime.FormattingEnabled = true;
            this.lstTime.ItemHeight = 21;
            this.lstTime.Location = new System.Drawing.Point(415, 204);
            this.lstTime.Name = "lstTime";
            this.lstTime.Size = new System.Drawing.Size(419, 235);
            this.lstTime.TabIndex = 467;
            this.lstTime.SelectedIndexChanged += new System.EventHandler(this.lstTime_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tmPckrTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tmPckrFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label172);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblDaysAc);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.lblPosition);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.cboDays);
            this.groupBox1.Controls.Add(this.lblIDNum);
            this.groupBox1.Location = new System.Drawing.Point(19, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 149);
            this.groupBox1.TabIndex = 468;
            this.groupBox1.TabStop = false;
            // 
            // frmSchedulingEditing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(853, 502);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstTime);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnUpdateSchedule);
            this.Controls.Add(this.lstDays);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnRemove);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSchedulingEditing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editing of Schedule";
            this.Load += new System.EventHandler(this.frmSchedulingEditing_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MetroFramework.Controls.MetroButton btnUpdateSchedule;
        public System.Windows.Forms.Label lblIDNum;
        public System.Windows.Forms.ListBox lstDays;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label3;
        public MetroFramework.Controls.MetroButton btnClearAll;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblDaysAc;
        public MetroFramework.Controls.MetroButton btnRemove;
        public System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.ComboBox cboDays;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel4;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.ListBox lstTime;
        private System.Windows.Forms.DateTimePicker tmPckrTo;
        private System.Windows.Forms.DateTimePicker tmPckrFrom;
        public MetroFramework.Controls.MetroButton btnAdd;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}