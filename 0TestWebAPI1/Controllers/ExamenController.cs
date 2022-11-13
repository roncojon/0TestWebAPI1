using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _0TestWebAPI1.Models;
using System;
using _0TestWebAPI1.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using _0TestWebAPI1.SupportFunctions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using _0TestWebAPI1.ClassesForTheApi;

namespace _0TestWebAPI1.Controllers
    {

    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerSuper<Examen9, Guid>
        {

        public ExamenController(PruebasDbContext context) : base(context)
            {
            }
        [NonAction]
        public async override Task<List<Examen9>> GetAll() { List<Examen9> temp = new List<Examen9>(); return temp; }
        
        [HttpGet]
        // [Authorize]
        // [Route("examenPlus")]
        public async Task<List<ExamenPlus>> GetAllPlus()
            {
            List<ExamenPlus> examenesPlus = new List<ExamenPlus>();

            List<Examen9> examenes = await _dbContext.Set<Examen9>().ToListAsync();

            foreach (var examen in examenes)
                {
                PruebaMatriz8 pmTemp = new PruebaMatriz8();
                pmTemp = await _dbContext.FindAsync<PruebaMatriz8>(examen.PruebaMatrizNombre);

                ExamenPlus newExamenPlus = new ExamenPlus();

                newExamenPlus.Id = examen.Id;
                newExamenPlus.PruebaMatrizNombre = examen.PruebaMatrizNombre;
                newExamenPlus.PatronClave = examen.PatronClave;
                newExamenPlus.Fecha = examen.Fecha;

                newExamenPlus.Descripcion = pmTemp.Descripcion;
                newExamenPlus.CantColumnas = pmTemp.CantColumnas;
                newExamenPlus.CantidadFilas = pmTemp.CantidadFilas;
                newExamenPlus.TiempoLimiteMs = pmTemp.TiempoLimiteMs;

                examenesPlus.Add(newExamenPlus);
                }

            return examenesPlus;
            // return await _dbContext.Set<Examen9>().ToListAsync();


            }

        [NonAction]
        public async override Task Post(Examen9 e) { }

        [AllowAnonymous]
        [HttpPost]
        public async Task Post(string pruebaMatrizNombre, bool isPatronOriginal,List<Guid> usuariosIds)
            {
            /*var fecha = value.Fecha.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-ES"));
            var temp = value;
            temp.Fecha = fecha;*/
           

            PruebaMatriz8 temp = await _dbContext.PruebaMatriz.FirstOrDefaultAsync(p => p.Nombre == pruebaMatrizNombre);
            string patronOriginal = temp.PatronOriginal;

            Examen9 newExamen = new Examen9();
            newExamen.Id = Guid.NewGuid();
            newExamen.PruebaMatrizNombre = pruebaMatrizNombre;
            newExamen.Fecha = DateTime.Now;

            if (!isPatronOriginal)
                {
                PatronExamen pattern = new PatronExamen(patronOriginal);
                newExamen.PatronClave = pattern.GenerarPatron();
                }
            else
                newExamen.PatronClave = patronOriginal;

            _dbContext.Entry(newExamen).State = EntityState.Added;
            // await _dbContext.Set<T>().AddAsync(value);
            await _dbContext.SaveChangesAsync();

            //CREANDO LA RELACION DE CADA USUARIO ASIGNADO AL EXAMEN
            foreach (var item in usuariosIds)
                {
                UsuarioExamen10 newUsuarioExamen = new UsuarioExamen10();
                newUsuarioExamen.Usuario1Id = item;
                newUsuarioExamen.Examen9Id = newExamen.Id;
                _dbContext.Entry(newUsuarioExamen).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();

                }
            }
        }
    }
