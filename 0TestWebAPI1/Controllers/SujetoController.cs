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
    public class SujetoController : ControllerBase
    {
        private PruebasDbContext _dbContext;

        public SujetoController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<SujetoController>
        //[HttpGet]
        //public IEnumerable<Sujeto> Get()
        //{
        //    return _dbContext.Sujeto;
        //}

        [HttpGet]
        public async Task<IEnumerable<Sujeto>> GetAsync()
        {
            IEnumerable<Sujeto> sujetos = await _dbContext.Sujeto.ToListAsync();

            return sujetos;
        }

        // GET api/<SujetoController>/5
        [HttpGet("{id}")]
        public async Task<Sujeto> GetAsync(int id)
        {
            return await _dbContext.Sujeto.FindAsync(id);
        }

        // POST api/<SujetoController>
        [HttpPost]
        public async Task PostAsync([FromBody] Sujeto subject)
        {
            await PostAsyncGrupoEtario(subject.GrupoEtario);
            await _dbContext.Sujeto.AddAsync(subject);
             await _dbContext.SaveChangesAsync();
        }

        public async Task PostAsyncGrupoEtario([FromBody] GrupoEtario group)
        {
            await _dbContext.GrupoEtario.AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<SujetoController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Sujeto value)
        {
            var sujetoAcambiar = await _dbContext.Sujeto.FindAsync(id);
            await _dbContext.GrupoEtario.AddAsync(value.GrupoEtario);
            sujetoAcambiar.Nombre = value.Nombre;
            sujetoAcambiar.Apellidos = value.Apellidos;
            sujetoAcambiar.Sexo = value.Sexo;
            sujetoAcambiar.Edad = value.Edad;
            sujetoAcambiar.GrupoEtario = value.GrupoEtario; //Aki se kita el value y se pone bien el grupo etario segun edad
            sujetoAcambiar.Escolaridad = value.Escolaridad; //Los posibles niveles de escolaridad se configuran en la bd

            await _dbContext.SaveChangesAsync();
        }


        // DELETE api/<SujetoController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            Sujeto temp =await _dbContext.Sujeto.FindAsync(id);
             _dbContext.Sujeto.Remove(temp);
            await _dbContext.SaveChangesAsync();
        }


    }
}
