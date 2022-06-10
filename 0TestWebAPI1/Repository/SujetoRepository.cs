
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Repository
{
    public class SujetoRepository: ISujetoRepository
    {
        private PruebasDbContext _dbContext; // use dependecy injection

        public SujetoRepository(PruebasDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Sujeto>> GetAllSujetos()
        {
            return await _dbContext.Sujeto.ToListAsync();
        }

        public async Task<IEnumerable<PruebaDeCaritas>> GetPruebasPorSujeto(int sujetoId)
        {
            var subject = await _dbContext.Sujeto.FindAsync(sujetoId);
            var pruebas = _dbContext.SujetoPruebaCaritas;
            List<PruebaDeCaritas> pruebasDeSubject = new List<PruebaDeCaritas>();

            foreach (var item in pruebas)
            {
                if (item.Sujeto.Id==subject.Id)
                    pruebasDeSubject.Add(item.PruebaCaritas);
            }
            return pruebasDeSubject;
        }

        public async Task Post(Sujeto sujeto)
        {
            await _dbContext.AddAsync(sujeto);

            await _dbContext.SaveChangesAsync();
        }
    }
}
