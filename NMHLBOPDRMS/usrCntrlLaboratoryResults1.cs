using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NMHLBOPDRMS
{
    public partial class usrCntrlLaboratoryResults1 : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public usrCntrlLaboratoryResults1()
        {
            InitializeComponent();
        }

        private void usrCntrlLaboratoryResults1_Load(object sender, EventArgs e)
        {
            getDataset();
            dgLaboratoryResults.Columns[0].Width = 125;
            dgLaboratoryResults.Columns[1].Width = 300;
            dgLaboratoryResults.Columns[2].Width = 100;
            dgLaboratoryResults.Columns[3].Width = 100;
            dgLaboratoryResults.Columns[3].Width = 100;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblLaboratory1_Results", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        public void getDataset()
        {
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient No.', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', status AS 'Status', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory1_Results";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tblLaboratory1_Results");
            dgLaboratoryResults.DataMember = "tblLaboratory1_Results";
            dgLaboratoryResults.DataSource = dS;
            conn.Close();
        }

        private void dgLaboratoryResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string number = dgLaboratoryResults.Rows[e.RowIndex].Cells[0].Value.ToString();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory1_Results WHERE patientNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            string lname = "";
            string fname = "";
            string mname = "";
            string status = "";
            string daterequested = "";
            string dateexamined = "";
            while (reader.Read())
            {
                lname = reader.GetString("lastName");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                status = reader.GetString("status");
                daterequested = reader.GetString("daterequested");
                dateexamined = reader.GetString("dateexamined");
            }
            conn.Close();

            frmLaboratory1 f1 = new frmLaboratory1();

            conn.Open();
            string sql1 = "SELECT * FROM tblLab1_Exam WHERE patientNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string labExam = "";
            while (reader.Read())
            {
                labExam = reader.GetString("labExam");
                f1.cboLabExams.Items.Add(labExam);
            }
            conn.Close();

            f1.btnSendTo.Visible = false;
            f1.btnSubmitBC.Visible = false;
            f1.btnSubmitFecalysis.Visible = false;
            f1.btnSubmitHematology.Visible = false;
            f1.btnSubmitUrinalysis.Visible = false;
            f1.btnSave1.Visible = false;
            f1.btnSave2.Visible = false;
            f1.btnSave3.Visible = false;
            f1.btnSave4.Visible = false;
            f1.lblStatus.Text = status;
            f1.lblNumberLab.Text = number;
            f1.lblNameLab.Text = lname + ", " + fname + " " + mname;
            f1.dtRequestedLab.Text = daterequested;
            f1.dtExaminedLab.Text = dateexamined;

            //Blood Chemistry
            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            string fasting_blood_sugar = "";
            string blood_urea_nitrogen = "";
            string creatinine = "";
            string cholesterol = "";
            string triglycerides = "";
            string hdl = "";
            string ldl = "";
            string vldl = "";
            string blood_uric_acid = "";
            string sodium = "";
            string potassium = "";
            string chloride = "";
            string sgpt_alt = "";
            string sgot_ast = "";
            while (reader2.Read())
            {
                fasting_blood_sugar = reader2.GetString("fasting_blood_sugar");
                blood_urea_nitrogen = reader2.GetString("blood_urea_nitrogen");
                creatinine = reader2.GetString("creatinine");
                cholesterol = reader2.GetString("cholesterol");
                triglycerides = reader2.GetString("triglycerides");
                hdl = reader2.GetString("hdl");
                ldl = reader2.GetString("ldl");
                vldl = reader2.GetString("vldl");
                blood_uric_acid = reader2.GetString("blood_uric_acid");
                sodium = reader2.GetString("sodium");
                potassium = reader2.GetString("potassium");
                chloride = reader2.GetString("chloride");
                sgpt_alt = reader2.GetString("sgpt_alt");
                sgot_ast = reader2.GetString("sgot_ast");
            }
            conn.Close();

            f1.txtFBS.Enabled = false;
            f1.txtBUN.Enabled = false;
            f1.txtCreatinine.Enabled = false;
            f1.txtCholesterol.Enabled = false;
            f1.txtTriglycerides.Enabled = false;
            f1.txtHDL.Enabled = false;
            f1.txtLDL.Enabled = false;
            f1.txtVLDL.Enabled = false;
            f1.txtBUA.Enabled = false;
            f1.txtSodium.Enabled = false;
            f1.txtPotassium.Enabled = false;
            f1.txtChloride.Enabled = false;
            f1.txtSGPT.Enabled = false;
            f1.txtSGOT.Enabled = false;
            f1.dtExaminedLab.Enabled = false;

            f1.txtFBS.Text = fasting_blood_sugar;
            f1.txtBUN.Text = blood_urea_nitrogen;
            f1.txtCreatinine.Text = creatinine;
            f1.txtCholesterol.Text = cholesterol;
            f1.txtTriglycerides.Text = triglycerides;
            f1.txtHDL.Text = hdl;
            f1.txtLDL.Text = ldl;
            f1.txtVLDL.Text = vldl;
            f1.txtBUA.Text = blood_uric_acid;
            f1.txtSodium.Text = sodium;
            f1.txtPotassium.Text = potassium;
            f1.txtChloride.Text = chloride;
            f1.txtSGPT.Text = sgpt_alt;
            f1.txtSGOT.Text = sgot_ast;

            //Fecalysis
            conn.Open();
            string sql3 = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            string color = "";
            string characters = "";
            string reaction = "";
            string occult_blood = "";
            while (reader3.Read())
            {
                color = reader3.GetString("color");
                characters = reader3.GetString("characters");
                reaction = reader3.GetString("reaction");
                occult_blood = reader3.GetString("occult_blood");
            }
            conn.Close();

            conn.Open();
            string sql4 = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            string pus_cells = "";
            string rbc = "";
            string fat_globules = "";
            string macrophage = "";
            string bacteria = "";
            string parasites_or_ova = "";
            while (reader4.Read())
            {
                pus_cells = reader4.GetString("pus_cells");
                rbc = reader4.GetString("rbc");
                fat_globules = reader4.GetString("fat_globules");
                macrophage = reader4.GetString("macrophage");
                bacteria = reader4.GetString("bacteria");
                parasites_or_ova = reader4.GetString("parasites_or_ova");
            }
            conn.Close();

            f1.txtColorFeca.Enabled = false;
            f1.txtCharacterFeca.Enabled = false;
            f1.txtReaction.Enabled = false;
            f1.txtOccultBlood.Enabled = false;
            f1.txtPUSCells.Enabled = false;
            f1.txtRbcFeca.Enabled = false;
            f1.txtFlatGobules.Enabled = false;
            f1.txtMacrophage.Enabled = false;
            f1.txtBacteriaFeca.Enabled = false;
            f1.txtParasites.Enabled = false;

            f1.txtColorFeca.Text = color;
            f1.txtCharacterFeca.Text = characters;
            f1.txtReaction.Text = reaction;
            f1.txtOccultBlood.Text = occult_blood;
            f1.txtPUSCells.Text = pus_cells;
            f1.txtRbcFeca.Text = rbc;
            f1.txtFlatGobules.Text = fat_globules;
            f1.txtMacrophage.Text = macrophage;
            f1.txtBacteriaFeca.Text = bacteria;
            f1.txtParasites.Text = parasites_or_ova;

            //Hematology
            conn.Open();
            string sql5 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
            cmd5.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader5 = cmd5.ExecuteReader();
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
            while (reader5.Read())
            {
                hemoglobin = reader5.GetString("hemoglobin");
                hematocrit = reader5.GetString("hematocrit");
                wbc_count = reader5.GetString("wbc_count");
                rbc_count = reader5.GetString("rbc_count");
                platelet = reader5.GetString("platelet");
                bleeding_time = reader5.GetString("bleeding_time");
                clotting_time = reader5.GetString("clotting_time");
                abo_group = reader5.GetString("abo_group");
                segmenters = reader5.GetString("segmenters");
                lymphocytes = reader5.GetString("lymphocytes");
                monocytes = reader5.GetString("monocytes");
                eosinophils = reader5.GetString("eosinophils");
                basophils = reader5.GetString("basophils");
                stab = reader5.GetString("stab");
                others = reader5.GetString("others");
            }
            conn.Close();

            f1.txtHemoglobin.Enabled = false;
            f1.txtHematocrit.Enabled = false;
            f1.txtWBC.Enabled = false;
            f1.txtRbcHema.Enabled = false;
            f1.txtPlatelet.Enabled = false;
            f1.txtBleeding.Enabled = false;
            f1.txtClotting.Enabled = false;
            f1.txtABO.Enabled = false;
            f1.txtSegmenters.Enabled = false;
            f1.txtLymphocytes.Enabled = false;
            f1.txtMonocytes.Enabled = false;
            f1.txtEosinophils.Enabled = false;
            f1.txtBasophils.Enabled = false;
            f1.txtStab.Enabled = false;
            f1.txtOthersHema.Enabled = false;

            f1.txtHemoglobin.Text = hemoglobin;
            f1.txtHematocrit.Text = hematocrit;
            f1.txtWBC.Text = wbc_count;
            f1.txtRbcHema.Text = rbc_count;
            f1.txtPlatelet.Text = platelet;
            f1.txtBleeding.Text = bleeding_time;
            f1.txtClotting.Text = clotting_time;
            f1.txtABO.Text = abo_group;
            f1.txtSegmenters.Text = segmenters;
            f1.txtLymphocytes.Text = lymphocytes;
            f1.txtMonocytes.Text = monocytes;
            f1.txtEosinophils.Text = eosinophils;
            f1.txtBasophils.Text = basophils;
            f1.txtStab.Text = stab;
            f1.txtOthersHema.Text = others;

            //Urinalysis
            conn.Open();
            string sql6 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
            cmd6.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader6 = cmd6.ExecuteReader();
            string colorU = "";
            string charactersU = "";
            string protein = "";
            string sugar = "";
            string ph = "";
            string spGr = "";
            string pregnancyTest = "";
            while (reader6.Read())
            {
                colorU = reader6.GetString("color");
                charactersU = reader6.GetString("characters");
                protein = reader6.GetString("protein");
                sugar = reader6.GetString("sugar");
                ph = reader6.GetString("ph");
                spGr = reader6.GetString("spGr");
                pregnancyTest = reader6.GetString("pregnancyTest");
            }
            conn.Close();

            conn.Open();
            string sql7 = "SELECT * FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd7 = new MySqlCommand(sql7, conn);
            cmd7.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader7 = cmd7.ExecuteReader();
            string pus_cellsU = "";
            string rbcU = "";
            string epith_cells = "";
            string bacteriaU = "";
            string mucus_thread = "";
            string amorphous_urates = "";
            string casts = "";
            string crystals = "";
            string othersU = "";
            while (reader7.Read())
            {
                pus_cellsU = reader7.GetString("pus_cells");
                rbcU = reader7.GetString("rbc");
                epith_cells = reader7.GetString("epith_cells");
                bacteriaU = reader7.GetString("bacteria");
                mucus_thread = reader7.GetString("mucus_thread");
                amorphous_urates = reader7.GetString("amorphous_urates");
                casts = reader7.GetString("casts");
                crystals = reader7.GetString("crystals");
                othersU = reader7.GetString("others");
            }
            conn.Close();

            f1.txtColor.Enabled = false;
            f1.txtCharacter.Enabled = false;
            f1.txtProtein.Enabled = false;
            f1.txtSugar.Enabled = false;
            f1.txtPH.Enabled = false;
            f1.txtSpGr.Enabled = false;
            f1.txtPregnancyTest.Enabled = false;
            f1.txtPUSCells.Enabled = false;
            f1.txtRBC.Enabled = false;
            f1.txtEpithCells.Enabled = false;
            f1.txtBacteria.Enabled = false;
            f1.txtMucusThread.Enabled = false;
            f1.txtAmorphousUrates.Enabled = false;
            f1.txtCasts.Enabled = false;
            f1.txtCrystals.Enabled = false;
            f1.txtOthers.Enabled = false;

            f1.txtColor.Text = colorU;
            f1.txtCharacter.Text = charactersU;
            f1.txtProtein.Text = protein;
            f1.txtSugar.Text = sugar;
            f1.txtPH.Text = ph;
            f1.txtSpGr.Text = spGr;
            f1.txtPregnancyTest.Text = pregnancyTest;
            f1.txtPUSCells.Text = pus_cellsU;
            f1.txtRBC.Text = rbcU;
            f1.txtEpithCells.Text = epith_cells;
            f1.txtBacteria.Text = bacteriaU;
            f1.txtMucusThread.Text = mucus_thread;
            f1.txtAmorphousUrates.Text = amorphous_urates;
            f1.txtCasts.Text = casts;
            f1.txtCrystals.Text = crystals;
            f1.txtOthers.Text = othersU;


            f1.Show();
        }
    }
}
