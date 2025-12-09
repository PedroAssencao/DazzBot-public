using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AtendenteController : ControllerBase
    {
        protected readonly IAtendenteInterfaceServices _repository;

        public AtendenteController(IAtendenteInterfaceServices repository)
        {
            _repository = repository;
        }

        [HttpGet("Atendente")]
        public async Task<IActionResult> BuscarTodosAtendentes()
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

        [HttpGet("Atendente/BuscarTodosAtendentePorLogId")]
        public async Task<IActionResult> BuscarTodosAtendentesDeUmLogId(int id)
        {
            try
            {
                return Ok(await _repository.BuscarTodosAtendentesPorLogId(id));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpGet("Atendente/{id}")]
        public async Task<IActionResult> BuscarAtendendePorId(int id)
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

        [HttpPost("Atendente/Create")]
        public async Task<IActionResult> AdicionarAtendente(AtendenteDttoForPost Model)
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

        [HttpPut("Atendente/Atualizar")]
        public async Task<IActionResult> AtualizarAtendente(AtendenteDttoForPut Model)
        {
            try
            {
                return Ok(await _repository.UpdatePost(Model));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpDelete("Atendente/Remove")]
        public async Task<IActionResult> ApagarAtendente([FromQuery]int id)
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
