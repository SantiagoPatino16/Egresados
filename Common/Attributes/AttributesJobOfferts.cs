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
    [Table("OfertasEmpleo")]
    public class AttributesJobOfferts
    {
        //El enum EstadoOferta representa los posibles estados de una oferta de empleo.
        public enum EstadoOferta
        {
            Activa = 1,
            Cerrada = 2,
            Suspendida = 3,
            PorVerificar = 4,
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Claves
        public int IdOferta { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoContrato { get; set; }
        public int IdModalidadTrabajo { get; set; }
        public int IdCategoria { get; set; }
        public int IdPostulacion { get; set; }
        public int IdFavorito { get; set; }



        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public decimal Salario { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public EstadoOferta Estado { get; set; }

        // Relaciones
        public virtual ICollection<AttributesApplications> Postulaciones { get; set; }
        public virtual AttributesUser Empresa { get; set; }
        public virtual AttributesContractType TipoContrato { get; set; }

        public virtual AttributesCategoriesJobs CategoriasEmpleo { get; set; }

        public virtual ICollection<AttributesFavoritesJobs> FavoritosEmpleo { get; set; }
        public virtual AttributesWorkModalities ModalidadesTrabajo { get; set; }
    }
}
