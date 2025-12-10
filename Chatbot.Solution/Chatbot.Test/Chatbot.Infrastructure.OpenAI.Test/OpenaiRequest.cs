using Chatbot.Infrastrucutre.OpenAI.Repository;
using Chatbot.Infrastrucutre.OpenAI.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Test.Chatbot.Infrastructure.OpenAI.Test
{
    public class OpenaiRequest
    {
        private readonly IOpenaiRequest _services;
        private readonly string Token = "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";

        public OpenaiRequest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IOpenaiRequest, OpenaiRequestRepository>();
            serviceCollection.AddTransient<IOpenaiClientConfiguration, OpenaiClientConfigurationRepository>();
            serviceCollection.AddHttpClient();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _services = serviceProvider.GetRequiredService<IOpenaiRequest>();
        }

        [Fact]
        public async Task FazerRequisicaoComSucessoParaOpenAi()
        {
            //Arrange
            var comand = "Me Resposnda somete a palavra 'teste' e no final coloque .123 tudo junto e minusculo e quero que a resposta seja extritamente igual a 'teste.123'";
           
            //Act
            var result = await _services.PostAsync(Token, comand);

            //Assert
            Assert.Equal("teste.123", result);
        }

        [Fact]
        public async Task FazerRequisicaoComSucessoParaOpenAiGeracaoRandomizada()
        {
            //Arrange
            var comand = "Me Resposnda qualquer coisa diferente da palavra 'teste' e no final coloque .123 tudo junto e minusculo e quero que a resposta seja extritamente diferente de 'teste.123'";
      
            //Act
            var result = await _services.PostAsync(Token, comand);

            //Assert
            Assert.NotEqual("teste.123", result);
        }
    }
}
