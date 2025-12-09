
using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.Services.Interfaces
{
    public interface IChatsInterfaceServices : IBaseInterfaceServices<ChatsDttoGet>
    {
        Task<ChatsDttoGetForMensagens> BuscarChatParaMensagen(int id);
        Task<ChatsDttoGet> AdicionarPost(ChatsDttoPost Model);
        Task<ChatsDttoGet> AtualziarPut(ChatsDttoPut Model);
        public Task<ChatsDttoGet?> RetornarChatPorAtenId(int atenId);
        public Task<ChatsDttoGet> ChatIsNull(DataAndType dados, AtendimentoDttoGet Item);
        public Task<ChatsDttoGet?> RetornarChatPorConIdELogId(int? conId, int? LogId);
        Task<List<ChatsDttoGet>> RetornarTodosOsChatPorLogId(int? logId);
    }
}
