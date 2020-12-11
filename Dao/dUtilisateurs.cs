using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace test.Dao
{
    public class dUtilisateurs
    {
        private static object SyncRoot = new object();
        private static dUtilisateurs instance;
        public static dUtilisateurs Access
        {
            get
            {

                instance = new dUtilisateurs();

                return instance;
            }
        }
        public string Ajouter(Utilisateurs oUtilisateur)
        {
            string msg = "";

            string query = $@"INSERT INTO [test].[dbo].[Utilisateurs]
                           (
                              [CodeUtilisateur]
                              ,[Nom]
                              ,[Prenom]
                              ,[NomUnique]
                              ,[dateNaissance]
                          
                              ,[photo]
                              ,[Password])
                     VALUES
                           (
                           '{oUtilisateur.CodeUtilisateur}'
                           ,'{oUtilisateur.Nom}'
                           ,'{oUtilisateur.Prenom}'
                           ,'{oUtilisateur.NomUnique}'
                           ,'{oUtilisateur.dateNaissance}' ,'{oUtilisateur.Photo}' ,'{oUtilisateur.password}')
                           ";



            using (MySqlCommand cmd = new MySqlCommand(query, DbConnection.Access.getConnexion()))
            {
                try
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        msg = "1";

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
            }
            return msg;

        }


        public string StockerPhoto(string CodeUtilisateur, Utilisateurs utilisateurs)
        {
            string msg = "";
            string query = $@"UPDATE [test].[dbo].[Utilisateurs] 
                    SET [photo] = '{utilisateurs.Photo}' where [CodeUtilisateur] == {CodeUtilisateur}";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, DbConnection.Access.getConnexion()))
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