using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;

namespace Chatbot.API.Repository
{
    public class AtendimentoRepository : BaseRepository<Atendimento>, IAtendimentoInterface
    {
        public AtendimentoRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }

        public async Task<List<Atendimento>> GetALl() => await GetAll();
        public async Task<Atendimento> GetPorId(int id) => await GetPorID(id);
        public async Task<Atendimento> Create(Atendimento Model) => await Adicionar(Model);
        public async Task<Atendimento> update(Atendimento Model) => await Update(Model);
        public async Task<Atendimento> delete(int id) => await Delete(id);
    }
}
