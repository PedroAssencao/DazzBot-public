import React, { useEffect, useRef } from "react";
import "./style.css";

export default function DepartamentoChar(props) {

  const data = props.data

  const chartRef = useRef(null);

  useEffect(() => {
    const canvas = document.getElementById("Departamento");
    const ctx = canvas.getContext("2d");
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(
      (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
    );

    // Se já houver um gráfico, destrua-o
    if (chartRef.current) {
      chartRef.current.destroy();
    }

    // Criar o gráfico
    chartRef.current = new Chart(ctx, {
      type: "pie",
      data: {
        labels: data.topDepartamentos.map(departamento => departamento.nome),
        datasets: [
          {
            data: data.topDepartamentos.map(departamento => departamento.totalAtendimentos),
            backgroundColor: data.topDepartamentos.map((_, index) => {
              // Criação de gradiente para cada fatia usando as cores fornecidas
              const gradient = ctx.createLinearGradient(0, 0, 0, 400);

              // Definindo as cores do gradiente com as cores fornecidas no array
              gradient.addColorStop(0, "rgba(0, 40, 150, 0.7)"); // Cor inicial
              gradient.addColorStop(1, "rgba(0, 40, 150, 0.3)"); // Cor final

              return gradient;
            }),
          },
        ],
      },
      plugins: [ChartDataLabels],
      options: {
        responsive: true,
        plugins: {
          datalabels: {
            color: "rgb(255, 255, 255)",
            formatter: (value, ctx) => {
              const totalSum = ctx.dataset.data.reduce(
                (accumulator, currentValue) => accumulator + currentValue,
                0
              );
              const percentage = (value / totalSum) * 100;
              return `${percentage.toFixed(1)}%`;
            },
          },
          legend: {
            display: false,
          },
        },
      },
    });


    // Limpeza ao desmontar o componente
    return () => {
      tooltipList.forEach((tooltip) => tooltip.dispose());
      if (chartRef.current) {
        chartRef.current.destroy();
      }
    };
  }, []);

  return (
    <div className="card Departamento FirstCardsClass">
      <div className="DepartamentoHeader w-100 d-flex justify-content-between">
        <div className="DepartamentoInfo">
          <p className="DepartamentoTitle">Atendimentos Ativos Por Departamento</p>
          {/* <select className="form-select form-select mb-3 DepartamentoBtn" aria-label="Large select example">
          <option selected>Atendimentos Pendentes</option>
          <option value="1">Atendimentos Ativos</option>
        </select> */}
        </div>
        <svg
          data-bs-toggle="tooltip"
          data-bs-title="Atendimento Ativos de Cada Departamento"
          xmlns="http://www.w3.org/2000/svg"
          width="16"
          height="16"
          fill="#263a6d"
          className="bi bi-info-circle"
          viewBox="0 0 16 16"
        >
          <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
          <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
        </svg>
      </div>
      <hr />



      <div className="Container">
        <div className="DepartamentoGrafico">
          <canvas id="Departamento"></canvas>
        </div>
      </div>
    </div>
  );
}