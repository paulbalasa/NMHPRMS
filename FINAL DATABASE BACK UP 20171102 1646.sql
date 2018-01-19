-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.91-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema dbhospital
--

CREATE DATABASE IF NOT EXISTS dbhospital;
USE dbhospital;

--
-- Definition of table `tblaccount_profile`
--

DROP TABLE IF EXISTS `tblaccount_profile`;
CREATE TABLE `tblaccount_profile` (
  `IDNumber` varchar(45) NOT NULL,
  `accountType` varchar(50) NOT NULL,
  `doctorPosition` varchar(50) NOT NULL,
  `lastName` varchar(50) NOT NULL,
  `firstName` varchar(50) NOT NULL,
  `middleName` varchar(50) NOT NULL,
  `birthday` date NOT NULL,
  `email` varchar(50) default NULL,
  `profilePicture` longblob,
  `pathName` varchar(255) default NULL,
  PRIMARY KEY  (`IDNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblaccount_profile`
--

/*!40000 ALTER TABLE `tblaccount_profile` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblaccount_profile` ENABLE KEYS */;


--
-- Definition of table `tblaccounts`
--

DROP TABLE IF EXISTS `tblaccounts`;
CREATE TABLE `tblaccounts` (
  `accountNumber` int(10) unsigned NOT NULL,
  `accountType` varchar(45) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `IDNumber` varchar(50) NOT NULL,
  `archive` int(10) unsigned NOT NULL,
  `logInStatus` varchar(20) NOT NULL,
  `lockStatus` int(10) unsigned NOT NULL,
  `count` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblaccounts`
--

/*!40000 ALTER TABLE `tblaccounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblaccounts` ENABLE KEYS */;


--
-- Definition of table `tblactivity_log`
--

DROP TABLE IF EXISTS `tblactivity_log`;
CREATE TABLE `tblactivity_log` (
  `logNum` varchar(45) NOT NULL,
  `accountType` varchar(45) NOT NULL,
  `name` varchar(65) NOT NULL,
  `activity` varchar(755) NOT NULL,
  `date` date NOT NULL,
  `time` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblactivity_log`
--

/*!40000 ALTER TABLE `tblactivity_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblactivity_log` ENABLE KEYS */;


--
-- Definition of table `tblarchive_blood_chemistry`
--

DROP TABLE IF EXISTS `tblarchive_blood_chemistry`;
CREATE TABLE `tblarchive_blood_chemistry` (
  `patientsNo` varchar(45) NOT NULL,
  `fasting_blood_sugar` varchar(45) default NULL,
  `blood_urea_nitrogen` varchar(45) default NULL,
  `creatinine` varchar(45) default NULL,
  `cholesterol` varchar(45) default NULL,
  `triglycerides` varchar(45) default NULL,
  `hdl` varchar(45) default NULL,
  `ldl` varchar(45) default NULL,
  `vldl` varchar(45) default NULL,
  `blood_uric_acid` varchar(45) default NULL,
  `sodium` varchar(45) default NULL,
  `potassium` varchar(45) default NULL,
  `chloride` varchar(45) default NULL,
  `sgpt_alt` varchar(45) default NULL,
  `sgot_ast` varchar(45) default NULL,
  `medtech` varchar(65) NOT NULL,
  `pathologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_blood_chemistry`
--

/*!40000 ALTER TABLE `tblarchive_blood_chemistry` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_blood_chemistry` ENABLE KEYS */;


--
-- Definition of table `tblarchive_fecalysis_mac`
--

DROP TABLE IF EXISTS `tblarchive_fecalysis_mac`;
CREATE TABLE `tblarchive_fecalysis_mac` (
  `patientsNo` varchar(45) NOT NULL,
  `color` varchar(45) default NULL,
  `characters` varchar(45) default NULL,
  `reaction` varchar(45) default NULL,
  `occult_blood` varchar(45) default NULL,
  `medtech` varchar(65) NOT NULL,
  `pathologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_fecalysis_mac`
--

/*!40000 ALTER TABLE `tblarchive_fecalysis_mac` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_fecalysis_mac` ENABLE KEYS */;


--
-- Definition of table `tblarchive_fecalysis_mic`
--

DROP TABLE IF EXISTS `tblarchive_fecalysis_mic`;
CREATE TABLE `tblarchive_fecalysis_mic` (
  `patientsNo` varchar(45) NOT NULL,
  `pus_cells` varchar(45) default NULL,
  `rbc` varchar(45) default NULL,
  `fat_gobules` varchar(45) default NULL,
  `macrophage` varchar(45) default NULL,
  `bacteria` varchar(45) default NULL,
  `parasites_or_ova` varchar(45) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_fecalysis_mic`
--

/*!40000 ALTER TABLE `tblarchive_fecalysis_mic` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_fecalysis_mic` ENABLE KEYS */;


--
-- Definition of table `tblarchive_hematology`
--

DROP TABLE IF EXISTS `tblarchive_hematology`;
CREATE TABLE `tblarchive_hematology` (
  `patientsNo` varchar(45) NOT NULL,
  `hemoglobin` varchar(45) default NULL,
  `hematocrit` varchar(45) default NULL,
  `wbc_count` varchar(45) default NULL,
  `rbc_count` varchar(45) default NULL,
  `platelet` varchar(45) default NULL,
  `bleeding_time` varchar(45) default NULL,
  `clotting_time` varchar(45) default NULL,
  `abo_group` varchar(45) default NULL,
  `segmenters` varchar(45) default NULL,
  `lymphocytes` varchar(45) default NULL,
  `monocytes` varchar(45) default NULL,
  `eosinophils` varchar(45) default NULL,
  `basophils` varchar(45) default NULL,
  `stab` varchar(45) default NULL,
  `others` varchar(45) default NULL,
  `medtech` varchar(65) NOT NULL,
  `pathologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_hematology`
--

/*!40000 ALTER TABLE `tblarchive_hematology` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_hematology` ENABLE KEYS */;


--
-- Definition of table `tblarchive_medical_laboratory_1`
--

DROP TABLE IF EXISTS `tblarchive_medical_laboratory_1`;
CREATE TABLE `tblarchive_medical_laboratory_1` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(65) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_medical_laboratory_1`
--

/*!40000 ALTER TABLE `tblarchive_medical_laboratory_1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_medical_laboratory_1` ENABLE KEYS */;


--
-- Definition of table `tblarchive_medical_laboratory_ultrasound`
--

DROP TABLE IF EXISTS `tblarchive_medical_laboratory_ultrasound`;
CREATE TABLE `tblarchive_medical_laboratory_ultrasound` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_medical_laboratory_ultrasound`
--

/*!40000 ALTER TABLE `tblarchive_medical_laboratory_ultrasound` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_medical_laboratory_ultrasound` ENABLE KEYS */;


--
-- Definition of table `tblarchive_medical_laboratory_xray`
--

DROP TABLE IF EXISTS `tblarchive_medical_laboratory_xray`;
CREATE TABLE `tblarchive_medical_laboratory_xray` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_medical_laboratory_xray`
--

/*!40000 ALTER TABLE `tblarchive_medical_laboratory_xray` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_medical_laboratory_xray` ENABLE KEYS */;


--
-- Definition of table `tblarchive_ultrasound`
--

DROP TABLE IF EXISTS `tblarchive_ultrasound`;
CREATE TABLE `tblarchive_ultrasound` (
  `patientsNo` varchar(45) NOT NULL,
  `typeofexaminations` varchar(100) NOT NULL,
  `impression` varchar(2000) NOT NULL,
  `result` varchar(2000) NOT NULL,
  `sonologists` varchar(200) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_ultrasound`
--

/*!40000 ALTER TABLE `tblarchive_ultrasound` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_ultrasound` ENABLE KEYS */;


--
-- Definition of table `tblarchive_urinalysis_mac`
--

DROP TABLE IF EXISTS `tblarchive_urinalysis_mac`;
CREATE TABLE `tblarchive_urinalysis_mac` (
  `idNo` varchar(45) NOT NULL,
  `color` varchar(45) default NULL,
  `characters` varchar(45) default NULL,
  `protein` varchar(45) default NULL,
  `sugar` varchar(45) default NULL,
  `ph` varchar(45) default NULL,
  `spGr` varchar(45) default NULL,
  `pregnancyTest` varchar(45) default NULL,
  `medtech` varchar(65) NOT NULL,
  `pathologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_urinalysis_mac`
--

/*!40000 ALTER TABLE `tblarchive_urinalysis_mac` DISABLE KEYS */;
INSERT INTO `tblarchive_urinalysis_mac` (`idNo`,`color`,`characters`,`protein`,`sugar`,`ph`,`spGr`,`pregnancyTest`,`medtech`,`pathologists`,`daterequested`,`dateexamined`) VALUES 
 ('2017-1-NMH','','','','','','','','RODNIE TERUEL CAPITANIA R.M.T.','MICKEE S. MENDOZA ','2017-11-01','2017-11-01');
/*!40000 ALTER TABLE `tblarchive_urinalysis_mac` ENABLE KEYS */;


--
-- Definition of table `tblarchive_urinalysis_mic`
--

DROP TABLE IF EXISTS `tblarchive_urinalysis_mic`;
CREATE TABLE `tblarchive_urinalysis_mic` (
  `patientsNo` varchar(45) NOT NULL,
  `pus_cells` varchar(45) default NULL,
  `rbc` varchar(45) default NULL,
  `epith_cells` varchar(45) default NULL,
  `bacteria` varchar(45) default NULL,
  `mucus_thread` varchar(45) default NULL,
  `amorphous_urates` varchar(45) default NULL,
  `casts` varchar(45) default NULL,
  `crystals` varchar(45) default NULL,
  `others` varchar(45) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_urinalysis_mic`
--

/*!40000 ALTER TABLE `tblarchive_urinalysis_mic` DISABLE KEYS */;
INSERT INTO `tblarchive_urinalysis_mic` (`patientsNo`,`pus_cells`,`rbc`,`epith_cells`,`bacteria`,`mucus_thread`,`amorphous_urates`,`casts`,`crystals`,`others`) VALUES 
 ('2017-1-NMH','','fgydetg','','','','','','','');
/*!40000 ALTER TABLE `tblarchive_urinalysis_mic` ENABLE KEYS */;


--
-- Definition of table `tblarchive_xray`
--

DROP TABLE IF EXISTS `tblarchive_xray`;
CREATE TABLE `tblarchive_xray` (
  `patientsNo` varchar(45) NOT NULL,
  `typeofexamination` varchar(45) NOT NULL,
  `impression` varchar(2000) NOT NULL,
  `results` varchar(2000) NOT NULL,
  `radiologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblarchive_xray`
--

/*!40000 ALTER TABLE `tblarchive_xray` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblarchive_xray` ENABLE KEYS */;


--
-- Definition of table `tbldoctors`
--

DROP TABLE IF EXISTS `tbldoctors`;
CREATE TABLE `tbldoctors` (
  `doctorPosition` varchar(75) NOT NULL,
  PRIMARY KEY  (`doctorPosition`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbldoctors`
--

/*!40000 ALTER TABLE `tbldoctors` DISABLE KEYS */;
INSERT INTO `tbldoctors` (`doctorPosition`) VALUES 
 ('PHYSICIAN');
/*!40000 ALTER TABLE `tbldoctors` ENABLE KEYS */;


--
-- Definition of table `tbldoctors_schedule`
--

DROP TABLE IF EXISTS `tbldoctors_schedule`;
CREATE TABLE `tbldoctors_schedule` (
  `IDNumber` varchar(45) NOT NULL,
  `name` varchar(200) NOT NULL,
  `schedule` varchar(45) NOT NULL,
  `time` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbldoctors_schedule`
--

/*!40000 ALTER TABLE `tbldoctors_schedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbldoctors_schedule` ENABLE KEYS */;


--
-- Definition of table `tbldoctors_schedule1`
--

DROP TABLE IF EXISTS `tbldoctors_schedule1`;
CREATE TABLE `tbldoctors_schedule1` (
  `IDNumber` varchar(45) NOT NULL,
  `name` varchar(200) NOT NULL,
  `position` varchar(200) NOT NULL,
  `schedule1` varchar(45) NOT NULL,
  PRIMARY KEY  (`IDNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbldoctors_schedule1`
--

/*!40000 ALTER TABLE `tbldoctors_schedule1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbldoctors_schedule1` ENABLE KEYS */;


--
-- Definition of table `tbldrafts_nurse`
--

DROP TABLE IF EXISTS `tbldrafts_nurse`;
CREATE TABLE `tbldrafts_nurse` (
  `numID` int(10) unsigned NOT NULL,
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(200) NOT NULL,
  `firstName` varchar(200) NOT NULL,
  `middleName` varchar(200) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  `sentBy` varchar(200) NOT NULL,
  `sentTo` varchar(200) NOT NULL,
  PRIMARY KEY  (`numID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbldrafts_nurse`
--

/*!40000 ALTER TABLE `tbldrafts_nurse` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbldrafts_nurse` ENABLE KEYS */;


--
-- Definition of table `tblemail`
--

DROP TABLE IF EXISTS `tblemail`;
CREATE TABLE `tblemail` (
  `emailadd` varchar(55) NOT NULL,
  `password` varchar(55) NOT NULL,
  `host` varchar(45) NOT NULL,
  `port` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblemail`
--

/*!40000 ALTER TABLE `tblemail` DISABLE KEYS */;
INSERT INTO `tblemail` (`emailadd`,`password`,`host`,`port`) VALUES 
 ('nmhdev0@gmail.com','nmhadmin','smtp.gmail.com','587');
/*!40000 ALTER TABLE `tblemail` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory1_results`
--

DROP TABLE IF EXISTS `tbllaboratory1_results`;
CREATE TABLE `tbllaboratory1_results` (
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `middleName` varchar(45) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory1_results`
--

/*!40000 ALTER TABLE `tbllaboratory1_results` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory1_results` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory2_results`
--

DROP TABLE IF EXISTS `tbllaboratory2_results`;
CREATE TABLE `tbllaboratory2_results` (
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `middleName` varchar(45) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory2_results`
--

/*!40000 ALTER TABLE `tbllaboratory2_results` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory2_results` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_blood_chemistry`
--

DROP TABLE IF EXISTS `tbllaboratory_blood_chemistry`;
CREATE TABLE `tbllaboratory_blood_chemistry` (
  `patientsNo` varchar(45) NOT NULL,
  `fasting_blood_sugar` varchar(45) default NULL,
  `blood_urea_nitrogen` varchar(45) default NULL,
  `creatinine` varchar(45) default NULL,
  `cholesterol` varchar(45) default NULL,
  `triglycerides` varchar(45) default NULL,
  `hdl` varchar(45) default NULL,
  `ldl` varchar(45) default NULL,
  `vldl` varchar(45) default NULL,
  `blood_uric_acid` varchar(45) default NULL,
  `sodium` varchar(45) default NULL,
  `potassium` varchar(45) default NULL,
  `chloride` varchar(45) default NULL,
  `sgpt_alt` varchar(45) default NULL,
  `sgot_ast` varchar(45) default NULL,
  `medtech` varchar(45) NOT NULL,
  `pathologists` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_blood_chemistry`
--

/*!40000 ALTER TABLE `tbllaboratory_blood_chemistry` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_blood_chemistry` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_fecalysis_mac`
--

DROP TABLE IF EXISTS `tbllaboratory_fecalysis_mac`;
CREATE TABLE `tbllaboratory_fecalysis_mac` (
  `patientsNo` varchar(45) NOT NULL,
  `color` varchar(45) default NULL,
  `characters` varchar(45) default NULL,
  `reaction` varchar(45) default NULL,
  `occult_blood` varchar(45) default NULL,
  `medtech` varchar(45) NOT NULL,
  `pathologists` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_fecalysis_mac`
--

/*!40000 ALTER TABLE `tbllaboratory_fecalysis_mac` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_fecalysis_mac` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_fecalysis_mic`
--

DROP TABLE IF EXISTS `tbllaboratory_fecalysis_mic`;
CREATE TABLE `tbllaboratory_fecalysis_mic` (
  `patientsNo` varchar(45) NOT NULL,
  `pus_cells` varchar(45) default NULL,
  `rbc` varchar(45) default NULL,
  `fat_globules` varchar(45) default NULL,
  `macrophage` varchar(45) default NULL,
  `bacteria` varchar(45) default NULL,
  `parasites_or_ova` varchar(45) default NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_fecalysis_mic`
--

/*!40000 ALTER TABLE `tbllaboratory_fecalysis_mic` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_fecalysis_mic` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_hematology`
--

DROP TABLE IF EXISTS `tbllaboratory_hematology`;
CREATE TABLE `tbllaboratory_hematology` (
  `patientsNo` varchar(45) NOT NULL,
  `hemoglobin` varchar(45) default NULL,
  `hematocrit` varchar(45) default NULL,
  `wbc_count` varchar(45) default NULL,
  `rbc_count` varchar(45) default NULL,
  `platelet` varchar(45) default NULL,
  `bleeding_time` varchar(45) default NULL,
  `clotting_time` varchar(45) default NULL,
  `abo_group` varchar(45) default NULL,
  `segmenters` varchar(45) default NULL,
  `lymphocytes` varchar(45) default NULL,
  `monocytes` varchar(45) default NULL,
  `eosinophils` varchar(45) default NULL,
  `basophils` varchar(45) default NULL,
  `stab` varchar(45) default NULL,
  `others` varchar(45) default NULL,
  `medtech` varchar(65) NOT NULL,
  `pathologists` varchar(65) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_hematology`
--

/*!40000 ALTER TABLE `tbllaboratory_hematology` DISABLE KEYS */;
INSERT INTO `tbllaboratory_hematology` (`patientsNo`,`hemoglobin`,`hematocrit`,`wbc_count`,`rbc_count`,`platelet`,`bleeding_time`,`clotting_time`,`abo_group`,`segmenters`,`lymphocytes`,`monocytes`,`eosinophils`,`basophils`,`stab`,`others`,`medtech`,`pathologists`,`daterequested`,`dateexamined`) VALUES 
 ('2017-1-NMH','','','edrtfdg','','','','','','','','','','','','','RODNIE TERUEL CAPITANIA R.M.T.','MICKEE S. MENDOZA ','2017-11-01','2017-11-01');
/*!40000 ALTER TABLE `tbllaboratory_hematology` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_ultrasound`
--

DROP TABLE IF EXISTS `tbllaboratory_ultrasound`;
CREATE TABLE `tbllaboratory_ultrasound` (
  `patientsNo` varchar(45) NOT NULL,
  `typeofexaminations` varchar(100) NOT NULL,
  `impression` varchar(2000) NOT NULL,
  `result` varchar(2000) NOT NULL,
  `sonologists` varchar(200) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_ultrasound`
--

/*!40000 ALTER TABLE `tbllaboratory_ultrasound` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_ultrasound` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_urinalysis_mac`
--

DROP TABLE IF EXISTS `tbllaboratory_urinalysis_mac`;
CREATE TABLE `tbllaboratory_urinalysis_mac` (
  `patientsNo` varchar(45) NOT NULL,
  `color` varchar(45) default NULL,
  `characters` varchar(45) default NULL,
  `protein` varchar(45) default NULL,
  `sugar` varchar(45) default NULL,
  `ph` varchar(45) default NULL,
  `spGr` varchar(45) default NULL,
  `pregnancyTest` varchar(45) default NULL,
  `medtech` varchar(45) NOT NULL,
  `pathologists` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_urinalysis_mac`
--

/*!40000 ALTER TABLE `tbllaboratory_urinalysis_mac` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_urinalysis_mac` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_urinalysis_mic`
--

DROP TABLE IF EXISTS `tbllaboratory_urinalysis_mic`;
CREATE TABLE `tbllaboratory_urinalysis_mic` (
  `patientsNo` varchar(45) NOT NULL,
  `pus_cells` varchar(45) default NULL,
  `rbc` varchar(45) default NULL,
  `epith_cells` varchar(45) default NULL,
  `bacteria` varchar(45) default NULL,
  `mucus_thread` varchar(45) default NULL,
  `amorphous_urates` varchar(45) default NULL,
  `casts` varchar(45) default NULL,
  `crystals` varchar(45) default NULL,
  `others` varchar(45) default NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_urinalysis_mic`
--

/*!40000 ALTER TABLE `tbllaboratory_urinalysis_mic` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_urinalysis_mic` ENABLE KEYS */;


--
-- Definition of table `tbllaboratory_xray`
--

DROP TABLE IF EXISTS `tbllaboratory_xray`;
CREATE TABLE `tbllaboratory_xray` (
  `patientsNo` varchar(45) NOT NULL,
  `typeofexaminations` varchar(100) NOT NULL,
  `impression` varchar(2000) NOT NULL,
  `result` varchar(2000) NOT NULL,
  `radiologists` varchar(200) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbllaboratory_xray`
--

/*!40000 ALTER TABLE `tbllaboratory_xray` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbllaboratory_xray` ENABLE KEYS */;


--
-- Definition of table `tblmedical_diagnosis`
--

DROP TABLE IF EXISTS `tblmedical_diagnosis`;
CREATE TABLE `tblmedical_diagnosis` (
  `patientsNo` varchar(35) NOT NULL,
  `medical_doctor` varchar(100) NOT NULL,
  `status` varchar(45) NOT NULL,
  `admissionStatus` varchar(45) NOT NULL,
  `diagnosis` varchar(255) NOT NULL,
  `notes` varchar(655) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_diagnosis`
--

/*!40000 ALTER TABLE `tblmedical_diagnosis` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_diagnosis` ENABLE KEYS */;


--
-- Definition of table `tblmedical_information`
--

DROP TABLE IF EXISTS `tblmedical_information`;
CREATE TABLE `tblmedical_information` (
  `patientsNo` varchar(35) NOT NULL,
  `temperature` varchar(20) default NULL,
  `bp` varchar(20) default NULL,
  `cr` varchar(20) default NULL,
  `rr` varchar(20) default NULL,
  `weight` varchar(45) default NULL,
  `height` varchar(45) default NULL,
  `bmi` varchar(45) default NULL,
  `bmiCategory` varchar(65) default NULL,
  `complaints` varchar(500) default NULL,
  `symptoms` varchar(45) NOT NULL,
  `numberOfDaysWithSymptoms` varchar(45) default NULL,
  `morbidityWeek` varchar(45) default NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_information`
--

/*!40000 ALTER TABLE `tblmedical_information` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_information` ENABLE KEYS */;


--
-- Definition of table `tblmedical_laboratory_1`
--

DROP TABLE IF EXISTS `tblmedical_laboratory_1`;
CREATE TABLE `tblmedical_laboratory_1` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_laboratory_1`
--

/*!40000 ALTER TABLE `tblmedical_laboratory_1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_laboratory_1` ENABLE KEYS */;


--
-- Definition of table `tblmedical_laboratory_ultrasound`
--

DROP TABLE IF EXISTS `tblmedical_laboratory_ultrasound`;
CREATE TABLE `tblmedical_laboratory_ultrasound` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_laboratory_ultrasound`
--

/*!40000 ALTER TABLE `tblmedical_laboratory_ultrasound` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_laboratory_ultrasound` ENABLE KEYS */;


--
-- Definition of table `tblmedical_laboratory_xray`
--

DROP TABLE IF EXISTS `tblmedical_laboratory_xray`;
CREATE TABLE `tblmedical_laboratory_xray` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_laboratory_xray`
--

/*!40000 ALTER TABLE `tblmedical_laboratory_xray` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_laboratory_xray` ENABLE KEYS */;


--
-- Definition of table `tblmedical_treatment`
--

DROP TABLE IF EXISTS `tblmedical_treatment`;
CREATE TABLE `tblmedical_treatment` (
  `patientsNo` varchar(35) NOT NULL,
  `treatment` varchar(500) NOT NULL,
  `medical_doctor` varchar(50) NOT NULL,
  `date` date NOT NULL,
  PRIMARY KEY  (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmedical_treatment`
--

/*!40000 ALTER TABLE `tblmedical_treatment` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblmedical_treatment` ENABLE KEYS */;


--
-- Definition of table `tblnext_in_line_doctor`
--

DROP TABLE IF EXISTS `tblnext_in_line_doctor`;
CREATE TABLE `tblnext_in_line_doctor` (
  `numID` int(10) unsigned NOT NULL,
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(100) NOT NULL,
  `firstName` varchar(100) NOT NULL,
  `middleName` varchar(100) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  `sentBy` varchar(45) NOT NULL,
  `sentTo` varchar(65) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblnext_in_line_doctor`
--

/*!40000 ALTER TABLE `tblnext_in_line_doctor` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblnext_in_line_doctor` ENABLE KEYS */;


--
-- Definition of table `tblnext_in_line_laboratory1`
--

DROP TABLE IF EXISTS `tblnext_in_line_laboratory1`;
CREATE TABLE `tblnext_in_line_laboratory1` (
  `numID` int(10) unsigned NOT NULL,
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `middleName` varchar(45) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblnext_in_line_laboratory1`
--

/*!40000 ALTER TABLE `tblnext_in_line_laboratory1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblnext_in_line_laboratory1` ENABLE KEYS */;


--
-- Definition of table `tblnext_in_line_laboratory2`
--

DROP TABLE IF EXISTS `tblnext_in_line_laboratory2`;
CREATE TABLE `tblnext_in_line_laboratory2` (
  `numID` int(10) unsigned NOT NULL,
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `middleName` varchar(45) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `examType` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblnext_in_line_laboratory2`
--

/*!40000 ALTER TABLE `tblnext_in_line_laboratory2` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblnext_in_line_laboratory2` ENABLE KEYS */;


--
-- Definition of table `tblold_medical_diagnosis`
--

DROP TABLE IF EXISTS `tblold_medical_diagnosis`;
CREATE TABLE `tblold_medical_diagnosis` (
  `patientsNo` varchar(35) NOT NULL,
  `medical_doctor` varchar(100) NOT NULL,
  `status` varchar(45) NOT NULL,
  `admissionStatus` varchar(45) NOT NULL,
  `diagnosis` varchar(255) NOT NULL,
  `notes` varchar(655) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblold_medical_diagnosis`
--

/*!40000 ALTER TABLE `tblold_medical_diagnosis` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblold_medical_diagnosis` ENABLE KEYS */;


--
-- Definition of table `tblold_medical_information`
--

DROP TABLE IF EXISTS `tblold_medical_information`;
CREATE TABLE `tblold_medical_information` (
  `patientsNo` varchar(35) NOT NULL,
  `temperature` varchar(20) default NULL,
  `bp` varchar(20) default NULL,
  `cr` varchar(20) default NULL,
  `rr` varchar(20) default NULL,
  `weight` varchar(20) default NULL,
  `height` varchar(20) default NULL,
  `bmi` varchar(45) default NULL,
  `bmiCategory` varchar(65) default NULL,
  `complaints` varchar(500) default NULL,
  `symptoms` varchar(45) NOT NULL,
  `numberOfDaysWithSymptoms` varchar(45) NOT NULL,
  `morbidityWeek` varchar(45) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblold_medical_information`
--

/*!40000 ALTER TABLE `tblold_medical_information` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblold_medical_information` ENABLE KEYS */;


--
-- Definition of table `tblold_medical_laboratory`
--

DROP TABLE IF EXISTS `tblold_medical_laboratory`;
CREATE TABLE `tblold_medical_laboratory` (
  `patientsNo` varchar(45) NOT NULL,
  `labExam` varchar(200) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblold_medical_laboratory`
--

/*!40000 ALTER TABLE `tblold_medical_laboratory` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblold_medical_laboratory` ENABLE KEYS */;


--
-- Definition of table `tblold_medical_treatment`
--

DROP TABLE IF EXISTS `tblold_medical_treatment`;
CREATE TABLE `tblold_medical_treatment` (
  `patientsNo` varchar(35) NOT NULL,
  `treatment` varchar(500) NOT NULL,
  `medical_doctor` varchar(50) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblold_medical_treatment`
--

/*!40000 ALTER TABLE `tblold_medical_treatment` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblold_medical_treatment` ENABLE KEYS */;


--
-- Definition of table `tblpatients_info`
--

DROP TABLE IF EXISTS `tblpatients_info`;
CREATE TABLE `tblpatients_info` (
  `patientsNo` varchar(45) NOT NULL,
  `lastName` varchar(75) NOT NULL,
  `firstName` varchar(75) NOT NULL,
  `middleName` varchar(75) NOT NULL,
  `sex` varchar(10) NOT NULL,
  `civilStatus` varchar(25) NOT NULL,
  `birthday` date NOT NULL,
  `age` int(10) unsigned NOT NULL,
  `address` varchar(355) NOT NULL,
  `date` date NOT NULL,
  PRIMARY KEY  USING BTREE (`patientsNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblpatients_info`
--

/*!40000 ALTER TABLE `tblpatients_info` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblpatients_info` ENABLE KEYS */;


--
-- Definition of table `tblquestions`
--

DROP TABLE IF EXISTS `tblquestions`;
CREATE TABLE `tblquestions` (
  `IDNumber` varchar(45) NOT NULL,
  `qNum` int(10) unsigned NOT NULL,
  `question` varchar(255) NOT NULL,
  `answer` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblquestions`
--

/*!40000 ALTER TABLE `tblquestions` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblquestions` ENABLE KEYS */;


--
-- Definition of table `tblstaff`
--

DROP TABLE IF EXISTS `tblstaff`;
CREATE TABLE `tblstaff` (
  `id` int(10) unsigned NOT NULL,
  `staff` varchar(55) NOT NULL,
  `firstName` varchar(55) NOT NULL,
  `middleInitial` varchar(5) NOT NULL,
  `lastName` varchar(55) NOT NULL,
  `degree` varchar(55) default NULL,
  `archive` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblstaff`
--

/*!40000 ALTER TABLE `tblstaff` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblstaff` ENABLE KEYS */;


--
-- Definition of table `tblstatdisease`
--

DROP TABLE IF EXISTS `tblstatdisease`;
CREATE TABLE `tblstatdisease` (
  `month` varchar(45) NOT NULL,
  `year` varchar(45) NOT NULL,
  `disease` varchar(100) NOT NULL,
  `pedia` int(10) unsigned NOT NULL,
  `adult` int(10) unsigned NOT NULL,
  `count` int(10) unsigned NOT NULL,
  `L1M` int(10) unsigned NOT NULL,
  `L1F` int(10) unsigned NOT NULL,
  `OM` int(10) unsigned NOT NULL,
  `OF` int(10) unsigned NOT NULL,
  `FM` int(10) unsigned NOT NULL,
  `FF` int(10) unsigned NOT NULL,
  `TM` int(10) unsigned NOT NULL,
  `TF` int(10) unsigned NOT NULL,
  `FFM` int(10) unsigned NOT NULL,
  `FFF` int(10) unsigned NOT NULL,
  `TWM` int(10) unsigned NOT NULL,
  `TWF` int(10) unsigned NOT NULL,
  `FFFM` int(10) unsigned NOT NULL,
  `FFFF` int(10) unsigned NOT NULL,
  `GSFM` int(10) unsigned NOT NULL,
  `GSFF` int(10) unsigned NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblstatdisease`
--

/*!40000 ALTER TABLE `tblstatdisease` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblstatdisease` ENABLE KEYS */;


--
-- Definition of table `tblstatpatient`
--

DROP TABLE IF EXISTS `tblstatpatient`;
CREATE TABLE `tblstatpatient` (
  `month` varchar(45) NOT NULL,
  `year` varchar(45) NOT NULL,
  `pedia` int(10) unsigned NOT NULL,
  `adult` int(10) unsigned NOT NULL,
  `count` int(10) unsigned NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblstatpatient`
--

/*!40000 ALTER TABLE `tblstatpatient` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblstatpatient` ENABLE KEYS */;


--
-- Definition of table `tblsymptoms`
--

DROP TABLE IF EXISTS `tblsymptoms`;
CREATE TABLE `tblsymptoms` (
  `id` int(10) unsigned NOT NULL,
  `symptoms` varchar(65) NOT NULL,
  PRIMARY KEY  (`symptoms`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblsymptoms`
--

/*!40000 ALTER TABLE `tblsymptoms` DISABLE KEYS */;
INSERT INTO `tblsymptoms` (`id`,`symptoms`) VALUES 
 (7,'ARTHRALGIA'),
 (5,'ARTHRITIS - ANKLES'),
 (4,'ARTHRITIS - FEET'),
 (3,'ARTHRITIS - HANDS'),
 (6,'ARTHRITIS - OTHER'),
 (15,'ASTHENIA'),
 (10,'BACK PAIN'),
 (2,'COLD'),
 (1,'COUGH'),
 (11,'HEADACHE'),
 (16,'MANIGOENCIPHALITIS'),
 (13,'MUSCOCAL BLEEDING'),
 (9,'MYALGIA'),
 (12,'NAUSEA'),
 (17,'PERIARTICULAR EDEMA'),
 (8,'SKIN MANIFESTATIONS'),
 (14,'VOMITTING');
/*!40000 ALTER TABLE `tblsymptoms` ENABLE KEYS */;


--
-- Definition of table `tblwaiting_patient_nurse`
--

DROP TABLE IF EXISTS `tblwaiting_patient_nurse`;
CREATE TABLE `tblwaiting_patient_nurse` (
  `numID` int(10) unsigned NOT NULL,
  `patientNo` varchar(45) NOT NULL,
  `lastName` varchar(100) NOT NULL,
  `firstName` varchar(100) NOT NULL,
  `middleName` varchar(100) NOT NULL,
  `status` varchar(45) NOT NULL,
  `daterequested` date NOT NULL,
  `dateexamined` date NOT NULL,
  `sentBy` varchar(45) NOT NULL,
  `sentTo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblwaiting_patient_nurse`
--

/*!40000 ALTER TABLE `tblwaiting_patient_nurse` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblwaiting_patient_nurse` ENABLE KEYS */;


--
-- Definition of table `tblweekly_notifiable`
--

DROP TABLE IF EXISTS `tblweekly_notifiable`;
CREATE TABLE `tblweekly_notifiable` (
  `id` int(10) unsigned NOT NULL,
  `weeklyNotifiable` varchar(65) NOT NULL,
  PRIMARY KEY  USING BTREE (`weeklyNotifiable`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblweekly_notifiable`
--

/*!40000 ALTER TABLE `tblweekly_notifiable` DISABLE KEYS */;
INSERT INTO `tblweekly_notifiable` (`id`,`weeklyNotifiable`) VALUES 
 (1,'ACUTE BLOODY DIARRHEA'),
 (4,'ACUTE ENSEPHALITIS SYNDROME'),
 (3,'ACUTE HEMORRHAGIC FEVER'),
 (21,'ACUTE VIRAL HEPATITIS'),
 (25,'AEFI'),
 (26,'AFP'),
 (2,'AMES'),
 (27,'ANTHRAX'),
 (15,'BACTERIAL MENINGITIS'),
 (16,'CHIKUNGUYA'),
 (17,'CHOLERA'),
 (5,'DENGUE'),
 (6,'DIPHTERIA'),
 (28,'FEVER'),
 (10,'HAND FOOT AND MOUTH DISEASE'),
 (23,'HIGHBLOOD PRESSURE'),
 (7,'INFLUENZA-LIKE-ILLNESS'),
 (20,'LEPTOSPIROSIS'),
 (22,'MALARIA'),
 (12,'MEASLES'),
 (11,'MENINGOCOCCAL DISEASE'),
 (13,'NEONATAL TETANUS'),
 (18,'NON-NEONATAL TETANUS'),
 (8,'PARALYTIC SHELLFISH POISONING'),
 (19,'PERTUSSIS'),
 (9,'RABBIES'),
 (24,'ROTAVIRUS'),
 (14,'TYPHOID AND PARATYPHOID FEVER');
/*!40000 ALTER TABLE `tblweekly_notifiable` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
