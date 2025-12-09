using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Test.Chatbot.Mock;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Test.Chatbot.Infrastructure.Test.Get
{

    public class ContatoServicesTest : IClassFixture<ChatbotConnection>
    {
        private readonly IContatosInterface _repository;

        public ContatoServicesTest(ChatbotConnection application)
        {
            ChatbotMockDate.CreateDates(application, true).Wait();
            var scope = application.Services.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<IContatosInterface>();
        }

        [Fact]
        public async Task BuscarTodosContatos()
        {
            try
            {
                var result = await _repository.GetALl();
                Assert.True(result.Count > 0);
                Assert.True(result.Count == 2);
                Assert.True(result.First().ConId > 0);
                Assert.True(result.First().ConNome == "Pedro Assenção");
                Assert.True(result.First().ConBloqueadoStatus == false);
                Assert.True(result.First().LogId == 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarTodosContatos, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarContatosPorConId(int conId)
        {
            try
            {
                var result = await _repository.GetPorId(conId);
                Assert.True(result != null);
                Assert.True(result.ConId > 0);
                Assert.True(result.ConNome == "Pedro Assenção");
                Assert.True(result.ConBloqueadoStatus == false);
                Assert.True(result.LogId == 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarContatosPorConId, error: " + ex.Message);
            }
        }

        [Fact]
        public async Task AtualizarContato()
        {
            try
            {
                await _repository.update(new Contato
                {
                    ConId = 1,
                    ConNome = "Contato.Atualizado",
                    ConWaId = "teste.ContatoWaId"
                });
                var result = await _repository.GetALl();
                Assert.True(result.First().ConNome == "Contato.Atualizado");
                Assert.True(result.First().ConWaId == "teste.ContatoWaId");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste AtualizarContato, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task DeletarContato(int conId)
        {
            try
            {
                await _repository.delete(conId);
                var result = await _repository.GetALl();
                Assert.True(result.Count == 1);
                Assert.True(result.First().ConNome == "BABABA");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste DeletarContato, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarUltimaEntidadeRastreadaEntityFrameWork(int conId)
        {
            try
            {
                await _repository.GetPorId(conId);
                var result = _repository.UltimaEntidadeManipuladaEntity();
                Assert.True(result?.LogId == 1);
                Assert.True(result.ConNome == "Pedro Assenção");
                Assert.True(result.ConId == 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarUltimaEntidadeRastreadaEntityFrameWork, error: " + ex.Message);
            }
        }

    }
}
