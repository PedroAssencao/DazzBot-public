using Chatbot.API.DAL;
using Microsoft.EntityFrameworkCore;
namespace Chatbot.API.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly chatbotContext _chatbotContext;
        public BaseRepository(chatbotContext chatbotContext)
        {
            _chatbotContext = chatbotContext;
        }

        public async Task<List<T>> GetAll() => await _chatbotContext.Set<T>().ToListAsync();

        public async Task<T?> GetPorID(int id) => await _chatbotContext.Set<T>().FindAsync(id);

        public async Task<T> Adicionar(T Model) 
        {
            try
            {
                await _chatbotContext.AddAsync(Model);
                await _chatbotContext.SaveChangesAsync();
                return Model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> Update(T model)
        {
            _chatbotContext.Update(model);
            await _chatbotContext.SaveChangesAsync();
            return model;
        }

        public async Task<T?> Delete(int id)
        {
            
            try
            {
                var Dados = await GetPorID(id);
                if (Dados != null)
                {
                    _chatbotContext.Remove(Dados);
                    await _chatbotContext.SaveChangesAsync();
                    return Dados;
                }
                else
                {
                    throw new Exception();
                }
             
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T? UltimaEntidadeManipuladaEntity() => _chatbotContext.ChangeTracker.Entries<T>().LastOrDefault()?.Entity;
    }
}
