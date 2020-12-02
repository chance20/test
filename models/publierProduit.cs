using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.models
{
    public class publierProduit 
    {      
        public Produits Produits;
        public Commande Commande;
        public DetailCommande DetailCommande;

        public string CodeProduit { get; set; }

    }
}
