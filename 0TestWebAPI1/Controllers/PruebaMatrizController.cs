using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaMatrizController : ControllerSuper<Test, Guid>
    {
        public PruebaMatrizController(PruebasDbContext context) : base(context)
        {
        }
    }
}
