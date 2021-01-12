using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.Dao;
using test.models;

namespace test.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailCommandeController : ControllerBase
    {
     
        //Mise a jour des commandes passées
        [HttpGet]
        public string MiseAjourCommande(publierProduit detailCommande)
        { 
            return dDetailCommande.Access.MiseAjourCommande(detailCommande);
        }


        
    }
}
