
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Repository
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private PruebasDbContext _dbContext; // use dependecy injection

        public UsuarioRepository(PruebasDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Usuario1>> GetAllSujetos()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        //public async Task<IEnumerable<PruebaDeCaritas>> GetPruebasPorSujeto(int sujetoId)
        //{
        //    var subject = await _dbContext.Sujeto.FindAsync(sujetoId);
        //    var pruebas = _dbContext.SujetoPruebaBase;
        //    List<PruebaBase> pruebasDeSubject = new List<PruebaBase>();

        //    foreach (var item in pruebas)
        //    {
        //        if (item.Sujeto.Id==subject.Id)
        //            pruebasDeSubject.Add(item.PruebaBase);
        //    }
        //    return pruebasDeSubject;
        //}

        public async Task Post(Usuario1 sujeto)
        {
            await _dbContext.AddAsync(sujeto);

            await _dbContext.SaveChangesAsync();
        }
    }
}
