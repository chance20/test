using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.models
{
    public class DetailCommande
    {   [Required]
        public string CodeProduit { get; set; }
        [Required]
        public string CodeCommande { get; set; }
        [Required]
        public int QteCommandee { get; set; }
        [Required]
        public double MontantUnitaire { get; set; }
        [Required]
        public string sens { get; set; }
        [Required]
        public string Monnaie { get; set; }
    }
}
