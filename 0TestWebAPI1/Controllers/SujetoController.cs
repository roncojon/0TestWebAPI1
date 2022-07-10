using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SujetoController : ControllerBase
    {
        private PruebasDbContext _dbContext;

        public SujetoController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<SujetoController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _dbContext.Usuario;
        }

        // GET api/<SujetoController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            return _dbContext.Usuario.Find(id);
        }

        // POST api/<SujetoController>
        [HttpPost]
        public void Post([FromBody] Usuario subject)
        {
            _dbContext.Usuario.Add(subject);
            _dbContext.SaveChanges();
        }

        // PUT api/<SujetoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SujetoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
