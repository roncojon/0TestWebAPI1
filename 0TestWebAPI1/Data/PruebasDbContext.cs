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

            Guid rolExaminado = Guid.NewGuid();
            Guid rolAdministrador = Guid.NewGuid();
            Guid rolExaminador = Guid.NewGuid();
            modelBuilder.Entity<Rol7>().HasData(new Rol7[] {
                new Rol7 {UId=rolExaminado , Nombre ="EXAMINADO" },
                new Rol7 {UId=rolAdministrador ,  Nombre ="ADMINISTRADOR" },
                new Rol7 {UId=rolExaminador ,  Nombre ="EXAMINADOR" }
            });

            Guid geMuyJoven = Guid.NewGuid();
            Guid geJoven = Guid.NewGuid();
            Guid geMedio = Guid.NewGuid();
            Guid geMayor = Guid.NewGuid();
            Guid geMuyMayor = Guid.NewGuid();
            modelBuilder.Entity<GrupoEtario>().HasData(new GrupoEtario[] {
                new GrupoEtario {UId=geMuyJoven,Nombre="Muy joven", EdadMinima=0, EdadMaxima=11},
                new GrupoEtario {UId=geJoven,Nombre="Joven", EdadMinima=12, EdadMaxima=18},
                new GrupoEtario {UId=geMedio,Nombre="Medio", EdadMinima=19, EdadMaxima=30},
                new GrupoEtario {UId=geMayor,Nombre="Mayor", EdadMinima=31, EdadMaxima=60},
                new GrupoEtario {UId=geMuyMayor,Nombre="Muy mayor", EdadMinima=61, EdadMaxima=200}
            });

            Guid eBasico = Guid.NewGuid();
            Guid eMedio = Guid.NewGuid();
            Guid eSuperior = Guid.NewGuid();
            modelBuilder.Entity<Escolaridad>().HasData(new Escolaridad[] {
                new Escolaridad { UId=eBasico, Nombre="Básico"},
                new Escolaridad { UId=eMedio, Nombre="Medio Superior"},
                new Escolaridad { UId=eSuperior, Nombre="Superior"},
            });

            /*Guid centroGuiteras = Guid.NewGuid();
            Guid centroUmcc = Guid.NewGuid();
             modelBuilder.Entity<Centro4>().HasData(new Centro4[] {
                 new Centro4 {UId = centroGuiteras, Nombre="CTE Antonio Guiteras"},
                 new Centro4 {UId =centroUmcc, Nombre="UMCC"},
             });*/

            Guid sMasculino = Guid.NewGuid();
            Guid sFemenino = Guid.NewGuid();
            modelBuilder.Entity<Sexo2>().HasData(new Sexo2[] {
                new Sexo2 {UId=sMasculino, Nombre="Masculino"},
                new Sexo2 {UId=sFemenino, Nombre="Femenino"},
            });

            /*Guid userAdminId = Guid.NewGuid();
            Guid userExaminadorId = Guid.NewGuid();
            Guid userExaminadoId = Guid.NewGuid();
            Guid userAdminId2 = Guid.NewGuid();
            Guid userExaminadorId2 = Guid.NewGuid();
            Guid userExaminadoId2 = Guid.NewGuid();*/
            modelBuilder.Entity<Usuario1>().HasData(new Usuario1[]
            {
               new Usuario1
               {
                   // UId = userAdminId,
                   Nombre = "Admin",
                   Apellidos = "Admin",
                   // // Ci = "Admin",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   // RolNombre = "ADMINISTRADOR",
                   Ci = "94052920987",
                   SexoUId = sMasculino,
                   // Edad = 37,
                   GrupoEtarioUId = geMedio,
                   EscolaridadUId = eBasico,
                   // Centro4Id = centroGuiteras
               },
                new Usuario1
               {
                    // UId = userExaminadorId,
                   Nombre = "Examinador",
                   Apellidos = "Examinador",
                   // // Ci = "Examinador",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   // RolNombre = "EXAMINADOR",
                   Ci = "86060670067",
                   SexoUId = sMasculino,
                   // Edad = 36,
                   GrupoEtarioUId = geMayor,
                   EscolaridadUId = eSuperior,
                   // Centro4Id = centroGuiteras
               },
                new Usuario1
               {
                    // UId = userExaminadoId,
                   Nombre = "Examinado",
                   Apellidos = "Examinado",
                   // Ci = "Examinado",
                   Password = SecurePasswordHasherHelper.Hash("ExaminadoMaster.10*"),
                   // RolNombre = "EXAMINADO",
                   Ci = "96060670065",
                   SexoUId = sMasculino,
                   // Edad = 36,
                   GrupoEtarioUId = geMedio,
                   EscolaridadUId = eSuperior,
                   // Centro4Id = centroUmcc
               },
                new Usuario1
               {
                    // UId = userAdminId2,
                   Nombre = "Juan",
                   Apellidos = "Martinez",
                   // Ci = "JuanMartinez",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   // RolNombre = "ADMINISTRADOR",
                   Ci = "85050560659",
                   SexoUId = sMasculino,
                   // Edad = 37,
                   GrupoEtarioUId = geMayor,
                   EscolaridadUId = eBasico,
               },
                new Usuario1
               {
                    // UId = userExaminadorId2,
                   Nombre = "Alberto",
                   Apellidos = "Perez",
                   // Ci = "AlbertoPerez",
                   Password = SecurePasswordHasherHelper.Hash("PsychoMaster.10*"),
                   // RolNombre = "EXAMINADOR",
                   Ci = "76060670067",
                   SexoUId = sMasculino,
                   // Edad = 36,
                   GrupoEtarioUId = geMayor,
                   EscolaridadUId = eSuperior,
               },
                new Usuario1
               {
                    // UId = userExaminadoId2,
                   Nombre = "Alberta",
                   Apellidos = "Perez",
                   // Ci = "AlbertaPerez",
                   Password = SecurePasswordHasherHelper.Hash("ExaminadoMaster.10*"),
                   // RolNombre = "EXAMINADO",
                   Ci = "76060670065",
                   SexoUId = sFemenino,
                   // Edad = 36,
                   GrupoEtarioUId = geMayor,
                   EscolaridadUId = eSuperior,
               }
               });
            modelBuilder.Entity<UsuarioExamen10>()
       .HasKey(nameof(UsuarioExamen10.UsuarioCi), nameof(UsuarioExamen10.ExamenId), nameof(UsuarioExamen10.Fecha) );

            modelBuilder.Entity<UsuarioRol6>()
       .HasKey(u => new { u.UsuarioCi, u.RolUId });

            modelBuilder.Entity<Test>().HasData(new Test
                {
                UId = Guid.NewGuid(),
                Nombre = "Caritas",
                Descripcion = "Test de percepción de diferencias. Selecciona la carita única en cada trío",
                CantidadFilas = 15,
                CantColumnas = 4,
                TiempoLimiteMs = 180000,
                PatronOriginal = "i1 3,i2 2,i3 2,i4 2,i5 3,i6 3,i7 2,i8 3,i9 1,i10 1,i11 1,i12 3,i13 2,i14 1,i15 2,i16 2,i17 2,i18 2,i19 3,i20 1,i21 2,i22 2,i23 1,i24 1,i25 1,i26 1,i27 1,i28 3,i29 1,i30 3,i31 2,i32 2,i33 1,i34 1,i35 2,i36 3,i37 1,i38 1,i39 2,i40 3,i41 2,i42 2,i43 3,i44 3,i45 3,i46 2,i47 2,i48 1,i49 1,i50 1,i51 1,i52 2,i53 2,i54 3,i55 2,i56 3,i57 3,i58 1,i59 2,i60 1,"
            });

            modelBuilder.Entity<UsuarioRol6>().HasData(new UsuarioRol6[] {
                new UsuarioRol6 { UsuarioCi="94052920987", RolUId = rolAdministrador},
                new UsuarioRol6 { UsuarioCi="94052920987", RolUId =rolExaminador},
                new UsuarioRol6 { UsuarioCi="94052920987", RolUId =rolExaminado},
               
                new UsuarioRol6 {UsuarioCi="86060670067", RolUId =rolExaminador},
                new UsuarioRol6 {UsuarioCi="96060670065", RolUId=rolExaminador},
                new UsuarioRol6 {UsuarioCi="85050560659", RolUId=rolExaminado},
                new UsuarioRol6 {UsuarioCi="76060670067", RolUId=rolExaminado},
                new UsuarioRol6 {UsuarioCi="76060670065", RolUId=rolExaminado},
            });
            }
       
public DbSet<Usuario1> Usuario { get; set; }
        public DbSet<Sexo2> Sexo { get; set; }
        public DbSet<GrupoEtario> GrupoEtario { get; set; }
        // public DbSet<Centro4> Centro { get; set; }
        public DbSet<Escolaridad> Escolaridad { get; set; }
        public DbSet<UsuarioRol6> UsuarioRol { get; set; }
        public DbSet<Rol7> Rol { get; set; }
        // public DbSet<Fecha> Fecha { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Examen9> Examen { get; set; }
        // public DbSet<ExamenTerminado> ExamenTerminado { get; set; }
        public DbSet<UsuarioExamen10> UsuarioExamen { get; set; }
        }
    }
