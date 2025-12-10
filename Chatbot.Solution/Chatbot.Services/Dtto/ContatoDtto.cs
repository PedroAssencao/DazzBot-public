
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.Dtto
{
    public class ContatoDttoGetForView
    {
        public int Codigo { get; set; }
        public string? CodigoWhatsapp { get; set; }
        public string? Nome { get; set; }
    }
    public class ContatoDttoGet
    {
        public int Codigo { get; set; }
        public string? CodigoWhatsapp { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool? BloqueadoStatus { get; set; }
        public int Codigologin { get; set; }

    }

}
