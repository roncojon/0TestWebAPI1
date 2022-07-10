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
    public class BolonController : ControllerBase
    {

        private PruebasDbContext _dbContext;

        public BolonController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<BolonController>
        //[HttpGet]
        //public async Task<IEnumerable<Sujeto>> GetAsync()
        //{

        //    IEnumerable<Sujeto> sujetos = await _dbContext.Sujeto.ToListAsync();

        //    return sujetos;
        //}

        // GET api/<BolonController>/5
        [HttpGet]
        public async Task<IEnumerable<Usuario>> GetAsync()
        {

            IEnumerable<Usuario> sujetos = await _dbContext.Usuario.ToListAsync();

            return sujetos;
        }

        // POST api/<BolonController>
        [HttpPost]
        public async Task PostAsync([FromBody] Bolon bolon)
        {
            Usuario temp = bolon.Sujeto;
            //Sujeto - Centro
            await _dbContext.Centro.AddAsync(bolon.Centro);
            UsuarioCentro newSujetoCentro = new UsuarioCentro();
            newSujetoCentro.Usuario = bolon.Sujeto;
            newSujetoCentro.Centro = bolon.Centro;
            await _dbContext.UsuarioCentro.AddAsync(newSujetoCentro);

            //Sujeto - PruebaCaritas
            await _dbContext.PruebaBase.AddAsync(bolon.PruebaBase);
            await _dbContext.PruebaCaritas.AddAsync(bolon.PruebaDeCaritas);
            PruebaBase pruebaBaseTemp = new PruebaBase();
            PruebaDeCaritas pruebaCaritasTemp = new PruebaDeCaritas();
            pruebaBaseTemp = bolon.PruebaBase;
            pruebaCaritasTemp = bolon.PruebaDeCaritas;
            pruebaCaritasTemp.PruebaBase = pruebaBaseTemp;

            UsuarioPruebaBase sPb = new UsuarioPruebaBase();
            sPb.PruebaBase = pruebaBaseTemp;
            sPb.Usuario = temp;

            //Sujeto - Escolaridad
            //temp.EscolaridadId = bolon.Escolaridad;
            await _dbContext.Escolaridad.AddAsync(bolon.Escolaridad);




            await _dbContext.GrupoEtario.AddAsync(bolon.GrupoEtario);

            await _dbContext.Usuario.AddAsync(temp);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<BolonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BolonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
