using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        protected readonly IOptionInterfaceServices _repository;
        protected readonly IMenuInterfaceServices _menu;

        public OptionController(IOptionInterfaceServices repository, IMenuInterfaceServices menu)
        {
            _repository = repository;
            _menu = menu;
        }

        [HttpGet("Option")]
        public async Task<IActionResult> BuscarTodasOption()
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

        [HttpGet("Option/{id}")]
        public async Task<IActionResult> BuscarOptionPorId(int id)
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

        [HttpPost("Option/Create")]
        public async Task<IActionResult> AdicionarOption(OptionDttoPost Model)
        {
            try
            {
                //essa logica aqui e funcional porem não fica no padrão de projeto dar uma atenção a isso aqui futuramente
                if (Model != null)
                {
                    if (Model?.CodigoMenu != null)
                    {
                        var menu = await _menu.GetPorId(Convert.ToInt32(Model?.CodigoMenu));
                        if (menu != null)
                        {
                            if (menu?.Options?.Count >= 10)
                            {
                                throw new Exception("Numero de opções por menu execedida");
                            }
                        }
                    }
                }

                return Ok(await _repository.AdicionarPost(Model));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpPut("Option/Update")]
        public async Task<IActionResult> AtualizarOption(OptionDttoPut Model)
        {
            try
            {
                return Ok(await _repository.AtualizarPost(Model));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpDelete("Option/Delete")]
        public async Task<IActionResult> ApagarOption(int optid)
        {
            try
            {
                return Ok(await _repository.Delete(optid));
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

        [HttpDelete("Option/DeleteOptionCascade")]
        public async Task<IActionResult> ApagarOpDeleteOptionCascadetion(int menid,int optid)
        {
            try
            {
                await _repository.DeleteOptionCascade(menid, optid, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("error: " + ex.Message);
            }
        }

    }
}
