using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMatricula.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Matriculas");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Matriculas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarreraId",
                table: "Cursos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MatriculaCurso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatriculaId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatriculaCurso_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaCurso_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_CarreraId",
                table: "Cursos",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaCurso_CursoId",
                table: "MatriculaCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaCurso_MatriculaId",
                table: "MatriculaCurso",
                column: "MatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas");

            migrationBuilder.DropTable(
                name: "MatriculaCurso");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_CarreraId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Cursos");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Matriculas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Matriculas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Carreras_CarreraId",
                table: "Matriculas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
