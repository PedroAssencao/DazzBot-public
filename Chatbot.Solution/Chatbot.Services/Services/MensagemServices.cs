using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Domain.Models.Enums;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Infrastructure.Services.Interfaces;
using Chatbot.Services.Services.Interfaces;

namespace Chatbot.Services.Services
{
    public class MensagemServices : IMensagemInterfaceServices
    {
        protected readonly IMensagemInterface _repository;
        protected readonly IContatoInterfaceServices _contatoRepository;
        protected readonly ILoginInterfaceServices _loginInterfaceRepository;

        public MensagemServices(IMensagemInterface repository, IContatoInterfaceServices contatoRepository, ILoginInterfaceServices loginInterfaceRepository)
        {
            _repository = repository;
            _contatoRepository = contatoRepository;
            _loginInterfaceRepository = loginInterfaceRepository;
        }

        public async Task<List<MensagensDttoGet>> GetALl()
        {
            try
            {
                var dados = await _repository.GetALl();
                List<MensagensDttoGet> list = new List<MensagensDttoGet>();
                foreach (var item in dados)
                {
                    MensagensDttoGet Model = new MensagensDttoGet
                    {
                        Codigo = item.MensId,
                        Data = item.MensData,
                        TipoDaMensagem = item.MenTipo,
                        Descricao = item.MensDescricao,
                        CodigoChat = Convert.ToInt32(item.ChaId),
                        CodigoWhatsapp = item.mensWaId,
                        StatusDaMensagen = item.mensStatus,
                        Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                        Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
                    };
                    list.Add(Model);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MensagensDttoGet>> retornarTodasMensagensPorLogId(int id)
        {
            var dados = await GetALl();
            return dados.Where(x => x?.Login?.Codigo == id).ToList();         
        }

        public async Task<MensagensDttoGet> GetPorId(int id)
        {
            try
            {
                var item = await _repository.GetPorId(id);
                MensagensDttoGet Model = new MensagensDttoGet
                {
                    Codigo = item.MensId,
                    Data = item.MensData,
                    TipoDaMensagem = item.MenTipo,
                    Descricao = item.MensDescricao,
                    CodigoChat = Convert.ToInt32(item.ChaId),
                    CodigoWhatsapp = item.mensWaId,
                    StatusDaMensagen = item.mensStatus,
                    Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                    Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
                };
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MensagensDttoGetForView>> BuscarMensagensDeUmChat(int cha, int log)
        {
            try
            {
                var dados = await _repository.GetALl();
                var dadosFiltrados = dados.Where(x => x.ChaId == cha && x.MenTipo == ETipoMensagem.MensagemEnviada).ToList();
                List<MensagensDttoGetForView> list = new List<MensagensDttoGetForView>();
                foreach (var item in dadosFiltrados)
                {
                    MensagensDttoGetForView Model = new MensagensDttoGetForView
                    {
                        Codigo = item.MensId,
                        Data = item.MensData,
                        Descricao = item.MensDescricao,
                        StatusDaMensagen = item.mensStatus,
                        MensagemWaId = item.mensWaId,
                        Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId))
                    };
                    list.Add(Model);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MensagensDttoGet?> BuscarMensagemPorWaId(string waID)
        {
            try
            {
                var dados = await GetALl();
                var model = dados.FirstOrDefault(x => x.CodigoWhatsapp == waID);
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MensagensDttoGet?> PegarUltimaMensagemDeUmContatoPorLogConWaIdEConWaId(string ConWaID, string LogConWaID)
        {
            try
            {
                var dados = await GetALl();
                var item = dados.LastOrDefault(x => x?.Contato?.CodigoWhatsapp == ConWaID && x?.Login?.CodigoWhatsapp == LogConWaID && x.TipoDaMensagem == ETipoMensagem.MensagemEnviada);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MensagensDttoGet> AdicionarPost(MensagensDttoPost Model)
        {
            try
            {
                Mensagen NewModel = new Mensagen
                {
                    MensData = Model.Data,
                    MensDescricao = Model.Descricao,
                    MenTipo = Model.TipoDaMensagem,
                    mensWaId = Model.CodigoWhatsapp,
                    mensStatus = Model.StatusDaMensagen,
                    ChaId = Model.CodigoChat,
                    LogId = Model.CodigoLogin,
                    ConId = Model.CodigoContato
                };
                var item = await _repository.Adicionar(NewModel);
                MensagensDttoGet ViewModel = new MensagensDttoGet
                {
                    Codigo = item.MensId,
                    Data = item.MensData,
                    TipoDaMensagem = item.MenTipo,
                    Descricao = item.MensDescricao,
                    CodigoWhatsapp = item.mensWaId,
                    StatusDaMensagen = item.mensStatus,
                    CodigoChat = item.ChaId == null ? 0 : Convert.ToInt32(item.ChaId),
                    Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                    Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
                };
                return ViewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<MensagensDttoGetForView?> SaveMensageWithCodigoWhatsappId(LoginDttoGet Login, ContatoDttoGet contato, ChatsDttoGet chat, string descricao, string CodigoWhatsapp)
        {
            //metodo feito apenas para salvar a mensagem recebida caso passe em todas as verificações iniciais
            try
            {
                MensagensDttoPost NewModel = new MensagensDttoPost
                {
                    CodigoLogin = Login.Codigo,
                    CodigoContato = contato.Codigo,
                    CodigoChat = chat.Codigo,
                    CodigoWhatsapp = CodigoWhatsapp,
                    StatusDaMensagen = ETipoStatusMensagem.delivered,
                    Data = DateTime.Now,
                    Descricao = descricao,
                    TipoDaMensagem = ETipoMensagem.MensagemEnviada
                };
                var result = await AdicionarPost(NewModel);
                MensagensDttoGetForView Model = new MensagensDttoGetForView
                {
                    Codigo = result.Codigo,
                    Contato = result?.Contato,
                    Data = result?.Data,
                    StatusDaMensagen = result?.StatusDaMensagen,
                    Descricao = result?.Descricao
                };
                return Model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<MensagensDttoGetForView?> SaveMensage(int Login, int chat, string descricao)
        {
            //metodo feito apenas para salvar a mensagem recebida caso passe em todas as verificações iniciais
            try
            {
                MensagensDttoPost NewModel = new MensagensDttoPost
                {
                    CodigoLogin = Login,
                    CodigoContato = null,
                    CodigoChat = chat,
                    Data = DateTime.Now,
                    Descricao = descricao,
                    StatusDaMensagen = ETipoStatusMensagem.delivered,
                    TipoDaMensagem = ETipoMensagem.MensagemEnviada
                };
                var result = await AdicionarPost(NewModel);
                MensagensDttoGetForView Model = new MensagensDttoGetForView
                {
                    Codigo = result.Codigo,
                    Contato = result?.Contato,
                    Data = result?.Data,
                    StatusDaMensagen = result?.StatusDaMensagen,
                    Descricao = result?.Descricao
                };
                return Model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MensagensDttoGet> AtualizarPut(MensagensDttoPut Model)
        {
            try
            {
                Mensagen NewModel = new Mensagen
                {
                    MensId = Model.Codigo,
                    MensData = Model.Data,
                    MensDescricao = Model.Descricao,
                    MenTipo = Model.TipoDaMensagem,
                    mensWaId = Model.CodigoWhatsapp,
                    mensStatus = Model.StatusDaMensagen,
                    ChaId = Model.CodigoChat,
                    LogId = Model.CodigoLogin,
                    ConId = Model.CodigoContato
                };
                var item = await _repository.update(NewModel);
                MensagensDttoGet ViewModel = new MensagensDttoGet
                {
                    Codigo = item.MensId,
                    Data = item.MensData,
                    TipoDaMensagem = item.MenTipo,
                    Descricao = item.MensDescricao,
                    CodigoWhatsapp = item.mensWaId,
                    StatusDaMensagen = item.mensStatus,
                    CodigoChat = Convert.ToInt32(item.ChaId),
                    Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                    Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
                };
                return ViewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MensagensDttoGet> Delete(int id)
        {
            try
            {
                var item = await _repository.GetPorId(id);

                MensagensDttoGet ViewModel = new MensagensDttoGet
                {
                    Codigo = item.MensId,
                    Data = item.MensData,
                    TipoDaMensagem = item.MenTipo,
                    Descricao = item.MensDescricao,
                    CodigoWhatsapp = item.mensWaId,
                    StatusDaMensagen = item.mensStatus,
                    CodigoChat = Convert.ToInt32(item.ChaId),
                    Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                    Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
                };
                await _repository.delete(ViewModel.Codigo);
                return ViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //não utilizados
        public Task<MensagensDttoGet> Create(MensagensDttoGet Model)
        {
            throw new NotImplementedException();
        }

        public Task<MensagensDttoGet> Update(MensagensDttoGet Model)
        {
            throw new NotImplementedException();
        }

        public async Task<MensagensDttoGet>? UltimaMensagem()
        {
            var item = _repository.UltimaMensagem();
            MensagensDttoGet Model = new MensagensDttoGet
            {
                Codigo = item.MensId,
                Data = item.MensData,
                TipoDaMensagem = item.MenTipo,
                Descricao = item.MensDescricao,
                CodigoChat = Convert.ToInt32(item.ChaId),
                CodigoWhatsapp = item.mensWaId,
                StatusDaMensagen = item.mensStatus,
                Contato = item.ConId == null ? null : await _contatoRepository.GetContatoForViewPorId(Convert.ToInt32(item.ConId)),
                Login = item.LogId == null ? null : await _loginInterfaceRepository.GetPorIdLoginView(Convert.ToInt32(item.LogId)),
            };
            return Model;
        }

        public async Task UpdateWithDirectiveDbContext(MensagensDttoGet Model)
        {
            Mensagen mensagen = new Mensagen
            {
                MensId = Model.Codigo,
                mensWaId = Model?.CodigoWhatsapp,
                ChaId = Model?.CodigoChat,
                ConId = Model?.Contato?.Codigo,
                LogId = Model?.Login?.Codigo,
                MensDescricao = Model?.Descricao,
                MensData = Model?.Data,
                mensStatus = Model?.StatusDaMensagen,
                MenTipo = Model?.TipoDaMensagem
            };
            using (var newContext = new chatbotContext())
            {
                newContext.Mensagens.Update(mensagen);
                await newContext.SaveChangesAsync();
            }
        }

        public async Task<ChatsDttoGet> MarcarMensagensComoLida(ChatsDttoGet Models)
        {
            try
            {
                List<MensagensDttoGetForView> Model = new List<MensagensDttoGetForView>();
                foreach (var item in Models.Mensagens)
                {
                    if (item.StatusDaMensagen != ETipoStatusMensagem.read && item.Contato != null)
                    {
                        MensagensDttoPut newmodel = new MensagensDttoPut
                        {
                            Codigo = item.Codigo,
                            Data = item.Data,
                            CodigoContato = item?.Contato?.Codigo,
                            Descricao = item?.Descricao,
                            StatusDaMensagen = ETipoStatusMensagem.read,
                            CodigoChat = Models?.Codigo,
                            CodigoLogin = Models?.Atendimento?.Login?.Codigo,
                            CodigoWhatsapp = item?.MensagemWaId,
                            TipoDaMensagem = ETipoMensagem.MensagemEnviada
                        };
                        await AtualizarPut(newmodel);
                        item.StatusDaMensagen = ETipoStatusMensagem.read;
                        Model.Add(item);
                    }
                    else
                    {
                        Model.Add(item);
                    }

                }
                Models.Mensagens = Model;
                return Models;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
