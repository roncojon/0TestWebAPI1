using _0TestWebAPI1.Data;
using _0TestWebAPI1.IService;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/




namespace _0TestWebAPI1.Service

{
    public class CentroService : IGenericService<Centro4>
    {
        private readonly PruebasDbContext _dbContext;

        public CentroService(PruebasDbContext context)
        {
            _dbContext = context;
        }

        public void Actualizar(Centro4 item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Crear(Centro4 item)
        {
            throw new System.NotImplementedException();
        }

        public void EliminarById<Z>(Z id)
        {
            throw new System.NotImplementedException();
        }

        public  List<Centro4> GetAll()
        {
            /*_dbContext.Entry(item).State = EntityState.Modified;*/
            return  _dbContext.Set<Centro4>().ToList();
        }

        public Centro4 GetById<Z>(Z id)
        {
            throw new System.NotImplementedException();
        }
    }
}
