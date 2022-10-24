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
            modelBuilder.Entity<GrupoEtario3>().HasData(new GrupoEtario3[] {
                new GrupoEtario3 {Nombre="Joven", Nivel = 1, EdadMinima=12, EdadMaxima=18},
                new GrupoEtario3 {Nombre="Medio", Nivel = 2, EdadMinima=19, EdadMaxima=30},
                new GrupoEtario3 {Nombre="Mayor", Nivel = 1, EdadMinima=31, EdadMaxima=60}
            });
            modelBuilder.Entity<Escolaridad5>().HasData(new Escolaridad5[] {
                new Escolaridad5 { Nivel = 1, Nombre="Básico"},
                new Escolaridad5 { Nivel = 2, Nombre="Medio Superior"},
                new Escolaridad5 { Nivel = 3, Nombre="Superior"},
            });
            modelBuilder.Entity<Centro4>().HasData(new Centro4[] {
                new Centro4 {Id = Guid.NewGuid(), Nombre="CTE Antonio Guiteras"},
                new Centro4 {Id = Guid.NewGuid(), Nombre="UMCC"},
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
               }
               });
            modelBuilder.Entity<UsuarioExamen10>()
       .HasKey(u => new { u.UsuarioId, u.ExamenId });

            modelBuilder.Entity<UsuarioRol6>()
       .HasKey(u => new { u.UsuarioId, u.RolNombre });

            /*modelBuilder.Entity<ExamenPruebaMatriz>()
      .HasKey(e => new
      {
          e.ExamenId,
          e.PruebaMatrizId
      });*/
            modelBuilder.Entity<PruebaMatriz8>().HasData(new PruebaMatriz8
            {
                Nombre = "Caritas",
                Descripcion="Test de percepcion de diferencias. Selecciona la carita única en cada trío",
                CantidadFilas=15,
                CantColumnas=4,
                TiempoLimiteMs= 180000
            });
      }
    /*        modelBuilder.Entity<UsuarioCentro>().HasData(new UsuarioCentro[] {
                new UsuarioCentro { Id = 1, UsuarioId=1,CentroId=1 },
                new UsuarioCentro { Id = 2, UsuarioId=2,CentroId=1 },
                new UsuarioCentro { Id = 3, UsuarioId=1,CentroId=2 },
            });

                for (int i = 1; i < 61; i++) {
                    modelBuilder.Entity<Position>().HasData(new Position { Id = i });
                }
            modelBuilder.Entity<AtomicResult>().HasData(new AtomicResult[] {
                    new AtomicResult {Id = 1, Value = "Anotado"},
                    new AtomicResult {Id = 2, Value = "Error"},
                    new AtomicResult {Id = 3, Value = "Omitido"},
                    new AtomicResult {Id = 4, Value = "Restante"}
                });
        }*/

        public DbSet<Usuario1> Usuario { get; set; }
        public DbSet<Sexo2> Sexo { get; set; }
        public DbSet<GrupoEtario3> GrupoEtario { get; set; }
        public DbSet<Centro4> Centro { get; set; }
        public DbSet<Escolaridad5> Escolaridad { get; set; }
        public DbSet<UsuarioRol6> UsuarioRol { get; set; }
        // public DbSet<ExamenPruebaMatriz> ExamenPruebaMatriz { get; set; }

        public DbSet<Rol7> Rol { get; set; }
        public DbSet<PruebaMatriz8> PruebaMatriz { get; set; }

        // public DbSet<UsuarioCentro> UsuarioCentro { get; set; }

        // public DbSet<PruebaBase> PruebaBase { get; set; }

        // public  DbSet<Fila> Fila { get; set; }
        // public DbSet<PruebaDeCaritas> PruebaCaritas { get; set; }
        // public DbSet<PruebaToulosePieron> PruebaToulosePieron { get; set; }
        public DbSet<Examen9> Examen { get; set; }

        //public DbSet<PruebaSimbolos> PruebaSimbolos { get; set; }

        public DbSet<UsuarioExamen10> UsuarioExamen { get; set; }

     /*   public DbSet<PruebaEnMatriz> PruebaEnMatriz { get; set; }
        public DbSet<FilaDeMatriz> FilaDeMatriz { get; set; }
        public DbSet<AtomicResultObjectPosVal> AtomicResultObjectPosVal { get; set; }
        public DbSet<AtomicResult> AtomicResult { get; set; }
        public DbSet<Position> Position { get; set; }*/

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Centro>().HasData(new Centro { Id = 97, Nombre = "TermoelectricaUnidad2" });




        //}
    }
}
