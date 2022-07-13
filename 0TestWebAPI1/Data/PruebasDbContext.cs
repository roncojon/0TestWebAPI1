using _0TestWebAPI1.Models;
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
            });

            modelBuilder.Entity<Usuario>().HasData(new Usuario[]
            {
               new Usuario
               {Id = 1,
                   Nombre = "Admin",
                   Apellidos = "Admin",
                   NickName = "Admin",
                   Password = "PsychoMaster.10*",
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
                   Password = "PsychoMaster.10*",
                   RolId = 3,
                   Ci = 86060670067,
                   Sexo = false,
                   Edad = 36,
                   GrupoEtarioId = 1,
                   EscolaridadId = 3,
               },
               });

            //modelBuilder.Entity<Usuario>().HasData(
            //    new Usuario
            //    {
            //        Id = "f42559a2-2776-4e9b-9ba1-268597eff72b",
            //        UserName = "admin",
            //        NormalizedUserName = "ADMIN",
            //        Email = "admin@nauta.cu",
            //        NormalizedEmail = "ADMIN@NAUTA.CU",
            //        PasswordHash = "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==",
            //        SecurityStamp = "43VMKYQKNTENYZVJNU2TII26X23H5PGV",
            //        ConcurrencyStamp = "36fd2616-8e8a-4cc6-8a5a-52d963207836",
            //        Activo = true,
            //        Nombres = "Administrador",
            //        Apellidos = "General",
            //    }
            //);

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "f42559a2-2776-4e9b-9ba1-268597eff72b",
            //        RoleId = "1"
            //    }
            //);
            //// Leer del json y guardar en base de datos los elementos por defecto
            //var r = System.IO.File.ReadAllText("Data/DatosIniciales/redesSociales.json");
            //var redesSociales = JsonConvert.DeserializeObject<List<RedSocial>>(r);

            //foreach (var item in redesSociales)
            //{
            //    modelBuilder.Entity<RedSocial>().HasData(
            //        new RedSocial
            //        {
            //            Id = item.Id,
            //            Nombre = item.Nombre,
            //            Icon = item.Icon
            //        }
            //    );
            //    modelBuilder.ForNpgsqlUseIdentityColumns();

            //}
        }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

        public DbSet<Centro> Centro { get; set; }

        public DbSet<UsuarioCentro> UsuarioCentro { get; set; }

        public DbSet<PruebaBase> PruebaBase { get; set; }

        public DbSet<PruebaDeCaritas> PruebaCaritas { get; set; }
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
