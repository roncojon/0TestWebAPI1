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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private PruebasDbContext _dbContext;

        public UsersController(PruebasDbContext dbContext)
        { _dbContext = dbContext; }

        [HttpGet]
        [Route("usuarios")]//asdasd
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            //var result = await _dbContext.Sujeto
            //    .Include(s => s.GrupoEtario)
            //    .Include(s => s.Escolaridad)
            //    .Select(x => new MostrarSujeto(){
            //        Id = x.Id,
            //        Nombre = x.Nombre,
            //        GrupoEtario = x.GrupoEtario.Grupo,
            //        Escolaridad = x.Escolaridad.NivelEscolar
            //    }).ToListAsync();
            //return result;

            var result =
                await
                (from user in _dbContext.Usuario
                 select new Usuario
                 {
                     Id = user.Id,
                     Nombre = user.Nombre,
                     Apellidos = user.Apellidos,
                     RolId = user.RolId,
                     Password = user.Password,
                     Sexo = user.Sexo,
                     GrupoEtarioId = user.GrupoEtarioId,
                     EscolaridadId = user.EscolaridadId
                 })
                 .ToListAsync();

            return result;
        }


        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] Usuario user)
        {
            var userWithSameCi = _dbContext.Usuario.Where(u => u.Ci == user.Ci).SingleOrDefault();
            if (userWithSameCi!=null)
            {
                return BadRequest("Ya existe un usuario con este CI");
            }

            var userObj = new Usuario
            {
                Ci = user.Ci,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                RolId = user.RolId,
                Rol=user.Rol,
                EscolaridadId =user.EscolaridadId,
                GrupoEtarioId = user.GrupoEtarioId,
                Password = user.Password,
                Sexo = user.Sexo,
                Edad = user.Edad
            };
            _dbContext.Usuario.Add(userObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
