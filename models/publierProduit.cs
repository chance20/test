using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.models
{
    public class publierProduit 
    {
        [Required]
        public string CodeProduit { get; set; }
        public string description { get; set; }
        public string Photo { get; set; }
        [Required]
        public string designation { get; set; }
        [Required]
        public double prixUnitaire { get; set; }
        [Required]
        public string Monnaie { get; set; }
        [Required]
        public string CodeCommande { get; set; }
        [Required]
        public string CodeUtilisateur { get; set; }

        [Required]
        //Le sens peut etre achat ou vente
        public string sens { get; set; }

        [Required]
        public int QteCommandee { get; set; }
        [Required]
        public double MontantUnitaire { get; set; }
        public DateTime DateCommande { get; set; }


       

    }
}
