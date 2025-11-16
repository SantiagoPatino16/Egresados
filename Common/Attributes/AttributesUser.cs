using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("Usuarios")]
    public class AttributesUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        // Información básica
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string ClaveHash { get; set; }
        public string Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public string FotoPerfil { get; set; }

        // Información académica
        public string ProgramaAcademico { get; set; }
        public int AnioGraduacion { get; set; }

        // Información profesional
        public string EmpresaActual { get; set; }
        public string CargoActual { get; set; }
        public string CiudadResidencia { get; set; }

        // Enlaces y redes
        public string LinkedIn { get; set; }
        public string SitioWebPersonal { get; set; }

        //Datos empresa (solo se llenan cuando el Rol sea "Empresa")
        public string Nit { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string UbicacionEmpresa { get; set; }
        public string PersonaRepresentante { get; set; }
        public string CargoRepresentante { get; set; }
        public string TelefonoContacto { get; set; }
        public string SectorIndustria { get; set; }
        public string CiudadEmpresa { get; set; }

        // Actividad
        public DateTime? UltimaConexion { get; set; }

        // Descripción libre
        public string Biografia { get; set; }

        //Estado de verificación de empresa.
        public bool Verificado { get; set; } = true;

        //Relación a tabla de postulaciones
        public virtual ICollection<AttributesApplications> Postulaciones { get; set; }

        //Relación a tabla de ofertas de empleo (si es empresa)
        public virtual ICollection<AttributesJobOfferts> OfertasEmpleo { get; set; }

        //Mensajería 
        public virtual ICollection<AttributesMessages> MensajesEnviados { get; set; }
        public virtual ICollection<AttributesMessages> MensajesRecibidos { get; set; }
        public virtual ICollection<AttributesMessageReactions> Reacciones { get; set; }


    }

}
