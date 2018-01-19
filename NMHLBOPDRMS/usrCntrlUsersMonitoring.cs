using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;

namespace NMHLBOPDRMS
{
    public partial class usrCntrlUsersMonitoring : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter;
        DataSet dS;

        public usrCntrlUsersMonitoring()
        {
            InitializeComponent();
        }

        private void usrCntrlUsersMonitoring_Load(object sender, EventArgs e)
        {
            activitylog();

            dgActivityLog.Columns[0].Width = 100;
            dgActivityLog.Columns[1].Width = 125;
            dgActivityLog.Columns[2].Width = 200;
            dgActivityLog.Columns[3].Width = 75;
            dgActivityLog.Columns[4].Width = 75;
            dgActivityLog.Columns[5].Width = 550;

            dtFrom.Text = DateTime.Now.ToShortDateString();
            dtTo.Text = DateTime.Now.ToShortDateString();
        }

        public void activitylog()
        {
            conn.Open();
            string sql = "SELECT logNum AS 'Log No.', accountType AS 'Account Type', name AS 'Account Name', time AS 'Time', date AS 'Date', activity AS 'Activity' FROM tblActivity_Log";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblActivity_Log");
            dgActivityLog.DataMember = "tblActivity_Log";
            dgActivityLog.DataSource = dS;
            conn.Close();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            activitylog();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT logNum AS 'Log No.', accountType AS 'Account Type', name AS 'Account Name', time AS 'Time', date AS 'Date', activity AS 'Activity' FROM tblActivity_Log WHERE date BETWEEN '" + dtFrom.Value.ToString("yyyy-MM-dd") + "' AND '" + dtTo.Value.ToString("yyyy-MM-dd") + "' ";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblActivity_Log");
            dgActivityLog.DataMember = "tblActivity_Log";
            dgActivityLog.DataSource = dS;
            conn.Close();
        }

        private void lnkLblPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPrintActivityLog f1 = new frmPrintActivityLog();

            ReportParameter param = new ReportParameter("dtFrom", dtFrom.Value.ToString("MMMM dd, yyyy"));
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("dtTo", dtTo.Value.ToString("MMMM dd, yyyy"));
            f1.reportViewer1.LocalReport.SetParameters(param);

            DataSet1 ds = new DataSet1();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM tblActivity_Log WHERE date BETWEEN '" + dtFrom.Value.ToString("yyyy-MM-dd") + "' AND '" + dtTo.Value.ToString("yyyy-MM-dd") + "' ", conn);
            da.Fill(ds, ds.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("dsActivityLog", ds.Tables[0]);
            f1.reportViewer1.LocalReport.DataSources.Clear();
            f1.reportViewer1.LocalReport.DataSources.Add(rds);
            f1.reportViewer1.LocalReport.Refresh();
            f1.reportViewer1.RefreshReport();

            f1.Show();
        }
        
    }
}
