using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Dao;

namespace test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FonctionPlusController : ControllerBase
    {
        [HttpGet]
        public string GetDataServer()
        {
            return DbConnection.Access.getDateServer().ToShortDateString();
        }
    }
}
