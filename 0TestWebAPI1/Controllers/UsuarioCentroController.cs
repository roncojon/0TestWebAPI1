using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCentroController : ControllerBase
    {
        private readonly PruebasDbContext _dbContext;

        public UsuarioCentroController(PruebasDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/UsuarioCentroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCentro>>> GetUsuarioCentro()
        {
            return await _dbContext.UsuarioCentro.ToListAsync();
        }

        // GET: api/UsuarioCentroes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UsuarioCentro>> GetUsuarioCentro(int id)
        //{
        //    var usuarioCentro = await _dbContext.UsuarioCentro.FindAsync(id);

        //    if (usuarioCentro == null)
        //    {
        //        return NotFound();
        //    }

        //    return usuarioCentro;
        //}

        // PUT: api/UsuarioCentroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioCentro(int id, UsuarioCentro usuarioCentro)
        {
            if (id != usuarioCentro.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(usuarioCentro).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioCentroExists(id))
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

        // POST: api/UsuarioCentroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioCentro>>  PostUsuarioCentro(int userId,[FromBody] int[] centros)
        {
            Usuario usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u=>u.Id==userId);
            
            foreach (var centro in centros)
            {
                UsuarioCentro usuarioCentro = new UsuarioCentro();
                Centro center = await _dbContext.Centro.FirstOrDefaultAsync(u => u.Id == centro);
                usuarioCentro.UsuarioId = userId;
                usuarioCentro.Usuario = usuario;
                usuarioCentro.CentroId = center.Id;
                usuarioCentro.Centro = center;
                await _dbContext.UsuarioCentro.AddAsync(usuarioCentro);
                await _dbContext.SaveChangesAsync();
            }
            return StatusCode(201);



            //_dbContext.UsuarioCentro.Add(usuarioCentro);
            //await _dbContext.SaveChangesAsync();

            //return CreatedAtAction("GetUsuarioCentro", new { id = usuarioCentro.Id }, usuarioCentro);
        }

        // DELETE: api/UsuarioCentroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCentro(int id)
        {
            var usuarioCentro = await _dbContext.UsuarioCentro.FindAsync(id);
            if (usuarioCentro == null)
            {
                return NotFound();
            }

            _dbContext.UsuarioCentro.Remove(usuarioCentro);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioCentroExists(int id)
        {
            return _dbContext.UsuarioCentro.Any(e => e.Id == id);
        }
    }
}
