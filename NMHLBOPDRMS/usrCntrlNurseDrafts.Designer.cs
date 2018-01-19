namespace NMHLBOPDRMS
{
    partial class usrCntrlNurseDrafts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDoctorsName = new System.Windows.Forms.ComboBox();
            this.btnSendTo = new MetroFramework.Controls.MetroButton();
            this.dgDrafts = new System.Windows.Forms.DataGridView();
            this.tmrRefreshDGV = new System.Windows.Forms.Timer(this.components);
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearAll = new MetroFramework.Controls.MetroButton();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgDrafts)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(13, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 21);
            this.label5.TabIndex = 441;
            this.label5.Text = "Doctor\'s Name:";
            // 
            // cboDoctorsName
            // 
            this.cboDoctorsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoctorsName.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cboDoctorsName.FormattingEnabled = true;
            this.cboDoctorsName.Location = new System.Drawing.Point(141, 8);
            this.cboDoctorsName.Margin = new System.Windows.Forms.Padding(5);
            this.cboDoctorsName.Name = "cboDoctorsName";
            this.cboDoctorsName.Size = new System.Drawing.Size(345, 25);
            this.cboDoctorsName.TabIndex = 440;
            this.cboDoctorsName.SelectedIndexChanged += new System.EventHandler(this.cboDoctorName_SelectedIndexChanged);
            // 
            // btnSendTo
            // 
            this.btnSendTo.Location = new System.Drawing.Point(560, 508);
            this.btnSendTo.Margin = new System.Windows.Forms.Padding(5);
            this.btnSendTo.Name = "btnSendTo";
            this.btnSendTo.Size = new System.Drawing.Size(163, 32);
            this.btnSendTo.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSendTo.TabIndex = 439;
            this.btnSendTo.Text = "SEND TO DOCTOR";
            this.btnSendTo.Click += new System.EventHandler(this.btnSendTo_Click);
            // 
            // dgDrafts
            // 
            this.dgDrafts.AllowUserToAddRows = false;
            this.dgDrafts.AllowUserToDeleteRows = false;
            this.dgDrafts.AllowUserToResizeColumns = false;
            this.dgDrafts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgDrafts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDrafts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(116)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrafts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDrafts.ColumnHeadersHeight = 30;
            this.dgDrafts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDrafts.EnableHeadersVisualStyles = false;
            this.dgDrafts.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgDrafts.Location = new System.Drawing.Point(17, 44);
            this.dgDrafts.Margin = new System.Windows.Forms.Padding(5);
            this.dgDrafts.Name = "dgDrafts";
            this.dgDrafts.ReadOnly = true;
            this.dgDrafts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgDrafts.RowHeadersVisible = false;
            this.dgDrafts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgDrafts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDrafts.Size = new System.Drawing.Size(706, 457);
            this.dgDrafts.TabIndex = 438;
            this.dgDrafts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDrafts_CellClick);
            this.dgDrafts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDrafts_CellContentClick);
            // 
            // tmrRefreshDGV
            // 
            this.tmrRefreshDGV.Enabled = true;
            this.tmrRefreshDGV.Interval = 3000;
            this.tmrRefreshDGV.Tick += new System.EventHandler(this.tmrRefreshDGV_Tick);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(129, 515);
            this.lblTotalRecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(118, 18);
            this.lblTotalRecords.TabIndex = 443;
            this.lblTotalRecords.Text = "Total Record(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 514);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 442;
            this.label2.Text = "Total Record(s):";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(483, 508);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(66, 32);
            this.btnClearAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnClearAll.TabIndex = 444;
            this.btnClearAll.Text = "CLEAR ALL";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(407, 508);
            this.btnClear.Margin = new System.Windows.Forms.Padding(5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(66, 32);
            this.btnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnClear.TabIndex = 445;
            this.btnClear.Text = "CLEAR";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // usrCntrlNurseDrafts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboDoctorsName);
            this.Controls.Add(this.btnSendTo);
            this.Controls.Add(this.dgDrafts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "usrCntrlNurseDrafts";
            this.Size = new System.Drawing.Size(740, 551);
            this.Load += new System.EventHandler(this.usrCntrlNurseDrafts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDrafts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cboDoctorsName;
        private MetroFramework.Controls.MetroButton btnSendTo;
        private System.Windows.Forms.DataGridView dgDrafts;
        private System.Windows.Forms.Timer tmrRefreshDGV;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroButton btnClearAll;
        private MetroFramework.Controls.MetroButton btnClear;
    }
}
