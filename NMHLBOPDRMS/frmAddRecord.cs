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
    public partial class frmAddRecord : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmAddRecord()
        {
            InitializeComponent();

            dtPickerBirthday.MaxDate = DateTime.Now.ToLocalTime();
        }

        public void updatePersonalInfo()
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
                string activityy = "UPDATED PERSONAL INFORMATION OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

                conStr.activityLog(acctype, fname, mname, lname, activityy);

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

        private void btnUpdatePersonalInfo_Click(object sender, EventArgs e)
        {
            updatePersonalInfo();

            metroTabControl1.SelectedIndex = 1;
            metroTabControl1.Refresh();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updatePersonalInfo();

                metroTabControl1.SelectedIndex = 1;
                metroTabControl1.Refresh();
            }
        }

        private void btnSubmitMedicalInfo_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT date FROM tblMedical_Information WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumber.Text);
            string date = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                date = reader.GetDateTime("date").ToString("yyyy-MM-dd");
            }
            conn.Close();

            frmNurseSendTo f1 = new frmNurseSendTo();
            f1.number = lblNumber.Text;
            f1.lname = txtLname.Text;
            f1.fname = txtFname.Text;
            f1.mname = txtMname.Text;
            f1.status = txtPIRStatus.Text;
            f1.date = date;
            f1.Show();
        }

        private void frmAddRecord_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
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

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                close();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void txtNumberSymptoms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtLname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        
        public string date = "";

        public void oldRecordMedicalInformation()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblMedical_Information WHERE patientsNo = @patientnum";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string patientnum = "";
            string temperature = "";
            string bp = "";
            string complaints = "";
            string symptoms = "";
            string numberOfDaysWithSymptoms = "";
            string morbidityWeek = "";
            string cr = "";
            string rr = "";
            string weight = "";
            string height = "";
            string bmi = "";
            string bmiStat = "";
            while (reader.Read())
            {
                patientnum = reader.GetString("patientsNo");
                temperature = reader.GetString("temperature");
                bp = reader.GetString("bp");
                complaints = reader.GetString("complaints");
                symptoms = reader.GetString("symptoms");
                numberOfDaysWithSymptoms = reader.GetString("numberOfDaysWithSymptoms");
                morbidityWeek = reader.GetString("morbidityWeek");
                date = reader.GetDateTime("date").ToString("yyyy-MM-dd");
                cr = reader.GetString("cr");
                rr = reader.GetString("rr");
                weight = reader.GetString("weight");
                height = reader.GetString("height");
                bmi = reader.GetString("bmi");
                bmiStat = reader.GetString("bmiCategory");

                lstOld.Items.Add(symptoms);
            }
            conn.Close();

            for (int a = 0; a < lstOld.Items.Count; a++)
            {
                conn.Open();
                string sql1 = "INSERT INTO tblOld_Medical_Information VALUES (@patientnum, @temperature, @bp, @cr, @rr, @weight, @height, @bmi, @category, @complaints, @symptoms, @numberOfDaysWithSymptoms, @morbidityWeek, @date); DELETE FROM tblMedical_Information WHERE patientsNo = @patientnum";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@patientnum", patientnum);
                cmd1.Parameters.AddWithValue("@temperature", temperature);
                cmd1.Parameters.AddWithValue("@bp", bp);
                cmd1.Parameters.AddWithValue("@cr", cr);
                cmd1.Parameters.AddWithValue("@rr", rr);
                cmd1.Parameters.AddWithValue("@weight", weight);
                cmd1.Parameters.AddWithValue("@height", height);
                cmd1.Parameters.AddWithValue("@bmi", bmi);
                cmd1.Parameters.AddWithValue("@category", bmiStat);
                cmd1.Parameters.AddWithValue("@complaints", complaints);
                cmd1.Parameters.AddWithValue("@symptoms", lstOld.Items[a].ToString());
                cmd1.Parameters.AddWithValue("@numberOfDaysWithSymptoms", numberOfDaysWithSymptoms);
                cmd1.Parameters.AddWithValue("@morbidityWeek", morbidityWeek);
                cmd1.Parameters.AddWithValue("@date", date);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void btnSaveSymptoms_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (txtStatusBloodPressure.Text != "INVALID")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    oldRecordMedicalInformation();

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
                                cmd.Parameters.AddWithValue("@numberofdays", txtNumberSymptoms.Text);
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
                                cmd.Parameters.AddWithValue("@numberofdays", txtNumberSymptoms.Text);
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
                    string activityy = "SUCCESFULLY SAVED MEDICAL INFORMATION OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Medical Information successfuly saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSubmitMedicalInfo.Enabled = true;
                    btnSaveSymptoms.Enabled = false;
                }
                else
                    MessageBox.Show("Blood Pressure must be valid!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtNumberSymptoms_TextChanged(object sender, EventArgs e)
        {
            txtNumberSymptoms.MaxLength = 3;

            int temp;
            if (txtNumberSymptoms.Text != "")
            {
                if (int.TryParse(txtNumberSymptoms.Text, out temp))
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

        private void btnUndo_Click(object sender, EventArgs e)
        {
            lbl3.Visible = false;
            txtOtherSymptom.Visible = false;
            cboSymptoms.Enabled = true;
            cboSymptoms.Text = "";
            txtOtherSymptom.Clear();
            btnUndo.Visible = false;
            btnAddOtherSymptoms.Visible = false;
            cboSymptoms.Focus();
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

        private void txtOtherSymptom_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherSymptom.Text != "")
                btnAddOtherSymptoms.Enabled = true;
            else
                btnAddOtherSymptoms.Enabled = false;
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

        private void txtTemperature_TextChanged(object sender, EventArgs e)
        {
            txtTemperature.MaxLength = 2;

            if (txtTemperature.Text == "")
            {
                lstSymptoms.Items.Remove("FEVER");
                txtStatusTemperature.Text = "STATUS";
            }
        }

        private void txtDiastolic_TextChanged(object sender, EventArgs e)
        {
            txtDiastolic.MaxLength = 3;

            if (txtDiastolic.Text == "")
                txtStatusBloodPressure.Text = "STATUS";
        }

        private void txtCRright_TextChanged(object sender, EventArgs e)
        {
            txtCRright.MaxLength = 3;

            if (txtCRright.Text == "")
                txtCRStatus.Text = "STATUS";
        }

        private void txtRRright_TextChanged(object sender, EventArgs e)
        {
            txtRRright.MaxLength = 3;

            if (txtRRright.Text == "")
                txtRRStatus.Text = "STATUS";
        }

        private void txtCRleft_TextChanged(object sender, EventArgs e)
        {
            txtCRleft.MaxLength = 3;
        }

        private void txtSystolic_TextChanged(object sender, EventArgs e)
        {
            txtSystolic.MaxLength = 3;
        }

        private void txtRRleft_TextChanged(object sender, EventArgs e)
        {
            txtRRleft.MaxLength = 3;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            txtHeight.MaxLength = 3;

            if (txtHeight.Text == "")
                txtBMICategory.Text = "STATUS";
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            txtWeight.MaxLength = 3;
        }

        private void dtPickerBirthday_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan tmspn = DateTime.Now - dtPickerBirthday.Value;
            int years = DateTime.Now.Year - dtPickerBirthday.Value.Year;
            if (dtPickerBirthday.Value.AddYears(years) > DateTime.Now)
                years--;
            txtAge.Text = years.ToString();
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
                else if (temp > 37)
                {
                    lstSymptoms.Items.Remove("FEVER");
                    lstSymptoms.Items.Add("FEVER");
                    txtStatusTemperature.Text = "NOT NORMAL";
                }
                else if (temp == 36 || temp == 37)
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

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            double weight, height;
            double bmi = 0.00;
            
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
    }
}

