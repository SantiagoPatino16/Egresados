using Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("ReaccionesMensaje")]
    public class AttributesMessageReactions
    {
        public enum ReactionsMessages
        {
            MeGusta = 1,
            Corazon = 2,
            Risa = 3,
            Asombrado = 4,
            Enojo = 5
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReaccion { get; set; }

        public int IdMensaje { get; set; }
        public int IdUsuario { get; set; }

        public ReactionsMessages TipoReaccion { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("IdMensaje")]
        public virtual AttributesMessages Mensaje { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual AttributesUser Usuario { get; set; }
    }
}
