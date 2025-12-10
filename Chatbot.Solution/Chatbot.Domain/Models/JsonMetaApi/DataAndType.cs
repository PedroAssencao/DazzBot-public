using Chatbot.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    public class DataAndType
    {
        public ETipoRetornoJson Tipo { get; set; }
        public dynamic? Dados { get; set; }
    }
}
