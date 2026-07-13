using System.ComponentModel.DataAnnotations;

namespace GestionMatricula.Models
{
    public class Carrera
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string TipoCarrera { get; set; }

    }
}
