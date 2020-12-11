using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace test.models
{
    public class Utilisateurs
    {
        [Required]
        public string CodeUtilisateur { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [Required]
        public string NomUnique { get; set; }

        public DateTime dateNaissance { get; set; }
      
        [Required]
        public string password { get; set; }
        public string Photo { get; set; }
    }
}
