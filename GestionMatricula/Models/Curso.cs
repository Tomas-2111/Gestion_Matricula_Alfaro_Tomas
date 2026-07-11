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

        [Required]
        [Range(1, 5, ErrorMessage = "Los créditos deben estar entre 1 y 5.")]
        public int Creditos { get; set; }

        public int? ProfesorId { get; set; }

        [ForeignKey("ProfesorId")]
        public Profesor? Profesor { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
