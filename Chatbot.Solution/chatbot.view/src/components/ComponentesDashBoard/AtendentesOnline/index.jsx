import { useEffect } from "react";
import "./style.css";

export default function AtendimentosPendentes(props) {
  // Ativar os tooltips após a montagem do componente
  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(
      (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
    );
    return () => {
      // Limpar tooltips ao desmontar o componente
      tooltipList.forEach((tooltip) => tooltip.dispose());
    };
  }, []);

  return (
    <div className="card secondCardsClass">
      <div className="AtendimentoOnHeader">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="64"
          height="64"
          fill="#263a6d"
          className="bi bi-router"
          viewBox="0 0 16 16"
        >
          <path d="M4 16s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1zm4-5.95a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5" />
          <path d="M2 1a2 2 0 0 0-2 2v9.5A1.5 1.5 0 0 0 1.5 14h.653a5.4 5.4 0 0 1 1.066-2H1V3a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1v9h-2.219c.554.654.89 1.373 1.066 2h.653a1.5 1.5 0 0 0 1.5-1.5V3a2 2 0 0 0-2-2z" />
        </svg>
      </div>
      <div className="AtendimentoOnColumn">
        <p
          className="AtendimentoOnTitle"

        >
          Atendentes online
        </p>
        <p className="AtendimentoOnNumber">{props.qtdAtendentesOnline}</p>
      </div>
      <svg
        data-bs-toggle="tooltip"
        data-bs-title="Quantidade de Atendentes Online"
        xmlns="http://www.w3.org/2000/svg"
        width="16"
        height="16"
        fill="#263a6d"
        className="bi bi-info-circle AtendimentoOnIcon"
        viewBox="0 0 16 16"
      >
        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
        <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
      </svg>
    </div>
  );
}
