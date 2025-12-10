using Chatbot.Domain.Models.Enums;
using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Meta.Repository.Interfaces;
using Chatbot.Infrastructure.Meta.Repository.SignalRForChat;
using Chatbot.Infrastructure.Services.Interfaces;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;
using System.Text.Json;
namespace Chatbot.Infrastructure.Meta.Repository
{
    public class MetodoCheckRepository : IMetodoCheck
    {
        protected readonly IContatoInterfaceServices _contatoInterfaceServices;
        protected readonly IMensagemInterfaceServices _MensagemInterfaceServices;
        protected readonly ILoginInterfaceServices _LoginInterfaceServices;
        protected readonly IAtendimentoInterfaceServices _AtendimentoInterfaceServices;
        private readonly IChatsInterfaceServices _ChatsInterfaceServices;
        private readonly IOptionInterfaceServices _OptionInterfaceServices;
        private readonly IHubContext<ChatHub> _hubContext;
        public MetodoCheckRepository(IContatoInterfaceServices contatoInterfaceServices, IMensagemInterfaceServices mensagemInterfaceServices, ILoginInterfaceServices loginInterfaceServices,
            IAtendimentoInterfaceServices atendimentoInterfaceServices, IChatsInterfaceServices chatsInterfaceServices, IHubContext<ChatHub> hubContext, IOptionInterfaceServices optionInterfaceServices)
        {
            _contatoInterfaceServices = contatoInterfaceServices;
            _MensagemInterfaceServices = mensagemInterfaceServices;
            _LoginInterfaceServices = loginInterfaceServices;
            _AtendimentoInterfaceServices = atendimentoInterfaceServices;
            _ChatsInterfaceServices = chatsInterfaceServices;
            _hubContext = hubContext;
            _OptionInterfaceServices = optionInterfaceServices;
        }

