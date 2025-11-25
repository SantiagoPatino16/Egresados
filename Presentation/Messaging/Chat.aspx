<%@ Page Title="Chat" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Presentation.Messaging.Chat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  Variables para JS  --%>
    <asp:HiddenField ID="hfUsuarioActual" runat="server" Value='<%= UsuarioActual %>' />
    <asp:HiddenField ID="hfUsuarioDestino" runat="server" Value='<%= UsuarioDestino %>' />

    <%--  Contenedor FLUIDO  --%>
    <div class="container-xl mt-3 mt-lg-4 mb-4">

        <%--  Header  --%>
        <div class="row mb-3">
            <div class="col-12 d-flex align-items-center">
                <a href="Inbox.aspx" class="btn btn-sm btn-outline-secondary rounded-pill me-3">← Volver</a>
                <img id="imgAvatar" runat="server" class="rounded-circle me-2" width="36" height="36" />
                <div>
                    <h5 class="mb-0 fw-bold"><%= NombreDestino %></h5>
                </div>
            </div>
        </div>

        <%--  Ventana de mensajes  --%>
        <div class="row mb-3">
            <div class="col-12">
                <div id="chatWindow" class="border rounded-3 bg-white p-3"
                    style="min-height: 45vh; max-height: 65vh; overflow-y: auto;">
                    <asp:Repeater ID="rptHistorial" runat="server" OnItemDataBound="rptHistorial_ItemDataBound">
                        <ItemTemplate>
                            <div class="d-flex mb-2 <asp:Literal ID='ltrAlign' runat='server'></asp:Literal>">
                                <div class="p-2 rounded-3 shadow-sm <asp:Literal ID='ltrStyle' runat='server'></asp:Literal>"
                                    data-id='<%# Eval("IdMensaje") %>'
                                    data-emisor='<%# Eval("IdEmisor") %>'>

                                    <div class="contenido"><%# Eval("Contenido") %></div>

                                    <div class="small text-muted mt-1 d-flex justify-content-between align-items-center">
                                        <span><%# ((DateTime)Eval("FechaEnvio")).ToString("g") %></span>
                                        <i class="bi bi-check-all"></i>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <%--  Barra de envío  --%>
        <div class="row">
            <div class="col-12">
                <div class="input-group">
                    <input id="txtMensaje" type="text" class="form-control form-control-lg rounded-pill-start"
                        placeholder="Escribe un mensaje…" maxlength="500">
                    <button id="btnEnviar" class="btn btn-primary rounded-pill-end px-3" type="button">
                        <!-- Flecha Messenger -->
                        <svg width="20" height="20" fill="currentColor" viewBox="0 0 24 24">
                            <path d="M2.01 21L23 12 2.01 3 2 10l15 2-15 2z" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script src="/signalr/hubs"></script>
    <script src="../Messaging/HUB/chat.js"></script>
</asp:Content>
