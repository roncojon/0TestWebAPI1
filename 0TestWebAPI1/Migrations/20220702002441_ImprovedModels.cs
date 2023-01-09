using Microsoft.EntityFrameworkCore.Migrations;

namespace _0TestWebAPI1.Migrations
{
    public partial class ImprovedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PruebaCaritas_PruebaBase_PruebaBaseId",
                table: "ResultadoDe1Examen");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Escolaridad_EscolaridadId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_GrupoEtario_GrupoEtarioId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Rol_RolNombre",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCentro_Usuario_SujetoId",
                table: "UsuarioCentro");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPruebaBase_PruebaBase_PruebaBaseId",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPruebaBase_Usuario_SujetoId",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioPruebaBase_SujetoId",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropColumn(
                name: "SujetoId",
                table: "UsuarioPruebaBase");

            migrationBuilder.RenameColumn(
                name: "SujetoId",
                table: "UsuarioCentro",
                newName: "UsuarioCi");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioCentro_SujetoId",
                table: "UsuarioCentro",
                newName: "IX_UsuarioCentro_UsuarioCi");

            migrationBuilder.AlterColumn<int>(
                name: "PruebaBaseId",
                table: "UsuarioPruebaBase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCi",
                table: "UsuarioPruebaBase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RolNombre",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GrupoEtarioNombre",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EscolaridadNombre",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PruebaBaseId",
                table: "ResultadoDe1Examen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPruebaBase_UsuarioCi",
                table: "UsuarioPruebaBase",
                column: "UsuarioCi");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebaCaritas_PruebaBase_PruebaBaseId",
                table: "ResultadoDe1Examen",
                column: "PruebaBaseId",
                principalTable: "PruebaBase",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Escolaridad_EscolaridadId",
                table: "Usuario",
                column: "EscolaridadNombre",
                principalTable: "Escolaridad",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_GrupoEtario_GrupoEtarioId",
                table: "Usuario",
                column: "GrupoEtarioNombre",
                principalTable: "GrupoEtario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Rol_RolNombre",
                table: "Usuario",
                column: "RolNombre",
                principalTable: "Rol",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCentro_Usuario_UsuarioCi",
                table: "UsuarioCentro",
                column: "UsuarioCi",
                principalTable: "Usuario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPruebaBase_PruebaBase_PruebaBaseId",
                table: "UsuarioPruebaBase",
                column: "PruebaBaseId",
                principalTable: "PruebaBase",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPruebaBase_Usuario_UsuarioCi",
                table: "UsuarioPruebaBase",
                column: "UsuarioCi",
                principalTable: "Usuario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PruebaCaritas_PruebaBase_PruebaBaseId",
                table: "ResultadoDe1Examen");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Escolaridad_EscolaridadId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_GrupoEtario_GrupoEtarioId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Rol_RolNombre",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCentro_Usuario_UsuarioCi",
                table: "UsuarioCentro");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPruebaBase_PruebaBase_PruebaBaseId",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPruebaBase_Usuario_UsuarioCi",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioPruebaBase_UsuarioCi",
                table: "UsuarioPruebaBase");

            migrationBuilder.DropColumn(
                name: "UsuarioCi",
                table: "UsuarioPruebaBase");

            migrationBuilder.RenameColumn(
                name: "UsuarioCi",
                table: "UsuarioCentro",
                newName: "SujetoId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioCentro_UsuarioCi",
                table: "UsuarioCentro",
                newName: "IX_UsuarioCentro_SujetoId");

            migrationBuilder.AlterColumn<int>(
                name: "PruebaBaseId",
                table: "UsuarioPruebaBase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SujetoId",
                table: "UsuarioPruebaBase",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RolNombre",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GrupoEtarioNombre",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaridadNombre",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PruebaBaseId",
                table: "ResultadoDe1Examen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPruebaBase_SujetoId",
                table: "UsuarioPruebaBase",
                column: "SujetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebaCaritas_PruebaBase_PruebaBaseId",
                table: "ResultadoDe1Examen",
                column: "PruebaBaseId",
                principalTable: "PruebaBase",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Escolaridad_EscolaridadId",
                table: "Usuario",
                column: "EscolaridadNombre",
                principalTable: "Escolaridad",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_GrupoEtario_GrupoEtarioId",
                table: "Usuario",
                column: "GrupoEtarioNombre",
                principalTable: "GrupoEtario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Rol_RolNombre",
                table: "Usuario",
                column: "RolNombre",
                principalTable: "Rol",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCentro_Usuario_SujetoId",
                table: "UsuarioCentro",
                column: "SujetoId",
                principalTable: "Usuario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPruebaBase_PruebaBase_PruebaBaseId",
                table: "UsuarioPruebaBase",
                column: "PruebaBaseId",
                principalTable: "PruebaBase",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPruebaBase_Usuario_SujetoId",
                table: "UsuarioPruebaBase",
                column: "SujetoId",
                principalTable: "Usuario",
                principalColumn: "UId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
