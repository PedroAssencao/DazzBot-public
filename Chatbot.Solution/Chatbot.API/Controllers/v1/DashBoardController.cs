using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Services.Interfaces;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Chatbot.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Chatbot.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IAtendenteInterfaceServices _atendente;
        private readonly IMensagemInterfaceServices _mensagem;
        private readonly IAtendimentoInterfaceServices _atendimento;
        private readonly IContatoInterfaceServices _contato;
        private readonly IDepartamentoInterfaceServices _departamento;

        public DashBoardController(IAtendenteInterfaceServices atendente, IMensagemInterfaceServices mensagem, IAtendimentoInterfaceServices atendimento, IContatoInterfaceServices contato, IDepartamentoInterfaceServices departamento)
        {
            _atendente = atendente;
            _mensagem = mensagem;
            _atendimento = atendimento;
            _contato = contato;
            _departamento = departamento;
        }

        [HttpGet("BuscarDadosDashBoard")]
        public async Task<IActionResult> PegarDadosDashboard(int logid)
        {
            try
            {
                List<AtendimentoDttoGet> atendimento = await _atendimento.RetornarTodosAtendimentosPorLogId(logid);
                List<AtendenteDttoGet> atendentes = await _atendente.BuscarTodosAtendentesObjPorLogId(logid);
                List<ContatoDttoGet> contatos = await _contato.GetListaDeContatosPorLogId(logid);
                List<MensagensDttoGet> mensagens = await _mensagem.retornarTodasMensagensPorLogId(logid);
                List<DepartamentoDttoGet> Departamentos = await _departamento.GetAllByLogId(logid);

                var atendimentosAtivos = atendimento
                    .Where(a => a.EstadoAtendimento == EEstadoAtendimento.HUMANO && a.Atendente != null)
                    .ToList();

                // Agrupar e contar atendimentos por atendente
                var atendimentosPorAtendente = atendimentosAtivos
                    .GroupBy(a => a.Atendente?.Codigo)
                    .Select(g => new
                    {
                        Nome = g?.First()?.Atendente?.Nome ?? "Desconhecido",
                        TotalAtendimentos = g.Count()
                    })
                    .OrderByDescending(x => x.TotalAtendimentos)
                    .ToList();

                // Pegue os 10 atendentes com mais atendimentos
                var topAtendentes = atendimentosPorAtendente.Take(10);

                // Soma dos atendimentos restantes
                var outrosAtendentes = new
                {
                    Nome = "Outros",
                    TotalAtendimentos = atendimentosPorAtendente.Skip(10).Sum(x => x.TotalAtendimentos)
                };

                var atendimentosPorDepartamento = atendimentosAtivos
                  .GroupBy(x => x.Departamento?.Codigo)
                  .Select(y => new
                  {
                      Nome = y.First().Departamento?.NomeDepartamento ?? "Desconhecido",
                      TotalAtendimentos = y.Count()
                  })
                  .OrderByDescending(x => x.TotalAtendimentos)
                  .ToList();


                var topDepartamentos = atendimentosPorDepartamento.Take(10);
                    
                var outrosDepartamentos = new
                {
                    Nome = "Outros",
                    TotalAtendimentos = atendimentosPorDepartamento.Skip(10).Sum(x => x.TotalAtendimentos)
                };

                var ContatosPorDia = contatos
                    .GroupBy(x => Convert.ToDateTime(x.DataCadastro).Date)
                    .Select(y => new
                    {
                        Nome = y.Key.ToString("yyyy-MM-dd") ?? "Desconhecido", // Data formatada
                        TotalContatos = y.Count() // Total de contatos naquele dia
                    })
                    .OrderBy(x => DateTime.Parse(x.Nome)) // Ordena pela data em ordem decrescente
                    .Take(7) // Pega os 7 primeiros
                    .ToList();

                var MensagensPorDia = mensagens
                    .GroupBy(x => Convert.ToDateTime(x.Data).Date)
                    .Select(y => new
                    {
                        Nome = y.Key.ToString("yyyy-MM-dd") ?? "Desconhecido",
                        TotalMensagens = y.Count()
                    })
                    .OrderBy(x => DateTime.Parse(x.Nome))
                    .Take(7)
                    .ToList();

                var AtendimentosPorDia = atendimento
                    .GroupBy(x => Convert.ToDateTime(x.Data).Date)
                    .Select(y => new
                    {
                        Nome = y.Key.ToString("yyyy-MM-dd") ?? "Desconhecido",
                        TotalAtendimentos = y.Count()
                    })
                    .OrderBy(x => DateTime.Parse(x.Nome))
                    .Take(7)
                    .ToList();

                dynamic Dados = new
                {
                    AtendentesDados = new
                    {
                        qtdAtendenteOnline = atendentes.Count(x => x.EstadoAtendente == true),
                        qtdTotalAtendentes = atendentes.Count()
                    },
                    MensagensDados = new
                    {
                        qtdTotalDeMensagens = mensagens.Count(),
                        DadosParaGraficoSecundarioMensagens = new
                        {
                            MensagensPorDia = MensagensPorDia
                        }
                    },
                    AtendimentoDados = new
                    {
                        qtdTotalAtendimento = atendimento.Count(),
                        qtdTotalAtendimentoAtivos = atendimentosAtivos.Count(),
                        qtdTotalAtendimentoEsperando = atendimento.Count(x => x.EstadoAtendimento == EEstadoAtendimento.HUMANO && x.Atendente == null),
                        DadosParaGraficoPrincipal01 = new
                        {
                            TopAtendentes = topAtendentes,
                            Outros = outrosAtendentes
                        },
                        DadosParaGraficoPrincipal02 = new
                        {
                            TopDepartamentos = topDepartamentos,
                            Outros = outrosDepartamentos
                        },
                        DadosParaGraficoSecundarioAtendimento = new
                        {
                            AtendimentosPorDia = AtendimentosPorDia
                        }
                    },
                    ContatosDados = new
                    {
                        qtdTotalDeContatos = contatos.Count(),
                        ContatosPorDia = new {
                            ContatosPorDia = ContatosPorDia
                        } 
                    }
                };

                return Ok(Dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocorreu um erro ao processar os dados.",
                    Erro = ex.Message
                });
            }
        }

    }
}
