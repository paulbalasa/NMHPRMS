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
    public partial class frmMainLaboratory2 : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public frmMainLaboratory2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        public int count;

        public void getDataset()
        {
            conn.Open();
            string sql = "SELECT  examType AS 'Laboratory Examination', patientNo AS 'Patient No', lastName AS 'Last Name', firstName AS 'First Name',  middleName AS 'Middle Name',  status AS 'Status', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory2";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tblNext_In_Line_Laboratory2");
            dgNextInLineLaboratory2.DataMember = "tblNext_In_Line_Laboratory2";
            dgNextInLineLaboratory2.DataSource = dS;
            conn.Close();

            dgNextInLineLaboratory2.Columns[0].Width = 175;
            dgNextInLineLaboratory2.Columns[1].Width = 150;
            dgNextInLineLaboratory2.Columns[2].Width = 150;
            dgNextInLineLaboratory2.Columns[3].Width = 150;
            dgNextInLineLaboratory2.Columns[4].Width = 150;
            dgNextInLineLaboratory2.Columns[5].Width = 125;
            dgNextInLineLaboratory2.Columns[6].Width = 125;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory2", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void frmMainLaboratory2_Load(object sender, EventArgs e)
        {
            getDataset();

            dtPicker.Text = DateTime.Now.ToShortDateString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            timer1.Start();
        }

        private void pnlSlctnHome_Click(object sender, EventArgs e)
        {
            pnlHome.Show();
            usrCntrlLaboratoryResults2.Hide();
            usrCntrlLaboratoryResults2.Hide();
            usrCntrlProfileLaboratory21.Hide();
            if (pnlSlctnHome.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnNextInLine_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory2.Show();
            usrCntrlLaboratoryResults2.Hide();
            usrCntrlProfileLaboratory21.Hide();
            if (pnlSlctnNextInLine.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.Black;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnNextInLine.Checked == false)
            {
                pnlSlctnNextInLine.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.Black;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnLaboratoryResults_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory2.Hide();
            usrCntrlLaboratoryResults2.Show();
            usrCntrlProfileLaboratory21.Hide();
            if (pnlSlctnLaboratoryResults.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnLaboratoryResults.Checked == false)
            {
                pnlSlctnLaboratoryResults.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnProfile.Checked = false;
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
            string accType = "LABORATORY 2";
            string activity = "SUCCESSFULLY LOGGED OUT.";

            conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmLogin f1 = new frmLogin();
            f1.Show();
        }

        private void pnlSlctnProfile_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory2.Hide();
            usrCntrlLaboratoryResults2.Hide();
            usrCntrlProfileLaboratory21.Show();
            if (pnlSlctnProfile.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnProfile.Checked == false)
            {
                pnlSlctnProfile.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
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
                    string accType = "LABORATORY 2";
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
                    string accType = "LABORATORY 2";
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

        private void dgNextInLineLaboratory2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string lab = dgNextInLineLaboratory2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string number = dgNextInLineLaboratory2.Rows[e.RowIndex].Cells[1].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblNext_In_Line_Laboratory2 WHERE patientNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            string lname = "";
            string fname = "";
            string mname = "";
            string status = "";
            string date = "";
            string labExam = "";
            while (reader.Read())
            {
                lname = reader.GetString("lastName");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                status = reader.GetString("status");
                date = reader.GetString("daterequested");
            }
            conn.Close();

            frmLaboratory2 f1 = new frmLaboratory2();

            if (lab == "XRAY")
            {
                conn.Open();
                string sqll = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @num";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@num", number);
                MySqlDataReader readerr = cmdd.ExecuteReader();
                int x = 0;
                while (readerr.Read())
                {
                    x += 1;
                }
                conn.Close();

                if (x == 1)
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboTypeXray.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.metroTabControl2.SelectedIndex = 1;
                    f1.metroTabControl2.Refresh();
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
                else
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboTypeXray.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.btnSubmitXray.SendToBack();
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.metroTabControl2.SelectedIndex = 1;
                    f1.metroTabControl2.Refresh();
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
            }
            else if (lab == "ULTRASOUND")
            {
                conn.Open();
                string sqll = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@num", number);
                MySqlDataReader readerr = cmdd.ExecuteReader();
                int x = 0;
                while (readerr.Read())
                {
                    x += 1;
                }
                conn.Close();

                if (x == 1)
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboType.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
                else
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboType.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.btnSubmitUltrasound.SendToBack();
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
            }
            else if (lab == "ULTRASOUND / XRAY")
            {
                conn.Open();
                string sqll = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@num", number);
                MySqlDataReader readerr = cmdd.ExecuteReader();
                int x = 0;
                while (readerr.Read())
                {
                    x += 1;
                }
                conn.Close();

                if (x == 1)
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboType.Items.Add(labExam);
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @num";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        labExam = reader2.GetString("labExam");
                        f1.cboTypeXray.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
                else
                {
                    conn.Open();
                    string sql1 = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @num";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        labExam = reader1.GetString("labExam");
                        f1.cboType.Items.Add(labExam);
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @num";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@num", number);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        labExam = reader2.GetString("labExam");
                        f1.cboTypeXray.Items.Add(labExam);
                    }
                    conn.Close();

                    f1.lblNumberLab.Text = number;
                    f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                    f1.lblStatus.Text = status;
                    f1.dtRequestedLab.Text = date;
                    f1.btnSubmitUltrasound.SendToBack();
                    f1.labIdNumber = lblIDNumber.Text;
                    f1.cboSonologists.Items.Add(lblNameOfLaboratory.Text);
                    f1.Show();
                }
            }
        }

        private void tmrRefreshDGV_Tick(object sender, EventArgs e)
        {
            dS.Clear();

            conn.Open();
            string sql = "SELECT  examType AS 'Laboratory Examination', patientNo AS 'Patient No', lastName AS 'Last Name', firstName AS 'First Name',  middleName AS 'Middle Name',  status AS 'Status', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory2";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tblNext_In_Line_Laboratory2");
            dgNextInLineLaboratory2.DataMember = "tblNext_In_Line_Laboratory2";
            dgNextInLineLaboratory2.DataSource = dS;
            conn.Close();

            dgNextInLineLaboratory2.Columns[0].Width = 175;
            dgNextInLineLaboratory2.Columns[1].Width = 150;
            dgNextInLineLaboratory2.Columns[2].Width = 150;
            dgNextInLineLaboratory2.Columns[3].Width = 150;
            dgNextInLineLaboratory2.Columns[4].Width = 150;
            dgNextInLineLaboratory2.Columns[5].Width = 125;
            dgNextInLineLaboratory2.Columns[6].Width = 125;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory2", conn);
            int countt = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = countt.ToString() + " records.";

            if (countt > count)
            {
                notifyIcon1.BalloonTipText = "New Patient received.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
            }
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
