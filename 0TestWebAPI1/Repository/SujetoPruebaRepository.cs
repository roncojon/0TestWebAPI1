using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Repository
{
    public class SujetoPruebaRepository
    {
        private PruebasDbContext _dbContext;

        public SujetoPruebaRepository(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Centro>> GetCentros(int centroId)
        {
            List<Centro> centros = await _dbContext.Centro.ToListAsync();
            return centros;
        }

        public async Task<IEnumerable<Sujeto>> GetSujetosPorCentro(int centroId)
        {
            var center = await _dbContext.Centro.FindAsync(centroId);

            var subjectCenter = _dbContext.SujetoCentro;

            List<Sujeto> sujetos = new List<Sujeto>();

            foreach (var item in subjectCenter)
            {
                if (item.Centro.Id == centroId)
                {
                    foreach (var unit in _dbContext.Sujeto)
                    {
                        if (item.Sujeto.Id == unit.Id)
                        {
                            sujetos.Add(unit);
                        }
                    }

                }
            }
            return sujetos;
        }
    }
}
