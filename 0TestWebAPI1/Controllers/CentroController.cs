using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class CentroController : ControllerBase
    {

        private PruebasDbContext _dbContext;

        public CentroController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CentroController>
        //[Authorize]
        [HttpGet]
        public IEnumerable<Centro> Get()
        {
            return _dbContext.Centro;
        }

        // GET api/<CentroController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CentroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CentroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CentroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
