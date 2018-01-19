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
    public partial class usrCntrlLaboratoryResults2 : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public usrCntrlLaboratoryResults2()
        {
            InitializeComponent();
        }

        private void usrCntrlLaboratoryResults2_Load(object sender, EventArgs e)
        {
            getDataset();
            dgLaboratoryResults.Columns[0].Width = 125;
            dgLaboratoryResults.Columns[1].Width = 300;
            dgLaboratoryResults.Columns[2].Width = 100;
            dgLaboratoryResults.Columns[3].Width = 100;
            dgLaboratoryResults.Columns[3].Width = 100;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblLaboratory2_Results", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        public void getDataset()
        {
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient No.', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', status AS 'Status', daterequested AS 'Date Requested', dateexamined AS 'Date Examined' FROM tblLaboratory2_Results";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tblLaboratory2_Results");
            dgLaboratoryResults.DataMember = "tblLaboratory2_Results";
            dgLaboratoryResults.DataSource = dS;
            conn.Close();
        }

        private void dgLaboratoryResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string number = dgLaboratoryResults.Rows[e.RowIndex].Cells[0].Value.ToString();
            conn.Open();
            string sql = "SELECT * FROM tblLaboratory2_Results WHERE patientNo = @num";
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

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }

            frmLaboratory2 f1 = new frmLaboratory2();
            conn.Open();
            string sql1 = "SELECT * FROM tblLab2_Exam WHERE patientNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            string labExam = "";
            while (reader1.Read())
            {
                labExam = reader1.GetString("labExam");
                f1.cboType.Items.Add(labExam);
            }
            conn.Close();

            f1.lblStatus.Text = status;
            f1.btnSendTo.Visible = false;
            f1.btnSubmitUltrasound.Visible = false;
            f1.btnSubmitXray.Visible = false;
            f1.lblNumberLab.Text = number;
            f1.lblNameLab.Text = lname + ", " + fname + " " + mname;
            f1.dtRequestedLab.Text = daterequested;
            f1.dtExaminedLab.Text = dateexamined;
            
            //Ultrasound
            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Ultrasound WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            string typeofexaminations = "";
            string impression = "";
            string result = "";
            while (reader2.Read())
            {
                typeofexaminations = reader2.GetString("typeofexaminations");
                impression = reader2.GetString("impression");
                result = reader2.GetString("result");
            }
            conn.Close();

            f1.cboType.Text = typeofexaminations;
            f1.txtImpressionsUltrasound.Text = impression;
            f1.txtResultUltrasound.Text = result;

            //Xray
            conn.Open();
            string sql3 = "SELECT * FROM tblLaboratory_Xray WHERE patientsNo = @num";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            string typeofexaminationsX = "";
            string impressionX = "";
            string resultX = "";
            while (reader3.Read())
            {
                typeofexaminationsX = reader3.GetString("typeofexaminations");
                impressionX = reader3.GetString("impression");
                resultX = reader3.GetString("result");
            }
            conn.Close();

            f1.cboTypeXray.Text = typeofexaminationsX;
            f1.txtImpressionsXray.Text = impressionX;
            f1.txtResultsXray.Text = resultX;

            f1.Show();
        }
    }
}
