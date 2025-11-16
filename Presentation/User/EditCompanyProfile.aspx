<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true"
    CodeBehind="EditCompanyProfile.aspx.cs" Inherits="Presentation.User.EditCompanyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="card shadow-lg border-0 rounded-4">
            <div class="card-body p-4">
                <h3 class="text-center mb-4 text-primary fw-bold">Editar Perfil de Empresa</h3>

                <!-- DATOS DE LA EMPRESA -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 text-primary">Información de la empresa</h5>
                <div class="row g-3">

                    <div class="col-md-6">
                        <label for="txtNombreEmpresa" class="form-label">Nombre de la empresa</label>
                        <asp:TextBox ID="txtNombreEmpresa" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtNit" class="form-label">NIT</label>
                        <asp:TextBox ID="txtNit" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-12">
                        <label for="txtDescripcionEmpresa" class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcionEmpresa" CssClass="form-control shadow-sm"
                            TextMode="MultiLine" Rows="3" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtUbicacionEmpresa" class="form-label">Dirección / Ubicación</label>
                        <asp:TextBox ID="txtUbicacionEmpresa" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtCiudadEmpresa" class="form-label">Ciudad</label>
                        <asp:TextBox ID="txtCiudadEmpresa" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                </div>


                <!-- INFORMACIÓN DEL REPRESENTANTE -->
                <h5 class="fw-semibold border-bottom pb-2 mb-3 mt-4 text-primary">Representante legal</h5>
                <div class="row g-3">

                    <div class="col-md-6">
                        <label for="txtPersonaRepresentante" class="form-label">Nombre del representante</label>
                        <asp:TextBox ID="txtPersonaRepresentante" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtCargoRepresentante" class="form-label">Cargo</label>
                        <asp:TextBox ID="txtCargoRepresentante" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtTelefonoContacto" class="form-label">Teléfono de contacto</label>
                        <asp:TextBox ID="txtTelefonoContacto" CssClass="form-control shadow-sm" runat="server" />
                    </div>

                    <div class="col-md-6">
                        <label for="txtSectorIndustria" class="form-label">Sector / Industria</label>
                        <asp:TextBox ID="txtSectorIndustria" CssClass="form-control shadow-sm" runat="server" />
                    </div>
                </div>

                <!-- BOTÓN GUARDAR -->
                <div class="text-end mt-4">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary px-4 shadow"
                        Text="Guardar cambios" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
