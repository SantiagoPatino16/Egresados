<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Presentation.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro - Red Social Egresados</title>
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Para login y registro (css) -->
    <link href="../css/login-styles.css" rel="stylesheet" />
    <!-- Main css para botones -->
    <link href="../css/Main.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <div class="card shadow-lg rounded-3">
                        <div class="card-header card-header-custom text-center">
                            <h4>Crear cuenta</h4>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre completo" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan Pérez"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="ejemplo@pascualbravo.edu.co"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblClave" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblRol" runat="server" Text="Rol" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Egresado" Value="Egresado"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success w-100" OnClick="btnRegistrar_Click" />

                            <div class="text-center mt-3">
                                <a href="Login.aspx">¿Ya tienes cuenta? Inicia sesión</a>
                            </div>
                            <div class="text-center mt-3">
                                <a href="RegisterCompany.aspx">¿Eres una empresa? ¡Registrate!</a>
                            </div>
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
