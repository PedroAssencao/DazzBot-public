using Chatbot.Domain.Models.Enums;
using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Services.Interfaces
{
    public interface IAtendimentoInterfaceServices : IBaseInterfaceServices<AtendimentoDttoGet>
    {
        public Task<List<AtendimentoDttoGet>> RetornarTodosAtendimentosPorLogId(int id);
        public Task<AtendimentoDttoGet> AdicionarPost(AtendimentoDttoPost Model);
        public Task<AtendimentoDttoGet> AtualizarPut(AtendimentoDttoPut Model);
        public Task<AtendimentoDttoGet?> ResgatarAtendimentoPorLogIdEContatoWaId(string ConWaId, int LogWaId);
        public Task<AtendimentoDttoGet> AtendimentoExiste(LoginDttoGet login, ContatoDttoGet contato);
        public Task AtualizarAtendimentoComDttoDeGet(AtendimentoDttoGet Atendimento);
        public Task AtualizarEstadoAtendimento(AtendimentoDttoGet IsAtendimentoGoing, EEstadoAtendimento estado, int? codDep, int? codAte);
        public Task<AtendimentoDttoGet> AtendimentoIsNull(DataAndType dados, ContatoDttoGet contato, LoginDttoGet Login);
    }
}
