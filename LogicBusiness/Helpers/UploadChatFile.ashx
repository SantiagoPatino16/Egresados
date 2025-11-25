using LogicBusiness.Service;
using System;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Presentation.Messaging.HUB;   // TU HUB

namespace LogicBusiness.Helpers
{
    public class UploadChatFile : IHttpHandler
    {
        private readonly MessageAttachmentsService attachmentsService;

        public UploadChatFile()
        {
            attachmentsService = new MessageAttachmentsService();
        }

        public void ProcessRequest(HttpContext context)
        {
            var file = context.Request.Files["file"];
            var idEmisor = int.Parse(context.Request["idEmisor"]);
            var idReceptor = int.Parse(context.Request["idReceptor"]);

            if (file == null)
            {
                WriteJson(context, new { ok = false, msg = "No file." });
                return;
            }

            /* =============================
               1. VALIDACIÓN DE ARCHIVO
               ============================= */
            // Tamaño (20 MB)
            if (file.ContentLength > 20 * 1024 * 1024)
            {
                WriteJson(context, new { ok = false, msg = "File too big." });
                return;
            }

            // Tipos permitidos
            var mime = file.ContentType.ToLower();
            var allowed = new[]
            {
                "image/jpeg", "image/png", "image/gif",
                "application/pdf",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/vnd.ms-excel",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

            if (!allowed.Contains(mime))
            {
                WriteJson(context, new { ok = false, msg = "Invalid file type." });
                return;
            }

            /* =============================
               2. GUARDAR EN DISCO
               ============================= */
            var folder = "../Resources/chats/";
            var physical = context.Server.MapPath(folder);
            Directory.CreateDirectory(physical);

            var ext = Path.GetExtension(file.FileName).ToLower();
            var safeName = Guid.NewGuid().ToString("N") + ext;
            var path = Path.Combine(physical, safeName);
            file.SaveAs(path);

            var rutaPublica = folder + safeName;

            /* =============================
               3. GUARDAR EN BD
               ============================= */
            int idMensaje = attachmentsService.InsertarAdjunto(
                idEmisor,
                idReceptor,
                rutaPublica,
                mime
            );

            /* =============================
               4. NOTIFICAR VIA SIGNALR
               ============================= */
            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

            hub.Clients.Group(idReceptor.ToString()).adjuntoRecibido(
                idMensaje,
                rutaPublica,
                mime
            );

            /* =============================
               5. RESPUESTA AL CLIENTE
               ============================= */
            WriteJson(context, new
            {
                ok = true,
                idMensaje,
                ruta = rutaPublica,
                mime
            });
        }

        public bool IsReusable => false;

        private void WriteJson(HttpContext ctx, object obj)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
    }
}




