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
    public partial class frmListLaboratory : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmListLaboratory()
        {
            InitializeComponent();
        }

        public string docID = "";

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Hide();
        }

        private void btnSendLaboratory1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you you want to send to Laboratory 1?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (lstLaboratory1.Items.Count != 0)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @number";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@number", lblNumber.Text);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lname = reader.GetString("lastName");
                        fname = reader.GetString("firstName");
                        mname = reader.GetString("middleName");
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblNext_In_Line_Laboratory1";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int x = 0;
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        x = reader2.GetInt16("numID");
                        x++;
                    }
                    conn.Close();

                    for (int a = 0; a < lstLaboratory1.Items.Count; a++)
                    {
                        conn.Open();
                        string sqll = "INSERT INTO tblMedical_Laboratory_1 VALUES (@patientsNo, @labExam)";
                        MySqlCommand cmddd = new MySqlCommand(sqll, conn);
                        cmddd.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                        cmddd.Parameters.AddWithValue("@labExam", lstLaboratory1.Items[a]);
                        cmddd.ExecuteNonQuery();
                        conn.Close();
                    }

                    conn.Open();
                    string sql3 = "INSERT INTO tblNext_In_Line_Laboratory1 VALUES (@numID, @patientsNo, @lname, @fname, @mname, @status, @daterequested); DELETE FROM tblNext_In_line_Doctor WHERE patientNo = @patientsNo";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@numID", x);
                    cmd3.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                    cmd3.Parameters.AddWithValue("@lname", lname);
                    cmd3.Parameters.AddWithValue("@fname", fname);
                    cmd3.Parameters.AddWithValue("@mname", mname);
                    cmd3.Parameters.AddWithValue("@status", lblStatus.Text);
                    cmd3.Parameters.AddWithValue("@daterequested", dtPicker.Value);
                    cmd3.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql5 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.Parameters.AddWithValue("@id", docID);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader4 = cmd5.ExecuteReader();
                    while (reader4.Read())
                    {
                        lnamee = reader4.GetString("lastname");
                        fnamee = reader4.GetString("firstname");
                        mnamee = reader4.GetString("middlename");
                    }
                    conn.Close();

                    conn.Open();
                    string sql6 = "SELECT * FROM tblPatients_Info WHERE patientsNo = @id";
                    MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
                    cmd6.Parameters.AddWithValue("@id", lblNumber.Text);
                    string ln = "";
                    string fn = "";
                    string mn = "";
                    MySqlDataReader reader5 = cmd6.ExecuteReader();
                    while (reader5.Read())
                    {
                        ln = reader5.GetString("lastname");
                        fn = reader5.GetString("firstname");
                        mn = reader5.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "DOCTOR";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + fn + " " + mn + " " + ln + ") TO LABORATORY 1.";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient was sent to Laboratory 1's Next in Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSendLaboratory1.Enabled = false;
                    btnDone.Enabled = true;
                }
                else
                    MessageBox.Show("Select Laboratory Examination first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSendLaboratoryUltrasound2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you you want to send to Laboratory 2?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (lstUltrasound.Items.Count != 0 && lstXray.Items.Count != 0)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @number";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@number", lblNumber.Text);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lname = reader.GetString("lastName");
                        fname = reader.GetString("firstName");
                        mname = reader.GetString("middleName");
                    }
                    conn.Close();
                    conn.Open();
                    string sql2 = "SELECT * FROM tblNext_In_Line_Laboratory2";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int x = 0;
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        x = reader2.GetInt16("numID");
                        x++;
                    }
                    conn.Close();

                    conn.Open();
                    string sql3 = "INSERT INTO tblNext_In_Line_Laboratory2 VALUES (@numID, @patientsNo, @lname, @fname, @mname, @status, @daterequested, @lab); DELETE FROM tblNext_In_Line_Doctor WHERE patientNo = @patientsNo";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@numID", x);
                    cmd3.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                    cmd3.Parameters.AddWithValue("@lname", lname);
                    cmd3.Parameters.AddWithValue("@fname", fname);
                    cmd3.Parameters.AddWithValue("@mname", mname);
                    cmd3.Parameters.AddWithValue("@status", lblStatus.Text);
                    cmd3.Parameters.AddWithValue("@daterequested", dtPicker.Value);
                    cmd3.Parameters.AddWithValue("@lab", "ULTRASOUND / XRAY");
                    cmd3.ExecuteNonQuery();
                    conn.Close();

                    if (lstUltrasound.Items.Count >= 1)
                    {
                        for (int a = 0; a < lstUltrasound.Items.Count; a++)
                        {
                            conn.Open();
                            string sqll = "INSERT INTO tblMedical_Laboratory_Ultrasound VALUES (@patientsNo, @labExam)";
                            MySqlCommand cmddd = new MySqlCommand(sqll, conn);
                            cmddd.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                            cmddd.Parameters.AddWithValue("@labExam", lstUltrasound.Items[a]);
                            cmddd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (lstXray.Items.Count >= 1)
                    {
                        for (int b = 0; b < lstXray.Items.Count; b++)
                        {
                            conn.Open();
                            string sqll = "INSERT INTO tblMedical_Laboratory_Xray VALUES (@patientsNo, @labExam)";
                            MySqlCommand cmddd = new MySqlCommand(sqll, conn);
                            cmddd.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                            cmddd.Parameters.AddWithValue("@labExam", lstXray.Items[b]);
                            cmddd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    //Activity Log
                    conn.Open();
                    string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    cmd4.Parameters.AddWithValue("@id", docID);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader1 = cmd4.ExecuteReader();
                    while (reader1.Read())
                    {
                        lnamee = reader1.GetString("lastname");
                        fnamee = reader1.GetString("firstname");
                        mnamee = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "DOCTOR";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + fname + " " + mname + " " + lname + ") TO LABORATORY 2.";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient was sent to Laboratory 2's Next in Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSendLaboratoryUltrasound2.Enabled = false;
                    btnDone.Enabled = true;
                }
                else if (lstXray.Items.Count != 0)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @number";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@number", lblNumber.Text);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lname = reader.GetString("lastName");
                        fname = reader.GetString("firstName");
                        mname = reader.GetString("middleName");
                    }
                    conn.Close();

                    conn.Open();
                    string sql2 = "SELECT * FROM tblNext_In_Line_Laboratory2";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int x = 0;
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        x = reader2.GetInt16("numID");
                        x++;
                    }
                    conn.Close();

                    conn.Open();
                    string sql3 = "INSERT INTO tblNext_In_Line_Laboratory2 VALUES (@numID, @patientsNo, @lname, @fname, @mname, @status, @daterequested, @lab); DELETE FROM tblNext_In_Line_Doctor WHERE patientNo = @patientsNo";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@numID", x);
                    cmd3.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                    cmd3.Parameters.AddWithValue("@lname", lname);
                    cmd3.Parameters.AddWithValue("@fname", fname);
                    cmd3.Parameters.AddWithValue("@mname", mname);
                    cmd3.Parameters.AddWithValue("@status", lblStatus.Text);
                    cmd3.Parameters.AddWithValue("@daterequested", dtPicker.Value);
                    cmd3.Parameters.AddWithValue("@lab", "XRAY");
                    cmd3.ExecuteNonQuery();
                    conn.Close();

                    if (lstXray.Items.Count >= 1)
                    {
                        for (int b = 0; b < lstXray.Items.Count; b++)
                        {
                            conn.Open();
                            string sqll = "INSERT INTO tblMedical_Laboratory_Xray VALUES (@patientsNo, @labExam)";
                            MySqlCommand cmddd = new MySqlCommand(sqll, conn);
                            cmddd.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                            cmddd.Parameters.AddWithValue("@labExam", lstXray.Items[b]);
                            cmddd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    //Activity Log
                    conn.Open();
                    string sql5 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.Parameters.AddWithValue("@id", docID);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader4 = cmd5.ExecuteReader();
                    while (reader4.Read())
                    {
                        lnamee = reader4.GetString("lastname");
                        fnamee = reader4.GetString("firstname");
                        mnamee = reader4.GetString("middlename");
                    }
                    conn.Close();

                    conn.Open();
                    string sql6 = "SELECT * FROM tblPatients_Info WHERE patientsNo = @id";
                    MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
                    cmd6.Parameters.AddWithValue("@id", lblNumber.Text);
                    string ln = "";
                    string fn = "";
                    string mn = "";
                    MySqlDataReader reader5 = cmd6.ExecuteReader();
                    while (reader5.Read())
                    {
                        ln = reader5.GetString("lastname");
                        fn = reader5.GetString("firstname");
                        mn = reader5.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "DOCTOR";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + fn + " " + mn + " " + ln + ") TO LABORATORY 2.";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient was sent to Laboratory 2's Next in Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSendLaboratoryUltrasound2.Enabled = false;
                    btnDone.Enabled = true;
                }
                else if (lstUltrasound.Items.Count != 0)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @number";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@number", lblNumber.Text);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lname = reader.GetString("lastName");
                        fname = reader.GetString("firstName");
                        mname = reader.GetString("middleName");
                    }
                    conn.Close();
                    conn.Open();
                    string sql2 = "SELECT * FROM tblNext_In_Line_Laboratory2";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int x = 0;
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        x = reader2.GetInt16("numID");
                        x++;
                    }
                    conn.Close();

                    conn.Open();
                    string sql3 = "INSERT INTO tblNext_In_Line_Laboratory2 VALUES (@numID, @patientsNo, @lname, @fname, @mname, @status, @daterequested, @lab); DELETE FROM tblNext_In_Line_Doctor WHERE patientNo = @patientsNo";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@numID", x);
                    cmd3.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                    cmd3.Parameters.AddWithValue("@lname", lname);
                    cmd3.Parameters.AddWithValue("@fname", fname);
                    cmd3.Parameters.AddWithValue("@mname", mname);
                    cmd3.Parameters.AddWithValue("@status", lblStatus.Text);
                    cmd3.Parameters.AddWithValue("@daterequested", dtPicker.Value);
                    cmd3.Parameters.AddWithValue("@lab", "ULTRASOUND");
                    cmd3.ExecuteNonQuery();
                    conn.Close();

                    for (int a = 0; a < lstUltrasound.Items.Count; a++)
                    {
                        conn.Open();
                        string sqll = "INSERT INTO tblMedical_Laboratory_Ultrasound VALUES (@patientsNo, @labExam)";
                        MySqlCommand cmddd = new MySqlCommand(sqll, conn);
                        cmddd.Parameters.AddWithValue("@patientsNo", lblNumber.Text);
                        cmddd.Parameters.AddWithValue("@labExam", lstUltrasound.Items[a]);
                        cmddd.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Activity Log
                    conn.Open();
                    string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    cmd4.Parameters.AddWithValue("@id", docID);
                    string lnamee = "";
                    string fnamee = "";
                    string mnamee = "";
                    MySqlDataReader reader1 = cmd4.ExecuteReader();
                    while (reader1.Read())
                    {
                        lnamee = reader1.GetString("lastname");
                        fnamee = reader1.GetString("firstname");
                        mnamee = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "DOCTOR";
                    string activityy = "SUCCESSFULLY SENT PATIENT(" + fname + " " + mname + " " + lname + ") TO LABORATORY 2.";

                    conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                    MessageBox.Show("Patient was sent to Laboratory 2's Next in Line.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSendLaboratoryUltrasound2.Enabled = false;
                    btnDone.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Select Laboratory Examination first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboUltrasound.Focus();
                }
            }
        }

        private void frmListLaboratory_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
        }

        public string formName = "";
        public string stat = "";
        public string notes = "";
        public string number = "";
        public string status = "";
        public string sentby = "";

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you are done?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cboClinical_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstLaboratory1.Items.Contains(cboClinical.Text + " - CLINICAL MICROSCOPY"))
            {
                lstLaboratory1.Items.Add(cboClinical.Text + " - CLINICAL MICROSCOPY");
                cboClinical.Text = "";
                cboClinical.Focus();
            }
        }

        private void cboHematology_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstLaboratory1.Items.Contains(cboHematology.Text + " - HEMATOLOGY"))
            {
                lstLaboratory1.Items.Add(cboHematology.Text + " - HEMATOLOGY");
                cboHematology.Text = "";
                cboHematology.Focus();
            }
        }

        private void cboBloodChemistry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstLaboratory1.Items.Contains(cboBloodChemistry.Text + " - BLOOD CHEMISTRY"))
            {
                lstLaboratory1.Items.Add(cboBloodChemistry.Text + " - BLOOD CHEMISTRY");
                cboBloodChemistry.Text = "";
                cboBloodChemistry.Focus();
            }
        }

        private void cboXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lstXray.Items.Contains(cboXray.Text))
            {
                lstXray.Items.Add(cboXray.Text);
                cboXray.Text = "";
                cboXray.Focus();
            }
        }
        
        private void cboUltrasound_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (!lstUltrasound.Items.Contains(cboUltrasound.Text))
                {
                    lstUltrasound.Items.Add(cboUltrasound.Text);
                    cboUltrasound.Text = "";
                    cboUltrasound.Focus();
                }
        }

        private void lstUltrasound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUltrasound.SelectedItem != null)
                btnRemoveUltrasound.Enabled = true;
        }

        private void btnRemoveUltrasound_Click(object sender, EventArgs e)
        {
            lstUltrasound.Items.Remove(lstUltrasound.SelectedItem);
            btnRemoveUltrasound.Enabled = false;
        }

        private void lstXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstXray.SelectedItem != null)
                btnRemoveXray.Enabled = true;
        }

        private void btnRemoveXray_Click(object sender, EventArgs e)
        {
            lstXray.Items.Remove(lstXray.SelectedItem);
            btnRemoveXray.Enabled = false;
        }

        private void lstLaboratory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLaboratory1.SelectedItem != null)
                btnRemoveLab1.Enabled = true;
        }

        private void btnRemoveLab1_Click(object sender, EventArgs e)
        {
            lstLaboratory1.Items.Remove(lstLaboratory1.SelectedItem);
            btnRemoveLab1.Enabled = false;
        }
        
    }
}
