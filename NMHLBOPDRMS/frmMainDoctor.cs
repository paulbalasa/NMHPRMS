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
    public partial class frmMainDoctor : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public frmMainDoctor()
        {
            InitializeComponent();
        }

        private void pnlSlctnLogout_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlll = "UPDATE tblAccounts SET logInStatus = 'INACTIVE' WHERE IDNumber = @id";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@id", lblIDNumber.Text);
            cmddd.ExecuteNonQuery();
            conn.Close();

            //Activity Log
            string accType = "DOCTOR";
            string activity = "SUCCESSFULLY LOGGED OUT.";

            conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmLogin f1 = new frmLogin();
            f1.Show();
        }

        private void pnlSlctnHome_Click(object sender, EventArgs e)
        {
            pnlHome.Show();
            usrCntrlPatientRecordDoctor1.Hide();
            usrCntrlNextInLineDoctor1.Hide();
            usrCntrlProfileDoctor1.Hide();
            if (pnlSlctnHome.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnPatientsRecord_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecordDoctor1.Show();
            usrCntrlNextInLineDoctor1.Hide();
            usrCntrlProfileDoctor1.Hide();
            if (pnlSlctnPatientsRecord.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnPatientsRecord.Checked == false)
            {
                pnlSlctnPatientsRecord.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.Black;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnNextInLinePatient_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecordDoctor1.Hide();
            usrCntrlNextInLineDoctor1.Show();
            usrCntrlProfileDoctor1.Hide();
            if (pnlSlctnNextInLinePatient.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnNextInLinePatient.Checked == false)
            {
                pnlSlctnNextInLinePatient.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        public int count;

        private void frmMainDoctor_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            timer1.Start();

            conn.Open();
            string sql6 = "SELECT patientNo AS 'Patient No.', lastName AS 'Lastname', firstName AS 'Firstname', middleName AS 'Middlename' ,status AS 'Status', sentBy AS 'Sent By', sentTo AS 'Sent To', daterequested AS 'Date Requested' FROM tblNext_In_Line_Doctor WHERE sentTo LIKE '" + lblDoctor.Text + "' ";
            adapter = new MySqlDataAdapter(sql6, conn);
            adapter.Fill(dS, "tblNext_In_Line_Doctor");
            dgNextInLineDoctor.DataMember = "tblNext_In_Line_Doctor";
            dgNextInLineDoctor.DataSource = dS;
            conn.Close();

            dgNextInLineDoctor.Columns[0].Width = 125;
            dgNextInLineDoctor.Columns[1].Width = 175;
            dgNextInLineDoctor.Columns[2].Width = 175;
            dgNextInLineDoctor.Columns[3].Width = 175;
            dgNextInLineDoctor.Columns[4].Width = 100;
            dgNextInLineDoctor.Columns[5].Width = 100;
            dgNextInLineDoctor.Columns[6].Width = 200;
            dgNextInLineDoctor.Columns[7].Width = 100;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor WHERE sentTo = '" + lblDoctor.Text + "' ", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void pnlSlctnProfile_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlPatientRecordDoctor1.Hide();
            usrCntrlNextInLineDoctor1.Hide();
            usrCntrlProfileDoctor1.Show();
            if (pnlSlctnProfile.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnProfile.Checked == false)
            {
                pnlSlctnProfile.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnPatientsRecord.ForeColor = Color.White;
                pnlSlctnNextInLinePatient.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnPatientsRecord.Checked = false;
                pnlSlctnNextInLinePatient.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void btnUpdateAccountProfile_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    byte[] img = null;
                    FileStream fs = new FileStream(lblPath.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);

                    conn.Open();
                    string sql = "UPDATE tblAccount_Profile SET lastName = @lname, firstName = @fname, middleName = @mname, birthday = @bday, profilePicture = @picture, pathName = @path WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@lname", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text);
                    cmd.Parameters.AddWithValue("@bday", dtBirthday.Value);
                    cmd.Parameters.AddWithValue("@picture", img);
                    cmd.Parameters.AddWithValue("@path", lblPath.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "DOCTOR";
                    string activity = "SUCCESSFULLY UPDATED PERSONAL INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Personal Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Select image first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                else if (txtUsername.Text == "")
                    txtUsername.Focus();
                else if (txtPassword.Text == "")
                    txtPassword.Focus();
            }
        }

        private void btnUpdateAccountSettings_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "" && txtRetypePassword.Text != "")
            {
                if (txtRetypePassword.Text == txtPassword.Text)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "UPDATE tblAccounts SET username =  @username, password = @password WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "DOCTOR";
                    string activity = "SUCCESSFULLY UPDATED ACCOUNT INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Account Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChange.Enabled = true;
                    txtRetypePassword.Enabled = false;
                    txtRetypePassword.Clear();
                    btnCancel.Visible = false;
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
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string imgLocation = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG files(*.jpg)|*.jpg|PNG files(*.png)|*png|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pctrProfile.ImageLocation = imgLocation;
                lblPath.Text = imgLocation;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
            txtPassword.Focus();
            btnChange.Enabled = false;
            txtRetypePassword.Enabled = true;
            btnCancel.Visible = true;
        }

        private void dgNextInLineDoctor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string number = dgNextInLineDoctor.Rows[e.RowIndex].Cells[0].Value.ToString(); 
            string statuss = dgNextInLineDoctor.Rows[e.RowIndex].Cells[4].Value.ToString();
            string sentBy = dgNextInLineDoctor.Rows[e.RowIndex].Cells[5].Value.ToString();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            string patientnum = "";
            string lname = "";
            string fname = "";
            string mname = "";
            string sex = "";
            string status = "";
            string birthday = "";
            int age = 0;
            string address = "";
            while (reader.Read())
            {
                patientnum = reader.GetString("patientsNo");
                lname = reader.GetString("lastName");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                sex = reader.GetString("sex");
                status = reader.GetString("civilStatus");
                birthday = reader.GetString("birthday");
                age = reader.GetInt16("age");
                address = reader.GetString("address");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblNext_In_Line_Doctor WHERE patientNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string stat = "";
            while (reader1.Read())
            {
                stat = reader1.GetString("status");
            }
            conn.Close();

            if (statuss == "NEW PATIENT" && sentBy == "NURSE")
            {
                frmNewPatientDoctor f1 = new frmNewPatientDoctor();
                f1.status = statuss;
                f1.sentby = sentBy;
                f1.number = number;

                //Personal Information
                f1.lblNumber.Text = patientnum;
                f1.txtLname.Text = lname;
                f1.txtFname.Text = fname;
                f1.txtMname.Text = mname;
                f1.cboSex.Text = sex;
                f1.cboStatus.Text = status;
                f1.dtPickerBirthday.Text = birthday;
                f1.txtAge.Text = age.ToString();
                f1.txtAddress.Text = address;
                f1.lblBackNurse.Visible = false;

                //Display Medical Information
                conn.Open();
                string sql2 = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                string temp = "";
                string bp = "";
                string complaints = "";
                string symptoms = "";
                string numberOfDaysWithSymptoms = "";
                string morbidityWeek = "";
                string date = "";
                string cr = "";
                string rr = "";
                string weight = "";
                string height = "";
                string bmi = "";
                string bmiStat = "";
                while (reader2.Read())
                {
                    temp = reader2.GetString("temperature");
                    bp = reader2.GetString("bp");
                    complaints = reader2.GetString("complaints");
                    symptoms = reader2.GetString("symptoms");
                    numberOfDaysWithSymptoms = reader2.GetString("numberOfDaysWithSymptoms");
                    morbidityWeek = reader2.GetString("morbidityWeek");
                    date = reader2.GetString("date");
                    cr = reader2.GetString("cr");
                    rr = reader2.GetString("rr");
                    weight = reader2.GetString("weight");
                    height = reader2.GetString("height");
                    bmi = reader2.GetString("bmi");
                    bmiStat = reader2.GetString("bmiCategory");

                    f1.lstSymptoms.Items.Add(symptoms);
                }
                conn.Close();

                f1.lblTemperature.Text = temp;
                f1.lblBP.Text = bp;
                f1.txtComplaints.Text = complaints;
                f1.txtNumberOfDaySymptoms.Text = numberOfDaysWithSymptoms;
                f1.cboMorbidity.Text = morbidityWeek;
                f1.dtPicker.Text = date;
                f1.lblCR.Text = cr;
                f1.lblRR.Text = rr;
                f1.lblWeight.Text = weight;
                f1.lblHeight.Text = height;
                f1.lblBMI.Text = bmi;
                f1.lblBMIStat.Text = bmiStat;
                f1.docID = lblIDNumber.Text;
                f1.ShowDialog();
            }
            else if (statuss == "OLD PATIENT" && sentBy == "NURSE")
            {
                frmAddRecordDoctor f1 = new frmAddRecordDoctor();
                f1.status = statuss;
                f1.sentby = sentBy;
                f1.number = number;

                //Display Personal Information
                f1.lblNumber.Text = patientnum;
                f1.txtLname.Text = lname;
                f1.txtFname.Text = fname;
                f1.txtMname.Text = mname;
                f1.cboSex.Text = sex;
                f1.cboStatus.Text = status;
                f1.dtPickerBirthday.Text = birthday;
                f1.txtAge.Text = age.ToString();
                f1.txtAddress.Text = address;
                f1.lblBackNurse.Visible = false;

                //Display Medical Information
                conn.Open();
                string sql2 = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                string temp = "";
                string bp = "";
                string complaints = "";
                string symptoms = "";
                string numberOfDaysWithSymptoms = "";
                string morbidityWeek = "";
                string date = "";
                string weight = "";
                string height = "";
                string bmi = "";
                string bmiStat = "";
                while (reader2.Read())
                {
                    temp = reader2.GetString("temperature");
                    bp = reader2.GetString("bp");
                    complaints = reader2.GetString("complaints");
                    symptoms = reader2.GetString("symptoms");
                    numberOfDaysWithSymptoms = reader2.GetString("numberOfDaysWithSymptoms");
                    morbidityWeek = reader2.GetString("morbidityWeek");
                    date = reader2.GetString("date");
                    weight = reader2.GetString("weight");
                    height = reader2.GetString("height");
                    bmi = reader2.GetString("bmi");
                    bmiStat = reader2.GetString("bmiCategory");

                    f1.lstSymptoms.Items.Add(symptoms);
                }
                conn.Close();

                f1.lblTemperature.Text = temp;
                f1.lblBP.Text = bp;
                f1.txtComplaints.Text = complaints;
                f1.txtNumberSymptoms.Text = numberOfDaysWithSymptoms;
                f1.cboMorbidity.Text = morbidityWeek;
                f1.dtPicker.Text = date;
                f1.lblWeight.Text = weight;
                f1.lblHeight.Text = height;
                f1.lblBMI.Text = bmi;
                f1.lblBMIStat.Text = bmiStat;
                f1.docID = lblIDNumber.Text;
                f1.Show();
            }
            else if (statuss == "NEW PATIENT" && sentBy == "LABORATORY")
            {
                frmNewPatientDoctor f1 = new frmNewPatientDoctor();
                f1.status = statuss;
                f1.sentby = sentBy;
                f1.number = number;

                //Display Personal Information
                f1.lblNumber.Text = patientnum;
                f1.txtLname.Text = lname;
                f1.txtFname.Text = fname;
                f1.txtMname.Text = mname;
                f1.cboSex.Text = sex;
                f1.cboStatus.Text = status;
                f1.dtPickerBirthday.Text = birthday;
                f1.txtAge.Text = age.ToString();
                f1.txtAddress.Text = address;
                f1.lblBackNurse.Visible = false;

                //Display Medical Information
                conn.Open();
                string sql2 = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                string temp = "";
                string bp = "";
                string complaints = "";
                string symptoms = "";
                string numberOfDaysWithSymptoms = "";
                string morbidityWeek = "";
                string date = "";
                while (reader2.Read())
                {
                    temp = reader2.GetString("temperature");
                    bp = reader2.GetString("bp");
                    complaints = reader2.GetString("complaints");
                    symptoms = reader2.GetString("symptoms");
                    numberOfDaysWithSymptoms = reader2.GetString("numberOfDaysWithSymptoms");
                    morbidityWeek = reader2.GetString("morbidityWeek");
                    date = reader2.GetString("date");

                    f1.lstSymptoms.Items.Add(symptoms);
                }
                conn.Close();

                f1.lblTemperature.Text = temp;
                f1.lblBP.Text = bp;
                f1.txtComplaints.Text = complaints;
                f1.txtNumberOfDaySymptoms.Text = numberOfDaysWithSymptoms;
                f1.cboMorbidity.Text = morbidityWeek;
                f1.dtPicker.Text = date;
                
                //Display Laboratory
                //List of Exam
                conn.Open();
                string sq = "SELECT * FROM tblmedical_laboratory_1 WHERE patientsNo = @num";
                MySqlCommand cm = new MySqlCommand(sq, conn);
                cm.Parameters.AddWithValue("@num", number);
                MySqlDataReader reade = cm.ExecuteReader();
                string labExam = "";
                while (reade.Read())
                {
                    labExam = reade.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam);
                }
                conn.Close();

                conn.Open();
                string sqq = "SELECT * FROM tblmedical_laboratory_ultrasound WHERE patientsNo = @num";
                MySqlCommand cmm = new MySqlCommand(sqq, conn);
                cmm.Parameters.AddWithValue("@num", number);
                MySqlDataReader readee = cmm.ExecuteReader();
                string labExam1 = "";
                while (readee.Read())
                {
                    labExam1 = readee.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam1);
                }
                conn.Close();

                conn.Open();
                string sqqq = "SELECT * FROM tblmedical_laboratory_xray WHERE patientsNo = @num";
                MySqlCommand cmmm = new MySqlCommand(sqqq, conn);
                cmmm.Parameters.AddWithValue("@num", number);
                MySqlDataReader readeee = cmmm.ExecuteReader();
                string labExam11 = "";
                while (readeee.Read())
                {
                    labExam11 = readeee.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam11);
                }
                conn.Close();

                //Blood Chemistry
                conn.Open();
                string sql5 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                cmd5.Parameters.AddWithValue("@num", number);
                string Bfasting_blood_sugar = "";
                string Bblood_urea_nitrogen = "";
                string Bcreatinine = "";
                string Bcholesterol = "";
                string Btriglycerides = "";
                string Bhdl = "";
                string Bldl = "";
                string Bvldl = "";
                string Bblood_uric_acid = "";
                string Bsodium = "";
                string Bpotassium = "";
                string Bchloride = "";
                string Bsgpt_alt = "";
                string Bsgot_ast = "";
                string Bmedtech = "";
                string Bpathologist = "";
                string Bdaterequested = "";
                string Bdateexamined = "";
                MySqlDataReader reader5 = cmd5.ExecuteReader();
                while (reader5.Read())
                {
                    Bfasting_blood_sugar = reader5.GetString("fasting_blood_sugar");
                    Bblood_urea_nitrogen = reader5.GetString("blood_urea_nitrogen");
                    Bcreatinine = reader5.GetString("creatinine");
                    Bcholesterol = reader5.GetString("cholesterol");
                    Btriglycerides = reader5.GetString("triglycerides");
                    Bhdl = reader5.GetString("hdl");
                    Bldl = reader5.GetString("ldl");
                    Bvldl = reader5.GetString("vldl");
                    Bblood_uric_acid = reader5.GetString("blood_uric_acid");
                    Bsodium = reader5.GetString("sodium");
                    Bpotassium = reader5.GetString("potassium");
                    Bchloride = reader5.GetString("chloride");
                    Bsgpt_alt = reader5.GetString("sgpt_alt");
                    Bsgot_ast = reader5.GetString("sgot_ast");
                    Bmedtech = reader5.GetString("medtech");
                    Bpathologist = reader5.GetString("pathologists");
                    Bdaterequested = reader5.GetString("daterequested");
                    Bdateexamined = reader5.GetString("dateexamined");
                }
                conn.Close();
                f1.dtDateRequested.Text = Bdaterequested;
                f1.dtDateExamined.Text = Bdateexamined;
                f1.lblFastingBlood.Text = Bfasting_blood_sugar;
                f1.lblBloodUrea.Text = Bblood_urea_nitrogen;
                f1.lblCreatinineBlood.Text = Bcreatinine;
                f1.lblCholesterolBlood.Text = Bcholesterol;
                f1.lblTriglyBlood.Text = Btriglycerides;
                f1.lblHDLBlood.Text = Bhdl;
                f1.lblLDLBlood.Text = Bldl;
                f1.lblVDLBlood.Text = Bvldl;
                f1.lblBloodUric.Text = Bblood_uric_acid;
                f1.lblSodiumBlood.Text = Bsodium;
                f1.lblPotassiumBlood.Text = Bpotassium;
                f1.lblChlorideBlood.Text = Bchloride;
                f1.lblSGPTBlood.Text = Bsgpt_alt;
                f1.lblSGOTBlood.Text = Bsgot_ast;
                f1.lblMedTechBloodChem.Text = Bmedtech;
                f1.lblPathologistBloodChem.Text = Bpathologist;

                //Fecalysis
                conn.Open();
                string sql6 = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
                MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
                cmd6.Parameters.AddWithValue("@num", number);
                string Fcolor = "";
                string Fcharacters = "";
                string Freaction = "";
                string Foccult_blood = "";
                string Fpus_cells = "";
                string Frbc = "";
                string Ffat_globules = "";
                string Fmacrophage = "";
                string Fbacteria = "";
                string Fparasites_or_ova = "";
                string Fmedtech = "";
                string Fpathologist = "";
                string Fdaterequested = "";
                string Fdateexamined = "";
                MySqlDataReader reader6 = cmd6.ExecuteReader();
                while (reader6.Read())
                {
                    Fcolor = reader6.GetString("color");
                    Fcharacters = reader6.GetString("characters");
                    Freaction = reader6.GetString("reaction");
                    Foccult_blood = reader6.GetString("occult_blood");
                    Fmedtech = reader6.GetString("medtech");
                    Fpathologist = reader6.GetString("pathologists");
                    Fdaterequested = reader6.GetString("daterequested");
                    Fdateexamined = reader6.GetString("dateexamined");
                }
                conn.Close();

                conn.Open();
                string sql7 = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd7 = new MySqlCommand(sql7, conn);
                cmd7.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader7 = cmd7.ExecuteReader();
                while (reader7.Read())
                {
                    Fpus_cells = reader7.GetString("pus_cells");
                    Frbc = reader7.GetString("rbc");
                    Ffat_globules = reader7.GetString("fat_globules");
                    Fmacrophage = reader7.GetString("macrophage");
                    Fbacteria = reader7.GetString("bacteria");
                    Fparasites_or_ova = reader7.GetString("parasites_or_ova");
                }
                conn.Close();
                f1.lblColorFeca.Text = Fcolor;
                f1.lblCharacterFeca.Text = Fcharacters;
                f1.lblReactionFeca.Text = Freaction;
                f1.lblOccultFeca.Text = Foccult_blood;
                f1.lblPluesFeca.Text = Fpus_cells;
                f1.lblRBCFeca.Text = Frbc;
                f1.lblFlatFeca.Text = Ffat_globules;
                f1.lblMacroFeca.Text = Fmacrophage;
                f1.lblBacteriaFeca.Text = Fbacteria;
                f1.lblParasitesFeca.Text = Fparasites_or_ova;
                f1.lblMedTechFecalysis.Text = Fmedtech;
                f1.lblPathologistsFecalysis.Text = Fpathologist;

                //Hematology
                conn.Open();
                string sql8 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
                MySqlCommand cmd8 = new MySqlCommand(sql8, conn);
                cmd8.Parameters.AddWithValue("@num", number);
                string Hhemoglobin = "";
                string Hhematocrit = "";
                string Hwbc_count = "";
                string Hrbc_count = "";
                string Hplatelet = "";
                string Hbleeding_time = "";
                string Hclotting_time = "";
                string Habo_group = "";
                string Hsegmenters = "";
                string Hlymphocytes = "";
                string Hmonocytes = "";
                string Heosinophils = "";
                string Hbasophils = "";
                string Hstab = "";
                string Hothers = "";
                string Hmedtech = "";
                string Hpathologist = "";
                string Hdaterequested = "";
                string Hdateexamined = "";
                MySqlDataReader reader8 = cmd8.ExecuteReader();
                while (reader8.Read())
                {
                    Hhemoglobin = reader8.GetString("hemoglobin");
                    Hhematocrit = reader8.GetString("hematocrit");
                    Hwbc_count = reader8.GetString("wbc_count");
                    Hrbc_count = reader8.GetString("rbc_count");
                    Hplatelet = reader8.GetString("platelet");
                    Hbleeding_time = reader8.GetString("bleeding_time");
                    Hclotting_time = reader8.GetString("clotting_time");
                    Habo_group = reader8.GetString("abo_group");
                    Hsegmenters = reader8.GetString("segmenters");
                    Hlymphocytes = reader8.GetString("lymphocytes");
                    Hmonocytes = reader8.GetString("monocytes");
                    Heosinophils = reader8.GetString("eosinophils");
                    Hbasophils = reader8.GetString("basophils");
                    Hstab = reader8.GetString("stab");
                    Hothers = reader8.GetString("others");
                    Hmedtech = reader8.GetString("medtech");
                    Hpathologist = reader8.GetString("pathologists");
                    Hdaterequested = reader8.GetString("daterequested");
                    Hdateexamined = reader8.GetString("dateexamined");
                }
                conn.Close();
                f1.lblHemoglobin.Text = Hhemoglobin;
                f1.lblHematocrit.Text = Hhematocrit;
                f1.lblWBC.Text = Hwbc_count;
                f1.lblRBC.Text = Hrbc_count;
                f1.lblPlatelet.Text = Hplatelet;
                f1.lblBleeding.Text = Hbleeding_time;
                f1.lblClotting.Text = Hclotting_time;
                f1.lblABO.Text = Habo_group;
                f1.lblSegmenters.Text = Hsegmenters;
                f1.lblLymphocytes.Text = Hlymphocytes;
                f1.lblMonocytes.Text = Hmonocytes;
                f1.lblEosinophils.Text = Heosinophils;
                f1.lblBasophils.Text = Hbasophils;
                f1.lblStab.Text = Hstab;
                f1.lblOthers.Text = Hothers;
                f1.lblMedTechHematology.Text = Hmedtech;
                f1.lblPathologistHematology.Text = Hpathologist;

                //Urinalysis
                conn.Open();
                string sql9 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
                MySqlCommand cmd9 = new MySqlCommand(sql9, conn);
                cmd9.Parameters.AddWithValue("@num", number);
                string Uricolor = "";
                string Uricharacters = "";
                string Uriprotein = "";
                string Urisugar = "";
                string Uriph = "";
                string UrispGr = "";
                string UripregnancyTest = "";
                string Urimedtech = "";
                string Uripathologist = "";
                string Uridaterequested = "";
                string Uridateexamined = "";
                MySqlDataReader reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {
                    Uricolor = reader9.GetString("color");
                    Uricharacters = reader9.GetString("characters");
                    Uriprotein = reader9.GetString("protein");
                    Urisugar = reader9.GetString("sugar");
                    Uriph = reader9.GetString("ph");
                    UrispGr = reader9.GetString("spGr");
                    UripregnancyTest = reader9.GetString("pregnancyTest");
                    Urimedtech = reader9.GetString("medtech");
                    Uripathologist = reader9.GetString("pathologists");
                    Uridaterequested = reader9.GetString("daterequested");
                    Uridateexamined = reader9.GetString("dateexamined");
                }
                conn.Close();

                conn.Open();
                string sql10 = "SELECT * FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd10 = new MySqlCommand(sql10, conn);
                cmd10.Parameters.AddWithValue("@num", number);
                string Uripus_cells = "";
                string Urirbc = "";
                string Uriepith_cells = "";
                string Uribacteria = "";
                string Urimucus_thread = "";
                string Uriamorphous_urates = "";
                string Uricasts = "";
                string Uricrystals = "";
                string Uriothers = "";
                MySqlDataReader reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {
                    Uripus_cells = reader10.GetString("pus_cells");
                    Urirbc = reader10.GetString("rbc");
                    Uriepith_cells = reader10.GetString("epith_cells");
                    Uribacteria = reader10.GetString("bacteria");
                    Urimucus_thread = reader10.GetString("mucus_thread");
                    Uriamorphous_urates = reader10.GetString("amorphous_urates");
                    Uricasts = reader10.GetString("casts");
                    Uricrystals = reader10.GetString("crystals");
                    Uriothers = reader10.GetString("others");
                }
                conn.Close();
                f1.lblColorUri.Text = Uricolor;
                f1.lblCharacterUri.Text = Uricharacters;
                f1.lblProteinUri.Text = Uriprotein;
                f1.lblSugarUri.Text = Urisugar;
                f1.lblPHUri.Text = Uriph;
                f1.lblSpUri.Text = UrispGr;
                f1.lblPregUri.Text = UripregnancyTest;
                f1.lblPUSUri.Text = Uripus_cells;
                f1.lblRBCUri.Text = Urirbc;
                f1.lblEpithUri.Text = Uriepith_cells;
                f1.lblBacteriaUri.Text = Uribacteria;
                f1.lblMucusUri.Text = Urimucus_thread;
                f1.lblAmorphousUri.Text = Uriamorphous_urates;
                f1.lblCastsUri.Text = Uricasts;
                f1.lblCrystalsUri.Text = Uricrystals;
                f1.lblOthersUri.Text = Uriothers;
                f1.lblMedTechUrinalysis.Text = Urimedtech;
                f1.lblPathologistUrinalysis.Text = Uripathologist;

                //Ultrasound
                conn.Open();
                string sql11 = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
                MySqlCommand cmd11 = new MySqlCommand(sql11, conn);
                cmd11.Parameters.AddWithValue("@num", number);
                string Upatientnum = "";
                string Utypeofexaminations = "";
                string Uresults = "";
                string Uimpression = "";
                string Usonologists = "";
                string Udaterequested = "";
                string Udateexamined = "";
                MySqlDataReader reader11 = cmd11.ExecuteReader();
                while (reader11.Read())
                {
                    Upatientnum = reader11.GetString("patientsNo");
                    Utypeofexaminations = reader11.GetString("typeofexaminations");
                    Uresults = reader11.GetString("result");
                    Uimpression = reader11.GetString("impression");
                    Usonologists = reader11.GetString("sonologists");
                    Udaterequested = reader11.GetString("daterequested");
                    Udateexamined = reader11.GetString("dateexamined");
                    f1.lblTypeUltra.Items.Add(Utypeofexaminations);
                }
                conn.Close();
                f1.lstXrayFindings.Text = Uresults;
                f1.lstXrayImpression.Text = Uimpression;
                f1.lblMedTechUltrasound.Text = Usonologists;

                //Xray
                conn.Open();
                string sql12 = "SELECT * FROM tblLaboratory_Xray WHERE patientsNo = @num";
                MySqlCommand cmd12 = new MySqlCommand(sql12, conn);
                cmd12.Parameters.AddWithValue("@num", number);
                string Xpatientnum = "";
                string Xtypeofexamination = "";
                string Xresults = "";
                string Ximpression = "";
                string Xsonologists = "";
                string Xdaterequested = "";
                string Xdateexamined = "";
                MySqlDataReader reader12 = cmd12.ExecuteReader();
                while (reader12.Read())
                {
                    Xpatientnum = reader12.GetString("patientsNo");
                    Xtypeofexamination = reader12.GetString("typeofexaminations");
                    Xresults = reader12.GetString("result");
                    Ximpression = reader12.GetString("impression");
                    Xsonologists = reader12.GetString("radiologists");
                    Xdaterequested = reader12.GetString("daterequested");
                    Xdateexamined = reader12.GetString("dateexamined");
                    f1.lblTypeXray.Items.Add(Xtypeofexamination);
                }
                conn.Close();
                f1.lstXrayFindings.Text = Xresults;
                f1.lstXrayImpression.Text = Ximpression;
                f1.lblMedTechXray.Text = Xsonologists;
                f1.docID = lblIDNumber.Text;
                f1.Show();
            }
            else if (statuss == "OLD PATIENT" && sentBy == "LABORATORY")
            {
                frmAddRecordDoctor f1 = new frmAddRecordDoctor();
                f1.status = statuss;
                f1.sentby = sentBy;
                f1.number = number;

                //Personal Information
                f1.lblNumber.Text = patientnum;
                f1.txtLname.Text = lname;
                f1.txtFname.Text = fname;
                f1.txtMname.Text = mname;
                f1.cboSex.Text = sex;
                f1.cboStatus.Text = status;
                f1.dtPickerBirthday.Text = birthday;
                f1.txtAge.Text = age.ToString();
                f1.txtAddress.Text = address;
                f1.lblBackNurse.Visible = false;

                //Display Medical Information
                conn.Open();
                string sql2 = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                string temp = "";
                string bp = "";
                string complaints = "";
                string symptoms = "";
                string numberOfDaysWithSymptoms = "";
                string morbidityWeek = "";
                string date = "";
                while (reader2.Read())
                {
                    temp = reader2.GetString("temperature");
                    bp = reader2.GetString("bp");
                    complaints = reader2.GetString("complaints");
                    symptoms = reader2.GetString("symptoms");
                    numberOfDaysWithSymptoms = reader2.GetString("numberOfDaysWithSymptoms");
                    morbidityWeek = reader2.GetString("morbidityWeek");
                    date = reader2.GetString("date");

                    f1.lstSymptoms.Items.Add(symptoms);
                }
                conn.Close();

                f1.lblTemperature.Text = temp;
                f1.lblBP.Text = bp + " mmHg";
                f1.txtComplaints.Text = complaints;
                f1.txtNumberSymptoms.Text = numberOfDaysWithSymptoms;
                f1.cboMorbidity.Text = morbidityWeek;
                f1.dtPicker.Text = date;
                
                //Display Laboratory
                //List of Exam
                conn.Open();
                string sq = "SELECT * FROM tblmedical_laboratory_1 WHERE patientsNo = @num";
                MySqlCommand cm = new MySqlCommand(sq, conn);
                cm.Parameters.AddWithValue("@num", number);
                MySqlDataReader reade = cm.ExecuteReader();
                string labExam = "";
                while (reade.Read())
                {
                    labExam = reade.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam);
                }
                conn.Close();

                conn.Open();
                string sqq = "SELECT * FROM tblmedical_laboratory_ultrasound WHERE patientsNo = @num";
                MySqlCommand cmm = new MySqlCommand(sqq, conn);
                cmm.Parameters.AddWithValue("@num", number);
                MySqlDataReader readee = cmm.ExecuteReader();
                string labExam1 = "";
                while (readee.Read())
                {
                    labExam1 = readee.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam1);
                }
                conn.Close();

                conn.Open();
                string sqqq = "SELECT * FROM tblmedical_laboratory_xray WHERE patientsNo = @num";
                MySqlCommand cmmm = new MySqlCommand(sqqq, conn);
                cmmm.Parameters.AddWithValue("@num", number);
                MySqlDataReader readeee = cmmm.ExecuteReader();
                string labExam11 = "";
                while (readeee.Read())
                {
                    labExam11 = readeee.GetString("labExam");
                    f1.lstLabExam.Items.Add(labExam11);
                }
                conn.Close();

                //Blood Chemistry
                conn.Open();
                string sql5 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                cmd5.Parameters.AddWithValue("@num", number);
                string Bfasting_blood_sugar = "";
                string Bblood_urea_nitrogen = "";
                string Bcreatinine = "";
                string Bcholesterol = "";
                string Btriglycerides = "";
                string Bhdl = "";
                string Bldl = "";
                string Bvldl = "";
                string Bblood_uric_acid = "";
                string Bsodium = "";
                string Bpotassium = "";
                string Bchloride = "";
                string Bsgpt_alt = "";
                string Bsgot_ast = "";
                string Bmedtech = "";
                string Bpathologist = "";
                string Bdaterequested = "";
                string Bdateexamined = "";
                MySqlDataReader reader5 = cmd5.ExecuteReader();
                while (reader5.Read())
                {
                    Bfasting_blood_sugar = reader5.GetString("fasting_blood_sugar");
                    Bblood_urea_nitrogen = reader5.GetString("blood_urea_nitrogen");
                    Bcreatinine = reader5.GetString("creatinine");
                    Bcholesterol = reader5.GetString("cholesterol");
                    Btriglycerides = reader5.GetString("triglycerides");
                    Bhdl = reader5.GetString("hdl");
                    Bldl = reader5.GetString("ldl");
                    Bvldl = reader5.GetString("vldl");
                    Bblood_uric_acid = reader5.GetString("blood_uric_acid");
                    Bsodium = reader5.GetString("sodium");
                    Bpotassium = reader5.GetString("potassium");
                    Bchloride = reader5.GetString("chloride");
                    Bsgpt_alt = reader5.GetString("sgpt_alt");
                    Bsgot_ast = reader5.GetString("sgot_ast");
                    Bmedtech = reader5.GetString("medtech");
                    Bpathologist = reader5.GetString("pathologists");
                    Bdaterequested = reader5.GetString("daterequested");
                    Bdateexamined = reader5.GetString("dateexamined");
                }
                conn.Close();
                f1.dtDateRequested.Text = Bdaterequested;
                f1.dtDateExamined.Text = Bdateexamined;
                f1.lblFastingBlood.Text = Bfasting_blood_sugar;
                f1.lblBloodUrea.Text = Bblood_urea_nitrogen;
                f1.lblCreatinineBlood.Text = Bcreatinine;
                f1.lblCholesterolBlood.Text = Bcholesterol;
                f1.lblTriglyBlood.Text = Btriglycerides;
                f1.lblHDLBlood.Text = Bhdl;
                f1.lblLDLBlood.Text = Bldl;
                f1.lblVDLBlood.Text = Bvldl;
                f1.lblBloodUric.Text = Bblood_uric_acid;
                f1.lblSodiumBlood.Text = Bsodium;
                f1.lblPotassiumBlood.Text = Bpotassium;
                f1.lblChlorideBlood.Text = Bchloride;
                f1.lblSGPTBlood.Text = Bsgpt_alt;
                f1.lblSGOTBlood.Text = Bsgot_ast;
                f1.lblMedTechBloodChem.Text = Bmedtech;
                f1.lblPathologistBloodChem.Text = Bpathologist;

                //Fecalysis
                conn.Open();
                string sql6 = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
                MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
                cmd6.Parameters.AddWithValue("@num", number);
                string Fcolor = "";
                string Fcharacters = "";
                string Freaction = "";
                string Foccult_blood = "";
                string Fpus_cells = "";
                string Frbc = "";
                string Ffat_globules = "";
                string Fmacrophage = "";
                string Fbacteria = "";
                string Fparasites_or_ova = "";
                string Fmedtech = "";
                string Fpathologist = "";
                string Fdaterequested = "";
                string Fdateexamined = "";
                MySqlDataReader reader6 = cmd6.ExecuteReader();
                while (reader6.Read())
                {
                    Fcolor = reader6.GetString("color");
                    Fcharacters = reader6.GetString("characters");
                    Freaction = reader6.GetString("reaction");
                    Foccult_blood = reader6.GetString("occult_blood");
                    Fmedtech = reader6.GetString("medtech");
                    Fpathologist = reader6.GetString("pathologists");
                    Fdaterequested = reader6.GetString("daterequested");
                    Fdateexamined = reader6.GetString("dateexamined");
                }
                conn.Close();

                conn.Open();
                string sql7 = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd7 = new MySqlCommand(sql7, conn);
                cmd7.Parameters.AddWithValue("@num", number);
                MySqlDataReader reader7 = cmd7.ExecuteReader();
                while (reader7.Read())
                {
                    Fpus_cells = reader7.GetString("pus_cells");
                    Frbc = reader7.GetString("rbc");
                    Ffat_globules = reader7.GetString("fat_globules");
                    Fmacrophage = reader7.GetString("macrophage");
                    Fbacteria = reader7.GetString("bacteria");
                    Fparasites_or_ova = reader7.GetString("parasites_or_ova");
                }
                conn.Close();
                f1.lblColorFeca.Text = Fcolor;
                f1.lblCharacterFeca.Text = Fcharacters;
                f1.lblReactionFeca.Text = Freaction;
                f1.lblOccultFeca.Text = Foccult_blood;
                f1.lblPluesFeca.Text = Fpus_cells;
                f1.lblRBCFeca.Text = Frbc;
                f1.lblFlatFeca.Text = Ffat_globules;
                f1.lblMacroFeca.Text = Fmacrophage;
                f1.lblBacteriaFeca.Text = Fbacteria;
                f1.lblParasitesFeca.Text = Fparasites_or_ova;
                f1.lblMedTechFecalysis.Text = Fmedtech;
                f1.lblPathologistsFecalysis.Text = Fpathologist;

                //Hematology
                conn.Open();
                string sql8 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
                MySqlCommand cmd8 = new MySqlCommand(sql8, conn);
                cmd8.Parameters.AddWithValue("@num", number);
                string Hhemoglobin = "";
                string Hhematocrit = "";
                string Hwbc_count = "";
                string Hrbc_count = "";
                string Hplatelet = "";
                string Hbleeding_time = "";
                string Hclotting_time = "";
                string Habo_group = "";
                string Hsegmenters = "";
                string Hlymphocytes = "";
                string Hmonocytes = "";
                string Heosinophils = "";
                string Hbasophils = "";
                string Hstab = "";
                string Hothers = "";
                string Hmedtech = "";
                string Hpathologist = "";
                string Hdaterequested = "";
                string Hdateexamined = "";
                MySqlDataReader reader8 = cmd8.ExecuteReader();
                while (reader8.Read())
                {
                    Hhemoglobin = reader8.GetString("hemoglobin");
                    Hhematocrit = reader8.GetString("hematocrit");
                    Hwbc_count = reader8.GetString("wbc_count");
                    Hrbc_count = reader8.GetString("rbc_count");
                    Hplatelet = reader8.GetString("platelet");
                    Hbleeding_time = reader8.GetString("bleeding_time");
                    Hclotting_time = reader8.GetString("clotting_time");
                    Habo_group = reader8.GetString("abo_group");
                    Hsegmenters = reader8.GetString("segmenters");
                    Hlymphocytes = reader8.GetString("lymphocytes");
                    Hmonocytes = reader8.GetString("monocytes");
                    Heosinophils = reader8.GetString("eosinophils");
                    Hbasophils = reader8.GetString("basophils");
                    Hstab = reader8.GetString("stab");
                    Hothers = reader8.GetString("others");
                    Hmedtech = reader8.GetString("medtech");
                    Hpathologist = reader8.GetString("pathologists");
                    Hdaterequested = reader8.GetString("daterequested");
                    Hdateexamined = reader8.GetString("dateexamined");
                }
                conn.Close();
                f1.lblHemoglobin.Text = Hhemoglobin;
                f1.lblHematocrit.Text = Hhematocrit;
                f1.lblWBC.Text = Hwbc_count;
                f1.lblRBC.Text = Hrbc_count;
                f1.lblPlatelet.Text = Hplatelet;
                f1.lblBleeding.Text = Hbleeding_time;
                f1.lblClotting.Text = Hclotting_time;
                f1.lblABO.Text = Habo_group;
                f1.lblSegmenters.Text = Hsegmenters;
                f1.lblLymphocytes.Text = Hlymphocytes;
                f1.lblMonocytes.Text = Hmonocytes;
                f1.lblEosinophils.Text = Heosinophils;
                f1.lblBasophils.Text = Hbasophils;
                f1.lblStab.Text = Hstab;
                f1.lblOthers.Text = Hothers;
                f1.lblMedTechHematology.Text = Hmedtech;
                f1.lblPathologistHematology.Text = Hpathologist;

                //Urinalysis
                conn.Open();
                string sql9 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
                MySqlCommand cmd9 = new MySqlCommand(sql9, conn);
                cmd9.Parameters.AddWithValue("@num", number);
                string Uricolor = "";
                string Uricharacters = "";
                string Uriprotein = "";
                string Urisugar = "";
                string Uriph = "";
                string UrispGr = "";
                string UripregnancyTest = "";
                string Urimedtech = "";
                string Uripathologist = "";
                string Uridaterequested = "";
                string Uridateexamined = "";
                MySqlDataReader reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {
                    Uricolor = reader9.GetString("color");
                    Uricharacters = reader9.GetString("characters");
                    Uriprotein = reader9.GetString("protein");
                    Urisugar = reader9.GetString("sugar");
                    Uriph = reader9.GetString("ph");
                    UrispGr = reader9.GetString("spGr");
                    UripregnancyTest = reader9.GetString("pregnancyTest");
                    Urimedtech = reader9.GetString("medtech");
                    Uripathologist = reader9.GetString("pathologists");
                    Uridaterequested = reader9.GetString("daterequested");
                    Uridateexamined = reader9.GetString("dateexamined");
                }
                conn.Close();

                conn.Open();
                string sql10 = "SELECT * FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd10 = new MySqlCommand(sql10, conn);
                cmd10.Parameters.AddWithValue("@num", number);
                string Uripus_cells = "";
                string Urirbc = "";
                string Uriepith_cells = "";
                string Uribacteria = "";
                string Urimucus_thread = "";
                string Uriamorphous_urates = "";
                string Uricasts = "";
                string Uricrystals = "";
                string Uriothers = "";
                MySqlDataReader reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {
                    Uripus_cells = reader10.GetString("pus_cells");
                    Urirbc = reader10.GetString("rbc");
                    Uriepith_cells = reader10.GetString("epith_cells");
                    Uribacteria = reader10.GetString("bacteria");
                    Urimucus_thread = reader10.GetString("mucus_thread");
                    Uriamorphous_urates = reader10.GetString("amorphous_urates");
                    Uricasts = reader10.GetString("casts");
                    Uricrystals = reader10.GetString("crystals");
                    Uriothers = reader10.GetString("others");
                }
                conn.Close();
                f1.lblColorUri.Text = Uricolor;
                f1.lblCharacterUri.Text = Uricharacters;
                f1.lblProteinUri.Text = Uriprotein;
                f1.lblSugarUri.Text = Urisugar;
                f1.lblPHUri.Text = Uriph;
                f1.lblSpUri.Text = UrispGr;
                f1.lblPregUri.Text = UripregnancyTest;
                f1.lblPUSUri.Text = Uripus_cells;
                f1.lblRBCUri.Text = Urirbc;
                f1.lblEpithUri.Text = Uriepith_cells;
                f1.lblBacteriaUri.Text = Uribacteria;
                f1.lblMucusUri.Text = Urimucus_thread;
                f1.lblAmorphousUri.Text = Uriamorphous_urates;
                f1.lblCastsUri.Text = Uricasts;
                f1.lblCrystalsUri.Text = Uricrystals;
                f1.lblOthersUri.Text = Uriothers;
                f1.lblMedTechUrinalysis.Text = Urimedtech;
                f1.lblPathologistUrinalysis.Text = Uripathologist;

                //Ultrasound
                conn.Open();
                string sql11 = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
                MySqlCommand cmd11 = new MySqlCommand(sql11, conn);
                cmd11.Parameters.AddWithValue("@num", number);
                string Upatientnum = "";
                string Utypeofexaminations = "";
                string Uresults = "";
                string Uimpression = "";
                string Usonologists = "";
                string Udaterequested = "";
                string Udateexamined = "";
                MySqlDataReader reader11 = cmd11.ExecuteReader();
                while (reader11.Read())
                {
                    Upatientnum = reader11.GetString("patientsNo");
                    Utypeofexaminations = reader11.GetString("typeofexaminations");
                    Uresults = reader11.GetString("result");
                    Uimpression = reader11.GetString("impression");
                    Usonologists = reader11.GetString("sonologists");
                    Udaterequested = reader11.GetString("daterequested");
                    Udateexamined = reader11.GetString("dateexamined");
                    f1.lblTypeUltra.Items.Add(Utypeofexaminations);
                }
                conn.Close();
                f1.lstXrayFindings.Text = Uresults;
                f1.lstXrayImpression.Text = Uimpression;
                f1.lblMedTechUltrasound.Text = Usonologists;

                //Xray
                conn.Open();
                string sql12 = "SELECT * FROM tbllaboratory_Xray WHERE patientsNo = @num";
                MySqlCommand cmd12 = new MySqlCommand(sql12, conn);
                cmd12.Parameters.AddWithValue("@num", number);
                string Xpatientnum = "";
                string Xtypeofexamination = "";
                string Xresults = "";
                string Ximpression = "";
                string Xsonologists = "";
                string Xdaterequested = "";
                string Xdateexamined = "";
                MySqlDataReader reader12 = cmd12.ExecuteReader();
                while (reader12.Read())
                {
                    Xpatientnum = reader12.GetString("patientsNo");
                    Xtypeofexamination = reader12.GetString("typeofexaminations");
                    Xresults = reader12.GetString("result");
                    Ximpression = reader12.GetString("impression");
                    Xsonologists = reader12.GetString("radiologists");
                    Xdaterequested = reader12.GetString("daterequested");
                    Xdateexamined = reader12.GetString("dateexamined");
                    f1.lblTypeXray.Items.Add(Xtypeofexamination);
                }
                conn.Close();
                f1.lstXrayFindings.Text = Xresults;
                f1.lstXrayImpression.Text = Ximpression;
                f1.lblMedTechXray.Text = Xsonologists;
                f1.docID = lblIDNumber.Text;
                f1.Show();
            }
        }

        private void tmrRefreshDGV_Tick(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            dS.Clear();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Doctor WHERE sentTo = '" + lblDoctor.Text + "' ", conn);
            Int32 countt = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = countt.ToString() + " records.";

            if (countt > count)
            {
                notifyIcon1.BalloonTipText = "New Patient received.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
            }

            conn.Open();
            string sql6 = "SELECT patientNo AS 'Patient No.', lastName AS 'Lastname', firstName AS 'Firstname', middleName AS 'Middlename', status AS 'Status', sentBy AS 'Sent By', sentTo AS 'Sent To', daterequested AS 'Date Requested' FROM tblNext_In_Line_Doctor WHERE sentTo LIKE '" + lblDoctor.Text + "' ";
            adapter = new MySqlDataAdapter(sql6, conn);
            adapter.Fill(dS, "tblNext_In_Line_Doctor");
            dgNextInLineDoctor.DataMember = "tblNext_In_Line_Doctor";
            dgNextInLineDoctor.DataSource = dS;
            conn.Close();

            dgNextInLineDoctor.Columns[0].Width = 125;
            dgNextInLineDoctor.Columns[1].Width = 175;
            dgNextInLineDoctor.Columns[2].Width = 175;
            dgNextInLineDoctor.Columns[3].Width = 175;
            dgNextInLineDoctor.Columns[4].Width = 100;
            dgNextInLineDoctor.Columns[5].Width = 100;
            dgNextInLineDoctor.Columns[6].Width = 200;
            dgNextInLineDoctor.Columns[7].Width = 100;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRetypePassword.Clear();
            txtRetypePassword.Enabled = false;
            btnCancel.Visible = false;
            btnChange.Enabled = true;
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }
    }
}
