using NMHLBOPDRMS;
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
    public partial class frmAdminTrialLock : Form
    {
        public frmAdminTrialLock()
        {
            InitializeComponent();
        }

        public int h;
        public int m;
        public int s;

        private void frmAdminTrialLock_Load(object sender, EventArgs e)
        {
            h = 0;
            m = 10;
            s = 0;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s = s - 1;

            if (s == -1)
            {
                m = m - 1;
                s = 59;
            }
            if (m == -1)
            {
                h = h - 1;
                m = 59;
            }

            string hh = Convert.ToString(h);
            string mm = Convert.ToString(m);
            string ss = Convert.ToString(s);

            lblHour.Text = "0" + hh;
            lblMinute.Text = "0" + mm;
            if (s < 10)
                lblSecond.Text = "0" + ss;
            else
            {
                lblSecond.Text = ss;
                if (s < 10)
                    lblSecond.Text = "0" + ss;
            }
                

            if (h == 0 && m == 00 && s == 0)
            {
                timer1.Stop();
                MessageBox.Show("Trial Lock ended! " + Environment.NewLine + "You can now Log in.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frmLogin f1 = new frmLogin();
                f1.Show();
            }
        }
    }
}
