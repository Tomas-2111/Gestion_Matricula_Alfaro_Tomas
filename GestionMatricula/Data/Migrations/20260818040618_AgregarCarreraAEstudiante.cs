using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMatricula.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCarreraAEstudiante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarreraId",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CarreraId",
                table: "Estudiantes",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Carreras_CarreraId",
                table: "Estudiantes",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Carreras_CarreraId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_CarreraId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Estudiantes");
        }
    }
}
