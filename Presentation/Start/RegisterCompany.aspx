<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs" Inherits="Presentation.Start.RegisterCompany" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Registro de Empresa</title>

    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>

    <!-- Bootstrap 5 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <link href="css/login-styles.css" rel="stylesheet" />
</head>

<body id="background" class="w-full min-h-screen flex flex-col items-center justify-start pt-6 pb-10">

    <form id="form1" runat="server" class="w-full max-w-lg bg-white rounded-2xl shadow-2xl p-8 relative overflow-hidden">

        <!-- LOGO -->
        <div class="flex justify-center mb-2">
            <asp:Image ID="Image1" runat="server" CssClass="h-16 w-auto rounded-md" ImageUrl="~/Resources/Logo.png" />
        </div>

        <!-- TÍTULO -->
        <div class="text-center mb-3">
            <h2 class="text-2xl font-bold tracking-tight text-[#013549]">Registro de Empresa</h2>
            <p class="text-xs text-[#013549]">Completa la información para enviar tu solicitud</p>
        </div>

        <!-- Nombre Empresa -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Nombre de la empresa</label>
            <asp:TextBox ID="txtNombreEmpresa" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"
                placeholder="Ej: TechCorp SAS"></asp:TextBox>
        </div>

        <!-- NIT -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">NIT o identificación tributaria</label>
            <asp:TextBox ID="txtNIT" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Representante -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Nombre del representante legal</label>
            <asp:TextBox ID="txtRepresentante" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Cargo -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Cargo del representante</label>
            <asp:TextBox ID="txtCargo" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Teléfono -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Teléfono de contacto</label>
            <asp:TextBox ID="txtTelefono" runat="server" TextMode="Phone"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Ciudad -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Ciudad sede principal</label>
            <asp:TextBox ID="txtCiudad" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Sector -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Sector o industria</label>
            <asp:TextBox ID="txtSector" runat="server"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Descripción -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Descripción corta</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="2"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Correo -->
        <div class="mb-2">
            <label class="block text-sm font-medium text-[#013549]">Correo de contacto</label>
            <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- Clave -->
        <div class="mb-4">
            <label class="block text-sm font-medium text-[#013549]">Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server" TextMode="Password"
                CssClass="mt-1 block w-full bg-gray-100 border border-gray-300 rounded-lg text-[#013549]
                focus:border-indigo-400 focus:ring-2 focus:ring-indigo-400 px-2 py-1"></asp:TextBox>
        </div>

        <!-- BOTÓN REGISTRAR -->
        <asp:Button ID="btnRegistrar" runat="server"
            Text="REGISTRAR EMPRESA"
            CssClass="w-full py-2 rounded-lg bg-[#013549] hover:bg-[#00b2c1] transition-all duration-300
            font-semibold text-white shadow-md hover:shadow-[#013549]/30"
            OnClick="btnRegistrar_Click" />

        <!-- Volver -->
        <div class="text-center mt-3">
            <a href="Login.aspx" class="text-[#013549] text-sm font-semibold hover:text-indigo-500 transition">
                ¿Ya tienes cuenta? Inicia sesión
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
