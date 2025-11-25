using Common.Attributes;
using LogicBusiness.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Presentation.Messaging
{
    public partial class Inbox : System.Web.UI.Page
    {
        private readonly MessagesService _messagesService;
        private readonly UserService _userService;

        public Inbox()
        {
            _messagesService = new MessagesService();
            _userService = new UserService();
        }
        MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios(string filtro = "")
        {
            int usuarioId = ObtenerUsuarioLogueadoId();
            var ultimosMensajes = _messagesService.ObtenerUltimosMensajes(usuarioId);

            // Obtener IDs de usuarios distintos con los que se tiene conversación
            var usuariosId = ultimosMensajes
                .Select(m => m.IdEmisor == usuarioId ? m.IdReceptor : m.IdEmisor)
                .Distinct()
                .ToList();

            // Traer datos de usuario solo de esos IDs
            var usuarios = _userService.ObtenerUsuariosPorIds(usuariosId);

            if (!string.IsNullOrEmpty(filtro))
            {
                usuarios = usuarios
                    .Where(u => u.Nombre.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            rptUsuarios.DataSource = usuarios;
            rptUsuarios.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscarUsuario.Text.Trim();
            CargarUsuarios(filtro);
        }

        private int ObtenerUsuarioLogueadoId()
        {
            // Ajusta según tu mecanismo de autenticación
            if (Session["IdUsuario"] != null)
                return Convert.ToInt32(Session["IdUsuario"]);
            else
                MasterPage.MostrarModal("Error", "Usuario no logueado.");
            return 0;
        }
        protected void btnNuevoChat_Click(object sender, EventArgs e)
        {
            // Aquí puedes abrir un modal o redirigir a una página de búsqueda de usuarios
            Response.Redirect("../Messaging/NewChat.aspx");
        }

    }
}
