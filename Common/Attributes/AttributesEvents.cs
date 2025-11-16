using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    [Table("Eventos")]
    public class AttributesEvents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEvento { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCreacion { get; set; } //Almacenar la fecha de creación del evento

        public DateTime? FechaFin { get; set; }

        [StringLength(100)]
        public string Lugar { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } // Ej: conferencia, taller, reunión, etc.

        [StringLength(100)]
        public string Organizador { get; set; }

        public bool Activo { get; set; } = true;

        public string RutaImagen { get; set; } //propiedad para la ruta de la imagen
    }
}
