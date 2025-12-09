import "./style.css";
import ModalMensagemEmMassa from "../../components/ComponentesMensagemEmMassa/ModalMensagemEmMassa";
import ModalBase from "../../components/BaseComponents/BaseModal";
import Departamento from '../../components/ComponentesDepartamentos/Departamento';
import { useEffect, useState } from "react";
import { urlBase, UsuarioLogado } from "../../appsettings";
import LoadScreen from "../../components/BaseComponents/loadingScreen";
import Mensagem from "../../components/ComponentesDashBoard/Mensagem";
// import Card from '../../components/ComponentesUsuario/card';


export default function departamento() {

  const [result, setResult] = useState(null);
  const [resultDep, setResultDep] = useState(null);
  const [ModalStatus, setModalStatus] = useState("Add");
  const [DepartamentoAtivoId, setDepartamentoAtivoId] = useState(0);
  const [UsuarioAtivoObj, setusuarioAtivo] = useState({});
  const [IsLoading, setIsLoading] = useState(true)
  const [IdUsuarioLogado, setIdUsuarioLogado] = useState(0)

  const fetchData = async (param) => {
    var url = `${urlBase}/v1/Contato/Contatos/Get/BuscarTodosOsContatosDeUmLogID/${param}`;
    try {
      const response = await fetch(url);

      if (!response.ok) {
        throw new Error('Erro na requisição');
      }

      const data = await response.json();
      console.log(data)
      var listTratada = []
      data.map(x => {
        listTratada.push({
          id: x.codigo,
          descricao: x.nome,
          data: x.codigoWhatsapp,
          logid: x.codigologin,
        })
      })
      console.log(listTratada)
      console.log(listTratada[0].id)
      setResultDep(listTratada);
      setDepartamentoAtivoId(listTratada[0].id)
      setIsLoading(false)
      console.log(listTratada)
      console.log(resultDep)
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
        fetchData(result.usuarioLogadoId);
        setIdUsuarioLogado(result.usuarioLogadoId)
      }
    });
  }, []);

  const CriarDepartamento = () => {
    const url = `${urlBase}/v1/Departamaneto/Departamento/Create`;
    console.log(url)

    const data = {
      NomeDepartamento: document.querySelector("#DescricaoInputDepartamento").value,
      CodigoLogin: IdUsuarioLogado
    };

    fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json();
      })
      .then(data => {
        console.log('Success:', data);
        fetchData(IdUsuarioLogado)
        const modalElement = document.getElementById('staticBackdrop');
        const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
        bootstrapModal.hide();
      })
      .catch(error => {
        console.error('Error:', error);
      });

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

  const ExcluirDepartamento = async () => {
    let url = `${urlBase}/v1/Departamaneto/Departamento/Delete?id=${DepartamentoAtivoId}`;
    console.log(UsuarioAtivoObj.codigo)
    try {
      const response = await fetch(url, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error('Erro na requisição');
      }

      await fetchData(IdUsuarioLogado);
      const modalElement = document.getElementById('ModalExcluirDepartmento');
      const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
      bootstrapModal.hide();
    } catch (error) {
      console.log(error);
    }
  }

  function obterListaDeContatosSelecionados() {
    const listaInicial = [];
    const checkboxes = document.querySelectorAll('input.form-check-input');
    const mensagem = document.querySelector("#DescricaoInputDepartamento").value; // Mensagem capturada uma vez
    
    // Lista para armazenar os ContatoDttoGet
    const listaContatoDttoGet = [];
  
    checkboxes.forEach(element => {
      if (element.checked) { // Verifica se o checkbox está marcado
        // Cria um objeto ContatoDttoGet para o checkbox marcado
        const ContatoDttoGet = {
          Codigo:  parseInt(element.dataset.codigo) || null,            
          CodigoWhatsapp: element.dataset.codigowhatsapp || null, 
          Nome: null, // Pode ser ajustado para capturar dinamicamente se necessário
          DataCadastro: null,
          BloqueadoStatus: null,
          Codigologin: parseInt(element.dataset.codigologin) || null
        };
  
        // Adiciona o ContatoDttoGet à lista
        listaContatoDttoGet.push(ContatoDttoGet);
      }

     
    });
  
    // Cria o objeto final que contém a lista de ContatoDttoGet e a Mensagem
    const data = {
      contatos: listaContatoDttoGet
    };

  
    // Adiciona o objeto final à listaInicial
    console.log("Lista aqui")
    console.log(data.contatos)

     // Envia a requisição POST
  fetch(`http://localhost:5058/api/v1/Meta/EnvioMensagensEmMassa?conteudo=${mensagem}`, {
    method: 'POST', // Método da requisição
    headers: {
      'Content-Type': 'application/json', // Indicando que o corpo da requisição é JSON
    },
    body: JSON.stringify(listaContatoDttoGet) // Convertendo o objeto data para uma string JSON
  })
  .then(response => response) // Fazendo o parse da resposta
  .then(data => {

    console.log('Sucesso:', data); // Manipula a resposta da requisição
  })
  .catch((error) => {
    console.error('Erro:', error); // Manipula erros da requisição
  });
  
    console.log(listaInicial); // Exibe a lista final no console
    const modalElement = document.getElementById('staticBackdrop');
    const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
    bootstrapModal.hide();
    return listaInicial; // Retorna a lista de contatos selecionados
  }
  


  const AtualizarDepartamento = () => {

    console.log("Bateu em atualizar")
    var id = document.querySelector("#DescricaoInputDepartamento").value

    const url = `${urlBase}/v1/Departamaneto/Departamento/Update`;

    const data = {
      codigo: DepartamentoAtivoId,
      NomeDepartamento: document.querySelector("#DescricaoInputDepartamento").value,
      CodigoLogin: IdUsuarioLogado
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
        fetchData(IdUsuarioLogado);
        const modalElement = document.getElementById('staticBackdrop');
        const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
        bootstrapModal.hide();
      })
      .catch(error => {
        console.error('Erro capturado:', error);
      });
  };

  return (
    <>
      {IsLoading ? (
        <LoadScreen />
      ) : (
        <div className="col">
          <div className="Header">
            <h1 className="Title">Mensagem Em Massa</h1>
            <button onClick={() => SetarSttsDoDepartamento({ Prop: "Add" })} data-bs-toggle="modal" data-bs-target="#staticBackdrop" className="btn btn-primary BlueButton">Mandar Mensagem   	&nbsp;
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" className="bi bi-plus-circle" viewBox="0 0 16 16">
                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
                <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4" />
              </svg>
            </button>
          </div>

          <hr></hr>
          <div className="container-fluid text-center DepartamentoCampos">
            <div className="row teste table-responsive">

              <table class="table tablecomprimida table-striped">
                <thead>
                  <tr>
                    <th scope="col">Código</th>
                    <th scope="col">Nome</th>
                    <th scope="col">Telefone</th>
                    <th scope="col">Mensagem</th>
                  </tr>
                </thead>
                <tbody>

                  {resultDep.map(x => (
                    <tr>
                      <th scope="row">{x.id}</th>
                      <td>{x.descricao}</td>
                      <td>{x.data}</td>
                      <td>
                        <div class="form-check">
                          <input class="form-check-input" data-Codigo={x.id} data-CodigoWhatsapp={x.data} data-CodigoLogin={x.logid} type="checkbox" value="" id="flexCheckDefault"></input>
                        </div></td>
                    </tr>
                  ))}



                </tbody>
              </table>




            </div>
          </div>
          <ModalMensagemEmMassa  onClick={obterListaDeContatosSelecionados}  optionsList={resultDep} Usuario={UsuarioAtivoObj} isButtondelete={false} title={"Adicionar novo Departamento"} descricao={"Preencha Campos"} />

        </div>

      )}

    </>

  );
}
