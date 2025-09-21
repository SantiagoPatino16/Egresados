using LogicBusiness.Service;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.UI;

namespace Presentation
{
    public partial class Perfil : System.Web.UI.Page
    {
        private UserService _userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userService = new UserService();
            if (!IsPostBack)
                CargarPerfil();
        }

        private void CargarPerfil()
        {
            var usuario = _userService.ObtenerUsuarioPorId(Convert.ToInt32(Session["IdUsuario"]));

            lblNombre.Text = usuario.NombreCompleto;
            lblCorreo.Text = usuario.Correo;
            lblRol.Text = usuario.Rol;
            lblFechaRegistro.Text = usuario.FechaRegistro.ToString("dd/MM/yyyy");
            lblActivo.Text = usuario.Activo ? "Sí" : "No";

            if (!string.IsNullOrEmpty(usuario.FotoPerfil))
                imgFotoPerfil.ImageUrl = usuario.FotoPerfil;
        }

        protected void btnSubirFoto_Click(object sender, EventArgs e)
        {

        }
    }
}
