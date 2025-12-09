using Chatbot.Infrastructure.Services.Interfaces;
using Chatbot.Test.Chatbot.Mock;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Test.Chatbot.Infrastructure.Test.Get
{

    public class ContatosServicesTesta : IClassFixture<ChatbotConnection>
    {
        private readonly IContatoInterfaceServices _services;

        public ContatosServicesTesta(ChatbotConnection application)
        {
            ChatbotMockDate.CreateDates(application, true).Wait();
            var scope = application.Services.CreateScope();
            _services = scope.ServiceProvider.GetRequiredService<IContatoInterfaceServices>();
        }

        [Fact]
        public async Task BuscarTodosContatosServices()
        {
            try
            {
                var result = await _services.GetALl();
                Assert.True(result.Count > 0);
                Assert.True(result.Count == 2);
                Assert.True(result.First().Codigo > 0);
                Assert.True(result.First().Nome == "Pedro Assenção");
                Assert.True(result.First().BloqueadoStatus == false);
                Assert.True(result.First().Codigologin == 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarTodosContatos, error: " + ex.Message);
            }
        }

    }
}
