import "./style.css";
import image from "./do-utilizador.png"
import { urlBase, UsuarioLogado } from "../../appsettings";
import { useEffect, useState } from "react";
import LoadScreen from "../../components/BaseComponents/loadingScreen";
import ModalAddOuAttUsuario from "../../components/ComponentesPerfil/ModalAddOuAttPerfil";
import ModalBase from "../../components/BaseComponents/BaseModal";

export default function perfil() {
  const [result, setResult] = useState(null);
  const [resultDep, setResultDep] = useState(null);
  const [ModalStatus, setModalStatus] = useState("Add");
  const [DepartamentoAtivoId, setDepartamentoAtivoId] = useState(0);
  const [UsuarioAtivoObj, setusuarioAtivo] = useState({});
  const [IsLoading, setIsLoading] = useState(true)
  const [IdUsuarioLogado, setIdUsuarioLogado] = useState(0)
  const fetchData = async (param, tipoUsuario) => {
    let url = ""
    console.log("Tipo Usuario Legal.123")
    console.log(tipoUsuario)
    if (tipoUsuario == "Atendente") {
      url = `${urlBase}/v1/Atendente/Atendente/${param}`;

      try {
        const response = await fetch(url);

        if (!response.ok) {
          throw new Error('Erro na requisição');
        }

        const data = await response.json();
        console.log(data)
        setResult(data);
      } catch (error) {
        console.log(error)
      }
    }

    if (tipoUsuario !== "Atendente") {
      url = `${urlBase}/v1/Login/login/${param}`;

      try {
        const response = await fetch(url);

        if (!response.ok) {
          throw new Error('Erro na requisição');
        }

        const data = await response.json();
        console.log(data)
        setResult(data);
      } catch (error) {
        console.log(error)
      }
    }



    url = `${urlBase}/v1/Departamaneto/Departamento/BuscarTodosDepartamentoPorLogId?id=${param}`;

    try {
      const response = await fetch(url);

      if (!response.ok) {
        throw new Error('Erro na requisição');
      }

      const data = await response.json();
      var listTratada = []
      data.map(x => {
        listTratada.push({
          id: x.codigo,
          descricao: x.nomeDepartamento,
          value: x.codigo
        })
      })
      setResultDep(listTratada);
      setDepartamentoAtivoId(listTratada[0].id)
      setIsLoading(false)
    } catch (error) {
      console.log(error)
    }
  };

  useEffect(() => {
    UsuarioLogado().then(result => {
      if (result.usuarioLogadoId == null) {
        alert("Usuario precisa estar logado")
        location.replace(location.origin + "/login");
      } else {
        if (result.idUsuarioCliente !== null) {
          fetchData(result.idUsuarioCliente, result.tipoUsuario);
        } else {
          fetchData(result.usuarioLogadoId, result.tipoUsuario);
        }
        setIdUsuarioLogado({ codigo: result.usuarioLogadoId, tipoUsuario: result.tipoUsuario })
      }
    });
  }, []);

  // const CriarUsuario = () => {
  //   const url = `${urlBase}/v1/Atendente/Atendente/Create`;

  //   const data = {
  //     Nome: document.querySelector("#NomeUsuarioInputUsuarios").value,
  //     Email: document.querySelector("#EmailInputUsuarios").value,
  //     Senha: document.querySelector("#SenhaInputUsuarios").value,
  //     Imagem: null,
  //     EstadoAtendente: false,
  //     CodigoDepartamento: DepartamentoAtivoId,
  //     CodigoLogin: IdUsuarioLogado
  //   };

  //   fetch(url, {
  //     method: 'POST',
  //     headers: {
  //       'Content-Type': 'application/json',
  //     },
  //     body: JSON.stringify(data),
  //   })
  //     .then(response => {
  //       if (!response.ok) {
  //         throw new Error('Network response was not ok ' + response.statusText);
  //       }
  //       return response.json();
  //     })
  //     .then(data => {
  //       console.log('Success:', data);
  //       fetchData(IdUsuarioLogado)
  //       const modalElement = document.getElementById('staticBackdrop');
  //       const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
  //       bootstrapModal.hide();
  //     })
  //     .catch(error => {
  //       console.error('Error:', error);
  //     });

  // }

  const AtualizarUsuario = () => {

    if (IdUsuarioLogado.tipoUsuario == "Atendente") {
      const url = `${urlBase}/v1/Atendente/Atendente/Atualizar`;

      const data = {
        codigo: UsuarioAtivoObj.codigo,
        Nome: document.querySelector("#NomeUsuarioInputUsuarios").value,
        Email: document.querySelector("#EmailInputUsuarios").value,
        Senha: document.querySelector("#SenhaInputUsuarios").value,
        Imagem: null,
        EstadoAtendente: document.querySelector("#EstadoAtendentePerfil").checked,
        CodigoDepartamento: DepartamentoAtivoId,
        CodigoLogin: IdUsuarioLogado.codigo
      };

      fetch(url, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      })
        .then(response => {
          if (!response.ok) {
            return response.json().then(err => {
              throw new Error('Erro na resposta: ' + JSON.stringify(err));
            });
          }
          return response.json();
        })
        .then(data => {
          console.log('Success:', data);
          fetchData(IdUsuarioLogado.codigo, IdUsuarioLogado.tipoUsuario);
          const modalElement = document.getElementById('staticBackdrop');
          const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
          bootstrapModal.hide();
          document.querySelector(".modal-backdrop").remove()
        })
        .catch(error => {
          console.error('Erro capturado:', error);
        });
      return
    }

    const url = `${urlBase}/v1/Login/login/Atualizar`;

    const data = {
      Codigo: result.codigo,
      Usuario: document.querySelector("#NomeUsuarioInputUsuarios").value,
      Email: document.querySelector("#EmailInputUsuarios").value,
      Senha: document.querySelector("#SenhaInputUsuarios").value,
      Tipo: IdUsuarioLogado.tipoUsuario == "Master" ? 1 : 2,
      Plano: result.plano,
      CodigoWhatsap: result.codigoWhatsap,
      Imagem: null,
    };

    fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
      .then(response => {
        if (!response.ok) {
          return response.json().then(err => {
            throw new Error('Erro na resposta: ' + JSON.stringify(err));
          });
        }
        return response.json();
      })
      .then(data => {
        console.log('Success:', data);
        fetchData(IdUsuarioLogado.codigo, IdUsuarioLogado.tipoUsuario);
        const modalElement = document.getElementById('staticBackdrop');
        const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
        bootstrapModal.hide();
        document.querySelector(".modal-backdrop").remove()
      })
      .catch(error => {
        console.error('Erro capturado:', error);
      });




  };


  const SetarIdDoDepartamentoAtivo = (x) => {
    setDepartamentoAtivoId(x)
  }

  const SetarSttsDoDepartamento = (x) => {
    console.log(x)
    setModalStatus(x?.Prop)
    setusuarioAtivo(x?.Model)
    if (x.OpenModal) {
      var myModal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
      myModal.show();
    }
  }

  // useEffect(() => {
  //     console.log("Carregou use effect")
  //     UsuarioLogado().then(result => {
  //         console.log("Entrou no usuario logado funciton")

  //         //aqui podemos redirecionar para qualquer tela dependendo se o usuario esta logado, se e um usuario cliente se e master se e atendete etc...

  //         console.log(result)
  //         if (result.usuarioLogadoId == null) {
  //             console.log("Usuario Redirecionado")
  //             alert("Usuario precisa estar logado")
  //             location.replace(location.origin + "/login");
  //         }

  //         if (result.tipoUsuario == "Atendente") {
  //             console.log("Usuario Redirecionado")
  //             alert("Usuario Não tem permissão para acessar essa tela")
  //             location.replace(location.origin + "/Atendimento");
  //         }
  //         setIsLoading(false)
  //     });
  // }, []);
  const [showPassword, setShowPassword] = useState(false);

  const togglePasswordVisibility = () => {
    setShowPassword((prevState) => !prevState);
  };

  return (

    <>
      {IsLoading ? (
        <LoadScreen />
      ) : (

        <div className="col principal espaco">
          <br></br>
          <h1 className="Title">Perfil</h1>

          <hr></hr>


          <div className="row padding" >

            <div className="cardprincipal">

              <div className="CardPerfilSubTitle">
                <p className="SubTitlePerfil">Resumo do seu Perfil</p>
              </div>


              <div className="PerfilImagem">
                <img src={image}></img>
                <div className="col NomePerfil">
                  <p id="title" className="title" aria-describedby="addon-wrapping"> {result?.nome == null ? result?.usuario : result?.nome}</p>
                  <p className="SubTitleName">{IdUsuarioLogado.tipoUsuario}</p>
                </div>
              </div>

              <br></br>
              <div className="input-group flex-nowrap colcentro">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="grey" className="bi bi-file-code-fill" viewBox="0 0 16 16">
                  <path d="M12 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2M6.646 5.646a.5.5 0 1 1 .708.708L5.707 8l1.647 1.646a.5.5 0 0 1-.708.708l-2-2a.5.5 0 0 1 0-.708zm2.708 0 2 2a.5.5 0 0 1 0 .708l-2 2a.5.5 0 0 1-.708-.708L10.293 8 8.646 6.354a.5.5 0 1 1 .708-.708" />
                </svg>
                <p> &nbsp; Código login: &nbsp;</p>
                <p aria-label="Login" aria-describedby="addon-wrapping">{result?.codigo}</p>
              </div>

              <div className="input-group flex-nowrap colcentro">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pass-fill" viewBox="0 0 16 16">
                  <path d="M10 0a2 2 0 1 1-4 0H3.5A1.5 1.5 0 0 0 2 1.5v13A1.5 1.5 0 0 0 3.5 16h9a1.5 1.5 0 0 0 1.5-1.5v-13A1.5 1.5 0 0 0 12.5 0zM4.5 5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1 0-1m0 2h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1 0-1" />
                </svg>
                <p>&nbsp; Senha: &nbsp;</p>
                <p aria-label="Senha">{showPassword ? result.senha : "••••••••"}</p>
                <a
                  type="button"
                  className="ms-2 text-decoration-none text-dark"
                  onClick={togglePasswordVisibility}
                >
                  {showPassword ? <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye" viewBox="0 0 16 16">
                    <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z" />
                    <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0" />
                  </svg> : <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye-slash" viewBox="0 0 16 16">
                    <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7 7 0 0 0-2.79.588l.77.771A6 6 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755q-.247.248-.517.486z" />
                    <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829" />
                    <path d="M3.35 5.47q-.27.24-.518.487A13 13 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7 7 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12z" />
                  </svg>}
                </a>
              </div>

              {IdUsuarioLogado.tipoUsuario == "Atendente" &&
                <div className="input-group flex-nowrap colcentro">
                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="grey" className="bi bi-file-person-fill" viewBox="0 0 16 16">
                    <path d="M12 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2m-1 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0m-3 4c2.623 0 4.146.826 5 1.755V14a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1v-1.245C3.854 11.825 5.377 11 8 11" />
                  </svg>
                  <p>&nbsp; estadoAtendente: &nbsp;</p>
                  <p aria-label="Login" aria-describedby="addon-wrapping">{result.estadoAtendente == true ? "Online" : "Offline"}</p>
                </div>
              }

              {IdUsuarioLogado.tipoUsuario == "Atendente" &&
                <div className="input-group flex-nowrap colcentro">
                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="grey" className="bi bi-building-fill" viewBox="0 0 16 16">
                    <path d="M3 0a1 1 0 0 0-1 1v14a1 1 0 0 0 1 1h3v-3.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 .5.5V16h3a1 1 0 0 0 1-1V1a1 1 0 0 0-1-1zm1 2.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm3 0a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm3.5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5v-1a.5.5 0 0 1 .5-.5M4 5.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zM7.5 5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5v-1a.5.5 0 0 1 .5-.5m2.5.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zM4.5 8h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5v-1a.5.5 0 0 1 .5-.5m2.5.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm3.5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5v-1a.5.5 0 0 1 .5-.5" />
                  </svg>
                  <p>&nbsp; Departamento: &nbsp;</p>
                  <p aria-label="Login" aria-describedby="addon-wrapping">{result.departamento.nomeDepartamento}</p>
                </div>
              }

              <div className="footerPerfil">
                <button data-bs-toggle="modal" data-bs-target="#staticBackdrop" className="btn btn-primary" role="button" onClick={() => SetarSttsDoDepartamento({ Prop: "Att", Model: result })}>Alterar Dados</button>
              </div>

            </div>



          </div>

          <ModalAddOuAttUsuario onClick={AtualizarUsuario} tipoUsuario={IdUsuarioLogado.tipoUsuario} SetDepartamentoAtivoId={SetarIdDoDepartamentoAtivo} optionsList={resultDep} Usuario={UsuarioAtivoObj} isButtondelete={true} title={"Atualizar atendente"} descricao={"Preencha Campos"} />
        </div>
      )}
    </>
  );
}
