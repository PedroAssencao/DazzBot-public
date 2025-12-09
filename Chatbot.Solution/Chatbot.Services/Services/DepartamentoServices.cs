using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Services.Services.Interfaces;

namespace Chatbot.Services.Services
{
    public class DepartamentoServices : IDepartamentoInterfaceServices
    {
        protected readonly IDepartamentoInterface _repository;

        public DepartamentoServices(IDepartamentoInterface repository)
        {
            _repository = repository;
        }

        public async Task<List<DepartamentoDttoGet>> GetALl()
        {
            try
            {
                var dados = await _repository.GetALl();
                List<DepartamentoDttoGet> List = new List<DepartamentoDttoGet>();
                foreach (var item in dados)
                {
                    DepartamentoDttoGet Model = new DepartamentoDttoGet
                    {
                        Codigo = item.DepId,
                        NomeDepartamento = item.DepDescricao,
                        CodigoLogin = Convert.ToInt32(item.LogId)
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

        public async Task<List<DepartamentoDttoGet>> GetAllByLogId(int id)
        {
            try
            {
                var dados = await GetALl();
                return dados.Where(x => x.CodigoLogin == id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DepartamentoDttoGet> GetPorId(int id)
        {
            try
            {
                var dados = await _repository.GetPorId(id);
                DepartamentoDttoGet Model = new DepartamentoDttoGet
                {
                    Codigo = dados.DepId,
                    NomeDepartamento = dados.DepDescricao,
                    CodigoLogin = Convert.ToInt32(dados.LogId)
                };
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DepartamentoDttoGet> Create(DepartamentoDttoGet Model)
        {
            try
            {
                Departamento NewModel = new Departamento
                {
                    DepDescricao = Model.NomeDepartamento,
                    LogId = Model.CodigoLogin
                };
                await _repository.Adicionar(NewModel);
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DepartamentoDttoGet> Update(DepartamentoDttoGet Model)
        {
            try
            {
                Departamento NewModel = new Departamento
                {
                    DepId = Convert.ToInt32(Model.Codigo),
                    DepDescricao = Model.NomeDepartamento,
                    LogId = Model.CodigoLogin
                };
                await _repository.update(NewModel);
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DepartamentoDttoGet> Delete(int id)
        {
            try
            {
                var Model = await _repository.delete(id);
                DepartamentoDttoGet NewModel = new DepartamentoDttoGet
                {
                    Codigo = Model.DepId,
                    NomeDepartamento = Model.DepDescricao,
                    CodigoLogin = Convert.ToInt32(Model.LogId)
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
