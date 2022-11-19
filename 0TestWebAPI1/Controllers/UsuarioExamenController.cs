using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<UsuarioExamen10> GetByIds(string UsuarioCi, Guid examenId)
            {
            return await _dbContext.FindAsync<UsuarioExamen10>(UsuarioCi, examenId);
            }
        [HttpPost]
        [Route("uexamen2")]
        public async Task PostExamen(string UsuarioCi, Guid examenId, string respuestaDeUsuarioAExamen)
            {
            try
                {
                var usuarioExamen = await _dbContext.FindAsync<UsuarioExamen10>(UsuarioCi, examenId,(long)0);
                UsuarioExamen10 ue2 = new UsuarioExamen10();
                ue2.UsuarioCi = usuarioExamen.UsuarioCi;
                ue2.ExamenId = usuarioExamen.ExamenId;
                ue2.PatronUsuario = usuarioExamen.PatronUsuario;

                DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                ue2.Fecha = dto.ToUnixTimeMilliseconds(); ;

                usuarioExamen.PatronUsuario = respuestaDeUsuarioAExamen;

                // DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                // Fecha fechaNow = new Fecha(/*dto.ToUnixTimeMilliseconds()*/);
                // fechaNow.TimeStamp = dto.ToUnixTimeMilliseconds();

                /* DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                 usuarioExamen.Fecha = dto.ToUnixTimeMilliseconds();*/
                _dbContext.Remove(usuarioExamen);
                await _dbContext.SaveChangesAsync();

                _dbContext.Entry(ue2).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                }
            catch (Exception)
                {
                throw;
                }
            }
        }
    }
