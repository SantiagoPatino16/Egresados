using Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("OfertasFavoritas")]
    public class AttributesFavoritesJobs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFavorito { get; set; }

        public int IdEgresado { get; set; }
        public int IdOferta { get; set; }

        public DateTime FechaGuardado { get; set; } = DateTime.Now;

        // Relaciones
        public virtual AttributesUser Egresado { get; set; }
        public virtual AttributesJobOfferts Oferta { get; set; }
    }
}
