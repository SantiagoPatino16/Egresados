<%@ Page Title="Bandeja de Entrada" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="Presentation.Messaging.Inbox" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  Contenedor FLUIDO  --%>
    <div class="container-xl mt-3 mt-lg-4 mb-4">

        <%--  Buscador  --%>
        <div class="p-3 bg-white border-bottom">
            <asp:TextBox ID="txtBuscarUsuario" runat="server"
                CssClass="form-control rounded-pill"
                Placeholder="Buscar usuario…" />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                CssClass="btn btn-primary btn-sm w-100 mt-2 rounded-pill" />
        </div>

        <%--  Header / Nuevo chat  --%>
        <div class="px-3 py-2 d-flex justify-content-between align-items-center bg-white border-bottom">
            <h6 class="mb-0 fw-bold text-dark">Conversaciones</h6>
            <asp:Button ID="btnNuevoChat" runat="server" Text="+ Nuevo chat"
                CssClass="btn btn-success btn-sm rounded-pill px-3" onClick="btnNuevoChat_Click"/>
        </div>

        <%--  Lista de usuarios  --%>
        <div class="flex-fill overflow-auto p-3 bg-light">
            <asp:Repeater ID="rptUsuarios" runat="server">
                <ItemTemplate>
                    <div class="d-flex align-items-center p-2 mb-2 bg-white rounded-3 shadow-sm inbox-user"
                        data-userid='<%# Eval("IdUsuario") %>'
                        onclick="abrirConversacion(<%# Eval("IdUsuario") %>, '<%# Eval("Nombre") %>')">

                        <%-- Avatar placeholder --%>
                        <div class="flex-shrink-0 bg-gradient rounded-circle me-3" style="width: 44px; height: 44px;"></div>

                        <div class="flex-fill">
                            <div class="fw-semibold text-dark"><%# Eval("Nombre") %></div>
                            <small class="text-muted">Abrir conversación</small>
                        </div>

                        <%-- Indicador “en línea” --%>
                        <span class="badge bg-success border border-2 border-light rounded-pill">&nbsp;</span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script>
        function abrirConversacion(idUsuario, nombre) {
            window.location.href = 'Chat.aspx?usuarioId=' + idUsuario;
        }
    </script>
</asp:Content>
