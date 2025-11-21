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
            int idEgresado = 1;

            string cvBase64 = "";

            if (fuCV.HasFile)
                cvBase64 = Convert.ToBase64String(fuCV.FileBytes);

            var postulacion = new AttributesApplications
            {
                IdOferta = idOferta,
                IdEgresado = idEgresado,
                FechaPostulacion = DateTime.Now,
                EstadoFinal = AttributesApplications.EstadoPostulacion.Postulado,
                Mensaje = "Postulación creada correctamente.",
                CvUrl = cvBase64
            };

            service.Agregar(postulacion);
            alertExito.Visible = true;
        }
    }
}
