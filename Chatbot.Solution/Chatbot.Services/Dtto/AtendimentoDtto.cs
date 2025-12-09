using Chatbot.Domain.Models.Enums;

namespace Chatbot.Infrastructure.Dtto
{
    public class AtendimentoDttoGet
    {
        public int Codigo { get; set; }
        public EEstadoAtendimento? EstadoAtendimento { get; set; }
        public DateTime? Data { get; set; }
        public AtendenteDttoGet? Atendente { get; set; }
        public DepartamentoDttoGet? Departamento { get; set; }
        public ContatoDttoGetForView? Contato { get; set; }
        public LoginDttoGetForView? Login { get; set; }
    }

    public class AtendimentoDttoPost
    {
        public EEstadoAtendimento? EstadoAtendimento { get; set; }
        public DateTime? Data { get; set; }
        public int? CodigoAtendente { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoContato { get; set; }
        public int? CodigoLogin { get; set; }
    }
    public class AtendimentoDttoPut
    {
        public int Codigo { get; set; }
        public EEstadoAtendimento? EstadoAtendimento { get; set; }
        public DateTime? Data { get; set; }
        public int? CodigoAtendente { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoContato { get; set; }
        public int? CodigoLogin { get; set; }
    }

}
