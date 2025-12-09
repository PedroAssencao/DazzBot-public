import { urlBase, UsuarioLogado } from "../../appsettings"
import { dataMock } from "./MockDatesForTestFluxoBot"
var optionsCounter = 0
var MenuCounter = 0
var loopsCount = 0
let data = []
var tipoOpcao = 0
var tipoOpcaoAtt = 0
var idmenuresponse = ""
export const fetchNewDatas = async () => {
  try {
    var UsuarioLogadoId = await UsuarioLogado()
    console.log(UsuarioLogadoId)
    const response = await fetch(`${urlBase}/v1/Menus/Menus/GetAllMenusByLogId/${UsuarioLogadoId.usuarioLogadoId}`);
    const responseJson = await response.json();
    console.log(responseJson)
    data = responseJson;
    if (data.length > 0) {
      return true;
    } else {
      return false
    }
  } catch (error) {
    console.error(error);
    alert("Erro ocorreu ao carregar dados")
    //deixar isso aqui apenas no ambiente de teste 
    //data = dataMock;    
    return false;
  }
}

const AdicionarNovaOpcao = async (element) => {
  console.log(element)
  if (element?.CodigoLogin == undefined) {
    var UsuarioLogadoIdResult = await UsuarioLogado()
    var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)
    element.CodigoLogin = UsuarioLogadoId
  }
  try {
    const response = await fetch(`${urlBase}/v1/Option/Option/Create`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(element),
    });

    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }

    const Model = await response.json();
    return Model
  } catch (error) {
    console.error('Erro ao adicionar nova opção:', error);
    alert("Erro ocorreu ao adicionar opção")
    return false
  }
};

const AtualziarOpcao = async (element) => {
  console.log(element)
  if (element?.CodigoLogin == undefined) {
    var UsuarioLogadoIdResult = await UsuarioLogado()
    var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)
    element.CodigoLogin = UsuarioLogadoId
  }
  try {
    const response = await fetch(`${urlBase}/v1/Option/Option/Update`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(element),
    });

    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }

    const Model = await response.json();
    return Model
  } catch (error) {
    console.error('Erro ao Atualizar opção:', error);
    alert("Erro ocorreu ao atualizar opção")
    return false
  }
};

const AdicionarNovoMenu = async (element) => {
  console.log(element)
  if (element?.CodigoLogin == undefined) {
    var UsuarioLogadoIdResult = await UsuarioLogado()
    var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)
    element.CodigoLogin = UsuarioLogadoId
  }
  try {
    const response = await fetch(`${urlBase}/v1/Menus/Menus/Create`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(element),
    });

    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }

    const Model = await response.json();
    return Model
  } catch (error) {
    console.error('Erro ao adicionar novo Menu:', error);
    alert("Erro ocorreu ao adicionar menu")
    return false
  }
}

const AtualizarMenu = async (element) => {
  console.log(element)
  if (element?.CodigoLogin == undefined) {
    var UsuarioLogadoIdResult = await UsuarioLogado()
    var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)
    element.CodigoLogin = UsuarioLogadoId
  }
  try {
    const response = await fetch(`${urlBase}/v1/Menus/Menus/Atualizar`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(element),
    });

    if (!response.ok) {
      throw new Error(`Erro na requisição: ${response.status}`);
    }

    const Model = await response.json();
    return Model
  } catch (error) {
    console.error('Erro ao Atualziar Menu:', error);
    alert("Erro ocorreu ao atualizar menu")
    return false
  }
}

export const SelectTipoHandEvent = (element) => {
  if (element.target.value == "0") {
    document.querySelector('#DepartamentoSelect').style.display = "none"
    document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
    document.querySelector("#TextareaSelect").style.display = "none"
    document.querySelector('#MultiplaEscolha').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
    document.querySelector("#TextareaSelectIA").style.display = "none"
    tipoOpcao = 0
  }

  if (element.target.value == "6") {
    document.querySelector('#DepartamentoSelect').style.display = "block"
    document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
    document.querySelector('#MultiplaEscolha').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSections').style.display = "flex"
    document.querySelector("#TextareaSelect").style.display = "none"
    document.querySelector("#TextareaSelectIA").style.display = "none"
    tipoOpcao = 6
  }

  if (element.target.value == "1") {
    document.querySelector("#FinalizarAtendimentoSelect").style.display = "Flex"
    document.querySelector("#TextareaSelect").style.display = "Flex"
    document.querySelector('#DepartamentoSelect').style.display = "none"
    document.querySelector('#MultiplaEscolha').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
    document.querySelector("#TextareaSelectIA").style.display = "none"
    tipoOpcao = 1
  }

  if (element.target.value == "3") {
    document.querySelector('#MultiplaEscolha').style.display = "Flex"
    document.querySelector('#DepartamentoSelect').style.display = "none"
    document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
    document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
    document.querySelector("#TextareaSelect").style.display = "none"
    document.querySelector("#TextareaSelectIA").style.display = "none"
    tipoOpcao = 3
  }

  if (element.target.value == "4") {
    document.querySelector("#FinalizarAtendimentoSelect").style.display = "Flex"
    document.querySelector("#TextareaSelectIA").style.display = "Flex"
    document.querySelector("#TextareaSelect").style.display = "none"
    document.querySelector('#DepartamentoSelect').style.display = "none"
    document.querySelector('#MultiplaEscolha').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
    tipoOpcao = 4
  }
}

