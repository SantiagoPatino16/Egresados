using Common.Attributes;
using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Web.UI;

namespace Presentation
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                lblMensaje.Text = "Por favor complete todos los campos.";
                return;
            }

            var usuario = _userService.ValidarUsuario(correo, clave);

            if (usuario != null && usuario.Activo)
            {
                Session["IdUsuario"] = usuario.IdUsuario;
                Session["NombreCompleto"] = usuario.Nombre;
                Session["Rol"] = usuario.Rol;
                Session["Verificado"] = usuario.Verificado;

                // Si es empresa
                if (usuario.Rol == "Empresa")
                {
                    Session["IdEmpresa"] = usuario.IdUsuario;
                }

                // Si es egresado, guarda su Id
                if (usuario.Rol == "Egresado")
                {
                    Session["IdEgresado"] = usuario.IdUsuario;
                }

                if (usuario.Rol == "Empresa" && usuario.Verificado == false)
                {
                    lblMensaje.Text = "Su cuenta aún no ha sido verificada.";
                    return;
                }

                Response.Redirect("../Inicio/Home.aspx");
            }

            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }

    }
}
