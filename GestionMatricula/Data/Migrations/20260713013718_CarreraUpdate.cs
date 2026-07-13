using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMatricula.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarreraUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_CarreraId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Matriculas");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Carreras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoCarrera",
                table: "Carreras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "TipoCarrera",
                table: "Carreras");

            migrationBuilder.AddColumn<int>(
                name: "CarreraId",
                table: "Matriculas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CarreraId",
                table: "Matriculas",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }
    }
}
