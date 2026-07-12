using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMatricula.Models
{
    public class MatriculaCurso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MatriculaId { get; set; }
        [ForeignKey("MatriculaId")]
        public Matricula Matricula { get; set; }

        [Required]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public virtual Curso Curso { get; set; } = null!;
    }
}
