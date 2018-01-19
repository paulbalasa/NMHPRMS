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
    public partial class frmConfirmationAdmin : Form
    {
        MySQL_Code connectionString = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmConfirmationAdmin()
        {
            InitializeComponent();
        }

        private void frmConfirmationAdmin_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sqll = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' AND password = @pass";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@pass", txtPassword.Text);
                int x = 0;
                MySqlDataReader readerr = cmdd.ExecuteReader();
                while (readerr.Read())
                {
                    x = x + 1;
                }
                conn.Close();
                if (x == 1)
                {
                    conn.Open();
                    string sql = "UPDATE tblAccounts SET username =  @username, password = @password WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", lblID.Text);
                    cmd.Parameters.AddWithValue("@username", lblUsername.Text);
                    cmd.Parameters.AddWithValue("@password", lblPassword.Text);
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
                    string activityy = "UPDATED ACCOUNT INFORMATION OF " + lblType.Text + ".";

                    connectionString.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Account Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Access Denied! Check password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("Empty filed not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