export const SelectTipoHandEventAtualizacao = (element) => {
  console.log("element que caiu na function aqui")
  console.log(element)
  if (element?.tipo != null) {

    if (element?.tipo == "menu") {
      document.querySelector('#MultiplaEscolhaAtt').style.display = "Flex"
      document.querySelector('#DepartamentoSelectAtt').style.display = "none"
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
      document.querySelector("#TextareaSelectAtt").style.display = "none"
      document.querySelector("#TextareaSelectIAAtt").style.display = "none"
      document.querySelector("#textAreaContentIAAtt").style.display = "none"
      document.querySelector("#TituloSimplesInputIAAtt").style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "none"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "none"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "block"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "block"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "block"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "block"
      document.getElementById("SelectTipoAtt").selectedIndex = 3;
      document.getElementById("SelectTipoAtt").disabled = true
      tipoOpcaoAtt = 5
      idmenuresponse = ""
      return
    }

    if (element.codigo == "0") {
      document.querySelector('#DepartamentoSelectAtt').style.display = "none"
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
      document.querySelector("#TextareaSelectAtt").style.display = "none"
      document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "block"
      document.querySelector("#TextareaSelectIAAtt").style.display = "none"
      document.querySelector("#textAreaContentIAAtt").style.display = "none"
      document.querySelector("#TituloSimplesInputIAAtt").style.display = "none"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "block"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "block"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "block"
      document.querySelector(`#SelectTipoAtt option[value="${element?.codigo}"]`).selected = true;
      document.getElementById("SelectTipoAtt").disabled = false
      tipoOpcaoAtt = 0
      idmenuresponse = ""
    }

    if (element.codigo == "6") {
      console.log("Caiu em redirecionar")
      console.log(document.getElementById("SelectTipoAtt"))
      document.querySelector('#DepartamentoSelectAtt').style.display = "block"
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
      document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
      document.querySelector("#TextareaSelectIAAtt").style.display = "none"
      document.querySelector("#textAreaContentIAAtt").style.display = "none"
      document.querySelector("#TituloSimplesInputIAAtt").style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "flex"
      document.querySelector("#TextareaSelectAtt").style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "block"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "block"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "block"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "block"
      document.querySelector(`#SelectTipoAtt option[value="${element?.codigo}"]`).selected = true;
      document.getElementById("SelectTipoAtt").disabled = false
      tipoOpcaoAtt = 6
      idmenuresponse = ""
    }

    if (element.codigo == "1") {
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "Flex"
      document.querySelector("#TextareaSelectAtt").style.display = "Flex"
      document.querySelector("#TextareaSelectIAAtt").style.display = "none"
      document.querySelector("#textAreaContentIAAtt").style.display = "none"
      document.querySelector("#TituloSimplesInputIAAtt").style.display = "none"
      document.querySelector('#DepartamentoSelectAtt').style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "block"
      document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "block"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "block"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "block"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "block"
      document.querySelector(`#SelectTipoAtt option[value="${element?.codigo}"]`).selected = true;
      document.getElementById("SelectTipoAtt").disabled = false
      tipoOpcaoAtt = 1
      idmenuresponse = ""
    }

    if (element.codigo == "4") {
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "flex"
      document.querySelector("#TextareaSelectIAAtt").style.display = "flex"
      document.querySelector("#textAreaContentIAAtt").style.display = "flex"
      document.querySelector("#TituloSimplesInputIAAtt").style.display = "flex"
      document.querySelector("#TextareaSelectAtt").style.display = "none"
      document.querySelector('#DepartamentoSelectAtt').style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "none"
      document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "none"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "none"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "none"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "none"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "none"
      document.querySelector(`#SelectTipoAtt option[value="${element?.codigo}"]`).selected = true;
      document.getElementById("SelectTipoAtt").disabled = false
      tipoOpcaoAtt = 4
      idmenuresponse = ""
    }

    if (element.codigo == "3") {
      document.querySelector('#MultiplaEscolhaAtt').style.display = "Flex"
      document.querySelector('#DepartamentoSelectAtt').style.display = "none"
      document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
      document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
      document.querySelector("#TextareaSelectAtt").style.display = "none"
      document.querySelector("#TituloOpcaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#DescricaoMenuInputAttContainer").style.display = "block"
      document.querySelector("#rodapeMenuInputAttContainer").style.display = "none"
      document.querySelector("#corpoMenuInputAttContainer").style.display = "none"
      document.querySelector("#cabecalhoMenuInputAttContainer").style.display = "none"
      document.querySelector("#TituloMenuInputAttContainer").style.display = "none"
      document.querySelector(`#SelectTipoAtt option[value="${element?.codigo}"]`).selected = true;
      document.getElementById("SelectTipoAtt").disabled = true
      tipoOpcaoAtt = 3
      idmenuresponse = element.menuRespose.toString()
    }
    return
  }

  console.log(element.target.value)


  if (element.target.value == "0") {
    document.querySelector('#DepartamentoSelectAtt').style.display = "none"
    document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
    document.querySelector("#TextareaSelectAtt").style.display = "none"
    document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
    tipoOpcaoAtt = 0
  }

  if (element.target.value == "6") {
    document.querySelector('#DepartamentoSelectAtt').style.display = "block"
    document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
    document.querySelector('#MultiplaEscolhaAtt').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "flex"
    document.querySelector("#TextareaSelectAtt").style.display = "none"
    tipoOpcaoAtt = 1
  }

  if (element.target.value == "1") {
    document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "Flex"
    document.querySelector("#TextareaSelectAtt").style.display = "Flex"
    document.querySelector('#DepartamentoSelectAtt').style.display = "none"
    document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
    tipoOpcaoAtt = 2
  }

  if (element.target.value == "3") {
    document.querySelector('#MultiplaEscolhaAtt').style.display = "Flex"
    document.querySelector('#DepartamentoSelectAtt').style.display = "none"
    document.querySelector("#FinalizarAtendimentoSelectAtt").style.display = "none"
    document.querySelector('#RedirecionamentoInputsSectionsAtt').style.display = "none"
    document.querySelector("#TextareaSelectAtt").style.display = "none"
    tipoOpcaoAtt = 3
  }
}

