using Chatbot.Services.Extensions;
using Chatbot.Infrastructure.Extensions;
using Chatbot.Services.Meta.Extensions;
using Chatbot.Infrastructure.Meta.Extensions;
using Chatbot.Infrastrucutre.OpenAI.Extensions;
using Chatbot.Infrastructure.Meta.Repository.SignalRForChat;
using Microsoft.OpenApi.Models;
namespace Chatbot.API.Extensions
{
    public static class Startup
    {
        public static void StartConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chatbot API",
                    Version = "v1",
                    Description = "API para gerenciar o chatbot"
                });
            });
            services.AddSignalR();
            services.AddHostedService<VerificarAtendimentoService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5173")
                                //.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.StartConfiguration();
            services.AddRepositoryStartUp(configuration);
            services.AddServicesSetup();
            services.AddAuthorization();
            services.ConfigureServicesMeta();
            services.ConfigureServicesOpenAi();
        }

        public static void ConfigureServicesMeta(this IServiceCollection services)
        {
            services.AddServicesMetaExtension();
            services.AddRepositoryMetaStartUp();
        }

        public static void ConfigureServicesOpenAi(this IServiceCollection services)
        {
            services.AddInfraOpenAiExtension();
        }

        public static void Configure(this WebApplication app)
        {
            app.UseSwagger();

            // Configurar Swagger UI como página inicial
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatbot API v1");
                options.RoutePrefix = string.Empty; // Carregar Swagger na raiz "/"
            });
            app.UseCors("AllowLocalhost");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<ChatHub>("api/chatHub");
        }

    }
}
