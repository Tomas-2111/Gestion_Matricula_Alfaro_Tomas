using System.ComponentModel.DataAnnotations;

namespace GestionMatricula.Models
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La especialidad es obligatoria")]
        [StringLength(100)]
        public string Especialidad { get; set; } 

        [Required(ErrorMessage = "El grado academico es obligatoriO")]
        [StringLength(100)]
        public string GradoAcademico { get; set; } 

        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
