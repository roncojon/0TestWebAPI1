using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _0TestWebAPI1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Centro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escolaridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NivelEscolar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolaridad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoEtario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoEtario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PruebaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Filas = table.Column<int>(type: "int", nullable: false),
                    Intentos = table.Column<int>(type: "int", nullable: false),
                    Anotaciones = table.Column<int>(type: "int", nullable: false),
                    Errores = table.Column<int>(type: "int", nullable: false),
                    Omisiones = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebaBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ci = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<bool>(type: "bit", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    GrupoEtarioId = table.Column<int>(type: "int", nullable: true),
                    EscolaridadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Escolaridad_EscolaridadId",
                        column: x => x.EscolaridadId,
                        principalTable: "Escolaridad",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_GrupoEtario_GrupoEtarioId",
                        column: x => x.GrupoEtarioId,
                        principalTable: "GrupoEtario",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoDe1Examen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PruebaBaseId = table.Column<int>(type: "int", nullable: true),
                    IGAP = table.Column<double>(type: "float", nullable: false),
                    ICI = table.Column<double>(type: "float", nullable: false),
                    PorCientoDeAciertos = table.Column<double>(type: "float", nullable: false),
                    EficaciaAtencional = table.Column<double>(type: "float", nullable: false),
                    EficienciaAtencional = table.Column<double>(type: "float", nullable: false),
                    RendimientoAtencional = table.Column<double>(type: "float", nullable: false),
                    CalidadDeLaAtencion = table.Column<double>(type: "float", nullable: false),
                    DatosAtencion = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebaCaritas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PruebaCaritas_PruebaBase_PruebaBaseId",
                        column: x => x.PruebaBaseId,
                        principalTable: "PruebaBase",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCentro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SujetoId = table.Column<int>(type: "int", nullable: false),
                    CentroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCentro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioCentro_Centro_CentroId",
                        column: x => x.CentroId,
                        principalTable: "Centro",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCentro_Usuario_SujetoId",
                        column: x => x.SujetoId,
                        principalTable: "Usuario",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPruebaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SujetoId = table.Column<int>(type: "int", nullable: true),
                    PruebaBaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPruebaBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPruebaBase_PruebaBase_PruebaBaseId",
                        column: x => x.PruebaBaseId,
                        principalTable: "PruebaBase",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioPruebaBase_Usuario_SujetoId",
                        column: x => x.SujetoId,
                        principalTable: "Usuario",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PruebaCaritas_PruebaBaseId",
                table: "ResultadoDe1Examen",
                column: "PruebaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EscolaridadId",
                table: "Usuario",
                column: "EscolaridadNombre");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GrupoEtarioId",
                table: "Usuario",
                column: "GrupoEtarioNombre");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCentro_CentroId",
                table: "UsuarioCentro",
                column: "CentroId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCentro_SujetoId",
                table: "UsuarioCentro",
                column: "SujetoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPruebaBase_PruebaBaseId",
                table: "UsuarioPruebaBase",
                column: "PruebaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPruebaBase_SujetoId",
                table: "UsuarioPruebaBase",
                column: "SujetoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultadoDe1Examen");

            migrationBuilder.DropTable(
                name: "UsuarioCentro");

            migrationBuilder.DropTable(
                name: "UsuarioPruebaBase");

            migrationBuilder.DropTable(
                name: "Centro");

            migrationBuilder.DropTable(
                name: "PruebaBase");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Escolaridad");

            migrationBuilder.DropTable(
                name: "GrupoEtario");
        }
    }
}