        //ver maneira melhor de fazer esse metodo, fica muito dificil de ler com esse monte de try aninhado, porem por enquanto funciona
        public async Task<DataAndType> VerificarTipoDeRetorno(dynamic Values)
        {
            try
            {
                var dados = await TipoMensagemSimples(Values);
                return dados;
            }
            catch (Exception)
            {
                try
                {
                    var dados = await TipoMensagemMultiplaEscolha(Values);
                    return dados;
                }
                catch (Exception)
                {
                    try
                    {
                        var dados = TipomensagemDeStatus(Values);
                        return dados;
                    }
                    catch (Exception)
                    {
                        throw new Exception("Metodo não Identificado");
                    }
                }
            }
        }
        public DataAndType TipomensagemDeStatus(dynamic Values)
        {
            //mensagem que atualiza o status de uma mensagem porem como ainda não foi implementado ele vai para analise ainda
            try
            {
                var dados = JsonSerializer.Deserialize<recaiveStatusMensagem.Root>(Values.ToString());
                DataAndType Model = new DataAndType
                {
                    Tipo = ETipoRetornoJson.TipoStatus,
                    Dados = dados
                };
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DataAndType> TipoMensagemMultiplaEscolha(dynamic Values)
        {
            //metodo feito para identificar se o objeto recebido e uma resposta a um menu ou seja uma mensagem de multipla escolha
            try
            {
                var dados = JsonSerializer.Deserialize<recaiveMensagemWithMultipleOption.Root>(Values.ToString());
                if (dados.entry[0].changes[0].value.messages[0].interactive.list_reply.description == null)
                {
                    throw new Exception("Metodo errado");
                }
                DataAndType Model = new DataAndType
                {
                    Tipo = ETipoRetornoJson.TipoMultiplaEscolhas,
                    Dados = dados
                };
                return await VerificaçõesIniciais(Model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DataAndType> TipoMensagemSimples(dynamic Values)
        {
            //metodo feito para verificar se o tipo da mensagem recebida e apenas um texto
            try
            {
                var dados = JsonSerializer.Deserialize<recaiveMensagem.Root>(Values.ToString());
                if (dados.entry[0].changes[0].value.messages == null || dados.entry[0].changes[0].value.messages[0].text == null)
                {
                    throw new Exception("Metodo errado");
                }

                DataAndType Model = new DataAndType
                {
                    Tipo = ETipoRetornoJson.TipoSimples,
                    Dados = dados
                };
                return await VerificaçõesIniciais(Model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DataAndType?> ContatoIsBlock(DataAndType dados)
        {
            //metodo para enviar um alerta ao lead caso seu contato tenha sido bloqueado pelo client
            try
            {
                var responseObject = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = dados.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id,
                    type = "text",
                    text = new { preview_url = false, body = "Seu Contato Esta Bloqueado" }
                };
                DataAndType newmodel = new DataAndType
                {
                    Tipo = ETipoRetornoJson.TipoPost,
                    Dados = responseObject
                };
                return newmodel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<DataAndType> MensageIsRepetead(DataAndType dados)
        {
            //metodo para analise feito para enviar um alarme em caso da mensagem ser repetida
            try
            {
                var responseObject = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = dados.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id,
                    type = "text",
                    text = new { preview_url = false, body = "Mensagem Repetida" }
                };
                DataAndType newmodel = new DataAndType
                {
                    Tipo = ETipoRetornoJson.TipoPost,
                    Dados = responseObject
                };
                return newmodel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<DataAndType> VerificaçõesIniciais(DataAndType dados)
        {

            try
            {
                ContatoDttoGet contato = await _contatoInterfaceServices.RetornarConIdPorWaID(dados?.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id);
                LoginDttoGet Login = await _LoginInterfaceServices.RetornarLogIdPorWaID(dados?.Dados?.entry[0]?.changes[0]?.value?.metadata?.display_phone_number);
                AtendimentoDttoGet Item = await _AtendimentoInterfaceServices.ResgatarAtendimentoPorLogIdEContatoWaId(dados?.Dados?.entry[0].changes[0].value.contacts[0].wa_id, Login.Codigo);
                ChatsDttoGet? chat = await _ChatsInterfaceServices.RetornarChatPorAtenId(Item != null ? Item.Codigo : 0);

                //checkando para ver se e necessario criar alguma dessas informações
                contato = contato == null ? contato = await _contatoInterfaceServices.ContatoIsNull(dados, Login) : contato;
                Item = Item == null ? await _AtendimentoInterfaceServices.AtendimentoIsNull(dados, contato, Login) : Item;
                chat = chat == null ? await _ChatsInterfaceServices.ChatIsNull(dados, Item) : chat;

                ETipoRetornoJson tipoRetorno;

                string descricao = "";

                dynamic ModelDesc = null;

                if (dados.Tipo == ETipoRetornoJson.TipoSimples)
                {
                    descricao = Convert.ToString(dados.Dados.entry[0].changes[0].value.messages[0].text.body);

                    ModelDesc = new
                    {
                        descricao = descricao,
                        TipoRetorno = ETipoRetornoJson.TipoSimples,
                        OptionSelecionada = ""
                    };

                }

                if (dados.Tipo == ETipoRetornoJson.TipoMultiplaEscolhas)
                {
                    var codigoMensagem = dados.Dados.entry[0].changes[0].value.messages[0].interactive.list_reply.id;
                    OptionDttoGet OptionSelecionada = await _OptionInterfaceServices.GetPorId(Convert.ToInt32(codigoMensagem));
                    descricao = Convert.ToString(dados.Dados.entry[0].changes[0].value.messages[0].interactive.list_reply.description);
                    ModelDesc = new
                    {
                        descricao = descricao,
                        TipoRetorno = ETipoRetornoJson.TipoMultiplaEscolhas,
                        OptionSelecionada = OptionSelecionada
                    };

                }


                //verificar se o contato esta bloqueado antes de enviar alguma mensagem
                if (contato.BloqueadoStatus == true)
                {
                    return await ContatoIsBlock(dados);
                }

                //verficar se a mensagem e repetida 
                if (await _MensagemInterfaceServices.BuscarMensagemPorWaId(dados.Dados.entry[0].changes[0].value.messages[0].id) != null)
                {
                    //return await MensageIsRepetead(dados); esse metodo faz com que quando ele indetificar uma mensagemm repetida ele mandar diretamente no chat
                    throw new Exception("mensagem repetida");
                }

                //se tudo ocorrer bem salvar a mensagem e continuar
                await SaveAndNotifyAsync(Login, contato, chat, ModelDesc, dados.Dados.entry[0].changes[0].value.messages[0].id);
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu Algum Erro ao enviar a mensagem {ex.Message}");
            }
        }
        public async Task SaveAndNotifyAsync(LoginDttoGet Login, ContatoDttoGet contato, ChatsDttoGet chat, dynamic Model, string CodigoWhatsapp)
        {
            try
            {
                var mensage = await _MensagemInterfaceServices.SaveMensageWithCodigoWhatsappId(Login, contato, chat, Model.descricao, CodigoWhatsapp);

                if (Model.TipoRetorno == ETipoRetornoJson.TipoMultiplaEscolhas)
                {
                    if (Model.OptionSelecionada.Tipo == ETiposDeOptions.RedirecinamentoHumano)
                    {
                        chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.HUMANO;
                        chat.Atendente = null;
                        chat.Atendimento.Atendente = null;
                        chat.Atendimento.Departamento = null;
                    }

                    if (Model.OptionSelecionada.Finalizar)
                    {
                        chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                        chat.Atendimento.Atendente = null;
                        chat.Atendimento.Departamento = null;
                    }
                }

                if (Model.TipoRetorno == ETipoRetornoJson.TipoSimples)
                {
                    if (chat.Atendimento.EstadoAtendimento == EEstadoAtendimento.Finalizado)
                    {
                        chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                        chat.Atendimento.Atendente = null;
                        chat.Atendimento.Departamento = null;
                    }
                }

                if (Model.descricao.Trim().ToLower() == "/finalizar")
                {
                    chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                    chat.Atendimento.Atendente = null;
                    chat.Atendimento.Departamento = null;
                }

                if (Model.descricao.Trim().ToLower() == "/resetar")
                {
                    chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                    chat.Atendimento.Atendente = null;
                    chat.Atendimento.Departamento = null;
                }

                chat?.Mensagens?.Add(mensage);
                await _hubContext.Clients.Group(Convert.ToString(chat?.Atendimento?.Login?.Codigo)).SendAsync("ReceiveChats", chat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
