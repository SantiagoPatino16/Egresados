using System;
using LogicBusiness.Service;
using Common.Attributes;

namespace Presentation.JobBoardList
{
    public partial class ApplyJob : System.Web.UI.Page
    {
        ApplicationsService service = new ApplicationsService();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnPostular_Click(object sender, EventArgs e)
        {
            int idOferta = Convert.ToInt32(Request.QueryString["id"]);
            int idEgresado = Convert.ToInt32(Session["IdUsuario"]);

            string rutaRelativaCV = "";

            if (fuCV.HasFile)
            {
                // Validar que el archivo sea PDF
                if (System.IO.Path.GetExtension(fuCV.FileName).ToLower() != ".pdf")
                {
                    // Aquí puedes mostrar un mensaje de error si deseas
                    return;
                }

                // Crear nombre único para evitar sobreescrituras
                string fileName = $"CV_{idEgresado}_{DateTime.Now.Ticks}.pdf";

                // Ruta física en el servidor
                string path = Server.MapPath("~/Resources/CV/" + fileName);

                // Guardar el archivo en el servidor
                fuCV.SaveAs(path);

                // Guardamos solo la ruta que se irá a la BD
                rutaRelativaCV = "/Resources/CV/" + fileName;
            }

            var postulacion = new AttributesApplications
            {
                IdOferta = idOferta,
                IdEgresado = idEgresado,
                FechaPostulacion = DateTime.Now,
                EstadoFinal = AttributesApplications.EstadoPostulacion.Postulado,
                Mensaje = "Postulación creada correctamente.",

                // Enviar solo la ruta del archivo, no base64
                CvUrl = rutaRelativaCV
            };

            service.Agregar(postulacion);
            alertExito.Visible = true;
        }

    }
}
