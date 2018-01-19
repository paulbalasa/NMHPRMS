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
    public partial class usrCntrlPatientRecord : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public usrCntrlPatientRecord()
        {
            InitializeComponent();
            autoCompleteLastname();
        }

        private void btnNewPatient_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmNewPatient f1 = new frmNewPatient();
            f1.Show();
        }

        public string number = "";

        private void btnAddNewRecord_Click(object sender, EventArgs e)
        {
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

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }

            frmAddRecord f1 = new frmAddRecord();
            f1.lblNumber.Text = patientnum;
            f1.txtLname.Text = lname;
            f1.txtFname.Text = fname;
            f1.txtMname.Text = mname;
            f1.cboSex.Text = sex;
            f1.cboStatus.Text = status;
            f1.dtPickerBirthday.Text = birthday;
            f1.txtAge.Text = age.ToString();
            f1.txtAddress.Text = address;
            f1.Show();
        }

        private void usrCntrlPatientRecord_Load(object sender, EventArgs e)
        {
            getDataSet();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblPatients_Info", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        public void getDataSet()
        {
            conn.Open();
            string sql = "SELECT patientsNo AS 'Patient No.', lastName AS 'Last Name' , firstName AS 'First Name' , middleName AS 'Middle Name' FROM tblPatients_Info";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            DataSet dS = new DataSet();
            adapter.Fill(dS, "tblPatients_Info");
            dgPatientsInfo.DataSource = dS;
            dgPatientsInfo.DataMember = "tblPatients_Info";
            conn.Close();

            dgPatientsInfo.Columns[0].Width = 125;
            dgPatientsInfo.Columns[1].Width = 200;
            dgPatientsInfo.Columns[2].Width = 200;
            dgPatientsInfo.Columns[3].Width = 200;
        }

        void autoCompleteLastname()
        {
            txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            string sqlStatement = "SELECT * FROM tblPatients_Info";
            MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string lastname = reader.GetString("lastName");
                    coll.Add(lastname);
                    string firstname = reader.GetString("firstName");
                    coll.Add(firstname);
                    string middlename = reader.GetString("middleName");
                    coll.Add(middlename);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtSearch.AutoCompleteCustomSource = coll;
        }

        private void dgPatientsInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            number = dgPatientsInfo.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            conn.Open();
            string sql = "SELECT COUNT(patientsNo) AS 'Patients Number', lastName AS 'Last Name' , firstName AS 'First Name' , middleName AS 'Middle Name' FROM tblPatients_Info WHERE lastName LIKE  '" + txtSearch.Text + "' OR firstName LIKE  '" + txtSearch.Text + "' OR middleName LIKE  '" + txtSearch.Text + "' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            DataSet dS = new DataSet();
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            adapter.Fill(dS, "tblPatients_Info");
            dgPatientsInfo.DataSource = dS;
            dgPatientsInfo.DataMember = "tblPatients_Info";
            conn.Close();

            lblTotalRecords.Text = count.ToString() + " records.";

            dgPatientsInfo.Columns[0].Width = 125;
            dgPatientsInfo.Columns[1].Width = 200;
            dgPatientsInfo.Columns[2].Width = 200;
            dgPatientsInfo.Columns[3].Width = 200;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            getDataSet();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblPatients_Info", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                getDataSet();

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblPatients_Info", conn);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                lblTotalRecords.Text = count.ToString() + " records.";

            }
        }

        private void dgPatientsInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string number = dgPatientsInfo.Rows[e.RowIndex].Cells[0].Value.ToString();

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

            frmPIRAdmin f1 = new frmPIRAdmin();
            f1.accountType = "NURSE";
            f1.lblNumber.Text = patientnum;
            f1.txtLname.Text = lname;
            f1.txtFname.Text = fname;
            f1.txtMname.Text = mname;
            f1.txtSex.Text = sex;
            f1.txtStatus.Text = status;
            f1.dtPickerBirthday.Text = birthday;
            f1.txtAge.Text = age.ToString();
            f1.txtAddress.Text = address;
            f1.lblBackNurse.Visible = false;

            //Medical Information Current Record
            conn.Open();
            string sqll = "SELECT * FROM tblMedical_Information WHERE patientsNo = @num";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            cmdd.Parameters.AddWithValue("@num", number);
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

                f1.lstSymptoms.Items.Add(symptoms);
            }
            conn.Close();

            f1.dtCheckup.Text = date;
            f1.lblTemp.Text = temp;
            f1.lblBp.Text = bp;
            f1.lblDays.Text = days;
            f1.lblWeek.Text = week;
            f1.lblComplaints.Text = complaints;
            f1.lblCR.Text = cr;
            f1.lblRR.Text = rr;
            f1.lblWeight.Text = weight;
            f1.lblHeight.Text = height;
            f1.lblBMI.Text = bmi;
            f1.lblBMIStat.Text = bmiStat;

            //Medical Information Old Record
            conn.Open();
            string sql1 = "SELECT DISTINCT patientsNo AS 'Patient No', temperature AS 'Temperature', bp AS 'Blood Pressure', cr AS 'Circulatory Rate', rr AS 'Respiratory Rate', weight AS 'Weight', height AS 'Height', bmi AS 'BMI', bmiCategory AS 'BMI Category', complaints AS 'Complaints', numberOfDaysWithSymptoms AS 'Number of Days with Symptoms', morbidityWeek AS 'Morbidity Week', date AS 'Date' FROM tblOld_Medical_Information WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(sql1, conn);
            DataSet dS1 = new DataSet();
            adapter1.Fill(dS1, "tblOld_Medical_Information");
            f1.dgOldRecordMedicalInformation.DataMember = "tblOld_Medical_Information";
            f1.dgOldRecordMedicalInformation.DataSource = dS1;
            conn.Close();

            //Medical Diagnosis Current Record
            conn.Open();
            string sqlll = "SELECT * FROM tblMedical_Diagnosis WHERE patientsNo = @num";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@num", number);
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
                f1.lstDiagnosis.Items.Add(diagnosis);
            }
            conn.Close();

            f1.dtCheckupDiagnosis.Text = datee;
            f1.lblDoctor.Text = "Dr. " + doctor;
            f1.lblOldNew.Text = stat;
            f1.lblAdmission.Text = admiStat;
            f1.lblNotes.Text = notes;

            //Medical Diagnosis Old Record
            conn.Open();
            string sql3 = "SELECT DISTINCT patientsNo AS 'Patient No', medical_doctor AS 'Medical Doctor', status AS 'Status',  admissionStatus AS 'Admission Status', notes AS 'Notes', date AS 'Date' FROM tblOld_Medical_Diagnosis WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter3 = new MySqlDataAdapter(sql3, conn);
            DataSet dS3 = new DataSet();
            adapter3.Fill(dS3, "tblOld_Medical_Diagnosis");
            f1.dgOldRecordMedicalDiagnosis.DataMember = "tblOld_Medical_Diagnosis";
            f1.dgOldRecordMedicalDiagnosis.DataSource = dS3;
            conn.Close();

            //Laboratory Blood Chemistry Current Record
            conn.Open();
            string sql4 = "SELECT patientsNo AS 'Patient No', fasting_blood_sugar AS 'Fasting Blood Sugar', blood_urea_nitrogen AS 'Blood Urea Nitrogen',  creatinine AS 'Creatinine',  cholesterol AS 'Cholesterol', triglycerides AS 'Triglycerides', hdl AS 'HDL', ldl AS 'LDL', vldl AS 'VLDL', blood_uric_acid AS 'Blood Uric Acid', sodium AS 'Sodium',  potassium AS 'Potassium',  chloride AS 'Chloride', sgpt_alt AS 'SGPT ALT', sgot_ast AS 'SGOT ALT', medtech AS 'Medical Technologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Blood_Chemistry WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter4 = new MySqlDataAdapter(sql4, conn);
            DataSet dS4 = new DataSet();
            adapter4.Fill(dS4, "tblLaboratory_Blood_Chemistry");
            f1.dgCurrentRecordBloodChemistry.DataMember = "tblLaboratory_Blood_Chemistry";
            f1.dgCurrentRecordBloodChemistry.DataSource = dS4;
            conn.Close();

            //Laboratory Blood Chemistry Old Record
            conn.Open();
            string sql5 = "SELECT patientsNo AS 'Patient No', fasting_blood_sugar AS 'Fasting Blood Sugar', blood_urea_nitrogen AS 'Blood Urea Nitrogen',  creatinine AS 'Creatinine',  cholesterol AS 'Cholesterol', triglycerides AS 'Triglycerides', hdl AS 'HDL', ldl AS 'LDL', vldl AS 'VLDL', blood_uric_acid AS 'Blood Uric Acid', sodium AS 'Sodium',  potassium AS 'Potassium',  chloride AS 'Chloride', sgpt_alt AS 'SGPT ALT', sgot_ast AS 'SGOT ALT', medtech AS 'Medical Technologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Blood_Chemistry WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter5 = new MySqlDataAdapter(sql5, conn);
            DataSet dS5 = new DataSet();
            adapter5.Fill(dS5, "tblArchive_Blood_Chemistry");
            f1.dgOldRecordBloodChemistry.DataMember = "tblArchive_Blood_Chemistry";
            f1.dgOldRecordBloodChemistry.DataSource = dS5;
            conn.Close();

            //Laboratory Fecalysis Macroscopic Current Record
            conn.Open();
            string sql6 = "SELECT patientsNo AS 'Patient No', color AS 'Color', reaction AS 'Reaction',  occult_blood AS 'Occult Blood',  medtech AS 'Medical Technologists', pathologists AS 'Pathologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter6 = new MySqlDataAdapter(sql6, conn);
            DataSet dS6 = new DataSet();
            adapter6.Fill(dS6, "tblLaboratory_Fecalysis_Mac");
            f1.dgCurrentRecordFecalysisMacro.DataMember = "tblLaboratory_Fecalysis_Mac";
            f1.dgCurrentRecordFecalysisMacro.DataSource = dS6;
            conn.Close();

            //Laboratory Fecalysis Macroscopic Old Record
            conn.Open();
            string sql7 = "SELECT patientsNo AS 'Patient No', color AS 'Color', reaction AS 'Reaction',  occult_blood AS 'Occult Blood',  medtech AS 'Medical Technologists', pathologists AS 'Pathologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Fecalysis_Mac WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter7 = new MySqlDataAdapter(sql7, conn);
            DataSet dS7 = new DataSet();
            adapter7.Fill(dS7, "tblArchive_Fecalysis_Mac");
            f1.dgOldRecordFecalysisMacro.DataMember = "tblArchive_Fecalysis_Mac";
            f1.dgOldRecordFecalysisMacro.DataSource = dS7;
            conn.Close();

            //Laboratory Fecalysis Microscopic Current Record
            conn.Open();
            string sql8 = "SELECT patientsNo AS 'Patient No', pus_cells AS 'PUS Cells', rbc AS 'RBC',  fat_globules AS 'Flat Globules',  macrophage AS 'Macrophage', bacteria AS 'Bacteria', parasites_or_ova AS 'Parasites or Ova' FROM tblLaboratory_Fecalysis_Mic WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter8 = new MySqlDataAdapter(sql8, conn);
            DataSet dS8 = new DataSet();
            adapter8.Fill(dS8, "tblLaboratory_Fecalysis_Mic");
            f1.dgCurrentRecordFecalysisMicro.DataMember = "tblLaboratory_Fecalysis_Mic";
            f1.dgCurrentRecordFecalysisMicro.DataSource = dS8;
            conn.Close();

            //Laboratory Fecalysis Microscopic Old Record
            conn.Open();
            string sql9 = "SELECT patientsNo AS 'Patient No', pus_cells AS 'PUS Cells', rbc AS 'RBC',  fat_gobules AS 'Flat Globules',  macrophage AS 'Macrophage', bacteria AS 'Bacteria', parasites_or_ova AS 'Parasites or Ova' FROM tblArchive_Fecalysis_Mic WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter9 = new MySqlDataAdapter(sql9, conn);
            DataSet dS9 = new DataSet();
            adapter9.Fill(dS9, "tblArchive_Fecalysis_Mic");
            f1.dgOldRecordFecalysisMicro.DataMember = "tblArchive_Fecalysis_Mic";
            f1.dgOldRecordFecalysisMicro.DataSource = dS9;
            conn.Close();

            //Laboratory Hematology Current Record
            conn.Open();
            string sql10 = "SELECT patientsNo AS 'Patient No', hemoglobin AS 'Hemoglobin', hematocrit AS 'Hematocrit',  wbc_count AS 'WBC Count',  rbc_count AS 'RBC Count', platelet AS 'Platelet', bleeding_time AS 'Bleeding Time', clotting_time AS 'Clotting Time', abo_group AS 'ABO Group', segmenters AS 'Segmenters',  lymphocytes AS 'Lymphocytes',  monocytes AS 'Monocytes', eosinophils AS 'Eosinophils', basophils AS 'Basophils', stab AS 'Stab', others AS 'Others',  medtech AS 'Medical Technologists',  pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Hematology WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter10 = new MySqlDataAdapter(sql10, conn);
            DataSet dS10 = new DataSet();
            adapter10.Fill(dS10, "tblLaboratory_Hematology");
            f1.dgCurrentRecordHematology.DataMember = "tblLaboratory_Hematology";
            f1.dgCurrentRecordHematology.DataSource = dS10;
            conn.Close();

            //Laboratory Hematology Old Record
            conn.Open();
            string sql11 = "SELECT patientsNo AS 'Patient No', hemoglobin AS 'Hemoglobin', hematocrit AS 'Hematocrit',  wbc_count AS 'WBC Count',  rbc_count AS 'RBC Count', platelet AS 'Platelet', bleeding_time AS 'Bleeding Time', clotting_time AS 'Clotting Time', abo_group AS 'ABO Group', segmenters AS 'Segmenters',  lymphocytes AS 'Lymphocytes',  monocytes AS 'Monocytes', eosinophils AS 'Eosinophils', basophils AS 'Basophils', stab AS 'Stab', others AS 'Others',  medtech AS 'Medical Technologists',  pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Hematology WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter11 = new MySqlDataAdapter(sql11, conn);
            DataSet dS11 = new DataSet();
            adapter11.Fill(dS11, "tblArchive_Hematology");
            f1.dgOldRecordHematology.DataMember = "tblArchive_Hematology";
            f1.dgOldRecordHematology.DataSource = dS11;
            conn.Close();

            //Laboratory Urinalysis Macroscopic Current Record
            conn.Open();
            string sql12 = "SELECT patientsNo AS 'Patient No', color AS 'Color', characters AS 'Characters',  protein AS 'Protein', sugar AS 'Sugar', ph AS 'pH', spGr AS 'spGr', pregnancyTest AS 'Pregnancy Test', medtech AS 'Medical Technologists', pathologists AS 'Pathologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter12 = new MySqlDataAdapter(sql12, conn);
            DataSet dS12 = new DataSet();
            adapter12.Fill(dS12, "tblLaboratory_Urinalysis_Mac");
            f1.dgCurrentRecordUrinalysisMacro.DataMember = "tblLaboratory_Urinalysis_Mac";
            f1.dgCurrentRecordUrinalysisMacro.DataSource = dS12;
            conn.Close();

            //Laboratory Urinalysis Macroscopic Old Record
            conn.Open();
            string sql13 = "SELECT idNo AS 'Patient No', color AS 'Color', characters AS 'Characters',  protein AS 'Protein', sugar AS 'Sugar', ph AS 'pH', spGr AS 'spGr', pregnancyTest AS 'Pregnancy Test',  medtech AS 'Medical Technologists', pathologists AS 'Pathologists', pathologists AS 'Pathologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Urinalysis_Mac WHERE idNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter13 = new MySqlDataAdapter(sql13, conn);
            DataSet dS13 = new DataSet();
            adapter13.Fill(dS13, "tblArchive_Urinalysis_Mac");
            f1.dgOldRecordUrinalysisMacro.DataMember = "tblArchive_Urinalysis_Mac";
            f1.dgOldRecordUrinalysisMacro.DataSource = dS13;
            conn.Close();

            //Laboratory Urinalysis Microscopic Current Record
            conn.Open();
            string sql14 = "SELECT patientsNo AS 'Patient No', pus_cells AS 'PUS Cells', rbc AS 'RBC',  epith_cells AS 'Epith Cells',  bacteria AS 'Bacteria', mucus_thread AS 'Mucus thread', amorphous_urates AS 'Amorphous Urates', casts AS 'Casts', crystals AS 'Crystals', others AS 'Others' FROM tblLaboratory_Urinalysis_Mic WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter14 = new MySqlDataAdapter(sql14, conn);
            DataSet dS14 = new DataSet();
            adapter14.Fill(dS14, "tblLaboratory_Urinalysis_Mic");
            f1.dgCurrentRecordUrinalysisMicro.DataMember = "tblLaboratory_Urinalysis_Mic";
            f1.dgCurrentRecordUrinalysisMicro.DataSource = dS14;
            conn.Close();

            //Laboratory Urinalysis Microscopic Old Record
            conn.Open();
            string sql15 = "SELECT patientsNo AS 'Patient No', pus_cells AS 'PUS Cells', rbc AS 'RBC',  epith_cells AS 'Epith Cells',  bacteria AS 'Bacteria', mucus_thread AS 'Mucus thread', amorphous_urates AS 'Amorphous Urates', casts AS 'Casts', crystals AS 'Crystals', others AS 'Others' FROM tblArchive_Urinalysis_Mic WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter15 = new MySqlDataAdapter(sql15, conn);
            DataSet dS15 = new DataSet();
            adapter15.Fill(dS15, "tblArchive_Urinalysis_Mic");
            f1.dgOldRecordUrinalysisMicro.DataMember = "tblArchive_Urinalysis_Mic";
            f1.dgOldRecordUrinalysisMicro.DataSource = dS15;
            conn.Close();

            //Laboratory Ultrasound Current Record
            conn.Open();
            string sql16 = "SELECT patientsNo AS 'Patient No', typeofexaminations AS 'Type of Examination', impression AS 'Impression',  result AS 'Result', sonologists AS 'Sonologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Ultrasound WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter16 = new MySqlDataAdapter(sql16, conn);
            DataSet dS16 = new DataSet();
            adapter16.Fill(dS16, "tblLaboratory_Ultrasound");
            f1.dgCurrentRecordUltrasound.DataMember = "tblLaboratory_Ultrasound";
            f1.dgCurrentRecordUltrasound.DataSource = dS16;
            conn.Close();

            //Laboratory Ultrasound Old Record
            conn.Open();
            string sql17 = "SELECT patientsNo AS 'Patient No', typeofexaminations AS 'Type of Examination', impression AS 'Impression',  result AS 'Result', sonologists AS 'Sonologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Ultrasound WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter17 = new MySqlDataAdapter(sql17, conn);
            DataSet dS17 = new DataSet();
            adapter17.Fill(dS17, "tblArchive_Ultrasound");
            f1.dgOldRecordUltrasound.DataMember = "tblArchive_Ultrasound";
            f1.dgOldRecordUltrasound.DataSource = dS17;
            conn.Close();

            //Laboratory Xray Current Record
            conn.Open();
            string sql18 = "SELECT patientsNo AS 'Patient No', typeofexaminations AS 'Type of Examination', impression AS 'Impression',  result AS 'Result', radiologists AS 'Radiologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory_Xray WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter18 = new MySqlDataAdapter(sql18, conn);
            DataSet dS18 = new DataSet();
            adapter18.Fill(dS18, "tblLaboratory_Xray");
            f1.dgCurrentRecordXray.DataMember = "tblLaboratory_Xray";
            f1.dgCurrentRecordXray.DataSource = dS18;
            conn.Close();

            //Laboratory Xray Old Record
            conn.Open();
            string sql19 = "SELECT patientsNo AS 'Patient No', typeofexamination AS 'Type of Examination', impression AS 'Impression',  results AS 'Result', radiologists AS 'Radiologists', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblArchive_Xray WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter19 = new MySqlDataAdapter(sql19, conn);
            DataSet dS19 = new DataSet();
            adapter19.Fill(dS19, "tblArchive_Xray");
            f1.dgOldRecordXray.DataMember = "tblArchive_Xray";
            f1.dgOldRecordXray.DataSource = dS19;
            conn.Close();

            //Medical Treatment Current Record
            conn.Open();
            string sqllll = "SELECT * FROM tblMedical_Treatment WHERE patientsNo = @num";
            MySqlCommand cmdddd = new MySqlCommand(sqllll, conn);
            cmdddd.Parameters.AddWithValue("@num", number);
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

            f1.dtCheckupTreatment.Text = dateee;
            f1.lblDoctorName.Text = doctorr;
            f1.lblTreatment.Text = treatment;

            //Medical Treatment Old Record
            conn.Open();
            string sql21 = "SELECT patientsNo AS 'Patient No', treatment AS 'Treatment', medical_doctor AS 'Medical Doctor', date AS 'Date' FROM tblOld_Medical_Treatment WHERE patientsNo LIKE  '" + number + "' ";
            MySqlDataAdapter adapter21 = new MySqlDataAdapter(sql21, conn);
            DataSet dS21 = new DataSet();
            adapter21.Fill(dS21, "tblOld_Medical_Treatment");
            f1.dgOldRecordTreatment.DataMember = "tblOld_Medical_Treatment";
            f1.dgOldRecordTreatment.DataSource = dS21;
            conn.Close();

            f1.Show();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void dgPatientsInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPatientsInfo.SelectedRows.Count != 0)
                btnAddNewRecord.Enabled = true;
            else
                btnAddNewRecord.Enabled = false;
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "LAST NAME / FIRST NAME / MIDDLE NAME")            
                txtSearch.Text = "";
        }

        private void txtSearch_MouseLeave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
                txtSearch.Text = "LAST NAME / FIRST NAME / MIDDLE NAME";
        }
    }
}
