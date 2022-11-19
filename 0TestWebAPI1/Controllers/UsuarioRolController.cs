using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerSuper<UsuarioRol6, Guid>
    {
        public UsuarioRolController(PruebasDbContext context) : base(context)
        {
        }
        [NonAction]
        public override async Task<UsuarioRol6> GetById(Guid id)
        {
            return new UsuarioRol6();
        }
        [HttpGet]
        [Route("urol")]
        public async Task<UsuarioRol6> GetByIds(Guid UsuarioCi, string rolId)
        {
            return await _dbContext.FindAsync<UsuarioRol6>(UsuarioCi, rolId);
        }
    }
}

