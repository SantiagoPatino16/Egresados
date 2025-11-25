using LogicBusiness.Service;
using System;

namespace Presentation.JobBoardList
{
    public partial class FavoritesJobs : System.Web.UI.Page
    {
        private readonly FavoritesJobsService _favService = new FavoritesJobsService();
        private readonly JobOffertsService _jobService = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFavoritos();
            }
        }

        private void CargarFavoritos()
        {
            if (Session["IdEgresado"] == null)
            {
                Response.Redirect("../Login.aspx");
                return;
            }

            int idEgresado = int.Parse(Session["IdEgresado"].ToString());

            // Traer la lista de favoritos
            var favoritos = _favService.ListarPorEgresado(idEgresado);

            // Importante: incluir los datos de la oferta
            foreach (var fav in favoritos)
            {
                fav.Oferta = _jobService.ObtenerPorId(fav.IdOferta);
            }

            rptFavoritos.DataSource = favoritos;
            rptFavoritos.DataBind();
        }
    }
}
