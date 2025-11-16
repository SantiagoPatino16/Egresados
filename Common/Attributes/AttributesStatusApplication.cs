using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Attributes
{
    [Table("EstadoPostulacion")]
    public class AttributesStatusApplication
    {
        public enum EstadoPostulacionEnum
        {
            Postulado = 1,
            CvVista = 2,
            EnProceso = 3,
            Finalizado = 4
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstadoPostulacion { get; set; }

        public int IdPostulacion { get; set; }

        public EstadoPostulacionEnum Estado { get; set; }

        public DateTime FechaPostulacion { get; set; } = DateTime.Now;

        // Relación con la entidad de postulaciones
        public virtual AttributesApplications Postulacion { get; set; }
    }
}
