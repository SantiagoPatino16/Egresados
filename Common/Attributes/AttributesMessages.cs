using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("Mensajes")]
    public class AttributesMessages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMensaje { get; set; }

        [Required]
        public int IdEmisor { get; set; }

        [Required]
        public int IdReceptor { get; set; }

        [Required]
        public string Contenido { get; set; }

        public DateTime FechaEnvio { get; set; }

        // Eliminación lógica por cada usuario
        public bool EliminadoPorEmisor { get; set; }
        public bool EliminadoPorReceptor { get; set; }

        // Editado
        public bool Editado { get; set; }
        public DateTime? FechaEdicion { get; set; }

        // Leído
        public bool Leido { get; set; }

        // Relaciones
        [ForeignKey("IdEmisor")]
        public virtual AttributesUser Emisor { get; set; }

        [ForeignKey("IdReceptor")]
        public virtual AttributesUser Receptor { get; set; }

        // Relación 1-N con reacciones
        public virtual ICollection<AttributesMessageReactions> Reacciones { get; set; }

        // Relación 1-N con adjuntos
        public virtual ICollection<AttributesMessageAttachments> Adjuntos { get; set; }
    }
}
