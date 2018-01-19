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
    public partial class frmMainLaboratory1 : Form
    {
        MySQL_Code conStr = new MySQL_Code();
        public MySqlConnection conn = new MySqlConnection(MySQL_Code.connectionString);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet dS = new DataSet();

        public frmMainLaboratory1()
        {
            InitializeComponent();
        }

        private void pnlSlctnLogout_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string sqlll = "UPDATE tblAccounts SET logInStatus = 'INACTIVE' WHERE IDNumber = @id";
            MySqlCommand cmddd = new MySqlCommand(sqlll, conn);
            cmddd.Parameters.AddWithValue("@id", lblIDNumber.Text);
            cmddd.ExecuteNonQuery();
            conn.Close();

            //Activity Log
            string accType = "LABORATORY 1";
            string activity = "SUCCESSFULLY LOGGED OUT.";

            conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Hide();
            }
            frmLogin f1 = new frmLogin();
            f1.Show();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            MetroSuite.MetroSeparator.MainColorScheme mainColorScheme6 = new MetroSuite.MetroSeparator.MainColorScheme();
            MetroSuite.MetroPanelSelection.MainColorScheme mainColorScheme5 = new MetroSuite.MetroPanelSelection.MainColorScheme();
            MetroSuite.MetroPanelSelection.MainColorScheme mainColorScheme4 = new MetroSuite.MetroPanelSelection.MainColorScheme();
            MetroSuite.MetroPanelSelection.MainColorScheme mainColorScheme3 = new MetroSuite.MetroPanelSelection.MainColorScheme();
            MetroSuite.MetroPanelSelection.MainColorScheme mainColorScheme2 = new MetroSuite.MetroPanelSelection.MainColorScheme();
            MetroSuite.MetroPanelSelection.MainColorScheme mainColorScheme1 = new MetroSuite.MetroPanelSelection.MainColorScheme();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainLaboratory1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIDNumber = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.metroSeparator2 = new MetroSuite.MetroSeparator();
            this.pnlSlctnHome = new MetroSuite.MetroPanelSelection();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pnlSlctnProfile = new MetroSuite.MetroPanelSelection();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.pnlSlctnLogout = new MetroSuite.MetroPanelSelection();
            this.pictureBox19 = new System.Windows.Forms.PictureBox();
            this.pnlSlctnNextInLine = new MetroSuite.MetroPanelSelection();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlSlctnLaboratoryResults = new MetroSuite.MetroPanelSelection();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.dtBirthday = new System.Windows.Forms.DateTimePicker();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIDNumber = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnChange = new MetroFramework.Controls.MetroButton();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtRetypePassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pctrProfile = new System.Windows.Forms.PictureBox();
            this.btnUpdateAccountProfile = new MetroFramework.Controls.MetroButton();
            this.btnUpdateAccountSettings = new MetroFramework.Controls.MetroButton();
            this.btnBrowse = new MetroFramework.Controls.MetroButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.usrCntrlProfileLaboratory11 = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl3 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl4 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl5 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.usrCntrlNextInLineLaboratory1 = new System.Windows.Forms.Panel();
            this.dgNextInLineLaboratory1 = new System.Windows.Forms.DataGridView();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.tmrRefreshDGV = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnlHome = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblNameOfLaboratory = new System.Windows.Forms.Label();
            this.pictureBox21 = new System.Windows.Forms.PictureBox();
            this.usrCntrlLaboratoryResults1 = new NMHLBOPDRMS.usrCntrlLaboratoryResults2();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.pnlSlctnHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.pnlSlctnProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            this.pnlSlctnLogout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).BeginInit();
            this.pnlSlctnNextInLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnlSlctnLaboratoryResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctrProfile)).BeginInit();
            this.usrCntrlProfileLaboratory11.SuspendLayout();
            this.usrCntrlNextInLineLaboratory1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNextInLineLaboratory1)).BeginInit();
            this.pnlHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.lblIDNumber);
            this.panel1.Controls.Add(this.pictureBox8);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblModuleName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Location = new System.Drawing.Point(188, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 83);
            this.panel1.TabIndex = 86;
            // 
            // lblIDNumber
            // 
            this.lblIDNumber.AutoSize = true;
            this.lblIDNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.lblIDNumber.Location = new System.Drawing.Point(6, 66);
            this.lblIDNumber.Name = "lblIDNumber";
            this.lblIDNumber.Size = new System.Drawing.Size(65, 13);
            this.lblIDNumber.TabIndex = 102;
            this.lblIDNumber.Text = "lblIDNumber";
            this.lblIDNumber.Visible = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Image = global::NMHLBOPDRMS.Properties.Resources.logofinal;
            this.pictureBox8.Location = new System.Drawing.Point(640, -4);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(95, 94);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 101;
            this.pictureBox8.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(197)))), ((int)(((byte)(235)))));
            this.label1.Location = new System.Drawing.Point(465, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 21);
            this.label1.TabIndex = 100;
            this.label1.Text = "Norzagaray, Bulacan";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(197)))), ((int)(((byte)(235)))));
            this.label10.Location = new System.Drawing.Point(304, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(342, 32);
            this.label10.TabIndex = 99;
            this.label10.Text = "Norzagaray Municipal Hospital";
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.lblModuleName.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.lblModuleName.Location = new System.Drawing.Point(14, 14);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(95, 33);
            this.lblModuleName.TabIndex = 84;
            this.lblModuleName.Text = "Home";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // metroSeparator2
            // 
            mainColorScheme6.Color1 = System.Drawing.Color.White;
            mainColorScheme6.Color2 = System.Drawing.Color.White;
            this.metroSeparator2.ColorScheme = mainColorScheme6;
            this.metroSeparator2.Location = new System.Drawing.Point(6, 254);
            this.metroSeparator2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroSeparator2.Name = "metroSeparator2";
            this.metroSeparator2.Size = new System.Drawing.Size(176, 2);
            this.metroSeparator2.TabIndex = 81;
            // 
            // pnlSlctnHome
            // 
            this.pnlSlctnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(145)))));
            this.pnlSlctnHome.Checked = true;
            mainColorScheme5.BackgroundColorChecked = System.Drawing.SystemColors.Control;
            mainColorScheme5.BackgroundColorHover = System.Drawing.Color.DeepSkyBlue;
            mainColorScheme5.BackgroundColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            mainColorScheme5.EffectBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme5.NormalBorderColor = System.Drawing.Color.Transparent;
            this.pnlSlctnHome.ColorScheme = mainColorScheme5;
            this.pnlSlctnHome.Controls.Add(this.pictureBox13);
            this.pnlSlctnHome.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnHome.ForeColor = System.Drawing.Color.Black;
            this.pnlSlctnHome.HeadlineFont = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnHome.Location = new System.Drawing.Point(-5, 83);
            this.pnlSlctnHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSlctnHome.Name = "pnlSlctnHome";
            this.pnlSlctnHome.Size = new System.Drawing.Size(222, 39);
            this.pnlSlctnHome.SubTextFont = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlSlctnHome.TabIndex = 1;
            this.pnlSlctnHome.TextHeadline = "            Home";
            this.pnlSlctnHome.TextSubline = "";
            this.pnlSlctnHome.Click += new System.EventHandler(this.pnlSlctnHome_Click_1);
            // 
            // pictureBox13
            // 
            this.pictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox13.Image = global::NMHLBOPDRMS.Properties.Resources.Home_64px;
            this.pictureBox13.Location = new System.Drawing.Point(17, 3);
            this.pictureBox13.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(22, 22);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox13.TabIndex = 77;
            this.pictureBox13.TabStop = false;
            // 
            // pnlSlctnProfile
            // 
            this.pnlSlctnProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(145)))));
            mainColorScheme4.BackgroundColorChecked = System.Drawing.SystemColors.Control;
            mainColorScheme4.BackgroundColorHover = System.Drawing.Color.DeepSkyBlue;
            mainColorScheme4.BackgroundColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            mainColorScheme4.EffectBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme4.NormalBorderColor = System.Drawing.Color.Transparent;
            this.pnlSlctnProfile.ColorScheme = mainColorScheme4;
            this.pnlSlctnProfile.Controls.Add(this.pictureBox20);
            this.pnlSlctnProfile.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnProfile.ForeColor = System.Drawing.Color.White;
            this.pnlSlctnProfile.HeadlineFont = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnProfile.Location = new System.Drawing.Point(-3, 282);
            this.pnlSlctnProfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSlctnProfile.Name = "pnlSlctnProfile";
            this.pnlSlctnProfile.Size = new System.Drawing.Size(222, 39);
            this.pnlSlctnProfile.SubTextFont = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlSlctnProfile.TabIndex = 80;
            this.pnlSlctnProfile.TextHeadline = "            Profile";
            this.pnlSlctnProfile.TextSubline = "";
            this.pnlSlctnProfile.Click += new System.EventHandler(this.pnlSlctnProfile_Click);
            // 
            // pictureBox20
            // 
            this.pictureBox20.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox20.Image = global::NMHLBOPDRMS.Properties.Resources.Circled_User_Male_64px1;
            this.pictureBox20.Location = new System.Drawing.Point(15, 0);
            this.pictureBox20.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(22, 22);
            this.pictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox20.TabIndex = 77;
            this.pictureBox20.TabStop = false;
            // 
            // pnlSlctnLogout
            // 
            this.pnlSlctnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(145)))));
            mainColorScheme3.BackgroundColorChecked = System.Drawing.SystemColors.Control;
            mainColorScheme3.BackgroundColorHover = System.Drawing.Color.DeepSkyBlue;
            mainColorScheme3.BackgroundColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            mainColorScheme3.EffectBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme3.NormalBorderColor = System.Drawing.Color.Transparent;
            this.pnlSlctnLogout.ColorScheme = mainColorScheme3;
            this.pnlSlctnLogout.Controls.Add(this.pictureBox19);
            this.pnlSlctnLogout.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnLogout.ForeColor = System.Drawing.Color.White;
            this.pnlSlctnLogout.HeadlineFont = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnLogout.Location = new System.Drawing.Point(-3, 326);
            this.pnlSlctnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSlctnLogout.Name = "pnlSlctnLogout";
            this.pnlSlctnLogout.Size = new System.Drawing.Size(222, 39);
            this.pnlSlctnLogout.SubTextFont = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlSlctnLogout.TabIndex = 83;
            this.pnlSlctnLogout.TextHeadline = "            Logout";
            this.pnlSlctnLogout.TextSubline = "";
            this.pnlSlctnLogout.Click += new System.EventHandler(this.pnlSlctnLogout_Click);
            // 
            // pictureBox19
            // 
            this.pictureBox19.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox19.Image = global::NMHLBOPDRMS.Properties.Resources.Logout_Rounded_Up_64px1;
            this.pictureBox19.Location = new System.Drawing.Point(15, 0);
            this.pictureBox19.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox19.Name = "pictureBox19";
            this.pictureBox19.Size = new System.Drawing.Size(22, 22);
            this.pictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox19.TabIndex = 77;
            this.pictureBox19.TabStop = false;
            // 
            // pnlSlctnNextInLine
            // 
            this.pnlSlctnNextInLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(145)))));
            mainColorScheme2.BackgroundColorChecked = System.Drawing.SystemColors.Control;
            mainColorScheme2.BackgroundColorHover = System.Drawing.Color.DeepSkyBlue;
            mainColorScheme2.BackgroundColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            mainColorScheme2.EffectBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme2.NormalBorderColor = System.Drawing.Color.Transparent;
            this.pnlSlctnNextInLine.ColorScheme = mainColorScheme2;
            this.pnlSlctnNextInLine.Controls.Add(this.pictureBox3);
            this.pnlSlctnNextInLine.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnNextInLine.ForeColor = System.Drawing.Color.White;
            this.pnlSlctnNextInLine.HeadlineFont = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnNextInLine.Location = new System.Drawing.Point(-5, 127);
            this.pnlSlctnNextInLine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSlctnNextInLine.Name = "pnlSlctnNextInLine";
            this.pnlSlctnNextInLine.Size = new System.Drawing.Size(222, 39);
            this.pnlSlctnNextInLine.SubTextFont = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlSlctnNextInLine.TabIndex = 79;
            this.pnlSlctnNextInLine.TextHeadline = "            Next-In Line Patient";
            this.pnlSlctnNextInLine.TextSubline = "";
            this.pnlSlctnNextInLine.Click += new System.EventHandler(this.pnlSlctnNextInLine_Click_1);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::NMHLBOPDRMS.Properties.Resources.Circled_Right_2_48px1;
            this.pictureBox3.Location = new System.Drawing.Point(17, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 22);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 77;
            this.pictureBox3.TabStop = false;
            // 
            // pnlSlctnLaboratoryResults
            // 
            this.pnlSlctnLaboratoryResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(145)))));
            mainColorScheme1.BackgroundColorChecked = System.Drawing.SystemColors.Control;
            mainColorScheme1.BackgroundColorHover = System.Drawing.Color.DeepSkyBlue;
            mainColorScheme1.BackgroundColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            mainColorScheme1.EffectBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(240)))));
            mainColorScheme1.NormalBorderColor = System.Drawing.Color.Transparent;
            this.pnlSlctnLaboratoryResults.ColorScheme = mainColorScheme1;
            this.pnlSlctnLaboratoryResults.Controls.Add(this.pictureBox4);
            this.pnlSlctnLaboratoryResults.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnLaboratoryResults.ForeColor = System.Drawing.Color.White;
            this.pnlSlctnLaboratoryResults.HeadlineFont = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlctnLaboratoryResults.Location = new System.Drawing.Point(3, 170);
            this.pnlSlctnLaboratoryResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSlctnLaboratoryResults.Name = "pnlSlctnLaboratoryResults";
            this.pnlSlctnLaboratoryResults.Size = new System.Drawing.Size(222, 39);
            this.pnlSlctnLaboratoryResults.SubTextFont = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlSlctnLaboratoryResults.TabIndex = 80;
            this.pnlSlctnLaboratoryResults.TextHeadline = "          Laboratory Results";
            this.pnlSlctnLaboratoryResults.TextSubline = "";
            this.pnlSlctnLaboratoryResults.Click += new System.EventHandler(this.pnlSlctnLaboratoryResults_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::NMHLBOPDRMS.Properties.Resources.Ratings_50px;
            this.pictureBox4.Location = new System.Drawing.Point(9, 2);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 77;
            this.pictureBox4.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel2.Controls.Add(this.dtPicker);
            this.panel2.Controls.Add(this.pnlSlctnLaboratoryResults);
            this.panel2.Controls.Add(this.pnlSlctnNextInLine);
            this.panel2.Controls.Add(this.pnlSlctnLogout);
            this.panel2.Controls.Add(this.pnlSlctnProfile);
            this.panel2.Controls.Add(this.pnlSlctnHome);
            this.panel2.Controls.Add(this.metroSeparator2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 654);
            this.panel2.TabIndex = 85;
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(6, 634);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(200, 20);
            this.dtPicker.TabIndex = 85;
            this.dtPicker.Value = new System.DateTime(2017, 8, 10, 0, 0, 0, 0);
            this.dtPicker.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtFirstName);
            this.panel6.Controls.Add(this.dtBirthday);
            this.panel6.Controls.Add(this.txtLastName);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.txtMiddleName);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.txtIDNumber);
            this.panel6.Location = new System.Drawing.Point(287, 44);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(425, 249);
            this.panel6.TabIndex = 168;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 133;
            this.label8.Text = "Last Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 130;
            this.label5.Text = "Middle Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(27, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 17);
            this.label9.TabIndex = 129;
            this.label9.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(166, 104);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(239, 20);
            this.txtFirstName.TabIndex = 121;
            // 
            // dtBirthday
            // 
            this.dtBirthday.Location = new System.Drawing.Point(166, 189);
            this.dtBirthday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtBirthday.Name = "dtBirthday";
            this.dtBirthday.Size = new System.Drawing.Size(239, 20);
            this.dtBirthday.TabIndex = 123;
            this.dtBirthday.Value = new System.DateTime(2017, 6, 6, 0, 0, 0, 0);
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(166, 64);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(239, 20);
            this.txtLastName.TabIndex = 120;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 194);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 17);
            this.label12.TabIndex = 127;
            this.label12.Text = "Birthday:";
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(166, 144);
            this.txtMiddleName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(239, 20);
            this.txtMiddleName.TabIndex = 122;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(27, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 17);
            this.label13.TabIndex = 119;
            this.label13.Text = "ID Number:";
            // 
            // txtIDNumber
            // 
            this.txtIDNumber.Enabled = false;
            this.txtIDNumber.Location = new System.Drawing.Point(166, 23);
            this.txtIDNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(239, 20);
            this.txtIDNumber.TabIndex = 118;
            this.txtIDNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnChange);
            this.panel3.Controls.Add(this.txtUsername);
            this.panel3.Controls.Add(this.txtRetypePassword);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtPassword);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(26, 365);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(686, 118);
            this.panel3.TabIndex = 170;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(417, 79);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnCancel.TabIndex = 171;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(417, 48);
            this.btnChange.Margin = new System.Windows.Forms.Padding(4);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(60, 21);
            this.btnChange.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnChange.TabIndex = 170;
            this.btnChange.Text = "CHANGE";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(159, 18);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(246, 20);
            this.txtUsername.TabIndex = 124;
            // 
            // txtRetypePassword
            // 
            this.txtRetypePassword.Enabled = false;
            this.txtRetypePassword.Location = new System.Drawing.Point(159, 80);
            this.txtRetypePassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRetypePassword.Name = "txtRetypePassword";
            this.txtRetypePassword.Size = new System.Drawing.Size(246, 20);
            this.txtRetypePassword.TabIndex = 126;
            this.txtRetypePassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 17);
            this.label7.TabIndex = 132;
            this.label7.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(159, 48);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(246, 20);
            this.txtPassword.TabIndex = 125;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 128;
            this.label4.Text = "Username:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 17);
            this.label6.TabIndex = 131;
            this.label6.Text = "Re-type Password:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.Controls.Add(this.pctrProfile);
            this.panel5.Location = new System.Drawing.Point(25, 44);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(245, 249);
            this.panel5.TabIndex = 169;
            // 
            // pctrProfile
            // 
            this.pctrProfile.BackColor = System.Drawing.Color.White;
            this.pctrProfile.Image = ((System.Drawing.Image)(resources.GetObject("pctrProfile.Image")));
            this.pctrProfile.Location = new System.Drawing.Point(3, 4);
            this.pctrProfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pctrProfile.Name = "pctrProfile";
            this.pctrProfile.Size = new System.Drawing.Size(239, 241);
            this.pctrProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctrProfile.TabIndex = 135;
            this.pctrProfile.TabStop = false;
            // 
            // btnUpdateAccountProfile
            // 
            this.btnUpdateAccountProfile.Location = new System.Drawing.Point(599, 298);
            this.btnUpdateAccountProfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateAccountProfile.Name = "btnUpdateAccountProfile";
            this.btnUpdateAccountProfile.Size = new System.Drawing.Size(113, 29);
            this.btnUpdateAccountProfile.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnUpdateAccountProfile.TabIndex = 171;
            this.btnUpdateAccountProfile.Text = "UPDATE";
            this.btnUpdateAccountProfile.Click += new System.EventHandler(this.btnUpdateAccountProfile_Click);
            // 
            // btnUpdateAccountSettings
            // 
            this.btnUpdateAccountSettings.Location = new System.Drawing.Point(598, 488);
            this.btnUpdateAccountSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateAccountSettings.Name = "btnUpdateAccountSettings";
            this.btnUpdateAccountSettings.Size = new System.Drawing.Size(114, 29);
            this.btnUpdateAccountSettings.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnUpdateAccountSettings.TabIndex = 172;
            this.btnUpdateAccountSettings.Text = "UPDATE";
            this.btnUpdateAccountSettings.Click += new System.EventHandler(this.btnUpdateAccountSettings_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(25, 298);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(62, 29);
            this.btnBrowse.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnBrowse.TabIndex = 173;
            this.btnBrowse.Text = "BROWSE";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Emoji", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 26);
            this.label3.TabIndex = 167;
            this.label3.Text = "Account Settings";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Emoji", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(20, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(209, 26);
            this.label11.TabIndex = 174;
            this.label11.Text = "Personal Information";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usrCntrlProfileLaboratory11
            // 
            this.usrCntrlProfileLaboratory11.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.bg;
            this.usrCntrlProfileLaboratory11.Controls.Add(this.lblPath);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.label14);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.label11);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.label3);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.btnBrowse);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.btnUpdateAccountSettings);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.btnUpdateAccountProfile);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.panel5);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.panel3);
            this.usrCntrlProfileLaboratory11.Controls.Add(this.panel6);
            this.usrCntrlProfileLaboratory11.Location = new System.Drawing.Point(188, 83);
            this.usrCntrlProfileLaboratory11.Name = "usrCntrlProfileLaboratory11";
            this.usrCntrlProfileLaboratory11.Size = new System.Drawing.Size(739, 571);
            this.usrCntrlProfileLaboratory11.TabIndex = 101;
            // 
            // lblPath
            // 
            this.lblPath.BackColor = System.Drawing.Color.White;
            this.lblPath.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(162, 299);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(333, 26);
            this.lblPath.TabIndex = 176;
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(93, 304);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 17);
            this.label14.TabIndex = 175;
            this.label14.Text = "File Path:";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel2;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.panel1;
            this.bunifuDragControl2.Vertical = true;
            // 
            // bunifuDragControl3
            // 
            this.bunifuDragControl3.Fixed = true;
            this.bunifuDragControl3.Horizontal = true;
            this.bunifuDragControl3.TargetControl = this.label10;
            this.bunifuDragControl3.Vertical = true;
            // 
            // bunifuDragControl4
            // 
            this.bunifuDragControl4.Fixed = true;
            this.bunifuDragControl4.Horizontal = true;
            this.bunifuDragControl4.TargetControl = this.label1;
            this.bunifuDragControl4.Vertical = true;
            // 
            // bunifuDragControl5
            // 
            this.bunifuDragControl5.Fixed = true;
            this.bunifuDragControl5.Horizontal = true;
            this.bunifuDragControl5.TargetControl = this.pictureBox8;
            this.bunifuDragControl5.Vertical = true;
            // 
            // usrCntrlNextInLineLaboratory1
            // 
            this.usrCntrlNextInLineLaboratory1.BackColor = System.Drawing.Color.White;
            this.usrCntrlNextInLineLaboratory1.Controls.Add(this.dgNextInLineLaboratory1);
            this.usrCntrlNextInLineLaboratory1.Controls.Add(this.lblTotalRecords);
            this.usrCntrlNextInLineLaboratory1.Controls.Add(this.label2);
            this.usrCntrlNextInLineLaboratory1.Controls.Add(this.lblID);
            this.usrCntrlNextInLineLaboratory1.Location = new System.Drawing.Point(188, 82);
            this.usrCntrlNextInLineLaboratory1.Name = "usrCntrlNextInLineLaboratory1";
            this.usrCntrlNextInLineLaboratory1.Size = new System.Drawing.Size(739, 572);
            this.usrCntrlNextInLineLaboratory1.TabIndex = 102;
            // 
            // dgNextInLineLaboratory1
            // 
            this.dgNextInLineLaboratory1.AllowUserToAddRows = false;
            this.dgNextInLineLaboratory1.AllowUserToDeleteRows = false;
            this.dgNextInLineLaboratory1.AllowUserToResizeColumns = false;
            this.dgNextInLineLaboratory1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgNextInLineLaboratory1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgNextInLineLaboratory1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(196)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(116)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNextInLineLaboratory1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgNextInLineLaboratory1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNextInLineLaboratory1.EnableHeadersVisualStyles = false;
            this.dgNextInLineLaboratory1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgNextInLineLaboratory1.Location = new System.Drawing.Point(4, 3);
            this.dgNextInLineLaboratory1.Margin = new System.Windows.Forms.Padding(4);
            this.dgNextInLineLaboratory1.Name = "dgNextInLineLaboratory1";
            this.dgNextInLineLaboratory1.ReadOnly = true;
            this.dgNextInLineLaboratory1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgNextInLineLaboratory1.RowHeadersVisible = false;
            this.dgNextInLineLaboratory1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgNextInLineLaboratory1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNextInLineLaboratory1.Size = new System.Drawing.Size(731, 543);
            this.dgNextInLineLaboratory1.TabIndex = 111;
            this.dgNextInLineLaboratory1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgNextInLineLaboratory1_CellDoubleClick);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(131, 550);
            this.lblTotalRecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(118, 18);
            this.lblTotalRecords.TabIndex = 110;
            this.lblTotalRecords.Text = "Total Record(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 549);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 109;
            this.label2.Text = "Total Record(s):";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(418, 549);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 13);
            this.lblID.TabIndex = 112;
            // 
            // tmrRefreshDGV
            // 
            this.tmrRefreshDGV.Enabled = true;
            this.tmrRefreshDGV.Interval = 3000;
            this.tmrRefreshDGV.Tick += new System.EventHandler(this.tmrRefreshDGV_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Laboratory 1";
            this.notifyIcon1.BalloonTipTitle = "Norzagaray Municipal Hospital";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Norzagaray Municipal Hospital";
            this.notifyIcon1.Visible = true;
            // 
            // pnlHome
            // 
            this.pnlHome.BackColor = System.Drawing.SystemColors.Control;
            this.pnlHome.BackgroundImage = global::NMHLBOPDRMS.Properties.Resources.LAB1;
            this.pnlHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHome.Controls.Add(this.pictureBox2);
            this.pnlHome.Controls.Add(this.lblTime);
            this.pnlHome.Controls.Add(this.lblDate);
            this.pnlHome.Controls.Add(this.lblNameOfLaboratory);
            this.pnlHome.Location = new System.Drawing.Point(187, 82);
            this.pnlHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(739, 571);
            this.pnlHome.TabIndex = 87;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::NMHLBOPDRMS.Properties.Resources.cob;
            this.pictureBox2.Location = new System.Drawing.Point(418, 329);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 33);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 93;
            this.pictureBox2.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.lblTime.Location = new System.Drawing.Point(454, 333);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(49, 25);
            this.lblTime.TabIndex = 90;
            this.lblTime.Text = "time";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.lblDate.Location = new System.Drawing.Point(124, 333);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 21);
            this.lblDate.TabIndex = 91;
            this.lblDate.Text = "Date";
            // 
            // lblNameOfLaboratory
            // 
            this.lblNameOfLaboratory.AutoSize = true;
            this.lblNameOfLaboratory.BackColor = System.Drawing.Color.Transparent;
            this.lblNameOfLaboratory.Font = new System.Drawing.Font("Segoe UI Emoji", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfLaboratory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.lblNameOfLaboratory.Location = new System.Drawing.Point(245, 231);
            this.lblNameOfLaboratory.Name = "lblNameOfLaboratory";
            this.lblNameOfLaboratory.Size = new System.Drawing.Size(209, 26);
            this.lblNameOfLaboratory.TabIndex = 84;
            this.lblNameOfLaboratory.Text = "*name of laboratory*";
            // 
            // pictureBox21
            // 
            this.pictureBox21.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox21.Image = global::NMHLBOPDRMS.Properties.Resources.About_64px1;
            this.pictureBox21.Location = new System.Drawing.Point(15, 0);
            this.pictureBox21.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox21.Name = "pictureBox21";
            this.pictureBox21.Size = new System.Drawing.Size(22, 22);
            this.pictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox21.TabIndex = 77;
            this.pictureBox21.TabStop = false;
            // 
            // usrCntrlLaboratoryResults1
            // 
            this.usrCntrlLaboratoryResults1.Location = new System.Drawing.Point(188, 83);
            this.usrCntrlLaboratoryResults1.Name = "usrCntrlLaboratoryResults1";
            this.usrCntrlLaboratoryResults1.Size = new System.Drawing.Size(739, 571);
            this.usrCntrlLaboratoryResults1.TabIndex = 87;
            // 
            // frmMainLaboratory1
            // 
            this.ClientSize = new System.Drawing.Size(927, 654);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.usrCntrlNextInLineLaboratory1);
            this.Controls.Add(this.usrCntrlLaboratoryResults1);
            this.Controls.Add(this.usrCntrlProfileLaboratory11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainLaboratory1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Laboratory 1";
            this.Load += new System.EventHandler(this.frmMainLaboratory1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.pnlSlctnHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.pnlSlctnProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            this.pnlSlctnLogout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).EndInit();
            this.pnlSlctnNextInLine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnlSlctnLaboratoryResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctrProfile)).EndInit();
            this.usrCntrlProfileLaboratory11.ResumeLayout(false);
            this.usrCntrlProfileLaboratory11.PerformLayout();
            this.usrCntrlNextInLineLaboratory1.ResumeLayout(false);
            this.usrCntrlNextInLineLaboratory1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNextInLineLaboratory1)).EndInit();
            this.pnlHome.ResumeLayout(false);
            this.pnlHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).EndInit();
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        public void getDataset()
        {
            conn.Open();
            string sql = "SELECT patientNo AS 'Patient No.', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', status AS 'Status', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory1";
            adapter = new MySqlDataAdapter(sql, conn);
            adapter.Fill(dS, "tblNext_In_Line_Laboratory1");
            dgNextInLineLaboratory1.DataMember = "tblNext_In_Line_Laboratory1";
            dgNextInLineLaboratory1.DataSource = dS;
            conn.Close();

            dgNextInLineLaboratory1.Columns[0].Width = 150;
            dgNextInLineLaboratory1.Columns[1].Width = 150;
            dgNextInLineLaboratory1.Columns[2].Width = 150;
            dgNextInLineLaboratory1.Columns[3].Width = 150;
            dgNextInLineLaboratory1.Columns[4].Width = 125;
            dgNextInLineLaboratory1.Columns[5].Width = 125;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory1", conn);
            c = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = c.ToString() + " records.";
        }

        private void frmMainLaboratory1_Load(object sender, EventArgs e)
        {
            getDataset();

            dtPicker.Text = DateTime.Now.ToShortDateString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            timer1.Start();

        }

        #region PanelSelection
        private void pnlSlctnHome_Click_1(object sender, EventArgs e)
        {
            pnlHome.Show();
            usrCntrlNextInLineLaboratory1.Hide();
            usrCntrlLaboratoryResults1.Hide();
            usrCntrlProfileLaboratory11.Hide();
            if (pnlSlctnHome.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnHome.Checked == false)
            {
                pnlSlctnHome.Checked = true;
                pnlSlctnHome.ForeColor = Color.Black;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnNextInLine_Click_1(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory1.Show();
            usrCntrlLaboratoryResults1.Hide();
            usrCntrlProfileLaboratory11.Hide();
            if (pnlSlctnNextInLine.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.Black;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnNextInLine.Checked == false)
            {
                pnlSlctnNextInLine.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.Black;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }

        private void pnlSlctnLaboratoryResults_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory1.Hide();
            usrCntrlLaboratoryResults1.Show();
            usrCntrlProfileLaboratory11.Hide();
            if (pnlSlctnLaboratoryResults.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnLaboratoryResults.Checked == false)
            {
                pnlSlctnLaboratoryResults.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.Black;
                pnlSlctnProfile.ForeColor = Color.White;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnProfile.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }
        
        private void pnlSlctnProfile_Click(object sender, EventArgs e)
        {
            pnlHome.Hide();
            usrCntrlNextInLineLaboratory1.Hide();
            usrCntrlLaboratoryResults1.Hide();
            usrCntrlProfileLaboratory11.Show();
            if (pnlSlctnProfile.Checked == true)
            {
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
            else if (pnlSlctnProfile.Checked == false)
            {
                pnlSlctnProfile.Checked = true;
                pnlSlctnHome.ForeColor = Color.White;
                pnlSlctnNextInLine.ForeColor = Color.White;
                pnlSlctnLaboratoryResults.ForeColor = Color.White;
                pnlSlctnProfile.ForeColor = Color.Black;
                pnlSlctnLogout.ForeColor = Color.White;
                pnlSlctnHome.Checked = false;
                pnlSlctnNextInLine.Checked = false;
                pnlSlctnLaboratoryResults.Checked = false;
                pnlSlctnLogout.Checked = false;
            }
        }
        #endregion

        private void btnUpdateAccountProfile_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtMiddleName.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    byte[] img = null;
                    FileStream fs = new FileStream(lblPath.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);

                    conn.Open();
                    string sql = "UPDATE tblAccount_Profile SET lastName = @lname, firstName = @fname, middleName = @mname, birthday = @bday, profilePicture = @picture, pathName = @path WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@lname", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@fname", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text);
                    cmd.Parameters.AddWithValue("@bday", dtBirthday.Value);
                    cmd.Parameters.AddWithValue("@picture", img);
                    cmd.Parameters.AddWithValue("@path", lblPath.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "LABORATORY 1";
                    string activity = "SUCCESSFULLY UPDATED PERSONAL INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Personal Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Select image first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtLastName.Text == "")
                    txtLastName.Focus();
                else if (txtFirstName.Text == "")
                    txtFirstName.Focus();
                else if (txtMiddleName.Text == "")
                    txtMiddleName.Focus();
                else if (txtUsername.Text == "")
                    txtUsername.Focus();
                else if (txtPassword.Text == "")
                    txtPassword.Focus();
            }
        }

        private void btnUpdateAccountSettings_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "" && txtRetypePassword.Text != "")
            {
                if (txtRetypePassword.Text == txtPassword.Text)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Open();
                    string sql = "UPDATE tblAccounts SET username =  @username, password = @password WHERE IDNumber = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtIDNumber.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Activity Log
                    string accType = "LABORATORY 1";
                    string activity = "SUCCESSFULLY UPDATED ACCOUNT INFORMATION.";

                    conStr.activityLog(accType, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, activity);
                    
                    MessageBox.Show("Account Information successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChange.Enabled = true;
                    txtRetypePassword.Enabled = false;
                    txtRetypePassword.Clear();
                    btnCancel.Visible = false;
                }
                else
                {
                    MessageBox.Show("Password didn't match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.SelectionStart = 0;
                    txtPassword.SelectionLength = txtPassword.TextLength;
                    txtPassword.Focus();
                }
            }
            else
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string imgLocation = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG files(*.jpg)|*.jpg|PNG files(*.png)|*png|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pctrProfile.ImageLocation = imgLocation;
                lblPath.Text = imgLocation;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
            txtPassword.Focus();
            btnChange.Enabled = false;
            txtRetypePassword.Enabled = true;
            btnCancel.Visible = true;
        }

        private void dgNextInLineLaboratory1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string number = dgNextInLineLaboratory1.Rows[e.RowIndex].Cells[0].Value.ToString();

            conn.Open();
            string sql = "SELECT * FROM tblNext_In_Line_Laboratory1 WHERE patientNo = @num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            string lname = "";
            string fname = "";
            string mname = "";
            string labExam = "";
            string status = "";
            string date = "";
            while (reader.Read())
            {
                lname = reader.GetString("lastName");
                fname = reader.GetString("firstName");
                mname = reader.GetString("middleName");
                date = reader.GetString("daterequested");
                status = reader.GetString("status");
            }
            conn.Close();

            frmLaboratory1 f1 = new frmLaboratory1();

            conn.Open();
            string sql1 = "SELECT labExam FROM tblMedical_Laboratory_1 WHERE patientsNo = @num";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                labExam = reader1.GetString("labExam");
                f1.cboLabExams.Items.Add(labExam);
            }
            conn.Close();

            conn.Open();
            string sql2 = "SELECT * FROM tblLaboratory_Blood_Chemistry WHERE patientsNo = @num";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            int a = 0;
            while (reader2.Read())
            {
                a += 1;
            }
            conn.Close();

            conn.Open();
            string sql3 = "SELECT * FROM tblLaboratory_Fecalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            int b = 0;
            while (reader3.Read())
            {
                b += 1;
            }
            conn.Close();

            conn.Open();
            string sql4 = "SELECT * FROM tblLaboratory_Hematology WHERE patientsNo = @num";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader4 = cmd4.ExecuteReader();
            int c = 0;
            while (reader4.Read())
            {
                c += 1;
            }
            conn.Close();

            conn.Open();
            string sql5 = "SELECT * FROM tblLaboratory_Urinalysis_Mac WHERE patientsNo = @num";
            MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
            cmd5.Parameters.AddWithValue("@num", number);
            MySqlDataReader reader5 = cmd5.ExecuteReader();
            int d = 0;
            while (reader5.Read())
            {
                d += 1;
            }
            conn.Close();

            if (a == 1 || b == 1 || c == 1 || d == 1)
            {
                f1.btnPrintBloodChemistry.Visible = false;
                f1.btnPrintFecalysis.Visible = false;
                f1.btnPrintHematology.Visible = false;
                f1.btnPrintUrinalysis.Visible = false;
                f1.lblNumberLab.Text = number;
                f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                f1.dtRequestedLab.Text = date;
                f1.lblStatus.Text = status;
                f1.labIdNumber = lblIDNumber.Text;
                f1.cboMedTech.Items.Add(lblNameOfLaboratory.Text + " R.M.T.");
                f1.ShowDialog();
            }
            else
            {
                f1.btnSave1.SendToBack();
                f1.btnSave2.SendToBack();
                f1.btnSave3.SendToBack();
                f1.btnSave4.SendToBack();
                f1.btnPrintBloodChemistry.Visible = false;
                f1.btnPrintFecalysis.Visible = false;
                f1.btnPrintHematology.Visible = false;
                f1.btnPrintUrinalysis.Visible = false;
                f1.lblNumberLab.Text = number;
                f1.lblNameLab.Text = fname + " " + mname + " " + lname;
                f1.dtRequestedLab.Text = date;
                f1.lblStatus.Text = status;
                f1.cboMedTech.Items.Add(lblNameOfLaboratory.Text + " R.M.T.");
                f1.ShowDialog();
            }
        }

        public int c;

        private void tmrRefreshDGV_Tick(object sender, EventArgs e)
        {
            dS.Clear();

            conn.Open();
            string sql = "SELECT patientNo AS 'Patient No.', lastName AS 'Last Name', firstName AS 'First Name', middleName AS 'Middle Name', status AS 'Status', daterequested AS 'Date Requested' FROM tblNext_In_Line_Laboratory1";
            adapter = new MySqlDataAdapter(sql, conn);
            dS = new DataSet();
            adapter.Fill(dS, "tblNext_In_Line_Laboratory1");
            dgNextInLineLaboratory1.DataMember = "tblNext_In_Line_Laboratory1";
            dgNextInLineLaboratory1.DataSource = dS;
            conn.Close();

            dgNextInLineLaboratory1.Columns[0].Width = 150;
            dgNextInLineLaboratory1.Columns[1].Width = 150;
            dgNextInLineLaboratory1.Columns[2].Width = 150;
            dgNextInLineLaboratory1.Columns[3].Width = 150;
            dgNextInLineLaboratory1.Columns[4].Width = 125;
            dgNextInLineLaboratory1.Columns[5].Width = 125;

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblNext_In_Line_Laboratory1", conn);
            int countt = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            lblTotalRecords.Text = countt.ToString() + " records.";

            if (c < countt)
            {
                notifyIcon1.BalloonTipText = "New Patient received.";
                notifyIcon1.ShowBalloonTip(5000);
                notifyIcon1.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRetypePassword.Clear();
            txtRetypePassword.Enabled = false;
            btnCancel.Visible = false;
            btnChange.Enabled = true;
        }
    }
}
