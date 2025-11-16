<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Presentation.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Sistema - Registro</title>

    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>

    <!-- Bootstrap 5 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <link href="css/login-styles.css" rel="stylesheet" />
</head>

<body id="background" class="w-full min-h-screen flex items-center justify-center">

    <form id="form1" runat="server" class="w-full max-w-sm bg-white rounded-2xl shadow-2xl p-8 relative overflow-hidden">

        <!-- LOGO -->
        <div class="flex justify-center mb-3">
            <asp:Image ID="Image1" runat="server" CssClass="h-20 w-auto rounded-md" ImageUrl="~/Resources/Logo.png" />
        </div>

        <!-- Título -->
        <div class="text-center mb-6">
            <h2 class="text-3xl font-bold tracking-tight text-[#013549]">Crear cuenta</h2>
            <p class="text-sm text-[#013549]">Registra tu información para continuar</p>
        </div>

        <!-- Nombre -->
        <div class="mb-4">
            <label for="txtNombre" class="block text-sm font-medium text-[#013549]">Nombre completo</label>
            <asp:TextBox ID="txtNombre" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-3 py-2 placeholder-gray-400"
                placeholder="Ej: Juan Pérez" required="true"></asp:TextBox>
        </div>

        <!-- Correo -->
        <div class="mb-4">
            <label for="txtCorreo" class="block text-sm font-medium text-[#013549]">Correo electrónico</label>
            <asp:TextBox ID="txtCorreo" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-3 py-2 placeholder-gray-400"
                TextMode="Email" placeholder="ejemplo@correo.com" required="true"></asp:TextBox>
        </div>

        <!-- Contraseña -->
        <div class="mb-4">
            <label for="txtClave" class="block text-sm font-medium text-[#013549]">Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-3 py-2 placeholder-gray-400"
                TextMode="Password" placeholder="Crea una contraseña" required="true"></asp:TextBox>
        </div>

        <!-- Rol -->
        <div class="mb-6">
            <label for="ddlRol" class="block text-sm font-medium text-[#013549]">Rol</label>
            <asp:DropDownList ID="ddlRol" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                px-3 py-2 focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400">
                <asp:ListItem Text="Egresado" Value="Egresado"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Botón -->
        <div>
            <asp:Button ID="btnRegistrar" runat="server"
                Text="REGISTRAR"
                CssClass="w-full py-3 rounded-lg bg-[#013549] hover:bg-[#00b2c1]
                transition-all duration-300 font-semibold text-white shadow-md hover:shadow-[#013549]/30"
                OnClick="btnRegistrar_Click" />
        </div>

        <!-- Mensajes -->
        <div class="text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-red-600 font-medium"></asp:Label>
        </div>

        <!-- Link de Login -->
        <div class="text-center mt-4">
            <a href="Login.aspx" class="text-[#013549] font-semibold hover:text-indigo-500 transition">
                ¿Ya tienes cuenta? Inicia sesión
            </a>
        </div>

        <!-- Empresas -->
        <div class="text-center mt-3">
            <a href="RegisterCompany.aspx" class="text-[#013549] font-semibold hover:text-indigo-500 transition">
                ¿Eres una empresa? Regístrate
            </a>
        </div>

    </form>

    <!-- Fondo dinámico -->
    <script>
        const background = document.getElementById('background');
        const images = [
            '../Resources/Login-1.jpg',
            '../Resources/Login-2.jpg',
            '../Resources/Login-3.jpg',
            '../Resources/Login-4.jpg',
        ];
        let index = 0;

        function changeBackground() {
            const img = new Image();
            img.src = images[index];
            img.onload = () => {
                background.style.backgroundImage = `url('${images[index]}')`;
                index = (index + 1) % images.length;
            };
        }

        changeBackground();
        setInterval(changeBackground, 6000);
    </script>

</body>
</html>
