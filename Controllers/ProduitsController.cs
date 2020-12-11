using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.models;
using test.Dao;
using System.Collections.ObjectModel;

namespace test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
       [HttpPost]
       public string PubliierProduit(publierProduit publierProduit)
        {
            return dProduits.Access.PublierProduit(publierProduit);
        }

        [HttpGet]
        public ObservableCollection<publierProduit> ListProduits(string codeUtilisateur)
        {
            ObservableCollection<publierProduit> list = new ObservableCollection<publierProduit>();
            list = dProduits.Access.ListeProduits(codeUtilisateur);
          
            return list;
        }

    }
}
