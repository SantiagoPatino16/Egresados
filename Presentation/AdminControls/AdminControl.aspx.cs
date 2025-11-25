using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation.User
{
    public partial class AdminControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIrEventos_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Events/AddEvents.aspx");
        }

        protected void btnVerEmpresas_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdminControls/ValidateCompany.aspx");
        }

        protected void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdminControls/AccountManagement.aspx");
        }

        protected void btnValidarOfertas_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdminControls/AdminJobOffers.aspx");
        }
    }
}