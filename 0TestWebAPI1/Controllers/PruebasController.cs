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
    public class PruebasController : ControllerBase
    {

        private PruebasDbContext _dbContext;

        public PruebasController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<PruebasController>
        [HttpGet]
        public IEnumerable<PruebaDeCaritas> Get()
        {
            return _dbContext.PruebaCaritas;
        }

        // GET api/<PruebasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PruebasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PruebasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PruebasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
