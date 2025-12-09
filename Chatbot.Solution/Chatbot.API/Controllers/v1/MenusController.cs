using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        protected readonly IMenuInterfaceServices _repository;

        public MenusController(IMenuInterfaceServices repository)
        {
            _repository = repository;
        }

        [HttpGet("Menus")]
        public async Task<IActionResult> BuscarTodosMenus()
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

        [HttpGet("Menus/{id}")]
        public async Task<IActionResult> BuscarMenusPorId(int id)
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

        [HttpGet("Menus/GetAllMenusByLogId/{id}")]
        public async Task<IActionResult> BuscarTodosOsMenusPorLogId(int id)
        {
            try
            {
                return Ok(await _repository.PegarTodosOsMenusPorLogID(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("Menus/MenuInicial/{id}")]
        public async Task<IActionResult> BuscarMenuInicial(int id)
        {
            try
            {
                return Ok(await _repository.PegarMenuPorLogIDEMenuInicial(id));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpPost("Menus/Create")]
        public async Task<IActionResult> AdicionarMenu(MenuDttoPost Model)
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

        [HttpPut("Menus/Atualizar")]
        public async Task<IActionResult> AtualizarMenu(MenuDttoPut Model)
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

        [HttpDelete("Menus/Delete")]
        public async Task<IActionResult> ApagarMenu(int id)
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
