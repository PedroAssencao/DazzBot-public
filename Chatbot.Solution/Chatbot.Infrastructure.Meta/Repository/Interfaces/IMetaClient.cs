using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;

namespace Chatbot.Infrastructure.Meta.Repository.Interfaces
{
    public interface IMetaClient
    {
        public HttpClient ConfigurarClient(string token, string url);
        public Task<string> PostAsync(string url, string token, dynamic data);
        public Task<dynamic> MensagemInicial(DataAndType Model);
        public Task<dynamic> BotResposta(DataAndType Model);
        public Task<dynamic> ChamarMetodo(dynamic Values);
        Task CompararData();
        Task SalvarMensagemAtendente(string descricao, int? chat, int? ate);
        public Task<string> EnvioDeMensagensEmMassa(List<ContatoDttoGet> Contatos, string conteudo);
    }
}
