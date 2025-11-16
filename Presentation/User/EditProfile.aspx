<%@ Page Title="Editar Perfil" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Presentation.User.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="card shadow-lg border-0 rounded-4">
            <div class="card-body p-4">
                <h3 class="text-center mb-4 text-primary fw-bold">Editar Perfil</h3>

                <!-- DATOS PERSONALES -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 text-primary">Datos personales</h5>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtNombreCompleto" class="form-label">Nombre completo</label>
                        <asp:TextBox ID="txtNombreCompleto" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtCiudadResidencia" class="form-label">Ciudad de residencia</label>
                        <asp:TextBox ID="txtCiudadResidencia" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                </div>

                <!-- INFORMACIÓN ACADÉMICA -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 mt-4 text-primary">Información académica</h5>
                <div class="row g-3">
                    <div class="col-md-8">
                        <label for="txtProgramaAcademico" class="form-label">Programa académico</label>
                        <asp:TextBox ID="txtProgramaAcademico" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <label for="txtAnioGraduacion" class="form-label">Año de graduación</label>
                        <asp:TextBox ID="txtAnioGraduacion" CssClass="form-control shadow-sm" runat="server" TextMode="Number" />
                    </div>
                </div>

                <!-- INFORMACIÓN PROFESIONAL -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 mt-4 text-primary">Información profesional</h5>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtEmpresaActual" class="form-label">Empresa actual</label>
                        <asp:TextBox ID="txtEmpresaActual" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtCargoActual" class="form-label">Cargo actual</label>
                        <asp:TextBox ID="txtCargoActual" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                </div>

                <!-- REDES Y ENLACES -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 mt-4 text-primary">Redes y enlaces</h5>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtLinkedIn" class="form-label">LinkedIn</label>
                        <asp:TextBox ID="txtLinkedIn" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtSitioWeb" class="form-label">Sitio web personal</label>
                        <asp:TextBox ID="txtSitioWeb" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                </div>

                <!-- BIOGRAFÍA -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 mt-4 text-primary">Biografía</h5>
                <div class="mb-3">
                    <asp:TextBox ID="txtBiografia" CssClass="form-control shadow-sm" runat="server" TextMode="MultiLine" Rows="4" />
                </div>

                <!-- BOTÓN GUARDAR -->
                <div class="text-end mt-4">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary px-4 shadow" Text="Guardar cambios" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
