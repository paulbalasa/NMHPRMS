using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NMHLBOPDRMS
{
    public partial class frmSplashScreen : Form
    {
       // MySQL_Code conStr = new MySQL_Code();
        //public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

      static  string connStr = "server=" + Properties.Settings.Default.server + ";username=" + Properties.Settings.Default.username + ";database=" + Properties.Settings.Default.database + ";port=" + Properties.Settings.Default.port + ";password=" + Properties.Settings.Default.password + ";";
        MySqlConnection conn = new MySqlConnection(connStr);

        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progBar.Increment(5);

            if (Properties.Settings.Default.database.Contains("dbHospital"))
            {
                timer1.Stop();
                conn.Open();
                string sql = "SELECT * FROM tblAccounts";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int x = 0; 
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    x++;
                }
                conn.Close();
                if (x == 0)
                {
                    this.Hide();
                    MessageBox.Show("Redirected to Administrator Account Set-up...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    frmAccountSetUp f1 = new frmAccountSetUp();
                    f1.Show();
                }
                else
                {
                    this.Hide();
                    frmLogin f2 = new frmLogin();
                    f2.Show();
                }
            }
            else
            {
                timer1.Stop();
                this.Hide();
                if (MessageBox.Show("Configure Database Connection first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    frmCheckConnection f1 = new frmCheckConnection();
                    f1.lblClose.Visible = false;
                    f1.lblMinimize.Location = new Point(299, 11);
                    f1.Show();
                }
                else
                    Application.Exit();
            }
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
