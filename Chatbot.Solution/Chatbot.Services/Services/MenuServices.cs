using Chatbot.Domain.Models;
using Chatbot.Domain.Models.Enums;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Services.Services.Interfaces;

namespace Chatbot.Services.Services
{
    public class MenuServices : IMenuInterfaceServices
    {
        protected readonly IMenuInterface _repository;
        protected readonly ILoginInterfaceServices _loginService;
        protected readonly IOptionInterfaceServices _optionService;

        public MenuServices(IMenuInterface repository, ILoginInterfaceServices loginService, IOptionInterfaceServices optionService)
        {
            _repository = repository;
            _loginService = loginService;
            _optionService = optionService;
        }

        public async Task<List<MenuDttoGet>> GetALl()
        {
            try
            {
                var dados = await _repository.GetALl();
                List<MenuDttoGet> List = new List<MenuDttoGet>();
                foreach (var item in dados)
                {
                    MenuDttoGet Model = new MenuDttoGet
                    {
                        Codigo = item.MenId,
                        Titulo = item.MenTitle,
                        Header = item.MenHeader,
                        Body = item.MenBody,
                        Footer = item.MenFooter,
                        Tipo = item.MenTipo,
                        Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                        Options = await _optionService.GetPorIdForMenu(item.MenId)
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

        public async Task<MenuDttoGet?> PegarMenuInicialPorLogId(int logId)
        {
            try
            {
                var dados = await GetALl();
                return dados.FirstOrDefault(x => x.Tipo == ETipoMenu.PrimeiraMensagem && x?.Login?.Codigo == logId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MenuDttoGet> PegarMenuDeIaPorLogId(int logId)
        {
            try
            {
                var dados = await GetALl();
                return dados.FirstOrDefault(x => x.Tipo == ETipoMenu.MenuDaIa && x?.Login?.Codigo == logId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MenuDttoGet> PegarMenuPorOptionId(int OptId)
        {
            try
            {
                var option = await _optionService.GetPorId(OptId);
                if (option == null)
                {
                    return null;
                }
                var dados = await PegarTodosOsMenusPorLogID(option.Login.Codigo);
                int menId = 0;
                try
                {
                    menId = Convert.ToInt32(option.Resposta);
                }
                catch (Exception)
                {
                    return null;
                }
                return dados.FirstOrDefault(x => x.Codigo == menId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MenuDttoGet> GetPorId(int id)
        {
            try
            {
                var item = await _repository.GetPorId(id);
                MenuDttoGet Model = new MenuDttoGet();
                if (item != null)
                {
                    Model = new MenuDttoGet
                    {
                        Codigo = item.MenId,
                        Titulo = item.MenTitle,
                        Header = item.MenHeader,
                        Body = item.MenBody,
                        Footer = item.MenFooter,
                        Tipo = item.MenTipo,
                        Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                        Options = await _optionService.GetPorIdForMenu(item.MenId)
                    };
                }

                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MenuDttoGet> PegarMenuPorLogIDEMenuInicial(int id)
        {
            try
            {
                var dados = await _repository.GetALl();
                var item = dados.First(x => x.LogId == id && x.MenTipo == ETipoMenu.PrimeiraMensagem);
                MenuDttoGet Model = new MenuDttoGet
                {
                    Codigo = item.MenId,
                    Titulo = item.MenTitle,
                    Header = item.MenHeader,
                    Body = item.MenBody,
                    Footer = item.MenFooter,
                    Tipo = item.MenTipo,
                    Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                    Options = await _optionService.GetPorIdForMenu(item.MenId)
                };
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MenuDttoGet>> PegarTodosOsMenusPorLogID(int id)
        {
            try
            {
                var dados = await _repository.GetALl();
                var DadosFiltrados = dados.Where(x => x.LogId == id).ToList();
                List<MenuDttoGet> List = new List<MenuDttoGet>();
                foreach (var item in DadosFiltrados)
                {
                    MenuDttoGet Model = new MenuDttoGet
                    {
                        Codigo = item.MenId,
                        Titulo = item.MenTitle,
                        Header = item.MenHeader,
                        Body = item.MenBody,
                        Footer = item.MenFooter,
                        Tipo = item.MenTipo,
                        Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                        Options = await _optionService.GetPorIdForMenu(item.MenId)
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

        public async Task<MenuDttoGet> AdicionarPost(MenuDttoPost Model)
        {
            try
            {
                Menu NewModel = new Menu
                {
                    MenBody = Model.Body,
                    MenFooter = Model.Footer,
                    MenHeader = Model.Header,
                    MenTipo = Model.Tipo,
                    MenTitle = Model.Titulo,
                    LogId = Convert.ToInt32(Model.CodigoLogin)
                };


                var item = await _repository.Adicionar(NewModel);
                MenuDttoGet ViewModel = new MenuDttoGet
                {
                    Codigo = item.MenId,
                    Titulo = item.MenTitle,
                    Header = item.MenHeader,
                    Body = item.MenBody,
                    Footer = item.MenFooter,
                    Tipo = item.MenTipo,
                    Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                    Options = await _optionService.GetPorIdForMenu(item.MenId)
                };
                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MenuDttoGet> AtualizarPut(MenuDttoPut Model)
        {
            try
            {
                Menu NewModel = new Menu
                {
                    MenId = Model.Codigo,
                    MenBody = Model.Body,
                    MenFooter = Model.Footer,
                    MenHeader = Model.Header,
                    MenTipo = Model.Tipo,
                    MenTitle = Model.Titulo,
                    LogId = Convert.ToInt32(Model.CodigoLogin)
                };
                var item = await _repository.update(NewModel);
                MenuDttoGet ViewModel = new MenuDttoGet
                {
                    Codigo = item.MenId,
                    Titulo = item.MenTitle,
                    Header = item.MenHeader,
                    Body = item.MenBody,
                    Footer = item.MenFooter,
                    Tipo = item.MenTipo,
                    Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                    Options = await _optionService.GetPorIdForMenu(item.MenId)
                };
                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MenuDttoGet> Delete(int id)
        {
            try
            {
                var item = await _repository.GetPorId(id);
                MenuDttoGet ViewModel = new MenuDttoGet
                {
                    Codigo = item.MenId,
                    Titulo = item.MenTitle,
                    Header = item.MenHeader,
                    Body = item.MenBody,
                    Footer = item.MenFooter,
                    Tipo = item.MenTipo,
                    Login = await _loginService.GetPorId(Convert.ToInt32(item.LogId)),
                    Options = await _optionService.GetPorIdForMenu(item.MenId)
                };

                foreach (var item1 in ViewModel.Options)
                {
                    await _optionService.Delete(item1.Codigo);
                }
                await _repository.delete(id);

                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodos que ficaram sobrando
        public async Task<MenuDttoGet> Create(MenuDttoGet Model)
        {
            throw new NotImplementedException();
        }
        public Task<MenuDttoGet> Update(MenuDttoGet Model)
        {
            throw new NotImplementedException();
        }
    }
}
