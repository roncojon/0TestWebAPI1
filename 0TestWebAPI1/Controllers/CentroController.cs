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
    public class CentroController : ControllerBase
    {
        private readonly PruebasDbContext _context;

        public CentroController(PruebasDbContext context)
        {
            _context = context;
        }

        // GET: api/Centro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Centro>>> GetCentro()
        {
            return await _context.Centro.ToListAsync();
        }

        // GET: api/Centro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Centro>> GetCentro(int id)
        {
            var centro = await _context.Centro.FindAsync(id);

            if (centro == null)
            {
                return NotFound();
            }

            return centro;
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

            _context.Entry(centro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            _context.Centro.Add(centro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCentro", new { id = centro.Id }, centro);
        }

        // DELETE: api/Centro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentro(int id)
        {
            var centro = await _context.Centro.FindAsync(id);
            if (centro == null)
            {
                return NotFound();
            }

            _context.Centro.Remove(centro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CentroExists(int id)
        {
            return _context.Centro.Any(e => e.Id == id);
        }
    }
}
