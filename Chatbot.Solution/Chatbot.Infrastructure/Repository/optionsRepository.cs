using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;
using System.Linq;

namespace Chatbot.API.Repository
{
    public class optionsRepository : BaseRepository<Option>, IOptionsInterface
    {
        public optionsRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }

        public async Task<List<Option>> GetALl() => await GetAll();
        public async Task<Option> GetPorId(int id) => await GetPorID(id);
        public async Task<Option> Create(Option Model) => await Adicionar(Model);
        public async Task<Option> update(Option Model) => await Update(Model);
        public async Task<Option> delete(int id) => await Delete(id);
    }
}
