using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Domain.Models.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Test.Chatbot.Mock
{
    public class ChatbotMockDate
    {
        public static async Task CreateDates(ChatbotConnection application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var catalogDbContext = provider.GetRequiredService<chatbotContext>())
                {
                    await catalogDbContext.Database.EnsureDeletedAsync();
                    await catalogDbContext.Database.EnsureCreatedAsync();

                    if (create)
                    {
                        // Criação do Modelo Login
                        var login = new Login
                        {
                            LogEmail = "master.123@123",
                            LogSenha = "c2VuYWkuMTIz",
                            LogImg = "img-placeholder",
                            LogUser = "master",
                            LogPlano = "Master",
                            LogWaid = "15550882003"
                        };

                        await catalogDbContext.Logins.AddAsync(login);
                        await catalogDbContext.SaveChangesAsync();

                        var departamento = new Departamento
                        {
                            DepDescricao = "Suporte",
                            LogId = login.LogId
                        };

                        await catalogDbContext.Departamentos.AddAsync(departamento);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação do modelo Atendentes
                        var atendente = new Atendente
                        {
                            AteEmail = "emailTeste@gmail.com",
                            AteNome = "AtendenteTeste",
                            AteImg = "placeholder.img",
                            AteSenha = "atendente@123",
                            LogId = login.LogId,
                            DepId = departamento.DepId
                        };

                        await catalogDbContext.Atendentes.AddAsync(atendente);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação dos modelos Contatos
                        var contatos = new List<Contato>
                        {
                            new Contato
                            {
                                ConWaId = "557988132044",
                                ConNome = "Pedro Assenção",
                                ConDataCadastro = Convert.ToDateTime("2024-07-24 16:27:14.220"),
                                ConBloqueadoStatus = false,
                                LogId = login.LogId
                            },
                            new Contato
                            {
                                ConWaId = "557988132044",
                                ConNome = "BABABA",
                                ConDataCadastro = DateTime.Now,
                                ConBloqueadoStatus = false,
                                LogId = login.LogId
                            }
                        };

                        await catalogDbContext.Contatos.AddRangeAsync(contatos);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação dos modelos Menus
                        var menus = new List<Menu>
                        {
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Seja Bem Vindo ao Nosso Robo de Atendimento...",
                                MenTipo = ETipoMenu.PrimeiraMensagem,
                                MenTitle = "Menu Inicial",
                                LogId = login.LogId
                            },
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Por Favor Escolha Qual Parte das Finança Voce Esta Tendo Problemas",
                                MenTipo = ETipoMenu.MenuBot,
                                MenTitle = "Finanças",
                                LogId = 1
                            },
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Por Favor Escolha Qual Setor de Suporte Que Voce Deseja Ser Atendido",
                                MenTipo = ETipoMenu.MenuBot,
                                MenTitle = "Suporte",
                                LogId = 1
                            },
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Escolha Quais Das Opções Abaixo Descreve Melhor o Seu Problema",
                                MenTipo = ETipoMenu.MenuBot,
                                MenTitle = "Menu de Dificuldades ao Acessar o Sistema",
                                LogId = 1
                            },
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Escolha Quais Das Opções Abaixo Descreve Melhor o Seu Problema de Pagamento",
                                MenTipo = ETipoMenu.MenuBot,
                                MenTitle = "DificuldadePagar",
                                LogId = 1
                            },
                            new Menu
                            {
                                MenHeader = "Empresas Senai",
                                MenFooter = "Todos direitos reservados",
                                MenBody = "Escolha Quais Das Opções Abaixo e a Sua Vontade Se Tiver Mais Alguma Pergunta Apenas Pergunte!",
                                MenTipo = ETipoMenu.MenuDaIa,
                                MenTitle = "Menu IA",
                                LogId = 1
                            }
                        };

                        await catalogDbContext.Menus.AddRangeAsync(menus);
                        await catalogDbContext.SaveChangesAsync(); // Salve para garantir que os menus estão no banco

                        // Criação dos modelos Options
                        var options = new List<Option>
                        {
                            new Option
                            {
                                MenId = 1,
                                LogId = login.LogId,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Referente a Financeiro",
                                OptFinalizar = false,
                                OptResposta = "2",
                                OptTipo = ETiposDeOptions.MensagemDeRespostaInterativa,
                                OptTitle = "Financeiro"
                            },
                            new Option
                            {
                                MenId = 1,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Referente a Suporte",
                                OptFinalizar = false,
                                OptResposta = "3",
                                OptTipo = ETiposDeOptions.MensagemDeRespostaInterativa,
                                OptTitle = "Suporte"
                            },
                            new Option
                            {
                                MenId = 1,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "História do Senai Contada Pela IA e Interação Geral Com IA",
                                OptFinalizar = false,
                                OptResposta = null,
                                OptTipo = ETiposDeOptions.MensagemPorIA,
                                OptTitle = "História Senai"
                            },
                            new Option
                            {
                                MenId = 5,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Pagamento Não Disponível",
                                OptFinalizar = true,
                                OptResposta = "Sua Forma de Pagamento não está disponível no sistema? Use esse QR code para pagar diretamente: (exemploQRCode)",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Pagamento Indisponível"
                            },
                            new Option
                            {
                                MenId = 5,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Pagamento Não Autorizado",
                                OptFinalizar = true,
                                OptResposta = "Sinto Muito Pelo Transtorno, se possível, tente checar seu saldo para verificar se houve uma transação errônea.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Pagamento Não Autorizado"
                            },
                            new Option
                            {
                                MenId = 5,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Finalizar Atendimento",
                                OptFinalizar = true,
                                OptResposta = "Muito Obrigado Por Interagir.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Finalizar"
                            },
                            new Option
                            {
                                MenId = 4,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Esqueci Minha Senha",
                                OptFinalizar = true,
                                OptResposta = "Aqui está um link para preencher as informações para o reset da sua senha: (linkExemplo), espero que fique bem.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Esquecimento da Senha"
                            },
                            new Option
                            {
                                MenId = 4,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Instabilidade No Geral",
                                OptFinalizar = true,
                                OptResposta = "Lamentamos se o sistema está lento hoje, estamos em período de manutenção e voltaremos ao normal em breve.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Dificuldades Sistemas"
                            },
                            new Option
                            {
                                MenId = 4,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Finalizar Atendimento",
                                OptFinalizar = true,
                                OptResposta = "Obrigado por interagir, volte sempre.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Finalizar"
                            },
                            new Option
                            {
                                MenId = 2,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Dificuldades no Pagamento",
                                OptFinalizar = false,
                                OptResposta = "5",
                                OptTipo = ETiposDeOptions.MensagemDeRespostaInterativa,
                                OptTitle = "Pagamento"
                            },
                            new Option
                            {
                                MenId = 2,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Finalizar Atendimento",
                                OptFinalizar = true,
                                OptResposta = "Obrigado por interagir, espero que tenha conseguido resolver seu problema.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Finalizar"
                            },
                            new Option
                            {
                                MenId = 3,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Dificuldades com o Sistema",
                                OptFinalizar = false,
                                OptResposta = "4",
                                OptTipo = ETiposDeOptions.MensagemDeRespostaInterativa,
                                OptTitle = "Sistema"
                            },
                            new Option
                            {
                                MenId = 3,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Falar com Atendente do Setor de Suporte",
                                OptFinalizar = false,
                                OptResposta = "1",
                                OptTipo = ETiposDeOptions.RedirecinamentoHumano,
                                OptTitle = "Suporte Humano"
                            },
                            new Option
                            {
                                MenId = 3,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Finalizar Atendimento",
                                OptFinalizar = true,
                                OptResposta = "Obrigado por interagir, espero que tenha conseguido resolver seu problema.",
                                OptTipo = ETiposDeOptions.MensagemDeResposta,
                                OptTitle = "Finalizar"
                            },
                            new Option
                            {
                                MenId = 6,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Voltar ao Fluxo de Atendimento Normal",
                                OptFinalizar = false,
                                OptResposta = "Sim",
                                OptTipo = ETiposDeOptions.MensagemPorIA,
                                OptTitle = "Sim"
                            },
                            new Option
                            {
                                MenId = 6,
                                LogId = 1,
                                OptData = Convert.ToDateTime("2024-07-23T22:31:35.673"),
                                OptDescricao = "Finalizar o Atendimento",
                                OptFinalizar = true,
                                OptResposta = "Obrigado por interagir com o sistema!",
                                OptTipo = ETiposDeOptions.MensagemPorIA,
                                OptTitle = "Finalizar"
                            }
                        };

                        await catalogDbContext.Options.AddRangeAsync(options);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação dos modelos Atendimento
                        var atendimento = new Atendimento
                        {
                            AtenEstado = EEstadoAtendimento.Finalizado,
                            AtenData = Convert.ToDateTime("2024-10-16 16:15:07.987"),
                            AteId = atendente.AteId,
                            DepId = departamento.DepId,
                            ConId = contatos.First().ConId,
                            LogId = login.LogId
                        };

                        await catalogDbContext.Atendimentos.AddAsync(atendimento);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação dos modelos Chats
                        var chat = new Chat
                        {
                            AtenId = atendimento.AtenId,
                            AteId = atendente.AteId, 
                            ConId = contatos.First().ConId,
                            LogId = login.LogId 
                        };

                        await catalogDbContext.Chats.AddAsync(chat);
                        await catalogDbContext.SaveChangesAsync();

                        // Criação dos modelos Mensagens
                        var mensagens = new List<Mensagen>
                        {
                            new Mensagen
                            {
                                MensData = Convert.ToDateTime("2024-10-16 16:15:08.453"),
                                MensDescricao = "Finalizar Atendimento",
                                MenTipo = ETipoMensagem.MensagemEnviada,
                                mensWaId = "wamid.HBgMNTU3OTg4MTMyMDQ0FQIAEhgWM0VCMDBFMzlGQzREMzZFQjUwQzkyMgA=",
                                mensStatus = ETipoStatusMensagem.read,
                                ChaId = chat.ChaId,
                                ConId = contatos.First().ConId,
                                LogId = login.LogId
                            },
                        };

                        await catalogDbContext.Mensagens.AddRangeAsync(mensagens);
                        await catalogDbContext.SaveChangesAsync();

                    }
                }
            }
        }
    }
}
