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
    public partial class frmLaboratory2 : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmLaboratory2()
        {
            InitializeComponent();
        }

        public string labIdNumber = "";

        private void frmLaboratory2_Load(object sender, EventArgs e)
        {
            //Sonologists
            conn.Open();
            string sql = "SELECT * FROM tblStaff WHERE staff = 'SONOLOGISTS' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            string lname = "";
            string fname = "";
            string mname = "";
            string degree = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lname = reader.GetString("lastName");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleInitial");
                degree = reader.GetString("degree");
                cboSonologists.Items.Add(fname + " " + mname + " " + lname + " " + degree);
            }
            conn.Close();

            //Radiologists
            conn.Open();
            string sql1 = "SELECT * FROM tblStaff WHERE staff = 'RADIOLOGISTS' ";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            string lnameR = "";
            string fnameR= "";
            string mnameR = "";
            string degreeR = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lnameR = reader1.GetString("lastName");
                fnameR = reader1.GetString("firstName");
                mnameR = reader1.GetString("middleInitial");
                degreeR = reader1.GetString("degree");
                cboRadiologists.Items.Add(fnameR + " " + mnameR + " " + lnameR + " " + degreeR);
            }
            conn.Close();

            dtPicker.Text = DateTime.Now.ToShortDateString();
            dtExaminedLab.Text = DateTime.Now.ToShortDateString();
            btnUltrasoudBack.Hide();
            btnBackXray.Hide();
        }

        private void btnUltrasoudNext_Click(object sender, EventArgs e)
        {
            lblResult.Show();
            lblImpression.Hide();
            txtImpressionsUltrasound.SendToBack();
            btnUltrasoudNext.Hide();
            btnUltrasoudBack.Show();
        }

        private void btnUltrasoudBack_Click(object sender, EventArgs e)
        {
            lblImpression.Show();
            lblResult.Hide();
            txtResultUltrasound.SendToBack();
            btnUltrasoudNext.Show();
            btnUltrasoudBack.Hide();
        }

        private void btnSubmitUltrasound_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboType.Text != "" && txtImpressionsUltrasound.Text != "" && txtResultUltrasound.Text != "" && cboSonologists.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sqlStatement = "INSERT INTO tblLaboratory_Ultrasound VALUES (@num, @type, @impression, @result, @sonologists, @daterequested, @dateexamined)";
                    MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                    cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    cmd.Parameters.AddWithValue("@type", cboType.Text);
                    cmd.Parameters.AddWithValue("@impression", txtImpressionsUltrasound.Text);
                    cmd.Parameters.AddWithValue("@result", txtResultUltrasound.Text);
                    cmd.Parameters.AddWithValue("@sonologists", cboSonologists.Text);
                    cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                    cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql1 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                    MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
                    cmd2.Parameters.AddWithValue("@id", labIdNumber);
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

                    string acctype = "LABORATORY 2";
                    string activityy = "SUCCESSFULLY SAVED ULTRASOUND LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Ultrasound Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUltrasoudNext.BringToFront();
                    txtResultUltrasound.SendToBack();
                    lblResult.Hide();
                    lblImpression.Show();
                    txtImpressionsUltrasound.Clear();
                    txtResultUltrasound.Clear();
                    cboType.Items.Remove(cboType.SelectedItem);
                }
                else
                {
                    if (cboType.Text == "")
                    {
                        MessageBox.Show("Select Type of Xray Examination!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboType.Focus();
                    }
                    else if (txtImpressionsUltrasound.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtImpressionsUltrasound.Focus();
                    }
                    else if (txtResultUltrasound.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtResultUltrasound.Focus();
                    }
                    if (cboSonologists.Text == "")
                    {
                        MessageBox.Show("Select Name of Sonologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboSonologists.Focus();
                    }
                }
            }
        }

        private void btnNextXray_Click(object sender, EventArgs e)
        { 
            lblImpressionXray.Hide();
            lblResultsXray.Show();
            txtImpressionsXray.SendToBack();
            btnNextXray.Hide();
            btnBackXray.Show();
        }

        private void btnBackXray_Click(object sender, EventArgs e)
        {
            lblImpressionXray.Show();
            lblResultsXray.Hide();
            txtResultsXray.SendToBack();
            btnNextXray.Show();
            btnBackXray.Hide();
        }

        private void btnSubmitXray_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboTypeXray.Text != "" && txtImpressionsXray.Text != "" && txtResultsXray.Text != "" && cboRadiologists.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sqlStatement = "INSERT INTO tblLaboratory_Xray VALUES (@num, @type, @impression, @result, @radiologists, @daterequested, @dateexamined)";
                    MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                    cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    cmd.Parameters.AddWithValue("@type", cboTypeXray.Text);
                    cmd.Parameters.AddWithValue("@impression", txtImpressionsXray.Text);
                    cmd.Parameters.AddWithValue("@result", txtResultsXray.Text);
                    cmd.Parameters.AddWithValue("@radiologists", cboRadiologists.Text);
                    cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                    cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql1 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
                    MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
                    cmd2.Parameters.AddWithValue("@id", labIdNumber);
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

                    string acctype = "LABORATORY 2";
                    string activityy = "SUCCESSFULLY SAVED X-RAY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);
                    
                    MessageBox.Show("Patient's X-Ray Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBackXray.SendToBack();
                    txtResultsXray.SendToBack();
                    lblResultsXray.Hide();
                    lblImpressionXray.Show();
                    txtImpressionsXray.Clear();
                    txtResultsXray.Clear();
                    cboTypeXray.Items.Remove(cboTypeXray.SelectedItem);
                }
                else
                {
                    if (cboTypeXray.Text == "")
                    {
                        MessageBox.Show("Select Type of Xray Examination!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboTypeXray.Focus();
                    }
                    else if (txtImpressionsXray.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtImpressionsXray.Focus();
                    }
                    else if (txtResultsXray.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtResultsXray.Focus();
                    }
                    if (cboSonologists.Text == "")
                    {
                        MessageBox.Show("Select Name of Sonologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboSonologists.Focus();
                    }
                }
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

        private void btnSendTo_Click(object sender, EventArgs e)
        {
            frmLab2SendTo fs = new frmLab2SendTo();
            fs.lblNumber.Text = lblNumberLab.Text;
            fs.lblName.Text = lblNameLab.Text;
            fs.dtRequested.Value = dtRequestedLab.Value;
            fs.labIdNumber = labIdNumber;
            fs.Show();
        }

        private void btnSubmitUltrasound1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboType.Text != "" && txtImpressionsUltrasound.Text != "" && txtResultUltrasound.Text != "" && cboSonologists.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    int x = 0;
                    string examtype = "";
                    string impression = "";
                    string result = "";
                    string sonologists = "";
                    string daterequested = "";
                    string dateexamined = "";
                    int counter = 0;
                    while (reader.Read())
                    {
                        x++;
                        examtype = reader.GetString("typeofexaminations");
                        impression = reader.GetString("impression");
                        result = reader.GetString("result");
                        sonologists = reader.GetString("sonologists");
                        daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                        dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
                        lstExamType.Items.Add(examtype);
                        lstImpression.Items.Add(impression);
                        lstResult.Items.Add(result);
                        counter++;
                    }
                    conn.Close();

                    if (x == 1)
                    {
                        for (int a = 0; a < counter; a++)
                        {
                            conn.Open();
                            string sql1 = "INSERT INTO tblArchive_Ultrasound VALUES (@patientnum, @examtype, @impression, @result, @sonologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Ultrasound WHERE patientsNo = @patientnum";
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                            cmd1.Parameters.AddWithValue("@patientnum", lblNumberLab.Text);
                            cmd1.Parameters.AddWithValue("@examtype", lstExamType.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@impression", lstImpression.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@result", lstResult.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@sonologists", sonologists);
                            cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                            cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        for (int a = 0; a < counter; a++)
                        {
                            conn.Open();
                            string sql1 = "INSERT INTO tblArchive_Ultrasound VALUES (@patientnum, @examtype, @impression, @result, @sonologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Ultrasound WHERE patientsNo = @patientnum";
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                            cmd1.Parameters.AddWithValue("@patientnum", lblNumberLab.Text);
                            cmd1.Parameters.AddWithValue("@examtype", lstExamType.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@impression", lstImpression.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@result", lstResult.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@sonologists", sonologists);
                            cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                            cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    conn.Open();
                    string sqlStatement = "INSERT INTO tblLaboratory_Ultrasound VALUES (@num, @type, @impression, @result, @sonologists, @daterequested, @dateexamined)";
                    MySqlCommand cmdd = new MySqlCommand(sqlStatement, conn);
                    cmdd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    cmdd.Parameters.AddWithValue("@type", cboType.Text);
                    cmdd.Parameters.AddWithValue("@impression", txtImpressionsUltrasound.Text);
                    cmdd.Parameters.AddWithValue("@result", txtResultUltrasound.Text);
                    cmdd.Parameters.AddWithValue("@sonologists", cboSonologists.Text);
                    cmdd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                    cmdd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                    cmdd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
                    cmd3.Parameters.AddWithValue("@id", labIdNumber);
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

                    string acctype = "LABORATORY 2";
                    string activityy = "SUCCESSFULLY SAVED ULTRASOUND LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Ultrasound Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUltrasoudNext.SendToBack();
                    txtResultUltrasound.SendToBack();
                    lblResult.Hide();
                    lblImpression.Show();
                    txtImpressionsUltrasound.Clear();
                    txtResultUltrasound.Clear();
                    cboType.Items.Remove(cboType.SelectedItem);
                }
                else
                {
                    if (cboType.Text == "")
                    {
                        MessageBox.Show("Select Type of Xray Examination!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboType.Focus();
                    }
                    else if (txtImpressionsUltrasound.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtImpressionsUltrasound.Focus();
                    }
                    else if (txtResultUltrasound.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtResultUltrasound.Focus();
                    }
                    if (cboSonologists.Text == "")
                    {
                        MessageBox.Show("Select Name of Sonologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboSonologists.Focus();
                    }
                }
            }
        }

        private void btnSubmitXray1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboTypeXray.Text != "" && txtImpressionsXray.Text != "" && txtResultsXray.Text != "" && cboRadiologists.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "SELECT * FROM tblLaboratory_Xray WHERE patientsNo = @num";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    int x = 0;
                    string examtype = "";
                    string impression = "";
                    string result = "";
                    string radiologists = "";
                    string daterequested = "";
                    string dateexamined = "";
                    int counter = 0;
                    while (reader.Read())
                    {
                        x++;
                        examtype = reader.GetString("typeofexaminations");
                        impression = reader.GetString("impression");
                        result = reader.GetString("result");
                        radiologists = reader.GetString("radiologists");
                        daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                        dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
                        lstExamType.Items.Add(examtype);
                        lstImpression.Items.Add(impression);
                        lstResult.Items.Add(result);
                        counter++;
                    }
                    conn.Close();

                    if (x == 1)
                    {
                        for (int a = 0; a < counter; a++)
                        {
                            conn.Open();
                            string sql1 = "INSERT INTO tblArchive_Xray VALUES (@patientnum, @examtype, @impression, @result, @sonologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Xray WHERE patientsNo = @patientnum";
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                            cmd1.Parameters.AddWithValue("@patientnum", lblNumberLab.Text);
                            cmd1.Parameters.AddWithValue("@examtype", lstExamType.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@impression", lstImpression.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@result", lstResult.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@sonologists", radiologists);
                            cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                            cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        for (int a = 0; a < counter; a++)
                        {
                            conn.Open();
                            string sql1 = "INSERT INTO tblArchive_Xray VALUES (@patientnum, @examtype, @impression, @result, @sonologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Xray WHERE patientsNo = @patientnum";
                            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                            cmd1.Parameters.AddWithValue("@patientnum", lblNumberLab.Text);
                            cmd1.Parameters.AddWithValue("@examtype", lstExamType.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@impression", lstImpression.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@result", lstResult.Items[a].ToString());
                            cmd1.Parameters.AddWithValue("@sonologists", radiologists);
                            cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                            cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    conn.Open();
                    string sqlStatement = "INSERT INTO tblLaboratory_Xray VALUES (@num, @type, @impression, @result, @radiologists, @daterequested, @dateexamined)";
                    MySqlCommand cmdd = new MySqlCommand(sqlStatement, conn);
                    cmdd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                    cmdd.Parameters.AddWithValue("@type", cboTypeXray.Text);
                    cmdd.Parameters.AddWithValue("@impression", txtImpressionsXray.Text);
                    cmdd.Parameters.AddWithValue("@result", txtResultsXray.Text);
                    cmdd.Parameters.AddWithValue("@radiologists", cboRadiologists.Text);
                    cmdd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                    cmdd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                    cmdd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
                    cmd3.Parameters.AddWithValue("@id", labIdNumber);
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

                    string acctype = "LABORATORY 2";
                    string activityy = "SUCCESSFULLY SAVED X-RAY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's X-Ray Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBackXray.BringToFront();
                    txtResultsXray.SendToBack();
                    lblResultsXray.Hide();
                    lblImpressionXray.Show();
                    txtImpressionsXray.Clear();
                    txtResultsXray.Clear();
                    cboTypeXray.Items.Remove(cboTypeXray.SelectedItem);
                }
                else
                {
                    if (cboTypeXray.Text == "")
                    {
                        MessageBox.Show("Select Type of Xray Examination!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboTypeXray.Focus();
                    }
                    else if (txtImpressionsXray.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtImpressionsXray.Focus();
                    }
                    else if (txtResultsXray.Text == "")
                    {
                        MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtResultsXray.Focus();
                    }
                    if (cboSonologists.Text == "")
                    {
                        MessageBox.Show("Select Name of Sonologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboSonologists.Focus();
                    }
                }
            }
        }

        private void btnPrintUltrasound_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string typeExam = "";
            string impression = "";
            string result = "";
            string sonologists = "";
            string dateexamined = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                typeExam = reader2.GetString("typeofexaminations");
                impression = reader2.GetString("impression");
                result = reader2.GetString("result");
                sonologists = reader2.GetString("sonologists");
                dateexamined = reader2.GetString("dateexamined");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.Parameters.AddWithValue("@id", labIdNumber);
            string num = "";
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                num = reader3.GetString("IDNumber");
            }
            conn.Close();

            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", num);
            string lnamee = "";
            string fnamee = "";
            string mnamee = "";
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                lnamee = reader4.GetString("lastname");
                fnamee = reader4.GetString("firstname");
                mnamee = reader4.GetString("middlename");
            }
            conn.Close();

            string acctype = "LABORATORY 2";
            string activityy = "PRINTED LABORATORY ULTRASOUND EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintUltrasound f1 = new frmPrintUltrasound();

            ReportParameter param = new ReportParameter("patientNo", id);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("name", fname + " " + mname + " " + lname);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("address", addr);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("age", age.ToString());
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sex", sex);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("date", dateexamined);
            f1.reportViewer1.LocalReport.SetParameters(param);

            param = new ReportParameter("typeofexam", typeExam);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("result", result);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("impression", impression);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sonologists", sonologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void btnPrintXray_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Xray WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string typeExam = "";
            string impression = "";
            string result = "";
            string sonologists = "";
            string dateexamined = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                typeExam = reader2.GetString("typeofexaminations");
                impression = reader2.GetString("impression");
                result = reader2.GetString("result");
                sonologists = reader2.GetString("sonologists");
                dateexamined = reader2.GetString("dateexamined");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE IDNumber = @id";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.Parameters.AddWithValue("@id", labIdNumber);
            string num = "";
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                num = reader3.GetString("IDNumber");
            }
            conn.Close();

            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", num);
            string lnamee = "";
            string fnamee = "";
            string mnamee = "";
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                lnamee = reader4.GetString("lastname");
                fnamee = reader4.GetString("firstname");
                mnamee = reader4.GetString("middlename");
            }
            conn.Close();

            string acctype = "LABORATORY 2";
            string activityy = "PRINTED LABORATORY X-RAY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintXray f1 = new frmPrintXray();

            ReportParameter param = new ReportParameter("patientNo", id);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("name", fname + " " + mname + " " + lname);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("address", addr);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("age", age.ToString());
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sex", sex);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("date", dateexamined);
            f1.reportViewer1.LocalReport.SetParameters(param);

            param = new ReportParameter("typeofexam", typeExam);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("result", result);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("impression", impression);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sonologists", sonologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void cboTypeXray_Click(object sender, EventArgs e)
        {
            if (cboTypeXray.Items.Count == 0 && cboType.Items.Count == 0)
                btnSendTo.Enabled = true;
        }

        private void cboType_Click(object sender, EventArgs e)
        {
            if (cboType.Items.Count == 0 && cboTypeXray.Items.Count == 0)
                btnSendTo.Enabled = true;
        }

    }
}
