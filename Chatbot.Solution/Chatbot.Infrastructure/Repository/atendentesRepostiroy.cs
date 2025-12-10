using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;

namespace Chatbot.API.Repository
{
    public class atendentesRepostiroy : BaseRepository<Atendente>, IAtendeteInterface
    {
        public atendentesRepostiroy(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }

        public async Task<List<Atendente>> GetALl() => await GetAll();
        public async Task<Atendente> GetPorId(int id) => await GetPorID(id);
        public async Task<Atendente> Create(Atendente Model) => await Adicionar(Model);
        public async Task<Atendente> update(Atendente Model) => await Update(Model);
        public async Task<Atendente> delete(int id) => await Delete(id);
    }
}
