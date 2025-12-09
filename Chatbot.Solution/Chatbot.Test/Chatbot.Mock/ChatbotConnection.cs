using Chatbot.API.DAL;
using Chatbot.API.Repository;
using Chatbot.Infrastructure.Interfaces;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Infrastructure.Services;
using Chatbot.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Chatbot.Test.Chatbot.Mock
{
    public class ChatbotConnection : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<chatbotContext>));
                services.AddDbContext<chatbotContext>(options =>
                {
                    options.UseInMemoryDatabase("DataBase", root);

                    options.ConfigureWarnings(warnings =>
                    {
                        warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
                    });
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
                services.AddScoped<IContatoInterfaceServices, ContatoServices>();
            });



            return base.CreateHost(builder);
        }
    }
}