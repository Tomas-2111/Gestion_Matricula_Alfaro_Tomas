using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMatricula.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20)]
        public string Cedula { get; set; }
  
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
