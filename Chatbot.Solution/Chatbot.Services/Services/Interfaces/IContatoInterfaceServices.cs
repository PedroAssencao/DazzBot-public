using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.Services.Interfaces
{
    public interface IContatoInterfaceServices : IBaseInterfaceServices<ContatoDttoGet>
    {
        public Task<ContatoDttoGet> RetornarConIdPorWaID(string waid);
        public Task<ContatoDttoGetForView> GetContatoForViewPorId(int id);
        public Task<ContatoDttoGet> CreateComCodigo(ContatoDttoGet Model);
        public Task<ContatoDttoGet> ContatoIsNull(DataAndType dados, LoginDttoGet Login);
        public Task<List<ContatoDttoGet>> GetListaDeContatosBloqueadosPorLogId(int logId);
        public Task<List<ContatoDttoGet>> GetListaDeContatosPorLogId(int logId);
        public Task<ContatoDttoGet> SetContatoBloqueado(int conId, bool estado);
    }
}
