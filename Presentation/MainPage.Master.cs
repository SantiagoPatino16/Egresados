using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AsignarNombreUsuario();
        }
        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();      // Limpia todos los valores de la sesión
            Session.Abandon(); 
            Response.Redirect("~/Start/Login.aspx"); // Redirige a la página de login
        }

        private void AsignarNombreUsuario()
        {
            if (Session["NombreCompleto"] != null)
            {
                lblUsuario.Text = Session["NombreCompleto"].ToString();
            }
            else
            {
                lblUsuario.Text = "Invitado";
            }
        }
    }
}