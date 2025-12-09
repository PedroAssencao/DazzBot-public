using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Interfaces;
using Chatbot.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Services.Interfaces
{
    public interface ILoginInterfaceServices : IBaseInterfaceServices<LoginDttoGet>
    {
        Task<LoginDttoGet> RetornarLogIdPorWaID(string waID);
        Task<LoginDttoGet> GetPorIdSenhaDescriptografada(int logid);
        Task<List<Claim>> Logar(LoginDttoPost Model, bool IsCadastre);
        Task<LoginDttoGet> Cadastrar(LoginDttoGet Model);
        Task<HttpStatusCode> Logout();
        Task<LoginDttoGetForView> GetPorIdLoginView(int id);
    }
}
