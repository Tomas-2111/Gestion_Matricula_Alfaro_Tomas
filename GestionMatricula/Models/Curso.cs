using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMatricula.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los creditos del curso son obligatorios.")]
        [Range(1, 5, ErrorMessage = "Los créditos deben estar entre 1 y 5.")]
        public int Creditos { get; set; }

        public int? ProfesorId { get; set; }

        [ForeignKey("ProfesorId")]
        public Profesor? Profesor { get; set; }

        [Required(ErrorMessage = "La carrera del curso es obligatoria.")]
        public int CarreraId { get; set; }

        [ForeignKey("CarreraId")]
        public virtual Carrera Carrera { get; set; } = null!;

        public ICollection<MatriculaCurso> MatriculaCursos { get; set; } = new List<MatriculaCurso>();
    }
}
