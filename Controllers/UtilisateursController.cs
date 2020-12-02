using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.models;
using test.Dao;

namespace test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilisateursController : Controller
    {      

        [HttpPost]
        public string CreateUser(Utilisateurs utilisateurs)
        {

            return dUtilisateurs.Access.Ajouter(utilisateurs);
        }  
    }
}
