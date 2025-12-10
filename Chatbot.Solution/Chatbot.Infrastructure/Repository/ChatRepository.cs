using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Interfaces;

namespace Chatbot.API.Repository
{
    public class ChatRepository : BaseRepository<Chat>, IChatsInterface
    {
  
        public ChatRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
            
        }

        public async Task<List<Chat>> GetALl() => await GetAll();
        public async Task<Chat> GetPorId(int id) => await GetPorID(id);
        public async Task<Chat> Create(Chat Model) => await Adicionar(Model);
        public async Task<Chat> update(Chat Model) => await update(Model);
        public async Task<Chat> delete(int id) => await delete(id);

    }
}
