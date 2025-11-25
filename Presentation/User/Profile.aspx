<%@ Page Title="Perfil de Usuario" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="Presentation.Perfil" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5 d-flex justify-content-center">
        <div class="card shadow-lg border-0" style="max-width: 850px; border-radius: 20px;">
            <div class="row g-0">
                <div class="container py-5 d-flex justify-content-center">
                    <div class="card shadow-lg border-0 rounded-4" style="max-width: 900px;">
                        <div class="row g-0">
                            <!-- COLUMNA IZQUIERDA -->
                            <div class="col-md-4 bg-primary text-white text-center d-flex flex-column justify-content-center align-items-center p-4 rounded-start-4">
                                <asp:Image ID="imgFotoPerfil" runat="server"
                                    ImageUrl="../Resources/perfiles/default.png"
                                    CssClass="rounded-circle border border-3 border-light shadow mb-3"
                                    Width="150px" Height="150px" />

                                <h4 class="fw-bold mb-1">
                                    <asp:Literal ID="litNombre" runat="server" />
                                </h4>

                                <span class="badge bg-light text-primary mb-3 px-3 py-2 rounded-pill shadow-sm">
                                    <asp:Literal ID="litRol" runat="server" />
                                </span>

                                <asp:FileUpload ID="fuFotoPerfil" runat="server" CssClass="form-control form-control-sm mb-2 border-0 shadow-sm" Accept="image/*" />
                                <asp:Button ID="btnCambiarFoto" runat="server" Text="Actualizar foto"
                                    CssClass="btn btn-light btn-sm fw-semibold shadow-sm"
                                    OnClick="btnCambiarFoto_Click" />
                            </div>

                            <!-- COLUMNA DERECHA -->
                            <div class="col-md-8 p-4 bg-light rounded-end-4">
                                <h4 class="fw-bold text-primary mb-4 border-bottom pb-2">Información principal</h4>
                                <ul class="list-group list-group-flush mb-3">
                                    <li class="list-group-item bg-light border-0"><strong>Fecha de Registro:</strong>
                                        <asp:Literal ID="litFechaRegistro" runat="server" /></li>
                                    <li class="list-group-item bg-light border-0"><strong>Activo:</strong>
                                        <asp:Literal ID="litActivo" runat="server" /></li>
                                    <li class="list-group-item bg-light border-0"><strong>Correo:</strong>
                                        <asp:Literal ID="litCorreo" runat="server" /></li>
                                </ul>

                                <div class="mt-4 d-flex justify-content-end gap-2">
                                    <button type="button" class="btn btn-outline-primary fw-semibold shadow-sm" data-bs-toggle="modal" data-bs-target="#modalDetalles">
                                        Ver más detalles
                                    </button>
                                    <asp:Button ID="btnEditarPerfil" runat="server"
                                        Text="Editar perfil"
                                        CssClass="btn btn-primary fw-semibold shadow-sm"
                                        OnClick="btnEditarPerfil_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- MODAL DETALLES -->
                <div class="modal fade" id="modalDetalles" tabindex="-1" aria-labelledby="modalDetallesLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-lg">
                        <div class="modal-content border-0 shadow-lg rounded-4">
                            <div class="modal-header bg-primary text-white rounded-top-4">
                                <h5 class="modal-title fw-semibold" id="modalDetallesLabel">Detalles del perfil</h5>
                                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>

                            <div class="modal-body bg-light">
                                <div class="row g-4">
                                    <div class="col-md-6">
                                        <div class="bg-white rounded-3 p-3 shadow-sm h-100">
                                            <p>
                                                <strong>Programa académico:</strong>
                                                <asp:Literal ID="litPrograma" runat="server" />
                                            </p>
                                            <p>
                                                <strong>Año de graduación:</strong>
                                                <asp:Literal ID="litAnioGraduacion" runat="server" />
                                            </p>
                                            <p>
                                                <strong>Empresa actual:</strong>
                                                <asp:Literal ID="litEmpresa" runat="server" />
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="bg-white rounded-3 p-3 shadow-sm h-100">
                                            <p>
                                                <strong>Cargo actual:</strong>
                                                <asp:Literal ID="litCargo" runat="server" />
                                            </p>
                                            <p>
                                                <strong>Ciudad de residencia:</strong>
                                                <asp:Literal ID="litCiudad" runat="server" />
                                            </p>
                                            <p>
                                                <strong>Última conexión:</strong>
                                                <asp:Literal ID="litUltimaConexion" runat="server" />
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="bg-white rounded-3 p-3 shadow-sm">
                                            <p>
                                                <strong>LinkedIn:</strong>
                                                <asp:Literal ID="litLinkedIn" runat="server" />
                                            </p>
                                            <p>
                                                <strong>Sitio web:</strong>
                                                <asp:Literal ID="litSitioWeb" runat="server" />
                                            </p>
                                            <hr />
                                            <p><strong>Biografía:</strong></p>
                                            <div class="border rounded p-3 bg-light-subtle">
                                                <asp:Literal ID="litBiografia" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer bg-light rounded-bottom-4">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>