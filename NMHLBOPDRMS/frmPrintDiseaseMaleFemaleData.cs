using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace NMHLBOPDRMS
{
    public partial class frmPrintDiseaseMaleFemaleData : Form
    {
        public frmPrintDiseaseMaleFemaleData()
        {
            InitializeComponent();
        }

        private void frmPrintDiseaseMaleFemaleData_Load(object sender, EventArgs e)
        {
            string month = DateTime.Now.ToString("MMMM");
            string year = DateTime.Now.ToString("yyyy");

            ReportParameter param = new ReportParameter("ToMonth", month);
            this.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("Year", year);
            this.reportViewer1.LocalReport.SetParameters(param);
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
