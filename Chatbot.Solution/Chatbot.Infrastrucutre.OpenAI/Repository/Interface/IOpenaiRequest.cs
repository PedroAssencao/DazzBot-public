using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastrucutre.OpenAI.Repository.Interface
{
    public interface IOpenaiRequest
    {
        Task<string> PostAsync(string Authorization, string comando);
    }
}
