using LogicBusiness.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

            var folder = "Resources/chats/";
            var physical = context.Server.MapPath(folder);

            Directory.CreateDirectory(physical);

            var name = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(physical, name);
            file.SaveAs(path);

            var rutaPublica = folder + name;

            // Guardar en BD (crear Mensaje con Tipo = adjunto)
            int idMensaje = attachmentsService.InsertarAdjunto(idEmisor, idReceptor, rutaPublica, file.ContentType);

            WriteJson(context, new
            {
                ok = true,
                idMensaje,
                ruta = rutaPublica,
                mime = file.ContentType
            });
        }

        public bool IsReusable => false;

        void WriteJson(HttpContext ctx, object obj)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
    }
}
