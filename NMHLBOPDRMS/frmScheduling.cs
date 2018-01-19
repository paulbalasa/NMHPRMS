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
    public partial class frmScheduling : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmScheduling()
        {
            InitializeComponent();
        }

        private void frnScheduling_Load(object sender, EventArgs e)
        {
            conn.Open();
            string sqlStatement = "SELECT * FROM tblaccount_profile WHERE accountType = 'DOCTOR'";
            MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            while (reader.Read())
            {
                id = reader.GetString("IDNumber");
                cboIDNum.Items.Add(id);
            }
            conn.Close();
        }

        private void cboIDNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIDNum.Text != "")
            {
                lstDays.Items.Clear();
                lstTime.Items.Clear();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstDays.Items.Clear();
                conn.Open();
                string sqlStatement = "SELECT * FROM tblaccount_profile WHERE IDNumber = '" + cboIDNum.SelectedItem + "'";
                MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string fname = "";
                string mname = "";
                string lname = "";
                string position = "";
                while (reader.Read())
                {
                    fname = reader.GetString("firstName");
                    mname = reader.GetString("middleName");
                    lname = reader.GetString("lastName");
                    position = reader.GetString("doctorPosition");
                }
                conn.Close();
                lblName.Text = "Dr. " + fname + " " + mname + " " + lname;
                lblPosition.Text = position;

                conn.Open();
                string sqlStatement1 = "SELECT * FROM tbldoctors_schedule WHERE IDNumber = '" + cboIDNum.SelectedItem + "'";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                string days = "";
                while (reader1.Read())
                {
                    days = reader1.GetString("schedule");
                    lstDays.Items.Add(days);
                }
                conn.Close();

                cboIDNum.Text = "";
                cboDays.Text = "";
                lblDaysAc.Text = "";
                lstDays.Items.Clear();
                cboDays.Enabled = true;
            }
            else
                cboDays.Enabled = false;
        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstDays.Items.Contains(cboDays.SelectedItem))
            {
                lstDays.Items.Add(cboDays.SelectedItem);
                tmPckrFrom.Enabled = true;

                lblDaysAc.Text = "";
                foreach (object liItem in lstDays.Items)
                {
                    if (liItem.ToString() == "Monday")
                        lblDaysAc.Text += "M ";
                    if (liItem.ToString() == "Tuesday")
                        lblDaysAc.Text += "T ";
                    if (liItem.ToString() == "Wednesday")
                        lblDaysAc.Text += "W ";
                    if (liItem.ToString() == "Thursday")
                        lblDaysAc.Text += "Th ";
                    if (liItem.ToString() == "Friday")
                        lblDaysAc.Text += "F ";
                    if (liItem.ToString() == "Saturday")
                        lblDaysAc.Text += "Sat ";
                    if (liItem.ToString() == "Sunday")
                        lblDaysAc.Text += "Sun ";
                }
                cboDays.Enabled = false;
                lblMessage.Visible = true;
            }
            else
                cboDays.Enabled = true;
        }

        private void btnSaveDays_Click(object sender, EventArgs e)
        {
            if (cboIDNum.Text != "" && lblName.Text != "" && lblPosition.Text != "" && lstDays.Items.Count != 0 && lstTime.Items.Count != 0)
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    for (int i = 0; i < lstDays.Items.Count; i++)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tbldoctors_schedule VALUES (@idnum, @name, @schedule, @time)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@idnum", cboIDNum.Text);
                        cmd.Parameters.AddWithValue("@name", lblName.Text + " (" + lblPosition.Text + ")");
                        cmd.Parameters.AddWithValue("@schedule", lstDays.Items[i].ToString());
                        cmd.Parameters.AddWithValue("@time", lstTime.Items[i].ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    conn.Open();
                    string sqlStatement1 = "INSERT INTO tbldoctors_schedule1 VALUES (@idnum, @name, @position, @schedule1)";
                    MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                    cmd1.Parameters.AddWithValue("@idnum", cboIDNum.Text);
                    cmd1.Parameters.AddWithValue("@name", lblName.Text);
                    cmd1.Parameters.AddWithValue("@position", lblPosition.Text);
                    cmd1.Parameters.AddWithValue("@schedule1", lblDaysAc.Text);
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql4 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    string num = "";
                    MySqlDataReader reader4 = cmd4.ExecuteReader();
                    while (reader4.Read())
                    {
                        num = reader4.GetString("IDNumber");
                    }
                    conn.Close();

                    conn.Open();
                    string sql5 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.Parameters.AddWithValue("@id", num);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader5 = cmd5.ExecuteReader();
                    while (reader5.Read())
                    {
                        lnamee = reader5.GetString("lastname");
                        fnamee = reader5.GetString("firstname");
                        mnamee = reader5.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "ADMINISTRATOR";
                    string activityy = "SUCCESSFULLY CREATED A SCHEDULE FOR " + lblName.Text + ".";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Schedule successfully saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboIDNum.Text = "";
                    lblName.Text = "";
                    lblPosition.Text = "";
                    cboDays.Text = "";
                    lblDaysAc.Text = "";
                    lstDays.Items.Clear();
                    lstTime.Items.Clear();
                    cboIDNum.Enabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Schedule for " + lblName.Text + " already created!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (cboIDNum.Text == "" && lblName.Text == "" && lblPosition.Text == "")
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (lstDays.Items.Count == 0 && lstTime.Items.Count == 0)
                    MessageBox.Show("Schedule must have atleast one Day/s and Time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (lstDays.Items.Count == 0)
                    MessageBox.Show("Schedule must have atleast one Day/s!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (lstTime.Items.Count == 0)
                    MessageBox.Show("Day/s must have time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstDays.SelectedItem != null)
            {
                cboDays.Enabled = true;
                lstTime.Items.Remove(lstTime.SelectedItem);
                lstDays.Items.Remove(lstDays.SelectedItem);

                lblDaysAc.Text = "";
                foreach (object liItem in lstDays.Items)
                {
                    if (liItem.ToString() == "Monday")
                        lblDaysAc.Text += "M ";
                    if (liItem.ToString() == "Tuesday")
                        lblDaysAc.Text += "T ";
                    if (liItem.ToString() == "Wednesday")
                        lblDaysAc.Text += "W ";
                    if (liItem.ToString() == "Thursday")
                        lblDaysAc.Text += "Th ";
                    if (liItem.ToString() == "Friday")
                        lblDaysAc.Text += "F ";
                    if (liItem.ToString() == "Saturday")
                        lblDaysAc.Text += "Sat ";
                    if (liItem.ToString() == "Sunday")
                        lblDaysAc.Text += "Sun ";
                }
            }
            else
                MessageBox.Show("Select Days first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    Application.OpenForms[i].Hide();
                }

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
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lstDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDays.SelectedItems.Count == 1)
            {
                lstTime.SelectedIndex = lstDays.SelectedIndex;
                btnRemove.Enabled = true;
            }
            else
                btnRemove.Enabled = false;
        }

        private void lstTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTime.SelectedItems.Count == 1)
            {
                lstDays.SelectedIndex = lstTime.SelectedIndex;
                btnRemove.Enabled = true;
            }
            else
                btnRemove.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string from = tmPckrFrom.Value.ToShortTimeString().ToString();
            string to = tmPckrTo.Value.ToShortTimeString().ToString();

            lstTime.Items.Add(from + " - " + to);
            tmPckrFrom.Enabled = false;
            tmPckrTo.Enabled = false;
            btnAdd.Enabled = false;
            cboDays.Enabled = true;
            lblMessage.Visible = false;
        }

        private void tmPckrFrom_MouseLeave(object sender, EventArgs e)
        {
            tmPckrTo.Enabled = true;
        }

        private void tmPckrTo_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }
    }
}
