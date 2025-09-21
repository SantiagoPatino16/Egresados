using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    [Table("Comentarios")]
    public class AttributesComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComentario { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdComentarioPadre { get; set; }

        public virtual AttributesUser Usuario { get; set; }
        public virtual AttributesPublications Publicacion { get; set; }

        // Para anidar respuestas
        public List<AttributesComments> Respuestas { get; set; } = new List<AttributesComments>();
    }
}
