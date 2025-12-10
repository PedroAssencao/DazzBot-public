using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.Services.Interfaces
{
    public interface IBaseInterfaceServices<T> where T : class
    {
        Task<List<T>> GetALl();
        Task<T> GetPorId(int id);
        Task<T> Create(T Model);
        Task<T> Update(T Model);
        Task<T> Delete(int id);

    }
}
