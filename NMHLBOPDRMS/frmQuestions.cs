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
    public partial class frmQuestions : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public string id = "";

        public frmQuestions()
        {
            InitializeComponent();
        }
        
        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string[] qnum = new string[2];
            qnum[0] = "1";
            qnum[1] = "2";

            string[] question = new string[2];
            question[0] = cboQuestions1.Text;
            question[1] = cboQuestions2.Text;

            string[] answer = new string[2];
            answer[0] = txtAnswer1.Text;
            answer[1] = txtAnswer2.Text;

            for (int i = 0; i < 2; i++)
            {
                conn.Open();
                string sql = "INSERT INTO tblQuestions VALUES (@id, @qnum, @question, @answer)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@qnum", qnum[i]);
                cmd.Parameters.AddWithValue("@question", question[i]);
                cmd.Parameters.AddWithValue("@answer", answer[i]);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Security Questions completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Open();
            string sql1 = "SELECT * FROM tblAccounts WHERE IDNumber = '" + id + "' ";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            MySqlDataReader dr = cmd1.ExecuteReader();
            string accountType = "";
            string lname = "";
            string fname = "";
            string mname = "";
            while (dr.Read())
            {
                accountType = dr.GetString("accountType");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr1 = cmd2.ExecuteReader();
            while (dr1.Read())
            {
                lname = dr1.GetString("lastname");
                fname = dr1.GetString("firstname");
                mname = dr1.GetString("middlename");
            }
            conn.Close();

            if (accountType == "ADMINISTRATOR")
            {
                string activity = "SUCCESSFULLY SAVED SECURITY QUESTIONS.";
                conStr.activityLog(accountType, fname, mname, lname, activity);

                frmMainAdmin f1 = new frmMainAdmin();

                conn.Open();
                string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string username = "";
                string password = "";
                while (reader3.Read())
                {
                    username = reader3.GetString("username");
                    password = reader3.GetString("password");
                }
                conn.Close();

                conn.Open();
                string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader4 = cmd4.ExecuteReader();
                string lnamee = "";
                string fnamee = "";
                string mnamee = "";
                string birthday = "";
                while (reader4.Read())
                {
                    lnamee = reader4.GetString("lastName");
                    fnamee = reader4.GetString("firstName");
                    mnamee = reader4.GetString("middleName");
                    birthday = reader4.GetString("birthday");

                    if (reader4["pathName"] != System.DBNull.Value)
                    {
                        string path = reader4.GetString("pathName");
                        f1.lblPath.Text = null;
                        f1.lblPath.Text = path;
                    }

                    if (reader4["profilePicture"] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])(reader4["profilePicture"]);
                        f1.pctrProfile.Image = null;

                        MemoryStream ms = new MemoryStream(img);
                        f1.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                conn.Close();

                this.Hide();

                f1.txtIDNumber.Text = id;
                f1.txtUsername.Text = username;
                f1.txtPassword.Text = password;
                f1.txtLastName.Text = lname;
                f1.lblNameOfAdmin.Text = fname + " " + mname + " " + lname;
                f1.lblIDNumber.Text = id;
                f1.txtFirstName.Text = fname;
                f1.txtMiddleName.Text = mname;
                f1.dtBirthday.Text = birthday;
                f1.Show();
            }
            else if (accountType == "DOCTOR")
            {
                string activity = "SUCCESSFULLY SAVED SECURITY QUESTIONS.";
                conStr.activityLog(accountType, fname, mname, lname, activity);

                frmMainDoctor f2 = new frmMainDoctor();

                string doctor = "";

                conn.Open();
                string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string username = "";
                string password = "";
                while (reader3.Read())
                {
                    username = reader3.GetString("username");
                    password = reader3.GetString("password");
                }
                conn.Close();

                conn.Open();
                string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader4 = cmd4.ExecuteReader();
                string lnamee = "";
                string fnamee = "";
                string mnamee = "";
                string birthday = "";
                string position = "";
                while (reader4.Read())
                {
                    lnamee = reader4.GetString("lastName");
                    fnamee = reader4.GetString("firstName");
                    mnamee = reader4.GetString("middleName");
                    birthday = reader4.GetString("birthday");
                    position = reader4.GetString("doctorPosition");

                    if (reader4["pathName"] != System.DBNull.Value)
                    {
                        string path = reader4.GetString("pathName");
                        f2.lblPath.Text = null;
                        f2.lblPath.Text = path;
                    }

                    if (reader4["profilePicture"] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])(reader4["profilePicture"]);
                        f2.pctrProfile.Image = null;

                        MemoryStream ms = new MemoryStream(img);
                        f2.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                conn.Close();

                this.Hide();
                f2.txtIDNumber.Text = id;
                f2.txtUsername.Text = username;
                f2.txtPassword.Text = password;
                f2.lblNameOfDoctor.Text = fname + " " + mname + " " + lname;
                f2.lblIDNumber.Text = id;
                f2.txtLastName.Text = lname;
                f2.txtFirstName.Text = fname;
                f2.txtMiddleName.Text = mname;
                f2.dtBirthday.Text = birthday;

                doctor = "Dr. " + fname + " " + mname + " " + lname + " (" + position + ")";

                conn.Open();
                string sql6 = "SELECT patientNo AS 'Patient No.', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name' ,status AS 'Status', sentBy AS 'Sent By', daterequested AS 'Date Requested', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tblNext_In_Line_Doctor WHERE sentTo = '" + doctor + "' ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql6, conn);
                DataSet dS = new DataSet();
                adapter.Fill(dS, "tblNext_In_Line_Doctor");
                f2.dgNextInLineDoctor.DataMember = "tblNext_In_Line_Doctor";
                f2.dgNextInLineDoctor.DataSource = dS;
                conn.Close();

                f2.lblDoctor.Text = doctor;
                f2.Show();

                conn.Open();
                MySqlCommand cmdd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor WHERE sentTo = '" + doctor + "' ", conn);
                Int32 count = Convert.ToInt32(cmdd.ExecuteScalar());
                conn.Close();
                notifyIcon1.BalloonTipText = "You have " + count + " Patient in your Next in Line Patient.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
                notifyIcon1.Icon = null;
            }
            else if (accountType == "NURSE")
            {
                string activity = "SUCCESSFULLY SAVED SECURITY QUESTIONS.";
                conStr.activityLog(accountType, fname, mname, lname, activity);

                frmMainNurse f3 = new frmMainNurse();

                conn.Open();
                string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string username = "";
                string password = "";
                while (reader3.Read())
                {
                    username = reader3.GetString("username");
                    password = reader3.GetString("password");
                }
                conn.Close();

                conn.Open();
                string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader4 = cmd4.ExecuteReader();
                string lnamee = "";
                string fnamee = "";
                string mnamee = "";
                string birthday = "";
                while (reader4.Read())
                {
                    lnamee = reader4.GetString("lastName");
                    fnamee = reader4.GetString("firstName");
                    mnamee = reader4.GetString("middleName");
                    birthday = reader4.GetString("birthday");

                    if (reader4["pathName"] != System.DBNull.Value)
                    {
                        string path = reader4.GetString("pathName");
                        f3.lblPath.Text = null;
                        f3.lblPath.Text = path;
                    }

                    if (reader4["profilePicture"] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])(reader4["profilePicture"]);
                        f3.pctrProfile.Image = null;

                        MemoryStream ms = new MemoryStream(img);
                        f3.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                conn.Close();

                this.Hide();
                f3.txtIDNumber.Text = id;
                f3.txtUsername.Text = username;
                f3.txtPassword.Text = password;
                f3.lblNameOfNurse.Text = fname + " " + mname + " " + lname;
                f3.lblIDNumber.Text = id;
                f3.txtLastName.Text = lname;
                f3.txtFirstName.Text = fname;
                f3.txtMiddleName.Text = mname;
                f3.dtBirthday.Text = birthday;
                f3.Show();

                conn.Open();
                MySqlCommand cmdd = new MySqlCommand("SELECT COUNT(*) FROM tblWaiting_Patient_Nurse", conn);
                Int32 count = Convert.ToInt32(cmdd.ExecuteScalar());
                conn.Close();
                notifyIcon1.BalloonTipText = "You have " + count + " Patient in your Waiting Patient.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
                notifyIcon1.Icon = null;
            }
            else if (accountType == "LABORATORY 1")
            {
                string activity = "SUCCESSFULLY SAVED SECURITY QUESTIONS.";
                conStr.activityLog(accountType, fname, mname, lname, activity);

                frmMainLaboratory1 f4 = new frmMainLaboratory1();

                conn.Open();
                string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string username = "";
                string password = "";
                while (reader3.Read())
                {
                    username = reader3.GetString("username");
                    password = reader3.GetString("password");
                }
                conn.Close();

                conn.Open();
                string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader4 = cmd4.ExecuteReader();
                string lnamee = "";
                string fnamee = "";
                string mnamee = "";
                string birthday = "";
                while (reader4.Read())
                {
                    lnamee = reader4.GetString("lastName");
                    fnamee = reader4.GetString("firstName");
                    mnamee = reader4.GetString("middleName");
                    birthday = reader4.GetString("birthday");

                    if (reader4["pathName"] != System.DBNull.Value)
                    {
                        string path = reader4.GetString("pathName");
                        f4.lblPath.Text = null;
                        f4.lblPath.Text = path;
                    }

                    if (reader4["profilePicture"] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])(reader4["profilePicture"]);
                        f4.pctrProfile.Image = null;

                        MemoryStream ms = new MemoryStream(img);
                        f4.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                conn.Close();

                conn.Open();
                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                cmddd.Parameters.AddWithValue("@id", id);
                cmddd.ExecuteNonQuery();
                conn.Close();

                this.Hide();
                f4.txtIDNumber.Text = id;
                f4.txtUsername.Text = username;
                f4.txtPassword.Text = password;
                f4.lblNameOfLaboratory.Text = fname + " " + mname + " " + lname;
                f4.lblIDNumber.Text = id;
                f4.txtLastName.Text = lname;
                f4.txtFirstName.Text = fname;
                f4.txtMiddleName.Text = mname;
                f4.dtBirthday.Text = birthday;
                f4.Show();

                conn.Open();
                MySqlCommand cmdd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory1", conn);
                Int32 count = Convert.ToInt32(cmdd.ExecuteScalar());
                conn.Close();
                notifyIcon1.BalloonTipText = "You have " + count + " Patient in your Next in Line Patient.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
                notifyIcon1.Icon = null;
            }
            else if (accountType == "LABORATORY 2")
            {
                string activity = "SUCCESSFULLY SAVED SECURITY QUESTIONS.";
                conStr.activityLog(accountType, fname, mname, lname, activity);

                frmMainLaboratory2 f5 = new frmMainLaboratory2();

                conn.Open();
                string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                string username = "";
                string password = "";
                while (reader3.Read())
                {
                    username = reader3.GetString("username");
                    password = reader3.GetString("password");
                }
                conn.Close();

                conn.Open();
                string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader4 = cmd4.ExecuteReader();
                string lnamee = "";
                string fnamee = "";
                string mnamee = "";
                string birthday = "";
                while (reader4.Read())
                {
                    lnamee = reader4.GetString("lastName");
                    fnamee = reader4.GetString("firstName");
                    mnamee = reader4.GetString("middleName");
                    birthday = reader4.GetString("birthday");

                    if (reader4["pathName"] != System.DBNull.Value)
                    {
                        string path = reader4.GetString("pathName");
                        f5.lblPath.Text = null;
                        f5.lblPath.Text = path;
                    }

                    if (reader4["profilePicture"] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])(reader4["profilePicture"]);
                        f5.pctrProfile.Image = null;

                        MemoryStream ms = new MemoryStream(img);
                        f5.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                conn.Close();

                this.Hide();
                f5.txtIDNumber.Text = id;
                f5.txtUsername.Text = username;
                f5.txtPassword.Text = password;
                f5.lblNameOfLaboratory.Text = fname + " " + mname + " " + lname;
                f5.lblIDNumber.Text = id;
                f5.txtLastName.Text = lname;
                f5.txtFirstName.Text = fname;
                f5.txtMiddleName.Text = mname;
                f5.dtBirthday.Text = birthday;
                f5.Show();

                conn.Open();
                MySqlCommand cmdd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory2", conn);
                Int32 count = Convert.ToInt32(cmdd.ExecuteScalar());
                conn.Close();
                notifyIcon1.BalloonTipText = "You have " + count + " Patient in your Next in Line Patient.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
                notifyIcon1.Icon = null;
            }
        }
    }
}
