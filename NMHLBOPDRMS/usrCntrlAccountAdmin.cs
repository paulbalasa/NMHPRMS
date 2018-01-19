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
using System.IO;

namespace NMHLBOPDRMS
{
    public partial class usrCntrlAccountAdmin : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public usrCntrlAccountAdmin()
        {
            InitializeComponent();
        }

        public void getDoctor()
        {
            string name = "";
            conn.Open();
            string sql = "SELECT * FROM tblaccount_profile WHERE accountType = 'DOCTOR'";
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd1.ExecuteReader();
            string fname = "";
            string mname = "";
            string lname = "";
            while (reader.Read())
            {
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                name = "Dr. " + fname + " " + mname + " " + lname;
                cboDoctor.Items.Add(name);
            }
            conn.Close();
        }

        public void getNurs()
        {
            string name = "";
            conn.Open();
            string sql = "SELECT * FROM tblaccount_profile WHERE accountType = 'NURSE'";
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd1.ExecuteReader();
            string fname = "";
            string mname = "";
            string lname = "";
            while (reader.Read())
            {
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                name = fname + " " + mname + " " + lname;
                cboDoctor.Items.Add(name);
            }
            conn.Close();
        }

        public void getAccount()
        {
            conn.Open();
            string sql = "SELECT a.accountType AS 'Account Type', b.doctorPosition AS 'Doctor (Type)', a.logInStatus AS 'Log In Status', a.username AS 'Username', a.IDNumber AS 'ID Number', b.lastName AS 'Last Name', b.firstName AS 'First Name', b.middleName AS 'Middle Name', b.birthday AS 'Birthday' FROM tblAccounts a JOIN tblAccount_Profile b ON a.IDNumber = b.IDNumber WHERE a.archive = 'NO' ";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblAccounts, tblAccount_Profile");
            dgAccounts.DataMember = "tblAccounts, tblAccount_Profile";
            dgAccounts.DataSource = dS;
            conn.Close();

            DataGridViewButtonColumn unlockButtonColumn = new DataGridViewButtonColumn();
            unlockButtonColumn.Name = "Unlock";
            unlockButtonColumn.Text = "Unlock";
            unlockButtonColumn.UseColumnTextForButtonValue = true;

            if (dgAccounts.Columns["Unlock"] == null)
            {
                dgAccounts.Columns.Insert(9, unlockButtonColumn);
            }

            DataGridViewButtonColumn archiveButton = new DataGridViewButtonColumn();
            archiveButton.Name = "Archive";
            archiveButton.Text = "Archive";
            archiveButton.UseColumnTextForButtonValue = true;

            if (dgAccounts.Columns["Archive"] == null)
            {
                dgAccounts.Columns.Insert(10, archiveButton);
            }

            conn.Open();
            string sql1 = "SELECT a.accountType AS 'Account Type', a.username AS 'Username', a.IDNumber AS 'ID Number', b.lastName AS 'Last Name', b.firstName AS 'First Name', b.middleName AS 'Middle Name', b.birthday AS 'Birthday' FROM tblAccounts a JOIN tblAccount_Profile b ON a.IDNumber = b.IDNumber";
            adapter = new MySqlDataAdapter(sql1, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblAccounts, tblAccount_Profile");
            dgAccounts1.DataMember = "tblAccounts, tblAccount_Profile";
            dgAccounts1.DataSource = dS;
            conn.Close();

            DataGridViewButtonColumn unarchiveButtonColumn = new DataGridViewButtonColumn();
            unarchiveButtonColumn.Name = "Unarchive";
            unarchiveButtonColumn.Text = "Unarchive";
            unarchiveButtonColumn.UseColumnTextForButtonValue = true;

            if (dgAccounts1.Columns["Unarchive"] == null)
            {
                dgAccounts1.Columns.Insert(7, unarchiveButtonColumn);
            }

            dgAccounts.Columns[0].Width = 175;
            dgAccounts.Columns[1].Width = 140;
            dgAccounts.Columns[2].Width = 100;
            dgAccounts.Columns[3].Width = 100;
            dgAccounts.Columns[4].Width = 150;
            dgAccounts.Columns[5].Width = 200;
            dgAccounts.Columns[6].Width = 200;
            dgAccounts.Columns[7].Width = 200;
            dgAccounts.Columns[8].Width = 100;
            dgAccounts.Columns[9].Width = 100;

            dgAccounts1.Columns[0].Width = 175;
            dgAccounts1.Columns[1].Width = 100;
            dgAccounts1.Columns[2].Width = 150;
            dgAccounts1.Columns[3].Width = 200;
            dgAccounts1.Columns[4].Width = 200;
            dgAccounts1.Columns[5].Width = 200;
            dgAccounts1.Columns[6].Width = 100;
            dgAccounts1.Columns[7].Width = 100;
        }

        public void getSchedule()
        {
            conn.Open();
            string sql2 = "SELECT IDNumber AS 'ID Number', name AS 'Name of Doctor', position AS 'Position of Doctor', schedule1 AS 'Schedule' FROM tbldoctors_schedule1";
            adapter = new MySqlDataAdapter(sql2, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tbldoctors_schedule1");
            dgSchedule.DataMember = "tbldoctors_schedule1";
            dgSchedule.DataSource = dS;
            conn.Close();

            dgSchedule.Columns[0].Width = 150;
            dgSchedule.Columns[1].Width = 262;
            dgSchedule.Columns[2].Width = 150;
            dgSchedule.Columns[3].Width = 150;
        }

        public void getStaff()
        {
            conn.Open();
            string sql3 = "SELECT id AS 'ID', staff AS 'Type of Staff', firstName AS 'First Name', middleInitial AS 'Middle Initial', lastName AS 'Last Name', degree AS 'Medical Degree' FROM tblStaff WHERE archive = 0";
            adapter = new MySqlDataAdapter(sql3, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblStaff");
            dgStaff.DataMember = "tblStaff";
            dgStaff.DataSource = dS;
            conn.Close();

            DataGridViewButtonColumn archiveButton = new DataGridViewButtonColumn();
            archiveButton.Name = "Archive";
            archiveButton.Text = "Archive";
            archiveButton.UseColumnTextForButtonValue = true;

            if (dgStaff.Columns["Archive"] == null)
            {
                dgStaff.Columns.Insert(6, archiveButton);
            }

            conn.Open();
            string sql4 = "SELECT id AS 'ID', staff AS 'Type of Staff', firstName AS 'First Name', middleInitial AS 'Middle Initial', lastName AS 'Last Name', degree AS 'Medical Degree' FROM tblStaff WHERE archive = 1";
            adapter = new MySqlDataAdapter(sql4, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblStaff");
            dgStaff1.DataMember = "tblStaff";
            dgStaff1.DataSource = dS;
            conn.Close();

            DataGridViewButtonColumn unArchiveButton = new DataGridViewButtonColumn();
            unArchiveButton.Name = "Unarchive";
            unArchiveButton.Text = "Unarchive";
            unArchiveButton.UseColumnTextForButtonValue = true;

            if (dgStaff1.Columns["Unarchive"] == null)
            {
                dgStaff1.Columns.Insert(0, unArchiveButton);
            }

            dgStaff.Columns[0].Width = 50;
            dgStaff.Columns[1].Width = 200;
            dgStaff.Columns[2].Width = 170;
            dgStaff.Columns[3].Width = 170;
            dgStaff.Columns[4].Width = 170;
            dgStaff.Columns[5].Width = 170;

            dgStaff1.Columns[0].Width = 100;
            dgStaff1.Columns[1].Width = 50;
            dgStaff1.Columns[2].Width = 200;
            dgStaff1.Columns[3].Width = 170;
            dgStaff1.Columns[4].Width = 170;
            dgStaff1.Columns[5].Width = 170;
            dgStaff1.Columns[6].Width = 170;
        }

        private void usrCntrlAccountAdmin_Load(object sender, EventArgs e)
        {
            getAccount();

            getSchedule();

            getStaff();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts WHERE archive = 'NO' ", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";

            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblDoctors_Schedule1", conn);
            Int32 count1 = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();
            lblTotalSchedule.Text = count1.ToString() + " records.";

            conn.Open();
            MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM tblstaff WHERE archive = 0", conn);
            Int32 count2 = Convert.ToInt32(cmd2.ExecuteScalar());
            conn.Close();
            lblTotalStaff.Text = count2.ToString() + " records.";

            getDoctor();
            getNurs();

            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        public string accountType;

        private void dgAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            accountType = dgAccounts.SelectedCells[0].Value.ToString();
            string idnum = dgAccounts.SelectedCells[4].Value.ToString();

            if (accountType != "ADMINISTRATOR")
            {
                frmAccount f1 = new frmAccount();

                conn.Open();
                string sql = "SELECT * FROM tblAccounts WHERE IDNumber = @idnum";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idnum", idnum);
                MySqlDataReader reader = cmd.ExecuteReader();
                string username = "";
                string password = "";
                string IDNumber = "";
                while (reader.Read())
                {
                    username = reader.GetString("username");
                    password = reader.GetString("password");
                    IDNumber = reader.GetString("IDNumber");
                }
                conn.Close();

                conn.Open();
                string sql1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @IDNumber";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@IDNumber", IDNumber);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                string lastName = "";
                string firstName = "";
                string middleName = "";
                string birthday = "";
                while (reader1.Read())
                {
                    lastName = reader1.GetString("lastName");
                    firstName = reader1.GetString("firstName");
                    middleName = reader1.GetString("middleName");
                    birthday = reader1.GetString("birthday");
                }
                conn.Close();

                f1.lblAccount.Text = accountType + "'s Account";
                f1.lblType.Text = accountType;
                f1.txtIDNumber.Text = IDNumber;
                f1.txtLastName.Text = lastName;
                f1.txtFirstName.Text = firstName;
                f1.txtMiddleName.Text = middleName;
                f1.dtBirthday.Text = birthday;
                f1.Show();
            }
            else
                MessageBox.Show("Administrator Account cannot edit Information here.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btnCreateUserAccount_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmCreateUserAccount f1 = new frmCreateUserAccount();
            f1.Show();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dgAccounts.SendToBack();
            btnView.SendToBack();
            lblActive.Visible = false;
            lblAll.Visible = true;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void btnCurrent_Click(object sender, EventArgs e)
        {
            dgAccounts1.SendToBack();
            btnCurrent.SendToBack();
            lblActive.Visible = true;
            lblAll.Visible = false;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblAccounts WHERE archive = 'NO' ", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void dgAccounts1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string accountType = dgAccounts1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string idnum = dgAccounts1.Rows[e.RowIndex].Cells[2].Value.ToString();

            frmAccount f1 = new frmAccount();

            conn.Open();
            string sql = "SELECT * FROM tblAccounts WHERE IDNumber = @idnum";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idnum", idnum);
            MySqlDataReader reader = cmd.ExecuteReader();
            string IDNumber = "";
            int lockStat = 0;
            while (reader.Read())
            {
                IDNumber = reader.GetString("IDNumber");
                lockStat = reader.GetInt16("lockStatus");
            }
            conn.Close();

            conn.Open();
            string sql1 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @IDNumber";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@IDNumber", IDNumber);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string lastName = "";
            string firstName = "";
            string middleName = "";
            string birthday = "";
            while (reader1.Read())
            {
                lastName = reader1.GetString("lastName");
                firstName = reader1.GetString("firstName");
                middleName = reader1.GetString("middleName");
                birthday = reader1.GetString("birthday");
            }
            conn.Close();

            f1.lblAccount.Text = accountType + "'s Account";
            f1.lblType.Text = accountType;
            f1.txtIDNumber.Text = IDNumber;
            f1.txtLastName.Text = lastName;
            f1.txtFirstName.Text = firstName;
            f1.txtMiddleName.Text = middleName;
            f1.dtBirthday.Text = birthday;
            f1.Show();
        }
        
        private void dgAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string account = dgAccounts.SelectedCells[2].Value.ToString();
            string id = dgAccounts.SelectedCells[8].Value.ToString();

            if (account != "ADMINISTRATOR")
            {
                if (e.ColumnIndex == dgAccounts.Columns["Unlock"].Index)
                {
                    if (MessageBox.Show("Are you sure you want to unlock " + account + "'s account?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        string sql = "UPDATE tblAccounts SET lockStatus = 0 WHERE IDNumber = @id";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                        MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                        string acctype = "ADMINISTRATOR";
                        string activityy = "SUCCESFULLY UNLOCKED " + account + "'s ACCOUNT.";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        MessageBox.Show(account + " successfully unlocked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                        {
                            Application.OpenForms[i].Hide();
                        }
                        close();
                    }
                }
                else if (e.ColumnIndex == dgAccounts.Columns["Archive"].Index)
                {
                    if (MessageBox.Show("Are you sure you want to Archive " + account + "'s account?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        conn.Open();
                        string sqll = "SELECT * FROM tblAccounts WHERE IDNumber = @id AND archive = 1";
                        MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                        cmdd.Parameters.AddWithValue("@id", id);
                        MySqlDataReader dr = cmdd.ExecuteReader();
                        int stat = 0;
                        while (dr.Read())
                        {
                            stat += 1;
                        }
                        if (stat == 0)
                        {
                            if (conn.State == ConnectionState.Open)
                                conn.Close();
                            conn.Open();
                            string sql = "UPDATE tblAccounts SET archive = 1 WHERE IDNumber = @id";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Activity Log
                            conn.Open();
                            string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                            MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                            string acctype = "ADMINISTRATOR";
                            string activityy = "SUCCESFULLY ARCHIVED " + account + "'s ACCOUNT.";

                            conStr.activityLog(acctype, fname, mname, lname, activityy);

                            MessageBox.Show(account + " successfully archived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                            {
                                Application.OpenForms[i].Hide();
                            }
                            close();
                        }
                        else
                            MessageBox.Show(account + " already archived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                if (e.ColumnIndex == dgAccounts.Columns["Unlock"].Index)
                    MessageBox.Show(account + "'s account cannot be unlock or lock!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else if (e.ColumnIndex == dgAccounts.Columns["Archive"].Index)
                    MessageBox.Show(account + "'s account cannot be archive!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void close()
        {
            frmMainAdmin f1 = new frmMainAdmin();

            conn.Open();
            string sqll = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
            MySqlCommand cmdd = new MySqlCommand(sqll, conn);
            string id = "";
            string username = "";
            string password = "";
            MySqlDataReader readerr = cmdd.ExecuteReader();
            while (readerr.Read())
            {
                id = readerr.GetString("IDNumber");
                username = readerr.GetString("username");
                password = readerr.GetString("password");
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
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lname = reader1.GetString("lastname");
                fname = reader1.GetString("firstname");
                mname = reader1.GetString("middlename");
                birthday = reader1.GetString("birthday");

                if (reader1["profilePicture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])(reader1["profilePicture"]);
                    f1.pctrProfile.Image = null;

                    MemoryStream ms = new MemoryStream(img);
                    f1.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            conn.Close();

            f1.lblNameOfAdmin.Text = lname + ", " + fname + " " + mname;
            f1.txtIDNumber.Text = id;
            f1.txtUsername.Text = username;
            f1.txtPassword.Text = password;
            f1.lblIDNumber.Text = id;
            f1.txtLastName.Text = lname;
            f1.txtFirstName.Text = fname;
            f1.txtMiddleName.Text = mname;
            f1.dtBirthday.Text = birthday;
            f1.ShowDialog();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmScheduling f1 = new frmScheduling();
            f1.Show();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            frmSchedulingEditing f1 = new frmSchedulingEditing();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlStatement = "SELECT a.IDNumber, a.name, a.schedule, a.time, b.IDNumber, b.position, b.schedule1 FROM tbldoctors_schedule a JOIN tbldoctors_schedule1 b ON a.IDNumber = @id AND b.IDNumber = @id";
            MySqlCommand cmd = new MySqlCommand(sqlStatement, conn);
            cmd.Parameters.AddWithValue("@id", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string name = "";
            string position = "";
            string days = "";
            string schedule1 = "";
            string time = "";
            while (reader.Read())
            {
                id = reader.GetString("IDNumber");
                name = reader.GetString("name");
                schedule1 = reader.GetString("schedule1");
                position = reader.GetString("position");
                days = reader.GetString("schedule");
                f1.lstDays.Items.Add(days);
                time = reader.GetString("time");
                f1.lstTime.Items.Add(time);
            }
            conn.Close();
            f1.lblIDNum.Text = id;
            f1.lblName.Text = name;
            f1.lblPosition.Text = position;
            f1.lblDaysAc.Text = schedule1;

            f1.Show();
        }

        public string number = "";

        private void dgSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            number = dgSchedule.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        public void getNurse()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlStatement = "SELECT patientNo AS 'Patients Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tblwaiting_patient_nurse";
            adapter = new MySqlDataAdapter(sqlStatement, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblwaiting_patient_nurse");
            dgNextInLine.DataSource = dS;
            dgNextInLine.DataMember = "tblwaiting_patient_nurse";
            conn.Close();
        }

        public void getDoc()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By' FROM tblNext_In_Line_Doctor";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblNext_In_Line_Doctor");
            dgNextInLine.DataSource = dS;
            dgNextInLine.DataMember = "tblNext_In_Line_Doctor";
            conn.Close();
        }

        public void getLab1()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory1";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblNext_In_Line_Laboratory1");
            dgNextInLine.DataSource = dS;
            dgNextInLine.DataMember = "tblNext_In_Line_Laboratory1";
            conn.Close();
        }

        public void getLab2()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory2";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblNext_In_Line_Laboratory2");
            dgNextInLine.DataSource = dS;
            dgNextInLine.DataMember = "tblNext_In_Line_Laboratory2";
            conn.Close();
        }

        private void cboAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccountType.Text == "Nurse")
            {
                label5.Visible = false;
                cboDoctor.Visible = false;
                label15.Visible = true;
                cboNurse.Visible = true;
                cboDoctor.SelectedItem = 0;

                getNurse();
                dgNextInLine.Columns[0].Width = 150;
                dgNextInLine.Columns[1].Width = 150;
                dgNextInLine.Columns[2].Width = 150;
                dgNextInLine.Columns[3].Width = 150;
                dgNextInLine.Columns[4].Width = 150;
                dgNextInLine.Columns[5].Width = 150;
            }

            else if (cboAccountType.Text == "Doctor")
            {
                label5.Visible = true;
                cboDoctor.Visible = true;
                label15.Visible = false;
                cboNurse.Visible = false;
                cboNurse.SelectedItem = 0;

                getDoc();
                dgNextInLine.Columns[0].Width = 150;
                dgNextInLine.Columns[1].Width = 150;
                dgNextInLine.Columns[2].Width = 150;
                dgNextInLine.Columns[3].Width = 150;
                dgNextInLine.Columns[4].Width = 150;
            }

            else if (cboAccountType.Text == "Laboratory 1")
            {
                label5.Visible = false;
                cboDoctor.Visible = false;
                label15.Visible = false;
                cboNurse.Visible = false;
                cboDoctor.SelectedItem = 0;
                cboNurse.SelectedItem = 0;

                getLab1();
                dgNextInLine.Columns[0].Width = 150;
                dgNextInLine.Columns[1].Width = 150;
                dgNextInLine.Columns[2].Width = 150;
                dgNextInLine.Columns[3].Width = 140;
                dgNextInLine.Columns[4].Width = 160;
            }

            else if (cboAccountType.Text == "Laboratory 2")
            {
                label5.Visible = false;
                cboDoctor.Visible = false;
                label15.Visible = false;
                cboNurse.Visible = false;
                cboDoctor.SelectedItem = 0;
                cboNurse.SelectedItem = 0;

                getLab2();
                dgNextInLine.Columns[0].Width = 150;
                dgNextInLine.Columns[1].Width = 150;
                dgNextInLine.Columns[2].Width = 150;
                dgNextInLine.Columns[3].Width = 140;
                dgNextInLine.Columns[4].Width = 160;
            }
        }

        private void cboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By' FROM tblNext_In_Line_Doctor WHERE sentTo = '" + cboDoctor.Text + "' ";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblNext_In_Line_Doctor");
            dgNextInLine.DataMember = "tblNext_In_Line_Doctor";
            dgNextInLine.DataSource = dS;
            conn.Close();

            dgNextInLine.Columns[0].Width = 150;
            dgNextInLine.Columns[1].Width = 150;
            dgNextInLine.Columns[2].Width = 150;
            dgNextInLine.Columns[3].Width = 150;
            dgNextInLine.Columns[4].Width = 150;
        }

        public string doctor = "";
        public string nurse = "";
        public string lab1 = "";
        public string lab2 = "";

        private void dgNextInLine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            doctor = dgNextInLine.Rows[e.RowIndex].Cells[0].Value.ToString();
            nurse = dgNextInLine.Rows[e.RowIndex].Cells[0].Value.ToString();
            lab1 = dgNextInLine.Rows[e.RowIndex].Cells[0].Value.ToString();
            lab2 = dgNextInLine.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove Patient from " + cboAccountType.Text + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (cboAccountType.Text == "Doctor")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblNext_In_Line_Doctor WHERE patientNo = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", doctor);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getDoc();
                }

                else if (cboAccountType.Text == "Nurse")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblwaiting_patient_nurse WHERE patientNo = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", nurse);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getNurse();
                }

                else if (cboAccountType.Text == "Laboratory 1")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblnext_in_line_laboratory1 WHERE patientNo = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", nurse);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getLab1();
                }

                else if (cboAccountType.Text == "Laboratory 2")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblnext_in_line_laboratory2 WHERE patientNo = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", nurse);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getLab2();
                }
            }
        }

        private void dgAccounts1_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {
            string account = dgAccounts1.SelectedCells[2].Value.ToString();
            string id = dgAccounts1.SelectedCells[6].Value.ToString();
            string archiveStat = dgAccounts1.SelectedCells[1].Value.ToString();

            if (e.ColumnIndex == dgAccounts1.Columns["Unarchive"].Index)
            {
                if (Convert.ToInt32(archiveStat) == 1)
                {
                    if (MessageBox.Show("Are you sure you want to unarchive " + account + "'s account?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        string sql = "UPDATE tblAccounts SET archive = 0 WHERE IDNumber = @id";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                        MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                        string acctype = "ADMINISTRATOR";
                        string activityy = "SUCCESFULLY UNARCHIVED " + account + "'s ACCOUNT.";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);
                        
                        MessageBox.Show(account + " successfully unarchived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                        {
                            Application.OpenForms[i].Hide();
                        }
                        close();
                    }
                }
                else
                    MessageBox.Show(account + "'s account is not archived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblStaff", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            if (cboTypeStaff.Text != "" && txtFirstName.Text != "" && txtMiddleInitial.Text != "" && txtLastName.Text != "")
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sqll = "SELECT * FROM tblStaff WHERE lastName = @lname AND firstName = @fname AND middleInitial = @minitial";
                    MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                    cmdd.Parameters.AddWithValue("@lname", txtLastName.Text);
                    cmdd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                    cmdd.Parameters.AddWithValue("@minitial", txtMiddleInitial.Text);
                    MySqlDataReader dr = cmdd.ExecuteReader();
                    int x = 0;
                    while (dr.Read())
                    {
                        x++;
                    }
                    conn.Close();

                    if (x == 0)
                    {
                        conn.Open();
                        string sql = "INSERT INTO tblStaff VALUES (@id, @staff, @fname, @minitial, @lname, @degree, archive)";
                        MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                        cmd1.Parameters.AddWithValue("@id", count = count + 1);
                        cmd1.Parameters.AddWithValue("@staff", cboTypeStaff.Text);
                        cmd1.Parameters.AddWithValue("@fname", txtFirstName.Text);
                        cmd1.Parameters.AddWithValue("@minitial", txtMiddleInitial.Text);
                        cmd1.Parameters.AddWithValue("@lname", txtLastName.Text);
                        cmd1.Parameters.AddWithValue("@degree", txtDegree.Text);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                        MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                        string acctype = "ADMINISTRATOR";
                        string activityy = "SUCCESSFULLY ADDED LABORATORY STAFF - " + cboTypeStaff.Text + " (" + txtFirstName.Text + " " + txtMiddleInitial.Text + " " + txtLastName.Text + ").";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        MessageBox.Show("Laboratory Staff succesfully added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getStaff();
                        
                        conn.Open();
                        MySqlCommand cmd4 = new MySqlCommand("SELECT COUNT(*) FROM tblstaff", conn);
                        Int32 count2 = Convert.ToInt32(cmd4.ExecuteScalar());
                        conn.Close();
                        lblTotalStaff.Text = count2.ToString() + " records.";
                    }
                    else
                    {
                        MessageBox.Show("Name '" + txtFirstName.Text + " " + txtMiddleInitial.Text + " " + txtLastName.Text + "' already a Laboratory Staff.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    cboTypeStaff.Text = "";
                    txtFirstName.Clear();
                    txtMiddleInitial.Clear();
                    txtLastName.Clear();
                    txtDegree.Clear();
                    cboTypeStaff.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Laboratory Staff already added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (txtFirstName.Text == "")
                {
                    MessageBox.Show("Empty field/s not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                }
                else if (txtMiddleInitial.Text == "")
                {
                    MessageBox.Show("Empty field/s not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMiddleInitial.Focus();
                }
                else if (txtLastName.Text == "")
                {
                    MessageBox.Show("Empty field/s not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                }
                else if (cboTypeStaff.Text == "")
                    MessageBox.Show("Select Type of Staff!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "UPDATE tblStaff SET staff = @staff, firstName = @fname, middleInitial = @minitial, lastname = @lname, degree = @degree WHERE staff = @staff";
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            cmd1.Parameters.AddWithValue("@staff", cboTypeStaff.Text);
            cmd1.Parameters.AddWithValue("@fname", txtFirstName.Text);
            cmd1.Parameters.AddWithValue("@minitial", txtMiddleInitial.Text);
            cmd1.Parameters.AddWithValue("@lname", txtLastName.Text);
            cmd1.Parameters.AddWithValue("@degree", txtDegree.Text);
            cmd1.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Laboratory Staff succesfully updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getStaff();
        }

        private void dgStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboTypeStaff.Text = dgStaff.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtLastName.Text = dgStaff.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtFirstName.Text = dgStaff.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMiddleInitial.Text = dgStaff.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDegree.Text = dgStaff.Rows[e.RowIndex].Cells[6].Value.ToString();
            btnEdit.Enabled = true;
            btnClear.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboTypeStaff.Text = "";
            txtFirstName.Clear();
            txtMiddleInitial.Clear();
            txtLastName.Clear();
            txtDegree.Clear();
            cboTypeStaff.Focus();
            btnEdit.Enabled = false;
            btnClear.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all Patient to " + cboAccountType.Text + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (cboAccountType.Text == "Doctor")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblNext_In_Line_Doctor";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getDoctor();
                }

                else if (cboAccountType.Text == "Nurse")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblwaiting_patient_nurse";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getNurse();
                }

                else if (cboAccountType.Text == "Laboratory 1")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblnext_in_line_laboratory1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getLab1();
                }

                else if (cboAccountType.Text == "Laboratory 2")
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "DELETE FROM tblnext_in_line_laboratory2";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getLab2();
                }
            }
        }

        private void dgSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgSchedule.SelectedRows.Count != 0)
                btnSchedule.Enabled = true;
            else
                btnSchedule.Enabled = false;
        }

        private void dgAccounts_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgAccounts1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgSchedule_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgStaff_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgNextInLine_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void tmrRefreshDGV_Tick(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT a.accountType AS 'Account Type', b.doctorPosition AS 'Doctor (Type)', a.logInStatus AS 'Log In Status', a.username AS 'Username', a.IDNumber AS 'ID Number', b.lastName AS 'Last Name', b.firstName AS 'First Name', b.middleName AS 'Middle Name', b.birthday AS 'Birthday' FROM tblAccounts a JOIN tblAccount_Profile b ON a.IDNumber = b.IDNumber WHERE a.archive = 'NO' ";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblAccounts, tblAccount_Profile");
            dgAccounts.DataMember = "tblAccounts, tblAccount_Profile";
            dgAccounts.DataSource = dS;
            conn.Close();

            DataGridViewButtonColumn unlockButtonColumn = new DataGridViewButtonColumn();
            unlockButtonColumn.Name = "Unlock";
            unlockButtonColumn.Text = "Unlock";
            unlockButtonColumn.UseColumnTextForButtonValue = true;

            if (dgAccounts.Columns["Unlock"] == null)
            {
                dgAccounts.Columns.Insert(9, unlockButtonColumn);
            }
        }

        private void cboNurse_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string sqlStatement = "SELECT patientNo AS 'Patients Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tblwaiting_patient_nurse WHERE sentTo = '" + cboNurse.Text + "' ";
            adapter = new MySqlDataAdapter(sqlStatement, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblwaiting_patient_nurse");
            dgNextInLine.DataSource = dS;
            dgNextInLine.DataMember = "tblwaiting_patient_nurse";
            conn.Close();
        }

        private void dgStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgStaff.SelectedCells[1].Value.ToString();
            string staff = dgStaff.SelectedCells[2].Value.ToString();
            string name = dgStaff.SelectedCells[3].Value.ToString() + " " + dgStaff.SelectedCells[4].Value.ToString() + " " + dgStaff.SelectedCells[5].Value.ToString() + " " + dgStaff.SelectedCells[6].Value.ToString();

            if (e.ColumnIndex == dgStaff.Columns["Archive"].Index)
            {
                if (MessageBox.Show("Are you sure you want to archive " + staff + "-" + name + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sqll = "SELECT * FROM tblStaff WHERE id = @id AND archive = 1";
                    MySqlCommand cmdd = new MySqlCommand(sqll, conn);
                    cmdd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = cmdd.ExecuteReader();
                    int stat = 0;
                    while (dr.Read())
                    {
                        stat += 1;
                    }
                    if (stat == 0)
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        conn.Open();
                        string sql = "UPDATE tblStaff SET archive = 1 WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        //Activity Log
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                        MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                        string acctype = "ADMINISTRATOR";
                        string activityy = "SUCCESFULLY ARCHIVED " + staff + "-" + name + ".";

                        conStr.activityLog(acctype, fname, mname, lname, activityy);

                        MessageBox.Show(staff + " - " + name + " successfully archived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                        {
                            Application.OpenForms[i].Hide();
                        }
                        close();
                    }
                    else
                        MessageBox.Show(staff + " - " + name + " already archived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgStaff1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgStaff1.SelectedCells[1].Value.ToString();
            string staff = dgStaff1.SelectedCells[2].Value.ToString();
            string name = dgStaff1.SelectedCells[3].Value.ToString() + " " + dgStaff1.SelectedCells[4].Value.ToString() + " " + dgStaff1.SelectedCells[5].Value.ToString() + " " + dgStaff1.SelectedCells[6].Value.ToString();

            if (e.ColumnIndex == dgStaff1.Columns["Unarchive"].Index)
            {
                if (MessageBox.Show("Are you sure you want to unarchive " + staff + "-" + name + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "UPDATE tblStaff SET archive = 0 WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql1 = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
                    MySqlCommand cmd2 = new MySqlCommand(sql1, conn);
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

                    string acctype = "ADMINISTRATOR";
                    string activityy = "SUCCESFULLY UNARCHIVED " + staff + "-" + name + ".";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show(staff + " - " + name + " successfully unarchived.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        Application.OpenForms[i].Hide();
                    }
                    close();
                }
            }
        }

        private void txtCurrentStaff_Click(object sender, EventArgs e)
        {
            txtCurrentStaff.SendToBack();
            dgStaff1.SendToBack();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblstaff WHERE archive = 0", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalStaff.Text = count.ToString() + " records.";
        }

        private void txtViewAllStaff_Click(object sender, EventArgs e)
        {
            txtViewAllStaff.SendToBack();
            dgStaff.SendToBack();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblstaff WHERE archive = 1", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalStaff.Text = count.ToString() + " records.";
        }
    }
}
