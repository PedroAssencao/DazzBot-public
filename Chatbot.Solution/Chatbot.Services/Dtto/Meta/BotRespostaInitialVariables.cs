using Chatbot.Infrastructure.Dtto;

namespace Chatbot.Services.Dtto.Meta
{
    public class BotRespostaInitialVariables
    {
        public string? descricaoDaMensagem { get; set; }
        public string? codigoMensagem { get; set; }
        public string? name { get; set; }
        public string? numero { get; set; }
        public LoginDttoGet? Login { get; set; }
        public AtendimentoDttoGet? Atendimento { get; set; }
        public OptionDttoGet? OptionSelecionada { get; set; }
        public MenuDttoGet? MenuSelecionadoOption { get; set; }
        public ChatsDttoGet? chat { get; set; }
    }
}
