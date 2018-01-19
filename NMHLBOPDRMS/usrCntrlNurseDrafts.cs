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
    public partial class usrCntrlNurseDrafts : UserControl
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public usrCntrlNurseDrafts()
        {
            InitializeComponent();
        }

        private void usrCntrlNurseDrafts_Load(object sender, EventArgs e)
        {
            getdata();

            string name = "";
            getdata();
            conn.Open();
            string sql = "SELECT * FROM tblaccount_profile WHERE accountType = 'DOCTOR'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string fname = "";
            string mname = "";
            string lname = "";
            string position = "";
            while (reader.Read())
            {
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                lname = reader.GetString("lastName");
                position = reader.GetString("doctorPosition");
                name = "Dr. " + fname + " " + mname + " " + lname;
                cboDoctorsName.Items.Add(name + " (" + position + ")");
            }
            conn.Close();

            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblDrafts_Nurse", conn);
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void getdata()
        {
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tblDrafts_Nurse ";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tbldrafts_nurse");
            dgDrafts.DataMember = "tbldrafts_nurse";
            dgDrafts.DataSource = dS;
            conn.Close();

            dgDrafts.Columns[0].Width = 150;
            dgDrafts.Columns[1].Width = 250;
            dgDrafts.Columns[2].Width = 250;
            dgDrafts.Columns[3].Width = 250;
            dgDrafts.Columns[4].Width = 150;
            dgDrafts.Columns[5].Width = 300;
        }

        private void cboDoctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dS.Clear();

            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tbldrafts_nurse WHERE sentTo = '" + cboDoctorsName.Text + "' ";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tbldrafts_nurse");
            dgDrafts.DataMember = "tbldrafts_nurse";
            dgDrafts.DataSource = dS;
            conn.Close();

            dgDrafts.Columns[0].Width = 150;
            dgDrafts.Columns[1].Width = 250;
            dgDrafts.Columns[2].Width = 250;
            dgDrafts.Columns[3].Width = 250;
            dgDrafts.Columns[4].Width = 150;
            dgDrafts.Columns[5].Width = 300;
        }

        public string number = "";
        public string lname = "";
        public string fname = "";
        public string mname = "";

        private void dgDrafts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            number = dgDrafts.Rows[e.RowIndex].Cells[0].Value.ToString();
            lname = dgDrafts.Rows[e.RowIndex].Cells[0].Value.ToString();
            fname = dgDrafts.Rows[e.RowIndex].Cells[0].Value.ToString();
            mname = dgDrafts.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnSendTo_Click(object sender, EventArgs e)
        {
            frmSendToDrafts f1 = new frmSendToDrafts();
            f1.number = number;
            f1.ln = lname;
            f1.fn = fname;
            f1.mn = mname;
            f1.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "DELETE * FROM tblDrafts_Nurse WHERE patientsNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", number);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Patient succesfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refresh();
        }

        private void tmrRefreshDGV_Tick(object sender, EventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            dS.Clear();

            conn.Open();
            string sql = "SELECT patientNo AS 'Patient Number', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', sentBy AS 'Sent By', sentTo AS 'Sent To' FROM tblDrafts_Nurse ";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tbldrafts_nurse");
            dgDrafts.DataMember = "tbldrafts_nurse";
            dgDrafts.DataSource = dS;
            conn.Close();

            dgDrafts.Columns[0].Width = 150;
            dgDrafts.Columns[1].Width = 250;
            dgDrafts.Columns[2].Width = 250;
            dgDrafts.Columns[3].Width = 250;
            dgDrafts.Columns[4].Width = 150;
            dgDrafts.Columns[5].Width = 300;

            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM tblDrafts_Nurse", conn);
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = count.ToString() + " records.";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all Patient in Drafts?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                string sql = "DELETE * FROM tblDrafts_Nurse";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@num", number);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Patient succesfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refresh();
            }
        }

        private void dgDrafts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDrafts.SelectedCells.Count >= 1)
                btnClear.Enabled = true;
            else
                btnClear.Enabled = false;
        }
    }
}
