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
    public class UsuarioExamenController : ControllerSuper<UsuarioExamen10, Guid>
        {
        public UsuarioExamenController(PruebasDbContext context) : base(context)
            {
            }
        [NonAction]
        public override async Task<UsuarioExamen10> GetById(Guid id)
            {
            return new UsuarioExamen10();
            }
        [HttpGet]
        [Route("uexamen")]
        public async Task<UsuarioExamen10> GetByIds(Guid usuarioId, Guid examenId)
            {
            return await _dbContext.FindAsync<UsuarioExamen10>(usuarioId, examenId);
            }
        [HttpPost]
        [Route("uexamen2")]
        public async void PostExamen(Guid usuarioId, Guid examenId, string respuestaDeUsuarioAExamen)
            {
            try
                {
                var usuarioExamen = await _dbContext.FindAsync<UsuarioExamen10>(usuarioId, examenId);
                usuarioExamen.PatronUsuario = respuestaDeUsuarioAExamen;
                await _dbContext.SaveChangesAsync();
                }
            catch (Exception)
                {
                throw;
                }

            }


        }
    }
