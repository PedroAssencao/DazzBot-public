import "./style.css";
 import { useEffect } from "react";

export default function AtendentesOnline(props) {
 
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
          <path d="M2 1a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h9.586a2 2 0 0 1 1.414.586l2 2V2a1 1 0 0 0-1-1zm12-1a2 2 0 0 1 2 2v12.793a.5.5 0 0 1-.854.353l-2.853-2.853a1 1 0 0 0-.707-.293H2a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2z" />
          <path d="M5 6a1 1 0 1 1-2 0 1 1 0 0 1 2 0m4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0m4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
        </svg>
      </div>
      <div className="AtendimentoOnColumn">
        <p className="AtendimentoOnTitle">Atendimentos pendentes</p>
        <p className="AtendimentoOnNumber">{props.AtendimentosPendentes}</p>
      </div>
      <svg
        data-bs-toggle="tooltip"
        data-bs-title="Quantidade de Atendimentos Pendentes"
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