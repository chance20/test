using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace test.models
{
    public class Produits
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




    }
}
