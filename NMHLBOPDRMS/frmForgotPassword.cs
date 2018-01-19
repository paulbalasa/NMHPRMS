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

namespace NMHLBOPDRMS
{
    public partial class frmForgotPassword : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmForgotPassword()
        {
            InitializeComponent();
        }

        public void submit()
        {
            if (bunifuTxtPassword.Text != "" && bunifuTxtRetypePass.Text != "")
            {
                if (bunifuTxtRetypePass.Text == bunifuTxtPassword.Text)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "UPDATE tblAccounts SET password = @pass WHERE username = @user";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", lblUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", bunifuTxtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    conn.Open();
                    string sql2 = "SELECT * FROM tblAccount_Profile WHERE IDNumber = @id";
                    MySqlCommand cmd3 = new MySqlCommand(sql2, conn);
                    cmd3.Parameters.AddWithValue("@id", lblUsername.Text);
                    string lname = "";
                    string fname = "";
                    string mname = "";
                    string type = "";
                    MySqlDataReader reader1 = cmd3.ExecuteReader();
                    while (reader1.Read())
                    {
                        lname = reader1.GetString("lastName");
                        fname = reader1.GetString("firstName");
                        mname = reader1.GetString("middleName");
                        type = reader1.GetString("accountType");
                    }
                    conn.Close();

                    string acctype = type;
                    string activityy = "SUCCESSFULLY UPDATED/CHANGED PASSWORD.";

                    conStr.activityLog(acctype, fname, mname, lname, activityy);

                    MessageBox.Show("Password succesfully changed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    frmLogin f1 = new frmLogin();
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Password not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bunifuTxtPassword.SelectionStart = 0;
                    bunifuTxtPassword.SelectionLength = bunifuTxtPassword.TextLength;
                    bunifuTxtPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("Empty field/s not accepted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (bunifuTxtPassword.Text == "")
                    bunifuTxtPassword.Focus();
                else if (bunifuTxtRetypePass.Text == "")
                    bunifuTxtRetypePass.Focus();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void bunifuTxtRetypePass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void bunifuTxtRetypePass_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void frmForgotPassword_Load(object sender, EventArgs e)
        {
            dtPicker.Text = DateTime.Now.ToShortDateString();
        }
    }
}
