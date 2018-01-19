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
    public partial class frmNewPatient : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmNewPatient()
        {
            InitializeComponent();

            dtPickerBirthday.MaxDate = DateTime.Now.ToLocalTime();
        }

        private void frmNewPatient_Load(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToShortDateString();
            var y = DateTime.Parse(date).Year;

            conn.Open();
            string sql = "SELECT DISTINCT(month) FROM tblstatpatient";
            MySqlCommand cm = new MySqlCommand(sql, conn);
            string month = "";
            MySqlDataReader rd = cm.ExecuteReader();
            while (rd.Read())
            {
                month = rd.GetString("month");
                lstMonths.Items.Add(month);
            }
            conn.Close();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblPatients_Info", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            count++;
            conn.Close();
            lblNumber.Text = y + "-" + count.ToString() + "-NMH";

            conn.Open();
            string sql1 = "SELECT * FROM tblSymptoms";
            MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
            string symptoms = "";
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                symptoms = reader.GetString("symptoms");
                cboSymptoms.Items.Add(symptoms);
            }
            conn.Close();

            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        public void close()
        {
            frmMainNurse f1 = new frmMainNurse();

            if (conn.State == ConnectionState.Open)
                conn.Close();
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
            string lname = "";
            string fname = "";
            string mname = "";
            string birthday = "";
            MySqlDataReader readerr1 = cmdd1.ExecuteReader();
            while (readerr1.Read())
            {
                lname = readerr1.GetString("lastname");
                fname = readerr1.GetString("firstname");
                mname = readerr1.GetString("middlename");
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

            f1.lblNameOfNurse.Text = lname + ", " + fname + " " + mname;
            f1.txtIDNumber.Text = id;
            f1.txtUsername.Text = username;
            f1.txtPassword.Text = password;
            f1.lblIDNumber.Text = id;
            f1.txtLastName.Text = lname;
            f1.txtFirstName.Text = fname;
            f1.txtMiddleName.Text = mname;
            f1.dtBirthday.Text = birthday;
            f1.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan tmspn = DateTime.Now - dtPickerBirthday.Value;
            int years = DateTime.Now.Year - dtPickerBirthday.Value.Year;
            if (dtPickerBirthday.Value.AddYears(years) > DateTime.Now)
                years--;
            txtAge.Text = years.ToString();
        }

        public void submitPersonalInfo()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "INSERT INTO tblPatients_Info VALUES (@patientnum, @lname, @fname, @mname, @sex, @status, @birthday, @age, @address, @date)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
            cmd.Parameters.AddWithValue("@lname", txtLname.Text);
            cmd.Parameters.AddWithValue("@fname", txtFname.Text);
            cmd.Parameters.AddWithValue("@mname", txtMname.Text);
            cmd.Parameters.AddWithValue("@sex", cboSex.Text);
            cmd.Parameters.AddWithValue("@status", cboStatus.Text);
            cmd.Parameters.AddWithValue("@birthday", dtPickerBirthday.Value);
            cmd.Parameters.AddWithValue("@age", txtAge.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@stat", "New");
            cmd.Parameters.AddWithValue("@date", dtPicker.Value);
            cmd.ExecuteNonQuery();
            conn.Close();

            //Activity Log
            conn.Open();
            string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
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

            string acctype = "NURSE";
            string activityy = "SUCCESSFULLY SAVED PERSONAL INFORMATION OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

            conStr.activityLog(acctype, fname, mname, lname, activityy);

            MessageBox.Show("Patient Information succesfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSubmitPersonalInfo.Enabled = false;
            btnUpdatePersonalInfo.Enabled = true;
            btnSaveSymptoms.Enabled = true;
        }

        private void btnSubmitPersonalInfo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (txtLname.Text != "" && txtFname.Text != "" && txtMname.Text != "" && cboSex.Text != "" && cboStatus.Text != "" && txtAge.Text != "" && txtAddress.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    submitPersonalInfo();

                    //Statistical
                    statistical();

                    metroTabControl1.SelectedIndex = 1;
                    metroTabControl1.Refresh();
                }
                else
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (txtLname.Text == "")
                        txtLname.Focus();
                    else if (txtFname.Text == "")
                        txtFname.Focus();
                    else if (txtMname.Text == "")
                        txtMname.Focus();
                    else if (cboSex.Text == "")
                        cboSex.Focus();
                    else if (cboStatus.Text == "")
                        cboStatus.Focus();
                    else if (txtAge.Text == "")
                        txtAge.Focus();
                    else if (txtAddress.Text == "")
                        txtAddress.Focus();
                }
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submitPersonalInfo();

                metroTabControl1.SelectedIndex = 1;
                metroTabControl1.Refresh();
            }
        }

        private void btnSubmitMedicalInfo_Click(object sender, EventArgs e)
        {
            frmNurseSendTo f1 = new frmNurseSendTo();
            f1.number = lblNumber.Text;
            f1.lname = txtLname.Text;
            f1.fname = txtFname.Text;
            f1.mname = txtMname.Text;
            f1.status = txtPIRStatus.Text;
            f1.dtRequested.Value = dtPicker.Value;
            f1.Show();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnUpdatePersonalInfo_Click(object sender, EventArgs e)
        {
            if (txtLname.Text != "" && txtFname.Text != "" && txtMname.Text != "" && cboSex.Text != "" && cboStatus.Text != "" && txtAge.Text != "" && txtAddress.Text != "")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sql = "UPDATE tblPatients_Info SET lastName = @lname, firstName = @fname, middleName = @mname, sex = @sex, civilStatus = @status, birthday = @birthday, age = @age, address = @address WHERE patientsNo = @patientnum";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
                cmd.Parameters.AddWithValue("@lname", txtLname.Text);
                cmd.Parameters.AddWithValue("@fname", txtFname.Text);
                cmd.Parameters.AddWithValue("@mname", txtMname.Text);
                cmd.Parameters.AddWithValue("@sex", cboSex.Text);
                cmd.Parameters.AddWithValue("@status", cboStatus.Text);
                cmd.Parameters.AddWithValue("@birthday", dtPickerBirthday.Value);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                //Activity Log
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblActivity_Log", conn);
                Int32 count = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

                conn.Open();
                string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
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

                conn.Open();
                string sql3 = "INSERT INTO tblActivity_Log VALUES (@logNum, @accType, @name, @activity, @date, @time)";
                MySqlCommand cmd4 = new MySqlCommand(sql3, conn);
                cmd4.Parameters.AddWithValue("@logNum", count++);
                cmd4.Parameters.AddWithValue("@accType", "NURSE");
                cmd4.Parameters.AddWithValue("@name", fname + " " + mname + " " + lname);
                cmd4.Parameters.AddWithValue("@activity", "UPDATED PERSONAL INFORMATION OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").");
                cmd4.Parameters.AddWithValue("@date", dtPicker.Value);
                cmd4.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
                cmd4.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Patient successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtLname.Text == "")
                    txtLname.Focus();
                else if (txtFname.Text == "")
                    txtFname.Focus();
                else if (txtMname.Text == "")
                    txtMname.Focus();
                else if (cboSex.Text == "")
                    cboSex.Focus();
                else if (cboStatus.Text == "")
                    cboStatus.Focus();
                else if (txtAge.Text == "")
                    txtAge.Focus();
                else if (txtAddress.Text == "")
                    txtAddress.Focus();
            }
        }

        private void txtTemperature_TextChanged(object sender, EventArgs e)
        {
            txtTemperature.MaxLength = 2;
            if (txtTemperature.Text == "")
            {
                lstSymptoms.Items.Remove("FEVER");
                txtStatusTemperature.Text = "STATUS";
            }
        }

        private void txtTemperature_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSystolic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDiastolic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtNumberOfDaySymptoms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDiastolic_TextChanged(object sender, EventArgs e)
        {
            txtDiastolic.MaxLength = 3;

            if (txtDiastolic.Text == "")
                txtStatusBloodPressure.Text = "STATUS";
        }

        private void txtLname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                close();
            }
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            if (txtLname.TextLength <= 0) return;
            string s = txtLname.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = txtLname.SelectionStart;
                int curSelLength = txtLname.SelectionLength;
                txtLname.SelectionStart = 0;
                txtLname.SelectionLength = 1;
                txtLname.SelectedText = s.ToUpper();
                txtLname.SelectionStart = curSelStart;
                txtLname.SelectionLength = curSelLength;
            }
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {
            if (txtFname.TextLength <= 0) return;
            string s = txtFname.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = txtFname.SelectionStart;
                int curSelLength = txtFname.SelectionLength;
                txtFname.SelectionStart = 0;
                txtFname.SelectionLength = 1;
                txtFname.SelectedText = s.ToUpper();
                txtFname.SelectionStart = curSelStart;
                txtFname.SelectionLength = curSelLength;
            }
        }

        private void txtMname_TextChanged(object sender, EventArgs e)
        {
            if (txtMname.TextLength <= 0) return;
            string s = txtMname.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = txtMname.SelectionStart;
                int curSelLength = txtMname.SelectionLength;
                txtMname.SelectionStart = 0;
                txtMname.SelectionLength = 1;
                txtMname.SelectedText = s.ToUpper();
                txtMname.SelectionStart = curSelStart;
                txtMname.SelectionLength = curSelLength;
            }
        }

        private void txtNumberOfDaySymptoms_TextChanged(object sender, EventArgs e)
        {
            txtNumberOfDaySymptoms.MaxLength = 3;

            int temp;
            if (txtNumberOfDaySymptoms.Text != "")
            {
                if (int.TryParse(txtNumberOfDaySymptoms.Text, out temp))
                {
                    if (temp <= 7)
                        cboMorbidity.Text = "1";
                    else if (temp > 7 && temp <= 14)
                        cboMorbidity.Text = "2";
                    else if (temp > 14 && temp <= 21)
                        cboMorbidity.Text = "3";
                    else if (temp > 21 && temp <= 28)
                        cboMorbidity.Text = "4";
                    else if (temp > 28 && temp <= 35)
                        cboMorbidity.Text = "5";
                    else if (temp > 35 && temp <= 42)
                        cboMorbidity.Text = "6";
                    else if (temp > 42 && temp <= 49)
                        cboMorbidity.Text = "7";
                    else if (temp > 49 && temp <= 56)
                        cboMorbidity.Text = "8";
                    else if (temp > 56 && temp <= 63)
                        cboMorbidity.Text = "9";
                    else if (temp > 63 && temp <= 70)
                        cboMorbidity.Text = "10";
                    else if (temp > 70 && temp <= 77)
                        cboMorbidity.Text = "11";
                    else if (temp > 77 && temp <= 84)
                        cboMorbidity.Text = "12";
                    else if (temp > 84 && temp <= 91)
                        cboMorbidity.Text = "13";
                    else if (temp > 91 && temp <= 98)
                        cboMorbidity.Text = "14";
                    else if (temp > 105 && temp <= 112)
                        cboMorbidity.Text = "15";
                    else if (temp > 112 && temp <= 119)
                        cboMorbidity.Text = "16";
                    else if (temp > 119 && temp <= 126)
                        cboMorbidity.Text = "17";
                    else if (temp > 126 && temp <= 133)
                        cboMorbidity.Text = "18";
                    else if (temp > 133 && temp <= 140)
                        cboMorbidity.Text = "19";
                    else if (temp > 140 && temp <= 147)
                        cboMorbidity.Text = "20";
                    else if (temp > 147 && temp <= 154)
                        cboMorbidity.Text = "21";
                    else if (temp > 154 && temp <= 161)
                        cboMorbidity.Text = "22";
                    else if (temp > 161 && temp <= 168)
                        cboMorbidity.Text = "23";
                    else if (temp > 168 && temp <= 175)
                        cboMorbidity.Text = "24";
                    else if (temp > 175 && temp <= 182)
                        cboMorbidity.Text = "25";
                    else if (temp > 182 && temp <= 189)
                        cboMorbidity.Text = "26";
                    else if (temp > 189 && temp <= 196)
                        cboMorbidity.Text = "27";
                    else if (temp > 196 && temp <= 203)
                        cboMorbidity.Text = "28";
                    else if (temp > 203 && temp <= 210)
                        cboMorbidity.Text = "29";
                    else if (temp > 210 && temp <= 217)
                        cboMorbidity.Text = "30";
                    else if (temp > 217 && temp <= 224)
                        cboMorbidity.Text = "31";
                    else if (temp > 224 && temp <= 231)
                        cboMorbidity.Text = "32";
                    else if (temp > 231 && temp <= 238)
                        cboMorbidity.Text = "33";
                    else if (temp > 238 && temp <= 245)
                        cboMorbidity.Text = "34";
                    else if (temp > 245 && temp <= 252)
                        cboMorbidity.Text = "35";
                    else if (temp > 252 && temp <= 259)
                        cboMorbidity.Text = "36";
                    else if (temp > 259 && temp <= 266)
                        cboMorbidity.Text = "37";
                    else if (temp > 266 && temp <= 273)
                        cboMorbidity.Text = "38";
                    else if (temp > 273 && temp <= 280)
                        cboMorbidity.Text = "39";
                    else if (temp > 280 && temp <= 287)
                        cboMorbidity.Text = "40";
                    else if (temp > 287 && temp <= 294)
                        cboMorbidity.Text = "41";
                    else if (temp > 294 && temp <= 301)
                        cboMorbidity.Text = "42";
                    else if (temp > 301 && temp <= 308)
                        cboMorbidity.Text = "43";
                    else if (temp > 308 && temp <= 315)
                        cboMorbidity.Text = "44";
                    else if (temp > 315 && temp <= 322)
                        cboMorbidity.Text = "45";
                    else if (temp > 322 && temp <= 329)
                        cboMorbidity.Text = "46";
                }
            }
            else
                cboMorbidity.Text = "";
        }

        private void btnSaveSymptoms_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                if (txtTemperature.Text != "" && txtSystolic.Text != "" && txtDiastolic.Text != "" && txtWeight.Text != "" && txtHeight.Text != "" && txtRRleft.Text != "" && txtRRright.Text != "" && txtCRleft.Text != "" && txtCRright.Text != "")
                {
                    string temp = txtTemperature.Text + " " + txtCelsius.Text;
                    string bp = txtSystolic.Text + " / " + txtDiastolic.Text + " " + txtmmHg.Text;
                    string cr = txtCRleft.Text + txtCRto.Text + txtCRright.Text + txtCRper.Text + txtCRmin.Text;
                    string rr = txtRRleft.Text + txtRRto.Text + txtRRright.Text + txtRRper.Text + txtRRmin.Text;
                    string weight = txtWeight.Text + txtKg.Text;
                    string height = txtHeight.Text + txtCM.Text;
                    string bmi = txtBMI.Text + lblBMIsymbol.Text;

                    if (lstSymptoms.Items.Count != 0)
                    {
                        for (int i = 0; i < lstSymptoms.Items.Count; i++)
                        {
                            conn.Open();
                            string sql = "INSERT INTO tblMedical_Information VALUES (@patientnum, @temp, @bp, @cr, @rr, @weight, @height, @bmi, @bmiCat, @complaints, @symptoms, @numberofdays, @morbidity, @date)";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
                            cmd.Parameters.AddWithValue("@temp", temp);
                            cmd.Parameters.AddWithValue("@bp", bp);
                            cmd.Parameters.AddWithValue("@cr", cr);
                            cmd.Parameters.AddWithValue("@rr", rr);
                            cmd.Parameters.AddWithValue("@weight", weight);
                            cmd.Parameters.AddWithValue("@height", height);
                            cmd.Parameters.AddWithValue("@bmi", bmi);
                            cmd.Parameters.AddWithValue("@bmiCat", txtBMICategory.Text);
                            cmd.Parameters.AddWithValue("@complaints", txtComplaints.Text);
                            cmd.Parameters.AddWithValue("@symptoms", lstSymptoms.Items[i].ToString());
                            cmd.Parameters.AddWithValue("@numberofdays", txtNumberOfDaySymptoms.Text);
                            cmd.Parameters.AddWithValue("@morbidity", cboMorbidity.Text);
                            cmd.Parameters.AddWithValue("@date", dtPicker.Value);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empty field for Symptoms not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (lstSymptoms.Items.Count == 0)
                            cboSymptoms.Focus();
                    }
                }
                else if (txtHeight.Text == "" && txtRRleft.Text == "" && txtRRright.Text == "" && txtCRright.Text == "" && txtCRleft.Text == "")
                {
                    string temp = txtTemperature.Text + " " + txtCelsius.Text;
                    string bp = txtSystolic.Text + " / " + txtDiastolic.Text + " " + txtmmHg.Text;
                    string cr = "";
                    string rr = "";
                    string weight = txtWeight.Text + txtKg.Text;
                    string height = "";
                    string bmi = "";

                    if (lstSymptoms.Items.Count != 0)
                    {
                        for (int i = 0; i < lstSymptoms.Items.Count; i++)
                        {
                            conn.Open();
                            string sql = "INSERT INTO tblMedical_Information VALUES (@patientnum, @temp, @bp, @cr, @rr, @weight, @height, @bmi, @bmiCat, @complaints, @symptoms, @numberofdays, @morbidity, @date)";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
                            cmd.Parameters.AddWithValue("@temp", temp);
                            cmd.Parameters.AddWithValue("@bp", bp);
                            cmd.Parameters.AddWithValue("@cr", cr);
                            cmd.Parameters.AddWithValue("@rr", rr);
                            cmd.Parameters.AddWithValue("@weight", weight);
                            cmd.Parameters.AddWithValue("@height", height);
                            cmd.Parameters.AddWithValue("@bmi", bmi);
                            cmd.Parameters.AddWithValue("@bmiCat", txtBMICategory.Text);
                            cmd.Parameters.AddWithValue("@complaints", txtComplaints.Text);
                            cmd.Parameters.AddWithValue("@symptoms", lstSymptoms.Items[i].ToString());
                            cmd.Parameters.AddWithValue("@numberofdays", txtNumberOfDaySymptoms.Text);
                            cmd.Parameters.AddWithValue("@morbidity", cboMorbidity.Text);
                            cmd.Parameters.AddWithValue("@date", dtPicker.Value);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empty field for Symptoms not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (lstSymptoms.Items.Count == 0)
                            cboSymptoms.Focus();
                    }
                }

                //Activity Log
                conn.Open();
                string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
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

                string acctype = "NURSE";
                string activityy = "SUCCESSFULLY SAVED MEDICAL INFORMATION OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

                conStr.activityLog(acctype, fname, mname, lname, activityy);

                MessageBox.Show("Medical Information successfuly saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSubmitMedicalInfo.Enabled = true;
                btnSaveSymptoms.Enabled = false;
            }
        }

        private void btnAddOtherSymptoms_Click(object sender, EventArgs e)
        {
            if (txtOtherSymptom.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to add '" + txtOtherSymptom.Text + "' to Symptoms?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblSymptoms", conn);
                        Int32 countt = Convert.ToInt32(cmd1.ExecuteScalar());
                        conn.Close();

                        conn.Open();
                        string sql = "INSERT INTO tblSymptoms VALUES (@id, @symptoms)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", countt = countt + 1);
                        cmd.Parameters.AddWithValue("@symptoms", txtOtherSymptom.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
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

                        string acctype = "NURSE";
                        string activityy = "SUCCESSFULLY ADDED '" + txtOtherSymptom.Text + "' TO SYMPTOMS.";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        cboSymptoms.Items.Add(txtOtherSymptom.Text);
                        cboSymptoms.Text = "";
                        txtOtherSymptom.Clear();
                        cboSymptoms.Enabled = true;
                        lbl3.Visible = false;
                        txtOtherSymptom.Visible = false;
                        btnUndo.Visible = false;
                        btnAddOtherSymptoms.Visible = false;
                        cboSymptoms.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("'" + txtOtherSymptom.Text + "' already added in Symptoms.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    txtOtherSymptom.SelectAll();
                    txtOtherSymptom.Focus();
                }
            }
            else
            {
                MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOtherSymptom.Focus();
            }
        }

        private void cboSymptoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSymptoms.Text != "")
            {
                if (cboSymptoms.Text != "ADD (+)")
                {
                    if (!lstSymptoms.Items.Contains(cboSymptoms.Text))
                        lstSymptoms.Items.Add(cboSymptoms.SelectedItem);
                }
                else
                {
                    if (cboSymptoms.Text == "ARTHRITIS - OTHER")
                        txtOtherSymptom.Text = "ARTHRITIS - ";
                    btnAddOtherSymptoms.Visible = true;
                    cboSymptoms.Enabled = false;
                    txtOtherSymptom.Visible = true;
                    txtOtherSymptom.Focus();
                    lbl3.Visible = true;
                    btnUndo.Visible = true;
                    cboSymptoms.Focus();
                }
            }
        }

        private void txtOtherSymptom_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherSymptom.Text != "")
            {
                btnAddOtherSymptoms.Enabled = true;
            }
            else
            {
                btnAddOtherSymptoms.Enabled = false;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            lbl3.Visible = false;
            txtOtherSymptom.Visible = false;
            cboSymptoms.Enabled = true;
            cboSymptoms.Text = "";
            txtOtherSymptom.Clear();
            btnUndo.Visible = false;
            cboSymptoms.Focus();
            btnAddOtherSymptoms.Visible = false;
        }

        private void lstSymptoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSymptoms.SelectedItem != null)
                btnRemove.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstSymptoms.Items.Remove(lstSymptoms.SelectedItem);
            btnRemove.Enabled = false;
        }

        private void txtCRright_TextChanged(object sender, EventArgs e)
        {
            txtCRright.MaxLength = 3;

            if (txtCRleft.Text != "" && txtCRright.Text != "")
                txtCRStatus.Text = "STATUS";
        }

        private void txtRRright_TextChanged(object sender, EventArgs e)
        {
            txtRRright.MaxLength = 3;

            if (txtRRleft.Text != "" && txtRRright.Text != "")
                txtRRStatus.Text = "STATUS";
        }

        private void txtRRleft_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtRRright_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCRleft_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCRright_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSystolic_TextChanged(object sender, EventArgs e)
        {
            txtSystolic.MaxLength = 3;
        }

        private void txtRRleft_TextChanged(object sender, EventArgs e)
        {
            txtRRleft.MaxLength = 3;
        }

        private void txtCRleft_TextChanged(object sender, EventArgs e)
        {
            txtCRleft.MaxLength = 3;
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            txtWeight.MaxLength = 3;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            txtHeight.MaxLength = 3;
        }

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            double weight, height;
            double bmi = 0.00;
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                if (double.TryParse(txtWeight.Text, out weight) && double.TryParse(txtHeight.Text, out height))
                {
                    double heightM = height / 100;
                    bmi = (weight / (heightM * heightM));
                    txtBMI.Text = bmi.ToString(".00");
                    double BMI = Convert.ToDouble(txtBMI.Text);

                    if (BMI < 18.5)
                    {
                        txtBMICategory.Text = "UNDERWEIGHT";
                    }
                    else if (BMI >= 18.5 && BMI <= 24.9)
                    {
                        txtBMICategory.Text = "NORMAL WEIGHT";
                    }
                    else if (BMI >= 25 && BMI <= 29.9)
                    {
                        txtBMICategory.Text = "OVERWEIGHT";
                    }
                    else if (BMI >= 30 && BMI <= 34.9)
                    {
                        txtBMICategory.Text = "CLASS 1 OBESE";
                    }
                    else if (BMI >= 35 && BMI <= 39.9)
                    {
                        txtBMICategory.Text = "CLASS 2 OBESE";
                    }
                    else
                    {
                        txtBMICategory.Text = "CLASS 3 OBESE";
                    }
                }
            }
            else
                txtBMICategory.Text = "STATUS";
        }

        private void txtTemperature_Leave(object sender, EventArgs e)
        {
            int temp;

            if (int.TryParse(txtTemperature.Text, out temp))
            {
                if (temp <= 35)
                {
                    lstSymptoms.Items.Remove("FEVER");
                    txtStatusTemperature.Text = "NOT NORMAL";
                }
                else if (temp >= 38)
                {
                    lstSymptoms.Items.Remove("FEVER");
                    lstSymptoms.Items.Add("FEVER");
                    txtStatusTemperature.Text = "NOT NORMAL";
                }
                else if (temp >= 36)
                {
                    lstSymptoms.Items.Remove("FEVER");
                    txtStatusTemperature.Text = "NORMAL";
                }
            }
        }

        private void txtDiastolic_Leave(object sender, EventArgs e)
        {
            int systolic;
            int diastolic;
            if (int.TryParse(txtSystolic.Text, out systolic))
            {
                if (int.TryParse(txtDiastolic.Text, out diastolic))
                {
                    if (systolic < 80 || diastolic < 60)
                        txtStatusBloodPressure.Text = "LOW BP";
                    else if ((systolic >= 80 && systolic <= 120) && (diastolic >= 60 && diastolic <= 80))
                        txtStatusBloodPressure.Text = "NORMAL BP";
                    else if ((systolic >= 121 && systolic <= 139) || (diastolic >= 81 && diastolic <= 89))
                        txtStatusBloodPressure.Text = "PREHYPERTENSION";
                    else if ((systolic >= 140 && systolic <= 159) || (diastolic >= 90 && diastolic <= 99))
                        txtStatusBloodPressure.Text = "HYPERTENSION STAGE 1";
                    else if ((systolic >= 160 && systolic <= 179) || (diastolic >= 100 && diastolic <= 109))
                        txtStatusBloodPressure.Text = "HYPERTENSION STAGE 2";
                    else if ((systolic >= 180 && systolic <= 230) || (diastolic >= 110 && diastolic <= 139))
                        txtStatusBloodPressure.Text = "HIGH BP CRISIS";
                    else if (systolic >= 301 || diastolic >= 301)
                        txtStatusBloodPressure.Text = "INVALID";
                }
            }
        }

        private void txtRRright_Leave(object sender, EventArgs e)
        {
            double rrl;
            double rrr;
            int age;
            if (int.TryParse(txtAge.Text, out age))
            {
                if (double.TryParse(txtRRleft.Text, out rrl) && double.TryParse(txtRRright.Text, out rrr))
                {
                    if (age > 18)
                    {
                        if (rrl >= 16 && rrr <= 20)
                            txtRRStatus.Text = "NORMAL";
                        else
                            txtRRStatus.Text = "NOT NORMAL";
                    }
                    else if (age <= 18)
                    {
                        if (rrl >= 20 && rrr <= 30)
                            txtRRStatus.Text = "NORMAL";
                        else
                            txtRRStatus.Text = "NOT NORMAL";
                    }
                    else if (age < 0)
                    {
                        if (rrl >= 30 && rrr <= 50)
                            txtRRStatus.Text = "NORMAL";
                        else
                            txtRRStatus.Text = "NOT NORMAL";
                    }
                }
            }
        }

        private void txtCRright_Leave(object sender, EventArgs e)
        {
            double crl;
            double crr;
            int age;
            if (int.TryParse(txtAge.Text, out age))
            {
                if (double.TryParse(txtCRleft.Text, out crl) && double.TryParse(txtCRright.Text, out crr))
                {
                    if (age > 18)
                    {
                        if (crl >= 60 && crr <= 100)
                            txtCRStatus.Text = "NORMAL";
                        else
                            txtCRStatus.Text = "NOT NORMAL";
                    }
                    else if (age <= 18)
                    {
                        if (crl >= 80 && crr <= 120)
                            txtCRStatus.Text = "NORMAL";
                        else
                            txtCRStatus.Text = "NOT NORMAL";
                    }
                    else if (age < 0)
                    {
                        if (crl >= 120 && crr <= 160)
                            txtCRStatus.Text = "NORMAL";
                        else
                            txtCRStatus.Text = "NOT NORMAL";
                    }
                }
            }
        }

        public void statistical()
        {
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MMMM");
            int age = Convert.ToInt32(txtAge.Text);
            List<string> mo = new List<string>();

            conn.Open();
            string sql = "SELECT DISTINCT(month) FROM tblstatpatient";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string m = "";
            while (reader.Read())
            {
                m = reader.GetString("month");
                mo.Add(m);
            }
            conn.Close();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            if (!mo.Contains("January") || !mo.Contains("February") || !mo.Contains("March") || !mo.Contains("April") || !mo.Contains("May") || !mo.Contains("June") || !mo.Contains("July") || !mo.Contains("August") || !mo.Contains("September") || !mo.Contains("October") || !mo.Contains("November") || !mo.Contains("December"))
            {
                if (month == "February")
                {
                    string[] mon = new string[2];
                    mon[0] = "January";
                    mon[1] = "February";

                    string[] d = new string[2];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";

                    for (int z = 0; z < 2; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "March")
                {
                    string[] mon = new string[3];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";

                    string[] d = new string[3];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";

                    for (int z = 0; z < 3; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "April")
                {
                    string[] mon = new string[4];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";

                    string[] d = new string[4];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";

                    for (int z = 0; z < 4; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "May")
                {
                    string[] mon = new string[5];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";

                    string[] d = new string[5];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";

                    for (int z = 0; z < 5; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "June")
                {
                    string[] mon = new string[6];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";

                    string[] d = new string[6];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";

                    for (int z = 0; z < 6; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "July")
                {
                    string[] mon = new string[7];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";

                    string[] d = new string[7];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";

                    for (int z = 0; z < 7; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "August")
                {
                    string[] mon = new string[8];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";
                    mon[7] = "August";

                    string[] d = new string[8];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";
                    d[7] = year + "-08-01";

                    for (int z = 0; z < 8; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "September")
                {
                    string[] mon = new string[9];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";
                    mon[7] = "August";
                    mon[8] = "September";

                    string[] d = new string[9];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";
                    d[7] = year + "-08-01";
                    d[8] = year + "-09-01";

                    for (int z = 0; z < 9; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "October")
                {
                    string[] mon = new string[10];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";
                    mon[7] = "August";
                    mon[8] = "September";
                    mon[9] = "October";

                    string[] d = new string[10];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";
                    d[7] = year + "-08-01";
                    d[8] = year + "-09-01";
                    d[9] = year + "-10-01";

                    for (int z = 0; z < 10; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "November")
                {
                    string[] mon = new string[11];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";
                    mon[7] = "August";
                    mon[8] = "September";
                    mon[9] = "October";
                    mon[10] = "November";

                    string[] d = new string[11];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";
                    d[7] = year + "-08-01";
                    d[8] = year + "-09-01";
                    d[9] = year + "-10-01";
                    d[10] = year + "-11-01";

                    for (int z = 0; z < 11; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                else if (month == "December")
                {
                    string[] mon = new string[12];
                    mon[0] = "January";
                    mon[1] = "February";
                    mon[2] = "March";
                    mon[3] = "April";
                    mon[4] = "May";
                    mon[5] = "June";
                    mon[6] = "July";
                    mon[7] = "August";
                    mon[8] = "September";
                    mon[9] = "October";
                    mon[10] = "November";
                    mon[11] = "December";

                    string[] d = new string[12];
                    d[0] = year + "-01-01";
                    d[1] = year + "-02-01";
                    d[2] = year + "-03-01";
                    d[3] = year + "-04-01";
                    d[4] = year + "-05-01";
                    d[5] = year + "-06-01";
                    d[6] = year + "-07-01";
                    d[7] = year + "-08-01";
                    d[8] = year + "-09-01";
                    d[9] = year + "-10-01";
                    d[10] = year + "-11-01";
                    d[11] = year + "-12-01";

                    for (int z = 0; z < 12; z++)
                    {
                        if (!lstMonths.Items.Contains(month))
                        {
                            conn.Open();
                            string sqlState = "INSERT INTO tblstatpatient VALUES (@month, @year, 0, 0, 0, @date)";
                            MySqlCommand cm = new MySqlCommand(sqlState, conn);
                            cm.Parameters.AddWithValue("@month", mon[z]);
                            cm.Parameters.AddWithValue("@year", year);
                            cm.Parameters.AddWithValue("@date", d[z]);
                            cm.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }


            



            if (age <= 18)
            {
                conn.Open();
                string sqlStatement = "UPDATE tblstatpatient SET count = count + 1, pedia = pedia + 1 WHERE month = @month AND year = @year";
                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                cmm.Parameters.AddWithValue("@month", month);
                cmm.Parameters.AddWithValue("@year", year);
                cmm.ExecuteNonQuery();
                conn.Close();
            }
            else if (age > 18)
            {
                conn.Open();
                string sqlStatement = "UPDATE tblstatpatient SET count = count + 1, adult = adult + 1 WHERE month = @month AND year = @year";
                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                cmm.Parameters.AddWithValue("@month", month);
                cmm.Parameters.AddWithValue("@year", year);
                cmm.ExecuteNonQuery();
                conn.Close();
            }
        }
        
    }
}
