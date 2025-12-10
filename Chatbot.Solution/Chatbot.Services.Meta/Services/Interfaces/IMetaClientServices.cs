using Chatbot.Infrastructure.Dtto;
using System.Text.Json;

namespace Chatbot.Infrastructure.Meta.Repository.Interfaces
{
    public interface IMetaClientServices
    {
        public Task<dynamic> MAIN(JsonElement Values);
        public HttpClient ConfigurarClientServices(string token, string url);
        public Task<string> PostAsyncServices(string url, string token, dynamic data);
        Task SalvarMensagemAtendente(string descricao,int chat, int ate);
        public Task<string> EnvioDeMensagensEmMassaServices(List<ContatoDttoGet> Contatos, string conteudo);
    }
}
