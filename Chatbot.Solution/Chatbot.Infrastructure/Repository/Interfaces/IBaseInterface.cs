
namespace Chatbot.Infrastructure.Interfaces
{
    public interface IBaseInterface<T> where T : class
    {
        Task<List<T>> GetALl();  
        Task<T> GetPorId(int id);
        Task<T> Adicionar(T Model);
        Task<T> update(T Model);
        Task<T> delete(int id);
        public T? UltimaEntidadeManipuladaEntity();
    }
}
