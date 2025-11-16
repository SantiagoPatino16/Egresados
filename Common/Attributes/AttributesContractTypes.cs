using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("TiposContrato")]
    public class AttributesContractType
    {
        public enum TipoContratoEnum
        {
            TerminoFijo = 1,
            TerminoIndefinido = 2,
            ObraLabor = 3
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Clave primaria
        public int IdTipoContrato { get; set; }

        // Tipo de contrato (enum)
        public TipoContratoEnum TipoContrato { get; set; }

        // Relación 
        public virtual ICollection<AttributesJobOfferts> Ofertas { get; set; }
    }
}
