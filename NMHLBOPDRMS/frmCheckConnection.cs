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
using System.Configuration;

namespace NMHLBOPDRMS
{
    public partial class frmCheckConnection : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        //public static string connectionString = "server = localhost; username = root; database = dbHospital; port = 3306; password = 1234";
        //public MySqlConnection conn = new MySqlConnection(connectionString);

        public frmCheckConnection()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text;
            string database = txtDatabase.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string port = txtPort.Text;

            if (server != "" && username != "" && database != "" && port != "" && password != "")
            {
                try
                { 
                    string conStr = "server = " +txtServer.Text+ "; username = " + txtUsername.Text + "; database = " +txtDatabase.Text + "; port = " + txtPort.Text+ "; password = " + txtPassword.Text+ ";";
                    MySqlConnection conn = new MySqlConnection(conStr);
                    conn.Open();

                    MessageBox.Show("Database Configuration saved. Successfully connected to the database!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    saveConfig();
                    MessageBox.Show("System Restart required... Click OK to proceed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    Application.Restart();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unknown database"))
                    {
                        if (MessageBox.Show("Error: Database doesn't exist. " + Environment.NewLine + "Create database? ", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            createDatabase();
                            MessageBox.Show("Succesfully created database!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtServer.Text == "")
                    txtServer.Focus();
                else if (txtUsername.Text == "")
                    txtUsername.Focus();
                else if (txtDatabase.Text == "")
                    txtDatabase.Focus();
                else if (txtPort.Text == "")
                    txtPort.Focus();
                else if (txtPassword.Text == "")
                    txtPassword.Focus();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void createDatabase()
        {
            MySqlCommand cmd;
            string dbname = txtDatabase.Text;

            conn.Open();
            cmd = new MySqlCommand("CREATE DATABASE `" + dbname + "` /*!40100 DEFAULT CHARACTER SET latin1 */;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblaccount_profile`; CREATE TABLE  `" + dbname + "`.`tblaccount_profile` (`IDNumber` varchar(45) NOT NULL, `accountType` varchar(50) NOT NULL, `doctorPosition` varchar(50) NOT NULL, `lastName` varchar(50) NOT NULL, `firstName` varchar(50) NOT NULL, `middleName` varchar(50) NOT NULL, `birthday` date NOT NULL, `profilePicture` longblob, `pathName` varchar(255) default NULL, PRIMARY KEY(`IDNumber`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblaccounts`; CREATE TABLE `" + dbname + "`.`tblaccounts` (`accountNumber` int(10) unsigned NOT NULL, `accountType` varchar(45) NOT NULL, `username` varchar(50) NOT NULL, `password` varchar(50) NOT NULL, `IDNumber` varchar(50) NOT NULL, `archive` int(10) unsigned NOT NULL, `logInStatus` varchar(20) NOT NULL, `lockStatus` int(10) unsigned NOT NULL, PRIMARY KEY  USING BTREE(`username`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblactivity_log`; CREATE TABLE  `" + dbname + "`.`tblactivity_log` (`logNum` varchar(45) NOT NULL, `accountType` varchar(45) NOT NULL, `name` varchar(65) NOT NULL, `activity` varchar(755) NOT NULL, `date` date NOT NULL, `time` varchar(25) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_blood_chemistry`; CREATE TABLE  `" + dbname + "`.`tblarchive_blood_chemistry` ( `patientsNo` varchar(45) NOT NULL, `fasting_blood_sugar` varchar(45) default NULL, `blood_urea_nitrogen` varchar(45) default NULL, `creatinine` varchar(45) default NULL, `cholesterol` varchar(45) default NULL, `triglycerides` varchar(45) default NULL, `hdl` varchar(45) default NULL, `ldl` varchar(45) default NULL, `vldl` varchar(45) default NULL, `blood_uric_acid` varchar(45) default NULL, `sodium` varchar(45) default NULL, `potassium` varchar(45) default NULL, `chloride` varchar(45) default NULL, `sgpt_alt` varchar(45) default NULL, `sgot_ast` varchar(45) default NULL, `medtech` varchar(65) NOT NULL, `pathologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_fecalysis_mac`; CREATE TABLE  `" + dbname + "`.`tblarchive_fecalysis_mac` (`patientsNo` varchar(45) NOT NULL, `color` varchar(45) default NULL, `characters` varchar(45) default NULL, `reaction` varchar(45) default NULL, `occult_blood` varchar(45) default NULL, `medtech` varchar(65) NOT NULL, `pathologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_fecalysis_mic`; CREATE TABLE  `" + dbname + "`.`tblarchive_fecalysis_mic` (`patientsNo` varchar(45) NOT NULL, `pus_cells` varchar(45) default NULL, `rbc` varchar(45) default NULL, `fat_gobules` varchar(45) default NULL, `macrophage` varchar(45) default NULL, `bacteria` varchar(45) default NULL, `parasites_or_ova` varchar(45) default NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_hematology`; CREATE TABLE  `" + dbname + "`.`tblarchive_hematology` (`patientsNo` varchar(45) NOT NULL, `hemoglobin` varchar(45) default NULL, `hematocrit` varchar(45) default NULL, `wbc_count` varchar(45) default NULL, `rbc_count` varchar(45) default NULL, `platelet` varchar(45) default NULL, `bleeding_time` varchar(45) default NULL, `clotting_time` varchar(45) default NULL, `abo_group` varchar(45) default NULL, `segmenters` varchar(45) default NULL, `lymphocytes` varchar(45) default NULL, `monocytes` varchar(45) default NULL, `eosinophils` varchar(45) default NULL, `basophils` varchar(45) default NULL, `stab` varchar(45) default NULL, `others` varchar(45) default NULL, `medtech` varchar(65) NOT NULL, `pathologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_ultrasound`; CREATE TABLE  `" + dbname + "`.`tblarchive_ultrasound` (`patientsNo` varchar(45) NOT NULL, `typeofexaminations` varchar(100) NOT NULL, `impression` varchar(2000) NOT NULL, `result` varchar(2000) NOT NULL, `sonologists` varchar(200) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_urinalysis_mac`; CREATE TABLE  `" + dbname + "`.`tblarchive_urinalysis_mac` (`idNo` varchar(45) NOT NULL, `color` varchar(45) default NULL, `characters` varchar(45) default NULL, `protein` varchar(45) default NULL, `sugar` varchar(45) default NULL, `ph` varchar(45) default NULL, `spGr` varchar(45) default NULL, `pregnancyTest` varchar(45) default NULL, `medtech` varchar(65) NOT NULL, `pathologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_urinalysis_mic`; CREATE TABLE  `" + dbname + "`.`tblarchive_urinalysis_mic` (`patientsNo` varchar(45) NOT NULL, `pus_cells` varchar(45) default NULL, `rbc` varchar(45) default NULL, `epith_cells` varchar(45) default NULL, `bacteria` varchar(45) default NULL, `mucus_thread` varchar(45) default NULL, `amorphous_urates` varchar(45) default NULL, `casts` varchar(45) default NULL, `crystals` varchar(45) default NULL, `others` varchar(45) default NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblarchive_xray`; CREATE TABLE  `" + dbname + "`.`tblarchive_xray` (`patientsNo` varchar(45) NOT NULL, `typeofexamination` varchar(45) NOT NULL, `impression` varchar(2000) NOT NULL, `results` varchar(2000) NOT NULL, `radiologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbldoctors`; CREATE TABLE  `" + dbname + "`.`tbldoctors` (`doctorPosition` varchar(75) NOT NULL, PRIMARY KEY(`doctorPosition`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbldoctors_schedule`; CREATE TABLE  `" + dbname + "`.`tbldoctors_schedule` (`IDNumber` varchar(45) NOT NULL, `name` varchar(200) NOT NULL, `schedule` varchar(45) NOT NULL, `time` varchar(45) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbldoctors_schedule1`; CREATE TABLE  `" + dbname + "`.`tbldoctors_schedule1` (`IDNumber` varchar(45) NOT NULL, `name` varchar(200) NOT NULL, `position` varchar(200) NOT NULL, `schedule1` varchar(45) NOT NULL, PRIMARY KEY(`IDNumber`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbldrafts_nurse`; CREATE TABLE  `" + dbname + "`.`tbldrafts_nurse` (`numID` int(10) unsigned NOT NULL, `patientNo` varchar(45) NOT NULL, `lastName` varchar(200) NOT NULL, `firstName` varchar(200) NOT NULL, `middleName` varchar(200) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, `sentBy` varchar(200) NOT NULL, `sentTo` varchar(200) NOT NULL, PRIMARY KEY(`numID`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_blood_chemistry`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_blood_chemistry` (`patientsNo` varchar(45) NOT NULL, `fasting_blood_sugar` varchar(45) default NULL, `blood_urea_nitrogen` varchar(45) default NULL, `creatinine` varchar(45) default NULL, `cholesterol` varchar(45) default NULL, `triglycerides` varchar(45) default NULL, `hdl` varchar(45) default NULL, `ldl` varchar(45) default NULL, `vldl` varchar(45) default NULL, `blood_uric_acid` varchar(45) default NULL, `sodium` varchar(45) default NULL, `potassium` varchar(45) default NULL, `chloride` varchar(45) default NULL, `sgpt_alt` varchar(45) default NULL, `sgot_ast` varchar(45) default NULL, `medtech` varchar(45) NOT NULL, `pathologists` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_fecalysis_mac`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_fecalysis_mac` (`patientsNo` varchar(45) NOT NULL, `color` varchar(45) default NULL, `characters` varchar(45) default NULL, `reaction` varchar(45) default NULL, `occult_blood` varchar(45) default NULL, `medtech` varchar(45) NOT NULL, `pathologists` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_fecalysis_mic`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_fecalysis_mic` (`patientsNo` varchar(45) NOT NULL, `pus_cells` varchar(45) default NULL, `rbc` varchar(45) default NULL, `fat_globules` varchar(45) default NULL, `macrophage` varchar(45) default NULL, `bacteria` varchar(45) default NULL, `parasites_or_ova` varchar(45) default NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_hematology`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_hematology` (`patientsNo` varchar(45) NOT NULL, `hemoglobin` varchar(45) default NULL, `hematocrit` varchar(45) default NULL, `wbc_count` varchar(45) default NULL, `rbc_count` varchar(45) default NULL, `platelet` varchar(45) default NULL, `bleeding_time` varchar(45) default NULL, `clotting_time` varchar(45) default NULL, `abo_group` varchar(45) default NULL, `segmenters` varchar(45) default NULL, `lymphocytes` varchar(45) default NULL, `monocytes` varchar(45) default NULL, `eosinophils` varchar(45) default NULL, `basophils` varchar(45) default NULL, `stab` varchar(45) default NULL, `others` varchar(45) default NULL, `medtech` varchar(65) NOT NULL, `pathologists` varchar(65) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_ultrasound`;CREATE TABLE  `" + dbname + "`.`tbllaboratory_ultrasound` (`patientsNo` varchar(45) NOT NULL, `typeofexaminations` varchar(100) NOT NULL, `impression` varchar(2000) NOT NULL, `result` varchar(2000) NOT NULL, `sonologists` varchar(200) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_urinalysis_mac`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_urinalysis_mac` (`patientsNo` varchar(45) NOT NULL, `color` varchar(45) default NULL, `characters` varchar(45) default NULL, `protein` varchar(45) default NULL, `sugar` varchar(45) default NULL, `ph` varchar(45) default NULL, `spGr` varchar(45) default NULL, `pregnancyTest` varchar(45) default NULL, `medtech` varchar(45) NOT NULL, `pathologists` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_urinalysis_mic`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_urinalysis_mic` (`patientsNo` varchar(45) NOT NULL, `pus_cells` varchar(45) default NULL, `rbc` varchar(45) default NULL, `epith_cells` varchar(45) default NULL, `bacteria` varchar(45) default NULL, `mucus_thread` varchar(45) default NULL, `amorphous_urates` varchar(45) default NULL, `casts` varchar(45) default NULL, `crystals` varchar(45) default NULL, `others` varchar(45) default NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory_xray`; CREATE TABLE  `" + dbname + "`.`tbllaboratory_xray` (`patientsNo` varchar(45) NOT NULL, `typeofexaminations` varchar(100) NOT NULL, `impression` varchar(2000) NOT NULL, `result` varchar(2000) NOT NULL, `radiologists` varchar(200) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory1_results`; CREATE TABLE  `" + dbname + "`.`tbllaboratory1_results` (`patientNo` varchar(45) NOT NULL, `lastName` varchar(45) NOT NULL, `firstName` varchar(45) NOT NULL, `middleName` varchar(45) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tbllaboratory2_results`; CREATE TABLE  `" + dbname + "`.`tbllaboratory2_results` (`patientNo` varchar(45) NOT NULL, `lastName` varchar(45) NOT NULL, `firstName` varchar(45) NOT NULL, `middleName` varchar(45) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_diagnosis`; CREATE TABLE  `" + dbname + "`.`tblmedical_diagnosis` (`patientsNo` varchar(35) NOT NULL, `medical_doctor` varchar(100) NOT NULL, `status` varchar(45) NOT NULL, `admissionStatus` varchar(45) NOT NULL, `diagnosis` varchar(255) NOT NULL, `notes` varchar(655) NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_information`; CREATE TABLE  `" + dbname + "`.`tblmedical_information` (`patientsNo` varchar(35) NOT NULL, `temperature` varchar(20) default NULL, `bp` varchar(20) default NULL, `cr` varchar(20) default NULL, `rr` varchar(20) default NULL, `weight` varchar(45) default NULL, `height` varchar(45) default NULL, `bmi` varchar(45) default NULL, `bmiCategory` varchar(65) default NULL, `complaints` varchar(500) default NULL, `symptoms` varchar(45) NOT NULL, `numberOfDaysWithSymptoms` varchar(45) default NULL, `morbidityWeek` varchar(45) default NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_laboratory_1`; CREATE TABLE  `" + dbname + "`.`tblmedical_laboratory_1` (`patientsNo` varchar(45) NOT NULL, `labExam` varchar(200) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_laboratory_ultrasound`; CREATE TABLE  `" + dbname + "`.`tblmedical_laboratory_ultrasound` (`patientsNo` varchar(45) NOT NULL, `labExam` varchar(200) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_laboratory_xray`; CREATE TABLE  `" + dbname + "`.`tblmedical_laboratory_xray` (`patientsNo` varchar(45) NOT NULL, `labExam` varchar(200) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblmedical_treatment`; CREATE TABLE  `" + dbname + "`.`tblmedical_treatment` (`patientsNo` varchar(35) NOT NULL, `treatment` varchar(500) NOT NULL, `medical_doctor` varchar(50) NOT NULL, `date` date NOT NULL, PRIMARY KEY(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblnext_in_line_doctor`; CREATE TABLE  `" + dbname + "`.`tblnext_in_line_doctor` (`numID` int(10) unsigned NOT NULL, `patientNo` varchar(45) NOT NULL, `lastName` varchar(100) NOT NULL, `firstName` varchar(100) NOT NULL, `middleName` varchar(100) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, `sentBy` varchar(45) NOT NULL, `sentTo` varchar(65) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblnext_in_line_laboratory1`; CREATE TABLE  `" + dbname + "`.`tblnext_in_line_laboratory1` ( `numID` int(10) unsigned NOT NULL, `patientNo` varchar(45) NOT NULL, `lastName` varchar(45) NOT NULL, `firstName` varchar(45) NOT NULL, `middleName` varchar(45) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblnext_in_line_laboratory2`; CREATE TABLE  `" + dbname + "`.`tblnext_in_line_laboratory2` (`numID` int(10) unsigned NOT NULL, `patientNo` varchar(45) NOT NULL, `lastName` varchar(45) NOT NULL, `firstName` varchar(45) NOT NULL, `middleName` varchar(45) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `examType` varchar(45) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblold_medical_diagnosis`; CREATE TABLE  `" + dbname + "`.`tblold_medical_diagnosis` (`patientsNo` varchar(35) NOT NULL, `medical_doctor` varchar(100) NOT NULL, `status` varchar(45) NOT NULL, `admissionStatus` varchar(45) NOT NULL, `diagnosis` varchar(255) NOT NULL, `notes` varchar(655) NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblold_medical_information`; CREATE TABLE  `" + dbname + "`.`tblold_medical_information` (`patientsNo` varchar(35) NOT NULL, `temperature` varchar(20) default NULL, `bp` varchar(20) default NULL, `cr` varchar(20) default NULL, `rr` varchar(20) default NULL, `weight` varchar(20) default NULL, `height` varchar(20) default NULL, `bmi` varchar(45) default NULL, `bmiCategory` varchar(65) default NULL, `complaints` varchar(500) default NULL, `symptoms` varchar(45) NOT NULL, `numberOfDaysWithSymptoms` varchar(45) NOT NULL, `morbidityWeek` varchar(45) NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblold_medical_laboratory`; CREATE TABLE  `" + dbname + "`.`tblold_medical_laboratory` ( `patientsNo` varchar(45) NOT NULL, `labExam` varchar(200) NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblold_medical_treatment`; CREATE TABLE  `" + dbname + "`.`tblold_medical_treatment` (`patientsNo` varchar(35) NOT NULL, `treatment` varchar(500) NOT NULL, `medical_doctor` varchar(50) NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblpatients_info`;CREATE TABLE  `" + dbname + "`.`tblpatients_info` (`patientsNo` varchar(45) NOT NULL, `lastName` varchar(75) NOT NULL, `firstName` varchar(75) NOT NULL, `middleName` varchar(75) NOT NULL, `sex` varchar(10) NOT NULL, `civilStatus` varchar(25) NOT NULL, `birthday` date NOT NULL, `age` int(10) unsigned NOT NULL, `address` varchar(355) NOT NULL, `date` date NOT NULL, PRIMARY KEY  USING BTREE(`patientsNo`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblstaff`; CREATE TABLE  `" + dbname + "`.`tblstaff` (`id` int(10) unsigned NOT NULL, `staff` varchar(55) NOT NULL, `firstName` varchar(55) NOT NULL, `middleInitial` varchar(5) NOT NULL, `lastName` varchar(55) NOT NULL, `degree` varchar(55) default NULL, PRIMARY KEY(`id`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblstatdisease`; CREATE TABLE  `" + dbname + "`.`tblstatdisease` (`month` varchar(45) NOT NULL, `year` varchar(45) NOT NULL, `disease` varchar(100) NOT NULL, `pedia` int(10) unsigned NOT NULL, `adult` int(10) unsigned NOT NULL, `count` int(10) unsigned NOT NULL, `L1M` int(10) unsigned NOT NULL, `L1F` int(10) unsigned NOT NULL, `OM` int(10) unsigned NOT NULL, `OF` int(10) unsigned NOT NULL, `FM` int(10) unsigned NOT NULL, `FF` int(10) unsigned NOT NULL, `TM` int(10) unsigned NOT NULL, `TF` int(10) unsigned NOT NULL, `FFM` int(10) unsigned NOT NULL, `FFF` int(10) unsigned NOT NULL, `TWM` int(10) unsigned NOT NULL, `TWF` int(10) unsigned NOT NULL, `FFFM` int(10) unsigned NOT NULL, `FFFF` int(10) unsigned NOT NULL, `GSFM` int(10) unsigned NOT NULL, `GSFF` int(10) unsigned NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblstatpatient`; CREATE TABLE  `" + dbname + "`.`tblstatpatient` (`month` varchar(45) NOT NULL, `year` varchar(45) NOT NULL, `pedia` int(10) unsigned NOT NULL, `adult` int(10) unsigned NOT NULL, `count` int(10) unsigned NOT NULL, `date` date NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblsymptoms`; CREATE TABLE  `" + dbname + "`.`tblsymptoms` (`id` int(10) unsigned NOT NULL, `symptoms` varchar(65) NOT NULL, PRIMARY KEY(`symptoms`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblwaiting_patient_nurse`; CREATE TABLE  `" + dbname + "`.`tblwaiting_patient_nurse` (`numID` int(10) unsigned NOT NULL, `patientNo` varchar(45) NOT NULL, `lastName` varchar(100) NOT NULL, `firstName` varchar(100) NOT NULL, `middleName` varchar(100) NOT NULL, `status` varchar(45) NOT NULL, `daterequested` date NOT NULL, `dateexamined` date NOT NULL, `sentBy` varchar(45) NOT NULL, `sentTo` varchar(100) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("DROP TABLE IF EXISTS `" + dbname + "`.`tblweekly_notifiable`; CREATE TABLE  `" + dbname + "`.`tblweekly_notifiable` (`id` int(10) unsigned NOT NULL, `weeklyNotifiable` varchar(65) NOT NULL, PRIMARY KEY  USING BTREE(`weeklyNotifiable`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        public void saveConfig()
        {
                Properties.Settings.Default.server = txtServer.Text;
                Properties.Settings.Default.database = txtDatabase.Text;
                Properties.Settings.Default.username = txtUsername.Text;
                Properties.Settings.Default.password = txtPassword.Text;
                Properties.Settings.Default.port = txtPort.Text;
                Properties.Settings.Default.Save();    
        }
        
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            MessageBox.Show("Successfully Resetted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmCheckConnection_Load(object sender, EventArgs e)
        {
            txtDatabase.Text = Properties.Settings.Default.database;
            txtServer.Text = Properties.Settings.Default.server;
            txtUsername.Text = Properties.Settings.Default.username;
            txtPassword.Text = Properties.Settings.Default.password;
            txtPort.Text = Properties.Settings.Default.port;
        }
    }
}
