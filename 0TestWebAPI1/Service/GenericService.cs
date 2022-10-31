using _0TestWebAPI1.Data;
using _0TestWebAPI1.IService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Service
{
    public class GenericOperations<T> where T :class ,new() /*: IGenericService<T>*/
    {
        private readonly PruebasDbContext _dbContext;

        public GenericOperations(PruebasDbContext context)
        {
            _dbContext = context;
        }

        /*public void Actualizar<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Crear<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public void EliminarById<Z>(Z id)
        {
            throw new System.NotImplementedException();
        }*/

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

      /*  public <T> GetById<Z>(Z id)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
