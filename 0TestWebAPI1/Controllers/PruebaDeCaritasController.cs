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
using _0TestWebAPI1.ClassesForTheApi;

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
            List<PruebaDeCaritas> pc = new List<PruebaDeCaritas>();
            
            foreach (PruebaDeCaritas prueba in _dbContext.PruebaCaritas)
            {
                List<Fila> filas = new List<Fila>();
                foreach (var fila in _dbContext.Fila)
                {
                    if (fila.PruebaBaseId == prueba.Id)
                    {
                        filas.Add(fila);
                    }
                }
                prueba.Filas = filas;
                pc.Add(prueba);
            }

            return pc;
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

        //[HttpPost]
        //public async Task<ActionResult<PruebaDeCaritas>> PostPruebaDeCaritas(PruebaDeCaritas pruebaDeCaritas, int userId)
        //{
        //    Usuario usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Id == userId);

        //    PruebaDeCaritas pc = new PruebaDeCaritas();
        //    pc = pruebaDeCaritas;
        //    pc.Evaluar(pruebaDeCaritas);
        //    //await _dbContext.PruebaBase.OfType<PruebaDeCaritas>();
        //    await _dbContext.PruebaCaritas.AddAsync(pc);
        //    if (usuario.PruebaDeCaritas == null)
        //    {
        //        usuario.PruebaDeCaritas = new List<PruebaDeCaritas>();
        //    }
        //    usuario.PruebaDeCaritas.Add(pc);

        //    _dbContext.SaveChanges();

        //    pc = usuario.PruebaDeCaritas.Where(p => p.UsuarioId == usuario.Id).OrderBy(p => p.Id).LastOrDefault();
        //    //pc = usuario.PruebaDeCaritas.LastOrDefault();
        //    List<Fila> filas = new List<Fila>();


        //    foreach (var fila in pruebaDeCaritas.Filas)
        //    {
        //        Fila filaTemp = new Fila();
        //        filaTemp.PruebaBaseId = pc.Id;
        //        filaTemp.Attempts = fila.Attempts;
        //        filaTemp.Annotations = fila.Annotations;
        //        filaTemp.Errors = fila.Errors;
        //        filaTemp.Omissions = fila.Omissions;
        //        await _dbContext.Fila.AddAsync(filaTemp);
        //        await _dbContext.SaveChangesAsync();
        //        filaTemp = await _dbContext.Fila.Where(f => f.PruebaBaseId == pc.Id).OrderBy(f => f.Id).LastOrDefaultAsync();
        //        //filaTemp = await _dbContext.Fila.LastOrDefaultAsync(f => f.PruebaBaseId == pc.Id);
        //        filas.Add(filaTemp);
        //        //pc.Filas.Add(filaTemp);
        //    }

        //    pc.Filas = filas;
        //    _dbContext.SaveChanges();

        //    return StatusCode(StatusCodes.Status201Created);

        //    //_context.PruebaCaritas.Add(pruebaDeCaritas);
        //    //await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetPruebaDeCaritas", new { id = pruebaDeCaritas.Id }, pruebaDeCaritas);
        //}
        [HttpPost]
        public  ActionResult<PruebaDeCaritas> PostPruebaDeCaritas(PruebaPlusFilas pruebaDeCaritas)
        {
            Usuario usuario =  _dbContext.Usuario.FirstOrDefault(u => u.Id == pruebaDeCaritas.PruebaCaritas.UsuarioId);

            PruebaDeCaritas pc = new PruebaDeCaritas();
            pc = pruebaDeCaritas.PruebaCaritas;
            pc.Evaluar(pruebaDeCaritas.PruebaCaritas);
            //await _dbContext.PruebaBase.OfType<PruebaDeCaritas>();
             _dbContext.PruebaCaritas.Add(pc);
            if (usuario.PruebaDeCaritas == null)
            {
                usuario.PruebaDeCaritas = new List<PruebaDeCaritas>();
            }
            usuario.PruebaDeCaritas.Add(pc);

            _dbContext.SaveChanges();

            pc = usuario.PruebaDeCaritas.Where(p => p.UsuarioId == usuario.Id).OrderBy(p => p.Id).LastOrDefault();
            List<Fila> filas = new List<Fila>();


            foreach (var fila in pruebaDeCaritas.Filas)
            {
                Fila filaTemp = new Fila();
                filaTemp.PruebaBaseId = pc.Id;
                filaTemp.Attempts = fila.Attempts;
                filaTemp.Annotations = fila.Annotations;
                filaTemp.Errors = fila.Errors;
                filaTemp.Omissions = fila.Omissions;
                 _dbContext.Fila.Add(filaTemp);
                 _dbContext.SaveChanges();
                filaTemp =  _dbContext.Fila.Where(f => f.PruebaBaseId == pc.Id).OrderBy(f => f.Id).LastOrDefault();
                filas.Add(filaTemp);
                //pc.Filas.Add(filaTemp);
            }

            pc.Filas = filas;
            _dbContext.SaveChanges();

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
