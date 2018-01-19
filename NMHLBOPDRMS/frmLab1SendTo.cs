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
    public partial class frmLab1SendTo : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmLab1SendTo()
        {
            InitializeComponent();
        }

        public string labIdNumber = "";

        public string lname = "";
        public string fname = "";
        public string mname = "";
        public string status = "";

        public void close()
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }

            frmMainLaboratory1 f2 = new frmMainLaboratory1();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = 'LABORATORY 1' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            string id = "";
            string username = "";
            string password = "";
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                id = reader3.GetString("IDNumber");
                username = reader3.GetString("username");
                password = reader3.GetString("password");
            }
            conn.Close();

            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", id);
            string lname = "";
            string fname = "";
            string mname = "";
            string birthday = "";
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                lname = reader4.GetString("lastname");
                fname = reader4.GetString("firstname");
                mname = reader4.GetString("middlename");
                birthday = reader4.GetString("birthday");

                if (reader4["profilePicture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])(reader4["profilePicture"]);
                    f2.pctrProfile.Image = null;

                    MemoryStream ms = new MemoryStream(img);
                    f2.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            conn.Close();

            f2.lblNameOfLaboratory.Text = lname + ", " + fname + " " + mname;
            f2.txtIDNumber.Text = id;
            f2.txtUsername.Text = username;
            f2.txtPassword.Text = password;
            f2.lblIDNumber.Text = id;
            f2.txtLastName.Text = lname;
            f2.txtFirstName.Text = fname;
            f2.txtMiddleName.Text = mname;
            f2.dtBirthday.Text = birthday;
            f2.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlll = "SELECT * FROM tblNext_In_Line_Laboratory1 WHERE patientNo = @patientNo";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@patientNo", lblNumber.Text);
            MySqlDataReader readerrr = cmddd.ExecuteReader();
            while (readerrr.Read())
            {
                lname = readerrr.GetString("lastName");
                fname = readerrr.GetString("firstName");
                mname = readerrr.GetString("middleName");
                status = readerrr.GetString("status");
            }
            conn.Close();
            if (rdoDoctor.Checked == true)
            {
                if (cboDoctorsName.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    this.Hide();
                    frmLaboratory1 f1 = new frmLaboratory1();

                    conn.Open();
                    string sqll = "SELECT * FROM tblNext_In_Line_Doctor WHERE patientNo = @pNo AND sentBy = @sentby";
                    MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                    cmdd.Parameters.AddWithValue("@pNo", lblNumber.Text);
                    cmdd.Parameters.AddWithValue("@sentby", "LABORATORY");
                    MySqlDataReader readerr = cmdd.ExecuteReader();
                    int x = 0;
                    while (readerr.Read())
                    {
                        x++;
                    }
                    conn.Close();
                    if (x == 0)
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor", conn);
                        Int32 a = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();

                        conn.Open();
                        string sql1 = "INSERT INTO tblNext_In_Line_Doctor VALUES (@numID, @patientNo, @lname, @fname, @mname, @status, @daterequested, @dateexamined, @sentby, @sentTo); DELETE FROM tblNext_In_Line_Laboratory1 WHERE patientNo = @patientNo";
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                        cmd1.Parameters.AddWithValue("@numID", a = a + 1);
                        cmd1.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                        cmd1.Parameters.AddWithValue("@lname", lname);
                        cmd1.Parameters.AddWithValue("@fname", fname);
                        cmd1.Parameters.AddWithValue("@mname", mname);
                        cmd1.Parameters.AddWithValue("@status", status);
                        cmd1.Parameters.AddWithValue("@daterequested", dtRequested.Value);
                        cmd1.Parameters.AddWithValue("@dateexamined", dtPicker.Value);
                        cmd1.Parameters.AddWithValue("@sentby", "LABORATORY");
                        cmd1.Parameters.AddWithValue("@sentTo", cboDoctorsName.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        string sql = "DELETE FROM tblNext_In_Line_Laboratory1 WHERE patientNo = @patientNo";
                        MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                        cmd1.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }

                    conn.Open();
                    string sql2 = "INSERT INTO tblLaboratory1_Results VALUES (@patientNo, @lname, @fname, @mname, @status, @daterequested, @dateexamined)";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                    cmd2.Parameters.AddWithValue("@lname", lname);
                    cmd2.Parameters.AddWithValue("@fname", fname);
                    cmd2.Parameters.AddWithValue("@mname", mname);
                    cmd2.Parameters.AddWithValue("@status", status);
                    cmd2.Parameters.AddWithValue("@daterequested", dtRequested.Value);
                    cmd2.Parameters.AddWithValue("@dateexamined", dtPicker.Value);
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    MySqlCommand cmd8 = new MySqlCommand("SELECT COUNT(*) FROM tbldrafts_nurse", conn);
                    Int32 count1 = Convert.ToInt32(cmd8.ExecuteScalar());
                    conn.Close();

                    conn.Open();
                    string sql9 = "INSERT INTO tbldrafts_nurse VALUES (@num, @patientnum, @lastname, @firstname, @middlename, @status, @daterequested, @dateexamined, @sentby, @sentto)";
                    MySqlCommand cmd9 = new MySqlCommand(sql9, conn);
                    cmd9.Parameters.AddWithValue("@num", count1 = count1 + 1);
                    cmd9.Parameters.AddWithValue("@patientnum", lblNumber.Text);
                    cmd9.Parameters.AddWithValue("@lastname", lname);
                    cmd9.Parameters.AddWithValue("@firstname", fname);
                    cmd9.Parameters.AddWithValue("@middlename", mname);
                    cmd9.Parameters.AddWithValue("@status", status);
                    cmd9.Parameters.AddWithValue("@daterequested", dtRequested.Value);
                    cmd9.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                    cmd9.Parameters.AddWithValue("@sentby", "LABORATORY");
                    cmd9.Parameters.AddWithValue("@sentto", cboDoctorsName.Text);
                    cmd9.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql4, conn);
                    cmd5.Parameters.AddWithValue("@id", labIdNumber);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader1 = cmd5.ExecuteReader();
                    while (reader1.Read())
                    {
                        lnamee = reader1.GetString("lastname");
                        fnamee = reader1.GetString("firstname");
                        mnamee = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + lblName.Text + ") TO DOCTOR( " + cboDoctorsName.Text + " ) .";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient successfully sent to Doctor's Next In Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    close();
                }
                else
                    MessageBox.Show("Select Doctor first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdoNurse.Checked == true)
            {
                if (cboNurseName.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    this.Hide();
                    frmLaboratory1 f1 = new frmLaboratory1();

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblWaiting_Patient_Nurse", conn);
                    Int32 a = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();

                    conn.Open();
                    string sql = "INSERT INTO tblWaiting_Patient_Nurse VALUES (@numID, @patientNo, @lname, @fname, @mname, @status, @daterequested, @dateexamined, @sentby, @sentto); DELETE FROM tblNext_In_Line_Laboratory1 WHERE patientNo = @patientNo";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("@numID", a = a + 1);
                    cmd1.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                    cmd1.Parameters.AddWithValue("@lname", lname);
                    cmd1.Parameters.AddWithValue("@fname", fname);
                    cmd1.Parameters.AddWithValue("@mname", mname);
                    cmd1.Parameters.AddWithValue("@status", status);
                    cmd1.Parameters.AddWithValue("@daterequested", f1.dtRequestedLab.Value);
                    cmd1.Parameters.AddWithValue("@dateexamined", f1.dtExaminedLab.Value);
                    cmd1.Parameters.AddWithValue("@sentby", "LABORATORY");
                    cmd1.Parameters.AddWithValue("@sentto", cboNurseName.Text);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    string sql1 = "INSERT INTO tblLaboratory1_Results VALUES (@patientNo, @lname, @fname, @mname, @status, @daterequested, @dateexamined)";
                    MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
                    cmd2.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                    cmd2.Parameters.AddWithValue("@lname", lname);
                    cmd2.Parameters.AddWithValue("@fname", fname);
                    cmd2.Parameters.AddWithValue("@mname", mname);
                    cmd2.Parameters.AddWithValue("@status", status);
                    cmd2.Parameters.AddWithValue("@daterequested", f1.dtRequestedLab.Value);
                    cmd2.Parameters.AddWithValue("@dateexamined", f1.dtExaminedLab.Value);
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql4, conn);
                    cmd5.Parameters.AddWithValue("@id", labIdNumber);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader1 = cmd5.ExecuteReader();
                    while (reader1.Read())
                    {
                        lnamee = reader1.GetString("lastname");
                        fnamee = reader1.GetString("firstname");
                        mnamee = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + lblName.Text + ") TO NURSE( " + cboNurseName.Text + " ) .";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient successfully sent to Nurse's Patient Waiting Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    close();
                }
                else
                    MessageBox.Show("Select Doctor first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmLab1SendTo_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();

            string day = DateTime.Now.ToString("dddd");
            conn.Open();
            string sql = "SELECT * FROM tbldoctors_schedule WHERE schedule = '" + day + "' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string doctorname = "";
            while (reader.Read())
            {
                doctorname = reader.GetString("name");
                cboDoctorsName.Items.Add(doctorname);
            }
            conn.Close();

            string name1 = "";
            conn.Open();
            string sql1 = "SELECT a.accountType, a.IDNumber, a.logInStatus, b.lastName, b.firstName , b.middleName FROM tblAccounts a JOIN tblAccount_Profile b ON a.IDNumber = b.IDNumber WHERE a.accountType = 'NURSE' AND a.logInStatus = 'ACTIVE' ";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string nurseLname = "";
            string nurseFname= "";
            string nurseMname = "";
            while (reader1.Read())
            {
                nurseLname = reader1.GetString("lastName");
                nurseFname = reader1.GetString("firstName");
                nurseMname = reader1.GetString("middleName");
                name1 = nurseFname + " " + nurseMname + " " + nurseLname;
                cboNurseName.Items.Add(name1);
            }
            conn.Close();
        }

        private void rdoDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDoctor.Checked == true)
            {
                cboDoctorsName.Enabled = true;
                btnSend.Enabled = true;
            }
            else
                cboDoctorsName.Enabled = false;
        }

        private void rdoNurse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNurse.Checked == true)
            {
                cboNurseName.Enabled = true;
                btnSend.Enabled = true;
            }
            else
                cboNurseName.Enabled = false;
        }

        private void cboDoctorsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string day = "";
            day = DateTime.Now.ToString("dddd");
            conn.Open();
            string sql = "SELECT * FROM tbldoctors_schedule WHERE name = '" + cboDoctorsName.Text + "' AND schedule = '" + day + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string time = "";
            while (reader.Read())
            {
                time = reader.GetString("time");
            }
            conn.Close();
            lblTime.Text = time;
        }

    }
}
