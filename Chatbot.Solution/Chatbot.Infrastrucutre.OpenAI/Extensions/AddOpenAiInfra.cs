using Chatbot.Infrastrucutre.OpenAI.Repository;
using Chatbot.Infrastrucutre.OpenAI.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Infrastrucutre.OpenAI.Extensions
{
    public static class AddOpenAiInfra
    {
        public static void AddInfraOpenAiExtension(this IServiceCollection services)
        {
            services.AddScoped<IOpenaiClientConfiguration, OpenaiClientConfigurationRepository>();
            services.AddScoped<IOpenaiRequest, OpenaiRequestRepository>();
            services.AddHttpClient();
        }
    }
}
