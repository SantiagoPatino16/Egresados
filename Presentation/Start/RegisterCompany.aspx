<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs" Inherits="Presentation.Start.RegisterCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro empresa</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/login-styles.css" rel="stylesheet" />
    <link href="../css/Main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-7">
                    <div class="card shadow-lg rounded-3">
                        <div class="card-header text-center bg-primary text-white">
                            <h4>Registro de Empresa</h4>
                            <small>Completa la información básica para enviar tu solicitud</small>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <asp:Label ID="lblNombreEmpresa" runat="server" Text="Nombre de la empresa" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblNIT" runat="server" Text="NIT o identificación tributaria" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtNIT" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblRepresentante" runat="server" Text="Nombre del representante legal" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtRepresentante" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCargo" runat="server" Text="Cargo del representante" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono de contacto" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCiudad" runat="server" Text="Ciudad sede principal" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblSector" runat="server" Text="Sector o industria" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtSector" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción corta de la empresa" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCorreo" runat="server" Text="Correo de contacto" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblClave" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success w-100" OnClick="btnRegistrar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
        <!-- Modal para mostrar mensajes -->
        <div class="modal fade" id="modalMensaje" tabindex="-1" aria-labelledby="modalMensajeLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title" id="modalMensajeLabel">
                            <asp:Literal ID="litTituloModal" runat="server"></asp:Literal>
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Literal ID="litMensajeModal" runat="server"></asp:Literal>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                            <asp:Literal ID="litTextoBotonModal" runat="server"></asp:Literal>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
