using Chatbot.Domain.Models;
using Chatbot.Domain.Models.Enums;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Services.Services.Interfaces;

namespace Chatbot.Services.Services
{
    public class AtendenteServices : IAtendenteInterfaceServices
    {
        protected readonly IAtendeteInterface _repository;
        protected readonly IDepartamentoInterfaceServices _departamento;
        protected readonly ILoginInterfaceServices _login;
        protected readonly IAtendimentoInterface _atendimentoInterface;
        public AtendenteServices(IAtendeteInterface repository, IDepartamentoInterfaceServices departamento, ILoginInterfaceServices login, IAtendimentoInterface atendimentoInterface)
        {
            _repository = repository;
            _departamento = departamento;
            _login = login;
            _atendimentoInterface = atendimentoInterface;
        }

        public async Task<List<AtendenteDttoGet>> GetALl()
        {
            try
            {
                var dados = await _repository.GetALl();
                List<AtendenteDttoGet> List = new List<AtendenteDttoGet>();
                foreach (var item in dados)
                {
                    AtendenteDttoGet ViewModel = new AtendenteDttoGet
                    {
                        Codigo = item.AteId,
                        Nome = item.AteNome,
                        Email = item.AteEmail,
                        Senha = item.AteSenha,
                        EstadoAtendente = item.AteEstado,
                        Departamento = item.DepId == null ? null : await _departamento.GetPorId(Convert.ToInt32(item.DepId)),
                        Login = item.LogId == null ? null : await _login.GetPorIdLoginView(Convert.ToInt32(item.LogId))
                    };
                    List.Add(ViewModel);
                }
                return List;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<dynamic>> BuscarTodosAtendentesPorLogId(int id)
        {
            try
            {
                //essa logica aqui e funcional porem não fica no padrão de projeto dar uma atenção a isso aqui futuramente
                var dados = await GetALl();
                var AtendimentoDados = await _atendimentoInterface.GetALl();
                List<dynamic> List = new List<dynamic>();
                foreach (var item in dados.Where(x => x.Login?.Codigo == id).ToList())
                {
                    var qtdFinalizado = AtendimentoDados.Where(x => x.AteId == item.Codigo && x.LogId == item?.Login?.Codigo && x?.AtenEstado == EEstadoAtendimento.Finalizado).Count();
                    var qtdAtivo = AtendimentoDados.Where(x => x.AteId == item.Codigo && x.LogId == item?.Login?.Codigo && x?.AtenEstado == EEstadoAtendimento.HUMANO).Count();
                    var qtdTotal = qtdFinalizado + qtdAtivo;
                    var Model = new
                    {
                        Atendente = item,
                        qtdFinalizado,
                        qtdAtivo,
                        qtdTotal
                    };
                    List.Add(Model);
                }

                return List;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AtendenteDttoGet>> BuscarTodosAtendentesObjPorLogId(int id)
        {
            try
            {
                //essa logica aqui e funcional porem não fica no padrão de projeto dar uma atenção a isso aqui futuramente
                var dados = await GetALl();
                return dados.Where(x => x?.Login?.Codigo == id).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AtendenteDttoGet> GetPorId(int id)
        {
            try
            {
                var dados = await _repository.GetPorId(id);

                AtendenteDttoGet Model = new AtendenteDttoGet
                {
                    Codigo = dados.AteId,
                    Nome = dados.AteNome,
                    Email = dados.AteEmail,
                    Senha = dados.AteSenha,
                    EstadoAtendente = dados.AteEstado,
                    Login = await _login.GetPorIdLoginView(Convert.ToInt32(dados.LogId)),
                    Departamento = await _departamento.GetPorId(Convert.ToInt32(dados.DepId)),
                };

                return Model;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> ExisteAtendenteVinculaoAoDepartamento(int depId)
        {
            try
            {
                var dados = await GetALl();
                return dados.Any(x => x?.Departamento?.Codigo == depId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteAtendenteVinculaoAoAtendimento(int AteId)
        {
            try
            {
                var dados = await _atendimentoInterface.GetALl();
                return dados.Any(x => x?.AteId == AteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AtendenteDttoGet> AdicionarPost(AtendenteDttoForPost Model)
        {
            try
            {
                Atendente NewModel = new Atendente
                {
                    AteNome = Model.Nome,
                    AteEmail = Model.Email,
                    AteSenha = Model.Senha,
                    AteImg = Model.Imagem,
                    AteEstado = Model.EstadoAtendente,
                    DepId = Model.CodigoDepartamento,
                    LogId = Model.CodigoLogin
                };
                var dados = await _repository.Adicionar(NewModel);
                AtendenteDttoGet ViewModel = new AtendenteDttoGet
                {
                    Codigo = dados.AteId,
                    Nome = dados.AteNome,
                    EstadoAtendente = dados.AteEstado,
                    Departamento = dados.DepId == null ? null : await _departamento.GetPorId(Convert.ToInt32(dados.DepId)),
                    Login = dados.LogId == null ? null : await _login.GetPorIdLoginView(Convert.ToInt32(dados.LogId))
                };
                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AtendenteDttoGet> UpdatePost(AtendenteDttoForPut Model)
        {
            try
            {
                Atendente NewModel = new Atendente
                {
                    AteId = Model.Codigo,
                    AteNome = Model.Nome,
                    AteEmail = Model.Email,
                    AteSenha = Model.Senha,
                    AteImg = Model.Imagem,
                    AteEstado = Model.EstadoAtendente,
                    DepId = Model.CodigoDepartamento,
                    LogId = Model.CodigoLogin
                };
                var dados = await _repository.update(NewModel);
                AtendenteDttoGet ViewModel = new AtendenteDttoGet
                {
                    Codigo = dados.AteId,
                    Nome = dados.AteNome,
                    EstadoAtendente = dados.AteEstado,
                    Departamento = dados.DepId == null ? null : await _departamento.GetPorId(Convert.ToInt32(dados.DepId)),
                    Login = dados.LogId == null ? null : await _login.GetPorIdLoginView(Convert.ToInt32(dados.LogId))
                };
                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AtendenteDttoGet> Delete(int id)
        {
            try
            {
                if (await ExisteAtendenteVinculaoAoAtendimento(id))
                {
                    throw new Exception("Existe um ou mais atendimentos vinculados ao atendente");
                }

                var dados = await _repository.delete(id);
                AtendenteDttoGet ViewModel = new AtendenteDttoGet
                {
                    Codigo = dados.AteId,
                    Nome = dados.AteNome,
                    EstadoAtendente = dados.AteEstado,
                    Departamento = dados.DepId == null ? null : await _departamento.GetPorId(Convert.ToInt32(dados.DepId)),
                    Login = dados.LogId == null ? null : await _login.GetPorIdLoginView(Convert.ToInt32(dados.LogId))
                };
                return ViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //ver oque fazer depois com esses metodos que ficaramSobrando
        public Task<AtendenteDttoGet> Create(AtendenteDttoGet Model)
        {
            throw new NotImplementedException();
        }

        public Task<AtendenteDttoGet> Update(AtendenteDttoGet Model)
        {
            throw new NotImplementedException();
        }
    }
}
