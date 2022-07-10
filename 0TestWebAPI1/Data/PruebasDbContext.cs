using _0TestWebAPI1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Data
{
    public class PruebasDbContext : DbContext /*IdentityDbContext<ApiUser>*/
    {
        public PruebasDbContext(DbContextOptions<PruebasDbContext> options): base(options)
        {

        }

       
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioPruebaBase> UsuarioPruebaCaritas { get; set; }

        public DbSet<Centro> Centro { get; set; }

        public DbSet<UsuarioCentro> UsuarioCentro { get; set; }

        public DbSet<PruebaBase> PruebaBase { get; set; }

        public DbSet<PruebaDeCaritas> PruebaCaritas { get; set; }
        public DbSet<GrupoEtario> GrupoEtario { get; set; }

        //public DbSet<PruebaSimbolos> PruebaSimbolos { get; set; }

        public DbSet<Escolaridad> Escolaridad { get; set; }

        public DbSet<UsuarioPruebaBase> UsuarioPruebaBase { get; internal set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Centro>().HasData(new Centro { Id = 97, Nombre = "TermoelectricaUnidad2" });




        //}
    }
}
