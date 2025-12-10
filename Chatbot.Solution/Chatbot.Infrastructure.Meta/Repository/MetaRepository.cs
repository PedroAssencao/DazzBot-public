using System;
using System.Text;
using Chatbot.API.DAL;
using Chatbot.Domain.Models;
using Chatbot.Domain.Models.Enums;
using Chatbot.Domain.Models.JsonMetaApi;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Meta.Repository.Interfaces;
using Chatbot.Infrastructure.Meta.Repository.SignalRForChat;
using Chatbot.Infrastructure.Services.Interfaces;
using Chatbot.Infrastrucutre.OpenAI.Repository.Interface;
using Chatbot.Services.Dtto.Meta;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Chatbot.Infrastructure.Meta.Repository
{
    public class MetaRepository : IMetaClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        protected readonly IMetodoCheck _metodoCheck;
        protected readonly IContatoInterfaceServices _contatoInterfaceServices;
        protected readonly IAtendimentoInterfaceServices _atendimentoInterfaceServices;
        protected readonly ILoginInterfaceServices _loginInterfaceServices;
        protected readonly IMenuInterfaceServices _menuInterfaceServices;
        protected readonly IChatsInterfaceServices _chatsInterfaceServices;
        protected readonly IOptionInterfaceServices _optionInterfaceServices;
        protected readonly IConfiguration _configuration;
        protected readonly IOpenaiRequest _openAiRequest;
        protected readonly IMensagemInterfaceServices _MensagemInterfaceServices;
        protected readonly IDepartamentoInterfaceServices _departamentoInterfaceServices;
        protected readonly IAtendenteInterfaceServices _atendenteInterfaceServices;
        private readonly IHubContext<ChatHub> _hubContext;
        public MetaRepository(IMetodoCheck metodoCheck, IContatoInterfaceServices contatoInterfaceServices,
            IAtendimentoInterfaceServices atendimentoInterfaceServices, ILoginInterfaceServices
            loginInterfaceServices, IMenuInterfaceServices menuInterfaceServices, IOptionInterfaceServices Option, IConfiguration config, IOpenaiRequest openai,
            IChatsInterfaceServices chatsInterfaceServices, IMensagemInterfaceServices mensagemInterfaceServices,
            IDepartamentoInterfaceServices departamentoInterfaceServices, IAtendenteInterfaceServices atendenteInterfaceServices, IHubContext<ChatHub> hubContext)
        {
            _metodoCheck = metodoCheck;
            _contatoInterfaceServices = contatoInterfaceServices;
            _atendimentoInterfaceServices = atendimentoInterfaceServices;
            _loginInterfaceServices = loginInterfaceServices;
            _menuInterfaceServices = menuInterfaceServices;
            _configuration = config;
            _optionInterfaceServices = Option;
            _openAiRequest = openai;
            _chatsInterfaceServices = chatsInterfaceServices;
            _MensagemInterfaceServices = mensagemInterfaceServices;
            _departamentoInterfaceServices = departamentoInterfaceServices;
            _atendenteInterfaceServices = atendenteInterfaceServices;
            _hubContext = hubContext;
        }

        public HttpClient ConfigurarClient(string token, string url)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                return _httpClient;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task CompararData()
        {
            try
            {
                var dados = await _chatsInterfaceServices.GetALl();

                var dadosJson = "";
                foreach (var item in dados)
                {
                    if (item.Atendimento != null)
                    {
                        if (item.Atendimento.EstadoAtendimento != null)
                        {
                            //Quando for subir a ver~soa oficial lembrar de descomitar essa linha ou colocar mais tempo para finalizar se for humano
                            //if (item.Atendimento.EstadoAtendimento.ToLower().Trim() != "Finalizado".ToLower().Trim() && item.Atendimento.EstadoAtendimento.ToLower().Trim() != "HUMANO".ToLower().Trim())
                            if (item.Atendimento.EstadoAtendimento != EEstadoAtendimento.Finalizado && item.Atendimento.EstadoAtendimento != EEstadoAtendimento.HUMANO)
                            {
                                var dataMensagem = Convert.ToDateTime(item.Mensagens.LastOrDefault().Data);
                                var dataAtual = DateTime.Now;
                                var diferenca = Math.Abs((dataAtual - dataMensagem).TotalMinutes);
                                string numero = item.Contato.CodigoWhatsapp == "557988132044" || item.Contato.CodigoWhatsapp == "557998468046" ? RetornarNumeroDeWhatsappParaNumeroTeste(item.Contato.CodigoWhatsapp) : item.Contato.CodigoWhatsapp;

                                if (dataAtual < dataMensagem)
                                {
                                    diferenca -= diferenca * 2;
                                }

                                //ver se tem uma opção melhor para enviar a mensagem por que fica paia essa não sendo enviada de vez em quando
                                //if (diferenca >= 5 && diferenca <= 10)
                                //{
                                //    await EnviarMensagemDoTipoSimples("Olá o atendimento ainda não foi finalizado, Se passar mais 10 minutos ele sera automaticamente finalizado!", numero);
                                //}
                                
                                //aumentei o tempo para 30 mim para não atrapalhar a apresentação
                                if (diferenca >= 30)
                                {
                                    AtendimentoDttoGet Atendimento = new AtendimentoDttoGet
                                    {
                                        Codigo = item.Atendimento.Codigo,
                                        EstadoAtendimento = EEstadoAtendimento.Finalizado,
                                        Data = DateTime.Now,
                                        Atendente = item?.Atendimento?.Atendente,
                                        Departamento = item?.Atendimento?.Departamento,
                                        Login = item?.Atendimento?.Login,
                                        Contato = item?.Atendimento?.Contato,
                                    };
                                    await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Atendimento, EEstadoAtendimento.Finalizado, null, null);
                                    item.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                                    await EnviarMensagemDoTipoSimples("O atendimento foi finalizado por inatividade.", numero, item.Atendimento, item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task SalvarMensagemAtendente(string descricao, int? chat, int? ate)
        {
            try
            {
                bool returnT = false;

                var dados = await _chatsInterfaceServices.GetPorId(Convert.ToInt32(chat));
                var numero = dados?.Contato?.CodigoWhatsapp == "557988132044" || dados?.Contato?.CodigoWhatsapp == "557998468046" ? RetornarNumeroDeWhatsappParaNumeroTeste(dados?.Contato?.CodigoWhatsapp) : dados?.Contato?.CodigoWhatsapp;
               
                if (descricao.ToLower() == "/resetar".Trim().ToLower())
                {
                    await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(dados.Atendimento, EEstadoAtendimento.Bot, null, null);
                    dados.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                    dados.Atendimento.Atendente = null;
                    dados.Atendimento.Departamento = null;
                    var menuselecionado = await _menuInterfaceServices.PegarMenuDeIaPorLogId(dados.Atendimento.Login.Codigo);
                    var result2 = await MontarMenuParaEnvio(menuselecionado, numero, dados.Atendimento, dados);
                    await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result2);
                    returnT = true;
                }

                if (descricao.Trim().ToLower() == "/finalizar".Trim().ToLower())
                {                    
                    await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(dados.Atendimento, EEstadoAtendimento.Finalizado, null, null);
                    dados.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                    dados.Atendimento.Departamento = null;
                    dados.Atendimento.Atendente = null;
                    await EnviarMensagemDoTipoSimples("Atendimento Finalizado, Obrigado Por Interagir!", numero, dados.Atendimento, dados);
                    returnT = true;
                }

                if (!returnT)
                {
                    if (dados?.Atendente?.Codigo != ate || dados?.Atendimento?.EstadoAtendimento != EEstadoAtendimento.HUMANO)
                    {
                        var atendenteModel = await _atendenteInterfaceServices.GetPorId(Convert.ToInt32(ate));
                        dados.Atendimento.Departamento = atendenteModel.Departamento;
                        await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(dados.Atendimento == null ? null : dados.Atendimento, EEstadoAtendimento.HUMANO, dados.Atendimento.Departamento == null ? null : dados.Atendimento.Departamento.Codigo, ate);
                        Chat NewModel = new Chat
                        {
                            ChaId = dados.Codigo,
                            AteId = ate,
                            AtenId = dados.Atendimento.Codigo,
                            ConId = dados?.Contato?.Codigo,
                            LogId = dados?.Atendimento?.Login?.Codigo
                        };
                        using (var newContext = new chatbotContext())
                        {
                            newContext.Chats.Update(NewModel);
                            await newContext.SaveChangesAsync();
                        }

                    }
                    MensagensDttoGetForView Message = new MensagensDttoGetForView
                    {
                        Contato = null,
                        Descricao = descricao,
                        Data = DateTime.Now,
                        Codigo = 0
                    };
                    dados.Atendimento.EstadoAtendimento = EEstadoAtendimento.HUMANO;

                    if (dados.Atendimento.Departamento != null)
                    {
                        dados.Atendimento.Departamento.Codigo = dados.Atendimento.Departamento.Codigo;
                    }

                    dados.Atendimento.Atendente.Codigo = Convert.ToInt32(ate);
                    await EnviarMensagemDoTipoSimples(descricao, numero, dados?.Atendimento, dados);
                    await _hubContext.Clients.Group(Convert.ToString(dados?.Atendimento?.Login?.Codigo)).SendAsync("ReceiveChats", dados);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> EnviarMensagemDoTipoSimples(string conteudo, string numero, AtendimentoDttoGet Atendimento, ChatsDttoGet chat)
        {
            try
            {
                var responseObject = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = numero,
                    type = "text",
                    text = new { preview_url = false, body = conteudo },
                };

                MensagensDttoGet Model = new MensagensDttoGet();

                if (Atendimento != null || chat != null)
                {
                    var mensagen = await _MensagemInterfaceServices.SaveMensage(Atendimento.Login.Codigo, chat.Codigo, conteudo);
                    chat?.Mensagens?.Add(mensagen);
                }
                await _hubContext.Clients.Group(Convert.ToString(chat?.Atendimento?.Login?.Codigo)).SendAsync("ReceiveChats", chat);
                return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], JsonConvert.SerializeObject(responseObject));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<dynamic> RespostaGpt(AtendimentoDttoGet Atendimento, string Conteudo, string numero, DataAndType Model)
        {
            try
            {
                ChatsDttoGet chat = await _chatsInterfaceServices.RetornarChatPorAtenId(Atendimento.Codigo);
                //voltar para o fluxo normal se a resposta enviada for sim
                if (Conteudo.Trim().ToLower() == "sim")
                {
                    await _atendimentoInterfaceServices.AtualizarAtendimentoComDttoDeGet(Atendimento);
                    return await MensagemInicial(Model);
                }

                //Enviar Mensagem de Aguardo a Mensagem do Gpt
                var mensagem = await EnviarMensagemDoTipoSimples("Aguardando Resposta...", numero, Atendimento, chat);
                //Enviar Mensagem de Resposta do Gpt
                string resposta = await _openAiRequest.PostAsync(_configuration.GetSection("AES").Value, Conteudo);

                await EnviarMensagemDoTipoSimples(resposta, numero, Atendimento, chat);

                var menuselecionado = await _menuInterfaceServices.PegarMenuDeIaPorLogId(Atendimento.Login.Codigo);
                chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.GPT;
                chat.Atendimento.Atendente = null;
                chat.Atendimento.Departamento = null;
                var result = await MontarMenuParaEnvio(menuselecionado, numero, Atendimento, chat);
                return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string?> AtualizarStatusDaMensagem(dynamic Values)
        {
            return "Status Atualizado";
        }
        public async Task<BotRespostaInitialVariables> montarObjDeMsg(DataAndType Model)
        {
            var descricaoDaMensagem = Model.Dados.entry[0].changes[0].value.messages[0].interactive.list_reply.description ?? Model.Dados.entry[0].changes[0].value.messages[0].text.body;
            var codigoMensagem = Model.Dados.entry[0].changes[0].value.messages[0].interactive.list_reply.id;
            string name = Model.Dados.entry[0].changes[0].value.metadata.display_phone_number;
            var Login = await _loginInterfaceServices.RetornarLogIdPorWaID(name);
            var numero = Model.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id == "557988132044" || Model.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id == "557998468046" ? RetornarNumeroDeWhatsappParaNumeroTeste(Model.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id) : Model.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id;
            AtendimentoDttoGet Atendimento = await _atendimentoInterfaceServices.ResgatarAtendimentoPorLogIdEContatoWaId(Model.Dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id, Convert.ToInt32(Login?.Codigo));
            OptionDttoGet OptionSelecionada = await _optionInterfaceServices.GetPorId(Convert.ToInt32(codigoMensagem));
            MenuDttoGet MenuSelecionadoOption = await _menuInterfaceServices.PegarMenuPorOptionId(Convert.ToInt32(codigoMensagem));
            ChatsDttoGet chat = await _chatsInterfaceServices.RetornarChatPorAtenId(Atendimento.Codigo);

            return new BotRespostaInitialVariables
            {
                descricaoDaMensagem = descricaoDaMensagem,
                codigoMensagem = codigoMensagem,
                name = name,
                Login = Login,
                numero = numero,
                Atendimento = Atendimento,
                OptionSelecionada = OptionSelecionada,
                MenuSelecionadoOption = MenuSelecionadoOption,
                chat = chat
            };
        }
        public async Task<dynamic> BotResposta(DataAndType Model)
        {
            BotRespostaInitialVariables Obj = await montarObjDeMsg(Model);
            var isFinalizar = false;
            var isBotAtendimento = false;
            try
            {
                //checkar se a mensagem não e nula
                if (Obj.descricaoDaMensagem != null && Obj.descricaoDaMensagem != "" && Obj.descricaoDaMensagem != " ")
                {

                    //finalizar atendimento se a option for tipo de finalizar
                    if (Obj.OptionSelecionada?.Finalizar == true)
                    {
                        if (Obj.Atendimento != null)
                        {
                            await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Obj.Atendimento, EEstadoAtendimento.Finalizado, null, null);
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                            Obj.chat.Atendimento.Departamento = null;
                            Obj.chat.Atendimento.Atendente = null;
                            isFinalizar = true;
                        }
                    }

                    //Se o atendimento for do tipo Gpt Ele ira checkar internamente e retornara a mensagem da ia corretamente
                    //não lembro o por que eu fiz esse codigo mais to achando que ele não ta sendo mais usado, vou deixar ai so por desencargo de consienciar - start
                    if (Obj.Atendimento?.EstadoAtendimento == EEstadoAtendimento.GPT)
                    {
                        if (Obj.descricaoDaMensagem.Trim().ToLower() == "Voltar ao Fluxo de Atendimento Normal".Trim().ToLower())
                        {
                            await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Obj.Atendimento, EEstadoAtendimento.Bot, null, null);
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                            Obj.chat.Atendimento.Departamento = null;
                            Obj.chat.Atendimento.Atendente = null;
                            string result = await MontarMenuParaEnvio(await _menuInterfaceServices.PegarMenuInicialPorLogId(Obj.Login.Codigo), Obj.numero, Obj.Atendimento, Obj.chat);
                            return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result);
                        }

                        if (Obj.descricaoDaMensagem.Trim().ToLower() == "Finalizar o Atendimento".Trim().ToLower())
                        {
                            await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Obj.Atendimento, EEstadoAtendimento.Finalizado, null, null);
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                            Obj.chat.Atendimento.Departamento = null;
                            Obj.chat.Atendimento.Atendente = null;
                            await EnviarMensagemDoTipoSimples("O Atendimento com IA foi finalizado Obrigado por interagir", Obj.numero, Obj.Atendimento, Obj.chat);
                            isFinalizar = true;
                        }

                        if (isFinalizar == false)
                        {
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.GPT;
                            Obj.chat.Atendimento.Departamento = null;
                            Obj.chat.Atendimento.Atendente = null;
                            await EnviarMensagemDoTipoSimples("Aguardando Resposta...", Obj.numero, Obj.Atendimento, Obj.chat);
                            string resposta = await _openAiRequest.PostAsync(_configuration.GetSection("AES").Value, Obj.descricaoDaMensagem);
                            await EnviarMensagemDoTipoSimples(resposta, Obj.numero, Obj.Atendimento, Obj.chat);
                        }
                    }
                    //- end

                    //Se o Menu for do tipo de mensagem com multipla escolha ele vai responder com essa resposta
                    if (Obj.OptionSelecionada?.Tipo == ETiposDeOptions.MensagemDeRespostaInterativa)
                    {
                        string result = await MontarMenuParaEnvio(Obj.MenuSelecionadoOption, Obj.numero, Obj.Atendimento, Obj.chat);
                        await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result);
                    }

                    //Se For Uma Mensagem Simples ele vai responder aqui
                    if (Obj.OptionSelecionada?.Tipo == ETiposDeOptions.MensagemDeResposta)
                    {
                        await EnviarMensagemDoTipoSimples(Obj.OptionSelecionada?.Resposta, Obj.numero, Obj.Atendimento, Obj.chat);
                    }

                    //Se For Uma Mensagem Feita Por Ia ele vai Respoder assim
                    if (Obj.OptionSelecionada?.Tipo == ETiposDeOptions.MensagemPorIA)
                    {
                        if (Obj.Atendimento != null && Obj.Atendimento?.EstadoAtendimento != EEstadoAtendimento.GPT && isFinalizar == false)
                        {
                            await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Obj.Atendimento, EEstadoAtendimento.GPT, null, null);
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.GPT;
                            Obj.chat.Atendimento.Departamento = null;
                            Obj.chat.Atendimento.Atendente = null;
                        }
                        await EnviarMensagemDoTipoSimples("Aguardando Resposta...", Obj.numero, Obj.Atendimento, Obj.chat);
                        string resposta = await _openAiRequest.PostAsync(_configuration.GetSection("AES").Value, Obj.descricaoDaMensagem);
                        await EnviarMensagemDoTipoSimples(resposta, Obj.numero, Obj.Atendimento, Obj.chat);
                        //if (isFinalizar == false)
                        //{
                            isBotAtendimento = true;
                        //}
                    }

                    //Se For Uma Mensagem Para conduzir para o atendimento humano
                    if (Obj.OptionSelecionada?.Tipo == ETiposDeOptions.RedirecinamentoHumano)
                    {
                        if (Obj.Atendimento != null && Obj.Atendimento?.EstadoAtendimento != EEstadoAtendimento.HUMANO)
                        {
                            //não essa não e a melhor forma de resolver esse problema, mais vai servir xD
                            Obj.Atendimento.Atendente = null;
                            Obj.Atendimento.Departamento = null;
                            using (var newContext = new chatbotContext())
                            {
                                Chat newChatModel = new Chat
                                {
                                    ConId = Obj?.chat?.Contato?.Codigo,
                                    ChaId = Obj.chat.Codigo,
                                    AteId = null,
                                    AtenId = Obj.Atendimento.Codigo,
                                    LogId = Obj?.Atendimento?.Login?.Codigo
                                };
                                newContext.Chats.Update(newChatModel);
                                await newContext.SaveChangesAsync();
                            }
                            await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(Obj.Atendimento, EEstadoAtendimento.HUMANO, Convert.ToInt32(Obj.OptionSelecionada?.Resposta), null);
                            Obj.chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.HUMANO;
                            Obj.chat.Atendimento.Departamento.Codigo = Convert.ToInt32(Obj.OptionSelecionada?.Resposta ?? "1");
                            Obj.chat.Atendimento.Atendente = null;
                        }
                        await EnviarMensagemDoTipoSimples("Voce entrou na nossa fila de atendimento por favor aguarde sua vez de ser atendido por um de nossos atendentes!", Obj.numero, Obj.Atendimento, Obj.chat);
                    }

                    //Verifcações do estado para resposta especiais dependendo do estado das variaveis definidas acima
                    if (isFinalizar && !isBotAtendimento)
                    {
                        await EnviarMensagemDoTipoSimples("O Atendimento foi Finalizado Se Tiver Mais Alguma Questão Apenas Intereja Novamente!", Obj.numero, Obj.Atendimento, Obj.chat);
                    }

                    if (isBotAtendimento && !isFinalizar)
                    {
                        await EnviarMensagemDoTipoSimples("Você entrou no modo de Interação com a IA Faça Uma Pergunta que ela ira te responder!", Obj.numero, Obj.Atendimento, Obj.chat);
                    }

                    if (isBotAtendimento && isFinalizar)
                    {
                        await EnviarMensagemDoTipoSimples("Opção configurada para Finalizar Atendimento Após a resposta da IA", Obj.numero, Obj.Atendimento, Obj.chat);
                    }

                    //se ocorrer tudo bem apenas ira retornar
                    return "Mensagem Enviada Com Sucesso";
                }

                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Menssagem Vazia Ou Feita Por Bot");
            }
        }
        public string RetornarNumeroDeWhatsappParaNumeroTeste(string contato)
        {
            if (contato == "557988132044")
            {
                contato = "5579988132044";
            }

            if (contato == "557998468046")
            {
                contato = "5579998468046";
            }

            return contato;
        }
        public async Task<string> IsAtendimentoNull(DataAndType Model)
        {
            try
            {
                var dadosJson = "";

                var teste = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = Model.Dados.entry[0].changes[0].value.contacts[0].wa_id,
                    type = "text",
                    text = new { preview_url = false, body = "Não entendi, Lembre-se de Escolher a opção no Menu Acima!" }
                };

                dadosJson = JsonConvert.SerializeObject(teste);
                return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], dadosJson);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> MontarMenuParaEnvio(MenuDttoGet menuselecionado, string numero, AtendimentoDttoGet Atendimento, ChatsDttoGet chat)
        {
            try
            {
                var responseObject = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = numero,
                    type = "interactive",
                    interactive = new
                    {
                        type = "list",
                        header = new { type = "text", text = menuselecionado?.Header },
                        body = new { text = menuselecionado?.Body },
                        footer = new { text = menuselecionado?.Footer },
                        action = new
                        {
                            button = "Menu de Opções",
                            sections = new[]
                         {
                            new { title = "Shorter Section Title", rows = menuselecionado?.Options?.Select(item => new { id = item.Codigo, title = item.Titulo, description = item.Descricao }).ToArray() }
                        }
                        }
                    }
                };
                var mensagen = await _MensagemInterfaceServices.SaveMensage(Atendimento.Login.Codigo, chat.Codigo, menuselecionado.Titulo);
                chat?.Mensagens?.Add(mensagen);
                await _hubContext.Clients.Group(Convert.ToString(chat?.Atendimento?.Login?.Codigo)).SendAsync("ReceiveChats", chat);
                return JsonConvert.SerializeObject(responseObject);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<dynamic> MensagemInicial(DataAndType Model)
        {
            //parei para pensar em uma coisa agora, a ideia de dar update invez de insert toda vez que cria uma atendimento era boa na epoca para não encher muito as tabelas no banco
            //mais agora para o caso da dashboard que precisa d eum historico isso fica meio paia
            //futuramente vai ter que trocar para invez de ele dar update em um atendimento ele inserir um novo
            try
            {
                recaiveMensagem.Root dados = Model.Dados;

                var login = await _loginInterfaceServices.RetornarLogIdPorWaID(dados?.entry[0]?.changes[0]?.value?.metadata?.display_phone_number);
                var contato = await _contatoInterfaceServices.RetornarConIdPorWaID(dados?.entry[0]?.changes[0]?.value?.contacts[0].wa_id);
                var menuselecionado = await _menuInterfaceServices.PegarMenuInicialPorLogId(login.Codigo);
                var conteudo = dados.entry[0].changes[0].value.messages[0].text.body;

                contato.CodigoWhatsapp = contato.CodigoWhatsapp == "557988132044" || contato.CodigoWhatsapp == "557998468046" ? RetornarNumeroDeWhatsappParaNumeroTeste(contato.CodigoWhatsapp) : contato.CodigoWhatsapp;

                var IsAtendimentoGoing = await _atendimentoInterfaceServices.AtendimentoExiste(login, contato);

                //if (IsAtendimentoGoing == null || IsAtendimentoGoing.EstadoAtendimento == EEstadoAtendimento.Bot || IsAtendimentoGoing.EstadoAtendimento == EEstadoAtendimento.HUMANO)
                //{
                if (conteudo.ToLower() == "/resetar".Trim().ToLower())
                {
                    var chat2 = await _chatsInterfaceServices.RetornarChatPorAtenId(IsAtendimentoGoing.Codigo);
                    await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(IsAtendimentoGoing, EEstadoAtendimento.Bot, null, null);
                    chat2.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                    chat2.Atendimento.Departamento = null;
                    chat2.Atendimento.Atendente = null;
                    var result2 = await MontarMenuParaEnvio(menuselecionado, contato.CodigoWhatsapp, chat2.Atendimento, chat2);
                    return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result2);
                }

                if (conteudo.Trim().ToLower() == "/finalizar".Trim().ToLower())
                {
                    var chat2 = await _chatsInterfaceServices.RetornarChatPorAtenId(IsAtendimentoGoing.Codigo);
                    await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(IsAtendimentoGoing, EEstadoAtendimento.Finalizado, null, null);
                    chat2.Atendimento.EstadoAtendimento = EEstadoAtendimento.Finalizado;
                    chat2.Atendimento.Departamento = null;
                    chat2.Atendimento.Atendente = null;
                    return await EnviarMensagemDoTipoSimples("Atendimento Finalizado, Obrigado Por Interajir!", contato.CodigoWhatsapp, IsAtendimentoGoing, chat2);
                }
                if (IsAtendimentoGoing.EstadoAtendimento == EEstadoAtendimento.Bot)
                {
                    return await IsAtendimentoNull(Model);
                }
                //}


                if (IsAtendimentoGoing.EstadoAtendimento != null)
                {
                    if (IsAtendimentoGoing.EstadoAtendimento == EEstadoAtendimento.GPT)
                    {
                        return await RespostaGpt(IsAtendimentoGoing, conteudo, contato.CodigoWhatsapp, Model);
                    }

                    //como a mensagem ja e salva e o chat ja esta configurado no metodo inicial vou deixar apenas para ele não retornar nada aqui por enquanto
                    if (IsAtendimentoGoing.EstadoAtendimento == EEstadoAtendimento.HUMANO)
                    {
                        throw new NotImplementedException();
                    }
                }

                var chat = await _chatsInterfaceServices.RetornarChatPorAtenId(IsAtendimentoGoing.Codigo);
                await _atendimentoInterfaceServices.AtualizarEstadoAtendimento(IsAtendimentoGoing, EEstadoAtendimento.Bot, null, null);
                chat.Atendimento.EstadoAtendimento = EEstadoAtendimento.Bot;
                chat.Atendimento.Departamento = null;
                chat.Atendimento.Atendente = null;
                var result = await MontarMenuParaEnvio(menuselecionado, contato.CodigoWhatsapp, chat.Atendimento, chat);
                return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<dynamic> ChamarMetodo(dynamic Values)
        {
            DataAndType dados = await _metodoCheck.VerificarTipoDeRetorno(Values);
            if (dados.Tipo == ETipoRetornoJson.TipoSimples)
            {
                return await MensagemInicial(dados);
            }
            if (dados.Tipo == ETipoRetornoJson.TipoMultiplaEscolhas)
            {
                return await BotResposta(dados);
            }
            if (dados.Tipo == ETipoRetornoJson.TipoStatus)
            {
                return await AtualizarStatusDaMensagem(dados.Dados);
            }
            if (dados.Tipo == ETipoRetornoJson.TipoPost)
            {
                return await PostAsync(_configuration["BaseUrl"], _configuration["Token"], JsonConvert.SerializeObject(dados.Dados));
            }
            throw new NotImplementedException();
        }
        public async Task<string> PostAsync(string url, string token, dynamic data)
        {
            try
            {
                var client = ConfigurarClient(token, url);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                var mensagens = await _MensagemInterfaceServices.UltimaMensagem();
                if (mensagens.CodigoWhatsapp == null)
                {
                    mensagens.CodigoWhatsapp = JObject.Parse(responseContent)?["messages"]?[0]?["id"]?.ToString();
                    await _MensagemInterfaceServices.UpdateWithDirectiveDbContext(mensagens);

                }
                return responseContent;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> EnvioDeMensagensEmMassa(List<ContatoDttoGet> Contatos, string conteudo)
        {
            try
            {
                foreach (var item in Contatos)
                {
                    var chat = await _chatsInterfaceServices.RetornarChatPorConIdELogId(item.Codigo, item.Codigologin);
                    if (chat != null)
                    {
                        if (chat.Atendimento != null)
                        {
                            await EnviarMensagemDoTipoSimples(conteudo, item.CodigoWhatsapp == "557988132044" || item.CodigoWhatsapp == "557998468046" ? RetornarNumeroDeWhatsappParaNumeroTeste(item.CodigoWhatsapp) : item.CodigoWhatsapp, chat.Atendimento, chat);
                        }
                    }
                }
                return "Mensagens Enviadas Com Sucesso";
            }
            catch (Exception ex)
            {
                return $"Error Ao Enviar Mensagens: Error{ex.Message}";
            }
        }
    }
}
