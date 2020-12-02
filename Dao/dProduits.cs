using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                           '{publierProduit.Produits.CodeProduit}'
                           ,'{publierProduit.Produits.description}'
                           ,'{publierProduit.Produits.Photo}'
                           ,'{publierProduit.Produits.designation}'
                           ,Convert(money,{publierProduit.Produits.prixUnitaire}),'{publierProduit.Produits.Monnaie}')
                           ";

            string query1 = $@"INSERT INTO [test].[dbo].[Commande]
                           (
                              [CodeCommande]
                              ,[CodeUtilisateur]
                              ,[Sens]
                              ,[DateCommande])
                              VALUES
                           (
                           '{publierProduit.Commande.CodeCommande}'
                           ,'{publierProduit.Commande.CodeUtilisateur}'
                           ,'{publierProduit.Commande.sens}'
                           ,'{publierProduit.Commande.DateCommande}'
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
                           '{publierProduit.DetailCommande.CodeProduit}'
                           ,'{publierProduit.DetailCommande.CodeCommande}'
                           ,'{publierProduit.DetailCommande.QteCommandee}'
                           ,Convert(money,{publierProduit.DetailCommande.MontantUnitaire})
                           ,'{publierProduit.DetailCommande.sens}','{publierProduit.DetailCommande.Monnaie}')
                           ";



            SqlConnection sqlConnection = DbConnection.Access.getConnexion();

            using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
            {
                try
                {
                    result = new SqlCommand(query, sqlConnection, sqlTransaction)
                      .ExecuteNonQuery() > 0 ? "1" : "0";

                    result1 = new SqlCommand(query1, sqlConnection, sqlTransaction)
                       .ExecuteNonQuery() > 0 ? "1" : "0";

                    result2 = new SqlCommand(query2, sqlConnection, sqlTransaction)
                       .ExecuteNonQuery() > 0 ? "1" : "0";

                    if (result.Equals("1") && result1.Equals("1") && result2.Equals("1"))
                    {
                        sqlTransaction.Commit();
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



        public publierProduit Remplir(SqlDataReader rd)
        {
            publierProduit oProduits = new publierProduit();

            oProduits.Produits.CodeProduit = rd["CodeProduit"].ToString();
            oProduits.Produits.description = rd["description"].ToString();
            oProduits.Produits.Photo = rd["Photo"].ToString();
            oProduits.Produits.designation = rd["designation"].ToString();
            oProduits.Produits.prixUnitaire = Convert.ToDouble(rd["prixUnitaire"]);
            oProduits.Produits.Monnaie = rd["Monnaie"].ToString();

            oProduits.DetailCommande.QteCommandee = Convert.ToInt32(rd["Monnaie"]);
            oProduits.DetailCommande.sens =rd["Monnaie"].ToString();
            oProduits.DetailCommande.CodeCommande = rd["CodeCommande"].ToString();

            return oProduits;
        }
        public ObservableCollection<publierProduit> ListeProduits(string CodeUtilisateur)
        {
            ObservableCollection<publierProduit> Listeproduits = new ObservableCollection<publierProduit>();
         
            string query = "";

            query = $@"SELECT produit.CodeProduit,produit.Photo,produit.description,produit.designation,produit.prixUnitaire,produit.[Monnaie],DetailCommande.QteCommandee, DetailCommande.[Sens], DetailCommande.[CodeCommande]  FROM [test].[dbo].[Produits] as produit 
                    join DetailCommande on produit.[CodeProduit] = DetailCommande.[CodeProduit]  
                    join Commande on DetailCommande.[CodeCommande] = Commande.[CodeCommande] where Commande.[CodeUtilisateur] = '{CodeUtilisateur}' ";
           
            using (SqlCommand cmd = new SqlCommand(query, DbConnection.Access.getConnexion()))
            {
                using (SqlDataReader rd = cmd.ExecuteReader())
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
