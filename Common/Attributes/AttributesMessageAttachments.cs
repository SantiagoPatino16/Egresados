using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("AdjuntosMensaje")]
    public class AttributesMessageAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdjunto { get; set; }

        public int IdMensaje { get; set; }

        public string RutaArchivo { get; set; }

        public string TipoMime { get; set; }

        public DateTime FechaAdjunto { get; set; }

        [ForeignKey("IdMensaje")]
        public virtual AttributesMessages Mensaje { get; set; }
    }
}
