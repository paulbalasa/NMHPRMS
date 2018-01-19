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
    public partial class frmAccount : Form
    {
        MySQL_Code connectionString = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmAccount()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    Application.OpenForms[i].Hide();
                }
                close();
            }
            else
                txtIDNumber.Focus();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        public void close()
        {
            frmMainAdmin f1 = new frmMainAdmin();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqll = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
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
            string sql1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@id", id);
            string lname = "";
            string fname = "";
            string mname = "";
            string birthday = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lname = reader1.GetString("lastname");
                fname = reader1.GetString("firstname");
                mname = reader1.GetString("middlename");
                birthday = reader1.GetString("birthday");

                if (reader1["profilePicture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])(reader1["profilePicture"]);
                    f1.pctrProfile.Image = null;

                    MemoryStream ms = new MemoryStream(img);
                    f1.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            conn.Close();

            this.Hide();
            
            f1.lblNameOfAdmin.Text = lname + ", " + fname + " " + mname;
            f1.txtIDNumber.Text = id;
            f1.txtUsername.Text = username;
            f1.txtPassword.Text = password;
            f1.lblIDNumber.Text = id;
            f1.txtLastName.Text = lname;
            f1.txtFirstName.Text = fname;
            f1.txtMiddleName.Text = mname;
            f1.dtBirthday.Text = birthday;
            f1.Show();
        }
        
        private void btnUpdatePersonalInfo_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sql = "UPDATE tblAccount_Profile SET lastName = @lname, firstName = @fname, middleName = @mname, birthday = @bday, email = @email WHERE IDNumber = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                cmd.Parameters.AddWithValue("@lname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@bday", dtBirthday.Value);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                //Activity Log
                conn.Open();
                string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
                string num = "";
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    num = reader.GetString("IDNumber");
                }
                conn.Close();

                conn.Open();
                string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
                cmd3.Parameters.AddWithValue("@id", num);
                string lname = "";
                string fname = "";
                string mname = "";
                MySqlDataReader reader1 = cmd3.ExecuteReader();
                while (reader1.Read())
                {
                    lname = reader1.GetString("lastname");
                    fname = reader1.GetString("firstname");
                    mname = reader1.GetString("middlename");
                }
                conn.Close();

                string acctype = "ADMINISTRATOR";
                string activityy = "UPDATED PERSONAL INFORMATION OF " + lblType.Text + ".";

                connectionString.activityLog(acctype, fname, mname, lname, activityy);

                MessageBox.Show("Personal Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
        }
    }
}
