using _0TestWebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Data
{
    public class PruebasDbContext : DbContext
    {
        public PruebasDbContext(DbContextOptions<PruebasDbContext> options): base(options)
        {

        }

        public DbSet<PruebaDeCaritas> PruebaCaritas { get; set; }
        public DbSet<Sujeto> Sujeto { get; set; }
        public DbSet<SujetoPruebaCaritas> SujetoPruebaCaritas { get; set; }

        public DbSet<Centro> Centro { get; set; }

        public DbSet<SujetoCentro> SujetoCentro { get; set; }

        public DbSet<PruebaBase> PruebaBase { get; set; }

        public DbSet<GrupoEtario> GrupoEtario { get; set; }

        public DbSet<Escolaridad> Escolaridad { get; set; }
    }
}
