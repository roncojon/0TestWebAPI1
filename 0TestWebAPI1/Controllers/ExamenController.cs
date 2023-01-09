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
        [Route("allPlus")]
        public async Task<List<ExamenPlus>> GetAllPlus()
            {
            List<ExamenPlus> examenesPlus = new List<ExamenPlus>();

            List<Examen9> examenes = await _dbContext.Set<Examen9>().ToListAsync();
            List<UsuarioExamen10> ueAll = await _dbContext.Set<UsuarioExamen10>().ToListAsync();

            foreach (var examen in examenes)
                {
                Test pmTemp = await _dbContext.FindAsync<Test>(examen.TestUId);

                ExamenPlus newExamenPlus = new ExamenPlus();

                newExamenPlus.Id = examen.UId;
                newExamenPlus.TestNombre = pmTemp.Nombre;
                newExamenPlus.TestUId = examen.TestUId;
                newExamenPlus.PatronClave = examen.PatronClave;
                newExamenPlus.FechaInicio = examen.FechaInicio;
                newExamenPlus.FechaFin = examen.FechaFin;

                List<UsuarioExamen10> ueListTemp = new List<UsuarioExamen10>();
                List<Usuario1> usersListTemp = new List<Usuario1>();
                foreach (UsuarioExamen10 ue in ueAll)
                    {
                    if (ue.ExamenId == examen.UId)
                        {
                        ueListTemp.Add(ue);
                        }
                    }
                foreach (var ue in ueListTemp)
                    {
                    Usuario1 userTemp = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Ci == ue.UsuarioCi);
                    usersListTemp.Add(userTemp);
                    }

                newExamenPlus.Usuarios = usersListTemp;
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
                // Hallando si es patron original
                newExamenPlus.EsPatronOriginal = newExamenPlus.PatronClave == pmTemp.PatronOriginal;
                // Hallando si esta activo
                DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                long fechaActual = dto.ToUnixTimeMilliseconds();
                bool isActive = false;
                if (examen.FechaInicio < fechaActual && examen.FechaFin > fechaActual)
                    isActive = true;
                newExamenPlus.EstaActivo = isActive;

                examenesPlus.Add(newExamenPlus);
                }

            return examenesPlus;
            // return await _dbContext.Set<Examen9>().ToListAsync();
            }

        /*[NonAction]
        public async override Task<Examen9> GetById(Guid examenId) { Examen9 temp = new Examen9(); return temp; }*/

        [NonAction]
        [HttpGet("{id}")]
        public override async Task<Examen9> GetById(Guid id) { return await _dbContext.FindAsync<Examen9>(id); }

        [HttpGet]
        [Route("onePlus")]
        public async Task<ExamenPlus> GetByUId(Guid examenId)
            {
            List<UsuarioExamen10> ueAll = await _dbContext.Set<UsuarioExamen10>().ToListAsync();
            Examen9 examen = await _dbContext.FindAsync<Examen9>(examenId);
            Test pmTemp = await _dbContext.FindAsync<Test>(examen.TestUId);

            ExamenPlus newExamenPlus = new ExamenPlus();

            newExamenPlus.Id = examen.UId;
            newExamenPlus.TestNombre = pmTemp.Nombre;
            newExamenPlus.TestUId = examen.TestUId;
            newExamenPlus.PatronClave = examen.PatronClave;
            newExamenPlus.FechaInicio = examen.FechaInicio;
            newExamenPlus.FechaFin = examen.FechaFin;

            List<UsuarioExamen10> ueListTemp = new List<UsuarioExamen10>();
            List<Usuario1> usersListTemp = new List<Usuario1>();
            foreach (UsuarioExamen10 ue in ueAll)
                {
                if (ue.ExamenId == examen.UId)
                    {
                    ueListTemp.Add(ue);
                    }
                }
            foreach (var ue in ueListTemp)
                {
                Usuario1 userTemp = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Ci == ue.UsuarioCi);
                usersListTemp.Add(userTemp);
                }

            newExamenPlus.Usuarios = usersListTemp;
            newExamenPlus.Descripcion = pmTemp.Descripcion;
            newExamenPlus.CantColumnas = pmTemp.CantColumnas;
            newExamenPlus.CantidadFilas = pmTemp.CantidadFilas;
            newExamenPlus.TiempoLimiteMs = pmTemp.TiempoLimiteMs;

            return newExamenPlus;
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
        public async Task<IActionResult> Post(ExamPostNeeds newExamData)
            {
            Test temp = await _dbContext.Test.FirstOrDefaultAsync(p => p.UId == newExamData.TestUId);
            if (temp != null)
                {
                string patronOriginal = temp.PatronOriginal;

                Examen9 newExamen = new Examen9();
                newExamen.UId = Guid.NewGuid();
                newExamen.TestUId = newExamData.TestUId;
                newExamen.FechaInicio = newExamData.FechaInicio;
                newExamen.FechaFin = newExamData.FechaFin;

                if (!newExamData.IsPatronOriginal)
                    {
                    PatronExamen pattern = new PatronExamen(patronOriginal);
                    newExamen.PatronClave = pattern.GenerarPatron();
                    }
                else
                    newExamen.PatronClave = patronOriginal;

                _dbContext.Entry(newExamen).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();

                //CREANDO LA RELACION DE CADA USUARIO ASIGNADO AL EXAMEN
                foreach (var item in newExamData.UsuariosCiList)
                    {
                    UsuarioExamen10 newUsuarioExamen = new UsuarioExamen10();
                    newUsuarioExamen.UsuarioCi = item;
                    newUsuarioExamen.ExamenId = newExamen.UId;

                    newUsuarioExamen.Fecha = 0;

                    _dbContext.Entry(newUsuarioExamen).State = EntityState.Added;
                    await _dbContext.SaveChangesAsync();
                    }
                return new OkResult();
                }
            else
                {
                return BadRequest("No se encuentra un test con ese Id");
                }
            }

        [NonAction]
        public async override Task Put(Examen9 e) { }
        [HttpPut("{UId}")]
        public async Task<IActionResult> Put(Guid UId, [FromBody] ExamPostNeeds newExamData)
            {

            Examen9 examenTemp = await _dbContext.Examen.FirstOrDefaultAsync(e => e.UId == UId);
            if (examenTemp == null)
                return BadRequest("No se encuentra un examen con ese Id");

            Test temp = await _dbContext.Test.FirstOrDefaultAsync(p => p.UId == newExamData.TestUId);
            if (temp != null)
                {
                string patronOriginal = temp.PatronOriginal;

                Examen9 newExamen = new Examen9();
                newExamen.UId = UId;
                newExamen.TestUId = newExamData.TestUId;
                newExamen.FechaInicio = newExamData.FechaInicio;
                newExamen.FechaFin = newExamData.FechaFin;

                //    *****   PATRON   *****
                // Si el patron es el original se usa el patron original y ya
                if (newExamData.IsPatronOriginal)
                    newExamen.PatronClave = patronOriginal;
                // Si el patron no es el original, se revisa si el q habia en el examen ya creado
                // antes de esta modificacion era original.
                else
                    {
                    // Si antes de la modificacion habia un patron original 
                    //  y ahora se quiere un patron random pues se crea ese nuevo patron random
                    if (examenTemp.PatronClave != temp.PatronOriginal)
                        {
                        PatronExamen pattern = new PatronExamen(patronOriginal);
                        newExamen.PatronClave = pattern.GenerarPatron();
                        }
                    // Pero si antes habia un patron random y llega de nuevo un patron random, se mantiene el viejo
                    else
                        {
                        newExamen.PatronClave = temp.PatronOriginal;
                        }
                    }

                _dbContext.Examen.Remove(examenTemp);
                await _dbContext.SaveChangesAsync();
                _dbContext.Entry(newExamen).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();

                //CREANDO LA RELACION DE CADA USUARIO ASIGNADO AL EXAMEN
                foreach (var item in newExamData.UsuariosCiList)
                    {
                    UsuarioExamen10 newUsuarioExamen = new UsuarioExamen10();
                    newUsuarioExamen.UsuarioCi = item;
                    newUsuarioExamen.ExamenId = newExamen.UId;

                    newUsuarioExamen.Fecha = 0;

                    _dbContext.Entry(newUsuarioExamen).State = EntityState.Added;
                    await _dbContext.SaveChangesAsync();
                    }
                return new OkResult();
                }
            else
                {
                return BadRequest("No se encuentra un test con ese Id");
                }
            }

        [HttpGet]

        [Route("activeS")]
        public async Task<List<ExamenPlus>> GetActives(string userCi)
            {
            // Result
            List<Examen9> examenes = new List<Examen9>();
            // Paso intermedio para hallar los examenes en q aparece este usuario
            List<UsuarioExamen10> ueList = await _dbContext.UsuarioExamen.Where(ue => ue.UsuarioCi == userCi).ToListAsync();
            // Fecha actual del sistema. O sea fecha cuando llega el request de ver los examenes
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            long fechaActual = dto.ToUnixTimeMilliseconds();

            // Por cada examen en el q aparezca este usuario
            foreach (var ue in ueList)
                {
                // Si la fecha esta en 0 significa q el usuario fue asignado al examen y aun no lo ha realizado
                if (ue.Fecha == 0)
                    {
                    // Hallando si la fecha actual esta dentro del rango del examen, si es asi, añade este examen a la lista
                    Examen9 examTemp = await _dbContext.Examen.FirstOrDefaultAsync(e => e.UId == ue.ExamenId);
                    if (examTemp.FechaInicio < fechaActual && examTemp.FechaFin > fechaActual)
                        {
                        examenes.Add(examTemp);
                        }
                    }

                }

            // CREANDO LOS EXAMENESPLUS DE LA LISTA DE EXAMENES
            List<ExamenPlus> examenesPlus = new List<ExamenPlus>();

            // List<Examen9> examenes = await _dbContext.Set<Examen9>().ToListAsync();
            List<UsuarioExamen10> ueAll = await _dbContext.Set<UsuarioExamen10>().ToListAsync();

            foreach (var examen in examenes)
                {
                Test pmTemp = await _dbContext.FindAsync<Test>(examen.TestUId);

                ExamenPlus newExamenPlus = new ExamenPlus();

                newExamenPlus.Id = examen.UId;
                newExamenPlus.TestNombre = pmTemp.Nombre;
                newExamenPlus.TestUId = examen.TestUId;
                newExamenPlus.PatronClave = examen.PatronClave;
                newExamenPlus.FechaInicio = examen.FechaInicio;
                newExamenPlus.FechaFin = examen.FechaFin;

                List<UsuarioExamen10> ueListTemp = new List<UsuarioExamen10>();
                List<Usuario1> usersListTemp = new List<Usuario1>();
                foreach (UsuarioExamen10 ue in ueAll)
                    {
                    if (ue.ExamenId == examen.UId)
                        {
                        ueListTemp.Add(ue);
                        }
                    }
                foreach (var ue in ueListTemp)
                    {
                    Usuario1 userTemp = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Ci == ue.UsuarioCi);
                    usersListTemp.Add(userTemp);
                    }

                newExamenPlus.Usuarios = usersListTemp;
                newExamenPlus.Descripcion = pmTemp.Descripcion;
                newExamenPlus.CantColumnas = pmTemp.CantColumnas;
                newExamenPlus.CantidadFilas = pmTemp.CantidadFilas;
                newExamenPlus.TiempoLimiteMs = pmTemp.TiempoLimiteMs;

                examenesPlus.Add(newExamenPlus);
                }

            return examenesPlus;
            }
        }
    }
