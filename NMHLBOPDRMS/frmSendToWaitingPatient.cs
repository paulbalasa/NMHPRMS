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
    public partial class frmSendToWaitingPatient : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmSendToWaitingPatient()
        {
            InitializeComponent();
        }

        private void frmSendToWaitingPatient_Load(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM tblDoctors_Schedule1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string doctorname = "";
            string position = "";
            while (reader.Read())
            {
                doctorname = reader.GetString("name");
                position = reader.GetString("position");
                cboDoctorsName.Items.Add(doctorname + " (" + position + ")");
            }
            conn.Close();

            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        public string number = "";

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cboDoctorsName.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sql = "SELECT * FROM tblWaiting_Patient_Nurse WHERE patientNo = @num";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@num", number);
                MySqlDataReader dr = cmd.ExecuteReader();
                string fn = "";
                string mn = "";
                string ln = "";
                string stat = "";
                string dtExamined = "";
                string dtRequested = "";
                while (dr.Read())
                {
                    fn = dr.GetString("firstName");
                    mn = dr.GetString("middleName");
                    ln = dr.GetString("lastName");
                    stat = dr.GetString("status");
                    dtRequested = dr.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                    dtExamined = dr.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
                }
                conn.Close();

                conn.Open();
                MySqlCommand c = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor", conn);
                Int32 co = Convert.ToInt32(c.ExecuteScalar());
                conn.Close();

                conn.Open();
                string sq = "INSERT INTO tblnext_in_line_doctor VALUES (@num, @patientNo, @lname, @fname, @mname, @status, @daterequested, @dateexamined, @sentby, @sentto); DELETE FROM tblwaiting_patient_nurse WHERE patientNo = @patientNo";
                MySqlCommand cm = new MySqlCommand(sq, conn);
                cm.Parameters.AddWithValue("@num", co = co + 1);
                cm.Parameters.AddWithValue("@patientNo", number);
                cm.Parameters.AddWithValue("@lname", ln);
                cm.Parameters.AddWithValue("@fname", fn);
                cm.Parameters.AddWithValue("@mname", mn);
                cm.Parameters.AddWithValue("@status", stat);
                cm.Parameters.AddWithValue("@daterequested", dtRequested);
                cm.Parameters.AddWithValue("@dateexamined", dtExamined);
                cm.Parameters.AddWithValue("@sentby", "NURSE");
                cm.Parameters.AddWithValue("@sentto", cboDoctorsName.Text);
                cm.ExecuteNonQuery();
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
                string sqll = "SELECT * FROM tblPatients_Info WHERE patientsNo = @numm ";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@numm", number);
                string fname = "";
                string mname = "";
                string lname = "";
                MySqlDataReader readerr = cmdd.ExecuteReader();
                while (readerr.Read())
                {
                    fname = readerr.GetString("firstName");
                    mname = readerr.GetString("middleName");
                    lname = readerr.GetString("lastName");
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
                string activityy = "PATIENT (" + fname + " " + mname + " " + lname + ") SENT TO DR. " + cboDoctorsName.Text + " .";

                conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                MessageBox.Show("Patient succesfully sent to " + cboDoctorsName.Text + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    Application.OpenForms[i].Hide();
                }

                frmMainNurse f1 = new frmMainNurse();

                conn.Open();
                string sqlll = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                string id = "";
                string username = "";
                string password = "";
                MySqlDataReader readerrr = cmddd.ExecuteReader();
                while (readerrr.Read())
                {
                    id = readerrr.GetString("IDNumber");
                    username = readerrr.GetString("username");
                    password = readerrr.GetString("password");
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
            else
                MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
