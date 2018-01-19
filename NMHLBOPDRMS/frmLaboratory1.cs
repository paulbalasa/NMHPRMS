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
    public partial class frmLaboratory1 : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmLaboratory1()
        {
            InitializeComponent();
        }

        public string labIdNumber;

        private void frmLaboratory1_Load(object sender, EventArgs e)
        {
            //Medical Technologists
            conn.Open();
            string sql1 = "SELECT * FROM tblStaff WHERE staff = 'MEDICAL TECHNOLOGISTS' ";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            string lname = "";
            string fname = "";
            string mname = "";
            string degree = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lname = reader1.GetString("lastName");
                fname = reader1.GetString("firstName");
                mname = reader1.GetString("middleInitial");
                degree = reader1.GetString("degree");
                cboMedTech.Items.Add(fname + " " + mname + " " + lname + " " + degree);
            }
            conn.Close();

            //Pathologists
            conn.Open();
            string sql3 = "SELECT * FROM tblStaff WHERE staff = 'PATHOLOGISTS' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            string lnamep = "";
            string fnamep = "";
            string mnamep = "";
            string degreep = "";
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                lnamep = reader3.GetString("lastName");
                fnamep = reader3.GetString("firstName");
                mnamep = reader3.GetString("middleInitial");
                degreep = reader3.GetString("degree");
                cboPathologist.Items.Add(fnamep + " " + mnamep + " " + lnamep + " " + degreep);
            }
            conn.Close();

            dtExaminedLab.Text = DateTime.Now.ToShortDateString();
            dtPicker.Text = DateTime.Now.ToShortDateString();
            btnBackBC.Hide();
            btnBackHema.Hide();
        }

        private void btnNextBC_Click(object sender, EventArgs e)
        {
            pnlBC1.SendToBack();
            btnNextBC.Hide();
            btnBackBC.Show();
        }

        private void btnBackBC_Click(object sender, EventArgs e)
        {
            pnlBC2.SendToBack();
            btnNextBC.Show();
            btnBackBC.Hide();
        }

        private void btnSubmitBC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Blood_Chemistry VALUES (@num, @fast, @blood, @create, @cholesterol, @trigly, @hdl, @ldl, @vldl, @bloodu, @sodium, @potassium, @chloride, @sgpt, @sgot, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@fast", txtFBS.Text);
                        cmd.Parameters.AddWithValue("@blood", txtBUN.Text);
                        cmd.Parameters.AddWithValue("@create", txtCreatinine.Text);
                        cmd.Parameters.AddWithValue("@cholesterol", txtCholesterol.Text);
                        cmd.Parameters.AddWithValue("@trigly", txtTriglycerides.Text);
                        cmd.Parameters.AddWithValue("@hdl", txtHDL.Text);
                        cmd.Parameters.AddWithValue("@ldl", txtLDL.Text);
                        cmd.Parameters.AddWithValue("@vldl", txtVLDL.Text);
                        cmd.Parameters.AddWithValue("@bloodu", txtBUA.Text);
                        cmd.Parameters.AddWithValue("@sodium", txtSodium.Text);
                        cmd.Parameters.AddWithValue("@potassium", txtPotassium.Text);
                        cmd.Parameters.AddWithValue("@chloride", txtChloride.Text);
                        cmd.Parameters.AddWithValue("@sgpt", txtSGPT.Text);
                        cmd.Parameters.AddWithValue("@sgot", txtSGOT.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Blood_Chemistry SET fasting_blood_sugar = '" + txtFBS.Text + "', blood_urea_nitrogen = '" + txtBUN.Text + "', creatinine = '" + txtCreatinine.Text + "', cholesterol = '" + txtCholesterol.Text + "', triglycerides = '" + txtTriglycerides.Text + "', hdl = '" + txtHDL.Text + "', ldl = '" + txtLDL.Text + "', vldl = '" + txtVLDL.Text + "', blood_uric_acid = '" + txtBUA.Text + "', sodium = '" + txtSodium.Text + "', potassium = '" + txtPotassium.Text + "', chloride = '" + txtChloride.Text + "', sgpt_alt = '" + txtSGPT.Text + "', sgot_ast = '" + txtSGOT.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

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

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED BLOOD CHEMISTRY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Blood Chemistry Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }        
        }

        private void btnSubmitUrinalysis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Urinalysis_Mac VALUES (@num, @color, @character, @protein, @sugar, @ph, @sp, @ptest, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@color", txtColor.Text);
                        cmd.Parameters.AddWithValue("@character", txtCharacter.Text);
                        cmd.Parameters.AddWithValue("@protein", txtProtein.Text);
                        cmd.Parameters.AddWithValue("@sugar", txtSugar.Text);
                        cmd.Parameters.AddWithValue("@ph", txtPH.Text);
                        cmd.Parameters.AddWithValue("@sp", txtSpGr.Text);
                        cmd.Parameters.AddWithValue("@ptest", txtPregnancyTest.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string sqlStatement1 = "INSERT INTO tblLaboratory_Urinalysis_Mic VALUES (@num, @pus, @rbc, @epith, @bacteria, @mucus, @amor, @casts, @crystals, @others)";
                        MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                        cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd1.Parameters.AddWithValue("@pus", txtPUSCells.Text);
                        cmd1.Parameters.AddWithValue("@rbc", txtRBC.Text);
                        cmd1.Parameters.AddWithValue("@epith", txtEpithCells.Text);
                        cmd1.Parameters.AddWithValue("@bacteria", txtBacteria.Text);
                        cmd1.Parameters.AddWithValue("@mucus", txtMucusThread.Text);
                        cmd1.Parameters.AddWithValue("@amor", txtAmorphousUrates.Text);
                        cmd1.Parameters.AddWithValue("@casts", txtCasts.Text);
                        cmd1.Parameters.AddWithValue("@crystals", txtCasts.Text);
                        cmd1.Parameters.AddWithValue("@others", txtOthers.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Urinalysis_Mac SET color = '" + txtColor.Text + "', characters = '" + txtCharacter.Text + "', protein = '" + txtProtein.Text + "', sugar = '" + txtSugar.Text + "', ph = '" + txtPH.Text + "', spGr = '" + txtSpGr.Text + "', pregnancyTest = '" + txtPregnancyTest.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'; UPDATE tblLaboratory_Urinalysis_Mic SET pus_cells = '" + txtPUSCells.Text + "', rbc = '" + txtRBC.Text + "', epith_cells = '" + txtEpithCells.Text + "', bacteria = '" + txtBacteria.Text + "', mucus_thread = '" + txtMucusThread.Text + "', amorphous_urates = '" + txtAmorphousUrates.Text + "', casts = '" + txtCasts.Text + "', crystals = '" + txtCrystals.Text + "', others = '" + txtOthers.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

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

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED URINALYSIS LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Urinalysis Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSubmitFecalysis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Fecalysis_Mac VALUES (@num, @color, @characters, @reaction, @occult_blood, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@color", txtColorFeca.Text);
                        cmd.Parameters.AddWithValue("@characters", txtCharacterFeca.Text);
                        cmd.Parameters.AddWithValue("@reaction", txtReaction.Text);
                        cmd.Parameters.AddWithValue("@occult_blood", txtOccultBlood.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string sqlStatement1 = "INSERT INTO tblLaboratory_Fecalysis_Mic VALUES (@num, @pus_cells, @rbc, @fat_globules, @macrophage, @bacteria, @parasites_or_ova)";
                        MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                        cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd1.Parameters.AddWithValue("@pus_cells", txtPcells.Text);
                        cmd1.Parameters.AddWithValue("@rbc", txtRbcFeca.Text);
                        cmd1.Parameters.AddWithValue("@fat_globules", txtFlatGobules.Text);
                        cmd1.Parameters.AddWithValue("@macrophage", txtMacrophage.Text);
                        cmd1.Parameters.AddWithValue("@bacteria", txtBacteriaFeca.Text);
                        cmd1.Parameters.AddWithValue("@parasites_or_ova", txtParasites.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Fecalysis_Mac SET color = '" + txtColorFeca.Text + "', characters = '" + txtCharacterFeca.Text + "', reaction = '" + txtReaction.Text + "', occult_blood = '" + txtOccultBlood.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'; UPDATE tblLaboratory_Fecalysis_Mic SET pus_cells = '" + txtPcells.Text + "', rbc = '" + txtRbcFeca.Text + "', fat_globules = '" + txtFlatGobules + "', macrophage = '" + txtMacrophage.Text + "', bacteria = '" + txtBacteriaFeca.Text + "', parasites_or_ova = '" + txtParasites.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'   ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

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

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED FECALYSIS LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Fecalysis Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSubmitHematology_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Hematology VALUES (@num, @hemoglobin, @hematocrit, @wbc_count, @rbc_count, @platelet, @bleeding_time, @clotting_time, @abo_group, @segmenters, @lymphocytes, @monocytes, @eosinophils, @basophils, @stab, @others, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@hemoglobin", txtHemoglobin.Text);
                        cmd.Parameters.AddWithValue("@hematocrit", txtHematocrit.Text);
                        cmd.Parameters.AddWithValue("@wbc_count", txtWBC.Text);
                        cmd.Parameters.AddWithValue("@rbc_count", txtRbcHema.Text);
                        cmd.Parameters.AddWithValue("@platelet", txtPlatelet.Text);
                        cmd.Parameters.AddWithValue("@bleeding_time", txtBleeding.Text);
                        cmd.Parameters.AddWithValue("@clotting_time", txtClotting.Text);
                        cmd.Parameters.AddWithValue("@abo_group", txtABO.Text);
                        cmd.Parameters.AddWithValue("@segmenters", txtSegmenters.Text);
                        cmd.Parameters.AddWithValue("@lymphocytes", txtLymphocytes.Text);
                        cmd.Parameters.AddWithValue("@monocytes", txtMonocytes.Text);
                        cmd.Parameters.AddWithValue("@eosinophils", txtEosinophils.Text);
                        cmd.Parameters.AddWithValue("@basophils", txtBasophils.Text);
                        cmd.Parameters.AddWithValue("@stab", txtStab.Text);
                        cmd.Parameters.AddWithValue("@others", txtOthersHema.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Hematology SET hemoglobin = '" + txtHemoglobin.Text + "', hematocrit = '" + txtHematocrit.Text + "', wbc_count = '" + txtWBC.Text + "', rbc_count = '" + txtRbcHema + "', platelet = '" + txtPlatelet.Text + "', bleeding_time = '" + txtBleeding.Text + "', clotting_time = '" + txtClotting.Text + "', abo_group = '" + txtABO.Text + "', segmenters = '" + txtSegmenters.Text + "', lymphocytes = '" + txtLymphocytes.Text + "', monocytes = '" + txtMonocytes.Text + "', eosinophils = '" + txtEosinophils.Text + "', basophils = '" + txtBasophils.Text + "', stab = '" + txtStab.Text + "', others = '" + txtOthersHema.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

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

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED HEMATOLOGY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Hematology Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnNextHema_Click(object sender, EventArgs e)
        {
            pnl1.SendToBack();
            btnNextHema.Hide();
            btnBackHema.Show();
        }

        private void btnBackHema_Click(object sender, EventArgs e)
        {
            pnl2.SendToBack();
            btnNextHema.Show();
            btnBackHema.Hide();
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
            frmLab1SendTo fs = new frmLab1SendTo();
            fs.lblNumber.Text = lblNumberLab.Text;
            fs.lblName.Text = lblNameLab.Text;
            fs.dtRequested.Value = dtRequestedLab.Value;
            fs.labIdNumber = labIdNumber;
            fs.Show();
        }

        private void btnPrintBloodChemistry_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string idd = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                idd = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
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
            string Bdateexamined = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                Bfasting_blood_sugar = reader1.GetString("fasting_blood_sugar");
                Bblood_urea_nitrogen = reader1.GetString("blood_urea_nitrogen");
                Bcreatinine = reader1.GetString("creatinine");
                Bcholesterol = reader1.GetString("cholesterol");
                Btriglycerides = reader1.GetString("triglycerides");
                Bhdl = reader1.GetString("hdl");
                Bldl = reader1.GetString("ldl");
                Bvldl = reader1.GetString("vldl");
                Bblood_uric_acid = reader1.GetString("blood_uric_acid");
                Bsodium = reader1.GetString("sodium");
                Bpotassium = reader1.GetString("potassium");
                Bchloride = reader1.GetString("chloride");
                Bsgpt_alt = reader1.GetString("sgpt_alt");
                Bsgot_ast = reader1.GetString("sgot_ast");
                Bmedtech = reader1.GetString("medtech");
                Bpathologist = reader1.GetString("pathologists");
                Bdateexamined = reader1.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql2, conn);
            cmd4.Parameters.AddWithValue("@id", labIdNumber);
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

            string acctype = "LABORATORY 1";
            string activityy = "PRINTED LABORATORY BLOOD CHEMISTRY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintBloodChemistry f1 = new frmPrintBloodChemistry();
            ReportParameter param = new ReportParameter("patientNo", idd);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("name", fname + " " + mname + " " + lname);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("address", addr);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("age", age.ToString());
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sex", sex);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("date", Bdateexamined);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("fbs", Bfasting_blood_sugar);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("bun", Bblood_urea_nitrogen);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("creatinine", Bcreatinine);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("cholesterol", Bcholesterol);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("triglycerides", Btriglycerides);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("hdl", Bhdl);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("ldl", Bldl);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("vldl", Bvldl);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("bua", Bblood_uric_acid);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sodium", Bsodium);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("potassium", Bpotassium);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("chloride", Bchloride);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sgpt", Bsgpt_alt);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sgot", Bsgot_ast);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("medtech", Bmedtech);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("pathologists", "DR. " + Bpathologist);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void btnPrintFecalysis_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string idd = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                idd = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string pus_cells = "";
            string rbc = "";
            string fat_globules = "";
            string macrophage = "";
            string bacteria = "";
            string parasites_or_ova = "";

            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                pus_cells = reader1.GetString("pus_cells");
                rbc = reader1.GetString("rbc");
                fat_globules = reader1.GetString("fat_globules");
                macrophage = reader1.GetString("macrophage");
                bacteria = reader1.GetString("bacteria");
                parasites_or_ova = reader1.GetString("parasites_or_ova");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string color = "";
            string characters = "";
            string reaction = "";
            string occult_blood = "";
            string medtech = "";
            string pathologists = "";
            string dateexamined = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                color = reader2.GetString("color");
                characters = reader2.GetString("characters");
                reaction = reader2.GetString("reaction");
                occult_blood = reader2.GetString("occult_blood");
                medtech = reader2.GetString("medtech");
                pathologists = reader2.GetString("pathologists");
                dateexamined = reader2.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", labIdNumber);
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

            string acctype = "LABORATORY 1";
            string activityy = "PRINTED LABORATORY FECALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintFecalysis f1 = new frmPrintFecalysis();
            ReportParameter param = new ReportParameter("patientNo", idd);
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
            param = new ReportParameter("color", color);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("character", characters);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("reaction", reaction);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("occultblood", occult_blood);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("puscells", pus_cells);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("rbc", rbc);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("fat", fat_globules);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("macrophage", macrophage);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("bacteria", bacteria);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("parasites", parasites_or_ova);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("medtech", medtech);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("pathologists", "DR. " + pathologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void btnPrintHematology_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string idd = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                idd = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string hemoglobin = "";
            string hematocrit = "";
            string wbc_count = "";
            string rbc_count = "";
            string platelet = "";
            string bleeding_time = "";
            string clotting_time = "";
            string abo_group = "";
            string segmenters = "";
            string lymphocytes = "";
            string monocytes = "";
            string eosinophils = "";
            string basophils = "";
            string stab = "";
            string others = "";
            string Bmedtech = "";
            string Bpathologist = "";
            string Bdateexamined = "";
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                hemoglobin = reader1.GetString("hemoglobin");
                hematocrit = reader1.GetString("hematocrit");
                wbc_count = reader1.GetString("wbc_count");
                rbc_count = reader1.GetString("rbc_count");
                platelet = reader1.GetString("platelet");
                bleeding_time = reader1.GetString("bleeding_time");
                clotting_time = reader1.GetString("clotting_time");
                abo_group = reader1.GetString("abo_group");
                segmenters = reader1.GetString("segmenters");
                lymphocytes = reader1.GetString("lymphocytes");
                monocytes = reader1.GetString("monocytes");
                eosinophils = reader1.GetString("eosinophils");
                basophils = reader1.GetString("basophils");
                stab = reader1.GetString("stab");
                others = reader1.GetString("others");
                Bmedtech = reader1.GetString("medtech");
                Bpathologist = reader1.GetString("pathologists");
                Bdateexamined = reader1.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql4 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@id", labIdNumber);
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

            string acctype = "LABORATORY 1";
            string activityy = "PRINTED LABORATORY HEMATOLOGY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintHematology f1 = new frmPrintHematology();
            ReportParameter param = new ReportParameter("patientNo", idd);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("name", fname + " " + mname + " " + lname);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("address", addr);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("age", age.ToString());
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sex", sex);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("date", Bdateexamined);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("hemoglobin", hemoglobin);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("hematocrit", hematocrit);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("wbc", wbc_count);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("rbc", rbc_count);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("platelet", platelet);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("bleeding", bleeding_time);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("clotting", clotting_time);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("abo", abo_group);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("segmenters", segmenters);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("lymphocytes", lymphocytes);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("monocytes", monocytes);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("eosinophils", eosinophils);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("basophils", basophils);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("stab", stab);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("others", others);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("medtech", Bmedtech);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("pathologists", "DR. " + Bpathologist);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void btnPrintUrinalysis_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            string idd = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            while (reader.Read())
            {
                idd = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string color = "";
            string characters = "";
            string protein = "";
            string sugar = "";
            string ph = "";
            string spGr = "";
            string pregnancyTest = "";
            string medtech = "";
            string pathologists = "";
            string dateexamined = "";

            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                color = reader1.GetString("color");
                characters = reader1.GetString("characters");
                protein = reader1.GetString("protein");
                sugar = reader1.GetString("sugar");
                ph = reader1.GetString("ph");
                spGr = reader1.GetString("spGr");
                pregnancyTest = reader1.GetString("pregnancyTest");
                medtech = reader1.GetString("medtech");
                pathologists = reader1.GetString("pathologists");
                dateexamined = reader1.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", lblNumberLab.Text);
            string pus_cells = "";
            string rbc = "";
            string epith_cells = "";
            string bacteria = "";
            string mucus_thread = "";
            string amorphous_urates = "";
            string casts = "";
            string crystals = "";
            string others = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                pus_cells = reader2.GetString("pus_cells");
                rbc = reader2.GetString("rbc");
                epith_cells = reader2.GetString("epith_cells");
                bacteria = reader2.GetString("bacteria");
                mucus_thread = reader2.GetString("mucus_thread");
                amorphous_urates = reader2.GetString("amorphous_urates");
                casts = reader2.GetString("casts");
                crystals = reader2.GetString("crystals");
                others = reader2.GetString("others");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd4 = new MySqlCommand(sql3, conn);
            cmd4.Parameters.AddWithValue("@id", labIdNumber);
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

            string acctype = "LABORATORY 1";
            string activityy = "PRINTED LABORATORY URINALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintUrinalysis f1 = new frmPrintUrinalysis();
            ReportParameter param = new ReportParameter("patientNo", idd);
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
            param = new ReportParameter("color", color);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("character", characters);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("protein", protein);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sugar", sugar);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("ph", ph);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("spgr", spGr);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("pregnancytest", pregnancyTest);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("puscells", pus_cells);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("rbc", rbc);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("epithcells", epith_cells);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("bacteria", bacteria);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("mucusthread", mucus_thread);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("aup", amorphous_urates);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("casts", casts);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("crystals", crystals);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("others", others);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("medtech", medtech);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("pathologists", "DR. " + pathologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void oldBloodChem()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            int x = 0;
            string fast = "";
            string blood = "";
            string create = "";
            string cholesterol = "";
            string trigly = "";
            string hdl = "";
            string ldl = "";
            string vldl = "";
            string bloodu = "";
            string sodium = "";
            string potassium = "";
            string chloride = "";
            string sgpt = "";
            string sgot = "";
            string medtech = "";
            string pathologists = "";
            string daterequested = "";
            string dateexamined = "";
            while (reader.Read())
            {
                x++;
                fast = reader.GetString("fasting_blood_sugar");
                blood = reader.GetString("blood_urea_nitrogen");
                create = reader.GetString("creatinine");
                cholesterol = reader.GetString("cholesterol");
                trigly = reader.GetString("triglycerides");
                hdl = reader.GetString("hdl");
                ldl = reader.GetString("ldl");
                vldl = reader.GetString("vldl");
                bloodu = reader.GetString("blood_uric_acid");
                sodium = reader.GetString("sodium");
                potassium = reader.GetString("potassium");
                chloride = reader.GetString("chloride");
                sgpt = reader.GetString("sgpt_alt");
                sgot = reader.GetString("sgot_ast");
                medtech = reader.GetString("medtech");
                pathologists = reader.GetString("pathologists");
                daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
            }
            conn.Close();

            if (x == 1)
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Blood_Chemistry VALUES (@num, @fast, @blood, @create, @cholesterol, @trigly, @hdl, @ldl, @vldl, @bloodu, @sodium, @potassium, @chloride, @sgpt, @sgot, @medtech, @pathologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@fast", fast);
                cmd1.Parameters.AddWithValue("@blood", blood);
                cmd1.Parameters.AddWithValue("@create", create);
                cmd1.Parameters.AddWithValue("@cholesterol", cholesterol);
                cmd1.Parameters.AddWithValue("@trigly", trigly);
                cmd1.Parameters.AddWithValue("@hdl", hdl);
                cmd1.Parameters.AddWithValue("@ldl", ldl);
                cmd1.Parameters.AddWithValue("@vldl", vldl);
                cmd1.Parameters.AddWithValue("@bloodu", bloodu);
                cmd1.Parameters.AddWithValue("@sodium", sodium);
                cmd1.Parameters.AddWithValue("@potassium", potassium);
                cmd1.Parameters.AddWithValue("@chloride", chloride);
                cmd1.Parameters.AddWithValue("@sgpt", sgpt);
                cmd1.Parameters.AddWithValue("@sgot", sgot);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Blood_Chemistry VALUES (@num, @fast, @blood, @create, @cholesterol, @trigly, @hdl, @ldl, @vldl, @bloodu, @sodium, @potassium, @chloride, @sgpt, @sgot, @medtech, @pathologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@fast", fast);
                cmd1.Parameters.AddWithValue("@blood", blood);
                cmd1.Parameters.AddWithValue("@create", create);
                cmd1.Parameters.AddWithValue("@cholesterol", cholesterol);
                cmd1.Parameters.AddWithValue("@trigly", trigly);
                cmd1.Parameters.AddWithValue("@hdl", hdl);
                cmd1.Parameters.AddWithValue("@ldl", ldl);
                cmd1.Parameters.AddWithValue("@vldl", vldl);
                cmd1.Parameters.AddWithValue("@bloodu", bloodu);
                cmd1.Parameters.AddWithValue("@sodium", sodium);
                cmd1.Parameters.AddWithValue("@potassium", potassium);
                cmd1.Parameters.AddWithValue("@chloride", chloride);
                cmd1.Parameters.AddWithValue("@sgpt", sgpt);
                cmd1.Parameters.AddWithValue("@sgot", sgot);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void oldFeca()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            int x = 0;
            string color = "";
            string characters = "";
            string reaction = "";
            string occult_blood = "";
            string medtech = "";
            string pathologists = "";
            string daterequested = "";
            string dateexamined = "";
            
            while (reader.Read())
            {
                x++;
                color = reader.GetString("color");
                characters = reader.GetString("characters");
                reaction = reader.GetString("reaction");
                occult_blood = reader.GetString("occult_blood");
                medtech = reader.GetString("medtech");
                pathologists = reader.GetString("pathologists");
                daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
            }
            conn.Close();

            conn.Open();
            string sqll = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader readerr = cmdd.ExecuteReader();
            string pus_cells = "";
            string rbc = "";
            string fat_globules = "";
            string macrophage = "";
            string bacteria = "";
            string parasites_or_ova = "";

            while (readerr.Read())
            {
                pus_cells = readerr.GetString("pus_cells");
                rbc = readerr.GetString("rbc");
                fat_globules = readerr.GetString("fat_globules");
                macrophage = readerr.GetString("macrophage");
                bacteria = readerr.GetString("bacteria");
                parasites_or_ova = readerr.GetString("parasites_or_ova");
            }
            conn.Close();

            if (x == 1)
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Fecalysis_Mac VALUES (@num, @color, @characters, @reaction, @occult_blood, @medtech, @pathologists, @daterequested, @dateexamined); INSERT INTO tblArchive_Fecalysis_Mic VALUES (@num, @pus_cells, @rbc, @fat_globules, @macrophage, @bacteria, @parasites_or_ova); DELETE FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num; DELETE FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@color", color);
                cmd1.Parameters.AddWithValue("@characters", characters);
                cmd1.Parameters.AddWithValue("@reaction", reaction);
                cmd1.Parameters.AddWithValue("@occult_blood", occult_blood);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                cmd1.Parameters.AddWithValue("@pus_cells", pus_cells);
                cmd1.Parameters.AddWithValue("@rbc", rbc);
                cmd1.Parameters.AddWithValue("@fat_globules", fat_globules);
                cmd1.Parameters.AddWithValue("@macrophage", macrophage);
                cmd1.Parameters.AddWithValue("@bacteria", bacteria);
                cmd1.Parameters.AddWithValue("@parasites_or_ova", parasites_or_ova);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Fecalysis_Mac VALUES (@num, @color, @characters, @reaction, @occult_blood, @medtech, @pathologists, @daterequested, @dateexamined); INSERT INTO tblArchive_Fecalysis_Mic VALUES (@num, @pus_cells, @rbc, @fat_globules, @macrophage, @bacteria, @parasites_or_ova); DELETE FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num; DELETE FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@color", color);
                cmd1.Parameters.AddWithValue("@characters", characters);
                cmd1.Parameters.AddWithValue("@reaction", reaction);
                cmd1.Parameters.AddWithValue("@occult_blood", occult_blood);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                cmd1.Parameters.AddWithValue("@pus_cells", pus_cells);
                cmd1.Parameters.AddWithValue("@rbc", rbc);
                cmd1.Parameters.AddWithValue("@fat_globules", fat_globules);
                cmd1.Parameters.AddWithValue("@macrophage", macrophage);
                cmd1.Parameters.AddWithValue("@bacteria", bacteria);
                cmd1.Parameters.AddWithValue("@parasites_or_ova", parasites_or_ova);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void oldHema()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            int x = 0;
            string hemoglobin = "";
            string hematocrit = "";
            string wbc_count = "";
            string rbc_count = "";
            string platelet = "";
            string bleeding_time = "";
            string clotting_time = "";
            string abo_group = "";
            string segmenters = "";
            string lymphocytes = "";
            string monocytes = "";
            string eosinophils = "";
            string basophils = "";
            string stab = "";
            string others = "";
            string medtech = "";
            string pathologists = "";
            string daterequested = "";
            string dateexamined = "";
            while (reader.Read())
            {
                x++;
                hemoglobin = reader.GetString("hemoglobin");
                hematocrit = reader.GetString("hematocrit");
                wbc_count = reader.GetString("wbc_count");
                rbc_count = reader.GetString("rbc_count");
                platelet = reader.GetString("platelet");
                bleeding_time = reader.GetString("bleeding_time");
                clotting_time = reader.GetString("clotting_time");
                abo_group = reader.GetString("abo_group");
                segmenters = reader.GetString("segmenters");
                lymphocytes = reader.GetString("lymphocytes");
                monocytes = reader.GetString("monocytes");
                eosinophils = reader.GetString("eosinophils");
                basophils = reader.GetString("basophils");
                stab = reader.GetString("stab");
                others = reader.GetString("others");
                medtech = reader.GetString("medtech");
                pathologists = reader.GetString("pathologists");
                daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
            }
            conn.Close();

            if (x == 1)
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Hematology VALUES (@num, @hemoglobin, @hematocrit, @wbc_count, @rbc_count, @platelet, @bleeding_time, @clotting_time, @abo_group, @segmenters, @lymphocytes, @monocytes, @eosinophils, @basophils, @stab, @others, @medtech, @pathologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Hematology WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@hemoglobin", hemoglobin);
                cmd1.Parameters.AddWithValue("@hematocrit", hematocrit);
                cmd1.Parameters.AddWithValue("@wbc_count", wbc_count);
                cmd1.Parameters.AddWithValue("@rbc_count", rbc_count);
                cmd1.Parameters.AddWithValue("@platelet", platelet);
                cmd1.Parameters.AddWithValue("@bleeding_time", bleeding_time);
                cmd1.Parameters.AddWithValue("@clotting_time", clotting_time);
                cmd1.Parameters.AddWithValue("@abo_group", abo_group);
                cmd1.Parameters.AddWithValue("@segmenters", segmenters);
                cmd1.Parameters.AddWithValue("@lymphocytes", lymphocytes);
                cmd1.Parameters.AddWithValue("@monocytes", monocytes);
                cmd1.Parameters.AddWithValue("@eosinophils", eosinophils);
                cmd1.Parameters.AddWithValue("@basophils", basophils);
                cmd1.Parameters.AddWithValue("@stab", stab);
                cmd1.Parameters.AddWithValue("@others", others);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Hematology VALUES (@num, @hemoglobin, @hematocrit, @wbc_count, @rbc_count, @platelet, @bleeding_time, @clotting_time, @abo_group, @segmenters, @lymphocytes, @monocytes, @eosinophils, @basophils, @stab, @others, @medtech, @pathologists, @daterequested, @dateexamined); DELETE FROM tblLaboratory_Hematology WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@hemoglobin", hemoglobin);
                cmd1.Parameters.AddWithValue("@hematocrit", hematocrit);
                cmd1.Parameters.AddWithValue("@wbc_count", wbc_count);
                cmd1.Parameters.AddWithValue("@rbc_count", rbc_count);
                cmd1.Parameters.AddWithValue("@platelet", platelet);
                cmd1.Parameters.AddWithValue("@bleeding_time", bleeding_time);
                cmd1.Parameters.AddWithValue("@clotting_time", clotting_time);
                cmd1.Parameters.AddWithValue("@abo_group", abo_group);
                cmd1.Parameters.AddWithValue("@segmenters", segmenters);
                cmd1.Parameters.AddWithValue("@lymphocytes", lymphocytes);
                cmd1.Parameters.AddWithValue("@monocytes", monocytes);
                cmd1.Parameters.AddWithValue("@eosinophils", eosinophils);
                cmd1.Parameters.AddWithValue("@basophils", basophils);
                cmd1.Parameters.AddWithValue("@stab", stab);
                cmd1.Parameters.AddWithValue("@others", others);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void oldUri()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            int x = 0;
            string color = "";
            string characters = "";
            string protein = "";
            string sugar = "";
            string ph = "";
            string spGr = "";
            string pregnancyTest = "";
            string medtech = "";
            string pathologists = "";
            string daterequested = "";
            string dateexamined = "";

            while (reader.Read())
            {
                x++;
                color = reader.GetString("color");
                characters = reader.GetString("characters");
                protein = reader.GetString("protein");
                sugar = reader.GetString("sugar");
                ph = reader.GetString("ph");
                spGr = reader.GetString("spGr");
                pregnancyTest = reader.GetString("pregnancyTest");
                medtech = reader.GetString("medtech");
                pathologists = reader.GetString("pathologists");
                daterequested = reader.GetDateTime("daterequested").ToString("yyyy-MM-dd");
                dateexamined = reader.GetDateTime("dateexamined").ToString("yyyy-MM-dd");
            }
            conn.Close();

            conn.Open();
            string sqll = "SELECT * FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("@num", lblNumberLab.Text);
            MySqlDataReader readerr = cmdd.ExecuteReader();
            string pus_cells = "";
            string rbc = "";
            string epith_cells = "";
            string bacteria = "";
            string mucus_thread = "";
            string amorphous_urates = "";
            string casts = "";
            string crystals = "";
            string others = "";

            while (readerr.Read())
            {
                pus_cells = readerr.GetString("pus_cells");
                rbc = readerr.GetString("rbc");
                epith_cells = readerr.GetString("epith_cells");
                bacteria = readerr.GetString("bacteria");
                mucus_thread = readerr.GetString("mucus_thread");
                amorphous_urates = readerr.GetString("amorphous_urates");
                casts = readerr.GetString("casts");
                crystals = readerr.GetString("crystals");
                others = readerr.GetString("others");
            }
            conn.Close();

            if (x == 1)
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Urinalysis_Mac VALUES (@num, @color, @characters, @protein, @sugar, @ph, @spGr, @pregnancyTest, @medtech, @pathologists, @daterequested, @dateexamined); INSERT INTO tblArchive_Urinalysis_Mic VALUES (@num, @pus_cells, @rbc, @epith_cells, @bacteria, @mucus_thread, @amorphous_urates, @casts, @crystals, @others); DELETE FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num; DELETE FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@color", color);
                cmd1.Parameters.AddWithValue("@characters", characters);
                cmd1.Parameters.AddWithValue("@protein", protein);
                cmd1.Parameters.AddWithValue("@sugar", sugar);
                cmd1.Parameters.AddWithValue("@ph", ph);
                cmd1.Parameters.AddWithValue("@spGr", spGr);
                cmd1.Parameters.AddWithValue("@pregnancyTest", pregnancyTest);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", daterequested);
                cmd1.Parameters.AddWithValue("@dateexamined", dateexamined);
                cmd1.Parameters.AddWithValue("@pus_cells", pus_cells);
                cmd1.Parameters.AddWithValue("@rbc", rbc);
                cmd1.Parameters.AddWithValue("@epith_cells", epith_cells);
                cmd1.Parameters.AddWithValue("@bacteria", bacteria);
                cmd1.Parameters.AddWithValue("@mucus_thread", mucus_thread);
                cmd1.Parameters.AddWithValue("@amorphous_urates", amorphous_urates);
                cmd1.Parameters.AddWithValue("@casts", casts);
                cmd1.Parameters.AddWithValue("@crystals", crystals);
                cmd1.Parameters.AddWithValue("@others", others);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Open();
                string sqlStatement = "INSERT INTO tblArchive_Urinalysis_Mac VALUES (@num, @color, @characters, @protein, @sugar, @ph, @spGr, @pregnancyTest, @medtech, @pathologists, @daterequested, @dateexamined); INSERT INTO tblArchive_Urinalysis_Mic VALUES (@num, @pus_cells, @rbc, @epith_cells, @bacteria, @mucus_thread, @amorphous_urates, @casts, @crystals, @others); DELETE FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num; DELETE FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sqlStatement, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                cmd1.Parameters.AddWithValue("@color", color);
                cmd1.Parameters.AddWithValue("@characters", characters);
                cmd1.Parameters.AddWithValue("@protein", protein);
                cmd1.Parameters.AddWithValue("@sugar", sugar);
                cmd1.Parameters.AddWithValue("@ph", ph);
                cmd1.Parameters.AddWithValue("@spGr", spGr);
                cmd1.Parameters.AddWithValue("@pregnancyTest", pregnancyTest);
                cmd1.Parameters.AddWithValue("@medtech", medtech);
                cmd1.Parameters.AddWithValue("@pathologists", pathologists);
                cmd1.Parameters.AddWithValue("@daterequested", "0000-00-00");
                cmd1.Parameters.AddWithValue("@dateexamined", "0000-00-00");
                cmd1.Parameters.AddWithValue("@pus_cells", pus_cells);
                cmd1.Parameters.AddWithValue("@rbc", rbc);
                cmd1.Parameters.AddWithValue("@epith_cells", epith_cells);
                cmd1.Parameters.AddWithValue("@bacteria", bacteria);
                cmd1.Parameters.AddWithValue("@mucus_thread", mucus_thread);
                cmd1.Parameters.AddWithValue("@amorphous_urates", amorphous_urates);
                cmd1.Parameters.AddWithValue("@casts", casts);
                cmd1.Parameters.AddWithValue("@crystals", crystals);
                cmd1.Parameters.AddWithValue("@others", others);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    oldBloodChem();

                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Blood_Chemistry VALUES (@num, @fast, @blood, @create, @cholesterol, @trigly, @hdl, @ldl, @vldl, @bloodu, @sodium, @potassium, @chloride, @sgpt, @sgot, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@fast", txtFBS.Text);
                        cmd.Parameters.AddWithValue("@blood", txtBUN.Text);
                        cmd.Parameters.AddWithValue("@create", txtCreatinine.Text);
                        cmd.Parameters.AddWithValue("@cholesterol", txtCholesterol.Text);
                        cmd.Parameters.AddWithValue("@trigly", txtTriglycerides.Text);
                        cmd.Parameters.AddWithValue("@hdl", txtHDL.Text);
                        cmd.Parameters.AddWithValue("@ldl", txtLDL.Text);
                        cmd.Parameters.AddWithValue("@vldl", txtVLDL.Text);
                        cmd.Parameters.AddWithValue("@bloodu", txtBUA.Text);
                        cmd.Parameters.AddWithValue("@sodium", txtSodium.Text);
                        cmd.Parameters.AddWithValue("@potassium", txtPotassium.Text);
                        cmd.Parameters.AddWithValue("@chloride", txtChloride.Text);
                        cmd.Parameters.AddWithValue("@sgpt", txtSGPT.Text);
                        cmd.Parameters.AddWithValue("@sgot", txtSGOT.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Blood_Chemistry SET fasting_blood_sugar = '" + txtFBS.Text + "', blood_urea_nitrogen = '" + txtBUN.Text + "', creatinine = '" + txtCreatinine.Text + "', cholesterol = '" + txtCholesterol.Text + "', triglycerides = '" + txtTriglycerides.Text + "', hdl = '" + txtHDL.Text + "', ldl = '" + txtLDL.Text + "', vldl = '" + txtVLDL.Text + "', blood_uric_acid = '" + txtBUA.Text + "', sodium = '" + txtSodium.Text + "', potassium = '" + txtPotassium.Text + "', chloride = '" + txtChloride.Text + "', sgpt_alt = '" + txtSGPT.Text + "', sgot_ast = '" + txtSGOT.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Activity Log
                    conn.Open();
                    string sql = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("@id", labIdNumber);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        lname = reader1.GetString("lastname");
                        fname = reader1.GetString("firstname");
                        mname = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED BLOOD CHEMISTRY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Blood Chemistry Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    oldFeca();

                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Fecalysis_Mac VALUES (@num, @color, @characters, @reaction, @occult_blood, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@color", txtColorFeca.Text);
                        cmd.Parameters.AddWithValue("@characters", txtCharacterFeca.Text);
                        cmd.Parameters.AddWithValue("@reaction", txtReaction.Text);
                        cmd.Parameters.AddWithValue("@occult_blood", txtOccultBlood.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string sqlStatement1 = "INSERT INTO tblLaboratory_Fecalysis_Mic VALUES (@num, @pus_cells, @rbc, @fat_globules, @macrophage, @bacteria, @parasites_or_ova)";
                        MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                        cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd1.Parameters.AddWithValue("@pus_cells", txtPcells.Text);
                        cmd1.Parameters.AddWithValue("@rbc", txtRbcFeca.Text);
                        cmd1.Parameters.AddWithValue("@fat_globules", txtFlatGobules.Text);
                        cmd1.Parameters.AddWithValue("@macrophage", txtMacrophage.Text);
                        cmd1.Parameters.AddWithValue("@bacteria", txtBacteriaFeca.Text);
                        cmd1.Parameters.AddWithValue("@parasites_or_ova", txtParasites.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Fecalysis_Mac SET color = '" + txtColorFeca.Text + "', characters = '" + txtCharacterFeca.Text + "', reaction = '" + txtReaction.Text + "', occult_blood = '" + txtOccultBlood.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'; UPDATE tblLaboratory_Fecalysis_Mic SET pus_cells = '" + txtPcells.Text + "', rbc = '" + txtRbcFeca.Text + "', fat_globules = '" + txtFlatGobules + "', macrophage = '" + txtMacrophage.Text + "', bacteria = '" + txtBacteriaFeca.Text + "', parasites_or_ova = '" + txtParasites.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'   ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Activity Log
                    conn.Open();
                    string sql = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@id", labIdNumber);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader1 = cmd2.ExecuteReader();
                    while (reader1.Read())
                    {
                        lname = reader1.GetString("lastname");
                        fname = reader1.GetString("firstname");
                        mname = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED FECALYSIS LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Fecalysis Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    oldHema();

                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Hematology VALUES (@num, @hemoglobin, @hematocrit, @wbc_count, @rbc_count, @platelet, @bleeding_time, @clotting_time, @abo_group, @segmenters, @lymphocytes, @monocytes, @eosinophils, @basophils, @stab, @others, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@hemoglobin", txtHemoglobin.Text);
                        cmd.Parameters.AddWithValue("@hematocrit", txtHematocrit.Text);
                        cmd.Parameters.AddWithValue("@wbc_count", txtWBC.Text);
                        cmd.Parameters.AddWithValue("@rbc_count", txtRbcHema.Text);
                        cmd.Parameters.AddWithValue("@platelet", txtPlatelet.Text);
                        cmd.Parameters.AddWithValue("@bleeding_time", txtBleeding.Text);
                        cmd.Parameters.AddWithValue("@clotting_time", txtClotting.Text);
                        cmd.Parameters.AddWithValue("@abo_group", txtABO.Text);
                        cmd.Parameters.AddWithValue("@segmenters", txtSegmenters.Text);
                        cmd.Parameters.AddWithValue("@lymphocytes", txtLymphocytes.Text);
                        cmd.Parameters.AddWithValue("@monocytes", txtMonocytes.Text);
                        cmd.Parameters.AddWithValue("@eosinophils", txtEosinophils.Text);
                        cmd.Parameters.AddWithValue("@basophils", txtBasophils.Text);
                        cmd.Parameters.AddWithValue("@stab", txtStab.Text);
                        cmd.Parameters.AddWithValue("@others", txtOthersHema.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Hematology SET hemoglobin = '" + txtHemoglobin.Text + "', hematocrit = '" + txtHematocrit.Text + "', wbc_count = '" + txtWBC.Text + "', rbc_count = '" + txtRbcHema + "', platelet = '" + txtPlatelet.Text + "', bleeding_time = '" + txtBleeding.Text + "', clotting_time = '" + txtClotting.Text + "', abo_group = '" + txtABO.Text + "', segmenters = '" + txtSegmenters.Text + "', lymphocytes = '" + txtLymphocytes.Text + "', monocytes = '" + txtMonocytes.Text + "', eosinophils = '" + txtEosinophils.Text + "', basophils = '" + txtBasophils.Text + "', stab = '" + txtStab.Text + "', others = '" + txtOthersHema.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Activity Log
                    conn.Open();
                    string sql = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("@id", labIdNumber);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        lname = reader1.GetString("lastname");
                        fname = reader1.GetString("firstname");
                        mname = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED HEMATOLOGY LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Hematology Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (cboMedTech.Text != "" && cboPathologist.Text != "")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    oldUri();

                    conn.Open();
                    MySqlCommand cm = new MySqlCommand("SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @pnum ", conn);
                    cm.Parameters.AddWithValue("@pnum", lblNumberLab.Text);
                    MySqlDataReader dr = cm.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sqlStatement = "INSERT INTO tblLaboratory_Urinalysis_Mac VALUES (@num, @color, @character, @protein, @sugar, @ph, @sp, @ptest, @medtech, @pathologists, @daterequested, @dateexamined)";
                        MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
                        cmd.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd.Parameters.AddWithValue("@color", txtColor.Text);
                        cmd.Parameters.AddWithValue("@character", txtCharacter.Text);
                        cmd.Parameters.AddWithValue("@protein", txtProtein.Text);
                        cmd.Parameters.AddWithValue("@sugar", txtSugar.Text);
                        cmd.Parameters.AddWithValue("@ph", txtPH.Text);
                        cmd.Parameters.AddWithValue("@sp", txtSpGr.Text);
                        cmd.Parameters.AddWithValue("@ptest", txtPregnancyTest.Text);
                        cmd.Parameters.AddWithValue("@medtech", cboMedTech.Text);
                        cmd.Parameters.AddWithValue("@pathologists", cboPathologist.Text);
                        cmd.Parameters.AddWithValue("@daterequested", dtRequestedLab.Value);
                        cmd.Parameters.AddWithValue("@dateexamined", dtExaminedLab.Value);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        string sqlStatement1 = "INSERT INTO tblLaboratory_Urinalysis_Mic VALUES (@num, @pus, @rbc, @epith, @bacteria, @mucus, @amor, @casts, @crystals, @others)";
                        MySqlCommand cmd1 = new MySqlCommand(sqlStatement1, conn);
                        cmd1.Parameters.AddWithValue("@num", lblNumberLab.Text);
                        cmd1.Parameters.AddWithValue("@pus", txtPUSCells.Text);
                        cmd1.Parameters.AddWithValue("@rbc", txtRBC.Text);
                        cmd1.Parameters.AddWithValue("@epith", txtEpithCells.Text);
                        cmd1.Parameters.AddWithValue("@bacteria", txtBacteria.Text);
                        cmd1.Parameters.AddWithValue("@mucus", txtMucusThread.Text);
                        cmd1.Parameters.AddWithValue("@amor", txtAmorphousUrates.Text);
                        cmd1.Parameters.AddWithValue("@casts", txtCasts.Text);
                        cmd1.Parameters.AddWithValue("@crystals", txtCasts.Text);
                        cmd1.Parameters.AddWithValue("@others", txtOthers.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cm1 = new MySqlCommand("UPDATE tblLaboratory_Urinalysis_Mac SET color = '" + txtColor.Text + "', characters = '" + txtCharacter.Text + "', protein = '" + txtProtein.Text + "', sugar = '" + txtSugar.Text + "', ph = '" + txtPH.Text + "', spGr = '" + txtSpGr.Text + "', pregnancyTest = '" + txtPregnancyTest.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "'; UPDATE tblLaboratory_Urinalysis_Mic SET pus_cells = '" + txtPUSCells.Text + "', rbc = '" + txtRBC.Text + "', epith_cells = '" + txtEpithCells.Text + "', bacteria = '" + txtBacteria.Text + "', mucus_thread = '" + txtMucusThread.Text + "', amorphous_urates = '" + txtAmorphousUrates.Text + "', casts = '" + txtCasts.Text + "', crystals = '" + txtCrystals.Text + "', others = '" + txtOthers.Text + "' WHERE patientsNo = '" + lblNumberLab.Text + "' ", conn);
                        cm1.ExecuteNonQuery();
                        conn.Close();
                    }

                    //Activity Log
                    conn.Open();
                    string sql = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@id", labIdNumber);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    MySqlDataReader reader1 = cmd2.ExecuteReader();
                    while (reader1.Read())
                    {
                        lname = reader1.GetString("lastname");
                        fname = reader1.GetString("firstname");
                        mname = reader1.GetString("middlename");
                    }
                    conn.Close();

                    string acctype = "LABORATORY 1";
                    string activityy = "SUCCESSFULLY SAVED URINALYSIS LABORATORY EXAM OF PATIENT(" + lblNameLab.Text + ").";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Patient's Urinalysis Laboratory Examination successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabExams.Items.Remove(cboLabExams.SelectedItem);
                }
                else
                {
                    if (cboMedTech.Text == "")
                        MessageBox.Show("Select Name of Medical Technologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (cboPathologist.Text == "")
                        MessageBox.Show("Select Name of Pathologists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cboLabExams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLabExams.Text == "ROUTINE URINALYSIS - CLINICAL MICROSCOPY")
            {
                metroTabControl2.SelectedIndex = 3;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "STOOL ANALYSIS - CLINICAL MICROSCOPY")
            {
                metroTabControl2.SelectedIndex = 1;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "PREGNANCY TEST - CLINICAL MICROSCOPY")
            {
                metroTabControl2.SelectedIndex = 3;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "SEMENALYSIS - CLINICAL MICROSCOPY")
            {
                metroTabControl2.SelectedIndex = 3;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "CBC - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "CTBT - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "PLATELET COUNT - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "PERIPHERAL SMEAR - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "PROTIME - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "ESR - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "WIDAL TEST - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "DENGUE DOT - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "TYPHIDOT DOT - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "COMPLETE HEPATITIS PROFILE - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "FBS - HEMATOLOGY")
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "BUN - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "SERUM CREATININE - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "TOTAL CHOLESTEROL - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "BLOOD URIC ACID - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "TRIGLYCERIDES - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "HDL - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "SGOT (AST) - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "SGPT (ALT) - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "SODIUM (Na+) - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "POTASIUM (K+) - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "T3 - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "T4 - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "TSH - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "HBA1C - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "FT3 - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (cboLabExams.Text == "FT4 - BLOOD CHEMISTRY")
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
        }

        private void cboLabExams_Click(object sender, EventArgs e)
        {
            if (cboLabExams.Items.Count == 0)
                btnSendTo.Enabled = true;
        }
    }
}
