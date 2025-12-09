using Chatbot.API.DAL;
using Chatbot.API.Repository;
using Chatbot.Infrastructure.Interfaces;
using Chatbot.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Infrastructure.Extensions
{
    public static class AddRepositoryExtensions
    {
        public static void AddRepositoryStartUp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<chatbotContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Chinook"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });
            services.AddTransient<IContatosInterface, ContatoRepository>();
            services.AddScoped<IMensagemInterface, MensagemRepository>();            
            services.AddScoped<ILoginInterface, LoginRepository>();
            services.AddScoped<IAtendimentoInterface, AtendimentoRepository>();
            services.AddScoped<IAtendeteInterface, atendentesRepostiroy>();
            services.AddScoped<IDepartamentoInterface, DepartamentoRepository>();
            services.AddScoped<IOptionsInterface, optionsRepository>();
            services.AddScoped<IChatsInterface, ChatRepository>();
            services.AddScoped<IMenuInterface, menuRepository>();
        }
    }
}
