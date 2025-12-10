using Chatbot.Infrastrucutre.OpenAI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {
        private readonly IOpenaiRequest _repository;
        private readonly IConfiguration _configuration;
        public OpenAiController(IOpenaiRequest repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        //[HttpGet]
        //public async Task<IActionResult> teste()
        //{
        //    try
        //    {
        //        return Ok(await _repository.PostAsync(_configuration.GetSection("AES").Value));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
