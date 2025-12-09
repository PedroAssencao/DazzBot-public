using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Test.Chatbot.Mock;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Test.Chatbot.Infrastructure.Test.Get
{
    public class LoginRepositoryTest : IClassFixture<ChatbotConnection>
    {
        private readonly ILoginInterface _repository;

        public LoginRepositoryTest(ChatbotConnection application)
        {
            ChatbotMockDate.CreateDates(application, true).Wait();
            var scope = application.Services.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<ILoginInterface>();
        }

        [Fact]
        public async Task BuscarTodosLogin()
        {
            try
            {
                var result = await _repository.GetALl();
                Assert.True(result.Count > 0);
                Assert.True(result.First().LogId > 0);
                Assert.True(result.First().LogId == 1);
                Assert.True(result.First().LogUser == "master");
                Assert.True(new LoginDttoGet().DescriptografaSenha(result?.First()?.LogSenha) == "senai.123");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarTodosLogin, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarLoginPorLogId(int logId)
        {
            try
            {
                var result = await _repository.GetPorId(logId);
                Assert.True(result != null);
                Assert.True(result.LogId > 0);
                Assert.True(result.LogId == 1);
                Assert.True(result.LogUser == "master");
                Assert.True(new LoginDttoGet().DescriptografaSenha(result?.LogSenha) == "senai.123");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarLoginPorLogId, error: " + ex.Message);
            }
        }

        [Fact]
        public async Task AtualizarLogin()
        {
            try
            {
                await _repository.update(new Login {
                    LogId = 1,
                    LogUser = "Usuario.Atualizado",
                    LogSenha = "teste"
                });
                var result = await _repository.GetALl();
                Assert.True(result.First().LogSenha == "teste");
                Assert.True(result.First().LogUser == "Usuario.Atualizado");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste AtualizarLogin, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task DeletarLogin(int logId)
        {
            try
            {
                await _repository.delete(logId);
                var result = await _repository.GetALl();
                Assert.True(result.Count == 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste DeletarLogin, error: " + ex.Message);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarUltimaEntidadeRastreadaEntityFrameWork(int logId)
        {
            try
            {
                await _repository.GetPorId(logId);
                var result = _repository.UltimaEntidadeManipuladaEntity();
                Assert.True(result?.LogId == 1);
                Assert.True(result.LogUser == "master");
                Assert.True(new LoginDttoGet().DescriptografaSenha(result?.LogSenha) == "senai.123");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um error ao tentar Executar o Teste BuscarUltimaEntidadeRastreadaEntityFrameWork, error: " + ex.Message);
            }
        }

    }
}
