<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Sistema - Login</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Para login y registro (css) -->
    <link href="../css/login-styles.css" rel="stylesheet" />
    <!-- Main css para botones -->
    <link href="../css/Main.css" rel="stylesheet" />
</head>


<body class="bg-light d-flex align-items-center justify-content-center vh-100">
    <form id="form1" runat="server" class="w-100 mt-5" style="max-width: 400px;">
        <div class="card shadow rounded-3">
            <div id="cardLogin" class="card-body p-4">
                <asp:Image
                    ID="Image1"
                    runat="server"
                    CssClass="img-fluid shadow mx-auto d-block mb-3"
                    Width="80%" />
                <h3 id="txtLogin" class="text-center mb-4">Iniciar Sesión</h3>

                <!-- Correo -->
                <div class="mb-3">
                    <label for="txtCorreo" class="form-label">Correo electrónico</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"
                        TextMode="Email" placeholder="ejemplo@correo.com" required="true"></asp:TextBox>
                    <div class="invalid-feedback">Por favor ingrese un correo válido.</div>
                </div>

                <!-- Clave -->
                <div class="mb-3">
                    <label for="txtClave" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"
                        TextMode="Password" placeholder="Ingrese su contraseña" required="true"></asp:TextBox>
                    <div class="invalid-feedback">La contraseña es obligatoria.</div>
                </div>

                <!-- Botón -->
                <div class="d-grid">
                    <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-primary"
                        Text="Ingresar" OnClick="btnIngresar_Click" />
                </div>

                <div class="text-center mt-3">
                    <a href="Register.aspx" class="text-blue fw-bold">REGISTRARSE</a>
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

    <!-- Validación de Bootstrap -->
    <script>
        (function () {
            'use strict';
            var forms = document.querySelectorAll('form');
            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
        })();
    </script>
</body>
</html>
