using Chatbot.API.Repository;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        protected readonly IChatsInterfaceServices _repository;

        public ChatController(IChatsInterfaceServices repository)
        {
            _repository = repository;
        }

        [HttpGet("chats")]
        public async Task<IActionResult> BuscarTodos() => Ok(await _repository.GetALl());

        [HttpGet("chats/{id}")]
        public async Task<IActionResult> BuscarPorID(int id) => Ok(await _repository.GetPorId(id));
        [HttpGet("chats/Get/BuscarTodosOsChatsPorLogId/{id}")]
        public async Task<IActionResult> BuscarTodosOSChatsPorLogId(int id)
        {
            try
            {
                return Ok(await _repository.RetornarTodosOsChatPorLogId(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("chats/create")]
        public async Task<IActionResult> Adicionar(ChatsDttoPost Model) => Ok(await _repository.AdicionarPost(Model));
        [HttpPut("chats/Atualizar")]
        public async Task<IActionResult> Atualizar(ChatsDttoPut Model) => Ok(await _repository.AtualziarPut(Model));

        [HttpDelete("chats/Delete")]
        public async Task<IActionResult> Delete(int id) => Ok(await _repository.Delete(id));

    }
}
