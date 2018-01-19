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
    public partial class frmNurseSendTo : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmNurseSendTo()
        {
            InitializeComponent();
        }

        public string number = "";
        public string lname = "";
        public string fname = "";
        public string mname = "";
        public string status = "";
        public string date = "";

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cboDoctorsName.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor", conn);
                    Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();

                    conn.Open();
                    string sql = "INSERT INTO tblNext_In_Line_Doctor VALUES (@num, @patientnum, @lastname, @firstname, @middlename, @status, @daterequested, @dateexamined, @sentby, @sentto)";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("@num", count = count + 1);
                    cmd1.Parameters.AddWithValue("@patientnum", number);
                    cmd1.Parameters.AddWithValue("@lastname", lname);
                    cmd1.Parameters.AddWithValue("@firstname", fname);
                    cmd1.Parameters.AddWithValue("@middlename", mname);
                    cmd1.Parameters.AddWithValue("@status", status);
                    cmd1.Parameters.AddWithValue("@daterequested", dtRequested.Value);
                    cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                    cmd1.Parameters.AddWithValue("@sentby", "NURSE");
                    cmd1.Parameters.AddWithValue("@sentto", cboDoctorsName.Text);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    MySqlCommand cmd8 = new MySqlCommand("SELECT COUNT(*) FROM tbldrafts_nurse", conn);
                    Int32 count1 = Convert.ToInt32(cmd8.ExecuteScalar());
                    conn.Close();

                    conn.Open();
                    string sql9 = "INSERT INTO tbldrafts_nurse VALUES (@num, @patientnum, @lastname, @firstname, @middlename, @status, @daterequested, @dateexamined, @sentby, @sentto)";
                    MySqlCommand cmd9 = new MySqlCommand(sql9, conn);
                    cmd9.Parameters.AddWithValue("@num", count1 = count1 + 1);
                    cmd9.Parameters.AddWithValue("@patientnum", number);
                    cmd9.Parameters.AddWithValue("@lastname", lname);
                    cmd9.Parameters.AddWithValue("@firstname", fname);
                    cmd9.Parameters.AddWithValue("@middlename", mname);
                    cmd9.Parameters.AddWithValue("@status", status);
                    cmd9.Parameters.AddWithValue("@daterequested", dtRequested.Value);
                    cmd9.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                    cmd9.Parameters.AddWithValue("@sentby", "NURSE");
                    cmd9.Parameters.AddWithValue("@sentto", cboDoctorsName.Text);
                    cmd9.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
                    MySqlCommand cmd3 = new MySqlCommand(sql1, conn);
                    string num = "";
                    MySqlDataReader reader = cmd3.ExecuteReader();
                    while (reader.Read())
                    {
                        num = reader.GetString("IDNumber");
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql2, conn);
                    cmd5.Parameters.AddWithValue("@id", num);
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

                    string acctype = "NURSE";
                    string activityy = "PATIENT (" + fname + " " + mname + " " + lname + ") SENT TO " + cboDoctorsName.Text + ".";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Successfully sent to Doctor!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        Application.OpenForms[i].Hide();
                    }

                    frmMainNurse f1 = new frmMainNurse();

                    conn.Open();
                    string sqll = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
                    MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                    string id = "";
                    string username = "";
                    string password = "";
                    MySqlDataReader readerr = cmdd.ExecuteReader();
                    while (readerr.Read())
                    {
                        id = readerr.GetString("IDNumber");
                        username = readerr.GetString("username");
                        password = readerr.GetString("password");
                    }
                    conn.Close();

                    conn.Open();
                    string sqll1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmdd1 = new MySqlCommand(sqll1, conn);
                    cmdd1.Parameters.AddWithValue("@id", id);
                    string lnam = "";
                    string fnam = "";
                    string mnam = "";
                    string birthday = "";
                    MySqlDataReader readerr1 = cmdd1.ExecuteReader();
                    while (readerr1.Read())
                    {
                        lnam = readerr1.GetString("lastname");
                        fnam = readerr1.GetString("firstname");
                        mnam = readerr1.GetString("middlename");
                        birthday = readerr1.GetString("birthday");

                        if (readerr1["profilePicture"] != System.DBNull.Value)
                        {
                            byte[] img = (byte[])(readerr1["profilePicture"]);
                            f1.pctrProfile.Image = null;

                            MemoryStream ms = new MemoryStream(img);
                            f1.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }
                    conn.Close();

                    f1.lblNameOfNurse.Text = lnam + ", " + fnam + " " + mnam;
                    f1.txtIDNumber.Text = id;
                    f1.txtUsername.Text = username;
                    f1.txtPassword.Text = password;
                    f1.lblIDNumber.Text = id;
                    f1.txtLastName.Text = lnam;
                    f1.txtFirstName.Text = fnam;
                    f1.txtMiddleName.Text = mnam;
                    f1.dtBirthday.Text = birthday;
                    f1.ShowDialog();
                }
            }
            else
                MessageBox.Show("Empty field not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public string doctorname = "";

        private void frmNurseSendTo_Load(object sender, EventArgs e)
        {
            string time = "";
            time = DateTime.Now.ToString("dddd");
            lblDaysAc.Text = time;
            conn.Open();
            string sql = "SELECT * FROM tbldoctors_schedule WHERE schedule = '" + lblDaysAc.Text + "' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                doctorname = reader.GetString("name");
                cboDoctorsName.Items.Add(doctorname);
            }
            conn.Close();
            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        private void cboDoctorsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM tbldoctors_schedule WHERE name = '" + cboDoctorsName.Text + "' AND schedule = '" + lblDaysAc.Text + "'";
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
