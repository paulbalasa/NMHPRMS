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
    public partial class frmBackupRestore : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmBackupRestore()
        {
            InitializeComponent();

            backDB_Worker.DoWork += backDB_Worker_DoWork;
            backDB_Worker.RunWorkerCompleted += backDB_Worker_RunWorkerCompleted;
            backDB_Worker.WorkerReportsProgress = true;
            backDB_Worker.WorkerSupportsCancellation = true;

            restoreDB_Worker.DoWork += new DoWorkEventHandler(restoreDB_Worker_DoWork);
            restoreDB_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(restoreDB_Worker_RunWorkerCompleted);
            restoreDB_Worker.WorkerReportsProgress = true;
            restoreDB_Worker.WorkerSupportsCancellation = true;

            n.Text = "Norzagaray Municipal Hospital";
            n.Visible = true;
            n.BalloonTipClicked += new EventHandler(n_BalloonTipClicked);
        }

        public void close()
        {
            frmMainAdmin ma = new frmMainAdmin();

            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sql = "SELECT * FROM tblAccounts WHERE accountType = 'ADMINISTRATOR' ";
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
                    ma.pctrProfile.Image = null;

                    MemoryStream ms = new MemoryStream(img);
                    ma.pctrProfile.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            conn.Close();

            this.Hide();
            
            ma.lblNameOfAdmin.Text = lname + ", " + fname + " " + mname;
            ma.txtIDNumber.Text = id;
            ma.txtUsername.Text = username;
            ma.txtPassword.Text = password;
            ma.lblIDNumber.Text = id;
            ma.txtLastName.Text = lname;
            ma.txtFirstName.Text = fname;
            ma.txtMiddleName.Text = mname;
            ma.dtBirthday.Text = birthday;
            ma.Show();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void n_BalloonTipClicked(object sender, EventArgs e)
        {
            string _filename = sad.FileName;
            int backslash = 0;
            for (int x = 0; x < _filename.Length; x++)
            {
                if (_filename[x] == '\\')
                    backslash++;
            }
            string _filetoStart = string.Empty;
            int counter = 0;
            for (int x = 0; x < _filename.Length; x++)
            {
                if (_filename[x] == '\\')
                {
                    counter++;
                }

                if (counter < backslash)
                {
                    _filetoStart += _filename[x];
                }
            }

            System.Diagnostics.Process.Start(_filetoStart);
        }

        private void backDB_Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.UseWaitCursor = false;
            if (e.Cancelled == true) { n.ShowBalloonTip(5000, "Backup Database", "Database failed to back up!", ToolTipIcon.Info); }
            else
            {
                n.ShowBalloonTip(5000, "Backup Database", "Database successfully backed up!\nClick to Open the folder", ToolTipIcon.Info);
            }
        }

        private void backDB_Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string constring = "server = localhost; username = root ; password = 1234; database = dbHospital; port = 3306; charset = utf8";
            string filename = sad.FileName.ToString() + "";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(filename);
                        conn.Close();
                    }
                }
            }
        }

        void restoreDB_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UseWaitCursor = false;
            if (e.Cancelled == true)
            {
                n.ShowBalloonTip(5000, "Restore Database", "Database failed to restor!", ToolTipIcon.Info);
            }
            else
            {
                n.ShowBalloonTip(5000, "Restore Database", "Database successfully restored!", ToolTipIcon.Info);
            }
        }

        void restoreDB_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            string constring = "server = localhost; username = root ; password = 1234; port = 3306; charset = utf8";
            string file = opf.FileName.ToString();
            using (conn = new MySqlConnection(constring))
            {
                using (cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportInfo.TargetDatabase = "dbHospital";
                        mb.ImportInfo.DatabaseDefaultCharSet = "utf8";
                        mb.ImportFromFile(file);
                        conn.Close();
                    }
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            sad.Title = "Backup Database";
            sad.Filter = "SQL Dump File (*.sql)|*.sql";
            sad.FileName = "Database Backup " + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "";
            if (sad.ShowDialog() == DialogResult.OK)
            {
                this.UseWaitCursor = true;
                n.ShowBalloonTip(100, "Backup Database", "Please wait while the Database is being backed up", ToolTipIcon.Info);
                backDB_Worker.RunWorkerAsync();
            }

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
            string activityy = "SUCCESSFULLY BACKED-UP DATABASE.";

            conStr.activityLog(acctype, fname, mname, lname, activityy);

            MessageBox.Show("Database backup completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            close();
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                opf.FileName = "";
                opf.Filter = "SQL Dump File (*.sql)|*.sql";
                opf.Title = "Restore Database";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    this.UseWaitCursor = true;
                    n.ShowBalloonTip(5000, "Restore Database", "Please wait while the Database is being restored", ToolTipIcon.Info);
                    restoreDB_Worker.RunWorkerAsync();
                }

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
                string activityy = "SUCCESSFULLY RESTORED-UP DATABASE.";

                conStr.activityLog(acctype, fname, mname, lname, activityy);

                MessageBox.Show("Database restore completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }
    }
}
