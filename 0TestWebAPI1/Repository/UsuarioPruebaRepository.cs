using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Repository
{
    public class UsuarioPruebaRepository
    {
        private PruebasDbContext _dbContext;

        public UsuarioPruebaRepository(PruebasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Centro>> GetCentros(int centroId)
        {
            List<Centro> centros = await _dbContext.Centro.ToListAsync();
            return centros;
        }

        public async Task<IEnumerable<Usuario>> GetSujetosPorCentro(int centroId)
        {
            var center = await _dbContext.Centro.FindAsync(centroId);

           /* var subjectCenter = _dbContext.UsuarioCentro;*/

            List<Usuario> sujetos = new List<Usuario>();

/*            foreach (var item in subjectCenter)
            {
                if (item.Centro.Id == centroId)
                {
                    foreach (var unit in _dbContext.Usuario)
                    {
                        if (item.Usuario.Id == unit.Id)
                        {
                            sujetos.Add(unit);
                        }
                    }

                }
            }
*/            return sujetos;
        }
    }
}
