using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        protected readonly IAtendimentoInterfaceServices _repository;

        public AtendimentoController(IAtendimentoInterfaceServices repository)
        {
            _repository = repository;
        }

        [HttpGet("Atendimento")]
        public async Task<IActionResult> BuscarTodosAtendimento()
        {
            try
            {
                return Ok(await _repository.GetALl());
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpGet("Atendimento/{id}")]
        public async Task<IActionResult> BuscarAtendimentoPorId(int id)
        {
            try
            {
                return Ok(await _repository.GetPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpGet("Atendimento/BuscarTodosPorLog/{id}")]
        public async Task<IActionResult> BuscarTodosAtendimentosPorLogId(int id)
        {
            try
            {
                return Ok(await _repository.RetornarTodosAtendimentosPorLogId(id));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpPost("Atendimento/Create")]
        public async Task<IActionResult> AdicionarAtendimento(AtendimentoDttoPost Model)
        {
            try
            {
                return Ok(await _repository.AdicionarPost(Model));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpPut("Atendimento/Atualizar")]
        public async Task<IActionResult> AtualizarAtendimento(AtendimentoDttoPut Model)
        {
            try
            {
                return Ok(await _repository.AtualizarPut(Model));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }
        [HttpDelete("Atendimento/Delete")]
        public async Task<IActionResult> DeletarAtendimento(int id)
        {
            try
            {
                return Ok(await _repository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }
    }
}
