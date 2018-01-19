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
    public partial class frmLogin : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public frmLogin()
        {
            InitializeComponent();
        }

        public static int lockk = 0;

        private void login(string user, string pass)
        {
            lockk++;

            if (user != "" && pass != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                string accountType = "";

                conn.Open();
                string sql = "SELECT * FROM tblAccounts WHERE username = @user AND password = @pass";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                int x = 0;
                int lockStatus = 0;
                string id = "";
                string lname = "";
                string fname = "";
                string mname = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    x += 1;
                    lockStatus = reader.GetInt16("lockStatus");
                    accountType = reader.GetString("accountType");
                    id = reader.GetString("IDNumber");
                }
                conn.Close();

                conn.Open();
                string sql1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    lname = reader1.GetString("lastname");
                    fname = reader1.GetString("firstname");
                    mname = reader1.GetString("middlename");
                }
                conn.Close();

                if (x == 1)
                {
                    if (lockStatus == 0)
                    {
                        if (accountType == "ADMINISTRATOR")
                        {
                            if (countLog(id) >= 1)
                            {
                                frmMainAdmin f1 = new frmMainAdmin();

                                string activity = "SUCCESSFULLY LOGGED IN.";

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

                                activityLog(accountType, fnamee, mnamee, lnamee, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
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
                            else
                            {
                                string activity = "SUCCESSFULLY LOGGED IN.";
                                activityLog(accountType, fname, mname, lname, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Answer 2 Questions first for security purposes of your account incase you forgot your password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                frmQuestions f1 = new frmQuestions();
                                f1.id = id;
                                f1.Show();
                            }
                        }
                        else if (accountType == "DOCTOR")
                        {
                            if (countLog(id) >= 1)
                            {
                                frmMainDoctor f2 = new frmMainDoctor();

                                string activity = "SUCCESSFULLY LOGGED IN.";
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

                                activityLog(accountType, fnamee, mnamee, lnamee, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
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
                                adapter = new MySqlDataAdapter(sql6, conn);
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
                            else
                            {
                                string activity = "SUCCESSFULLY LOGGED IN.";
                                activityLog(accountType, fname, mname, lname, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Answer 2 Questions first for security purposes of your account incase you forgot your password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                frmQuestions f1 = new frmQuestions();
                                f1.id = id;
                                f1.Show();
                            }
                        }
                        else if (accountType == "NURSE")
                        {
                            if (countLog(id) >= 1)
                            {
                                frmMainNurse f3 = new frmMainNurse();

                                string activity = "SUCCESSFULLY LOGGED IN.";

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

                                activityLog(accountType, fnamee, mnamee, lnamee, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
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
                            else
                            {
                                string activity = "SUCCESSFULLY LOGGED IN.";
                                activityLog(accountType, fname, mname, lname, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Answer 2 Questions first for security purposes of your account incase you forgot your password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                frmQuestions f1 = new frmQuestions();
                                f1.id = id;
                                f1.Show();
                            }
                        }
                        else if (accountType == "LABORATORY 1")
                        {
                            if (countLog(id) >= 1)
                            {
                                frmMainLaboratory1 f4 = new frmMainLaboratory1();

                                string activity = "SUCCESSFULLY LOGGED IN.";

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

                                activityLog(accountType, fnamee, mnamee, lnamee, activity);

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
                            else
                            {
                                string activity = "SUCCESSFULLY LOGGED IN.";
                                activityLog(accountType, fname, mname, lname, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Answer 2 Questions first for security purposes of your account incase you forgot your password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                frmQuestions f1 = new frmQuestions();
                                f1.id = id;
                                f1.Show();
                            }
                        }
                        else if (accountType == "LABORATORY 2")
                        {
                            if (countLog(id) >= 1)
                            {
                                frmMainLaboratory2 f5 = new frmMainLaboratory2();

                                string activity = "SUCCESSFULLY LOGGED IN.";

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

                                activityLog(accountType, fnamee, mnamee, lnamee, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
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
                            else
                            {
                                string activity = "SUCCESSFULLY LOGGED IN.";
                                activityLog(accountType, fname, mname, lname, activity);

                                conn.Open();
                                string sqlll = "UPDATE tblAccounts SET logInStatus = 'ACTIVE', count = count + 1 WHERE IDNumber = @id";
                                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                                cmddd.Parameters.AddWithValue("@id", id);
                                cmddd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Answer 2 Questions first for security purposes of your account incase you forgot your password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                frmQuestions f1 = new frmQuestions();
                                f1.id = id;
                                f1.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You're account was locked. Contact System Administrator to unlock your account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
                else
                {
                    if (lockk > 4)
                    {
                        isLocked(user);
                    }
                    else
                    {
                        MessageBox.Show("Account doesn't exist! Check username or password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Username and Password are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtUsername.Text == "")
                    txtUsername.Focus();
                else if (txtPassword.Text == "")
                    txtPassword.Focus();
            }
        }

        public int countLog(string id)
        {
            int c = 0;
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblAccounts WHERE IDNumber = '" + id + "' ", conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                c = dr.GetInt32("count");
            }
            conn.Close();
            return c;
        }

        private void activityLog(string acctype, string fname, string mname, string lname, string activity)
        {
            conn.Open();
            MySqlCommand cmd5 = new MySqlCommand("SELECT COUNT(*) FROM tblActivity_Log", conn);
            Int32 count = Convert.ToInt32(cmd5.ExecuteScalar());
            conn.Close();

            conn.Open();
            string sqlll = "INSERT INTO tblActivity_Log VALUES (@logNum, @accType, @name, @activity, @date, @time)";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@logNum", count++);
            cmddd.Parameters.AddWithValue("@accType", acctype);
            cmddd.Parameters.AddWithValue("@name", fname + " " + mname + " " + lname);
            cmddd.Parameters.AddWithValue("@activity", activity);
            cmddd.Parameters.AddWithValue("@date", dtPicker.Value);
            cmddd.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
            cmddd.ExecuteNonQuery();
            conn.Close();
        }

        private void isLocked(string username)
        {
            conn.Open();
            string sql4 = "SELECT * FROM tblAccounts WHERE username = @user";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@user", username);
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            string accType = "";
            while (reader4.Read())
            {
                accType = reader4.GetString("accountType");
            }
            conn.Close();

            if (accType != "ADMINISTRATOR")
            {
                string fname = "SYSTEM";
                string mname = "";
                string lname = "";
                string acc = "SYSTEM";
                string activity = "SYTEM LOCKED " + accType + "'s ACCOUNT.";

                conn.Open();
                string sqll = "UPDATE tblAccounts SET lockStatus = 1 WHERE username = @user";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmdd.ExecuteNonQuery();
                conn.Close();

                activityLog(acc, fname, mname, lname, activity);

                MessageBox.Show("Your account has been locked! You've tried to log in 3 times incorrectly. Contact System Administrator to unlock your account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                frmLogin f1 = new frmLogin();
                f1.Show();
            }
            else
            {
                MessageBox.Show("Administrator Log-in Trial locked! " + Environment.NewLine + "You've tried to log in 3 times incorrectly. " + Environment.NewLine + "Log-in Trial Lock activated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frmAdminTrialLock f1 = new frmAdminTrialLock();
                f1.Show();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            login(username, password);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (e.KeyCode == Keys.Enter)
            {
                login(username, password);
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmConfirmation f1 = new frmConfirmation();
            f1.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
            label1.Focus();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #region textBox watermark
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.DarkSlateGray;
                txtUsername.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Username";
                txtUsername.ForeColor = Color.DarkSlateGray;
                txtUsername.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.DarkSlateGray;
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.DarkSlateGray;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void txtUsername_MouseEnter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.DarkSlateGray;
                txtUsername.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void txtUsername_MouseLeave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Username";
                txtUsername.ForeColor = Color.DarkSlateGray;
                txtUsername.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void txtPassword_MouseEnter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.DarkSlateGray;
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void txtPassword_MouseLeave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.DarkSlateGray;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.TextAlign = HorizontalAlignment.Left;
            }
        }
        #endregion
    
    }
}
