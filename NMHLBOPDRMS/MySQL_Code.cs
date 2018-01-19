using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NMHLBOPDRMS
{
    class MySQL_Code
    {
      //  public static string connectionString = "server = localhost; username = root; database = dbHospital; port = 3306; password = 1234";
       public static string connectionString = "server=" + Properties.Settings.Default.server + ";username=" + Properties.Settings.Default.username + ";database=" + Properties.Settings.Default.database + ";port=" + Properties.Settings.Default.port + ";password=" + Properties.Settings.Default.password + ";";
        public MySqlConnection conn = new MySqlConnection(connectionString);

        public void activityLog(string acctype, string fname, string mname, string lname, string activity)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM tblActivity_Log", conn);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            conn.Open();
            string sql = "INSERT INTO tblActivity_Log VALUES (@logNum, @accType, @name, @activity, @date, @time)";
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            cmd1.Parameters.AddWithValue("@logNum", count++);
            cmd1.Parameters.AddWithValue("@accType", acctype);
            cmd1.Parameters.AddWithValue("@name", fname + " " + mname + " " + lname);
            cmd1.Parameters.AddWithValue("@activity", activity);
            cmd1.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd1.Parameters.AddWithValue("@time", DateTime.Now.ToShortTimeString());
            cmd1.ExecuteNonQuery();
            conn.Close();
        }
    }
}
