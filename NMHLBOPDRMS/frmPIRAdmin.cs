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
using Microsoft.Reporting.WinForms;

namespace NMHLBOPDRMS
{
    public partial class frmPIRAdmin : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        public string accountType = "";

        public frmPIRAdmin()
        {
            InitializeComponent();
        }

        private void frmPIRAdmin_Load(object sender, EventArgs e)
        {
            hide();
            dtPicker.Text = DateTime.Now.ToShortDateString();

            #region Datagridview cell width
            //Medical Information Table
            dgOldRecordMedicalInformation.Columns[0].Width = 100;
            dgOldRecordMedicalInformation.Columns[1].Width = 100;
            dgOldRecordMedicalInformation.Columns[2].Width = 100;
            dgOldRecordMedicalInformation.Columns[3].Width = 100;
            dgOldRecordMedicalInformation.Columns[4].Width = 100;
            dgOldRecordMedicalInformation.Columns[5].Width = 100;
            dgOldRecordMedicalInformation.Columns[6].Width = 100;
            dgOldRecordMedicalInformation.Columns[7].Width = 100;
            dgOldRecordMedicalInformation.Columns[8].Width = 100;
            dgOldRecordMedicalInformation.Columns[9].Width = 100;
            dgOldRecordMedicalInformation.Columns[10].Width = 100;
            dgOldRecordMedicalInformation.Columns[11].Width = 100;
            dgOldRecordMedicalInformation.Columns[12].Width = 100;

            //Medical Diagnosis Table
            dgOldRecordMedicalDiagnosis.Columns[0].Width = 100;
            dgOldRecordMedicalDiagnosis.Columns[1].Width = 150;
            dgOldRecordMedicalDiagnosis.Columns[2].Width = 100;
            dgOldRecordMedicalDiagnosis.Columns[3].Width = 100;
            dgOldRecordMedicalDiagnosis.Columns[4].Width = 350;
            dgOldRecordMedicalDiagnosis.Columns[5].Width = 100;

            //Laboratory Blood Chemistry Table
            dgCurrentRecordBloodChemistry.Columns[0].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[1].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[2].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[3].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[4].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[5].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[6].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[7].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[8].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[9].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[10].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[11].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[12].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[13].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[14].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[15].Width = 150;
            dgCurrentRecordBloodChemistry.Columns[16].Width = 150;
            dgCurrentRecordBloodChemistry.Columns[17].Width = 100;
            dgCurrentRecordBloodChemistry.Columns[18].Width = 100;

            dgOldRecordBloodChemistry.Columns[0].Width = 100;
            dgOldRecordBloodChemistry.Columns[1].Width = 100;
            dgOldRecordBloodChemistry.Columns[2].Width = 100;
            dgOldRecordBloodChemistry.Columns[3].Width = 100;
            dgOldRecordBloodChemistry.Columns[4].Width = 100;
            dgOldRecordBloodChemistry.Columns[5].Width = 100;
            dgOldRecordBloodChemistry.Columns[6].Width = 100;
            dgOldRecordBloodChemistry.Columns[7].Width = 100;
            dgOldRecordBloodChemistry.Columns[8].Width = 100;
            dgOldRecordBloodChemistry.Columns[9].Width = 100;
            dgOldRecordBloodChemistry.Columns[10].Width = 100;
            dgOldRecordBloodChemistry.Columns[11].Width = 100;
            dgOldRecordBloodChemistry.Columns[12].Width = 100;
            dgOldRecordBloodChemistry.Columns[13].Width = 100;
            dgOldRecordBloodChemistry.Columns[14].Width = 100;
            dgOldRecordBloodChemistry.Columns[15].Width = 150;
            dgOldRecordBloodChemistry.Columns[16].Width = 150;
            dgOldRecordBloodChemistry.Columns[17].Width = 100;
            dgOldRecordBloodChemistry.Columns[18].Width = 100;

            //Laboratory Fecalysis Table
            //Macroscopic
            dgCurrentRecordFecalysisMacro.Columns[0].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[1].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[2].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[3].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[4].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[5].Width = 150;
            dgCurrentRecordFecalysisMacro.Columns[6].Width = 150;
            dgCurrentRecordFecalysisMacro.Columns[7].Width = 100;
            dgCurrentRecordFecalysisMacro.Columns[8].Width = 100;

            dgOldRecordFecalysisMacro.Columns[0].Width = 100;
            dgOldRecordFecalysisMacro.Columns[1].Width = 100;
            dgOldRecordFecalysisMacro.Columns[2].Width = 100;
            dgOldRecordFecalysisMacro.Columns[3].Width = 100;
            dgOldRecordFecalysisMacro.Columns[4].Width = 100;
            dgOldRecordFecalysisMacro.Columns[5].Width = 150;
            dgOldRecordFecalysisMacro.Columns[6].Width = 150;
            dgOldRecordFecalysisMacro.Columns[7].Width = 100;
            dgOldRecordFecalysisMacro.Columns[8].Width = 100;

            //Microscopic
            dgCurrentRecordFecalysisMicro.Columns[0].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[1].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[2].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[3].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[4].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[5].Width = 100;
            dgCurrentRecordFecalysisMicro.Columns[6].Width = 100;

            dgOldRecordFecalysisMicro.Columns[0].Width = 100;
            dgOldRecordFecalysisMicro.Columns[1].Width = 100;
            dgOldRecordFecalysisMicro.Columns[2].Width = 100;
            dgOldRecordFecalysisMicro.Columns[3].Width = 100;
            dgOldRecordFecalysisMicro.Columns[4].Width = 100;
            dgOldRecordFecalysisMicro.Columns[5].Width = 100;
            dgOldRecordFecalysisMicro.Columns[6].Width = 100;

            //Laboratory Hematology Table
            dgCurrentRecordHematology.Columns[0].Width = 100;
            dgCurrentRecordHematology.Columns[1].Width = 100;
            dgCurrentRecordHematology.Columns[2].Width = 100;
            dgCurrentRecordHematology.Columns[3].Width = 100;
            dgCurrentRecordHematology.Columns[4].Width = 100;
            dgCurrentRecordHematology.Columns[5].Width = 100;
            dgCurrentRecordHematology.Columns[6].Width = 100;
            dgCurrentRecordHematology.Columns[7].Width = 100;
            dgCurrentRecordHematology.Columns[8].Width = 100;
            dgCurrentRecordHematology.Columns[9].Width = 100;
            dgCurrentRecordHematology.Columns[10].Width = 100;
            dgCurrentRecordHematology.Columns[11].Width = 100;
            dgCurrentRecordHematology.Columns[12].Width = 100;
            dgCurrentRecordHematology.Columns[13].Width = 100;
            dgCurrentRecordHematology.Columns[14].Width = 100;
            dgCurrentRecordHematology.Columns[15].Width = 150;
            dgCurrentRecordHematology.Columns[16].Width = 150;
            dgCurrentRecordHematology.Columns[17].Width = 150;
            dgCurrentRecordHematology.Columns[18].Width = 100;
            dgCurrentRecordHematology.Columns[19].Width = 100;

            dgOldRecordHematology.Columns[0].Width = 100;
            dgOldRecordHematology.Columns[1].Width = 100;
            dgOldRecordHematology.Columns[2].Width = 100;
            dgOldRecordHematology.Columns[3].Width = 100;
            dgOldRecordHematology.Columns[4].Width = 100;
            dgOldRecordHematology.Columns[5].Width = 100;
            dgOldRecordHematology.Columns[6].Width = 100;
            dgOldRecordHematology.Columns[7].Width = 100;
            dgOldRecordHematology.Columns[8].Width = 100;
            dgOldRecordHematology.Columns[9].Width = 100;
            dgOldRecordHematology.Columns[10].Width = 100;
            dgOldRecordHematology.Columns[11].Width = 100;
            dgOldRecordHematology.Columns[12].Width = 100;
            dgOldRecordHematology.Columns[13].Width = 100;
            dgOldRecordHematology.Columns[14].Width = 100;
            dgOldRecordHematology.Columns[15].Width = 150;
            dgOldRecordHematology.Columns[16].Width = 150;
            dgOldRecordHematology.Columns[17].Width = 150;
            dgOldRecordHematology.Columns[18].Width = 100;
            dgOldRecordHematology.Columns[19].Width = 100;

            //Laboratory Urinalysis Table
            //Macroscopic
            dgCurrentRecordUrinalysisMacro.Columns[0].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[1].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[2].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[3].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[4].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[5].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[6].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[7].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[8].Width = 150;
            dgCurrentRecordUrinalysisMacro.Columns[9].Width = 150;
            dgCurrentRecordUrinalysisMacro.Columns[10].Width = 100;
            dgCurrentRecordUrinalysisMacro.Columns[11].Width = 100;

            dgOldRecordUrinalysisMacro.Columns[0].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[1].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[2].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[3].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[4].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[5].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[6].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[7].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[8].Width = 150;
            dgOldRecordUrinalysisMacro.Columns[9].Width = 150;
            dgOldRecordUrinalysisMacro.Columns[10].Width = 100;
            dgOldRecordUrinalysisMacro.Columns[11].Width = 100;

            //Microscopic
            dgCurrentRecordUrinalysisMicro.Columns[0].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[1].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[2].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[3].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[4].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[5].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[6].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[7].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[8].Width = 100;
            dgCurrentRecordUrinalysisMicro.Columns[9].Width = 150;

            dgOldRecordUrinalysisMicro.Columns[0].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[1].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[2].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[3].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[4].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[5].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[6].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[7].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[8].Width = 100;
            dgOldRecordUrinalysisMicro.Columns[9].Width = 150;

            //Laboratory Ultrasound Table
            dgCurrentRecordUltrasound.Columns[0].Width = 100;
            dgCurrentRecordUltrasound.Columns[1].Width = 150;
            dgCurrentRecordUltrasound.Columns[2].Width = 250;
            dgCurrentRecordUltrasound.Columns[3].Width = 300;
            dgCurrentRecordUltrasound.Columns[4].Width = 150;
            dgCurrentRecordUltrasound.Columns[5].Width = 100;
            dgCurrentRecordUltrasound.Columns[6].Width = 100;

            dgOldRecordUltrasound.Columns[0].Width = 100;
            dgOldRecordUltrasound.Columns[1].Width = 150;
            dgOldRecordUltrasound.Columns[2].Width = 250;
            dgOldRecordUltrasound.Columns[3].Width = 300;
            dgOldRecordUltrasound.Columns[4].Width = 150;
            dgOldRecordUltrasound.Columns[5].Width = 100;
            dgOldRecordUltrasound.Columns[6].Width = 100;

            //Laboratory Xray Table
            dgCurrentRecordXray.Columns[0].Width = 100;
            dgCurrentRecordXray.Columns[1].Width = 150;
            dgCurrentRecordXray.Columns[2].Width = 250;
            dgCurrentRecordXray.Columns[3].Width = 300;
            dgCurrentRecordXray.Columns[4].Width = 150;
            dgCurrentRecordXray.Columns[5].Width = 100;
            dgCurrentRecordXray.Columns[6].Width = 100;

            dgOldRecordXray.Columns[0].Width = 100;
            dgOldRecordXray.Columns[1].Width = 150;
            dgOldRecordXray.Columns[2].Width = 250;
            dgOldRecordXray.Columns[3].Width = 300;
            dgOldRecordXray.Columns[4].Width = 150;
            dgOldRecordXray.Columns[5].Width = 100;
            dgOldRecordXray.Columns[6].Width = 100;

            //Medical Treatment Table
            dgOldRecordTreatment.Columns[0].Width = 150;
            dgOldRecordTreatment.Columns[1].Width = 700;
            dgOldRecordTreatment.Columns[2].Width = 220;
            dgOldRecordTreatment.Columns[3].Width = 100;
            #endregion
        }

