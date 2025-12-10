using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;

namespace Chatbot.API.Repository
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoInterface
    {
        public DepartamentoRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }

        public async Task<List<Departamento>> GetALl() => await GetAll();
        public async Task<Departamento> GetPorId(int id) => await GetPorID(id);
        public async Task<Departamento> Create(Departamento Model) => await Adicionar(Model);
        public async Task<Departamento> update(Departamento Model) => await Update(Model);
        public async Task<Departamento> delete(int id) => await Delete(id);
    }
}
