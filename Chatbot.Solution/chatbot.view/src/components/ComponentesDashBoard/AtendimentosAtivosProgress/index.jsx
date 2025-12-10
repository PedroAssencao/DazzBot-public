import "./style.css";

export default function AtendimentosAtivosProgress() {

return (
    <div className="ContainerProgress">
<div className="AtendimentoAtivosProgress">
<div className="AtendimentoAtivosProgressHeader">
  <p className="AtendimentosAtivosProgressTitle">
    Atendimentos por atendente
  </p>
  <div className="AtendimentosAtivosProgressIcons">
    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-left"
      viewBox="0 0 16 16">
      <path fill-rule="evenodd"
        d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0" />
    </svg>
    <p className="AtendimentosAtivosProgressNumber" >1</p>
    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-right"
      viewBox="0 0 16 16">
      <path fill-rule="evenodd"
        d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708" />
    </svg>
  </div>
</div>
<div className="AtendimentosAtivosProgressInfo">
  <div className="AtendimentosAtivosProgressColumn">
    <p className="AtendimentosAtivosProgressFont">Nome</p>
    <p className="AtendimentosAtivosProgressFont">Total</p>
  </div>

  <div className="AtendimentosAtivosProgressBots">
    <p>Vanilda</p>
    <div className="progress AtenProgress" role="progressbar" aria-label="Basic example" aria-valuenow="75"
      aria-valuemin="0" aria-valuemax="100">
      <div className="progress-bar AtenProgressBar"></div>
    </div>
    <p>1562</p>
  </div>

  <div className="AtendimentosAtivosProgressBots">
    <p>Vanilda</p>
    <div className="progress AtenProgress" role="progressbar" aria-label="Basic example" aria-valuenow="75"
      aria-valuemin="0" aria-valuemax="100">
      <div className="progress-bar AtenProgressBar"></div>
    </div>
    <p>1562</p>
  </div>

  <div className="AtendimentosAtivosProgressBots">
    <p>Vanilda</p>
    <div className="progress AtenProgress" role="progressbar" aria-label="Basic example" aria-valuenow="75"
      aria-valuemin="0" aria-valuemax="100">
      <div className="progress-bar AtenProgressBar"></div>
    </div>
    <p>1562</p>
  </div>

  <div className="AtendimentosAtivosProgressBots">
    <p>Vanilda</p>
    <div className="progress AtenProgress" role="progressbar" aria-label="Basic example" aria-valuenow="75"
      aria-valuemin="0" aria-valuemax="100">
      <div className="progress-bar AtenProgressBar"></div>
    </div>
    <p>1562</p>
  </div>

</div>
</div>
    </div>


);
}
