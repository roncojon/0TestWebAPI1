using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaridadController : ControllerSuper<Escolaridad, string>
    {
        public EscolaridadController(PruebasDbContext context) : base(context)
        {

        }
    }
}
