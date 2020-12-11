using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using test.models;
using System.Collections.ObjectModel;


namespace test.Dao
{
    public class dProduits
    {
        private static object SyncRoot = new object();
        private static dProduits instance;
        public static dProduits Access
        {
            get
            {

                instance = new dProduits();

                return instance;
            }
        }

        public string PublierProduit(publierProduit publierProduit)
        {
            string msg = "";
            string result = ""; string result1 = ""; string result2 = "";

            string query = $@"INSERT INTO [test].[dbo].[Produits]
                           (
                             [CodeProduit]
                              ,[description]
                              ,[photo]
                              ,[designation]
                              ,[prixUnitaire]
                              ,[Monnaie])
                     VALUES
                           (
                           '{publierProduit.CodeProduit}'
                           ,'{publierProduit.description}'
                           ,'{publierProduit.Photo}'
                           ,'{publierProduit.designation}'
                           ,Convert(money,'{publierProduit.prixUnitaire.ToString()}'),'{publierProduit.Monnaie}')
                           ";

            string query1 = $@"INSERT INTO [test].[dbo].[Commande]
                           (
                              [CodeCommande]
                              ,[CodeUtilisateur]
                              ,[Sens]
                              ,[DateCommande])
                              VALUES
                           (
                           '{publierProduit.CodeCommande}'
                           ,'{publierProduit.CodeUtilisateur}'
                           ,'{publierProduit.sens}'
                           ,'{publierProduit.DateCommande}'
                          )
                           ";

            string query2 = $@"INSERT INTO [test].[dbo].[DetailCommande]
                           (
                              [CodeProduit]
                              ,[CodeCommande]
                              ,[QteCommandee]
                              ,[MontantUnitaire]
                              ,[Sens]
                              ,[Monnaie])

                              VALUES
                           (
                           '{publierProduit.CodeProduit}'
                           ,'{publierProduit.CodeCommande}'
                           ,'{publierProduit.QteCommandee}'
                           ,Convert(money,'{publierProduit.MontantUnitaire.ToString()}')
                           ,'{publierProduit.sens}','{publierProduit.Monnaie}')
                           ";



            MySqlConnection sqlConnection = DbConnection.Access.getConnexion();

            using (MySqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
            {
                try
                {
                    result = new MySqlCommand(query, sqlConnection, sqlTransaction)
                      .ExecuteNonQuery() > 0 ? "1" : "0";

                    result1 = new MySqlCommand(query1, sqlConnection, sqlTransaction)
                       .ExecuteNonQuery() > 0 ? "1" : "0";

                    result2 = new MySqlCommand(query2, sqlConnection, sqlTransaction)
                       .ExecuteNonQuery() > 0 ? "1" : "0";

                    if (result.Equals("1") && result1.Equals("1") && result2.Equals("1"))
                    {
                        sqlTransaction.Commit();
                        msg = "1";
                    }
                    
                    else
                    {
                        sqlTransaction.Rollback();
                        msg = "0";
                    }

                }
                catch (Exception ex)
                {
                    DbConnection.Access.sqlTransaction.Rollback();
                    msg = ex.Message;
                }
                finally
                {

                    DbConnection.Access.setCloseConnexion();
                }

            }
            return msg;

        }



        public publierProduit Remplir(MySqlDataReader rd)
        {
            publierProduit oProduits = new publierProduit();

            oProduits.CodeProduit = rd["CodeProduit"].ToString();
            oProduits.description = rd["description"].ToString();
            oProduits.Photo = rd["Photo"].ToString();
            oProduits.designation = rd["designation"].ToString();
            oProduits.prixUnitaire = Convert.ToDouble(rd["prixUnitaire"]);
            oProduits.Monnaie = rd["Monnaie"].ToString();

            oProduits.QteCommandee = Convert.ToInt32(rd["QteCommandee"]);
            oProduits.sens =rd["sens"].ToString();
            oProduits.CodeCommande = rd["CodeCommande"].ToString();

            return oProduits;
        }
        public ObservableCollection<publierProduit> ListeProduits(string CodeUtilisateur)
        {
            ObservableCollection<publierProduit> Listeproduits = new ObservableCollection<publierProduit>();
         
            string query = "";

            query = $@"SELECT produit.CodeProduit,produit.Photo,produit.description,produit.designation,produit.prixUnitaire,produit.[Monnaie],DetailCommande.QteCommandee, DetailCommande.[Sens], DetailCommande.[CodeCommande]  FROM [test].[dbo].[Produits] as produit 
                    join DetailCommande on produit.[CodeProduit] = DetailCommande.[CodeProduit]  
                    join Commande on DetailCommande.[CodeCommande] = Commande.[CodeCommande] where Commande.[CodeUtilisateur] = '{CodeUtilisateur}' ";
           
            using (MySqlCommand cmd = new MySqlCommand(query, DbConnection.Access.getConnexion()))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Listeproduits.Add(Remplir(rd));

                    }
                    rd.Close();
                }
            }
            DbConnection.Access.setCloseConnexion();

            return Listeproduits;
        }      

    }
}
