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
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace NMHLBOPDRMS
{
    public partial class frmAccountSetUp : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        //public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmAccountSetUp()
        {
            InitializeComponent();

            dtPickerAdmin.MaxDate = DateTime.Now.ToLocalTime();
        }

        private void frmAccountSetUp_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();

            string date = DateTime.Now.ToShortDateString();
            var y = DateTime.Parse(date).Year;

            Random number = new Random();
            var x = number.Next(0, 1000).ToString();

            txtIDNumberA.Text = y + "-" + x + "-NMH-A";
        }

        private void btnSubmitAdmin_Click(object sender, EventArgs e)
        {
            admin();

            tbCntrlAccountSetup.SelectedIndex = 1;
            tbCntrlAccountSetup.Refresh();
        }

        public void admin()
        {
            if (txtIDNumberA.Text != "" && txtLastNameA.Text != "" && txtFirstNameA.Text != "" && txtMiddleNameA.Text != "" && txtUsernameA.Text != "" && txtPasswordA.Text != "" && txtRetypePasswordA.Text != "" && vldnvld.Text != "Invalid Email Address")
            {
                if (txtRetypePasswordA.Text == txtPasswordA.Text)
                {
                    string connectionString = "server=" + Properties.Settings.Default.server + ";username=" + Properties.Settings.Default.username + ";database=" + Properties.Settings.Default.database + ";port=" + Properties.Settings.Default.port + ";password=" + Properties.Settings.Default.password + ";";
                    MySqlConnection conn = new MySqlConnection(connectionString);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts", conn);
                    Int32 count = Convert.ToInt32(cmd1.ExecuteScalar());
                    conn.Close();

                    conn.Open();
                    string sql = "INSERT INTO tblAccounts VALUES (@accountnum, @type, @username, @password, @id, @archive, @logInStatus, @lockStatus, 0); INSERT INTO tblAccount_Profile VALUES (@id, @type, @docPos, @lastname, @firstname, @middlename, @birthday, @email, NULL, NULL)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@accountnum", count = count + 1);
                    cmd.Parameters.AddWithValue("@type", "ADMINISTRATOR");
                    cmd.Parameters.AddWithValue("@docPos", "N/A");
                    cmd.Parameters.AddWithValue("@id", txtIDNumberA.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsernameA.Text);
                    cmd.Parameters.AddWithValue("@password", txtPasswordA.Text);
                    cmd.Parameters.AddWithValue("@lastname", txtLastNameA.Text);
                    cmd.Parameters.AddWithValue("@firstname", txtFirstNameA.Text);
                    cmd.Parameters.AddWithValue("@middlename", txtMiddleNameA.Text);
                    cmd.Parameters.AddWithValue("@birthday", dtPickerAdmin.Value);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@archive", 0);
                    cmd.Parameters.AddWithValue("@logInStatus", "INACTIVE");
                    cmd.Parameters.AddWithValue("@lockStatus", 0);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string acctype = "SYSTEM";
                    string activityy = "SYSTEM SUCCESSFULLY CREATED ADMINISTRATOR ACCOUNT.";
                    string fname = "SYSTEM";
                    string mname = "";
                    string lname = "";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Account successfully created!" + Environment.NewLine + "You can now Log-in.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmLogin f1 = new frmLogin();
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Password didn't match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPasswordA.SelectionStart = 0;
                    txtPasswordA.SelectionLength = txtPasswordA.TextLength;
                    txtPasswordA.Focus();
                }
            }
            else
            {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtIDNumberA.Text == "")
                    txtIDNumberA.Focus();
                else if (txtLastNameA.Text == "")
                    txtLastNameA.Focus();
                else if (txtFirstNameA.Text == "")
                    txtFirstNameA.Focus();
                else if (txtMiddleNameA.Text == "")
                    txtMiddleNameA.Focus();
                else if (txtUsernameA.Text == "")
                    txtUsernameA.Focus();
                else if (txtPasswordA.Text == "")
                    txtPasswordA.Focus();
                else if (txtRetypePasswordA.Text == "")
                    txtRetypePasswordA.Focus();
                else if (vldnvld.Text == "Invalid Email Address")
                {
                    MessageBox.Show("Invalid email address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }

        private void txtRetypePasswordA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                admin();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtLastNameA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFirstNameA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMiddleNameA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (isValidEmail(txtEmail.Text))
            {
                vldnvld.Text = "(Valid Email Address)";
                vldnvld.ForeColor = Color.Blue;
            }
            else
            {
                vldnvld.Text = "(Invalid Email Address)";
                vldnvld.ForeColor = Color.Red;
            }
        }
    }
}
