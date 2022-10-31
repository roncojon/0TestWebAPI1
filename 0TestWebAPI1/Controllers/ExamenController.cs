using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _0TestWebAPI1.Models;
using System;
using _0TestWebAPI1.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;



namespace _0TestWebAPI1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerSuper<Examen9, Guid>
    {
        
        public ExamenController(PruebasDbContext context) : base(context)
        {
        }
        [HttpPost]
        public async override Task Post(Examen9 value)
        {
            /*var fecha = value.Fecha.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-ES"));
            var temp = value;
            temp.Fecha = fecha;*/
            _dbContext.Entry(value).State = EntityState.Added;
            // await _dbContext.Set<T>().AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }
    }
}