export const verificarRegrasSalvarOption = async (e) => {
  console.log(e)
  let critica = false
  if (tipoOpcao == 0) {
    critica = true
  }

  if (tipoOpcao == 6 && document.querySelector("#TituloOpcaoMenuInputRedirecionamento").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloOpcaoMenuInputRedirecionamento").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcao == 4 && document.querySelector("#TituloSimplesInputIA").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloSimplesInputIA").classList.add("errorParaCampos")
    critica = true
  }

  // if (tipoOpcao == 4 && document.querySelector("#textAreaContentIA").value.length) {
  //   if (critica == false) {
  //     alert("Campos Obrigatorio não foram preenchidos")
  //   }
  //   document.querySelector("#textAreaContentIA").classList.add("errorParaCampos")
  //   critica = true
  // }

  if (tipoOpcao == 1 && document.querySelector("#DescricaoSimplesInput").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#DescricaoSimplesInput").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcao == 1 && document.querySelector("#TituloSimplesInput").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloSimplesInput").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcao == 3 && document.querySelector("#TituloOpcaoMenuInput").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloOpcaoMenuInput").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcao == 3 && document.querySelector("#TituloMenuInput").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloMenuInput").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcao == 3 && document.querySelector("#corpoMenuInput").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#corpoMenuInput").classList.add("errorParaCampos")
    critica = true
  }

  if (critica == true) {
    return
  }

  await AdicionarEmDados(e)
}

