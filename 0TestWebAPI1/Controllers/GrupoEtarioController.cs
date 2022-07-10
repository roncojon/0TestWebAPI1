using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoEtarioController : ControllerBase
    {
        private PruebasDbContext _dbContext;

        public GrupoEtarioController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<GrupoEtarioController>
        [HttpGet]
        public async Task<IEnumerable<GrupoEtario>> GetAsync()
        {

            IEnumerable<GrupoEtario> grupos = await _dbContext.GrupoEtario.ToListAsync();

            return grupos;
        }

        // GET api/<GrupoEtarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GrupoEtarioController>
        [HttpPost]
        public async Task PostAsync([FromBody] GrupoEtario grupo)
        {
            await _dbContext.GrupoEtario.AddAsync(grupo);
            //await _dbContext.Sujeto.AddAsync(subject.Sujetos[0]);
            await _dbContext.SaveChangesAsync();
        }
    

        // PUT api/<GrupoEtarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GrupoEtarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
