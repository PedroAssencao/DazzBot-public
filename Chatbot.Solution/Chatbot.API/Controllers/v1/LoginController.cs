using Chatbot.Infrastructure.Dtto;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly ILoginInterfaceServices _repository;

        public LoginController(ILoginInterfaceServices repository)
        {
            _repository = repository;
        }
        [HttpGet("login")]
        public async Task<IActionResult> BuscarTodosLogin()
        {
            try
            {
                return Ok(await _repository.GetALl());
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }
        [HttpGet("login/GetClaimsInfo")]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tipoUsuario = User.FindFirst(ClaimTypes.Role)?.Value;
            var LogId = User.FindFirst(ClaimTypes.GivenName)?.Value;

            return Ok(new { UsuarioLogadoId = userId, UserName = userName , TipoUsuario = tipoUsuario, IdUsuarioCliente = LogId });
        }   

        [HttpGet("login/{id}")]
        public async Task<IActionResult> BuscarTodosLoginPorId(int id)
        {
            try
            {
                return Ok(await _repository.GetPorIdSenhaDescriptografada(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }

        [HttpPost("login/Cadastrar")]
        public async Task<IActionResult> Cadastrar(LoginDttoGet Model)
        {
            try
            {
                return Ok(await _repository.Cadastrar(Model));
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }


        [HttpPost("login/logar")]
        public async Task<IActionResult> Logar([FromBody]LoginDttoPost Model, bool Iscadastrate)
        {
            try
            {
                var result = await _repository.Logar(Model, Iscadastrate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }

        [HttpPut("login/Atualizar")]
        public async Task<IActionResult> AtualizarLogin(LoginDttoGet Model)
        {
            try
            {
                return Ok(await _repository.Update(Model));
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }

        [HttpDelete("login/Delete")]
        public async Task<IActionResult> ApagarLogin(int id)
        {
            try
            {
                return Ok(await _repository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }

        [HttpPost("login/Logout")]
        public async Task<IActionResult> logout()
        {
            try
            {
                return Ok(await _repository.Logout());
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception("error: " + ex.Message));
            }
        }
    }
}
