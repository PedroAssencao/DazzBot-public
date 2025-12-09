using Chatbot.Domain.Models;
using Chatbot.Infrastructure.Dtto;
using Chatbot.Infrastructure.Repository.Interfaces;
using Chatbot.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net;
using Chatbot.API.DAL;
using Microsoft.EntityFrameworkCore;
using Chatbot.Domain.Models.Enums;
namespace Chatbot.Services.Services
{
    public class LoginServices : ILoginInterfaceServices
    {
        protected readonly ILoginInterface _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAtendeteInterface _atendete;

        public LoginServices(ILoginInterface repository, IHttpContextAccessor httpContextAccessor, IAtendeteInterface atendete)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _atendete = atendete;
        }

        public async Task<List<LoginDttoGet>> GetALl()
        {
            try
            {
                var dados = await _repository.GetALl();
                List<LoginDttoGet> List = new List<LoginDttoGet>();
                foreach (var item in dados)
                {
                    LoginDttoGet NewModel = new LoginDttoGet
                    {
                        Codigo = item.LogId,
                        CodigoWhatsap = item.LogWaid,
                        Usuario = item.LogUser,
                        Email = item.LogEmail,
                        Senha = item.LogSenha,
                        Imagem = item.LogImg,
                        Tipo = item.logTipo,
                        Plano = item.LogPlano,

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
        public async Task<LoginDttoGet> GetPorId(int id)
        {
            try
            {
                var dados = await _repository.GetPorId(id);
                LoginDttoGet NewModel = new LoginDttoGet
                {
                    Codigo = dados.LogId,
                    CodigoWhatsap = dados.LogWaid,
                    Usuario = dados.LogUser,
                    Email = dados.LogEmail,
                    Senha = dados.LogSenha,
                    Imagem = dados.LogImg,
                    Tipo = dados.logTipo,
                    Plano = dados.LogPlano,
                };
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoginDttoGet> GetPorIdSenhaDescriptografada(int id)
        {
            try
            {
                var dados = await _repository.GetPorId(id);
                LoginDttoGet NewModel = new LoginDttoGet
                {
                    Codigo = dados.LogId,
                    CodigoWhatsap = dados.LogWaid,
                    Usuario = dados.LogUser,
                    Email = dados.LogEmail,
                    Senha = dados.LogSenha,
                    Imagem = dados.LogImg,
                    Tipo = dados.logTipo,
                    Plano = dados.LogPlano,
                };
                NewModel.Senha = NewModel.DescriptografaSenha(NewModel?.Senha);
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoginDttoGet> RetornarLogIdPorWaID(string waID)
        {
            try
            {
                if (waID == null)
                {
                    throw new Exception("Informe um Whatsapp Id");
                }
                else
                {
                    var dados = await _repository.GetALl();
                    var LoginEntity = dados.First(x => x.LogWaid == waID);
                    if (LoginEntity != null)
                    {
                        LoginDttoGet NewModel = new LoginDttoGet
                        {
                            Codigo = LoginEntity.LogId,
                            CodigoWhatsap = LoginEntity.LogWaid,
                            Usuario = LoginEntity.LogUser,
                            Email = LoginEntity.LogEmail,
                            Senha = LoginEntity.LogSenha,
                            Imagem = LoginEntity.LogImg,
                            Tipo = LoginEntity.logTipo,
                            Plano = LoginEntity.LogPlano,
                        };
                        return NewModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public async Task<LoginDttoGet> Create(LoginDttoGet Model)
        {
            try
            {
                Login NewModel = new Login
                {
                    LogWaid = Model.CodigoWhatsap,
                    LogUser = Model.Usuario,
                    LogEmail = Model.Email,
                    LogSenha = Model.Senha,
                    logTipo = Model.Tipo,
                    LogImg = Model.Imagem,
                    LogPlano = Model.Plano
                };
                await _repository.Adicionar(NewModel);
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LoginDttoGet> Update(LoginDttoGet Model)
        {
            try
            {
                Login NewModel = new Login
                {
                    LogId = Model.Codigo,
                    LogWaid = Model.CodigoWhatsap,
                    LogUser = Model.Usuario,
                    LogEmail = Model.Email,
                    LogSenha = Model.Senha,
                    logTipo = Model.Tipo,
                    LogImg = Model.Imagem,
                    LogPlano = Model.Plano
                };
                NewModel.LogSenha = Model.CriptografaSenha(NewModel?.LogSenha);
                await _repository.update(NewModel);
                return Model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LoginDttoGet> Delete(int id)
        {
            try
            {
                var item = await _repository.GetPorId(id);
                using (var context = new chatbotContext())
                {
                    var login = await context.Logins
                         .Include(x => x.Mensagens)
                         .Include(x => x.Menus)
                         .Include(x => x.Options)
                         .Include(x => x.Chats)
                         .Include(x => x.Atendentes)
                         .Include(x => x.Atendimentos)
                         .Include(x => x.Contatos)
                         .Include(x => x.Departamentos)
                         .SingleOrDefaultAsync(x => x.LogId == id);

                    if (login.Menus.Count != 0)
                    {
                        context.RemoveRange(login.Menus);
                    }
                    if (login.Chats.Count != 0)
                    {
                        context.RemoveRange(login.Chats);
                    }
                    if (login.Atendentes.Count != 0)
                    {
                        context.RemoveRange(login.Atendentes);
                    }
                    if (login.Atendimentos.Count != 0)
                    {
                        context.RemoveRange(login.Atendimentos);
                    }
                    if (login.Contatos.Count != 0)
                    {
                        context.RemoveRange(login.Contatos);
                    }
                    if (login.Departamentos.Count != 0)
                    {
                        context.RemoveRange(login.Departamentos);
                    }
                    if (login.Mensagens.Count != 0)
                    {
                        context.RemoveRange(login.Mensagens);
                    }
                    await context.SaveChangesAsync();
                }
                LoginDttoGet NewModel = new LoginDttoGet
                {
                    Codigo = item.LogId,
                    CodigoWhatsap = item.LogWaid,
                    Usuario = item.LogUser,
                    Email = item.LogEmail,
                    Senha = item.LogSenha,
                    Imagem = item.LogImg,
                    Tipo = item.logTipo,
                    Plano = item.LogPlano,
                };
                await _repository.delete(NewModel.Codigo);
                return NewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Claim>> Logar(LoginDttoPost Model, bool IsCadastre)
        {
            try
            {
                var usuarios = await _repository.GetALl();
                var usuarioSelecionado = usuarios.FirstOrDefault(x => x.LogEmail == Model?.Email && Model?.DescriptografaSenha(x?.LogSenha) == Model?.Senha);
                Atendente? teste = null;
                if (usuarioSelecionado == null)
                {
                    var dates = await _atendete.GetALl();
                    teste = dates.FirstOrDefault(x => x.AteEmail == Model?.Email && Model?.Senha == x.AteSenha);
                }
                if (usuarioSelecionado != null)
                {
                    var claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, usuarioSelecionado.LogId.ToString()),
                     new Claim(ClaimTypes.Role, usuarioSelecionado.logTipo == EPerfil.Master ? "Master" : "Usuario"),
                     new Claim(ClaimTypes.NameIdentifier, usuarioSelecionado.LogUser)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return claims;
                }

                if (teste != null)
                {
                    var claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, teste.AteId.ToString()),
                     new Claim(ClaimTypes.Role, "Atendente"),
                     new Claim(ClaimTypes.NameIdentifier, teste.AteNome),
                     new Claim(ClaimTypes.GivenName, teste.LogId.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return claims;
                }

                throw new Exception("Usuario Não Encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception("error: " + ex.Message);
            }
        }
        public async Task<LoginDttoGet> Cadastrar(LoginDttoGet Model)
        {
            try
            {
                if (Model != null)
                {

                    Login login = new Login
                    {
                        LogId = Model.Codigo,
                        LogEmail = Model.Email,
                        LogImg = Model.Imagem,
                        LogPlano = Model.Plano,
                        LogSenha = Model.CriptografaSenha(Model?.Senha),
                        LogUser = Model.Usuario,
                        LogWaid = Model.CodigoWhatsap,
                    };

                    Model.CriptografaSenha(Model.Senha);

                    var dados = await _repository.GetALl();
                    var checkEmailAndUser = dados.Where(x => x.LogEmail == login.LogEmail || x.LogUser == login.LogUser).ToList();
                    if (checkEmailAndUser.Count == 0)
                    {
                        LoginDttoPost NewModel = new LoginDttoPost
                        {
                            Email = Model.Email,
                            Senha = Model.Senha,
                        };
                        await _repository.Adicionar(login);
                        await Logar(NewModel, true);
                        return Model;
                    }

                    throw new Exception("Usuario ou Email Já Cadastrados");
                }
                throw new Exception("Não foi Possivel Cadastrar");
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex);
            }

        }

        public async Task<HttpStatusCode> Logout()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoginDttoGetForView> GetPorIdLoginView(int id)
        {
            try
            {
                var dados = await _repository.GetPorId(id);
                LoginDttoGetForView NewModel = new LoginDttoGetForView
                {
                    Codigo = dados.LogId,
                    CodigoWhatsapp = dados.LogWaid,
                    Usuario = dados.LogUser,
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
