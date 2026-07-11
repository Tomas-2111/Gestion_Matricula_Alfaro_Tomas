using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMatricula.Models
{
    public class Matricula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaMatricula { get; set; } = DateTime.Now;

 
        [Required]
        public int EstudianteId { get; set; }
        [ForeignKey("EstudianteId")]
        public virtual Estudiante Estudiante { get; set; }


        [Required]
        public int CarreraId { get; set; }
        [ForeignKey("CarreraId")]
        public virtual Carrera Carrera { get; set; } = null!;


        [Required]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public virtual Curso Curso { get; set; } = null!;
    }
}
