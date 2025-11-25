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
    [Table("Postulaciones")]
    public class AttributesApplications
    {
        public enum EstadoPostulacion
        {
            Aceptada = 1,
            Rechazada = 2,
            Postulado = 3
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPostulacion { get; set; }
        public int IdOferta { get; set; }
        public int IdEgresado { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public EstadoPostulacion EstadoFinal { get; set; }
        public string Mensaje { get; set; }
        public string CvUrl { get; set; }

        // Relaciones

        public virtual AttributesJobOfferts OfertaEmpleo { get; set; }
        public virtual AttributesUser Egresado { get; set; }
    }
}