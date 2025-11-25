/* =========================================================
 *  chat.js  –  Cliente SignalR para WebForms Chat 1-a-1
 *  Ruta:  ~/Messaging/HUB/chat.js
 * ========================================================= */

$(function () {

    // Evita inicializar dos veces (útil en pages con postbacks parciales)
    if (window.__egresados_chat_initialized) return;
    window.__egresados_chat_initialized = true;

    /* ---------- 1.  CONFIG INICIAL ---------- */
    const usuarioActual = parseInt($('[id$=hfUsuarioActual]').val()) || 0;
    const usuarioDestino = parseInt($('[id$=hfUsuarioDestino]').val()) || 0;
    if (!usuarioActual || !usuarioDestino) return;   // seguridad

    let pendingMsg = null;          // nodo DOM temporal del mensaje que envié

    const $win = $('#chatWindow');
    const $txt = $('#txtMensaje');
    const $btn = $('#btnEnviar');
    const $file = $('#fileInput');
    const $preview = $('#filePreview');

    let hubProxy = $.connection.chatHub;   // nombre de clase: ChatHub

    /* ---------- 2.  HANDLERS QUE EL HUB INVOCARÁ ---------- */
    hubProxy.client.mensajeRecibido = function (idMsg, emisor, contenido, fechaISO) {
        appendMsg(idMsg, emisor, contenido, fechaISO, 'received');
    };

    hubProxy.client.mensajeEnviado = function (idMsg, contenido, fechaISO) {
        let encontrado = false;
        for (let ts in pendientes) {
            const $p = pendientes[ts];
            // ahora $p.data('emisor') funciona correctamente porque data-emisor está en el mismo nodo que $p
            if ($p.find('.contenido').text() === contenido && $p.data('emisor') == usuarioActual) {
                // actualizar id definitivo en el mismo nodo pendiente
                $p.attr('data-id', idMsg).removeClass('pending');
                delete pendientes[ts];
                encontrado = true;
                break;
            }
        }
        if (!encontrado) {
            // caso raro: no teníamos pendiente → pintamos normal
            appendMsg(idMsg, usuarioActual, contenido, fechaISO, 'sent');
        }
    };

    hubProxy.client.mensajeEditado = function (idMsg, nuevoTexto) {
        $(`[data-id="${idMsg}"] .contenido`).text(nuevoTexto);
    };

    hubProxy.client.mensajeEliminado = function (idMsg) {
        $(`[data-id="${idMsg}"]`).fadeOut(200, function () { $(this).remove(); });
    };

    hubProxy.client.mensajeLeido = function (idMsg, fechaISO) {
        const $chk = $(`[data-id="${idMsg}"] .leido`);
        $chk.removeClass('d-none text-secondary').addClass('text-primary'); // Azul cuando se leyó
    };

    /* ---------- 3.  CONEXIÓN ---------- */
    $.connection.hub.qs = { userId: usuarioActual };
    if (!window.__egresados_chat_started) {
        $.connection.hub.start()
            .done(function () {
                console.log('SignalR conectado. ID=' + $.connection.hub.id);
            })
            .fail(function (err) {
                console.error(err);
                alert('No se pudo conectar al servidor de chat.');
            });
        window.__egresados_chat_started = true;
    }

    /* ---------- 4.  ENVÍO DE TEXTO ---------- */
    $btn.on('click', enviarTexto);
    $txt.on('keypress', function (e) {
        if (e.which === 13 && !e.shiftKey) {
            e.preventDefault();
            enviarTexto();
        }
    });

    let pendientes = {};   // clave = timestamp, valor = nodo DOM

    function enviarTexto() {
        const texto = $txt.val().trim();
        if (!texto) return;

        const ts = Date.now().toString();          // ID temporal único
        const $nodo = appendMsg(ts, usuarioActual, texto, new Date().toISOString(), 'sent');
        $nodo.addClass('pending');
        pendientes[ts] = $nodo;                    // guardamos referencia

        hubProxy.server.enviarMensaje(usuarioActual, usuarioDestino, texto)
            .done(() => {/* nada aquí */ })
            .fail(err => {
                $nodo.remove();
                delete pendientes[ts];
                alert('Error al enviar');
            });

        $txt.val('').focus();
    }


    /* ---------- 6.  HELPERS ---------- */

    function appendMsg(idMsg, emisorId, contenido, fechaISO, lado) {
        // Si ya existe un elemento con ese id (servidor) no volvemos a crear
        if (idMsg && $('#chatWindow').find(`[data-id="${idMsg}"]`).length) return null;

        const alignClass = lado === 'sent' ? 'justify-content-end' : 'justify-content-start';
        const styleClass = lado === 'sent' ? 'msg-out' : 'msg-in';
        const leidoHtml = lado === 'sent' ? '<i class="bi bi-check-all leido ms-1"></i>' : '';

        // MOVER data-id y data-emisor al contenedor externo (.msg-item) para que las búsquedas de pendientes funcionen
        const $msg = $(`
        <div class="d-flex mb-2 msg-item ${alignClass}" data-id="${idMsg}" data-emisor="${emisorId}">
            <div class="p-2 rounded-3 shadow-sm ${styleClass}">
                <div class="contenido mb-1">${contenido}</div>
                <small class="text-muted">${new Date(fechaISO).toLocaleString()}${leidoHtml}</small>
            </div>
        </div>
    `);

        $('#chatWindow').append($msg);
        // Mantengo el scroll animado cuando añadimos mensajes dinámicamente
        $('#chatWindow').animate({ scrollTop: $('#chatWindow')[0].scrollHeight }, 150);
        return $msg;
    }

    // Scroll hasta el último mensaje: llamada al iniciar y con fallback para imágenes/recursos
    function scrollToBottom(animate = true) {
        const el = $win[0];
        if (!el) return;
        const pos = el.scrollHeight;
        if (animate) $win.animate({ scrollTop: pos }, 150);
        else $win.scrollTop(pos);
    }

    // Scroll inmediato al cargar la página (mensajes renderizados por servidor)
    scrollToBottom(false);

    // Fallback: si hay imágenes u otros recursos que cambian el alto, reintenta después
    setTimeout(() => scrollToBottom(true), 300);

    // Si existen imágenes dentro del chat, aseguramos scroll cuando terminen de cargar
    $win.find('img').each(function () {
        if (this.complete) return;
        $(this).one('load', () => scrollToBottom(true));
    });

});