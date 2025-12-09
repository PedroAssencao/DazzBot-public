using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Services.Interfaces
{
    public interface IAtendenteInterfaceServices : IBaseInterfaceServices<AtendenteDttoGet>
    {
        Task<AtendenteDttoGet> AdicionarPost(AtendenteDttoForPost Model);
        Task<AtendenteDttoGet> UpdatePost(AtendenteDttoForPut Model);
        Task<List<dynamic>> BuscarTodosAtendentesPorLogId(int id);
        public Task<List<AtendenteDttoGet>> BuscarTodosAtendentesObjPorLogId(int id);
        public Task<bool> ExisteAtendenteVinculaoAoDepartamento(int depId);
    }
}
