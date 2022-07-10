﻿using _0TestWebAPI1.ClassesForTheApi;
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
    public class UsuarioController : ControllerBase
    {
        private PruebasDbContext _dbContext;

        public UsuarioController(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<SujetoController>
        //[HttpGet]
        //public IEnumerable<Sujeto> Get()
        //{
        //    return _dbContext.Sujeto;
        //}

        //[HttpGet]
        //public async Task<IEnumerable<Sujeto>> GetAsync()
        //{

        //    IEnumerable<Sujeto> sujetos = await _dbContext.Sujeto.ToListAsync();

        //    return sujetos;
        //}

        [HttpGet]
        [Route("usuarios")]//asdasd
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            //var result = await _dbContext.Sujeto
            //    .Include(s => s.GrupoEtario)
            //    .Include(s => s.Escolaridad)
            //    .Select(x => new MostrarSujeto(){
            //        Id = x.Id,
            //        Nombre = x.Nombre,
            //        GrupoEtario = x.GrupoEtario.Grupo,
            //        Escolaridad = x.Escolaridad.NivelEscolar
            //    }).ToListAsync();
            //return result;

            var result =
                await
                (from user in _dbContext.Usuario
                 select  new Usuario {
                     Id= user.Id,
                     Nombre= user.Nombre,
                     Apellidos=user.Apellidos,
                     RolId = user.RolId,
                     Password =user.Password,
                     Sexo = user.Sexo,
                     GrupoEtarioId = user.GrupoEtarioId, 
                     EscolaridadId = user.EscolaridadId
                 })
                 .ToListAsync();

            return result;
        }

        // GET api/<SujetoController>/5
        [HttpGet("{id}")]
        public async Task<Usuario> GetAsync(int id)
        {
            return await _dbContext.Usuario.FindAsync(id);
        }

        // POST api/<SujetoController>
        //[HttpPost]
        //public async Task PostAsync([FromBody] Usuario subject)
        //{
        //    //var sujetoTemp = subject;
        //    await _dbContext.Usuario.AddAsync(subject);
        //    //var temp = await _dbContext.GrupoEtario.
        //    //sujetoTemp.GrupoEtarioId =temp.Id;
        //    //await _dbContext.Sujeto.AddAsync(sujetoTemp);
        //     await _dbContext.SaveChangesAsync();
        //}

        //[HttpPost]
        //public async Task PostAsyncGrupoEtario([FromBody] GrupoEtario group)
        //{
        //    await _dbContext.GrupoEtario.AddAsync(group);
        //    await _dbContext.SaveChangesAsync();
        //}

        // PUT api/<SujetoController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Usuario value)
        {
            var sujetoAcambiar = await _dbContext.Usuario.FindAsync(id);
            //await _dbContext.GrupoEtario.AddAsync(value.GrupoEtario);
            sujetoAcambiar.Nombre = value.Nombre;
            sujetoAcambiar.Apellidos = value.Apellidos;
            sujetoAcambiar.Sexo = value.Sexo;
            sujetoAcambiar.Edad = value.Edad;
            //sujetoAcambiar.GrupoEtario = value.GrupoEtario; //Aki se kita el value y se pone bien el grupo etario segun edad
            //sujetoAcambiar.Escolaridad = value.Escolaridad; //Los posibles niveles de escolaridad se configuran en la bd

            await _dbContext.SaveChangesAsync();
        }


        // DELETE api/<SujetoController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            Usuario temp =await _dbContext.Usuario.FindAsync(id);
             _dbContext.Usuario.Remove(temp);
            await _dbContext.SaveChangesAsync();
        }


    }
}
