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


        public  ICollection<MatriculaCurso> MatriculaCursos { get; set; } = new List<MatriculaCurso>();
    }
}
