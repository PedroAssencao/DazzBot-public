using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;

namespace Chatbot.Services.Services.Interfaces
{
    public interface IDepartamentoInterfaceServices : IBaseInterfaceServices<DepartamentoDttoGet>
    {
        Task<List<DepartamentoDttoGet>> GetAllByLogId(int id);
    }
}
