using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public override async Task Post(UsuarioExamen10 ue) { }
        [NonAction]
        public override async Task<UsuarioExamen10> GetById(Guid id)
            {
            return new UsuarioExamen10();
            }
        [HttpGet]
        [Route("uexamen")]
        public async Task<UsuarioExamen10> GetByIds(string UsuarioCi, Guid examenId, long timeSpan)
            {
            List<UsuarioExamen10> ueList = await _dbContext.UsuarioExamen.ToListAsync();
            UsuarioExamen10 ueResult = new UsuarioExamen10();
            foreach (var ue in ueList)
                {
                if (ue.UsuarioCi == UsuarioCi && ue.ExamenId == examenId && ue.Fecha == timeSpan)
                    ueResult = ue;
                }
            return ueResult;
            }
        [HttpPost]
        [Route("uexamen2")]
        public async Task<IActionResult> PostExamen(string UsuarioCi, Guid examenId, string respuestaDeUsuarioAExamen)
            {
            try
                {
                List<UsuarioExamen10> ueList = await _dbContext.UsuarioExamen.Where(ue => ue.ExamenId == examenId && ue.UsuarioCi == UsuarioCi).ToListAsync();

                UsuarioExamen10 ueInDb = new UsuarioExamen10();
                if (ueList.Count == 1)
                    {
                    if (ueList[0].Fecha == 0)
                        {
                        ueInDb = ueList[0];
                        }
                    else
                        {
                        return BadRequest("Error, ya este usuario realizó este examen");
                        }
                    //UsuarioExamen10 usuarioExamen = await _dbContext.FindAsync<UsuarioExamen10>(UsuarioCi, examenId,(long)0);

                    UsuarioExamen10 ue2 = new UsuarioExamen10();
                    ue2.UsuarioCi = ueInDb.UsuarioCi;
                    ue2.ExamenId = ueInDb.ExamenId;
                    //ue2.PatronUsuario = ueInDb.PatronUsuario;
                    ue2.PatronUsuario = respuestaDeUsuarioAExamen;

                    DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                    Examen9 exam = await _dbContext.Examen.FirstOrDefaultAsync(e => e.UId == examenId);

                    ue2.Fecha = dto.ToUnixTimeMilliseconds();

                    if (ue2.Fecha > exam.FechaInicio && ue2.Fecha < exam.FechaFin)
                        {
                        _dbContext.Remove(ueInDb);
                        await _dbContext.SaveChangesAsync();

                        _dbContext.Entry(ue2).State = EntityState.Added;
                        await _dbContext.SaveChangesAsync();

                        return Ok();
                        }
                    if (ue2.Fecha < exam.FechaInicio)
                        {
                        return BadRequest("Aún no puede realizar este examen");
                        }
                    /*if (ue2.Fecha > exam.FechaFin)
                        {*/
                        return BadRequest("Ya no puede realizar este examen");
                        /*}*/
                    }
                else
                    {
                    return BadRequest("Error, este usuario no ha sido asignado a este examen");
                    }
                }
            catch (Exception)
                {
                throw;
                }
            }
        }
    }
