
using AutoMapper;
using Chatbot.API.DAL;
using Chatbot.API.Repository;
using Chatbot.Domain.Models;
using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Serialization;

namespace Chatbot.Infrastructure.Services
{
    public class ContatoServices : IContatoInterfaceServices
    {
        protected readonly IContatosInterface _contatoRepository;
        public ContatoServices(IContatosInterface contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task<List<ContatoDttoGet>> GetALl()
        {
            try
            {
                var dados = await _contatoRepository.GetALl();
                List<ContatoDttoGet> List = new List<ContatoDttoGet>();
                foreach (var item in dados)
                {
                    ContatoDttoGet NewModel = new ContatoDttoGet
                    {
                        Codigo = item.ConId,
                        CodigoWhatsapp = item.ConWaId,
                        Nome = item.ConNome,
                        DataCadastro = item.ConDataCadastro,
                        BloqueadoStatus = item.ConBloqueadoStatus,
                        Codigologin = Convert.ToInt32(item.LogId),
                    };
                    List.Add(NewModel);
                }
                return List;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContatoDttoGet> SetContatoBloqueado(int conId, bool estado)
        {
            try
            {
                var item = await _contatoRepository.GetPorId(conId);
                item.ConBloqueadoStatus = estado;
                await _contatoRepository.update(item);
                ContatoDttoGet NewModel = new ContatoDttoGet
                {
                    Codigo = item.ConId,
                    CodigoWhatsapp = item.ConWaId,
                    Nome = item.ConNome,
                    DataCadastro = item.ConDataCadastro,
                    BloqueadoStatus = item.ConBloqueadoStatus,
                    Codigologin = Convert.ToInt32(item.LogId),
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContatoDttoGet> ContatoIsNull(DataAndType dados, LoginDttoGet Login)
        {
            //se o contato não existir esse metodo vai crialo
            try
            {
                ContatoDttoGet newModel = new ContatoDttoGet
                {
                    CodigoWhatsapp = dados.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id,
                    DataCadastro = DateTime.Now,
                    BloqueadoStatus = false,
                    Nome = dados.Dados?.entry[0]?.changes[0]?.value?.contacts[0].profile.name,
                    Codigologin = Login.Codigo

                };
                var viewmodel = await CreateComCodigo(newModel);
                ContatoDttoGet bababa = new ContatoDttoGet
                {
                    Codigo = viewmodel.Codigo,
                    Codigologin = viewmodel.Codigologin,
                    CodigoWhatsapp = viewmodel.CodigoWhatsapp,
                    BloqueadoStatus = viewmodel.BloqueadoStatus,
                    DataCadastro = viewmodel.DataCadastro,
                    Nome = viewmodel.Nome,
                };
                return bababa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContatoDttoGet> GetPorId(int id)
        {
            try
            {
                var Model = await _contatoRepository.GetPorId(id);
                ContatoDttoGet NewModel = new ContatoDttoGet
                {
                    Codigo = Model.ConId,
                    CodigoWhatsapp = Model.ConWaId,
                    Nome = Model.ConNome,
                    DataCadastro = Model.ConDataCadastro,
                    BloqueadoStatus = Model.ConBloqueadoStatus,
                    Codigologin = Convert.ToInt32(Model.LogId)
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ContatoDttoGet> CreateComCodigo(ContatoDttoGet Model)
        {
            try
            {
                Contato NewModel = new Contato
                {
                    ConNome = Model.Nome,
                    ConDataCadastro = Model.DataCadastro,
                    ConWaId = Model.CodigoWhatsapp,
                    ConBloqueadoStatus = Model.BloqueadoStatus,
                    LogId = Model.Codigologin
                };
                var result = await _contatoRepository.Adicionar(NewModel);
                ContatoDttoGet viewModel = new ContatoDttoGet
                {
                    Codigo = result.ConId,
                    Codigologin = Convert.ToInt32(result.LogId),
                    CodigoWhatsapp = result.ConWaId,
                    DataCadastro = result.ConDataCadastro,
                    BloqueadoStatus = result.ConBloqueadoStatus,
                    Nome = result.ConNome,
                };
                return viewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ContatoDttoGet> Update(ContatoDttoGet Model)
        {
            try
            {
                Contato NewModel = new Contato
                {
                    ConId = Model.Codigo,
                    ConNome = Model.Nome,
                    ConDataCadastro = Model.DataCadastro,
                    ConWaId = Model.CodigoWhatsapp,
                    ConBloqueadoStatus = Model.BloqueadoStatus,
                    LogId = Model.Codigologin
                };
                await _contatoRepository.update(NewModel);
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContatoDttoGet> Delete(int id)
        {
            try
            {
                var model = await _contatoRepository.GetPorId(id);

                using (var context = new chatbotContext())
                {
                    var contato = await context.Contatos
                         .Include(x => x.Atendimentos)
                         .Include(x => x.Chats)
                         .Include(x => x.Mensagens)
                         .SingleOrDefaultAsync(x => x.ConId == id);

                    if (contato.Atendimentos.Count != 0)
                    {
                        context.RemoveRange(contato.Atendimentos);
                    }
                    if (contato.Chats.Count != 0)
                    {
                        context.RemoveRange(contato.Chats);
                    }
                    if (contato.Mensagens.Count != 0)
                    {
                        context.RemoveRange(contato.Mensagens);
                    }

                    context.Remove(contato);
                    await context.SaveChangesAsync();
                }

                ContatoDttoGet NewModel = new ContatoDttoGet
                {
                    Codigo = model.ConId,
                    CodigoWhatsapp = model.ConWaId,
                    Nome = model.ConNome,
                    DataCadastro = model.ConDataCadastro,
                    BloqueadoStatus = model.ConBloqueadoStatus,
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ContatoDttoGet> RetornarConIdPorWaID(string waID)
        {
            try
            {
                if (waID == null)
                {
                    throw new Exception("informe um Id do whatsapp");
                }
                else
                {
                    var dados = await GetALl();
                    var ContatoEntity = dados.FirstOrDefault(x => x.CodigoWhatsapp == waID);
                    if (ContatoEntity != null)
                    {
                        return ContatoEntity;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("ocorreu um error ao tentar fazer a requisição" + ex.Message);
            }

        }

        public async Task<List<ContatoDttoGet>> GetListaDeContatosBloqueadosPorLogId(int logId)
        {
            try
            {
                var dados = await GetALl();
                return dados.Where(x => x.BloqueadoStatus == true && x.Codigologin == logId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ContatoDttoGetForView> GetContatoForViewPorId(int id)
        {
            try
            {
                var Model = await _contatoRepository.GetPorId(id);
                ContatoDttoGetForView NewModel = new ContatoDttoGetForView
                {
                    Codigo = Model.ConId,
                    Nome = Model.ConNome,
                    CodigoWhatsapp = Model.ConWaId,
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<ContatoDttoGet> Create(ContatoDttoGet Model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContatoDttoGet>> GetListaDeContatosPorLogId(int logId)
        {
            try
            {
                var dados = await GetALl();
                return dados.Where(x => x.Codigologin == logId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