export const verificarRegrasSalvarOptionAtt = async (e) => {
  console.log("Clickado, verificarRegrasSalvarOptionAtt")
  console.log("tipoOpcaoAtt")
  console.log(tipoOpcaoAtt)
  let critica = false
  if (tipoOpcaoAtt == 0) {
    critica = true
  }

  if (tipoOpcaoAtt == 6 && document.querySelector("#TituloOpcaoMenuInputRedirecionamentoAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloOpcaoMenuInputRedirecionamentoAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 1 && document.querySelector("#DescricaoSimplesInputAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#DescricaoSimplesInputAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 1 && document.querySelector("#TituloSimplesInputAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloSimplesInputAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 4 && document.querySelector("#TituloSimplesInputIAAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloSimplesInputIAAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 3 && document.querySelector("#TituloOpcaoMenuInputAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloOpcaoMenuInputAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 3 && document.querySelector("#TituloMenuInputAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#TituloMenuInputAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (tipoOpcaoAtt == 3 && document.querySelector("#corpoMenuInputAtt").value.length == 0) {
    if (critica == false) {
      alert("Campos Obrigatorio não foram preenchidos")
    }
    document.querySelector("#corpoMenuInputAtt").classList.add("errorParaCampos")
    critica = true
  }

  if (critica == true) {
    return
  }

  await AtualizarEmDados(e)
}

// document.querySelector("#SelectTipo").addEventListener("change", (element) => {

//   if (element.target.value == "0") {
//     document.querySelector('#DepartamentoSelect').style.display = "none"
//     document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
//     document.querySelector("#TextareaSelect").style.display = "none"
//     document.querySelector('#MultiplaEscolha').style.display = "none"
//     document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
//   }

//   if (element.target.value == "1") {
//     document.querySelector('#DepartamentoSelect').style.display = "block"
//     document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
//     document.querySelector('#MultiplaEscolha').style.display = "none"
//     document.querySelector('#RedirecionamentoInputsSections').style.display = "flex"
//     document.querySelector("#TextareaSelect").style.display = "none"
//   }

//   if (element.target.value == "2") {
//     document.querySelector("#FinalizarAtendimentoSelect").style.display = "Flex"
//     document.querySelector("#TextareaSelect").style.display = "Flex"
//     document.querySelector('#DepartamentoSelect').style.display = "none"
//     document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
//   }

//   if (element.target.value == "3") {
//     document.querySelector('#MultiplaEscolha').style.display = "Flex"
//     document.querySelector('#DepartamentoSelect').style.display = "none"
//     document.querySelector("#FinalizarAtendimentoSelect").style.display = "none"
//     document.querySelector('#RedirecionamentoInputsSections').style.display = "none"
//     document.querySelector("#TextareaSelect").style.display = "none"
//   }

// });


// Funções para simular o backend enquanto não integra com a página principal

const getMenuPorId = (codigo) => data.filter(x => x.codigo == codigo)[0];
const getMenuPorTipo = (Tipo) => data.filter(x => x.tipo == Tipo)[0];

// Função para gerar o HTML dos menus em cascata
const gerarMenuHtml = (menu, nivel = 0) => {

  let DontRemoveInitialMenu = ""

  if (MenuCounter !== 0) {
    DontRemoveInitialMenu = `
      <li>
        <a class="dropdown-item border-top-5 border-dark"
           data-bs-toggle="modal"
           data-bs-target="#exampleModal2"
           href="#"
           id="MenuDeletar${menu.codigo}"
           data-body-menu="${menu.body}"
           data-footer-menu="${menu.footer}"
           data-header-menu="${menu.header}"
           data-tipo-menu="${menu.tipo}"
           data-titulo-menu="${menu.titulo}">
           Excluir Menu
        </a>
      </li>`;
  }



  MenuCounter = MenuCounter + 20
  let marginClass = `marginClasses-${nivel}`;
  let menuHtml = `
        <div class="col-12 p-0 mt-4 ${marginClass}">
            <div class="bg-light p-4 rounded-0 border border-dark border-2 menu" id="Menu${menu.codigo}">
                <strong class="h6 text-dark">${menu.titulo}</strong>
                <div class="dropdown">
                    <button class="btn text-dark border-0" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                        <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="currentColor" class="bi bi-arrow-right-circle" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M1 8a7 7 0 1 0 14 0A7 7 0 0 0 1 8m15 0A8 8 0 1 1 0 8a8 8 0 0 1 16 0M4.5 7.5a.5.5 0 0 0 0 1h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 1 0-.708.708L10.293 7.5z"/>
                        </svg>
                    </button>
                    <ul class="dropdown-menu dropdown-menu-start dropdownMargin" aria-labelledby="dropdownMenuButton">
                        <li>
                          <a data-bs-toggle="modal" data-bs-target="#exampleModal" 
                            onclick="localStorage.setItem('MenId', ${menu.codigo})" 
                            class="dropdown-item" href="#">
                            Adicionar Opção
                          </a>
                        </li>
                        <li>
                          <a class="dropdown-item"
                            data-bs-toggle="modal"
                            id="MenuAtualizar${menu.tipo}"
                            data-body-menu="${menu.body}"
                            data-footer-menu="${menu.footer}"
                            data-header-menu="${menu.header}"
                            data-tipo-menu="${menu.tipo}"
                            data-titulo-menu="${menu.titulo}"
                            onclick="localStorage.setItem('MenId', ${menu.codigo});"
                            data-bs-target="#exampleModalAtualizacao">
                            Atualizar Menu
                          </a>
                        </li>
                        ${DontRemoveInitialMenu}
                    </ul>
                </div>
            </div>
        </div>
    `;

  menu.options.forEach((option) => {
    optionsCounter = optionsCounter + 10
    //<strong class="h6 text-dark">sou uma option de ${menu.titulo} | ${option.titulo}</strong>
    var optionNivel = nivel + 1;
    let optionMarginClass = `marginClasses-${optionNivel}`;

    let TipoACriacao = ``
    let TipoAAtualziacao = ``

    //console.log("Opção Aqui")
    //console.log(option)

    if (option.tipo == 3) {
      console.log("menu selecionado")
      const menuSelecionado = getMenuPorId(parseInt(option.resposta))
      console.log(menuSelecionado)
      TipoAAtualziacao = `
         <li>
            <a class="dropdown-item" 
              id="OptionAtualizar${option.tipo}"
              data-bs-toggle="modal"
              data-titulo-option="${option.titulo}"
              data-descricao-option="${option.descricao}"
              data-resposta-option="${option.resposta}"
              data-finalizar-option="${option.finalizar}"
              data-menu-cabecalho-option="${menuSelecionado?.header}"
              data-menu-bababa-option="${menuSelecionado?.titulo}"
              data-menu-corpo-option="${menuSelecionado?.body}"
              data-menu-rodape-option="${menuSelecionado?.footer}"
              onclick="localStorage.setItem('OptId', ${option.codigo}); localStorage.setItem('MenId', ${option.codigoMenu});" 
              data-bs-target="#exampleModalAtualizacao">
              Atualizar Opção
            </a>
          </li>
          `
    }

    if (option.tipo == 5) {
      TipoACriacao = `
        <li>
          <a data-bs-toggle="modal" data-bs-target="#exampleModal" 
            onclick="localStorage.setItem('MenId', ${option.resposta})" 
            class="dropdown-item" href="#">
            Adicionar Opção Como Resposta
          </a>
        </li>`
    }

    if (option.tipo != 5 && option.tipo != 3) {
      TipoAAtualziacao = `
           <li>
              <a class="dropdown-item" 
                id="OptionAtualizar${option.tipo}"
                data-bs-toggle="modal"
                data-titulo-option="${option.titulo}"
                data-descricao-option="${option.descricao}"
                data-resposta-option="${option.resposta}"
                data-finalizar-option="${option.finalizar}"
                onclick="localStorage.setItem('OptId', ${option.codigo}); localStorage.setItem('MenId', ${option.codigoMenu});" 
                data-bs-target="#exampleModalAtualizacao">
                Atualizar Opção
              </a>
            </li>
            `
    }


    menuHtml += `
        <div class="col-12 p-0 mt-4 ${optionMarginClass}">
            <div class="bg-light p-4 rounded-0 border border-dark border-2 menu" id="option${option.codigo}">
                
                <strong class="h6 text-dark">${option.titulo}</strong>
                <div class="dropdown">
                    <button class="btn text-dark border-0" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                        <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="currentColor" class="bi bi-arrow-right-circle" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M1 8a7 7 0 1 0 14 0A7 7 0 0 0 1 8m15 0A8 8 0 1 1 0 8a8 8 0 0 1 16 0M4.5 7.5a.5.5 0 0 0 0 1h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 1 0-.708.708L10.293 7.5z"/>
                        </svg>
                    </button>
                    <ul class="dropdown-menu dropdown-menu-start dropdownMargin" aria-labelledby="dropdownMenuButton">
                        ${TipoACriacao}
                        ${TipoAAtualziacao}
                        <li>
                          <a class="dropdown-item border-top-5 border-dark" 
                            id="OptionDeletar${option.codigo}"
                            data-bs-toggle="modal"
                            data-bs-target="#exampleModal2"
                            href="#"
                            data-titulo-option="${option.titulo}"
                            data-tipo-option="${option.tipo}"
                            data-descricao-option="${option.descricao}"
                            data-resposta-option="${option.resposta}"
                            data-finalizar-option="${option.finalizar}">
                            Excluir Opção
                          </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        `;


    // Se a opção tiver um submenu, gera o HTML recursivamente
    if (option.tipo == 3 || option.tipo == 4) {
      const subMenu = getMenuPorId(parseInt(option.resposta));
      if (subMenu) {
        menuHtml += gerarMenuHtml(subMenu, optionNivel + 1);
      }
    }
  });

  return menuHtml;
};

export const resetarAndStartPlumbJS = () => {
  const containerPaiFluxoCards = document.querySelector(".ContainerPaiFluxoCards");
  if (containerPaiFluxoCards) {
    // console.log("Entrou no if ao resetar")
    containerPaiFluxoCards.scrollTop = 0;
  }

  // console.log("Ativou a função de repaint jsPlumb")
  //esse esquema com console.log so serve para desativar as mensagens que o plumbjs fica setando
  const originalLog = console.log;
  console.log = function () { };
  jsPlumb.deleteEveryConnection();
  jsPlumb.deleteEveryEndpoint();
  jsPlumb.reset();
  data.forEach(element => {
    conectarMenus(element);
  });
  jsPlumb.repaintEverything();
  console.log = originalLog;
};

const conectarMenus = (menu) => {
  menu.options.forEach(option => {

    jsPlumb.connect({
      source: `Menu${option.codigoMenu}`,
      target: `option${option.codigo}`,
      anchors: ["BottomLeft", "Left"],
      connector: ["Flowchart"],
      paintStyle: { stroke: "#000", strokeWidth: 2 },
      endpoint: "Blank",
      overlays: [
        ["Arrow", { width: 10, length: 10, location: 1 }]
      ]
    });

    // if (loopsCount == 0) {
    jsPlumb.connect({
      source: `CreateMenu`,
      target: `Menu${getMenuPorTipo(1).codigo}`.trim(),
      anchors: ["BottomLeft", "Left"],
      connector: ["Flowchart"],
      paintStyle: { stroke: "#000", strokeWidth: 2 },
      endpoint: "Blank",
      overlays: [
        ["Arrow", { width: 10, length: 10, location: 1 }]
      ]
    });
    // loopsCount = loopsCount + 1
    // }

    if (option.tipo == 3 || option.tipo == 4) {
      jsPlumb.connect({
        source: `option${option.codigo}`,
        target: `Menu${option.resposta}`,
        anchors: ["BottomLeft", "Left"],
        connector: ["Flowchart"],
        paintStyle: { stroke: "#000", strokeWidth: 2 },
        endpoint: "Blank",
        overlays: [
          ["Arrow", { width: 10, length: 10, location: 1 }]
        ]
      });
    }

  });
};

export const Iniciar = () => {
  jsPlumb.deleteEveryConnection();
  jsPlumb.repaintEverything();
  const MenuInicial = getMenuPorTipo(1);
  const menuHtml = gerarMenuHtml(MenuInicial);
  document.querySelector("#LinhaMenuPrincipal").innerHTML = `<div class="col-12 p-0">
            <button class="btn buttonAdicionarFromHome btnHoverClass" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick="localStorage.setItem('MenId', ${getMenuPorTipo(1).codigo})" id="CreateMenu"><strong>Adicionar Opção</strong></button>
        </div>
        ` + menuHtml;
  resetarAndStartPlumbJS()
}

// jsPlumb.ready(function () {
//   Iniciar()
// });

export const AtualizarEmDados = async (e) => {
  console.log(e)
  // e.target.disabled = true
  // e.target.innerHtml = "Loading..."
  var selectValue = document.querySelector("#SelectTipoAtt").value
  //Lembrar que como os id e 1,1 ele vao se preencher suave nao precisa ficar com dor de cabeca sobre isso
  // Ajustando o horário para o fuso horário de Brasília (UTC-3)
  const now = new Date();
  const HorarioDeBrasilia = new Date(now.setHours(now.getUTCHours() - 3))
  console.log("Fehcar modal foi executado")
  console.log(document.getElementById("exampleModalAtualizacao"))
  var demoModal = bootstrap.Modal.getInstance(
    document.getElementById("exampleModalAtualizacao")
  );
  demoModal.hide();

  var UsuarioLogadoIdResult = await UsuarioLogado()
  var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)
  console.log("Id Usuario logado")
  console.log(UsuarioLogadoId)


  if (tipoOpcaoAtt == 5) {
    const NewMenu = {
      "codigo": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloMenuInputAtt").value,
      "header": document.querySelector("#cabecalhoMenuInputAtt").value,
      "body": document.querySelector("#corpoMenuInputAtt").value,
      "footer": document.querySelector("#rodapeMenuInputAtt").value,
      "tipo": 2,
      "CodigoLogin": UsuarioLogadoId,
    }

    await AtualizarMenu(NewMenu)
    Iniciar();
    return
  }

  if (tipoOpcaoAtt == 4) {
    // const newOption = {
    //   "codigo": optionsCounter,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "titulo": document.querySelector("#TituloSimplesInput").value,
    //   "descricao": document.querySelector("#DescricaoSimplesInput").value,
    //   "resposta": document.querySelector("#textAreaContent").value,
    //   "tipo": 1,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    const newOption = {
      "codigo": parseInt(localStorage.getItem("OptId")),
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloSimplesInputIAAtt").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#textAreaContentIAAtt").value,
      "tipo": 4,
      "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
    }

    let optionResponse = await AtualziarOpcao(newOption)

    if (optionResponse) {
      // Recupera o menu atual
      const menu = getMenuPorId(parseInt(localStorage.getItem("MenId")));

      // Encontra o índice da opção com o mesmo código
      const existingOptionIndex = menu.options.findIndex(option => option.codigo === newOption.codigo);

      if (existingOptionIndex !== -1) {
        // Se a opção já existe, substitua-a
        menu.options[existingOptionIndex] = newOption;
      } else {
        // Se não existe, adicione a nova opção
        menu.options.push(newOption);
      }

      Iniciar();
    }

    return
  }

  if (tipoOpcaoAtt == 3) {


    if (idmenuresponse != "") {
      const newOption = {
        "codigo": parseInt(localStorage.getItem("OptId")),
        "CodigoLogin": UsuarioLogadoId,
        "codigoMenu": parseInt(localStorage.getItem("MenId")),
        "data": HorarioDeBrasilia,
        "titulo": document.querySelector("#TituloOpcaoMenuInputAtt").value,
        "descricao": document.querySelector("#DescricaoMenuInputAtt").value,
        "resposta": idmenuresponse,
        "tipo": 3,
        "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
      }

      await AtualziarOpcao(newOption)
      Iniciar();
      return
    } else {

      const NewMenu = {
        "titulo": document.querySelector("#TituloMenuInputAtt").value,
        "header": document.querySelector("#cabecalhoMenuInputAtt").value,
        "body": document.querySelector("#corpoMenuInputAtt").value,
        "footer": document.querySelector("#rodapeMenuInputAtt").value,
        "tipo": 2,
        "CodigoLogin": UsuarioLogadoId,
        "options": []
      }

      let MenuResponse = await AtualizarMenu(NewMenu)

      const newOption = {
        "codigo": parseInt(localStorage.getItem("OptId")),
        "CodigoLogin": UsuarioLogadoId,
        "codigoMenu": parseInt(localStorage.getItem("MenId")),
        "data": HorarioDeBrasilia,
        "titulo": document.querySelector("#TituloOpcaoMenuInputAtt").value,
        "descricao": document.querySelector("#DescricaoMenuInputAtt").value,
        "resposta": MenuResponse.codigo.toString(),
        "tipo": 3,
        "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
      }

      if (MenuResponse) {
        let optionResponse = await AtualziarOpcao(newOption)

        if (optionResponse) {
          data.push(MenuResponse)
          getMenuPorId(localStorage.getItem("MenId")).options.push(optionResponse)
          Iniciar()
        }

      }

      return
    }

  }

  if (selectValue == "6") {

    //esquema para deixar fixo para teste 

    // const newOption = {
    //   "codigo": optionsCounter,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "titulo": document.querySelector("#TituloOpcaoMenuInputRedirecionamento").value,
    //   "descricao": document.querySelector("#DescricaoMenuInputRedirecionamento").value,
    //   "resposta": document.querySelector("#selectDepartamento").id,
    //   "tipo": 6,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    const newOption = {
      "codigo": parseInt(localStorage.getItem("OptId")),
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloOpcaoMenuInputRedirecionamentoAtt").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#DescricaoMenuInputRedirecionamentoAtt").value,
      "resposta": document.querySelector("#selectDepartamentoAtt").options[document.querySelector("#selectDepartamentoAtt").selectedIndex].value,
      "tipo": 6,
      "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
    };

    let optionResponse = await AtualziarOpcao(newOption);

    if (optionResponse) {
      // Recupera o menu atual
      const menu = getMenuPorId(parseInt(localStorage.getItem("MenId")));

      // Encontra o índice da opção com o mesmo código
      const existingOptionIndex = menu.options.findIndex(option => option.codigo === newOption.codigo);

      if (existingOptionIndex !== -1) {
        // Se a opção já existe, substitua-a
        menu.options[existingOptionIndex] = newOption;
      } else {
        // Se não existe, adicione a nova opção
        menu.options.push(newOption);
      }

      Iniciar();
    }


  }

  if (selectValue == "1") {

    // const newOption = {
    //   "codigo": optionsCounter,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "titulo": document.querySelector("#TituloSimplesInput").value,
    //   "descricao": document.querySelector("#DescricaoSimplesInput").value,
    //   "resposta": document.querySelector("#textAreaContent").value,
    //   "tipo": 1,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    const newOption = {
      "codigo": parseInt(localStorage.getItem("OptId")),
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloSimplesInputAtt").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#DescricaoSimplesInputAtt").value,
      "resposta": document.querySelector("#textAreaContentAtt").value,
      "tipo": 1,
      "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
    }

    let optionResponse = await AtualziarOpcao(newOption)

    if (optionResponse) {
      // Recupera o menu atual
      const menu = getMenuPorId(parseInt(localStorage.getItem("MenId")));

      // Encontra o índice da opção com o mesmo código
      const existingOptionIndex = menu.options.findIndex(option => option.codigo === newOption.codigo);

      if (existingOptionIndex !== -1) {
        // Se a opção já existe, substitua-a
        menu.options[existingOptionIndex] = newOption;
      } else {
        // Se não existe, adicione a nova opção
        menu.options.push(newOption);
      }

      Iniciar();
    }

    return
  }

  if (selectValue == "3") {

    const NewMenu = {
      "titulo": document.querySelector("#TituloMenuInputAtt").value,
      "header": document.querySelector("#cabecalhoMenuInputAtt").value,
      "body": document.querySelector("#corpoMenuInputAtt").value,
      "footer": document.querySelector("#rodapeMenuInputAtt").value,
      "tipo": 2,
      "CodigoLogin": UsuarioLogadoId,
      "options": []
    }

    let MenuResponse = await AtualizarMenu(NewMenu)

    const newOption = {
      "codigo": parseInt(localStorage.getItem("OptId")),
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "data": HorarioDeBrasilia,
      "titulo": document.querySelector("#TituloOpcaoMenuInputAtt").value,
      "descricao": document.querySelector("#DescricaoMenuInputAtt").value,
      "resposta": MenuResponse.codigo.toString(),
      "tipo": 3,
      "finalizar": document.querySelector("#FinalizarCheckedAtt").checked
    }

    if (MenuResponse) {
      let optionResponse = await AtualziarOpcao(newOption)

      if (optionResponse) {
        data.push(MenuResponse)
        getMenuPorId(localStorage.getItem("MenId")).options.push(optionResponse)
        Iniciar()
      }

    }
  }
  // e.target.disabled = false
  // e.target.innerHtml = "Salvar"
  // console.log("Chegou no final da function de adicionar em dados")
}

