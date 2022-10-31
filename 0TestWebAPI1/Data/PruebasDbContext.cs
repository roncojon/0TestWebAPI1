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
            modelBuilder.Entity<Rol7>().HasData(new Rol7[] {
                new Rol7 { Nombre ="EXAMINADO" },
                new Rol7 {  Nombre ="ADMINISTRADOR" },
                new Rol7 {  Nombre ="EXAMINADOR" }
            });
            modelBuilder.Entity<GrupoEtario>().HasData(new GrupoEtario[] {
                new GrupoEtario {Nombre="Joven", Nivel = 1, EdadMinima=12, EdadMaxima=18},
                new GrupoEtario {Nombre="Medio", Nivel = 2, EdadMinima=19, EdadMaxima=30},
                new GrupoEtario {Nombre="Mayor", Nivel = 1, EdadMinima=31, EdadMaxima=60}
            });
            modelBuilder.Entity<Escolaridad>().HasData(new Escolaridad[] {
                new Escolaridad { Nivel = 1, Nombre="Básico"},
                new Escolaridad { Nivel = 2, Nombre="Medio Superior"},
                new Escolaridad { Nivel = 3, Nombre="Superior"},
            });
            Guid centroGuiteras = Guid.NewGuid();
            Guid centroUmcc = Guid.NewGuid();
            modelBuilder.Entity<Centro4>().HasData(new Centro4[] {
                new Centro4 {Id = centroGuiteras, Nombre="CTE Antonio Guiteras"},
                new Centro4 {Id =centroUmcc, Nombre="UMCC"},
            });
            modelBuilder.Entity<Sexo2>().HasData(new Sexo2[] {
                new Sexo2 {Nombre="Masculino"},
                new Sexo2 {Nombre="Femenino"},
            });


            modelBuilder.Entity<Usuario1>().HasData(new Usuario1[]
            {
               new Usuario1
               {Id = Guid.NewGuid(),
                   Nombre = "Admin",
                   Apellidos = "Admin",
                   NickName = "Admin",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   RolNombre = "ADMINISTRADOR",
                   Ci = 85050560959,
                   SexoNombre = "Masculino",
                   Edad = 37,
                   GrupoEtarioNombre = "Medio",
                   EscolaridadNombre = "Básico",
                   Centro4Id = centroGuiteras
               },
                new Usuario1
               {
                    Id = Guid.NewGuid(),
                   Nombre = "Examinador",
                   Apellidos = "Examinador",
                   NickName = "Examinador",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   RolNombre = "EXAMINADOR",
                   Ci = 86060670067,
                   SexoNombre = "Masculino",
                   Edad = 36,
                   GrupoEtarioNombre = "Medio",
                   EscolaridadNombre = "Superior",
                   Centro4Id = centroGuiteras
               },
                new Usuario1
               {
                    Id = Guid.NewGuid(),
                   Nombre = "Examinado",
                   Apellidos = "Examinado",
                   NickName = "Examinado",
                   Password = SecurePasswordHasherHelper.Hash("ExaminadoMaster.10*"),
                   RolNombre = "EXAMINADO",
                   Ci = 86060670065,
                   SexoNombre = "Femenino",
                   Edad = 36,
                   GrupoEtarioNombre = "Medio",
                   EscolaridadNombre = "Superior",
                   Centro4Id = centroUmcc
               }
               });
            modelBuilder.Entity<UsuarioExamen10>()
       .HasKey(u => new { u.Usuario1Id, u.Examen9Id });

            modelBuilder.Entity<UsuarioRol6>()
       .HasKey(u => new { u.UsuarioId, u.RolNombre });

            modelBuilder.Entity<PruebaMatriz8>().HasData(new PruebaMatriz8
                {
                Nombre = "Caritas",
                Descripcion = "Test de percepcion de diferencias. Selecciona la carita única en cada trío",
                CantidadFilas = 15,
                CantColumnas = 4,
                TiempoLimiteMs = 180000
                });
            }

        public DbSet<Usuario1> Usuario { get; set; }
        public DbSet<Sexo2> Sexo { get; set; }
        public DbSet<GrupoEtario> GrupoEtario { get; set; }
        public DbSet<Centro4> Centro { get; set; }
        public DbSet<Escolaridad> Escolaridad { get; set; }
        public DbSet<UsuarioRol6> UsuarioRol { get; set; }
        public DbSet<Rol7> Rol { get; set; }
        public DbSet<PruebaMatriz8> PruebaMatriz { get; set; }
        public DbSet<Examen9> Examen { get; set; }
        public DbSet<UsuarioExamen10> UsuarioExamen { get; set; }
        }
    }
