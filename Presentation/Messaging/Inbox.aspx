<%@ Page Title="Bandeja de Entrada" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="Presentation.Messaging.Inbox" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="inbox-container">
        <!-- Filtro de búsqueda -->
        <div class="inbox-search mb-3">
            <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control" Placeholder="Buscar usuario..." />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mt-2" OnClick="btnBuscar_Click" />
        </div>

        <!-- Lista de conversaciones o usuarios -->
        <div class="inbox-header mb-3">
            <asp:Button ID="btnNuevoChat" runat="server" Text="Nuevo Chat" CssClass="btn btn-success" OnClick="btnNuevoChat_Click" />
        </div>

        <div class="inbox-list">
            <asp:Repeater ID="rptUsuarios" runat="server">
                <ItemTemplate>
                    <div class="inbox-user p-2 border mb-1" data-userid='<%# Eval("IdUsuario") %>'
                        onclick="abrirConversacion(<%# Eval("IdUsuario") %>, '<%# Eval("Nombre") %>')">
                        <%# Eval("Nombre") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script type="text/javascript">
        function abrirConversacion(idUsuario, nombre) {
            window.location.href = 'Chat.aspx?usuarioId=' + idUsuario;
        }
    </script>
</asp:Content>
