import "./style.css";
import Card from '../../components/ComponentesUsuario/card';
import { urlBase, UsuarioLogado } from "../../appsettings";
import { useEffect, useState } from "react";
import LoadScreen from "../../components/BaseComponents/loadingScreen";
import ModalAddOuAttUsuario from "../../components/ComponentesUsuario/ModalAddOuAttUsuario";
import ModalBase from "../../components/BaseComponents/BaseModal";
export default function usuario() {

  const [result, setResult] = useState(null);
  const [resultDep, setResultDep] = useState(null);
  const [ModalStatus, setModalStatus] = useState("Add");
  const [DepartamentoAtivoId, setDepartamentoAtivoId] = useState(0);
  const [UsuarioAtivoObj, setusuarioAtivo] = useState({});
  const [IsLoading, setIsLoading] = useState(true)
  const [IdUsuarioLogado, setIdUsuarioLogado] = useState(0)
  const fetchData = async (param) => {
    let url = `${urlBase}/v1/Atendente/Atendente/BuscarTodosAtendentePorLogId?id=${param}`;

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
        fetchData(result.usuarioLogadoId);
        setIdUsuarioLogado(result.usuarioLogadoId)
      }
    });
  }, []);

  const CriarUsuario = () => {
    const url = `${urlBase}/v1/Atendente/Atendente/Create`;

    const data = {
      Nome: document.querySelector("#NomeUsuarioInputUsuarios").value,
      Email: document.querySelector("#EmailInputUsuarios").value,
      Senha: document.querySelector("#SenhaInputUsuarios").value,
      Imagem: null,
      EstadoAtendente: false,
      CodigoDepartamento: DepartamentoAtivoId,
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

  const AtualizarUsuario = () => {
    const url = `${urlBase}/v1/Atendente/Atendente/Atualizar`;

    const data = {
      codigo: UsuarioAtivoObj.codigo,
      Nome: document.querySelector("#NomeUsuarioInputUsuarios").value,
      Email: document.querySelector("#EmailInputUsuarios").value,
      Senha: document.querySelector("#SenhaInputUsuarios").value,
      Imagem: null,
      EstadoAtendente: UsuarioAtivoObj.estadoAtendente,
      CodigoDepartamento: DepartamentoAtivoId,
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

  const ExcluirFuncionario = async () => {
    let url = `${urlBase}/v1/Atendente/Atendente/Remove?id=${UsuarioAtivoObj.codigo}`;

    try {
      const response = await fetch(url, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        console.log("Resposta de erro do servidor:", response); // Log completo da resposta
        const errorMessage = await response.text().catch(() => 'Erro desconhecido');
        console.log("Mensagem de erro como texto:", errorMessage);
        throw new Error(errorMessage || 'Erro na requisição');
      }

      await fetchData(IdUsuarioLogado);
      const modalElement = document.getElementById('ModalExcluirAtendenteTelaUsuario');
      const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
      bootstrapModal.hide();
    } catch (error) {
      alert(error)
      console.log(error);
    }
  }

  return (
    <>
      {IsLoading ? (
        <LoadScreen />
      ) : (
        <div className="col">
          <div className="Header">
            <h1 className="Title">Usuario</h1>
            <button onClick={() => SetarSttsDoDepartamento({ Prop: "Add" })} className="btn btn-primary btnHoverUsuarios" data-bs-toggle="modal" data-bs-target="#staticBackdrop">Adicionar Usuario &nbsp;
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" className="bi bi-plus-circle" viewBox="0 0 16 16">
                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
                <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4" />
              </svg>
            </button>
          </div>

          <hr />

          <div className="container-fluid text-center">
            <div className="row justify-content-center align-items-center ">

              {result.map(x => (
                <div className="col Widthteste" key={x.atendente.codigo}>
                  <Card MudarStatusModal={SetarSttsDoDepartamento} date={x} />
                </div>
              ))}

            </div>
          </div>
          {ModalStatus == "Add" ? (
            <ModalAddOuAttUsuario onClick={CriarUsuario} SetDepartamentoAtivoId={SetarIdDoDepartamentoAtivo} optionsList={resultDep} Usuario={UsuarioAtivoObj} isButtondelete={false} title={"Adicionar novo atendente"} descricao={"Preencha Campos"} />
          ) : (
            <ModalAddOuAttUsuario onClick={AtualizarUsuario} SetDepartamentoAtivoId={SetarIdDoDepartamentoAtivo} optionsList={resultDep} Usuario={UsuarioAtivoObj} isButtondelete={true} title={"Atualizar atendente"} descricao={"Preencha Campos"} />
          )}
          <ModalBase id={"ModalExcluirAtendenteTelaUsuario"} HasHeader={true} title={"Excluir Atendente"} ButtonClassName={"btn btn-danger mb-3"} ButtonDescription={"Deletar"} Description={`Você realmente deseja excluir o atendente: ${UsuarioAtivoObj?.nome}?`} ButtonOnclick={ExcluirFuncionario} />
        </div>
      )}

    </>

  );
}
