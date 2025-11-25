<%@ Page Title="Añadir Evento" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="AddEvents.aspx.cs" Inherits="Presentation.Events.AddEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-5 mb-5">
    <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6">
            <div class="card shadow-lg border-0 rounded-4">
                <div class="card-header bg-primary text-white text-center rounded-top-4">
                    <h4 class="mb-0">Añadir Nuevo Evento</h4>
                </div>

                <div class="card-body p-4">

                    <!-- Título -->
                    <div class="mb-3">
                        <label for="txtTitulo" class="form-label fw-semibold">Título del evento</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej. Lanzamiento de nuevo producto"></asp:TextBox>
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label fw-semibold">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Describe brevemente el evento..."></asp:TextBox>
                    </div>

                    <!-- Fecha Inicio -->
                    <div class="mb-3">
                        <label for="txtFechaInicio" class="form-label fw-semibold">Fecha de inicio</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>

                    <!-- Fecha Fin -->
                    <div class="mb-3">
                        <label for="txtFechaFin" class="form-label fw-semibold">Fecha de finalización (opcional)</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>

                    <!-- Lugar -->
                    <div class="mb-3">
                        <label for="txtLugar" class="form-label fw-semibold">Lugar del evento</label>
                        <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control" placeholder="Ej. Auditorio Central, Bogotá"></asp:TextBox>
                    </div>

                    <!-- Tipo -->
                    <div class="mb-3">
                        <label for="ddlTipo" class="form-label fw-semibold">Tipo de evento</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccionar tipo..." Value="" />
                            <asp:ListItem Text="Social" Value="Social" />
                            <asp:ListItem Text="Académico" Value="Académico" />
                            <asp:ListItem Text="Cultural" Value="Cultural" />
                            <asp:ListItem Text="Deportivo" Value="Deportivo" />
                            <asp:ListItem Text="Otro" Value="Otro" />
                        </asp:DropDownList>
                    </div>

                    <!-- Organizador -->
                    <div class="mb-3">
                        <label for="txtOrganizador" class="form-label fw-semibold">Organizador</label>
                        <asp:TextBox ID="txtOrganizador" runat="server" CssClass="form-control" placeholder="Nombre del organizador o entidad"></asp:TextBox>
                    </div>

                    <!-- Imagen -->
                    <div class="mb-3">
                        <label for="fuImagen" class="form-label fw-semibold">Imagen del evento</label>
                        <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                        <small class="text-muted">Formatos permitidos: .jpg, .jpeg, .png (máx. 2MB)</small>
                    </div>

                    <div class="text-center mt-4">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Evento" CssClass="btn btn-success px-4 py-2 rounded-pill shadow-sm" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>

<!-- Modal Bootstrap para mensajes -->
<div class="modal fade" id="mensajeModal" tabindex="-1" aria-labelledby="mensajeModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content rounded-4 shadow-lg">
            <div class="modal-header bg-primary text-white rounded-top-4">
                <h5 class="modal-title" id="mensajeModalLabel">Mensaje del sistema</h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body text-center" id="mensajeModalBody"></div>
            <div class="modal-footer justify-content-center">
                <button type="button" class="btn btn-primary px-4" data-bs-dismiss="modal">Aceptar</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>
