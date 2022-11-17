using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0TestWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerSuper<T,Z> : ControllerBase where T : class, new()
    {
        public readonly PruebasDbContext _dbContext;

        public ControllerSuper(PruebasDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/<ControllerSuper>
        [HttpGet]
        // [Authorize]
        public virtual async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        // GET api/<ControllerSuper>/5
        [HttpGet("{id}")]
        public virtual async Task<T> GetById(Z id)  // public async Task<ActionResult<T>> GetById(Z id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        // POST api/<ControllerSuper>
        [HttpPost]
        public virtual async Task Post(T value)
        {
            var dbtry = Activator.CreateInstance(typeof(T));
            dbtry = value;

            if (typeof(T)==typeof(Examen9))
            {
                Guid id = new Guid();
                var tempExamen = value as Examen9;
                tempExamen.UId = id;
                dbtry = tempExamen;
            }
           /* if (typeof(T) == typeof(Centro4))
            {
                Guid id = new Guid();
                var tempCentro = value as Centro4;
                tempCentro.UId = id;
                dbtry = tempCentro;
            }*/
            if (typeof(T) == typeof(Usuario1) )
            {
                Guid id = new Guid();
                var tempUsuario = value as Usuario1;
                tempUsuario.UId = id;
                dbtry = tempUsuario;
            }

            _dbContext.Entry(dbtry).State = EntityState.Added;
            // await _dbContext.Set<T>().AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<ControllerSuper>/5
        [HttpPut("{id}")]
        public virtual async Task Put(T value/*, Z id*/)
        {
            /*T temp = await _dbContext.Set<T>().FindAsync(id);

             
            PropertyInfo[] lst = typeof(T).GetProperties();
            foreach (PropertyInfo oProperty in lst)
            {
                // cada prop de value se le asigna a temp, o sea cada prop del obj ya guardado en db
                // va a ser igual a cada prop del obj q entra por parametros 
                oProperty.SetValue(oProperty.GetValue(temp), oProperty.GetValue(value));

            }*/
            // _dbContext.Entry(value).State = EntityState.Modified;
            /*  T entity = await _dbContext.Set<T>().FindAsync(id);

              if (entity != null)
                  entity = value;*/
            /*foreach (var prop in temp)
            {

            }*/
             _dbContext.Entry(value).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // DELETE api/<ControllerSuper>/5
        [HttpDelete("{id}")]
        public virtual async Task Delete(Z id)
        {
             T temp = await _dbContext.FindAsync<T>(id);
            // _dbContext.Entry(temp).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(temp);
            await _dbContext.SaveChangesAsync();
        }
    }
}
