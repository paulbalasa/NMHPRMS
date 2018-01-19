using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMHLBOPDRMS
{
    public partial class frmPrintUltrasound : Form
    {
        public frmPrintUltrasound()
        {
            InitializeComponent();
        }

        private void frmPrintUltrasound_Load(object sender, EventArgs e)
        {
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
