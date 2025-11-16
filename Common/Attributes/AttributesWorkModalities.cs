using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("ModalidadesTrabajo")]
    public class AttributesWorkModalities
    {
        public enum ModalidadTrabajoEnum
        {
            Presencial = 1,
            Remoto = 2,
            Hibrido = 3
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModalidadTrabajo { get; set; }

        public ModalidadTrabajoEnum Modalidad { get; set; }

        // Relación inversa con ofertas
        public virtual ICollection<AttributesJobOfferts> Ofertas { get; set; }
    }
}
