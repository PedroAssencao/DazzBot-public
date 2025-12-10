using Chatbot.Infrastructure.Meta.Repository;
using Chatbot.Infrastructure.Meta.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Infrastructure.Meta.Extensions
{

    public static class AddMetaRepositoryExtensions
    {
        public static void AddRepositoryMetaStartUp(this IServiceCollection services)
        {
            services.AddScoped<IMetodoCheck,MetodoCheckRepository>();
            services.AddScoped<IMetaClient,MetaRepository>();
        }

    }

}
