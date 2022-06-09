using _0TestWebAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace _0TestWebAPI1.Repository
{
    public interface ISujetoRepository
    {
        Task<IEnumerable<Sujeto>> GetAllSujetos();
    }
}