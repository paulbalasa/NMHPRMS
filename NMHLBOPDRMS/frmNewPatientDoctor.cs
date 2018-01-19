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
using Microsoft.Reporting.WinForms;

namespace NMHLBOPDRMS
{
    public partial class frmNewPatientDoctor : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmNewPatientDoctor()
        {
            InitializeComponent();
        }

        public string docID = "";

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboStatus2.Text != "" && lstDiagnosis.Items.Count != 0)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    lstNotes.Items.Add(txtNotes.Text);

                    conn.Open();
                    string sql = "SELECT * FROM tblAccounts WHERE accountType = 'DOCTOR'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string idd = "";
                    while (reader.Read())
                    {
                        idd = reader.GetString("IDNumber");
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@id", idd);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    string LNdoctor = "";
                    string FNdoctor = "";
                    string MNdoctor = "";
                    while (reader2.Read())
                    {
                        LNdoctor = reader2.GetString("lastName");
                        FNdoctor = reader2.GetString("firstName");
                        MNdoctor = reader2.GetString("middleName");
                    }
                    conn.Close();

                    for (int x = 0; x < lstDiagnosis.Items.Count; x++)
                    {
                        conn.Open();
                        string sql3 = "INSERT INTO tblMedical_Diagnosis VALUES (@patientnum, @doctor, @status, @admissionStat, @diagnosis, @notes, @date)";
                        MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        cmd3.Parameters.AddWithValue("@patientnum", lblNumber.Text);
                        cmd3.Parameters.AddWithValue("@doctor", FNdoctor + " " + MNdoctor + " " + LNdoctor);
                        cmd3.Parameters.AddWithValue("@status", txtStatus1.Text);
                        cmd3.Parameters.AddWithValue("@admissionStat", cboStatus2.Text);
                        cmd3.Parameters.AddWithValue("@diagnosis", lstDiagnosis.Items[x].ToString());
                        cmd3.Parameters.AddWithValue("@notes", txtNotes.Text);
                        cmd3.Parameters.AddWithValue("@date", dtPicker.Value);
                        cmd3.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Statistical
                    statistical();

                    //Activity Log
                    conn.Open();
                    string sql5 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.Parameters.AddWithValue("@id", docID);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader3 = cmd5.ExecuteReader();
                    while (reader3.Read())
                    {
                        lname = reader3.GetString("lastname");
                        fname = reader3.GetString("firstname");
                        mname = reader3.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "DOCTOR";
                    string activityy = "SUCCESFULLLY SAVED MEDICAL DIAGNOSIS OF PATIENT (" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Medical Diagnosis successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEDIT.Enabled = false;
                    metroTabControl1.SelectedIndex = 4;
                    metroTabControl1.Refresh();
                }
                else
                {
                    MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (cboStatus2.Text == "")
                        cboStatus2.Focus();
                    else if (lstDiagnosis.Items.Count == 0)
                        cboWeeklyNotiable.Focus();
                }
            }
        }

        public void submitTreatment()
        {
            conn.Open();
            string sql = "INSERT INTO tblMedical_Treatment VALUES (@patientnum, @treatment, @doctor, @date); DELETE FROM tblNext_In_Line_Doctor WHERE patientNo = @patientnum; DELETE FROM tblDrafts_Nurse WHERE patientNo = @patientnum";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@patientnum", lblNumber.Text);
            cmd.Parameters.AddWithValue("@treatment", txtTreatment.Text);
            cmd.Parameters.AddWithValue("@doctor", lblMedicalDoctor.Text);
            cmd.Parameters.AddWithValue("@date", dtPicker.Value);
            cmd.ExecuteNonQuery();
            conn.Close();

            //Lab1
            conn.Open();
            string s = "SELECT * FROM tblMedical_Laboratory_1 WHERE patientsNo = @patientNo";
            MySqlCommand c = new MySqlCommand(s, conn);
            c.Parameters.AddWithValue("@patientNo", lblNumber.Text);
            MySqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                string labExam = dr.GetString("labExam");
                lstLab1.Items.Add(labExam);
            }
            conn.Close();

            for (int z = 0; z < lstLab1.Items.Count; z++)
            {
                conn.Open();
                string s1 = "INSERT INTO tblArchive_Medical_Laboratory_1 VALUES (@patientNo, @labexam); DELETE FROM tblMedical_Laboratory_1 WHERE patientsNo = @patientNo";
                MySqlCommand c1 = new MySqlCommand(s1, conn);
                c1.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                c1.Parameters.AddWithValue("@labexam", lstLab1.Items[z]);
                c1.ExecuteNonQuery();
                conn.Close();
            }

            //LabUltrasound
            conn.Open();
            string s2 = "SELECT * FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @patientNo";
            MySqlCommand c2 = new MySqlCommand(s2, conn);
            c2.Parameters.AddWithValue("@patientNo", lblNumber.Text);
            MySqlDataReader dr2 = c2.ExecuteReader();
            while (dr2.Read())
            {
                string labExam = dr2.GetString("labExam");
                lstUltrasound.Items.Add(labExam);
            }
            conn.Close();

            for (int h = 0; h < lstUltrasound.Items.Count; h++)
            {
                conn.Open();
                string s3 = "INSERT INTO tblArchive_Medical_Laboratory_Ultrasound VALUES (@patientNo, @labexam); DELETE FROM tblMedical_Laboratory_Ultrasound WHERE patientsNo = @patientNo";
                MySqlCommand c3 = new MySqlCommand(s3, conn);
                c3.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                c3.Parameters.AddWithValue("@labexam", lstUltrasound.Items[h]);
                c3.ExecuteNonQuery();
                conn.Close();
            }

            //LabXray
            conn.Open();
            string s4 = "SELECT * FROM tblMedical_Laboratory_Xray WHERE patientsNo = @patientNo";
            MySqlCommand c4 = new MySqlCommand(s4, conn);
            c4.Parameters.AddWithValue("@patientNo", lblNumber.Text);
            MySqlDataReader dr4 = c4.ExecuteReader();
            while (dr4.Read())
            {
                string labExam = dr4.GetString("labExam");
                lstXray.Items.Add(labExam);
            }
            conn.Close();

            for (int k = 0; k < lstXray.Items.Count; k++)
            {
                conn.Open();
                string s5 = "INSERT INTO tblArchive_Medical_Laboratory_Xray VALUES (@patientNo, @labexam); DELETE FROM tblMedical_Laboratory_Xray WHERE patientsNo = @patientNo";
                MySqlCommand c5 = new MySqlCommand(s5, conn);
                c5.Parameters.AddWithValue("@patientNo", lblNumber.Text);
                c5.Parameters.AddWithValue("@labexam", lstXray.Items[k]);
                c5.ExecuteNonQuery();
                conn.Close();
            }

            //Activity Log
            conn.Open();
            string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
            cmd3.Parameters.AddWithValue("@id", docID);
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

            string acctype = "DOCTOR";
            string activityy = "SUCCESSFULLY SAVED MEDICAL TREATMENT OF PATIENT(" + txtFname.Text + " " + txtMname.Text + " " + txtLname.Text + ").";

            conStr.activityLog(acctype, fname, mname, lname, activityy);
        }

        public void close()
        {
            frmMainDoctor f1 = new frmMainDoctor();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblAccounts WHERE accountType = 'DOCTOR' ";
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
            string position = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lname = reader1.GetString("lastname");
                fname = reader1.GetString("firstname");
                mname = reader1.GetString("middlename");
                birthday = reader1.GetString("birthday");
                position = reader1.GetString("doctorPosition");

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
            
            f1.lblNameOfDoctor.Text = lname + ", " + fname + " " + mname;
            f1.txtIDNumber.Text = id;
            f1.txtUsername.Text = username;
            f1.txtPassword.Text = password;
            f1.lblIDNumber.Text = id;
            f1.txtLastName.Text = lname;
            f1.txtFirstName.Text = fname;
            f1.txtMiddleName.Text = mname;
            f1.dtBirthday.Text = birthday;
            f1.lblDoctor.Text = "Dr. " + fname + " " + mname + " " + lname + " (" + position + ")";
            f1.Show();
        }

        private void btnSubmitTreatment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (txtTreatment.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    submitTreatment();

                    MessageBox.Show("Patient's Medical Treatment succesfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSubmitTreatment.Enabled = false;

                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        Application.OpenForms[i].Hide();
                    }
                    close();
                }
                else
                {
                    MessageBox.Show("Empty field not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTreatment.Focus();
                }
            }
        }

        private void cboLabResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLabResults.Text == "Blood Chemistry")
            {
                grpBloodChem.Show();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboLabResults.Text == "Fecalysis")
            {
                grpFecalysis.Show();
                grpBloodChem.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboLabResults.Text == "Hematology")
            {
                grpHematology.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboLabResults.Text == "Urinalysis")
            {
                grpUrinalysis.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboLabResults.Text == "Ultrasound")
            {
                grpUltrasound.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpXray.Hide();
            }
            else if (cboLabResults.Text == "X-ray")
            {
                grpXray.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
            }
        }

        private void frmNewPatientDoctor_Load(object sender, EventArgs e)
        {
            string time = "";
            time = DateTime.Now.ToString("MMMM");
            lblMonth.Text = time;

            conn.Open();
            string sqlll = "SELECT * FROM tblstatdisease";
            MySqlCommand cm = new MySqlCommand(sqlll, conn);
            string disease = "";
            MySqlDataReader rd = cm.ExecuteReader();
            while (rd.Read())
            {
                disease = rd.GetString("disease");
                lstDisease.Items.Add(disease);
            }
            conn.Close();

            conn.Open();
            string sqll = "SELECT * FROM tblWeekly_Notifiable";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            string notifiable = "";
            MySqlDataReader readerr = cmdd.ExecuteReader();
            while (readerr.Read())
            {
                notifiable = readerr.GetString("weeklyNotifiable");
                cboWeeklyNotiable.Items.Add(notifiable);
            }
            conn.Close();

            //Nurse
            conn.Open();
            string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE'";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string id = "";
            while (reader1.Read())
            {
                id = reader1.GetString("IDNumber");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            string LNnurse = "";
            string FNnurse = "";
            string MNnurse = "";
            while (reader2.Read())
            {
                LNnurse = reader2.GetString("lastName");
                FNnurse = reader2.GetString("firstName");
                MNnurse = reader2.GetString("middleName");
            }
            conn.Close();
            lblMedicalNurse.Text = FNnurse + " " + MNnurse + " " + LNnurse;

            //Doctor
            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", docID);
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            string LNdoctor = "";
            string FNdoctor = "";
            string MNdoctor = "";
            while (reader4.Read())
            {
                LNdoctor = reader4.GetString("lastName");
                FNdoctor = reader4.GetString("firstName");
                MNdoctor = reader4.GetString("middleName");
            }
            conn.Close();
            lblMedicalDoctor.Text = "Dr. " + FNdoctor + " " + MNdoctor + " " + LNdoctor;

            //Medical Technologists
            conn.Open();
            string sql6 = "SELECT * FROM tblAccount_Profile WHERE accountType = 'LABORATORY 1' ";
            MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
            string lname = "";
            string fname = "";
            string mname = "";
            MySqlDataReader reader6 = cmd6.ExecuteReader();
            while (reader6.Read())
            {
                lname = reader6.GetString("lastName");
                fname = reader6.GetString("firstName");
                mname = reader6.GetString("middleName");
            }
            conn.Close();
            lblMedTechBloodChem.Text = fname + " " + mname + " " + lname;
            lblMedTechFecalysis.Text = fname + " " + mname + " " + lname;
            lblMedTechHematology.Text = fname + " " + mname + " " + lname;
            lblMedTechUltrasound.Text = fname + " " + mname + " " + lname;
            lblMedTechUrinalysis.Text = fname + " " + mname + " " + lname;
            lblMedTechXray.Text = fname + " " + mname + " " + lname;

            //Pathologists
            conn.Open();
            string sql8 = "SELECT * FROM tblStaff WHERE staff = 'PATHOLOGISTS' ";
            MySqlCommand cmd8 = new MySqlCommand(sql8, conn);
            string lnamep = "";
            string fnamep = "";
            string mnamep = "";
            string degreep = "";
            MySqlDataReader reader8 = cmd8.ExecuteReader();
            while (reader8.Read())
            {
                lnamep = reader8.GetString("lastName");
                fnamep = reader8.GetString("firstName");
                mnamep = reader8.GetString("middleInitial");
                degreep = reader8.GetString("degree");
            }
            conn.Close();
            lblPathologistBloodChem.Text = fnamep + " " + mnamep + " " + lnamep + " " + degreep;
            lblPathologistHematology.Text = fnamep + " " + mnamep + " " + lnamep + " " + degreep;
            lblPathologistsFecalysis.Text = fnamep + " " + mnamep + " " + lnamep + " " + degreep;
            lblPathologistUrinalysis.Text = fnamep + " " + mnamep + " " + lnamep + " " + degreep;

            //Sonologists
            conn.Open();
            string sql9 = "SELECT * FROM tblAccount_Profile WHERE accountType = 'LABORATORY 2' ";
            MySqlCommand cmd9 = new MySqlCommand(sql9, conn);
            string lnames = "";
            string fnames = "";
            string mnames = "";
            MySqlDataReader reader9 = cmd9.ExecuteReader();
            while (reader9.Read())
            {
                lnames = reader9.GetString("lastName");
                fnames = reader9.GetString("firstName");
                mnames = reader9.GetString("middleName");
            }
            conn.Close();
            lblMedTechUltrasound.Text = fnames + " " + mnames + " " + lnames;

            //Radiologists
            conn.Open();
            string sql10 = "SELECT * FROM tblStaff WHERE staff = 'RADIOLOGISTS' ";
            MySqlCommand cmd10 = new MySqlCommand(sql10, conn);
            string lnamess = "";
            string fnamess = "";
            string mnamess = "";
            MySqlDataReader reader10 = cmd10.ExecuteReader();
            while (reader10.Read())
            {
                lnames = reader10.GetString("lastName");
                fnames = reader10.GetString("firstName");
                mnames = reader10.GetString("middleInitial");
            }
            conn.Close();
            lblMedTechXray.Text = fnamess + " " + mnamess + " " + lnamess;

            grpBloodChem.Hide();
            grpFecalysis.Hide();
            grpHematology.Hide();
            grpUltrasound.Hide();
            grpUrinalysis.Hide();
            grpXray.Hide();

            dtPicker.Text = DateTime.Now.ToShortDateString();
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblTypeXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblTypeXray.Text == "ANKLE (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'ANKLE (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "APICO LORDITIC (AP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'APICO LORDITIC (AP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "ARM (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'ARM (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "CALDWELL VIEW (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'CALDWELL VIEW (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "CHEST")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'CHEST' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "CHEST (CHILD APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'CHEST (CHILD APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "CLAVICLE (AP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'CLAVICLE (AP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "ELBOW (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'ELBOW (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "FACIAL (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'FACIAL (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "FETHOGRAPHY (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'FETHOGRAPHY (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "FINGER (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'FINGER (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "FOOT (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'FOOT (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "FOREARM (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'FOREARM (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "HAND (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'HAND (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "HIP (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'HIP (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "IVP (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'IVP (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "L AND R DECUBIUTS (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'L AND R DECUBIUTS (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "LEG (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'LEG (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "LUMBO SACRAL (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'LUMBO SACRAL (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "MANDIBLE (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'MANDIBLE (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "MASTOID (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'MASTOID (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "NASAL BONE (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'NASAL BONE (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "NECK (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'NECK (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PELVIS (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PELVIS (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PELVITMERY (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PELVITMERY (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PLAIN ABD (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PLAIN ABD (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PLAIN ABD (RIGHT AND SUP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PLAIN ABD (RIGHT AND SUP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PLAIN KUB (AP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PLAIN KUB (AP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "PNS (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'PNS (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "RIB CAGE (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'RIB CAGE (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "SCAPULA (AP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'SCAPULA (AP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "SHOULDER (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'SHOULDER (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "SKULL (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'SKULL (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "SPINE (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'SPINE (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "STERNUM (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'STERNUM (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "STL (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'STL (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "THIGH (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'THIGH (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "THORARIC RAGE (AP)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'THORARIC RAGE (AP)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "THORACO LUMBAR (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'THORACO LUMBAR (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "TOES (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'TOES (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "WATERS VIEW (APL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'WATERS VIEW (APL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeXray.Text == "WRIST (APL/APO)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstXrayImpression.Items.Clear();
                lstXrayFindings.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Xray WHERE typeofexaminations = 'WRIST (APL/APO)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstXrayImpression.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstXrayFindings.Items.Add(result);
                }
                conn.Close();
            }
        }

        private void lblTypeUltra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblTypeUltra.Text == "WHOLE ABDOMEN")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'WHOLE ABDOMEN' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "LOWER/UPPER ABDOMEN")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'LOWER/UPPER ABDOMEN' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "HBT")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'HBT' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "KUB")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'KUB' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "KUB WITH PROSTATE")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'KUB WITH PROSTATE' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "PELVIC")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'PELVIC' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "BPS")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'BPS' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "TVS (TRANSVAGINAL)")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'TVS (TRANSVAGINAL)' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "TRANSRECTAL")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'TRANSRECTAL' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "SCROTAL")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'SCROTAL' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "INGUINO/SCROTAL BILATERAL")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'INGUINO/SCROTAL BILATERAL' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "BREAST")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'BREAST' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "THRYROID")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'THRYROID' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "CHEST")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'CHEST' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "CRANIAL")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'CRANIAL' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
            else if (lblTypeUltra.Text == "MASS")
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lstUltraImpressions.Items.Clear();
                lstUltrasoundResult.Items.Clear();
                conn.Open();
                string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE typeofexaminations = 'MASS' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                string impressions = "";
                string result = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    impressions = reader.GetString("impression");
                    lstUltraImpressions.Items.Add(impressions);
                    result = reader.GetString("result");
                    lstUltrasoundResult.Items.Add(result);
                }
                conn.Close();
            }
        }

        private void lblBackNurse_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            lbl3.Visible = false;
            txtOtherWeeklyNotifiable.Visible = false;
            btnUndo.Visible = false;
            cboWeeklyNotiable.Text = "";
            cboWeeklyNotiable.Enabled = true;
            txtOtherWeeklyNotifiable.Clear();
            btnAddOtherWeeklyNotifiable.Visible = false;
        }

        private void cboWeeklyNotiable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWeeklyNotiable.Text != "")
            {
                if (cboWeeklyNotiable.Text != "ADD (+)")
                {
                    if (!lstDiagnosis.Items.Contains(cboWeeklyNotiable.Text))
                    {
                        lstDiagnosis.Items.Add(cboWeeklyNotiable.SelectedItem);
                        lstDiagnosisCopy.Items.Add(cboWeeklyNotiable.SelectedItem);
                    }
                }
                else
                {
                    btnAddOtherWeeklyNotifiable.Visible = true;
                    cboWeeklyNotiable.Enabled = false;
                    lbl3.Visible = true;
                    txtOtherWeeklyNotifiable.Visible = true;
                    txtOtherWeeklyNotifiable.Focus();
                    btnUndo.Visible = true;
                }
            }
        }

        private void btnAddOtherWeeklyNotifiable_Click(object sender, EventArgs e)
        {
            if (txtOtherWeeklyNotifiable.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to add '" + txtOtherWeeklyNotifiable.Text + "' to Weekly Notifiable?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblWeekly_Notifiable", conn);
                        Int32 countt = Convert.ToInt32(cmd1.ExecuteScalar());
                        conn.Close();

                        conn.Open();
                        string sql = "INSERT INTO tblWeekly_Notifiable VALUES (@id, @weeklyNotiable)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", countt = countt + 1);
                        cmd.Parameters.AddWithValue("@weeklyNotiable", txtOtherWeeklyNotifiable.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                        MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
                        cmd3.Parameters.AddWithValue("@id", docID);
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

                        string acctype = "DOCTOR";
                        string activityy = "SUCCESSFULLY ADDED '" + txtOtherWeeklyNotifiable.Text + "' TO WEEKLY NOTIFIABLE.";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        cboWeeklyNotiable.Items.Add(txtOtherWeeklyNotifiable.Text);
                        cboWeeklyNotiable.Text = "";
                        txtOtherWeeklyNotifiable.Clear();
                        cboWeeklyNotiable.Text = "";
                        cboWeeklyNotiable.Enabled = true;
                        lbl3.Visible = false;
                        txtOtherWeeklyNotifiable.Visible = false;
                        btnUndo.Visible = false;
                        btnAddOtherWeeklyNotifiable.Visible = false;
                        cboWeeklyNotiable.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("'" + txtOtherWeeklyNotifiable.Text + "' already added in Weekly Notifiable.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    txtOtherWeeklyNotifiable.SelectAll();
                    txtOtherWeeklyNotifiable.Focus();
                }
            }
            else
            {
                MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOtherWeeklyNotifiable.Focus();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string x = lstDiagnosis.SelectedItem.ToString();
            lstDiagnosis.Items.Remove(x);
            lstDiagnosisCopy.Items.Remove(x);
            btnRemove.Enabled = false;
        }

        public string number = "";
        public string status = "";
        public string sentby = "";

        private void btnLabExam_Click(object sender, EventArgs e)
        {
            string thisForm = this.Name;
            frmListLaboratory f1 = new frmListLaboratory();
            f1.formName = thisForm;
            f1.lblNumber.Text = lblNumber.Text;
            f1.lblStatus.Text = txtStatus1.Text;
            f1.stat = cboStatus2.Text;
            f1.notes = txtNotes.Text;
            for (int x = 0; x < lstDiagnosis.Items.Count; x++)
            {
                f1.lstDiagnosis.Items.Add(lstDiagnosis.Items[x]);
            }
            f1.number = number;
            f1.status = status;
            f1.sentby = sentby;
            f1.docID = docID;
            f1.Show();
        }

        private void lstDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDiagnosis.SelectedItem != null)
                btnRemove.Enabled = true;
        }

        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            if (txtAddDiagnosis.Text != "")
            {
                lstDiagnosis.Items.Add(txtAddDiagnosis.Text);
                lstDiagnosisCopy.Items.Add(txtAddDiagnosis.Text);
                txtAddDiagnosis.Clear();
            }
            else
            {
                MessageBox.Show("Empty field not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddDiagnosis.Focus();
            }
        }

        private void txtOtherWeeklyNotifiable_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherWeeklyNotifiable.Text != "")
                btnAddOtherWeeklyNotifiable.Enabled = true;
            else
                btnAddOtherWeeklyNotifiable.Enabled = false;
        }

        public void statistical()
        {
            string date = DateTime.Now.ToShortDateString();
            var year = DateTime.Parse(date).Year;
            int age = Convert.ToInt32(txtAge.Text);
            List<string> mo = new List<string>();

            conn.Open();
            string sql = "SELECT DISTINCT(month) FROM tblstatdisease";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string m = "";
            while (reader.Read())
            {
                m = reader.GetString("month");
                mo.Add(m);
            }

            if (!mo.Contains("January") || !mo.Contains("February") || !mo.Contains("March") || !mo.Contains("April") || !mo.Contains("May") || !mo.Contains("June") || !mo.Contains("July") || !mo.Contains("August") || !mo.Contains("September") || !mo.Contains("October") || !mo.Contains("November") || !mo.Contains("December"))
            {
                if (lblMonth.Text == "February")
                {
                    string[] month = new string[2];
                    month[0] = "January";
                    month[1] = "February";

                    string[] datee = new string[2];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";

                    for (int z = 0; z < 2; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "March")
                {
                    string[] month = new string[3];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";

                    string[] datee = new string[3];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";

                    for (int z = 0; z < 3; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "April")
                {
                    string[] month = new string[4];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";

                    string[] datee = new string[4];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";

                    for (int z = 0; z < 4; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "May")
                {
                    string[] month = new string[5];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";

                    string[] datee = new string[5];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";

                    for (int z = 0; z < 5; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "June")
                {
                    string[] month = new string[6];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";

                    string[] datee = new string[6];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";

                    for (int z = 0; z < 6; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "July")
                {
                    string[] month = new string[7];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";

                    string[] datee = new string[7];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";

                    for (int z = 0; z < 7; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "August")
                {
                    string[] month = new string[8];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";
                    month[7] = "August";

                    string[] datee = new string[8];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";
                    datee[7] = year + "-08-01";

                    for (int z = 0; z < 8; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "September")
                {
                    string[] month = new string[9];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";
                    month[7] = "August";
                    month[8] = "September";

                    string[] datee = new string[9];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";
                    datee[7] = year + "-08-01";
                    datee[8] = year + "-09-01";

                    for (int z = 0; z < 9; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "October")
                {
                    string[] month = new string[10];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";
                    month[7] = "August";
                    month[8] = "September";
                    month[9] = "October";

                    string[] datee = new string[10];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";
                    datee[7] = year + "-08-01";
                    datee[8] = year + "-09-01";
                    datee[9] = year + "-10-01";

                    for (int z = 0; z < 10; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "November")
                {
                    string[] month = new string[11];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";
                    month[7] = "August";
                    month[8] = "September";
                    month[9] = "October";
                    month[10] = "November";

                    string[] datee = new string[11];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";
                    datee[7] = year + "-08-01";
                    datee[8] = year + "-09-01";
                    datee[9] = year + "-10-01";
                    datee[10] = year + "-11-01";

                    for (int z = 0; z < 11; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (lblMonth.Text == "December")
                {
                    string[] month = new string[12];
                    month[0] = "January";
                    month[1] = "February";
                    month[2] = "March";
                    month[3] = "April";
                    month[4] = "May";
                    month[5] = "June";
                    month[6] = "July";
                    month[7] = "August";
                    month[8] = "September";
                    month[9] = "October";
                    month[10] = "November";
                    month[11] = "December";

                    string[] datee = new string[12];
                    datee[0] = year + "-01-01";
                    datee[1] = year + "-02-01";
                    datee[2] = year + "-03-01";
                    datee[3] = year + "-04-01";
                    datee[4] = year + "-05-01";
                    datee[5] = year + "-06-01";
                    datee[6] = year + "-07-01";
                    datee[7] = year + "-08-01";
                    datee[8] = year + "-09-01";
                    datee[9] = year + "-10-01";
                    datee[10] = year + "-11-01";
                    datee[11] = year + "-12-01";

                    for (int z = 0; z < 12; z++)
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (!lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlState = "INSERT INTO tblstatdisease VALUES (@month, @year, @disease, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, @date)";
                                MySqlCommand cm = new MySqlCommand(sqlState, conn);
                                cm.Parameters.AddWithValue("@month", month[z]);
                                cm.Parameters.AddWithValue("@year", year);
                                cm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cm.Parameters.AddWithValue("@date", datee[z]);
                                cm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            }
            
            lstDisease.Items.Clear();

            conn.Open();
            string sqlll = "SELECT * FROM tblstatdisease";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            string disease = "";
            MySqlDataReader readerrr = cmddd.ExecuteReader();
            while (readerrr.Read())
            {
                disease = readerrr.GetString("disease");
                lstDisease.Items.Add(disease);
            }
            conn.Close();

            if (age <= 18)
            {
                if (age < 1)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, L1M = L1M + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, L1F = L1F + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 1 && age <= 4)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, OM = OM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, OF = OF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 5 && age <= 9)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, FM = FM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, FF = FF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 10 && age <= 14)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, TM = TM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, TF = TF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 15 && age <= 18)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, FFM = FFM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, pedia = pedia + 1, FFF = FFF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            }
            else if (age > 18)
            {
                if (age == 19)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, FFM = FFM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, FFF = FFF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 20 && age <= 44)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, TWM = TWM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, TWF = TWF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 45 && age <= 64)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, FFFM = FFFM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, FFFF = FFFF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                else if (age >= 65)
                {
                    if (cboSex.Text == "MALE")
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, GSFM = GSFM + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < lstDiagnosis.Items.Count; a++)
                        {
                            if (lstDisease.Items.Contains(lstDiagnosis.Items[a]))
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                                conn.Open();
                                string sqlStatement = "UPDATE tblstatdisease SET count = count + 1, adult = adult + 1, GSFF = GSFF + 1 WHERE disease = @disease AND month = @month AND year = @year";
                                MySqlCommand cmm = new MySqlCommand(sqlStatement, conn);
                                cmm.Parameters.AddWithValue("@disease", lstDiagnosis.Items[a]);
                                cmm.Parameters.AddWithValue("@month", lblMonth.Text);
                                cmm.Parameters.AddWithValue("@year", year);
                                cmm.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            }
        }

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save and print?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (txtTreatment.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    submitTreatment();

                    MessageBox.Show("Patient's Medical Treatment succesfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSubmitTreatment.Enabled = false;

                    frmPrintTreatment f1 = new frmPrintTreatment();
                    ReportParameter param = new ReportParameter("Treatment", txtTreatment.Text);
                    f1.reportViewer1.LocalReport.SetParameters(param);
                    param = new ReportParameter("doctor", lblMedicalDoctor.Text);
                    f1.reportViewer1.LocalReport.SetParameters(param);
                    param = new ReportParameter("date", DateTime.Now.ToShortDateString());
                    f1.reportViewer1.LocalReport.SetParameters(param);

                    f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    f1.reportViewer1.DocumentMapCollapsed = true;
                    f1.reportViewer1.RefreshReport();
                    f1.reportViewer1.ZoomMode = ZoomMode.Percent;
                    f1.reportViewer1.ZoomPercent = 75;

                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Empty field not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTreatment.Focus();
                }
            }
        }
    }
}
