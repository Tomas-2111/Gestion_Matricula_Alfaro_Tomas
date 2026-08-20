using GestionMatricula.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionMatricula.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<MatriculaCurso> MatriculasCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Carrera)
                .WithMany(c => c.Estudiantes)
                .HasForeignKey(e => e.CarreraId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
