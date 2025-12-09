using Chatbot.Domain.Models.Enums;

namespace Chatbot.Infrastructure.Dtto
{
    public class OptionDttoGet
    {
        public int Codigo { get; set; }
        public int? CodigoMenu { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public bool? Finalizar { get; set; }
        public string? Resposta { get; set; }
        public ETiposDeOptions? Tipo { get; set; }
        public string? Titulo { get; set; }
        public LoginDttoGetForView? Login { get; set; }
    }
    public class OptionDttoPost
    {        
        public int? CodigoMenu { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public bool? Finalizar { get; set; }
        public string? Resposta { get; set; }
        public ETiposDeOptions Tipo { get; set; }
        public string? Titulo { get; set; }
        public int? CodigoLogin { get; set; }
    }

    public class OptionDttoPut
    {
        public int Codigo { get; set; }
        public int? CodigoMenu { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public bool? Finalizar { get; set; }
        public string? Resposta { get; set; }
        public ETiposDeOptions Tipo { get; set; }
        public string? Titulo { get; set; }
        public int? CodigoLogin { get; set; }
    }

    public class OptionDttoGetForMenu
    {
        public int Codigo { get; set; }
        public int? CodigoMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Resposta { get; set; }
        public ETiposDeOptions? Tipo { get; set; }
        public bool? Finalizar { get; set; }
    }

}
