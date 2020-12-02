using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using test.models;

namespace test.Dao
{
    public class dDetailCommande
    {
        private static object SyncRoot = new object();
        private static dDetailCommande instance;
        public static dDetailCommande Access
        {
            get
            {

                instance = new dDetailCommande();

                return instance;
            }
        }

        public string MiseAjourCommande(publierProduit detailCommande)
        {
  
            string msg = "";
            string query = $@"UPDATE [test].[dbo].[DetailCommande]  

                            SET [CodeProduit] = '{detailCommande.CodeProduit}'
                              ,[CodeCommande] = '{detailCommande.CodeCommande}'
                              ,[QteCommandee] ={detailCommande.QteCommandee}
                              ,[MontantUnitaire] = {detailCommande.MontantUnitaire}
                              ,[Sens] = '{detailCommande.sens}'
                              ,[Monnaie] = '{detailCommande.Monnaie}'

                              WHERE  [CodeProduit] ={detailCommande.CodeProduit}";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, DbConnection.Access.getConnexion()))
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        msg = "1";

                    }
                }
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }
            finally
            {
                DbConnection.Access.setCloseConnexion();
            }

            return msg;    
        }

    }
}
