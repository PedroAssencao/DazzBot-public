using Chatbot.Domain.Models.Enums;

namespace Chatbot.Infrastructure.Dtto
{
    public class MensagensDttoGetForView
    {
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public ETipoStatusMensagem? StatusDaMensagen { get; set; }
        public string? MensagemWaId { get; set; }
        public ContatoDttoGetForView? Contato { get; set; }
    }

    public class MensagensDttoGet
    {
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public ETipoMensagem? TipoDaMensagem { get; set; }
        public int CodigoChat { get; set; }
        public string? CodigoWhatsapp { get; set; }
        public ETipoStatusMensagem? StatusDaMensagen { get; set; }
        public ContatoDttoGetForView? Contato { get; set; }
        public LoginDttoGetForView? Login { get; set; }
    }
    public class MensagensDttoPost
    {        
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public ETipoMensagem? TipoDaMensagem { get; set; }
        public string? CodigoWhatsapp { get; set; }
        public ETipoStatusMensagem? StatusDaMensagen { get; set; }
        public int? CodigoContato { get; set; }
        public int? CodigoLogin { get; set; }
        public int? CodigoChat { get; set; }
    }

    public class MensagensDttoPut
    {
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public ETipoMensagem? TipoDaMensagem { get; set; }
        public string? CodigoWhatsapp { get; set; }
        public ETipoStatusMensagem? StatusDaMensagen { get; set; }
        public int? CodigoContato { get; set; }
        public int? CodigoLogin { get; set; }
        public int? CodigoChat { get; set; }
    }



}
