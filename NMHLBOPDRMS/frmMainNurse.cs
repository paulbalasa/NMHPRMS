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

namespace NMHLBOPDRMS
{
    public partial class frmMainNurse : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmMainNurse()
        {
            InitializeComponent();
        }


        private void frmMainNurse_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            timer1.Start();

            string name = "";
            conn.Open();
            string sql = "SELECT * FROM tblaccount_profile WHERE accountType = 'DOCTOR'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string fname = "";
            string mname = "";
            string lname = "";
            string position = "";
            while (reader.Read())
            {
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                position = reader.GetString("doctorPosition");
                name = "Dr. " + fname + " " + mname + " " + lname;
                cboDoctorsName.Items.Add(name + " (" + position + ")");
            }
            conn.Close();
        }

        public void getSchedule()
        {
            conn.Open();
            string sql2 = "SELECT schedule AS 'Day of Schedule', time AS 'Time of Schedule' FROM tbldoctors_schedule WHERE name = '" + cboDoctorsName.Text + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql2, conn);
            DataSet dS = new DataSet();
            adapter.Fill(dS, "tbldoctors_schedule");
            dgDoctorSchedule.DataMember = "tbldoctors_schedule";
            dgDoctorSchedule.DataSource = dS;
            conn.Close();

            dgDoctorSchedule.Columns[0].Width = 213;
            dgDoctorSchedule.Columns[1].Width = 500;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
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
            string accType = "NURSE";
            string activity = "SUCCESSFULLY LOGGED OUT.";

            conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmLogin f1 = new frmLogin();
            f1.Show();
        }

        private void pnlSlctnHome_Click(object sender, EventArgs e)
        {
            pnlHome.Show();
            usrCntrlPatientRecord1.Hide();
            usrCntrlWaitingPatientNurse1.Hide();
            usrCntrlNurseDrafts1.Hide();
            usrCntrlProfileNurse1.Hide();
            if (pnlSlctnHome.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnPatientsRecord_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecord1.Show();
            usrCntrlWaitingPatientNurse1.Hide();
            usrCntrlNurseDrafts1.Hide();
            usrCntrlProfileNurse1.Hide();
            if (pnlSlctnPatientsRecord.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnPatientsRecord.Checked == false)
            {
                pnlSlctnPatientsRecord.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnWaitingPatient_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecord1.Hide();
            usrCntrlWaitingPatientNurse1.Show();
            usrCntrlNurseDrafts1.Hide();
            usrCntrlProfileNurse1.Hide();
            if (pnlSlctnWaitingPatient.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.Black;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnWaitingPatient.Checked == false)
            {
                pnlSlctnWaitingPatient.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.Black;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnDrafts_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecord1.Hide();
            usrCntrlWaitingPatientNurse1.Hide();
            usrCntrlNurseDrafts1.Show();
            usrCntrlProfileNurse1.Hide();
            if (pnlSlctnDrafts.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnDrafts.Checked == false)
            {
                pnlSlctnDrafts.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnProfile_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecord1.Hide();
            usrCntrlWaitingPatientNurse1.Hide();
            usrCntrlProfileNurse1.Show();
            if (pnlSlctnProfile.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnProfile.Checked == false)
            {
                pnlSlctnProfile.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnWaitingPatient.ForeColor = Color.White;
                pnlSlctnDrafts.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnWaitingPatient.Checked = false;
                pnlSlctnDrafts.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
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
                    string accType = "NURSE";
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
                    string accType = "NURSE";
                    string activity = "SUCCESSFULLY UPDATED ACCOUNT INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Account Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChange.Enabled = true;
                    btnCancel.Visible = false;
                    txtRetypePassword.Enabled = false;
                    txtRetypePassword.Clear();
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

        private void cboDoctorsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSchedule();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRetypePassword.Clear();
            txtRetypePassword.Enabled = false;
            btnCancel.Visible = false;
            btnChange.Enabled = true;
        }
    }
}
