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
    public partial class frmSchedulingEditing : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmSchedulingEditing()
        {
            InitializeComponent();
        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstDays.Items.Contains(cboDays.SelectedItem))
            {
                lstDays.Items.Add(cboDays.SelectedItem);

                lblMessage.Visible = true;
                cboDays.Enabled = false;
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
            }
            else
                cboDays.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
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

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            if (lstDays.Items.Count != 0 && lstTime.Items.Count != 0)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sqlStatement = "UPDATE tbldoctors_schedule1 SET schedule1 = @schedule1 WHERE IDNumber = @id";
                MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                cmd.Parameters.AddWithValue("@id", lblIDNum.Text);
                cmd.Parameters.AddWithValue("@schedule1", lblDaysAc.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                string sql = "DELETE FROM tbldoctors_schedule WHERE IDNumber = @id";
                MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                cmd1.Parameters.AddWithValue("@id", lblIDNum.Text);
                cmd1.ExecuteNonQuery();
                conn.Close();

                for (int i = 0; i < lstDays.Items.Count; i++)
                {
                    conn.Open();
                    string sql1 = "INSERT INTO tbldoctors_schedule VALUES (@idnum, @name, @schedule, @time)";
                    MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
                    cmd2.Parameters.AddWithValue("@idnum", lblIDNum.Text);
                    cmd2.Parameters.AddWithValue("@name", lblName.Text);
                    cmd2.Parameters.AddWithValue("@schedule", lstDays.Items[i].ToString());
                    cmd2.Parameters.AddWithValue("@time", lstTime.Items[i].ToString());
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }

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
                string activityy = "SUCCESSFULLY EDITED A SCHEDULE FOR " + lblName.Text + ".";

                conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                MessageBox.Show("Successfully updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (lstDays.Items.Count == 0 && lstTime.Items.Count == 0)
                    MessageBox.Show("Schedule must have atleast one Day/s and Time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (lstDays.Items.Count == 0)
                    MessageBox.Show("Schedule must have atleast one Day/s!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (lstTime.Items.Count == 0)
                    MessageBox.Show("Day/s must have time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lstDays.Items.Clear();
            lstTime.Items.Clear();
            lblDaysAc.Text = "";
        }

        private void frmSchedulingEditing_Load(object sender, EventArgs e)
        {

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

        private void tmPckrFrom_MouseLeave(object sender, EventArgs e)
        {
            tmPckrTo.Enabled = true;
        }

        private void tmPckrTo_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
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
    }
}
