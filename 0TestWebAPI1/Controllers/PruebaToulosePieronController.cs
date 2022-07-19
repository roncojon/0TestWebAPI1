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
    public class PruebaToulosePieronController : ControllerBase
    {
        private readonly PruebasDbContext _context;

        public PruebaToulosePieronController(PruebasDbContext context)
        {
            _context = context;
        }

        // GET: api/PruebaToulosePieron
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PruebaToulosePieron>>> GetPruebaToulosePieron()
        {
            return await _context.PruebaToulosePieron.ToListAsync();
        }

        // GET: api/PruebaToulosePieron/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PruebaToulosePieron>> GetPruebaToulosePieron(int id)
        {
            var pruebaToulosePieron = await _context.PruebaToulosePieron.FindAsync(id);

            if (pruebaToulosePieron == null)
            {
                return NotFound();
            }

            return pruebaToulosePieron;
        }

        // PUT: api/PruebaToulosePieron/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPruebaToulosePieron(int id, PruebaToulosePieron pruebaToulosePieron)
        {
            if (id != pruebaToulosePieron.Id)
            {
                return BadRequest();
            }

            _context.Entry(pruebaToulosePieron).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PruebaToulosePieronExists(id))
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

        // POST: api/PruebaToulosePieron
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PruebaToulosePieron>> PostPruebaToulosePieron(PruebaToulosePieron pruebaToulosePieron)
        {
            _context.PruebaToulosePieron.Add(pruebaToulosePieron);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPruebaToulosePieron", new { id = pruebaToulosePieron.Id }, pruebaToulosePieron);
        }

        // DELETE: api/PruebaToulosePieron/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePruebaToulosePieron(int id)
        {
            var pruebaToulosePieron = await _context.PruebaToulosePieron.FindAsync(id);
            if (pruebaToulosePieron == null)
            {
                return NotFound();
            }

            _context.PruebaToulosePieron.Remove(pruebaToulosePieron);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PruebaToulosePieronExists(int id)
        {
            return _context.PruebaToulosePieron.Any(e => e.Id == id);
        }
    }
}
