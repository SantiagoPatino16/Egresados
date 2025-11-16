<%@ Page Title="Eventos" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EventsLoad.aspx.cs" Inherits="Presentation.Events.EventsLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="text-center mb-6 text-primary">Eventos</h2>

        <!--  Filtros de búsqueda -->
        <div class="card shadow-sm p-3 mb-4">
            <div class="row g-2 align-items-end">
                <div class="col-md-4">
                    <label for="txtBuscarTitulo" class="form-label fw-semibold">Título</label>
                    <asp:TextBox ID="txtBuscarTitulo" runat="server" CssClass="form-control" placeholder="Buscar por título..." />
                </div>
                <div class="col-md-2">
                    <label for="txtFechaInicioFiltro" class="form-label fw-semibold">Desde</label>
                    <asp:TextBox ID="txtFechaInicioFiltro" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <label for="txtFechaFinFiltro" class="form-label fw-semibold">Hasta</label>
                    <asp:TextBox ID="txtFechaFinFiltro" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-1">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click" />
                </div>
                <div>
                    <asp:Button ID="btnLimpiarCampos" runat="server" Text="Limpiar" CssClass="btn btn-primary w-100" OnClick="btnLimpiarCampos_Click" />
                </div>
            </div>
        </div>


        <!--  Repeater de eventos -->
        <asp:Repeater ID="rptEventos" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-3 d-inline-block">
                    <div class="card shadow-sm border-0">
                        <img src='<%# Eval("RutaImagen") %>' class="card-img-top" alt="Imagen evento" style="height: 200px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title text-dark fw-bold"><%# Eval("Titulo") %></h5>
                            <p class="card-text text-secondary" style="min-height: 60px;">
                                <%# Eval("Descripcion").ToString().Length > 90 ? Eval("Descripcion").ToString().Substring(0, 90) + "..." : Eval("Descripcion") %>
                            </p>
                            <p class="text-muted small mb-1">
                                <i class="bi bi-geo-alt"></i><%# Eval("Lugar") %>
                            </p>
                            <p class="text-muted small">
                                <i class="bi bi-calendar-event"></i>
                                <%# string.Format("{0:dd/MM/yyyy}", Eval("FechaInicio")) %> - <%# (Eval("FechaFin") != DBNull.Value ? string.Format("{0:dd/MM/yyyy}", Eval("FechaFin")) : "") %>
                            </p>
                            <a href="#"
                                class="btn btn-primary btn-sm w-100"
                                data-bs-toggle="modal"
                                data-bs-target="#detalleEventoModal"
                                data-titulo='<%# Eval("Titulo") %>'
                                data-descripcion='<%# Eval("Descripcion") %>'
                                data-lugar='<%# Eval("Lugar") %>'
                                data-fechainicio='<%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %>'
                                data-fechafin='<%# Eval("FechaFin") != DBNull.Value ? Eval("FechaFin", "{0:dd/MM/yyyy}") : "" %>'
                                data-organizador='<%# Eval("Organizador") %>'
                                data-imagen='<%# Eval("RutaImagen") %>'
                                onclick="cargarDetallesEvento(this)">Ver detalles
                            </a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!--  Sin resultados -->
        <asp:Label ID="lblSinEventos" runat="server" CssClass="text-center text-muted d-block mt-4" Visible="false"
            Text="No hay eventos disponibles por el momento."></asp:Label>
    </div>

    <!--  Modal Detalles -->
    <div class="modal fade" id="detalleEventoModal" tabindex="-1" aria-labelledby="detalleEventoLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="detalleEventoLabel">Detalles del Evento</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="text-center mb-3">
                        <img id="imgEventoModal" src="~/Resources/events/default.png" alt="Imagen evento" class="img-fluid rounded" style="max-height: 300px; object-fit: cover;">
                    </div>
                    <h4 id="tituloEventoModal" class="fw-bold"></h4>
                    <p id="descripcionEventoModal"></p>
                    <p class="text-muted mb-1"><i class="bi bi-geo-alt"></i><span id="lugarEventoModal"></span></p>
                    <p class="text-muted"><i class="bi bi-calendar-event"></i><span id="fechaEventoModal"></span></p>
                    <p class="text-muted"><i class="bi bi-person"></i>Organizador: <span id="organizadorEventoModal"></span></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
