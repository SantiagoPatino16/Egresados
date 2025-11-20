<%@ Page Title="Chat" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Presentation.Messaging.Chat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h4 id="chatTitle">Chat</h4>

        <!-- Contenedor de mensajes con scroll -->
        <div id="chatWindow" style="max-height: 500px; overflow-y: auto;"
            data-usuario="<%= UsuarioActual %>"
            data-destino="<%= UsuarioDestino %>">
            <asp:Repeater ID="rptMensajes" runat="server">
                <ItemTemplate>
                    <div class="mb-2" data-id='<%# Eval("IdMensaje") %>'>
                        <strong><%# Eval("Emisor.Nombre") %>:</strong>
                        <span class="contenido"><%# Eval("Contenido") %></span>
                        <span class="text-muted small">(<%# Eval("FechaEnvio", "{0:dd/MM/yyyy HH:mm}") %>)</span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Enviar mensaje -->
        <div class="input-group mt-2">
            <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" placeholder="Escribe un mensaje..."></asp:TextBox>
            <asp:Button ID="btnEnviarMensaje" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClientClick="return false;" />
        </div>
    </div>

    <!-- Scripts SignalR -->
    <!-- jQuery primero -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <!-- SignalR Core -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/2.4.2/jquery.signalR.min.js"></script>

    <!-- Proxies generados dinámicamente por SignalR -->
    <script src="/signalr/hubs"></script>

    <!-- Tu JS personalizado -->
    <script src="../Messaging/HUB/chat.js"></script>

</asp:Content>
