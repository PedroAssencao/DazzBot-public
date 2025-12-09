using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartamanetoController : ControllerBase
    {
        protected readonly IDepartamentoInterfaceServices _repository;
        protected readonly IAtendenteInterfaceServices _atendente;

        public DepartamanetoController(IDepartamentoInterfaceServices repository, IAtendenteInterfaceServices atendente)
        {
            _repository = repository;
            _atendente = atendente;
        }

        [HttpGet("Departamento")]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                return Ok(await _repository.GetALl());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Departamento/{id}")]
        public async Task<IActionResult> BuscarDepartamnetoPorId(int id)
        {
            try
            {
                return Ok(await _repository.GetPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Departamento/Create")]
        public async Task<IActionResult> AdicionarDepartamento(DepartamentoDttoGet Model)
        {
            try
            {
                return Ok(await _repository.Create(Model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Departamento/Update")]
        public async Task<IActionResult> AtualizarDepartamento(DepartamentoDttoGet Model)
        {
            try
            {
                return Ok(await _repository.Update(Model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Departamento/BuscarTodosDepartamentoPorLogId")]
        public async Task<IActionResult> BuscarTodosDepartamentosPorLogId(int id)
        {
            try
            {
                return Ok(await _repository.GetAllByLogId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Departamento/Delete")]
        public async Task<IActionResult> ApagarDepartamento(int id)
        {
            //não essa pratica não e boa tem que ser refatorada depois, eu sei
            try
            {
                if (await _atendente.ExisteAtendenteVinculaoAoDepartamento(id))
                {
                    throw new Exception("Departamento esta vinculado a algum atendente");
                }
                return Ok(await _repository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
