using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;

namespace Chatbot.API.Repository
{
    public class LoginRepository : BaseRepository<Login>, ILoginInterface
    {
        public LoginRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {

        }

        public async Task<List<Login>> GetALl() => await GetAll();
        public async Task<Login> GetPorId(int id) => await GetPorID(id);
        public async Task<Login> Create(Login Model) => await Adicionar(Model);
        public async Task<Login> update(Login Model) => await Update(Model);
        public async Task<Login> delete(int id) => await Delete(id);
    }
}
