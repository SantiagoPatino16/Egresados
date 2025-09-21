using System;
using LogicBusiness.Service; 


namespace Presentation
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Evitar que se recargue con postback sin necesidad
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                MostrarMensaje("Por favor complete todos los campos.");
                return;
            }

            var usuario = _userService.ValidarUsuario(correo, clave);

            if (usuario != null && usuario.Activo)
            {
                // Guardamos datos de sesión
                Session["IdUsuario"] = usuario.IdUsuario;
                Session["NombreCompleto"] = usuario.NombreCompleto;
                Session["Rol"] = usuario.Rol;

                // Redirigir a el foro de publicaciones
                Response.Redirect("../Forum/Publications.aspx");
            }
            else
            {
                MostrarMensaje("Credenciales incorrectas o usuario inactivo.");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            // Esto crea un alert en JS
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
