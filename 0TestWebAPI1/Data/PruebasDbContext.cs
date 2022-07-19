using _0TestWebAPI1.Models;
using AuthenticationPlugin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Data
{
    public class PruebasDbContext : DbContext /*IdentityDbContext<ApiUser>*/
    {
        public PruebasDbContext(DbContextOptions<PruebasDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rol>().HasData(new Rol[] {
                new Rol { Id = 1, NombreDelRol = "EXAMINADO" },
                new Rol { Id = 2, NombreDelRol = "ADMINISTRADOR" },
                new Rol { Id = 3, NombreDelRol = "ADMINISTRADORTESTER" },
            });
            modelBuilder.Entity<GrupoEtario>().HasData(new GrupoEtario[] {
                new GrupoEtario { Id = 1},
                new GrupoEtario { Id = 2},
                new GrupoEtario { Id = 3},
            });
            modelBuilder.Entity<Escolaridad>().HasData(new Escolaridad[] {
                new Escolaridad { Id = 1},
                new Escolaridad { Id = 2},
                new Escolaridad { Id = 3},
            });
            modelBuilder.Entity<Centro>().HasData(new Centro[] {
                new Centro { Id = 1,Nombre="CTE Antonio Guiteras"},
                new Centro { Id = 2,Nombre="UMCC"},
            });

            modelBuilder.Entity<Usuario>().HasData(new Usuario[]
            {
               new Usuario
               {Id = 1,
                   Nombre = "Admin",
                   Apellidos = "Admin",
                   NickName = "Admin",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   RolId = 2,
                   Ci = 85050560959,
                   Sexo = false,
                   Edad = 37,
                   GrupoEtarioId = 1,
                   EscolaridadId = 3,
               },
                new Usuario
               {
                    Id =2,
                   Nombre = "AdminTester",
                   Apellidos = "AdminTester",
                   NickName = "AdminTester",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   RolId = 3,
                   Ci = 86060670067,
                   Sexo = false,
                   Edad = 36,
                   GrupoEtarioId = 1,
                   EscolaridadId = 3,
               },
               });
            modelBuilder.Entity<UsuarioCentro>().HasData(new UsuarioCentro[] {
                new UsuarioCentro { Id = 1, UsuarioId=1,CentroId=1 },
                new UsuarioCentro { Id = 2, UsuarioId=2,CentroId=1 },
                new UsuarioCentro { Id = 3, UsuarioId=1,CentroId=2 },
            });
           
        }

        public DbSet<Book>  Books { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<BookCategory> BookCategory { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

        public DbSet<Centro> Centro { get; set; }

        public DbSet<UsuarioCentro> UsuarioCentro { get; set; }

        public DbSet<PruebaBase> PruebaBase { get; set; }

        public DbSet<PruebaDeCaritas> PruebaCaritas { get; set; }
        public DbSet<PruebaToulosePieron> PruebaToulosePieron { get; set; }
        public DbSet<GrupoEtario> GrupoEtario { get; set; }

        //public DbSet<PruebaSimbolos> PruebaSimbolos { get; set; }

        public DbSet<Escolaridad> Escolaridad { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Centro>().HasData(new Centro { Id = 97, Nombre = "TermoelectricaUnidad2" });




        //}
    }
}
