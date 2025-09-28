<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Publications.aspx.cs" Inherits="Presentation.Forum.Publications" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Modal de error (siempre al final, fuera del layout) -->
    <div class="modal fade" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalErrorLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="modalErrorLabel">Error</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Layout de dos columnas -->
    <div class="container-fluid mt-4 px-3">
        <div class="row g-4">

            <!-- Columna principal (izquierda) -->
            <div class="col-12 col-lg-8">

                <!-- === VISTA: Lista de publicaciones === -->
                <asp:PlaceHolder ID="phListaPublicaciones" runat="server" Visible="true">
                    <h2 class="mb-3">Publicaciones del Foro</h2>

                    <!-- Formulario para nueva publicación -->
                    <div class="mb-4">
                        <div class="card border-0 shadow-sm">
                            <div class="card-body p-3">
                                <div class="mb-3">
                                    <label for="<%= txtContenido.ClientID %>" class="form-label fw-medium text-muted small">¿Qué estás pensando?</label>
                                    <asp:TextBox ID="txtContenido" runat="server" 
                                        CssClass="form-control" 
                                        TextMode="MultiLine" 
                                        Rows="3" 
                                        placeholder="Escribe tu publicación..."></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-end">
                                    <asp:Button ID="btnPublicar" runat="server" 
                                        CssClass="btn btn-primary px-4" 
                                        Text="Publicar" 
                                        OnClick="btnPublicar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Lista de publicaciones -->
                    <asp:Repeater ID="rptPublicaciones" runat="server" OnItemDataBound="rptPublicaciones_ItemDataBound">
                        <ItemTemplate>
                            <div class="card mb-3">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("NombreUsuario") %></h5>
                                    <p class="card-text"><%# Eval("Contenido") %></p>
                                    <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                    <div class="mt-2">
                                        <asp:Button ID="btnVerComentarios" runat="server" 
                                                    Text='<%# "Ver comentarios (" + ((IEnumerable<Common.Attributes.AttributesComments>)Eval("Comentarios")).Count() + ")" %>'
                                                    CssClass="btn btn-link btn-sm"
                                                    CommandArgument='<%# Eval("IdPublicacion") %>' 
                                                    OnClick="btnVerComentarios_Click"/>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>

                <!-- === VISTA: Comentarios de una publicación === -->
                <asp:Panel ID="pnlComentariosPublicacion" runat="server" Visible="false">
                    <div class="card">
                        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Comentarios de la Publicación</h4>
                            <asp:Button ID="btnVolverPublicaciones" runat="server" Text="← Volver" 
                                        CssClass="btn btn-light btn-sm" OnClick="btnVolverPublicaciones_Click" />
                        </div>
                        <div class="card-body">
                            <div class="border p-3 mb-4 bg-light">
                                <h5><asp:Label ID="lblNombreUsuarioPub" runat="server" /></h5>
                                <p><asp:Label ID="lblContenidoPub" runat="server" /></p>
                                <small class="text-muted"><asp:Label ID="lblFechaPub" runat="server" /></small>
                            </div>

                            <div class="mb-4 p-3 bg-white border rounded">
                                <h5>Agregar comentario</h5>
                                <asp:TextBox ID="txtNuevoComentario" runat="server" CssClass="form-control mb-2" 
                                             TextMode="MultiLine" Rows="3" Placeholder="Escribe un comentario..."></asp:TextBox>
                                <asp:Button ID="btnAgregarComentario" runat="server" Text="Comentar" 
                                            CssClass="btn btn-primary" OnClick="btnAgregarComentario_Click" />
                            </div>

                            <asp:Repeater ID="rptComentariosPrincipales" runat="server" OnItemDataBound="rptComentariosPrincipales_ItemDataBound">
                                <ItemTemplate>
                                    <div class="border p-3 mb-3">
                                        <h6><%# Eval("NombreUsuario") %></h6>
                                        <p><%# Eval("Contenido") %></p>
                                        <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                        <div class="mt-2">
                                            <asp:Button ID="btnVerRespuestas" runat="server" 
                                                        Text='<%# "Ver respuestas (" + ((List<Common.Attributes.AttributesComments>)Eval("Respuestas")).Count + ")" %>' 
                                                        CssClass="btn btn-link btn-sm"
                                                        CommandArgument='<%# Eval("IdComentario") %>' 
                                                        OnClick="btnVerRespuestas_Click"/>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>

                <!-- === VISTA: Respuestas a un comentario === -->
                <asp:Panel ID="pnlRespuestasComentario" runat="server" Visible="false">
                    <div class="card">
                        <div class="card-header bg-success text-white d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Respuestas al Comentario</h4>
                            <asp:Button ID="btnVolverComentarios" runat="server" Text="← Volver" 
                                        CssClass="btn btn-light btn-sm" OnClick="btnVolverComentarios_Click" />
                        </div>
                        <div class="card-body">
                            <div class="border p-3 mb-4 bg-info text-white">
                                <h5><asp:Label ID="lblNombreUsuarioCom" runat="server" /></h5>
                                <p><asp:Label ID="lblContenidoCom" runat="server" /></p>
                                <small><asp:Label ID="lblFechaCom" runat="server" /></small>
                            </div>

                            <div class="mb-4 p-3 bg-white border rounded">
                                <h5>Agregar respuesta</h5>
                                <asp:TextBox ID="txtNuevaRespuesta" runat="server" CssClass="form-control mb-2" 
                                             TextMode="MultiLine" Rows="3" Placeholder="Escribe una respuesta..."></asp:TextBox>
                                <asp:Button ID="btnAgregarRespuesta" runat="server" Text="Responder" 
                                            CssClass="btn btn-success" OnClick="btnAgregarRespuesta_Click" />
                            </div>

                            <asp:Repeater ID="rptRespuestas" runat="server">
                                <ItemTemplate>
                                    <div class="border p-3 mb-2 bg-light">
                                        <h6><%# Eval("NombreUsuario") %></h6>
                                        <p><%# Eval("Contenido") %></p>
                                        <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>

            </div>

            <!-- Columna derecha: Sidebar -->
            <div class="col-12 col-lg-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-header bg-light py-2">
                        <h6 class="mb-0 fw-semibold">Categorías del Foro</h6>
                    </div>
                    <div class="card-body">
                        <ul class="list-group list-group-flush">
                            <asp:HiddenField ID="hfSelectedCategoria" runat="server" Value="1" />
                            <asp:Repeater ID="rptCategorias" runat="server" OnItemCommand="rptCategorias_ItemCommand">
                                <ItemTemplate>
                                    <li class="list-group-item">
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

</asp:Content>