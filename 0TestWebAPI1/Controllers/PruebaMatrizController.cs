using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaMatrizController : ControllerSuper<Test, string>
    {
        public PruebaMatrizController(PruebasDbContext context) : base(context)
        {
        }
    }
}
