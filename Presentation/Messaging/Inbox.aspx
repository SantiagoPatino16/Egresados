<%@ Page Title="Bandeja de Entrada" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="Presentation.Messaging.Inbox" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="inbox-container">
        <!-- Filtro de búsqueda -->
        <div class="inbox-search mb-3">
            <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control" Placeholder="Buscar usuario..." />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mt-2" OnClick="btnBuscar_Click" />
        </div>

        <!-- Lista de conversaciones o usuarios -->
        <div class="inbox-list">
            <asp:Repeater ID="rptUsuarios" runat="server">
                <ItemTemplate>
                    <div class="inbox-user p-2 border mb-1" data-userid='<%# Eval("IdUsuario") %>' 
                         onclick="abrirConversacion(<%# Eval("IdUsuario") %>, '<%# Eval("NombreCompleto") %>')">
                        <%# Eval("NombreCompleto") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script type="text/javascript">
        function abrirConversacion(idUsuario, nombre) {
            // Aquí puedes abrir la ventana de chat o cargar los mensajes vía AJAX/SignalR
            alert('Abrir conversación con: ' + nombre + ' (ID ' + idUsuario + ')');
        }
    </script>
</asp:Content>
