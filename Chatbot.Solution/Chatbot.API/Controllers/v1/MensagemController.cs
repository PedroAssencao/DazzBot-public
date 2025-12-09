using Chatbot.API.Repository;
using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        protected readonly IMensagemInterfaceServices _repository;

        public MensagemController(IMensagemInterfaceServices repository)
        {
            _repository = repository;
        }

        [HttpGet("mensagens")]
        public async Task<IActionResult> PegarTodasMensagens() => Ok(await _repository.GetALl());
        [HttpGet("mensagens/chat")]
        public async Task<IActionResult> PegarMensagensParaChat(int con, int log) => Ok(await _repository.BuscarMensagensDeUmChat(con, log));
        [HttpPost("mensagens/Create")]
        public async Task<IActionResult> CriarMensagem(MensagensDttoPost Model) => Ok(await _repository.AdicionarPost(Model));
        [HttpPut("mensagens/Atualizar")]
        public async Task<IActionResult> AtualizarMensagens(MensagensDttoPut Model) => Ok(await _repository.AtualizarPut(Model));
        [HttpPut("mensagens/MarcarMensagensComoLida")]
        public async Task<IActionResult> AtualizarLidoMensagens(ChatsDttoGet Models) { return Ok(await _repository.MarcarMensagensComoLida(Models)); }
        [HttpDelete("mensagens/Delete")]
        public async Task<IActionResult> ApagarMensagens(int id) => Ok(await _repository.Delete(id));
    }
}
