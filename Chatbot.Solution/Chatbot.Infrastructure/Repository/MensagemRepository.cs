using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Repository.Interfaces;
using System.Linq;

namespace Chatbot.API.Repository
{
    public class MensagemRepository : BaseRepository<Mensagen>, IMensagemInterface
    {
        public MensagemRepository(chatbotContext chatbotContext) : base(chatbotContext)
        {
        }
        
        public async Task<List<Mensagen>> GetALl() => await GetAll();
        public async Task<Mensagen> GetPorId(int id) => await GetPorID(id);
        public async Task<Mensagen> Create(Mensagen Model) => await Adicionar(Model);
        public async Task<Mensagen> update(Mensagen Model) => await Update(Model);
        public async Task<Mensagen> delete(int id) => await Delete(id);
        public Mensagen? UltimaMensagem() => UltimaEntidadeManipuladaEntity();
    }
    

}
