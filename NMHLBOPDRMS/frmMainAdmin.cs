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
using System.IO;
using Microsoft.Reporting.WinForms;

namespace NMHLBOPDRMS
{
    public partial class frmMainAdmin : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmMainAdmin()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblToMonth.Text = DateTime.Now.ToString("MMMM");
            lblToMonth1.Text = DateTime.Now.ToString("MMMM");
            lblYear.Text = DateTime.Now.ToString("yyyy");
            lblYear1.Text = DateTime.Now.ToString("yyyy");
            timer1.Start();

            //StatisticalDiseases
            conn.Open();
            string sqlStatement = "SELECT DISTINCT disease AS 'Discharge Diagnosis Primary', pedia AS 'Pedia (0-18 yrs. old)', adult AS 'Adult (19-65 yrs. old)', count AS 'Total' FROM tblstatdisease WHERE count >= 1 ORDER BY count DESC LIMIT 10"; 
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlStatement, conn);
            DataSet dS = new DataSet();
            adapter.Fill(dS, "tblstatdisease");
            dgLeadingCauses.DataMember = "tblstatdisease";
            dgLeadingCauses.DataSource = dS;
            conn.Close();

            dgLeadingCauses.Columns[0].Width = 300;
            dgLeadingCauses.Columns[1].Width = 155;
            dgLeadingCauses.Columns[2].Width = 155;
            dgLeadingCauses.Columns[3].Width = 104;

            //StatisticalMaleFemale
            conn.Open();
            string sqlStatement1 = "SELECT DISTINCT disease AS 'Discharge Diagnosis Primary', L1M AS 'Under 1yr old / Male', L1F AS 'Under 1yr old / Female', OM AS '1-4yrs old / Male', OF AS '1-4yrs old / Female', FM AS '5-9yrs old / Male', FF AS '5-9yrs old / Female', TM AS '10-14yrs old / Male', TF AS '10-14yrs old / Female', FFM AS '15-19yrs old / Male', FFF AS '15-19yrs old / Female', TWM AS '20-44yrs old / Male', TWF AS '20-44yrs old / Female', FFFM AS '45-64yrs old / Male', FFFF AS '45-64yrs old / Female', GSFM AS '65yrs old up / Male', GSFF AS '65yrs old up / Female', count AS 'Total' FROM tblstatdisease WHERE count >= 1 ORDER BY count DESC LIMIT 10";
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(sqlStatement1, conn);
            DataSet dS1 = new DataSet();
            adapter1.Fill(dS1, "tblstatdisease");
            dgMaleFemale.DataMember = "tblstatdisease";
            dgMaleFemale.DataSource = dS1;
            conn.Close();

            dgMaleFemale.Columns[0].Width = 300;

            conn.Open();
            string sql = "SELECT DISTINCT(disease) FROM tblstatdisease";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string d = "";
            while (reader.Read())
            {
                d = reader.GetString("disease");
                cboDisease.Items.Add(d);
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT DISTINCT(year) FROM tblstatdisease";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string y = "";
            while (reader1.Read())
            {
                y = reader1.GetString("year");
                cboYearDisease.Items.Add(y);
                cboYearDisease.SelectedItem = y;
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT DISTINCT(year) FROM tblstatpatient";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            string y1 = "";
            while (reader2.Read())
            {
                y1 = reader2.GetString("year");
                cboYearPatient.Items.Add(y1);
                cboYearPatient.SelectedItem = y1;
            }
            conn.Close();

            string date = DateTime.Now.ToShortDateString();
            var year = DateTime.Parse(date).Year;
            cboYearDisease.Text = year.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void pnlSlctnHome_Click(object sender, EventArgs e)
        {
            pnlHome.Show();
            usrCntrlPatientsRecordAdmin1.Hide();
            usrCntrlStatisticalReport1.Hide();
            usrCntrlUsersMonitoring1.Hide();
            usrCntrlAccountAdmin1.Hide();
            usrCntrlProfile1.Hide();
            if (pnlSlctnHome.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnPatientsRecord_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientsRecordAdmin1.Show();
            usrCntrlStatisticalReport1.Hide();
            usrCntrlUsersMonitoring1.Hide();
            usrCntrlAccountAdmin1.Hide();
            usrCntrlProfile1.Hide();
            if (pnlSlctnPatientsRecord.Checked == true) 
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnPatientsRecord.Checked == false)
            {
                pnlSlctnPatientsRecord.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnStatisticalReport_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientsRecordAdmin1.Hide();
            usrCntrlStatisticalReport1.Show();
            usrCntrlUsersMonitoring1.Hide();
            usrCntrlAccountAdmin1.Hide();
            usrCntrlProfile1.Hide();
            if (pnlSlctnStatisticalReport.Checked == true) 
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.Black;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnStatisticalReport.Checked == false)
            {
                pnlSlctnStatisticalReport.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.Black;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnUsersMonitoring_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientsRecordAdmin1.Hide();
            usrCntrlStatisticalReport1.Hide();
            usrCntrlUsersMonitoring1.Show();
            usrCntrlAccountAdmin1.Hide();
            usrCntrlProfile1.Hide();
            if (pnlSlctnUsersMonitoring.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.Black;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnUsersMonitoring.Checked == false)
            {
                pnlSlctnUsersMonitoring.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.Black;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnDoctor_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientsRecordAdmin1.Hide();
            usrCntrlStatisticalReport1.Hide();
            usrCntrlUsersMonitoring1.Hide();
            usrCntrlAccountAdmin1.Show();
            usrCntrlProfile1.Hide();
            if (pnlSlctnDoctor.Checked == true) 
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.Black;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnDoctor.Checked == false)
            {
                pnlSlctnDoctor.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.Black;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnBackup_Click(object sender, EventArgs e) 
        {
            this.Hide();
            frmBackupRestore f1 = new frmBackupRestore();
            f1.Show();
            if (pnlSlctnBackup.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.Black;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnBackup.Checked == false)
            {
                pnlSlctnBackup.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.Black;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnConfiguration_Click(object sender, EventArgs e)
        {
            frmCheckConnection f1 = new frmCheckConnection();
            f1.Show();
            if (pnlSlctnConfiguration.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnConfiguration.Checked == false)
            {
                pnlSlctnConfiguration.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnProfile_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientsRecordAdmin1.Hide();
            usrCntrlStatisticalReport1.Hide();
            usrCntrlUsersMonitoring1.Hide();
            usrCntrlAccountAdmin1.Hide();
            usrCntrlProfile1.Show();
            if (pnlSlctnProfile.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnProfile.Checked == false)
            {
                pnlSlctnProfile.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnStatisticalReport.ForeColor = Color.White;
                pnlSlctnUsersMonitoring.ForeColor = Color.White;
                pnlSlctnDoctor.ForeColor = Color.White;
                pnlSlctnBackup.ForeColor = Color.White;
                pnlSlctnConfiguration.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnStatisticalReport.Checked = false;
                pnlSlctnUsersMonitoring.Checked = false;
                pnlSlctnDoctor.Checked = false;
                pnlSlctnBackup.Checked = false;
                pnlSlctnConfiguration.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnLogout_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlll = "UPDATE tblAccounts SET logInStatus = 'INACTIVE' WHERE IDNumber = @id";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@id", lblIDNumber.Text);
            cmddd.ExecuteNonQuery();
            conn.Close();

            //Activity Log
            string accType = "ADMINISTRATOR";
            string activity = "SUCCESSFULLY LOGGED OUT.";

            conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmLogin f1 = new frmLogin();
            f1.Show();
        }

        private void btnUpdateAccountProfile_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    byte[] img = null;
                    FileStream fs = new FileStream(lblPath.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);

                    conn.Open();
                    string sql = "UPDATE tblAccount_Profile SET lastName = @lname, firstName = @fname, middleName = @mname, birthday = @bday, profilePicture = @picture, pathName = @path WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@lname", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text);
                    cmd.Parameters.AddWithValue("@bday", dtBirthday.Value);
                    cmd.Parameters.AddWithValue("@picture", img);
                    cmd.Parameters.AddWithValue("@path", lblPath.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "ADMINISTRATOR";
                    string activity = "SUCCESSFULLY UPDATED PERSONAL INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Personal Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Select image first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtLastName.Text == "")
                    txtLastName.Focus();
                else if (txtFirstName.Text == "")
                    txtFirstName.Focus();
                else if (txtMiddleName.Text == "")
                    txtMiddleName.Focus();
                else if (txtUsername.Text == "")
                    txtUsername.Focus();
                else if (txtPassword.Text == "")
                    txtPassword.Focus();
            }
        }

        private void btnUpdateAccountSettings_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "" && txtRetypePassword.Text != "")
            {
                if (txtRetypePassword.Text == txtPassword.Text)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "UPDATE tblAccounts SET username =  @username, password = @password WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "ADMINISTRATOR";
                    string activity = "SUCCESSFULLY UPDATED ACCOUNT INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Account Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChange.Enabled = true;
                    txtRetypePassword.Enabled = false;
                    txtRetypePassword.Clear();
                    btnCancel.Visible = false;
                }
                else
                {
                    MessageBox.Show("Password didn't match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.SelectionStart = 0;
                    txtPassword.SelectionLength = txtPassword.TextLength;
                    txtPassword.Focus();
                }
            }
            else
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string imgLocation = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG files(*.jpg)|*.jpg|PNG files(*.png)|*png|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pctrProfile.ImageLocation = imgLocation;
                lblPath.Text = imgLocation;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
            txtPassword.Focus();
            btnChange.Enabled = false;
            txtRetypePassword.Enabled = true;
            btnCancel.Visible = true;
        }

        private void btnShowPatient_Click(object sender, EventArgs e)
        {
            if (cboYearPatient.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                MySqlCommand com = new MySqlCommand("SELECT * FROM tblstatpatient WHERE year = '" + cboYearPatient.Text + "' ORDER BY MONTH(date) ASC", conn);
                MySqlDataReader dr = com.ExecuteReader();
                this.chart2.Series["Pedia"].Points.Clear();
                this.chart2.Series["Adult"].Points.Clear();
                while (dr.Read())
                {
                    this.chart2.Series["Pedia"].Points.AddXY(dr.GetString("month"), dr.GetInt32("pedia"));
                    this.chart2.Series["Adult"].Points.AddXY(dr.GetString("month"), dr.GetInt32("adult"));
                }
                conn.Close();
                lbl1.Visible = true;
            }
            else
            {
                if (cboYearPatient.Text == "")
                {
                    MessageBox.Show("Indicate Year first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboDisease.Focus();
                }
            }
        }

        private void lnkLblPrintDisease_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPrintDiseaseData f1 = new frmPrintDiseaseData();

            DataSet1 ds = new DataSet1();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT disease, pedia, adult, count FROM tblstatdisease WHERE count >= 1 ORDER BY count DESC LIMIT 10", conn);
            da.Fill(ds, ds.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("dsDiseasePediaAdult", ds.Tables[0]);
            f1.reportViewer1.LocalReport.DataSources.Clear();
            f1.reportViewer1.LocalReport.DataSources.Add(rds);
            f1.reportViewer1.LocalReport.Refresh();
            f1.reportViewer1.RefreshReport();

            f1.Show();
        }

        private void lnkLblPrintMaleFemale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPrintDiseaseMaleFemaleData f1 = new frmPrintDiseaseMaleFemaleData();

            DataSet1 ds = new DataSet1();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT disease, L1M, L1F, OM, OF, FM, FF, TM, TF, FFM, FFF, TWM, TWF, FFFM, FFFF, GSFM, GSFF, count FROM tblstatdisease WHERE count >= 1 ORDER BY count DESC LIMIT 10", conn);
            da.Fill(ds, ds.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("dsDiseaseMaleFemale", ds.Tables[0]);
            f1.reportViewer1.LocalReport.DataSources.Clear();
            f1.reportViewer1.LocalReport.DataSources.Add(rds);
            f1.reportViewer1.LocalReport.Refresh();
            f1.reportViewer1.RefreshReport();

            f1.Show();
        }

        private void cboDisease_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDisease.Text != "" && cboYearDisease.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                MySqlCommand com = new MySqlCommand("SELECT * FROM tblstatdisease WHERE disease = '" + cboDisease.Text + "' AND year = '" + cboYearDisease.Text + "' ORDER BY MONTH(date) ASC", conn);
                MySqlDataReader dr = com.ExecuteReader();
                this.chart1.Series["Disease"].Points.Clear();
                while (dr.Read())
                {
                    this.chart1.Series["Disease"].Points.AddXY(dr.GetString("month"), dr.GetInt32("count"));
                }
                conn.Close();
                lbl.Visible = true;
            }
            else
            {
                if (cboDisease.Text == "")
                {
                    MessageBox.Show("Select Disease first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboDisease.Focus();
                }
                else
                {
                    MessageBox.Show("Indicate Year first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboYearDisease.Focus();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRetypePassword.Clear();
            txtRetypePassword.Enabled = false;
            btnCancel.Visible = false;
            btnChange.Enabled = true;
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        
    }
}
