import "./style.css";
import Mensagem from "../../components/ComponentesDashBoard/Mensagem";
import Leads from "../../components/ComponentesDashBoard/Leads";
import Atendimentos from "../../components/ComponentesDashBoard/Atendimentos";
import Avaliacoes from "../../components/ComponentesDashBoard/Avaliacoes";
import { UsuarioLogado, urlBase } from "../../appsettings";
import Ativo from "../../components/ComponentesDashBoard/Ativo";
import Departamento from "../../components/ComponentesDashBoard/Departamento";
import AtendentesOnline from "../../components/ComponentesDashBoard/AtendentesOnline";
import AtendimentosAtivos from "../../components/ComponentesDashBoard/AtendimentosAtivos";
import AtendimentosPendentes from "../../components/ComponentesDashBoard/AtendimentosPendentes";
import BotsOnline from "../../components/ComponentesDashBoard/BotsOnline";
import AtendimentosAtivosProgress from "../../components/ComponentesDashBoard/AtendimentosAtivosProgress";
import { useEffect, useState } from "react";
import LoadScreen from "../../components/BaseComponents/loadingScreen";

export default function DashBoard() {

  // Vou comitar alguns graficos aqui e deixar para v2 
  const [IsLoading, setIsLoading] = useState(true);
  const [dataAtual, setDataAtual] = useState("");
  const [UsuarioLogadoObj, setUsuarioLogadoObj] = useState(null)
  const [Dados, setDados] = useState(null)
  const gatdateTimeNow = () => {
    const data = new Date();
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    setDataAtual(`${dia}/${mes}/${ano}`);
  };

  const fetchData = async (param) => {
    var url = `${urlBase}/v1/DashBoard/BuscarDadosDashBoard?logid=${param}`;
    try {
      const response = await fetch(url);

      if (!response.ok) {
        throw new Error('Erro na requisição');
      }

      const data = await response.json();
      console.log(data)
      setDados(data)
      setIsLoading(false)

    } catch (error) {
      console.log(error)
    }
  };


  useEffect(() => {
    console.log("Carregou use effect")
    UsuarioLogado().then(result => {
      console.log("Entrou no usuario logado funciton")

      //aqui podemos redirecionar para qualquer tela dependendo se o usuario esta logado, se e um usuario cliente se e master se e atendete etc...

      console.log(result)
      setUsuarioLogadoObj(result)
      if (result.usuarioLogadoId == null) {
        console.log("Usuario Redirecionado")
        // alert("Usuario precisa estar logado")
        location.replace(location.origin + "/login");
      }

      if (result.tipoUsuario == "Atendente") {
        console.log("Usuario Redirecionado")
        // alert("Usuario Não tem permissão para acessar essa tela")
        location.replace(location.origin + "/Atendimento");
      }

      gatdateTimeNow()
      fetchData(result.usuarioLogadoId)
    });
  }, []);
  return (
    <>
      {IsLoading ? (
        <LoadScreen />
      ) : (
        <div className="col">

          <div className="Header" style={{ display: "flex" }}>
            <h1 className="Title">DashBoard</h1>
            <div className="Date">
              <div id="dataAtualDashboard">
                {dataAtual}
              </div>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="30"
                height="30"
                fill="currentColor"
                className="bi bi-calendar-date"
                viewBox="0 0 16 16"
              >
                <path d="M6.445 11.688V6.354h-.633A13 13 0 0 0 4.5 7.16v.695c.375-.257.969-.62 1.258-.777h.012v4.61zm1.188-1.305c.047.64.594 1.406 1.703 1.406 1.258 0 2-1.066 2-2.871 0-1.934-.781-2.668-1.953-2.668-.926 0-1.797.672-1.797 1.809 0 1.16.824 1.77 1.676 1.77.746 0 1.23-.376 1.383-.79h.027c-.004 1.316-.461 2.164-1.305 2.164-.664 0-1.008-.45-1.05-.82zm2.953-2.317c0 .696-.559 1.18-1.184 1.18-.601 0-1.144-.383-1.144-1.2 0-.823.582-1.21 1.168-1.21.633 0 1.16.398 1.16 1.23" />
                <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4z" />
              </svg>
            </div>
          </div>
          <hr></hr>
          <div className="Content">
            <p className="DashBoardNovidades">Novidades</p>

            <div className="row justify-content-center align-items-center gap-3">
              <AtendentesOnline qtdAtendentesOnline={Dados?.atendentesDados?.qtdAtendenteOnline} />
              <AtendimentosAtivos AtendimentosAtivos={Dados?.atendimentoDados?.qtdTotalAtendimentoAtivos} />
              <AtendimentosPendentes AtendimentosPendentes={Dados?.atendimentoDados?.qtdTotalAtendimentoEsperando} />
              {/* <BotsOnline /> */}
            </div>
            <br></br>

            <div className="row justify-content-center align-items-center gap-3">
              <Ativo data={Dados?.atendimentoDados?.dadosParaGraficoPrincipal01} />
              <Departamento data={Dados?.atendimentoDados?.dadosParaGraficoPrincipal02} />
            </div>
            <br></br>

            <div className="row justify-content-center align-items-center gap-3 linhaGraph4" style={{ marginBottom: "5rem" }}>
              <Leads data={Dados?.contatosDados?.contatosPorDia} qtdTotalContatos={Dados?.contatosDados?.qtdTotalDeContatos} />
              <Mensagem data={Dados?.mensagensDados?.dadosParaGraficoSecundarioMensagens} qtdTotalMensagens={Dados?.mensagensDados?.qtdTotalDeMensagens} />
              <Atendimentos data={Dados?.atendimentoDados?.dadosParaGraficoSecundarioAtendimento} qtdTotalAtendimentos={Dados?.atendimentoDados?.qtdTotalAtendimento} />
              {/* <Avaliacoes /> */}
            </div>

            {/* <div className="GraficosPrimarios">
          <AtendimentosAtivosProgress />
          <AtendimentosAtivosProgress />
        // </div>

        <div className="GraficosPrimarios">
          <AtendimentosAtivosProgress />
          <AtendimentosAtivosProgress />
        </div> */}

          </div>
        </div>
      )}
    </>


  );
}
