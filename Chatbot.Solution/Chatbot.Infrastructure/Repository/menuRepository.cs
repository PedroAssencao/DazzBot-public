using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;

namespace Chatbot.API.Repository
{
    public class menuRepository : BaseRepository<Menu>, IMenuInterface
    {
        public menuRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }

        public async Task<List<Menu>> GetALl() => await GetAll();
        public async Task<Menu> GetPorId(int id) => await GetPorID(id);
        public async Task<Menu> Create(Menu Model) => await Adicionar(Model);
        public async Task<Menu> update(Menu Model) => await Update(Model);
        public async Task<Menu> delete(int id) => await Delete(id);
    }
}
