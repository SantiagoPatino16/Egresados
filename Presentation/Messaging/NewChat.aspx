<%@ Page Title="Nuevo Chat" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="NewChat.aspx.cs" Inherits="Presentation.Messaging.NewChat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-3">Iniciar nuevo chat</h3>

        <!-- Barra de búsqueda -->
        <div class="input-group mb-3">
            <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control" Placeholder="Buscar usuario..." />
            <button class="btn btn-primary" type="submit" runat="server" id="btnBuscar" OnClick="btnBuscar_Click">Buscar</button>
        </div>

        <!-- Lista de usuarios -->
        <div class="list-group">
            <asp:Repeater ID="rptUsuarios" runat="server">
                <ItemTemplate>
                    <a href="javascript:void(0);" 
                       class="list-group-item list-group-item-action"
                       data-userid='<%# Eval("IdUsuario") %>'
                       onclick="iniciarChat(<%# Eval("IdUsuario") %>, '<%# Eval("Nombre") %>')">
                        <%# Eval("Nombre") %>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script type="text/javascript">
        function iniciarChat(idUsuario, nombre) {
            // Redirigir a la página de chat con el usuario seleccionado
            window.location.href = 'Chat.aspx?usuarioId=' + idUsuario;
        }
    </script>
</asp:Content>
