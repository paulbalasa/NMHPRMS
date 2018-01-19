namespace NMHLBOPDRMS
{
    partial class usrCntrlLaboratoryResults1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgLaboratoryResults = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgLaboratoryResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgLaboratoryResults
            // 
            this.dgLaboratoryResults.AllowUserToAddRows = false;
            this.dgLaboratoryResults.AllowUserToDeleteRows = false;
            this.dgLaboratoryResults.AllowUserToResizeColumns = false;
            this.dgLaboratoryResults.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgLaboratoryResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLaboratoryResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(116)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLaboratoryResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgLaboratoryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgLaboratoryResults.EnableHeadersVisualStyles = false;
            this.dgLaboratoryResults.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgLaboratoryResults.Location = new System.Drawing.Point(4, 4);
            this.dgLaboratoryResults.Margin = new System.Windows.Forms.Padding(4);
            this.dgLaboratoryResults.Name = "dgLaboratoryResults";
            this.dgLaboratoryResults.ReadOnly = true;
            this.dgLaboratoryResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgLaboratoryResults.RowHeadersVisible = false;
            this.dgLaboratoryResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgLaboratoryResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLaboratoryResults.Size = new System.Drawing.Size(731, 541);
            this.dgLaboratoryResults.TabIndex = 106;
            this.dgLaboratoryResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLaboratoryResults_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 549);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 104;
            this.label2.Text = "Total Record(s):";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(126, 550);
            this.lblTotalRecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(118, 18);
            this.lblTotalRecords.TabIndex = 105;
            this.lblTotalRecords.Text = "Total Record(s)";
            // 
            // usrCntrlLaboratoryResults1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgLaboratoryResults);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.label2);
            this.Name = "usrCntrlLaboratoryResults1";
            this.Size = new System.Drawing.Size(739, 571);
            this.Load += new System.EventHandler(this.usrCntrlLaboratoryResults1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgLaboratoryResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgLaboratoryResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalRecords;
    }
}
