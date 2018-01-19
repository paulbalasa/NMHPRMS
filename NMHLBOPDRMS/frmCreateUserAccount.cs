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
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace NMHLBOPDRMS
{
    public partial class frmCreateUserAccount : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public frmCreateUserAccount()
        {
            InitializeComponent();

            dtBday.MaxDate = DateTime.Now.ToLocalTime();
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public void saveAccount()
        {
            if (cboAccountType.Text != "" && txtIDNumber.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtRetypePassword.Text != "" && vldnvld.Text != "Invalid Email Address")
            {
                if (txtRetypePassword.Text == txtPassword.Text)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        string sqll = "SELECT * FROM tblAccount_Profile WHERE lastName = @lname AND firstName = @fname AND middleName = @mname";
                        MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                        cmdd.Parameters.AddWithValue("@lname", txtLastName.Text);
                        cmdd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                        cmdd.Parameters.AddWithValue("@mname", txtMiddleName.Text);
                        MySqlDataReader dr = cmdd.ExecuteReader();
                        string accType = "";
                        int x = 0;
                        while (dr.Read())
                        {
                            accType = dr.GetString("accountType");
                            x++;
                        }
                        conn.Close();

                        if (x == 0)
                        {
                            conn.Open();
                            MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts", conn);
                            Int32 count = Convert.ToInt32(cmd1.ExecuteScalar());
                            conn.Close();

                            conn.Open();
                            string sql = "INSERT INTO tblAccounts VALUES (@accountnum, @type, @username, @password, @id, @archive, @logInStatus, @lockStatus, 0); INSERT INTO tblAccount_Profile VALUES (@id, @type, @docPos, @lastname, @firstname, @middlename, @birthday, @email, @profilepicture, NULL)";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@accountnum", count = count + 1);
                            cmd.Parameters.AddWithValue("@type", cboAccountType.Text);
                            cmd.Parameters.AddWithValue("@docPos", "N/A");
                            cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                            cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                            cmd.Parameters.AddWithValue("@middlename", txtMiddleName.Text);
                            cmd.Parameters.AddWithValue("@birthday", dtBday.Value);
                            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@profilepicture", null);
                            cmd.Parameters.AddWithValue("@archive", 0);
                            cmd.Parameters.AddWithValue("@logInStatus", "INACTIVE");
                            cmd.Parameters.AddWithValue("@lockStatus", 0);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Activity Log
                            conn.Open();
                            string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
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
                            MySqlCommand cmd4 = new MySqlCommand(sql2, conn);
                            cmd4.Parameters.AddWithValue("@id", num);
                            string lname = "";
                            string fname = "";
                            string mname = "";
                            MySqlDataReader reader1 = cmd4.ExecuteReader();
                            while (reader1.Read())
                            {
                                lname = reader1.GetString("lastname");
                                fname = reader1.GetString("firstname");
                                mname = reader1.GetString("middlename");
                            }
                            conn.Close();

                            string acctype = "ADMINISTRATOR";
                            string activityy = "SUCCESSFULLY CREATED/SAVED USER ACCOUNT - " + cboAccountType.Text + " (" + txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text + ").";

                            conStr.activityLog(acctype, fname, mname, lname, activityy);

                            MessageBox.Show(cboAccountType.Text + "'s Account successfully created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Name '" + txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text + "' already have an account.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        cboPosition.Text = "";
                        cboAccountType.Text = "";
                        txtLastName.Clear();
                        txtFirstName.Clear();
                        txtMiddleName.Clear();
                        txtEmail.Clear();
                        dtBday.Text = DateTime.Now.ToShortDateString();
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtRetypePassword.Clear();
                        cboAccountType.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Username already exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.SelectAll();
                        txtUsername.Focus();
                    }
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
            {
                if (cboAccountType.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboAccountType.Focus();
                }
                else if (txtIDNumber.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDNumber.Focus();
                }
                else if (txtLastName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                }
                else if (txtFirstName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                }
                else if (txtMiddleName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMiddleName.Focus();
                }
                else if (txtUsername.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                }
                else if (txtRetypePassword.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRetypePassword.Focus();
                }
                else if (cboAccountType.Text == "")
                    MessageBox.Show("Select Account Type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (vldnvld.Text == "Invalid Email Address")
                {
                    MessageBox.Show("Invalid email address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }

        public void close()
        {
            frmMainAdmin f1 = new frmMainAdmin();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            string id = "";
            string username = "";
            string password = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetString("IDNumber");
                username = reader.GetString("username");
                password = reader.GetString("password");
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

        private void txtRetypePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (cboAccountType.Text != "DOCTOR")
                    {
                        saveAccount();
                        cboAccountType.SelectedIndex = -1;
                    }
                    else
                    {
                        submitDoc();
                        cboAccountType.SelectedIndex = -1;
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboAccountType.Text != "DOCTOR")
                {
                    saveAccount();
                    cboAccountType.SelectedIndex = -1;
                }
                else
                {
                    submitDoc();
                    cboAccountType.SelectedIndex = -1;
                }
            }
        }

        private void frmCreateUserAccount_Load(object sender, EventArgs e)
        {
            dtBday.Text = DateTime.Now.ToShortDateString();
            dtPicker.Text = DateTime.Now.ToShortDateString();

            conn.Open();
            string sql = "SELECT * FROM tblDoctors";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            string doctors = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                doctors = reader.GetString("doctorPosition");
                cboPosition.Items.Add(doctors);
            }
            conn.Close();

            cboAccountType.Focus();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cboAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccountType.Text == "NURSE")
            {
                string date = DateTime.Now.ToShortDateString();
                var y = DateTime.Parse(date).Year;
                Random number = new Random();
                var a = number.Next(0, 1000).ToString();
                txtIDNumber.Text = y + "-" + a + "-NMH-N";
                lblPos.Visible = false;
                cboPosition.Visible = false;
            }
            else if (cboAccountType.Text == "DOCTOR")
            {
                string date = DateTime.Now.ToShortDateString();
                var y = DateTime.Parse(date).Year;
                Random number = new Random();
                var a = number.Next(0, 1000).ToString();
                txtIDNumber.Text = y + "-" + a + "-NMH-D";
                lblPos.Visible = true;
                cboPosition.Visible = true;
            }
            else if (cboAccountType.Text == "LABORATORY 1")
            {
                string date = DateTime.Now.ToShortDateString();
                var y = DateTime.Parse(date).Year;
                Random number = new Random();
                var a = number.Next(0, 1000).ToString();
                txtIDNumber.Text = y + "-" + a + "-NMH-L1";
                lblPos.Visible = false;
                cboPosition.Visible = false;
            }
            else if (cboAccountType.Text == "LABORATORY 2")
            {
                string date = DateTime.Now.ToShortDateString();
                var y = DateTime.Parse(date).Year;
                Random number = new Random();
                var a = number.Next(0, 1000).ToString();
                txtIDNumber.Text = y + "-" + a + "-NMH-L2";
                lblPos.Visible = false;
                cboPosition.Visible = false;
            }
        }

        private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPosition.Text == "ADD(+)")
            {
                txtPosition.Visible = true;
                btnAddPosition.Visible = true;
                btnCancel.Visible = true;
                lblPos.Visible = false;
                cboPosition.Visible = false;
            }
        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            cboPosition.Text = "";
            try
            {
                if (txtPosition.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "INSERT INTO tblDoctors VALUES (@doctor)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@doctor", txtPosition.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cboPosition.Items.Add(txtPosition.Text);

                    txtPosition.Clear();
                    txtPosition.Visible = false;
                    btnAddPosition.Visible = false;
                    btnCancel.Visible = false;
                    lblPos.Visible = true;
                    cboPosition.Visible = true;
                    cboPosition.Focus();
                    cboPosition.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Empty field not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPosition.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doctor Position already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void submitDoc()
        {
            if (cboAccountType.Text != "" && cboPosition.Text != "" && cboPosition.Text != "ADD(+)" && txtIDNumber.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtRetypePassword.Text != "" && vldnvld.Text != "Invalid Email Address")
            {
                if (txtRetypePassword.Text == txtPassword.Text)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts", conn);
                        Int32 count = Convert.ToInt32(cmd1.ExecuteScalar());
                        conn.Close();

                        conn.Open();
                        string sql = "INSERT INTO tblAccounts VALUES (@accountnum, @type, @username, @password, @id, @archive, @logInStatus, @lockStatus, 0); INSERT INTO tblAccount_Profile VALUES (@id, @type, @docPos, @lastname, @firstname, @middlename, @birthday, @email, @profilepicture, NULL)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@accountnum", count = count + 1);
                        cmd.Parameters.AddWithValue("@type", cboAccountType.Text);
                        cmd.Parameters.AddWithValue("@docPos", cboPosition.Text);
                        cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@middlename", txtMiddleName.Text);
                        cmd.Parameters.AddWithValue("@birthday", dtBday.Value);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@profilepicture", null);
                        cmd.Parameters.AddWithValue("@archive", 0);
                        cmd.Parameters.AddWithValue("@logInStatus", "INACTIVE");
                        cmd.Parameters.AddWithValue("@lockStatus", 0);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
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
                        MySqlCommand cmd4 = new MySqlCommand(sql2, conn);
                        cmd4.Parameters.AddWithValue("@id", num);
                        string lname = "";
                        string fname = "";
                        string mname = "";
                        MySqlDataReader reader1 = cmd4.ExecuteReader();
                        while (reader1.Read())
                        {
                            lname = reader1.GetString("lastname");
                            fname = reader1.GetString("firstname");
                            mname = reader1.GetString("middlename");
                        }
                        conn.Close();

                        string acctype = "ADMINISTRATOR";
                        string activityy = "SUCCESSFULLY CREATED/SAVED USER ACCOUNT - " + cboAccountType.Text + " (" + txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text + ").";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        MessageBox.Show(cboAccountType.Text + "'s Account successfully created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cboPosition.Text = "";
                        cboAccountType.Text = "";
                        cboPosition.Visible = false;
                        lblPos.Visible = false;
                        txtLastName.Clear();
                        txtFirstName.Clear();
                        txtMiddleName.Clear();
                        dtBday.Text = DateTime.Now.ToShortDateString();
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtRetypePassword.Clear();
                        txtEmail.Clear();
                        cboAccountType.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Username already exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.SelectAll();
                        txtUsername.Focus();
                    }
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
            {
                if (cboAccountType.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboAccountType.Focus();
                }
                else if (txtIDNumber.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDNumber.Focus();
                }
                else if (txtLastName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                }
                else if (txtFirstName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                }
                else if (txtMiddleName.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMiddleName.Focus();
                }
                else if (txtUsername.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                }
                else if (txtRetypePassword.Text == "")
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRetypePassword.Focus();
                }
                else if (cboAccountType.Text == "")
                    MessageBox.Show("Select Account Type!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cboPosition.Text == "")
                    MessageBox.Show("Select Position for Doctor!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cboPosition.Text == "ADD(+)")
                    MessageBox.Show("Select Position for Doctor!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (vldnvld.Text == "Invalid Email Address")
                {
                    MessageBox.Show("Invalid email address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPosition.Visible = false;
            btnAddPosition.Visible = false;
            btnCancel.Visible = false;
            lblPos.Visible = true;
            cboPosition.Visible = true;
            cboPosition.SelectedIndex = -1;
            cboPosition.Focus();

        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
            txtPassword.Focus();
        }

        private void txtRetypePassword_Click(object sender, EventArgs e)
        {
            txtRetypePassword.SelectAll();
            txtRetypePassword.Focus();
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
            else if (txtEmail.Text == "")
            {
                vldnvld.Text = "";
            }
            else
            {
                vldnvld.Text = "(Invalid Email Address)";
                vldnvld.ForeColor = Color.Red;
            }
        }
    }
}
