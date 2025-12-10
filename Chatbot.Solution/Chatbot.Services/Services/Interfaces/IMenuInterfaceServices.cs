using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Services.Interfaces
{
    public interface IMenuInterfaceServices : IBaseInterfaceServices<MenuDttoGet>
    {
        Task<List<MenuDttoGet>> PegarTodosOsMenusPorLogID(int id);
        Task<MenuDttoGet> PegarMenuPorLogIDEMenuInicial(int id);
        Task<MenuDttoGet> AdicionarPost(MenuDttoPost menu);
        Task<MenuDttoGet> AtualizarPut(MenuDttoPut menu);
        Task<MenuDttoGet> PegarMenuInicialPorLogId(int logId);
        Task<MenuDttoGet> PegarMenuDeIaPorLogId(int logId);
        Task<MenuDttoGet> PegarMenuPorOptionId(int OptId);
    }
}
