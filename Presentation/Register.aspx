<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Presentation.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro - Red Social Egresados</title>
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/login-styles.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <div class="card shadow-lg rounded-3">
                        <div class="card-header bg-primary text-white text-center">
                            <h4>Crear cuenta</h4>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre completo" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan Pérez"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="ejemplo@correo.com"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblClave" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblRol" runat="server" Text="Rol" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Egresado" Value="Egresado"></asp:ListItem>
                                    <asp:ListItem Text="Empresa" Value="Empresa"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success w-100" OnClick="btnRegistrar_Click" />
                            <div class="text-center mt-3">
                                <a href="Login.aspx">¿Ya tienes cuenta? Inicia sesión</a>
                            </div>
                            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-2 d-block"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
