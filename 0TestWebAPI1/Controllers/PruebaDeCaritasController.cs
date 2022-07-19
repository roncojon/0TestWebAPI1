using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using System.Reflection;

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaDeCaritasController : ControllerBase
    {
        private readonly PruebasDbContext _dbContext;

        public PruebaDeCaritasController(PruebasDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/PruebaDeCaritas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PruebaDeCaritas>>> GetPruebaCaritas()
        {
            return await _dbContext.PruebaCaritas.ToListAsync();
        }

        // GET: api/PruebaDeCaritas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PruebaDeCaritas>> GetPruebaDeCaritas(int id)
        {
            var pruebaDeCaritas = await _dbContext.PruebaCaritas.FindAsync(id);

            if (pruebaDeCaritas == null)
            {
                return NotFound();
            }

            return pruebaDeCaritas;
        }

        // PUT: api/PruebaDeCaritas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPruebaDeCaritas(int id, PruebaDeCaritas pruebaDeCaritas)
        {
            if (id != pruebaDeCaritas.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(pruebaDeCaritas).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PruebaDeCaritasExists(id))
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

        // POST: api/PruebaDeCaritas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<PruebaDeCaritas>> PostPruebaDeCaritas(PruebaDeCaritas pruebaDeCaritas, int userId)
        {
            //Usuario usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Id == pruebaDeCaritas.UsuarioId);
            //pruebaDeCaritas.Usuario = usuario;

            Usuario usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Id == userId);
            //_dbContext.Usuario.FirstOrDefaultAsync(u => u.Id == userId).PruebaDeCaritas.Add(pruebaDeCaritas);

            //PropertyInfo[] properties = usuario.GetType().GetProperties();
            //foreach (PropertyInfo pi in properties)
            //{
                
            //}

                await _dbContext.PruebaCaritas.AddAsync(pruebaDeCaritas);
            if (usuario.PruebaDeCaritas==null)
            {
                usuario.PruebaDeCaritas = new List<PruebaDeCaritas>();
            }
            usuario.PruebaDeCaritas.Add(pruebaDeCaritas);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);

            //_context.PruebaCaritas.Add(pruebaDeCaritas);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPruebaDeCaritas", new { id = pruebaDeCaritas.Id }, pruebaDeCaritas);
        }

        // DELETE: api/PruebaDeCaritas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePruebaDeCaritas(int id)
        {
            var pruebaDeCaritas = await _dbContext.PruebaCaritas.FindAsync(id);
            if (pruebaDeCaritas == null)
            {
                return NotFound();
            }

            _dbContext.PruebaCaritas.Remove(pruebaDeCaritas);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PruebaDeCaritasExists(int id)
        {
            return _dbContext.PruebaCaritas.Any(e => e.Id == id);
        }
    }
}
