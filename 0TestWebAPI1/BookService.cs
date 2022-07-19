using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAWEB.Data.Servicios
{
    public class BookService
    {
        private readonly PruebasDbContext _context;


        public BookService(PruebasDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var listado = new List<Book>();
            listado = await _context.Books.Include(x=>x.BookCategories).ThenInclude(x=>x.Category).ToListAsync();
            return listado;
        }
    }
}
