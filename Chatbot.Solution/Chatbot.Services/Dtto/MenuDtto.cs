
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.Domain.Models.Enums;

namespace Chatbot.Infrastructure.Dtto
{
    public class MenuDttoGet
    {
        public int Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Header { get; set; }
        public string? Body { get; set; }
        public string? Footer { get; set; }
        public ETipoMenu? Tipo { get; set; }
        public LoginDttoGet? Login { get; set; }
        public List<OptionDttoGetForMenu>? Options { get; set; }
    }
    public class MenuDttoPost
    {
        public string? Titulo { get; set; }
        public string? Header { get; set; }
        public string? Body { get; set; }
        public string? Footer { get; set; }
        public ETipoMenu? Tipo { get; set; }
        public int? CodigoLogin { get; set; }
    }

    public class MenuDttoPut
    {
        public int Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Header { get; set; }
        public string? Body { get; set; }
        public string? Footer { get; set; }
        public ETipoMenu? Tipo { get; set; }
        public int? CodigoLogin { get; set; }
    }

    public class MenuDttoGetForView
    {
        public int MenId { get; set; }
        public string? MenTitle { get; set; }
        public string? MenHeader { get; set; }
        public string? MenBody { get; set; }
        public string? MenFooter { get; set; }
        public ETipoMenu? MenTipo { get; set; }
        public LoginDttoGetForView? Login { get; set; }
        public List<OptionDttoGetForMenu>? Options { get; set; }
    }
}
