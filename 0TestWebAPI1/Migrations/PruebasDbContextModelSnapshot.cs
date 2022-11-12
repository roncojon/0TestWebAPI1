﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _0TestWebAPI1.Data;

namespace _0TestWebAPI1.Migrations
{
    [DbContext(typeof(PruebasDbContext))]
    partial class PruebasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("_0TestWebAPI1.Models.Centro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Centro");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.Escolaridad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("NivelEscolar")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Escolaridad");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.GrupoEtario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Grupo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GrupoEtario");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.PruebaBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Anotaciones")
                        .HasColumnType("int");

                    b.Property<int>("Errores")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Filas")
                        .HasColumnType("int");

                    b.Property<int>("Intentos")
                        .HasColumnType("int");

                    b.Property<int>("Omisiones")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PruebaBase");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.PruebaDeCaritas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("CalidadDeLaAtencion")
                        .HasColumnType("float");

                    b.Property<double>("DatosAtencion")
                        .HasColumnType("float");

                    b.Property<double>("EficaciaAtencional")
                        .HasColumnType("float");

                    b.Property<double>("EficienciaAtencional")
                        .HasColumnType("float");

                    b.Property<double>("ICI")
                        .HasColumnType("float");

                    b.Property<double>("IGAP")
                        .HasColumnType("float");

                    b.Property<double>("PorCientoDeAciertos")
                        .HasColumnType("float");

                    b.Property<int>("PruebaBaseId")
                        .HasColumnType("int");

                    b.Property<double>("RendimientoAtencional")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PruebaBaseId");

                    b.ToTable("PruebaCaritas");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("NombreDelRol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ci")
                        .HasColumnType("int");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<int>("EscolaridadNombre")
                        .HasColumnType("int");

                    b.Property<int>("GrupoEtarioNombre")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolNombre")
                        .HasColumnType("int");

                    b.Property<bool>("SexoNombre")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EscolaridadNombre");

                    b.HasIndex("GrupoEtarioNombre");

                    b.HasIndex("RolNombre");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.UsuarioCentro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CentroId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CentroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioCentro");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.UsuarioPruebaBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PruebaBaseId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PruebaBaseId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioPruebaBase");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.PruebaDeCaritas", b =>
                {
                    b.HasOne("_0TestWebAPI1.Models.PruebaBase", "PruebaBase")
                        .WithMany()
                        .HasForeignKey("PruebaBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PruebaBase");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.Usuario", b =>
                {
                    b.HasOne("_0TestWebAPI1.Models.Escolaridad", "Escolaridad")
                        .WithMany()
                        .HasForeignKey("EscolaridadNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0TestWebAPI1.Models.GrupoEtario", "GrupoEtario")
                        .WithMany()
                        .HasForeignKey("GrupoEtarioNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0TestWebAPI1.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escolaridad");

                    b.Navigation("GrupoEtario");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.UsuarioCentro", b =>
                {
                    b.HasOne("_0TestWebAPI1.Models.Centro", "Centro")
                        .WithMany()
                        .HasForeignKey("CentroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0TestWebAPI1.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Centro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("_0TestWebAPI1.Models.UsuarioPruebaBase", b =>
                {
                    b.HasOne("_0TestWebAPI1.Models.PruebaBase", "PruebaBase")
                        .WithMany()
                        .HasForeignKey("PruebaBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0TestWebAPI1.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PruebaBase");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
