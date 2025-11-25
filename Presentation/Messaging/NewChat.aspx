<%@ Page Title="Nuevo Chat" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="NewChat.aspx.cs" Inherits="Presentation.Messaging.NewChat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Contenedor fluido y centrado -->
    <div class="container-xl mt-4 mt-lg-5 mb-5">

        <!-- Card principal -->
        <div class="card shadow-institucional border-0">
            <div class="card-body p-4 p-lg-5">

                <!-- Header: título y subtítulo -->
                <div class="d-flex align-items-start justify-content-between mb-3">
                    <div>
                        <h2 class="mb-1 fw-bold">Iniciar nuevo chat</h2>
                        <p class="text-muted mb-0">Busca un usuario y comienza una conversación privada.</p>
                    </div>
                    <div class="d-none d-md-block">
                        <!-- pequeño widget visual (solo decoración) -->
                        <span class="badge bg-light text-dark shadow-sm">Mensajes</span>
                    </div>
                </div>

                <!-- Barra de búsqueda -->
                <div class="row justify-content-center mb-4">
                    <div class="col-12 col-md-8 col-lg-6">
                        <div class="input-group input-group-lg shadow-sm">
                            <span class="input-group-text bg-transparent border-0 pe-2">
                                <!-- icono SVG ligero -->
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#00b2c1" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001l3.85 3.85a1 1 0 0 0 1.415-1.415l-3.85-3.85zm-5.242.656a5 5 0 1 1 0-10 5 5 0 0 1 0 10z" />
                                </svg>
                            </span>

                            <asp:TextBox ID="txtBuscarUsuario" runat="server"
                                CssClass="form-control rounded-pill-start"
                                Placeholder="Buscar usuario..." />

                            <asp:Button ID="btnBuscar"
                                runat="server"
                                CssClass="btn btn-primary rounded-pill-end px-4"
                                Text="Buscar"
                                OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                </div>

                <!-- Lista de usuarios -->
                <div class="row justify-content-center">
                    <div class="col-12 col-md-10 col-lg-8">
                        <div class="list-group list-group-flush shadow-sm rounded-3 overflow-auto" style="max-height: 60vh;">
                            <asp:Repeater ID="rptUsuarios" runat="server">
                                <ItemTemplate>
                                    <a href="javascript:void(0);"
                                        class="list-group-item list-group-item-action d-flex align-items-center px-3 py-3"
                                        data-userid='<%# Eval("IdUsuario") %>'
                                        onclick="iniciarChat(<%# Eval("IdUsuario") %>, '<%# Eval("Nombre") %>')">
                                        <!-- Avatar placeholder institucional -->
                                        <div class="avatar-sm rounded-circle me-3"></div>

                                        <div class="flex-grow-1">
                                            <div class="d-flex align-items-center">
                                                <span class="fw-semibold h6 mb-0"><%# Eval("Nombre") %></span>
                                                <small class="text-muted ms-2 d-none d-md-inline">
                                                    <!-- espacio para rol/estado si se desea -->
                                                    <!-- placeholder -->
                                                </small>
                                            </div>
                                            <div class="text-muted small">Haz clic para chatear</div>
                                        </div>

                                        <div class="ms-3 d-flex align-items-center">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="text-muted" viewBox="0 0 16 16" aria-hidden="true">
                                                <path fill-rule="evenodd" d="M6.854 4.146a.5.5 0 0 1 .0.708L4.707 7l2.147 2.146a.5.5 0 0 1-.708.708l-2.5-2.5a.5.5 0 0 1 0-.708l2.5-2.5a.5.5 0 0 1 .708 0z" />
                                                <path fill-rule="evenodd" d="M10.5 7.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5z" />
                                            </svg>
                                        </div>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <script type="text/javascript">
        function iniciarChat(idUsuario, nombre) {
            window.location.href = 'Chat.aspx?usuarioId=' + idUsuario;
        }
    </script>
</asp:Content>
