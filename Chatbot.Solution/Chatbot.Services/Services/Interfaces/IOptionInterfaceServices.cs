using Chatbot.API.Repository;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Services.Interfaces
{
    public interface IOptionInterfaceServices : IBaseInterfaceServices<OptionDttoGet>
    {
        Task<List<OptionDttoGetForMenu>> GetPorIdForMenu(int id);
        Task<OptionDttoGet> AdicionarPost (OptionDttoPost post);
        Task<OptionDttoGet> AtualizarPost (OptionDttoPut post);
        Task DeleteOptionCascade(int menuId, int optId, bool teste);

    }
}
