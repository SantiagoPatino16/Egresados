$(function () {
    if (!$.connection.chatHub) {
        console.error("chatHub no está definido. Revisa el orden de scripts.");
        return;
    }
    // IDs de usuario y conexión al Hub
    var usuarioActual = parseInt($('#chatWindow').data('usuario'));
    var usuarioDestino = parseInt($('#chatWindow').data('destino'));
    var chatHub = $.connection.chatHub;

    $.connection.hub.qs = { 'userId': usuarioActual };

    $.connection.hub.start()
        .done(function () { console.log('Conectado a SignalR'); })
        .fail(function (err) { console.error('Error de conexión SignalR:', err); });

    // ---------------- IDs dinámicos de WebForms ----------------
    var btnEnviarID = '<%= btnEnviarMensaje.ClientID %>';
    var txtMensajeID = '<%= txtMensaje.ClientID %>';

    // ---------------- Funciones ----------------
    function agregarMensaje(id, emisor, contenido, fecha, esPropio) {
        var clase = esPropio ? "text-end" : "";
        var nombre = esPropio ? "Tú" : emisor;
        $('#chatWindow').append(
            `<div class="mb-2 ${clase}" data-id="${id}">
                <strong>${nombre}:</strong> <span class="contenido">${contenido}</span>
                <span class="text-muted small">(${fecha})</span>
             </div>`
        );
        $('#chatWindow').scrollTop($('#chatWindow')[0].scrollHeight);
    }

    function enviarMensaje() {
        var contenido = $('#' + txtMensajeID).val().trim();
        if (!contenido) return;

        chatHub.server.enviarMensaje(usuarioActual, usuarioDestino, contenido)
            .done(function () { $('#' + txtMensajeID).val(''); })
            .fail(function (err) { console.error('Error al enviar mensaje:', err); });
    }

    // ---------------- Eventos ----------------
    $('#' + btnEnviarID).click(enviarMensaje);
    $('#' + txtMensajeID).keydown(function (e) {
        if (e.which === 13) { enviarMensaje(); e.preventDefault(); }
    });

    // ---------------- Recepción de eventos ----------------
    chatHub.client.mensajeRecibido = function (id, emisor, contenido, fecha) {
        if (emisor === usuarioDestino) agregarMensaje(id, emisor, contenido, fecha, false);
    };

    chatHub.client.mensajeEnviado = function (id, contenido, fecha) {
        agregarMensaje(id, usuarioActual, contenido, fecha, true);
    };

    chatHub.client.mensajeEditado = function (id, nuevoContenido) {
        var msgDiv = $(`#chatWindow div[data-id='${id}']`);
        if (msgDiv.length) msgDiv.find('span.contenido').text(nuevoContenido);
    };

    chatHub.client.mensajeEliminado = function (id) {
        var msgDiv = $(`#chatWindow div[data-id='${id}']`);
        if (msgDiv.length) msgDiv.remove();
    };
});
