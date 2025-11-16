<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Publications.aspx.cs" Inherits="Presentation.Forum.Publications" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Modal de error -->
    <div class="modal fade" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalErrorLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 shadow-lg border-0">
                <div class="modal-header bg-danger text-white rounded-top-4">
                    <h5 class="modal-title" id="modalErrorLabel">Error</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger fw-semibold"></asp:Label>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-secondary px-4" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Layout de dos columnas -->
    <div class="container-fluid mt-4 px-3">
        <div class="row g-4">

            <!-- Columna principal (izquierda) -->
            <div class="col-12 col-lg-8">

                <!-- Vista: Lista de publicaciones -->
                <asp:PlaceHolder ID="phListaPublicaciones" runat="server" Visible="true">
                    <h2 class="mb-3 text-primary fw-bold">Publicaciones del Foro</h2>

                    <!-- Formulario para nueva publicación -->
                    <div class="mb-4">
                        <div class="card shadow-sm rounded-4 border-0">
                            <div class="card-body p-3">
                                <div class="mb-3">
                                    <label for="<%= txtContenido.ClientID %>" class="form-label fw-semibold text-muted small">¿Qué estás pensando?</label>
                                    <asp:TextBox ID="txtContenido" runat="server"
                                        CssClass="form-control rounded-3"
                                        TextMode="MultiLine"
                                        Rows="3"
                                        placeholder="Escribe tu publicación..."></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-end">
                                    <asp:Button ID="btnPublicar" runat="server"
                                        CssClass="btn btn-primary px-4 rounded-3"
                                        Text="Publicar"
                                        OnClick="btnPublicar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Lista de publicaciones -->
                    <asp:Repeater ID="rptPublicaciones" runat="server" OnItemDataBound="rptPublicaciones_ItemDataBound">
                        <ItemTemplate>
                            <div class="card mb-3 shadow-sm rounded-4 border-0">
                                <div class="card-body">
                                    <!-- Header con foto de perfil y datos del usuario -->
                                    <div class="d-flex align-items-center mb-3">
                                        <div class="me-3">
                                            <asp:PlaceHolder ID="phFotoPerfil" runat="server"></asp:PlaceHolder>
                                        </div>
                                        <div class="flex-grow-1">
                                            <h6 class="mb-0 fw-bold"><%# Eval("NombreUsuario") %></h6>
                                            <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                        </div>
                                    </div>

                                    <!-- Contenido de la publicación -->
                                    <p class="card-text mb-3"><%# Eval("Contenido") %></p>

                                    <!-- Botón de comentarios -->
                                    <div class="border-top pt-2">
                                        <asp:Button ID="btnVerComentarios" runat="server"
                                            Text='<%# "Ver comentarios (" + ((IEnumerable<Common.Attributes.AttributesComments>)Eval("Comentarios")).Count() + ")" %>'
                                            CssClass="btn btn-link btn-sm text-decoration-none"
                                            CommandArgument='<%# Eval("IdPublicacion") %>'
                                            OnClick="btnVerComentarios_Click" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>

                <!-- Vistas de comentarios y respuestas -->
                <asp:Panel ID="pnlComentariosPublicacion" runat="server" Visible="false">
                    <div class="card shadow-sm rounded-4 border-0">
                        <div class="card-header bg-primary text-white rounded-top-4 d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Comentarios de la Publicación</h4>
                            <asp:Button ID="btnVolverPublicaciones" runat="server" Text="← Volver"
                                CssClass="btn btn-light btn-sm rounded-3" OnClick="btnVolverPublicaciones_Click" />
                        </div>
                        <div class="card-body">

                            <!-- Publicación original -->
                            <div class="border rounded-3 p-3 mb-4 bg-light">
                                <div class="d-flex align-items-center mb-2">
                                    <div class="me-3">
                                        <asp:PlaceHolder ID="phFotoPerfilPub" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div>
                                        <h6 class="mb-0 fw-bold">
                                            <asp:Label ID="lblNombreUsuarioPub" runat="server" /></h6>
                                        <small class="text-muted">
                                            <asp:Label ID="lblFechaPub" runat="server" /></small>
                                    </div>
                                </div>
                                <p class="mb-0">
                                    <asp:Label ID="lblContenidoPub" runat="server" />
                                </p>
                            </div>

                            <div class="mb-4 p-3 bg-white border rounded-3">
                                <h6 class="fw-bold mb-3">Agregar comentario</h6>
                                <asp:TextBox ID="txtNuevoComentario" runat="server" CssClass="form-control mb-2 rounded-3"
                                    TextMode="MultiLine" Rows="3" Placeholder="Escribe un comentario..."></asp:TextBox>
                                <asp:Button ID="btnAgregarComentario" runat="server" Text="Comentar"
                                    CssClass="btn btn-primary btn-sm rounded-3" OnClick="btnAgregarComentario_Click" />
                            </div>

                            <asp:Repeater ID="rptComentariosPrincipales" runat="server" OnItemDataBound="rptComentariosPrincipales_ItemDataBound">
                                <ItemTemplate>
                                    <div class="border rounded-3 p-3 mb-3 bg-white shadow-sm">
                                        <div class="d-flex align-items-start mb-2">
                                            <div class="me-3">
                                                <asp:PlaceHolder ID="phFotoPerfilCom" runat="server"></asp:PlaceHolder>
                                            </div>
                                            <div class="flex-grow-1">
                                                <h6 class="mb-1 fw-bold"><%# Eval("NombreUsuario") %></h6>
                                                <p class="mb-1"><%# Eval("Contenido") %></p>
                                                <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                            </div>
                                        </div>
                                        <div class="ms-5">
                                            <asp:Button ID="btnVerRespuestas" runat="server"
                                                Text='<%# "Ver respuestas (" + ((List<Common.Attributes.AttributesComments>)Eval("Respuestas")).Count + ")" %>'
                                                CssClass="btn btn-link btn-sm text-decoration-none p-0"
                                                CommandArgument='<%# Eval("IdComentario") %>'
                                                OnClick="btnVerRespuestas_Click" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>

                <!-- Respuestas a un comentario -->
                <asp:Panel ID="pnlRespuestasComentario" runat="server" Visible="false">
                    <div class="card shadow-sm rounded-4 border-0">
                        <div class="card-header bg-success text-white rounded-top-4 d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Respuestas al Comentario</h4>
                            <asp:Button ID="btnVolverComentarios" runat="server" Text="← Volver"
                                CssClass="btn btn-light btn-sm rounded-3" OnClick="btnVolverComentarios_Click" />
                        </div>
                        <div class="card-body">
                            <div class="border rounded-3 p-3 mb-4 bg-info bg-opacity-25">
                                <div class="d-flex align-items-center mb-2">
                                    <div class="me-3">
                                        <asp:PlaceHolder ID="phFotoPerfilComResp" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div>
                                        <h6 class="mb-0 fw-bold">
                                            <asp:Label ID="lblNombreUsuarioCom" runat="server" /></h6>
                                        <small class="text-muted">
                                            <asp:Label ID="lblFechaCom" runat="server" /></small>
                                    </div>
                                </div>
                                <p class="mb-0">
                                    <asp:Label ID="lblContenidoCom" runat="server" />
                                </p>
                            </div>

                            <div class="mb-4 p-3 bg-white border rounded-3">
                                <h6 class="fw-bold mb-3">Agregar respuesta</h6>
                                <asp:TextBox ID="txtNuevaRespuesta" runat="server" CssClass="form-control mb-2 rounded-3"
                                    TextMode="MultiLine" Rows="3" Placeholder="Escribe una respuesta..."></asp:TextBox>
                                <asp:Button ID="btnAgregarRespuesta" runat="server" Text="Responder"
                                    CssClass="btn btn-success btn-sm rounded-3" OnClick="btnAgregarRespuesta_Click" />
                            </div>

                            <asp:Repeater ID="rptRespuestas" runat="server" OnItemDataBound="rptRespuestas_ItemDataBound">
                                <ItemTemplate>
                                    <div class="border rounded-3 p-3 mb-2 bg-light shadow-sm">
                                        <div class="d-flex align-items-start">
                                            <div class="me-3">
                                                <asp:PlaceHolder ID="phFotoPerfilResp" runat="server"></asp:PlaceHolder>
                                            </div>
                                            <div class="flex-grow-1">
                                                <h6 class="mb-1 fw-bold small"><%# Eval("NombreUsuario") %></h6>
                                                <p class="mb-1 small"><%# Eval("Contenido") %></p>
                                                <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>

            </div>
            <!-- Sidebar derecho con versión móvil (colapsable) -->
            <div class="col-12 col-lg-4">
                <!-- BOTÓN HAMBURGUESA SOLO EN MÓVILES -->
                <div class="d-lg-none mb-3 text-center">
                    <button class="btn btn-outline-primary w-100" type="button" data-bs-toggle="collapse" data-bs-target="#categoriasCollapse" aria-expanded="false" aria-controls="categoriasCollapse">
                        Categorías del Foro
            <i class="bi bi-filter ms-2"></i>
                    </button>
                </div>

                <!-- PANEL DE CATEGORÍAS -->
                <div class="collapse d-lg-block" id="categoriasCollapse">
                    <div class="card border-0 shadow-sm h-100 rounded-4">
                        <div class="card-header bg-light py-2 rounded-top-4 d-flex align-items-center justify-content-between">
                            <h6 class="mb-0 fw-semibold">Categorías del Foro</h6>
                        </div>

                        <div class="card-body">
                            <ul class="list-group list-group-flush">
                                <asp:HiddenField ID="hfSelectedCategoria" runat="server" Value="1" />
                                <asp:Repeater ID="rptCategorias" runat="server" OnItemCommand="rptCategorias_ItemCommand">
                                    <ItemTemplate>
                                        <li class="list-group-item rounded-3 mb-1">
                                            <asp:LinkButton
                                                ID="lnkCategoria"
                                                runat="server"
                                                Text='<%# Eval("Nombre") %>'
                                                CommandName="SelectCategoria"
                                                CommandArgument='<%# Eval("IdCategoria") %>'
                                                CssClass="text-decoration-none w-100 text-start" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>

                            <div class="mt-3">
                                <small class="text-muted">Haz clic en una categoría para filtrar.</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>

    <!-- Modal Bootstrap para mensajes -->
    <div class="modal fade" id="mensajeModal" tabindex="-1" aria-labelledby="mensajeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 shadow-lg border-0">
                <div class="modal-header bg-primary text-white rounded-top-4">
                    <h5 class="modal-title" id="mensajeModalLabel">Mensaje del sistema</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body text-center" id="mensajeModalBody">
                    <!-- Aquí se inserta el texto dinámicamente -->
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-primary px-4 rounded-3" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
