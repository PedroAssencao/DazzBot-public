using Chatbot.Infrastructure.Meta.Repository.Interfaces;
using Chatbot.Infrastructure.Meta.Services;
using Chatbot.Services.Meta.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Services.Meta.Extensions
{
    public static class AddServicesMetaExtensions
    {
        public static void AddServicesMetaExtension(this IServiceCollection services)
        {
            services.AddScoped<IMetaClientServices, MetaClientServices>();
            services.AddScoped<MetodoCheckServices>();
        }
    }
}
