using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SujetoDePruebaController : ControllerBase
    {
        List<Sujeto> Sujetos = new List<Sujeto>()
        {
            new Sujeto(){Nombre="Osvaldito",Apellidos="Perez",Edad=40,Sexo=false},
        }; 

        [HttpGet]
        public IEnumerable<Sujeto> Get()
        {
            return Sujetos;
        }

    }
}
