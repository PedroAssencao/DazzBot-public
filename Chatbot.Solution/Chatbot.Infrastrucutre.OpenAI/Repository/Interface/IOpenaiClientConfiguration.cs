using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastrucutre.OpenAI.Repository.Interface
{
    public interface IOpenaiClientConfiguration
    {
        void InitialVerification(string url);
        Task FinaleVerification(HttpResponseMessage response);
        public void InstanceClient();
        public void InstanceClient(string name);
        public void AddHeaders(Dictionary<string, string> headers);
        public Task<string> PostAsync(string url, HttpContent? request);
    }
}
