using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.models
{
    public class Commande
    {
        [Required]
        public string CodeCommande { get; set; }
        [Required]
        public string CodeUtilisateur { get; set; }
        
        [Required]
        //Le sens peut etre achat ou vente
        public string sens { get; set; }
        [Required]
        public DateTime DateCommande { get; set; }
       
    }
}
