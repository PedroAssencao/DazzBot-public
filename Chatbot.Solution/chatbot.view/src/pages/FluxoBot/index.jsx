import './style.css';
import { Iniciar, resetarAndStartPlumbJS, fetchNewDatas, SelectTipoHandEventAtualizacao } from '../../Repository/FluxoBoxRepository/index';
import { UsuarioLogado, urlBase } from '../../appsettings';
import { useEffect, useState } from 'react';
import BaseModal from '../../components/BaseComponents/BaseModal';
import LoadScreen from '../../components/BaseComponents/loadingScreen';
import HeaderControlFluxobot from '../../components/ComponentesFluxoBot/HeaderControlFluxoBot';
import ModalDeAdicaoFluxoBot from '../../components/ComponentesFluxoBot/ModalDeAdicaoFluxobot';
import ModalDeAtualizacaoFluxoBot from '../../components/ComponentesFluxoBot/ModalDeAtualizacaoFluxobot';
import SidebarControlFluxoBot from '../../components/ComponentesFluxoBot/SideBarControlFluxoBot';

export default function FluxoBot() {
  const [IsDataLoad, SetLoadDate] = useState(false);
  const [isChange, SetisChange] = useState({});
  const [objAtivo, setObjAtivo] = useState({});


  const initialize = async () => {
    console.log("Initialize foi chamado")
    try {
      const result = await fetchNewDatas();
      if (result) {
        jsPlumb.ready(Iniciar);
        window.addEventListener('resize', resetarAndStartPlumbJS);
        SetLoadDate(true);

        document.querySelectorAll('[id*="OptionAtualizar"]').forEach(element => {
          element.addEventListener("click", () => {
            const id = element.id.replace("OptionAtualizar", "");
            const tituloOption = element.dataset.tituloOption;
            const descricaoOption = element.dataset.descricaoOption;
            const respostaOption = element.dataset.respostaOption;
            const finalizarOption = element.dataset.finalizarOption;
            const MenuRodape = element.dataset.menuRodapeOption;
            const MenuCorpo = element.dataset.menuCorpoOption;
            const MenuTitulo = element.dataset.menuBababaOption;
            const menuCabecalho = element.dataset.menuCabecalhoOption;

            console.log("tituloOption:", tituloOption);
            console.log("descricaoOption:", descricaoOption);
            console.log("respostaOption:", respostaOption);
            console.log("finalizarOption:", finalizarOption);


            console.log("menu titulo: ", MenuTitulo)
            SelectTipoHandEventAtualizacao({ tipo: "option", codigo: id, menuRespose: respostaOption });
            SetisChange({
              tipo: "option",
              codigo: id,
              tituloOpcao: tituloOption,
              descricaoOpcao: descricaoOption,
              respostaopcao: respostaOption,
              finalizarAtendimento: finalizarOption,
              tituloMenu: MenuTitulo,
              descricaoMenu: "",
              cabecalhomenu: menuCabecalho,
              rodapemenu: MenuRodape,
              corpomenu: MenuCorpo
            });
          });
        });

        document.querySelectorAll('[id*="OptionDeletar"]').forEach(element => {
          element.addEventListener("click", () => {
            const id = element.id.replace("OptionDeletar", "");
            const tituloOption = element.dataset.tituloOption;
            const respostaOption = element.dataset.respostaOption;
            const tipoOption = element.dataset.tipoOption;

            if (tipoOption == 3) {
              setObjAtivo({ optid: id, menid: respostaOption, nome: tituloOption });
            } else {
              setObjAtivo({ optid: id, menid: 0, nome: tituloOption });
            }

          });
        });

        document.querySelectorAll('[id*="MenuAtualizar"]').forEach(element => {
          element.addEventListener("click", () => {
            const id = element.id.replace("MenuAtualizar", "");
            const bodymenu = element.dataset.bodyMenu;
            const footermenu = element.dataset.footerMenu;
            const headermenu = element.dataset.headerMenu;
            const tipomenu = element.dataset.tipoMenu;
            const titulomenu = element.dataset.tituloMenu;

            console.log("bodymenu:", bodymenu);
            console.log("footermenu:", footermenu);
            console.log("headermenu:", headermenu);
            console.log("tipomenu:", tipomenu);
            console.log("titulomenu:", titulomenu);

            SelectTipoHandEventAtualizacao({ tipo: "menu", codigo: id });
            SetisChange({
              tipo: "menu",
              codigo: id,
              tituloOpcao: "",
              descricaoOpcao: "",
              respostaopcao: "",
              finalizarAtendimento: "",
              tituloMenu: titulomenu,
              descricaoMenu: "",
              cabecalhomenu: headermenu,
              rodapemenu: footermenu,
              corpomenu: bodymenu
            });
          });
        });

        document.querySelectorAll('[id*="MenuDeletar"]').forEach(element => {
          element.addEventListener("click", () => {
            const id = element.id.replace("MenuDeletar", "");
            const titulomenu = element.dataset.tituloMenu;

            setObjAtivo({ optid: 0, menid: id, nome: titulomenu });
          });
        });

      } else {
        alert("Ocorreu algum erro ao tentar carregar os dados");
      }
    } catch (error) {
      console.error("Erro ao inicializar os dados:", error);
    }
  };

  const ExcluirOptionEMenu = async (param) => {
    console.log("Excluir option foi clickado")
    let url = `${urlBase}/v1/Option/Option/DeleteOptionCascade?menid=${param.menid}&optid=${param.optid}`;

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

      if (response.ok) {
        await initialize()
      }

      const modalElement = document.getElementById('exampleModal2');
      const bootstrapModal = bootstrap.Modal.getInstance(modalElement);
      bootstrapModal.hide();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    const checkUserAndInitialize = async () => {
      const result = await UsuarioLogado();

      if (result.usuarioLogadoId == null) {
        alert("Usuário precisa estar logado");
        location.replace(location.origin + "/login");
      } else if (result.tipoUsuario === "Atendente") {
        alert("Usuário não tem permissão para entrar na tela");
        location.replace(location.origin + "/home");
      } else {
        await initialize();
      }
    };

    checkUserAndInitialize();

    return () => {
      window.removeEventListener('resize', resetarAndStartPlumbJS);
    };
  }, []);


  // Estilo para o body da página
  document.querySelector("#bodyFromPageAll").style.overflowY = "auto";

  return (
    <div className='col'>
      {!IsDataLoad ? (
        <LoadScreen />
      ) : null}

      <>
        <div className="container-fluid border-bottom border-secondary border-2 mt-5">
          <h2 className="h2 TituloForFluxoBot">Dazzle Bot</h2>
        </div>

        <div className="row container-fluid flex-column flex-md-row p-0 mt-3 gap-5" style={{ marginLeft: "1px" }}>
          <div className="col-12 TamanhoSubMenuFluxobot p-0 bg-light containerInicio">
            <SidebarControlFluxoBot />
          </div>

          <div className="col p-0 bg-light ContainerInfos">
            <HeaderControlFluxobot />

            <div className="container-fluid overflow-x-hidden" id='ParentContainerToRender'>
              <div className="row p-3 ContainerPaiFluxoCards">
                {/* Linha do Primeiro Menu */}
                <div className="col-12 p-0" id="LinhaMenuPrincipal" style={{ marginLeft: "2rem" }}></div>
              </div>
            </div>

            <ModalDeAdicaoFluxoBot initialize={initialize} />
            <ModalDeAtualizacaoFluxoBot inicialise={initialize} Dados={isChange} />

            <BaseModal
              id="exampleModal2"
              HasHeader={false}
              ModalSize={null}
              Description={`Você realmente deseja a exclusão do item: ${objAtivo.nome}`}
              ButtonClassName="btn buttonDeletarFromModal"
              ButtonDescription="Excluir"
              ButtonOnclick={() => ExcluirOptionEMenu({ optid: objAtivo.optid, menid: objAtivo.menid })}
            />

          </div>
        </div>

        {/* Existe apenas para dar espaçamento na tela de celulares pequenos */}
        <div className='col mt-5' style={{ visibility: "hidden" }}></div>
      </>
    </div>
  );
}
