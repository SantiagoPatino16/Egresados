<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Presentation.Perfil" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Perfil del Usuario</h2>

        <!-- Imagen de perfil -->
        <div class="text-center mb-3">
            <asp:Image ID="imgFotoPerfil" runat="server"
                       ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4YreOWfDX3kK-QLAbAL4ufCPc84ol2MA8Xg&s" />
        </div>
        <!-- Información del perfil -->
        <div class="mx-auto" style="max-width:500px;">
            <table class="table table-bordered">
                <tbody>
                    <tr>
                        <th>Nombre Completo:</th>
                        <td><asp:Label ID="lblNombre" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>Correo:</th>
                        <td><asp:Label ID="lblCorreo" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>Rol:</th>
                        <td><asp:Label ID="lblRol" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>Fecha de Registro:</th>
                        <td><asp:Label ID="lblFechaRegistro" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>Activo:</th>
                        <td><asp:Label ID="lblActivo" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-center">
                            <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold text-success" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
