using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("CategoriasEmpleo")]
    public class AttributesCategoriesJobs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }

        // Relación
        public virtual ICollection<AttributesJobOfferts> Ofertas { get; set; }

    }
}