        void hide()
        {
            grpBloodChem.Hide();
            grpFecalysis.Hide();
            grpHematology.Hide();
            grpUltrasound.Hide();
            grpUrinalysis.Hide();
            grpXray.Hide();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblBackNurse_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblAccounts WHERE accountType = 'NURSE' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int x = 0;
            string num = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                x++;
                num = reader.GetString("IDNumber");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@id", num);
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

            this.Hide();
            frmMainNurse f1 = new frmMainNurse();
            f1.lblNameOfNurse.Text = lname + ", " + fname + " " + mname;
            f1.Show();

        }

        private void cboResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboResults.Text == "Blood Chemistry")
            {
                grpBloodChem.Show();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboResults.Text == "Fecalysis")
            {
                grpFecalysis.Show();
                grpBloodChem.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboResults.Text == "Hematology")
            {
                grpHematology.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboResults.Text == "Urinalysis")
            {
                grpUrinalysis.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUltrasound.Hide();
                grpXray.Hide();
            }
            else if (cboResults.Text == "Ultrasound")
            {
                grpUltrasound.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpXray.Hide();
            }
            else if (cboResults.Text == "X-ray")
            {
                grpXray.Show();
                grpBloodChem.Hide();
                grpFecalysis.Hide();
                grpHematology.Hide();
                grpUrinalysis.Hide();
                grpUltrasound.Hide();
            }
        }

        private void dgCurrentRecordBloodChemistry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordBloodChemistry.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY BLOOD CHEMISTRY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintBloodChemistry f1 = new frmPrintBloodChemistry();
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

        private void dgOldRecordBloodChemistry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string number = dgCurrentRecordBloodChemistry.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @number";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@number", number);
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
            string sql1 = "SELECT * FROM tblArchive_Blood_Chemistry WHERE patientsNo = @number";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@number", number);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY BLOOD CHEMISTRY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintBloodChemistry f1 = new frmPrintBloodChemistry();
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

        private void dgCurrentRecordFecalysisMacro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordFecalysisMacro.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY FECALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintFecalysis f1 = new frmPrintFecalysis();
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

        private void dgOldRecordFecalysisMacro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgOldRecordFecalysisMacro.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblArchive_Fecalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
                fat_globules = reader1.GetString("fat_gobules");
                macrophage = reader1.GetString("macrophage");
                bacteria = reader1.GetString("bacteria");
                parasites_or_ova = reader1.GetString("parasites_or_ova");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblArchive_Fecalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY FECALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintFecalysis f1 = new frmPrintFecalysis();
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

        private void dgCurrentRecordHematology_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordHematology.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY HEMATOLOGY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintHematology f1 = new frmPrintHematology();
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

        private void dgOldRecordHematology_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgOldRecordHematology.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblArchive_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY HEMATOLOGY EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintHematology f1 = new frmPrintHematology();
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

        private void dgCurrentRecordUrinalysisMacro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordUrinalysisMacro.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY URINALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintUrinalysis f1 = new frmPrintUrinalysis();
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

        private void dgOldRecordUrinalysisMacro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgOldRecordUrinalysisMacro.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
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
            string sql1 = "SELECT * FROM tblArchive_Urinalysis_Mac WHERE idNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", pnumber);
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
            string sql2 = "SELECT * FROM tblArchive_Urinalysis_Mic WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
            string activityy = "PRINTED LABORATORY URINALYSIS EXAM OF PATIENT(" + fname + " " + mname + " " + lname + ").";

            conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

            frmPrintUrinalysis f1 = new frmPrintUrinalysis();
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

        private void dgOldRecordMedicalInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblTemp.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblBp.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblCR.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[3].Value.ToString();
            lblRR.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[4].Value.ToString();
            lblWeight.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[5].Value.ToString();
            lblHeight.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[6].Value.ToString();
            lblBMI.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[7].Value.ToString();
            lblBMIStat.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[8].Value.ToString();
            lblComplaints.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[9].Value.ToString();
            lblDays.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[11].Value.ToString();
            lblWeek.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[12].Value.ToString();
            dtCheckup.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[13].Value.ToString();
            dtOld.Text = dgOldRecordMedicalInformation.Rows[e.RowIndex].Cells[13].Value.ToString();

            lstSymptoms.Items.Clear();
            conn.Open();
            string sqll = "SELECT * FROM tblOld_Medical_Information WHERE patientsNo = @num";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("@num", lblNumber.Text);
            MySqlDataReader readerr = cmdd.ExecuteReader();
            string symptoms = "";
            while (readerr.Read())
            {
                symptoms = readerr.GetString("symptoms");
                lstSymptoms.Items.Add(symptoms);
            }
            conn.Close();

            lbl1.Visible = false;
            metroTabControl2.SelectedIndex = 0;
            metroTabControl2.Refresh();
        }


        private void dgOldRecordMedicalDiagnosis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblDoctor.Text = "Dr. " + dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblOldNew.Text = dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblAdmission.Text = dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[3].Value.ToString();
            lblNotes.Text = dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[5].Value.ToString();
            dtCheckupDiagnosis.Text = dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[6].Value.ToString();
            dtOld.Text = dgOldRecordMedicalDiagnosis.Rows[e.RowIndex].Cells[6].Value.ToString();

            lstDiagnosis.Items.Clear();

            conn.Open();
            string sqll = "SELECT * FROM tblOld_Medical_Diagnosis WHERE patientsNo = @num";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("@num", lblNumber.Text);
            MySqlDataReader readerr = cmdd.ExecuteReader();
            string diagnosis = "";
            while (readerr.Read())
            {
                diagnosis = readerr.GetString("diagnosis");
                lstDiagnosis.Items.Add(diagnosis);
            }
            conn.Close();

            lbl1.Visible = false;
            metroTabControl2.SelectedIndex = 1;
            metroTabControl2.Refresh();
        }

        private void btnCurrentDiagnosis_Click(object sender, EventArgs e)
        {
            lstDiagnosis.Items.Clear();

            conn.Open();
            string sqlll = "SELECT * FROM tblMedical_Diagnosis WHERE patientsNo = @num";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@num", lblNumber.Text);
            MySqlDataReader readerrr = cmddd.ExecuteReader();
            string datee = "";
            string doctor = "";
            string stat = "";
            string admiStat = "";
            string diagnosis = "";
            string notes = "";
            while (readerrr.Read())
            {
                datee = readerrr.GetString("date");
                doctor = readerrr.GetString("medical_doctor");
                stat = readerrr.GetString("status");
                admiStat = readerrr.GetString("admissionStatus");
                diagnosis = readerrr.GetString("diagnosis");
                notes = readerrr.GetString("notes");
                lstDiagnosis.Items.Add(diagnosis);
            }
            conn.Close();

            dtCheckupDiagnosis.Text = datee;
            lblDoctor.Text = doctor;
            lblOldNew.Text = stat;
            lblAdmission.Text = admiStat;
            lblNotes.Text = notes;

            lbl1.Visible = true;
        }

        private void dgCurrentRecordUltrasound_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordUltrasound.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            string status = "";
            string birthday = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
                status = reader.GetString("civilStatus");
                birthday = reader.GetDateTime("birthday").ToString("MM-dd-yyyy");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
                dateexamined = reader2.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
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
            param = new ReportParameter("birthday", birthday);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("status", status);
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

        private void dgOldRecordUltrasound_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgOldRecordUltrasound.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            string status = "";
            string birthday = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
                status = reader.GetString("civilStatus");
                birthday = reader.GetDateTime("birthday").ToString("MM-dd-yyyy");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblArchive_Ultrasound WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
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
                dateexamined = reader2.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
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
            param = new ReportParameter("birthday", birthday);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("status", status);
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

        private void dgCurrentRecordXray_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgCurrentRecordXray.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            string status = "";
            string birthday = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
                status = reader.GetString("civilStatus");
                birthday = reader.GetDateTime("birthday").ToString("MM-dd-yyyy");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Xray WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
            string typeExam = "";
            string impression = "";
            string result = "";
            string radiologists = "";
            string dateexamined = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                typeExam = reader2.GetString("typeofexaminations");
                impression = reader2.GetString("impression");
                result = reader2.GetString("result");
                radiologists = reader2.GetString("radiologists");
                dateexamined = reader2.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
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
            param = new ReportParameter("status", status);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sex", sex);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("birthday", birthday);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("status", status);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("date", dateexamined);
            f1.reportViewer1.LocalReport.SetParameters(param);

            param = new ReportParameter("typeofexam", typeExam);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("result", result);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("impression", impression);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sonologists", radiologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void dgOldRecordXray_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            string pnumber = dgOldRecordXray.SelectedCells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", pnumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string fname = "";
            string mname = "";
            string lname = "";
            string addr = "";
            int age = 0;
            string sex = "";
            string status = "";
            string birthday = "";
            while (reader.Read())
            {
                id = reader.GetString("patientsNo");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                addr = reader.GetString("address");
                age = reader.GetInt16("age");
                sex = reader.GetString("sex");
                status = reader.GetString("civilStatus");
                birthday = reader.GetDateTime("birthday").ToString("MM-dd-yyyy");
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblArchive_Xray WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", pnumber);
            string typeExam = "";
            string impression = "";
            string result = "";
            string radiologists = "";
            string dateexamined = "";

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                typeExam = reader2.GetString("typeofexaminations");
                impression = reader2.GetString("impression");
                result = reader2.GetString("result");
                radiologists = reader2.GetString("radiologists");
                dateexamined = reader2.GetDateTime("dateexamined").ToString("MM-dd-yyyy");
            }
            conn.Close();

            //Activity Log
            conn.Open();
            string sql3 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
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

            string acctype = accountType;
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
            param = new ReportParameter("birthday", birthday);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("status", status);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("typeofexam", typeExam);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("result", result);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("impression", impression);
            f1.reportViewer1.LocalReport.SetParameters(param);
            param = new ReportParameter("sonologists", radiologists);
            f1.reportViewer1.LocalReport.SetParameters(param);

            f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            f1.reportViewer1.DocumentMapCollapsed = true;
            f1.reportViewer1.RefreshReport();
            f1.reportViewer1.ZoomMode = ZoomMode.Percent;
            f1.reportViewer1.ZoomPercent = 75;

            f1.Show();
        }

        private void dgOldRecordTreatment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblTreatment.Text = dgOldRecordTreatment.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblDoctorName.Text = dgOldRecordTreatment.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtCheckupTreatment.Text = dgOldRecordTreatment.Rows[e.RowIndex].Cells[3].Value.ToString();
            dtOld.Text = dgOldRecordTreatment.Rows[e.RowIndex].Cells[3].Value.ToString();

            lbl1.Visible = false;
            metroTabControl2.SelectedIndex = 2;
            metroTabControl2.Refresh();
        }

        private void metroTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl2.SelectedIndex == 0)
            {
                metroTabControl3.SelectedIndex = 0;
                metroTabControl3.Refresh();
            }
            else if (metroTabControl2.SelectedIndex == 1)
            {
                metroTabControl3.SelectedIndex = 1;
                metroTabControl3.Refresh();
            }
            else if (metroTabControl2.SelectedIndex == 2)
            {
                metroTabControl3.SelectedIndex = 2;
                metroTabControl3.Refresh();
            }
        }

        private void metroTabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl3.SelectedIndex == 0)
            {
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.Refresh();
            }
            else if (metroTabControl3.SelectedIndex == 1)
            {
                metroTabControl2.SelectedIndex = 1;
                metroTabControl2.Refresh();
            }
            else if (metroTabControl3.SelectedIndex == 2)
            {
                metroTabControl2.SelectedIndex = 2;
                metroTabControl2.Refresh();
            }
        }

        private void btnCurrentRecord_Click(object sender, EventArgs e)
        {
            if (metroTabControl2.SelectedIndex == 0)
            {
                lstSymptoms.Items.Clear();

                conn.Open();
                string sqll = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                cmdd.Parameters.AddWithValue("@num", lblNumber.Text);
                MySqlDataReader readerr = cmdd.ExecuteReader();
                string date = "";
                string temp = "";
                string bp = "";
                string days = "";
                string week = "";
                string symptoms = "";
                string complaints = "";
                string cr = "";
                string rr = "";
                string weight = "";
                string height = "";
                string bmi = "";
                string bmiStat = "";
                while (readerr.Read())
                {
                    date = readerr.GetString("date");
                    temp = readerr.GetString("temperature");
                    bp = readerr.GetString("bp");
                    days = readerr.GetString("numberOfDaysWithSymptoms");
                    week = readerr.GetString("morbidityWeek");
                    symptoms = readerr.GetString("symptoms");
                    complaints = readerr.GetString("complaints");
                    cr = readerr.GetString("cr");
                    rr = readerr.GetString("rr");
                    weight = readerr.GetString("weight");
                    height = readerr.GetString("height");
                    bmi = readerr.GetString("bmi");
                    bmiStat = readerr.GetString("bmiCategory");
                    lstSymptoms.Items.Add(symptoms);
                }
                conn.Close();

                dtCheckup.Text = date;
                lblTemp.Text = temp;
                lblBp.Text = bp;
                lblDays.Text = days;
                lblWeek.Text = week;
                lblComplaints.Text = complaints;
                lblCR.Text = cr;
                lblRR.Text = rr;
                lblWeight.Text = weight;
                lblHeight.Text = height;
                lblBMI.Text = bmi;
                lblBMIStat.Text = bmiStat;

                lbl1.Visible = true;
            }
            else if (metroTabControl2.SelectedIndex == 1)
            {
                lstDiagnosis.Items.Clear();

                conn.Open();
                string sqlll = "SELECT * FROM tblMedical_Diagnosis WHERE patientsNo = @num";
                MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
                cmddd.Parameters.AddWithValue("@num", lblNumber.Text);
                MySqlDataReader readerrr = cmddd.ExecuteReader();
                string datee = "";
                string doctor = "";
                string stat = "";
                string admiStat = "";
                string diagnosis = "";
                string notes = "";
                while (readerrr.Read())
                {
                    datee = readerrr.GetString("date");
                    doctor = readerrr.GetString("medical_doctor");
                    stat = readerrr.GetString("status");
                    admiStat = readerrr.GetString("admissionStatus");
                    diagnosis = readerrr.GetString("diagnosis");
                    notes = readerrr.GetString("notes");
                    lstDiagnosis.Items.Add(diagnosis);
                }
                conn.Close();

                dtCheckupDiagnosis.Text = datee;
                lblDoctor.Text = "Dr. " + doctor;
                lblOldNew.Text = stat;
                lblAdmission.Text = admiStat;
                lblNotes.Text = notes;

                lbl1.Visible = true;
            }
            else if (metroTabControl2.SelectedIndex == 2)
            {
                conn.Open();
                string sqllll = "SELECT * FROM tblMedical_Treatment WHERE patientsNo = @num";
                MySqlCommand cmdddd = new MySqlCommand(sqllll, conn);
                cmdddd.Parameters.AddWithValue("@num", lblNumber.Text);
                MySqlDataReader readerrrr = cmdddd.ExecuteReader();
                string treatment = "";
                string doctorr = "";
                string dateee = "";
                while (readerrrr.Read())
                {
                    treatment = readerrrr.GetString("treatment");
                    doctorr = readerrrr.GetString("medical_doctor");
                    dateee = readerrrr.GetString("date");
                }
                conn.Close();

                dtCheckupTreatment.Text = dateee;
                lblDoctorName.Text = doctorr;
                lblTreatment.Text = treatment;

                lbl1.Visible = true;
            }
        }

        private void btnPrintOPD_Click(object sender, EventArgs e)
        {
            if (lbl1.Visible == true)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@num", lblNumber.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                string id = "";
                string fname = "";
                string mname = "";
                string lname = "";
                string addr = "";
                int age = 0;
                string sex = "";
                string status = "";
                string birthday = "";
                while (reader.Read())
                {
                    id = reader.GetString("patientsNo");
                    fname = reader.GetString("firstName");
                    mname = reader.GetString("middleName");
                    lname = reader.GetString("lastName");
                    addr = reader.GetString("address");
                    age = reader.GetInt16("age");
                    sex = reader.GetString("sex");
                    status = reader.GetString("civilStatus");
                    birthday = reader.GetDateTime("birthday").ToString("MM-dd-yyyy");
                }
                conn.Close();

                conn.Open();
                string sql1 = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumber.Text);
                string temp = "";
                string bp = "";
                string cr = "";
                string rr = "";
                string weight = "";
                string height = "";
                string bmi = "";
                string bmiCat = "";
                string dateexamined = "";
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    temp = reader1.GetString("temperature");
                    bp = reader1.GetString("bp");
                    cr = reader1.GetString("cr");
                    rr = reader1.GetString("rr");
                    weight = reader1.GetString("weight");
                    height = reader1.GetString("height");
                    bmi = reader1.GetString("bmi");
                    bmiCat = reader1.GetString("bmiCategory");
                    dateexamined = reader1.GetDateTime("date").ToString("MM-dd-yyyy");
                }
                conn.Close();

                conn.Open();
                string sql2 = "SELECT * FROM tblMedical_Diagnosis WHERE patientsNo = @num";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", lblNumber.Text);
                string diagnosis = "";
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    diagnosis = reader2.GetString("diagnosis");
                }
                conn.Close();

                conn.Open();
                string sql3 = "SELECT * FROM tblMedical_Treatment WHERE patientsNo = @num";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@num", lblNumber.Text);
                string treatment = "";
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {
                    treatment = reader3.GetString("treatment");
                }
                conn.Close();

                //Activity Log
                conn.Open();
                string sql4 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
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

                string acctype = accountType;
                string activityy = "PRINTED OPD RECORD OF PATIENT(" + fname + " " + mname + " " + lname + ").";

                conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                frmPrintOPDRecord f1 = new frmPrintOPDRecord();

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
                param = new ReportParameter("status", status);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("birthday", birthday);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("temp", temp);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("bp", bp);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("cr", cr);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("rr", rr);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("weight", weight);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("height", height);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("bmi", bmi);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("bmiCategory", bmiCat);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("diagnosis", diagnosis);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("treatment", treatment);
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                string sql = "SELECT * FROM tblPatients_Info WHERE patientsNo = @num";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@num", lblNumber.Text);
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
                string sql1 = "SELECT * FROM tblOld_Medical_Information WHERE patientsNo = @num AND date = @date";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@num", lblNumber.Text);
                cmd1.Parameters.AddWithValue("@date", dtOld.Value);
                string temp = "";
                string bp = "";
                string cr = "";
                string rr = "";
                string dateexamined = "";
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    temp = reader1.GetString("temperature");
                    bp = reader1.GetString("bp");
                    cr = reader1.GetString("cr");
                    rr = reader1.GetString("rr");
                    dateexamined = reader1.GetDateTime("date").ToString("MM-dd-yyyy");
                }
                conn.Close();

                conn.Open();
                string sql2 = "SELECT * FROM tblOld_Medical_Diagnosis WHERE patientsNo = @num AND date = @date";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@num", lblNumber.Text);
                cmd2.Parameters.AddWithValue("@date", dtOld.Value);
                string diagnosis = "";
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    diagnosis = reader2.GetString("diagnosis");
                }
                conn.Close();

                conn.Open();
                string sql3 = "SELECT * FROM tblOld_Medical_Treatment WHERE patientsNo = @num AND date = @date";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@num", lblNumber.Text);
                cmd3.Parameters.AddWithValue("@date", dtOld.Value);
                string treatment = "";
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {
                    treatment = reader3.GetString("treatment");
                }
                conn.Close();

                //Activity Log
                conn.Open();
                string sql4 = "SELECT * FROM tblAccounts WHERE accountType = '" + accountType + "' ";
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

                string acctype = accountType;
                string activityy = "PRINTED OPD RECORD OF PATIENT(" + fname + " " + mname + " " + lname + ").";

                conStr.activityLog(acctype, fnamee, mnamee, lnamee, activityy);

                frmPrintOPDRecord f1 = new frmPrintOPDRecord();
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
                param = new ReportParameter("temp", temp);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("cr", cr);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("rr", rr);
                f1.reportViewer1.LocalReport.SetParameters(param);
                param = new ReportParameter("treatment", treatment);
                f1.reportViewer1.LocalReport.SetParameters(param);

                f1.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                f1.reportViewer1.DocumentMapCollapsed = true;
                f1.reportViewer1.RefreshReport();
                f1.reportViewer1.ZoomMode = ZoomMode.Percent;
                f1.reportViewer1.ZoomPercent = 75;

                f1.Show();
            }
        }
    }
}
