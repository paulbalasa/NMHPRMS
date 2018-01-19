namespace NMHLBOPDRMS
{
    partial class usrCntrlUsersMonitoring
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
            MetroSuite.MetroSeparator.MainColorScheme mainColorScheme1 = new MetroSuite.MetroSeparator.MainColorScheme();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefresh = new MetroFramework.Controls.MetroButton();
            this.btnView = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.metroSeparator2 = new MetroSuite.MetroSeparator();
            this.label200 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dgActivityLog = new System.Windows.Forms.DataGridView();
            this.lnkLblPrint = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgActivityLog)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(412, 40);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(55, 22);
            this.btnRefresh.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnRefresh.TabIndex = 407;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(349, 40);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(55, 22);
            this.btnView.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnView.TabIndex = 405;
            this.btnView.Text = "VIEW";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(264, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 24);
            this.label1.TabIndex = 404;
            this.label1.Text = "System Activity Log";
            // 
            // metroSeparator2
            // 
            mainColorScheme1.Color1 = System.Drawing.Color.Black;
            mainColorScheme1.Color2 = System.Drawing.Color.Black;
            this.metroSeparator2.ColorScheme = mainColorScheme1;
            this.metroSeparator2.Location = new System.Drawing.Point(226, 49);
            this.metroSeparator2.Margin = new System.Windows.Forms.Padding(4);
            this.metroSeparator2.Name = "metroSeparator2";
            this.metroSeparator2.Size = new System.Drawing.Size(11, 2);
            this.metroSeparator2.TabIndex = 403;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.BackColor = System.Drawing.Color.White;
            this.label200.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label200.ForeColor = System.Drawing.Color.Black;
            this.label200.Location = new System.Drawing.Point(17, 40);
            this.label200.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(101, 20);
            this.label200.TabIndex = 402;
            this.label200.Text = "Date Range:";
            // 
            // dtFrom
            // 
            this.dtFrom.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(121, 39);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(98, 23);
            this.dtFrom.TabIndex = 401;
            this.dtFrom.Value = new System.DateTime(2017, 8, 31, 0, 0, 0, 0);
            // 
            // dtTo
            // 
            this.dtTo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(244, 39);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(98, 23);
            this.dtTo.TabIndex = 400;
            this.dtTo.Value = new System.DateTime(2017, 8, 31, 0, 0, 0, 0);
            // 
            // dgActivityLog
            // 
            this.dgActivityLog.AllowUserToAddRows = false;
            this.dgActivityLog.AllowUserToDeleteRows = false;
            this.dgActivityLog.AllowUserToResizeColumns = false;
            this.dgActivityLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgActivityLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgActivityLog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(116)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActivityLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgActivityLog.ColumnHeadersHeight = 30;
            this.dgActivityLog.EnableHeadersVisualStyles = false;
            this.dgActivityLog.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgActivityLog.Location = new System.Drawing.Point(21, 70);
            this.dgActivityLog.Margin = new System.Windows.Forms.Padding(4);
            this.dgActivityLog.Name = "dgActivityLog";
            this.dgActivityLog.ReadOnly = true;
            this.dgActivityLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgActivityLog.RowHeadersVisible = false;
            this.dgActivityLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgActivityLog.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgActivityLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgActivityLog.Size = new System.Drawing.Size(696, 466);
            this.dgActivityLog.TabIndex = 399;
            // 
            // lnkLblPrint
            // 
            this.lnkLblPrint.AutoSize = true;
            this.lnkLblPrint.Location = new System.Drawing.Point(679, 46);
            this.lnkLblPrint.Name = "lnkLblPrint";
            this.lnkLblPrint.Size = new System.Drawing.Size(38, 16);
            this.lnkLblPrint.TabIndex = 408;
            this.lnkLblPrint.TabStop = true;
            this.lnkLblPrint.Text = "PRINT";
            this.lnkLblPrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblPrint_LinkClicked);
            // 
            // usrCntrlUsersMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lnkLblPrint);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroSeparator2);
            this.Controls.Add(this.label200);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dgActivityLog);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "usrCntrlUsersMonitoring";
            this.Size = new System.Drawing.Size(737, 553);
            this.Load += new System.EventHandler(this.usrCntrlUsersMonitoring_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgActivityLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnRefresh;
        private MetroFramework.Controls.MetroButton btnView;
        private System.Windows.Forms.Label label1;
        private MetroSuite.MetroSeparator metroSeparator2;
        private System.Windows.Forms.Label label200;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DataGridView dgActivityLog;
        private System.Windows.Forms.LinkLabel lnkLblPrint;
    }
}
