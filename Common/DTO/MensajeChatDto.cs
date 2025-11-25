using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class MensajeChatDto
    {
        public int IdMensaje { get; set; }
        public int IdEmisor { get; set; }
        public string Contenido { get; set; }
        public string FechaEnvio { get; set; }
        public bool Leido { get; set; }

        public List<AdjuntoDto> Adjuntos { get; set; }     // ← DTO plano
        public List<ReaccionDto> Reacciones { get; set; }  // ← DTO plano
    }
}
