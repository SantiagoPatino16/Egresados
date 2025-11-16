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
    [Table("Publicaciones")]
    public class AttributesPublications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } 
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }

        // FK a Categoría predefiniendo la entrada de la publicaciones a categoría "general" (IdCategoria = 1)
        public int? IdCategoria { get; set; }

        // Relación
        public virtual AttributesCategories Categoria{ get; set; }
        public virtual AttributesUser Usuario { get; set; }
        public virtual ICollection<AttributesComments> Comentarios { get; set; }
    }
}