export const AdicionarEmDados = async (e) => {
  console.log(e)
  // e.target.disabled = true
  // e.target.innerHtml = "Loading..."
  var selectValue = document.querySelector("#SelectTipo").value
  //Lembrar que como os id e 1,1 ele vao se preencher suave nao precisa ficar com dor de cabeca sobre isso
  // Ajustando o horário para o fuso horário de Brasília (UTC-3)
  const now = new Date();
  const HorarioDeBrasilia = new Date(now.setHours(now.getUTCHours() - 3))
  console.log("Fehcar modal foi executado")
  console.log(document.getElementById("exampleModal"))
  var demoModal = bootstrap.Modal.getInstance(
    document.getElementById("exampleModal")
  );
  demoModal.hide();
  var UsuarioLogadoIdResult = await UsuarioLogado()
  var UsuarioLogadoId = parseInt(UsuarioLogadoIdResult.usuarioLogadoId)

  if (selectValue == "4") {
    const newOption = {
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloSimplesInputIA").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#textAreaContentIA").value,
      "tipo": 4,
      "finalizar": document.querySelector("#FinalizarChecked").checked
    }

    let optionResponse = await AdicionarNovaOpcao(newOption)

    if (optionResponse) {
      getMenuPorId(parseInt(localStorage.getItem("MenId"))).options.push(newOption)
      Iniciar()
    }

    return
  }

  if (selectValue == "6") {


    console.log("Id Usuario logado")
    console.log(UsuarioLogadoId)
    //esquema para deixar fixo para teste 

    // const newOption = {
    //   "codigo": optionsCounter,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "titulo": document.querySelector("#TituloOpcaoMenuInputRedirecionamento").value,
    //   "descricao": document.querySelector("#DescricaoMenuInputRedirecionamento").value,
    //   "resposta": document.querySelector("#selectDepartamento").id,
    //   "tipo": 6,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    const newOption = {
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloOpcaoMenuInputRedirecionamento").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#DescricaoMenuInputRedirecionamento").value,
      "resposta": document.querySelector("#selectDepartamento").options[document.querySelector("#selectDepartamento").selectedIndex].value,
      "tipo": 6,
      "finalizar": document.querySelector("#FinalizarChecked").checked
    }

    let optionResponse = await AdicionarNovaOpcao(newOption)

    if (optionResponse) {
      getMenuPorId(parseInt(localStorage.getItem("MenId"))).options.push(newOption)
      Iniciar()
    }

  }

  if (selectValue == "1") {

    // const newOption = {
    //   "codigo": optionsCounter,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "titulo": document.querySelector("#TituloSimplesInput").value,
    //   "descricao": document.querySelector("#DescricaoSimplesInput").value,
    //   "resposta": document.querySelector("#textAreaContent").value,
    //   "tipo": 1,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    const newOption = {
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "titulo": document.querySelector("#TituloSimplesInput").value,
      "data": HorarioDeBrasilia,
      "descricao": document.querySelector("#DescricaoSimplesInput").value,
      "resposta": document.querySelector("#textAreaContent").value,
      "tipo": 1,
      "finalizar": document.querySelector("#FinalizarChecked").checked
    }

    let optionResponse = await AdicionarNovaOpcao(newOption)

    if (optionResponse) {
      getMenuPorId(parseInt(localStorage.getItem("MenId"))).options.push(newOption)
      Iniciar()
    }

    return
  }

  if (selectValue == "3") {

    // const NewMenu = {
    //   "codigo": MenuCounter,
    //   "titulo": document.querySelector("#TituloMenuInput").value,
    //   "header": document.querySelector("#cabecalhoMenuInput").value,
    //   "body": document.querySelector("#corpoMenuInput").value,
    //   "footer": document.querySelector("#rodapeMenuInput").value,
    //   "tipo": 2,
    //   "CodigoLogin": UsuarioLogadoId,
    //   "options": []
    // }

    const NewMenu = {
      "titulo": document.querySelector("#TituloMenuInput").value,
      "header": document.querySelector("#cabecalhoMenuInput").value,
      "body": document.querySelector("#corpoMenuInput").value,
      "footer": document.querySelector("#rodapeMenuInput").value,
      "tipo": 2,
      "CodigoLogin": UsuarioLogadoId,
      "options": []
    }

    // const newOption = {
    //   "codigo": optionsCounter,
    //   "CodigoLogin": UsuarioLogadoId,
    //   "codigoMenu": parseInt(localStorage.getItem("MenId")),
    //   "data": HorarioDeBrasilia,
    //   "titulo": document.querySelector("#TituloOpcaoMenuInput").value,
    //   "descricao": document.querySelector("#DescricaoMenuInput").value,
    //   "resposta": NewMenu.codigo.toString(),
    //   "tipo": 3,
    //   "finalizar": document.querySelector("#FinalizarChecked").checked
    // }

    let MenuResponse = await AdicionarNovoMenu(NewMenu)

    const newOption = {
      "CodigoLogin": UsuarioLogadoId,
      "codigoMenu": parseInt(localStorage.getItem("MenId")),
      "data": HorarioDeBrasilia,
      "titulo": document.querySelector("#TituloOpcaoMenuInput").value,
      "descricao": document.querySelector("#DescricaoMenuInput").value,
      "resposta": MenuResponse.codigo.toString(),
      "tipo": 3,
      "finalizar": document.querySelector("#FinalizarChecked").checked
    }

    if (MenuResponse) {
      let optionResponse = await AdicionarNovaOpcao(newOption)

      if (optionResponse) {
        data.push(MenuResponse)
        getMenuPorId(localStorage.getItem("MenId")).options.push(optionResponse)
        Iniciar()
      }

    }
  }
  // e.target.disabled = false
  // e.target.innerHtml = "Salvar"
  // console.log("Chegou no final da function de adicionar em dados")
}

