using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMatricula.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMatriculasCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatriculaCurso_Cursos_CursoId",
                table: "MatriculaCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_MatriculaCurso_Matriculas_MatriculaId",
                table: "MatriculaCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatriculaCurso",
                table: "MatriculaCurso");

            migrationBuilder.RenameTable(
                name: "MatriculaCurso",
                newName: "MatriculasCursos");

            migrationBuilder.RenameIndex(
                name: "IX_MatriculaCurso_MatriculaId",
                table: "MatriculasCursos",
                newName: "IX_MatriculasCursos_MatriculaId");

            migrationBuilder.RenameIndex(
                name: "IX_MatriculaCurso_CursoId",
                table: "MatriculasCursos",
                newName: "IX_MatriculasCursos_CursoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatriculasCursos",
                table: "MatriculasCursos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculasCursos_Cursos_CursoId",
                table: "MatriculasCursos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculasCursos_Matriculas_MatriculaId",
                table: "MatriculasCursos",
                column: "MatriculaId",
                principalTable: "Matriculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatriculasCursos_Cursos_CursoId",
                table: "MatriculasCursos");

            migrationBuilder.DropForeignKey(
                name: "FK_MatriculasCursos_Matriculas_MatriculaId",
                table: "MatriculasCursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatriculasCursos",
                table: "MatriculasCursos");

            migrationBuilder.RenameTable(
                name: "MatriculasCursos",
                newName: "MatriculaCurso");

            migrationBuilder.RenameIndex(
                name: "IX_MatriculasCursos_MatriculaId",
                table: "MatriculaCurso",
                newName: "IX_MatriculaCurso_MatriculaId");

            migrationBuilder.RenameIndex(
                name: "IX_MatriculasCursos_CursoId",
                table: "MatriculaCurso",
                newName: "IX_MatriculaCurso_CursoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatriculaCurso",
                table: "MatriculaCurso",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculaCurso_Cursos_CursoId",
                table: "MatriculaCurso",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculaCurso_Matriculas_MatriculaId",
                table: "MatriculaCurso",
                column: "MatriculaId",
                principalTable: "Matriculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
