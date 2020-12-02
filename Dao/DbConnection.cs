using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace test.Dao
{
    public class DbConnection
    {
        public SqlConnection Connexion = new SqlConnection(@"Server=FINANCES-PC;Database=test;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password='treso@123456'");
        public SqlTransaction sqlTransaction;
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
        public SqlConnection getConnexion()
        {
            if (Connexion.State == System.Data.ConnectionState.Closed)
            {

                Connexion.Open();
            }
            return Connexion;
        }


        public SqlConnection setCloseConnexion()
        {
            if (Connexion.State == System.Data.ConnectionState.Open)
            {
                Connexion.Close();
            }
            return Connexion;
        }
        public DateTime getDateServer()
        {

            string query = "select convert(datetime,GETDATE()) as madate";
            using (SqlCommand cmd = new SqlCommand(query, Access.getConnexion()))
            {
                using (SqlDataReader monRd = cmd.ExecuteReader())
                {
                    monRd.Read();
                    return Convert.ToDateTime(monRd["madate"]);
                }
            }
        }
    }
}
