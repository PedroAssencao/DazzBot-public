using Chatbot.Infrastructure.Dtto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Dtto.Meta
{
    public class MensagemMultiplaEscolhaDataAndType
    {
        public string? MenuJson { get; set; }
        public MensagensDttoGet? Model { get; set; }
    }
}
