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
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace NMHLBOPDRMS
{
    public partial class frmConfirmation : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        static  MySqlConnection cn = new MySqlConnection(MySQL_Code.connectionString);

        MailMessage mail = new MailMessage();
        SmtpClient sc = new SmtpClient();

        static MySqlCommand cm = new MySqlCommand();
        static MySqlDataReader dare = null;

        public frmConfirmation()
        {
            InitializeComponent();
        }

        public static string _Eemailadd()
        {
            string email = string.Empty;
            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                cm = new MySqlCommand("SELECT emailadd FROM tblemail", cn);
                dare = cm.ExecuteReader();
                while (dare.Read()) { email = dare["emailadd"].ToString(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { cn.Close(); }
            return email;
        }

        public static string _Epassword()
        {
            string password = string.Empty;
            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                cm = new MySqlCommand("SELECT password FROM tblemail", cn);
                dare = cm.ExecuteReader();
                while (dare.Read()) { password = dare["password"].ToString(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { cn.Close(); }
            return password;
        }

        public static string _Ehost()
        {
            string host = string.Empty;
            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                cm = new MySqlCommand("SELECT host FROM tblemail", cn);
                dare = cm.ExecuteReader();
                while (dare.Read()) { host = dare["host"].ToString(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { cn.Close(); }
            return host;
        }

        public static string _Eport()
        {
            string port = string.Empty;
            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                cm = new MySqlCommand("SELECT port FROM tblemail", cn);
                dare = cm.ExecuteReader();
                while (dare.Read()) { port = dare["port"].ToString(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { cn.Close(); }
            return port;
        }

        string password = string.Empty;

        void createAdmin_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            password = _getPassword();
            try
            {
                mail.From = new MailAddress(_Eemailadd());
                mail.To.Add(new MailAddress(txtEmail.Text));
                mail.Subject = "Getting Account Information - Norzagaray Municipal Hospital";
                mail.IsBodyHtml = true;
                mail.Body = "Your new password is " + password;
                sc.Host = _Ehost();
                sc.Port = Convert.ToInt32(_Eport());
                sc.Credentials = new
                System.Net.NetworkCredential(_Eemailadd(), _Epassword());
                sc.EnableSsl = true;
                sc.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                getPassword_Worker.CancelAsync();
                e.Cancel = true;
            }
        }

        public string _getPassword()
        {
            Random r = new Random();
            string password = string.Empty;
            int a = r.Next(65, 91); char _a = (char)a;
            int b = r.Next(65, 91); char _b = (char)b;
            int c = r.Next(65, 91); char _c = (char)c;
            int d = r.Next(65, 91); char _d = (char)d;
            int e = r.Next(65, 91); char _e = (char)e;
            int f = r.Next(65, 91); char _f = (char)f;
            int g = r.Next(65, 91); char _g = (char)g;
            int h = r.Next(65, 91); char _h = (char)h;
            int i = r.Next(65, 91); char _i = (char)i;
            int j = r.Next(65, 91); char _j = (char)j;
            password = _a.ToString() + _b.ToString() + _c.ToString() + _d.ToString() + _e.ToString() + _f.ToString() + _g.ToString() + _h.ToString() + _i.ToString() + _j.ToString();
            return password;
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            string sql = "SELECT * FROM tblAccount_Profile WHERE IDNumber = '" + txtIDNumber.Text + "'";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataReader rd = cmd.ExecuteReader();
            string email = "";
            while (rd.Read())
            {
                email = rd.GetString("email");
            }
            cn.Close();
            txtEmail.Text = email;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (isValidEmail(txtEmail.Text))
            {
                vldnvld.Text = "(Valid Email Address)";
                vldnvld.ForeColor = Color.Blue;
            }
            else
            {
                vldnvld.Text = "(Invalid Email Address)";
                vldnvld.ForeColor = Color.Red;
            }
        }

        private void frmConfirmation_Load(object sender, EventArgs e)
        {
            getPassword_Worker.DoWork += new DoWorkEventHandler(createAdmin_Worker_DoWork);
            getPassword_Worker.WorkerReportsProgress = true;
            getPassword_Worker.WorkerSupportsCancellation = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            password = _getPassword();
            if (txtEmail.Text.Trim() != "")
            {
                if (isValidEmail(txtEmail.Text))
                {
                    if (MessageBox.Show("Are you sure you want to save this record for Admin account?", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (!getPassword_Worker.IsBusy)
                        {
                            getPassword_Worker.WorkerSupportsCancellation = true;
                            getPassword_Worker.RunWorkerAsync();
                        }
                        MessageBox.Show("Successfully Sent! Check your email account to see your new password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cn.Open();
                        string sql = "UPDATE tblAccounts SET password = @password WHERE IDNumber = '" + txtIDNumber.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Email Address!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEmail.Focus();
                }
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                frmLogin f1 = new frmLogin();
                f1.Show();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void lblClickHere_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmQuestionsConfirmation f1 = new frmQuestionsConfirmation();
            f1.Show();
        }
    }
}
