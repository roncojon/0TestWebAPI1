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
using System.Linq;

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
                Test pmTemp = new Test();
                pmTemp = await _dbContext.FindAsync<Test>(examen.TestNombre);

                ExamenPlus newExamenPlus = new ExamenPlus();

                newExamenPlus.Id = examen.UId;
                newExamenPlus.PruebaMatrizNombre = examen.TestNombre;
                newExamenPlus.PatronClave = examen.PatronClave;

                /*List<Examen9> exams = _dbContext.Fecha.FirstOrDefault(f => f.Examenes)*/
               /* List<Fecha> fechasAll = await _dbContext.Set<Fecha>().ToListAsync();

                // por cada fecha salvada
                foreach (Fecha f in fechasAll)
                    {
                    // revisa todos los examenes en esa fecha
                    foreach (Examen9 e in f.Examenes)
                        {
                        // si el examen es el q busco, q es el q le quiero cojer la fecha
                        if (e.UId == examen.UId)
                            {
                            // le cojo esa fecha para mostrarla en examen plus
                            newExamenPlus.Fecha = f.TimeStamp;
                            }
                        }

                    }*/

                // newExamenPlus.Fecha = 
                // newExamenPlus.Activo = examen.Activo;


                newExamenPlus.Descripcion = pmTemp.Descripcion;
                newExamenPlus.CantColumnas = pmTemp.CantColumnas;
                newExamenPlus.CantidadFilas = pmTemp.CantidadFilas;
                newExamenPlus.TiempoLimiteMs = pmTemp.TiempoLimiteMs;

                examenesPlus.Add(newExamenPlus);
                }

            return examenesPlus;
            // return await _dbContext.Set<Examen9>().ToListAsync();
            }

       /* [HttpGet]
        // [Authorize]
        [Route("examenesActivos")]
        public async Task<List<ExamenPlus>> GetActivos()
            {
            List<ExamenPlus> examenesPlus = new List<ExamenPlus>();
            List<Examen9> examenes = await _dbContext.Set<Examen9>().Where(e => e.Activo == true).ToListAsync();

            foreach (var examen in examenes)
                {
                PruebaMatriz8 pmTemp = new PruebaMatriz8();
                pmTemp = await _dbContext.FindAsync<PruebaMatriz8>(examen.TestNombre);

                ExamenPlus newExamenPlus = new ExamenPlus();

                newExamenPlus.UId = examen.UId;
                newExamenPlus.TestNombre = examen.TestNombre;
                newExamenPlus.PatronClave = examen.PatronClave;
                newExamenPlus.Fecha = examen.FechaCreacion;


                newExamenPlus.Descripcion = pmTemp.Descripcion;
                newExamenPlus.CantColumnas = pmTemp.CantColumnas;
                newExamenPlus.CantidadFilas = pmTemp.CantidadFilas;
                newExamenPlus.TiempoLimiteMs = pmTemp.TiempoLimiteMs;

                examenesPlus.Add(newExamenPlus);
                }

            return examenesPlus;
            }*/

        [NonAction]
        public async override Task Post(Examen9 e) { }

        [AllowAnonymous]
        [HttpPost]
        public async Task Post(string pruebaMatrizNombre, bool isPatronOriginal,List<Guid> usuariosIds)
            {
            /*var fecha = value.Fecha.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-ES"));
            var temp = value;
            temp.Fecha = fecha;*/
           

            Test temp = await _dbContext.Test.FirstOrDefaultAsync(p => p.Nombre == pruebaMatrizNombre);
            string patronOriginal = temp.PatronOriginal;

            Examen9 newExamen = new Examen9();
            newExamen.UId = Guid.NewGuid();
            newExamen.TestNombre = pruebaMatrizNombre;

            // var fechaDbtry = Activator.CreateInstance(typeof(Fecha));

            // fechaNow.TimeStamp = dto.ToUnixTimeMilliseconds();
            // using (var transaction = _dbContext.Database.BeginTransaction()) { 
            /*using (_dbContext)
                {*/
                /*_dbContext.Database.OpenConnection();
                try
                    {
                    _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Fecha ON");*/
                    
            /*Fecha fechaNowTemp = new Fecha(*//*dto.ToUnixTimeMilliseconds()*//*);
                    DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                    fechaNowTemp.TimeStamp = dto.ToUnixTimeMilliseconds();
                    Fecha fechaNow = fechaNowTemp;
                    _dbContext.Add(fechaNow);

                    await _dbContext.SaveChangesAsync();*/

                   /* await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Fecha OFF");
                    }
                finally
                    {
                    _dbContext.Database.CloseConnection();
                    }*/
                /*}*/
            /*transaction.Commit();
            }*/

            if (!isPatronOriginal)
                {
                PatronExamen pattern = new PatronExamen(patronOriginal);
                newExamen.PatronClave = pattern.GenerarPatron();
                }
            else
                newExamen.PatronClave = patronOriginal;

            /*_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fecha ON");
            _dbContext.Entry(fechaNow).State = EntityState.Added;
            _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fecha OFF");*/

            // _dbContext.Entry(fechaNow).State = EntityState.Added;
            _dbContext.Entry(newExamen).State = EntityState.Added;
            // await _dbContext.Set<T>().AddAsync(value);
            await _dbContext.SaveChangesAsync();

            //CREANDO LA RELACION DE CADA USUARIO ASIGNADO AL EXAMEN
            foreach (var item in usuariosIds)
                {
                UsuarioExamen10 newUsuarioExamen = new UsuarioExamen10();
                newUsuarioExamen.UsuarioId = item;
                newUsuarioExamen.ExamenId = newExamen.UId;

                // DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                newUsuarioExamen.FechaTimeStamp = 0/*dto.ToUnixTimeMilliseconds()*/;

                _dbContext.Entry(newUsuarioExamen).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                }
            }

        /*[HttpPost]
        [Route("examenesActivos")]
        public  async Task DisableExam(Guid examenId*//* bool setIsActive*//*, Z id*//*)
            {
            Examen9 temp = await _dbContext.Examen.FirstOrDefaultAsync(e => e.UId == examenId);
            temp.Activo = false;
            await _dbContext.SaveChangesAsync();
            }*/
            }
        }
