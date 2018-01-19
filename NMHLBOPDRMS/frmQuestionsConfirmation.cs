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
    public partial class frmQuestionsConfirmation : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        static MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);

        public frmQuestionsConfirmation()
        {
            InitializeComponent();
        }
        
        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void getQuestion()
        {
            conn.Open();
            string sql = "SELECT * FROM tblQuestions WHERE IDNumber = '" + txtIDNumber.Text + "' AND qNum = 2 ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            int qnum = 0;
            string question = "";
            while (dr.Read())
            {
                qnum = dr.GetInt32("qNum");
                question = dr.GetString("question");
            }
            conn.Close();

            lblqNum.Text = qnum.ToString();
            lblQuestion.Text = question;
        }

        public int count = 1;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtAnswer.Text != "")
            {
                conn.Open();
                string sql = "SELECT * FROM tblQuestions WHERE IDNumber = '" + txtIDNumber.Text + "' AND question = '" + lblQuestion.Text + "' AND answer = '" + txtAnswer.Text + "' ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                int x = 0;
                while (dr.Read())
                {
                    x++;
                }
                conn.Close();

                if (x == 1)
                {
                    MessageBox.Show("You're answer is correct!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (count != 2)
                    {
                        count++;
                        txtAnswer.Clear();
                        getQuestion();
                    }
                    else
                    {
                        MessageBox.Show("Account succesfully verified!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        frmForgotPassword f1 = new frmForgotPassword();
                        conn.Open();
                        string sql1 = "SELECT * FROM tblAccounts WHERE IDNumber = '" + txtIDNumber.Text + "' ";
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        string username = "";
                        while (dr1.Read())
                        {
                            username = dr1.GetString("username");
                        }
                        conn.Close();
                        f1.lblUsername.Text = username;
                        f1.Show();
                    }
                }
                else
                {
                    lblChecker.Visible = true;
                    lblChecker.Text = "Answer is wrong!";
                }
            }
            else
            {
                MessageBox.Show("Empty field/s not accepted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnswer.Focus();
            }
        }

        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            lblChecker.Text = "";
        }

        private void frmQuestionsConfirmation_Load(object sender, EventArgs e)
        {
            
        }

        private void txtIDNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();
                string sql = "SELECT * FROM tblQuestions WHERE IDNumber = '" + txtIDNumber.Text + "' AND qnum = 1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                int qnum = 0;
                string question = "";
                while (dr.Read())
                {
                    qnum = dr.GetInt32("qNum");
                    question = dr.GetString("question");
                }
                conn.Close();
                lblqNum.Text = qnum.ToString();
                lblQuestion.Text = question;
            }
        }
    }
}
