using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using _0TestWebAPI1.ClassesForTheApi;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CentroController : ControllerBase
    {
        private readonly PruebasDbContext _dbContext;

        public CentroController(PruebasDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/Centro
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Centro>>> GetCentro()
        //{
        //    return await _dbContext.Centro.ToListAsync();
        //}

        [HttpGet]
        [Route("usuarios")]
        public async  Task<List<UsuariosXcentro>> GetUsuariosPorCentro()
        {
            List<UsuariosXcentro> usuariosXcentro = new List<UsuariosXcentro>();
            foreach (var centro in _dbContext.Centro)
            { 
                UsuariosXcentro uXcTemp = new UsuariosXcentro();
                uXcTemp.CentroId = centro.Id;
                uXcTemp.NombreDelCentro = centro.Nombre;
                List<Usuario> usuarios = new List<Usuario>();
                
                foreach (var uc in _dbContext.UsuarioCentro.Where(uc => uc.CentroId == centro.Id)
                //var uCent =await _dbContext.UsuarioCentro.ToListAsync();
                )
                {
                    Usuario user =await _dbContext.Usuario.FindAsync(uc.UsuarioId);
                    usuarios.Add(user);
                }

                uXcTemp.Usuarios = usuarios;
                usuariosXcentro.Add(uXcTemp);
            }


            return usuariosXcentro;
        }

        // GET: api/Centro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosXcentro>> GetCentro(int id)
        {
            var centro = await _dbContext.Centro.FindAsync(id);
            if (centro == null)
            {
                return NotFound();
            }

            List<Usuario> usuarios = new List<Usuario>();

            foreach (var uc in _dbContext.UsuarioCentro.Where(uc => uc.CentroId == id))
            {
                Usuario user = await _dbContext.Usuario.FindAsync(uc.UsuarioId);
                usuarios.Add(user);
            }
            UsuariosXcentro uXc = new UsuariosXcentro();
            uXc.CentroId = centro.Id;
            uXc.NombreDelCentro = centro.Nombre;
            uXc.Usuarios = usuarios;

            return uXc;
        }

        // PUT: api/Centro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCentro(int id, Centro centro)
        {
            if (id != centro.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(centro).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Centro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Centro>> PostCentro(Centro centro)
        {
            _dbContext.Centro.Add(centro);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCentro", new { id = centro.Id }, centro);
        }

        // DELETE: api/Centro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentro(int id)
        {
            var centro = await _dbContext.Centro.FindAsync(id);
            if (centro == null)
            {
                return NotFound();
            }

            _dbContext.Centro.Remove(centro);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CentroExists(int id)
        {
            return _dbContext.Centro.Any(e => e.Id == id);
        }
    }
}
