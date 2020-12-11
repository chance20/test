using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Sql;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace test.Dao
{
    public class DbConnection
    {
        public MySqlConnection Connexion = new MySqlConnection(@"Server=localhost;Database=mysql;User ID=root;Password='treso@123456'");
        public MySqlTransaction sqlTransaction;
        private static object SyncRoot = new object();

            private static DbConnection instance;
            public static DbConnection Access
            {
                get
                {
                    instance = new DbConnection();
                    return instance;
                }

            }
        public MySqlConnection getConnexion()
        {
            if (Connexion.State == System.Data.ConnectionState.Closed)
            {

                Connexion.Open();
            }
            return Connexion;
        }


        public MySqlConnection setCloseConnexion()
        {
            if (Connexion.State == System.Data.ConnectionState.Open)
            {
                Connexion.Close();
            }
            return Connexion;
        }
        public DateTime getDateServer()
        {

            string query = "select convert(now(),date) as madate";
            using (MySqlCommand cmd = new MySqlCommand(query, Access.getConnexion()))
            {
                using (MySqlDataReader monRd = cmd.ExecuteReader())
                {
                    monRd.Read();
                    return Convert.ToDateTime(monRd["madate"]);
                }
            }
        }
    }
}
