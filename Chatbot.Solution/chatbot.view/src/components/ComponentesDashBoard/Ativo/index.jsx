import React, { useEffect, useRef } from 'react';
import './style.css';

export default function Ativo(props) {
  const chartRef = useRef(null); // Ref para armazenar a instância do gráfico
  const data = props.data
  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(
      (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
    );

    if (window.Chart) {
      const ctx = document.getElementById('Ativo').getContext('2d');

      // Verifica se um gráfico já foi criado e o destrói
      if (chartRef.current) {
        chartRef.current.destroy();
      }

      // Cria um novo gráfico e armazena a instância na ref
      chartRef.current = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: data.topAtendentes.map(atendente => atendente.nome),
          datasets: [{
            label: 'Total Atendimento:',
            data: data.topAtendentes.map(atendente => atendente.totalAtendimentos),
            borderWidth: 1,
            backgroundColor: (context) => {
              const bgColor = [
                "rgba(75, 192, 192, 0.7)",
                "rgba(75, 192, 192, 0.2)"
              ];//colocar 11 cores diferentes aqui tambem
              if (!context.chart.chartArea) {
                return;
              }
              console.log(context.chart.chartArea)
              const { ctx, data, chartArea: { top, bottom } } = context.chart;
              const gradientBg = ctx.createLinearGradient(0, top, 0, bottom);
              gradientBg.addColorStop(0, bgColor[0])
              gradientBg.addColorStop(1, bgColor[1])
              return gradientBg
            },
            // backgroundColor: [
            //   'rgba(75, 192, 192, 0.2)',
            // ],
          }],
        },
        options: {
          plugins: {
            legend: {
              display: false // Hides the legend
            }
          },
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      });
    }

    // Limpeza ao desmontar o componente
    return () => {
      tooltipList.forEach((tooltip) => tooltip.dispose());
      if (chartRef.current) {
        chartRef.current.destroy();
      }
    };
  }, []); // O array vazio garante que o efeito só será executado na montagem inicial

  return (

    <div className="atendimentoAtivos card FirstCardsClass" style={{ position: "relative", top: "-0.5rem" }}>
      <div className='HeaderAtivo'>
        <p className='titleAtivo'>Atendimento ativos por atendentes</p>
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#263a6d" className="bi bi-info-circle"
          data-bs-toggle="tooltip"
          data-bs-title="Quantidade de Atendimentos Ativos Com Cada Atendente"
          viewBox="0 0 16 16">
          <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
          <path
            d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
        </svg>
      </div>
      <hr></hr>

      <div className='AtivoGrafico'>
        <canvas id="Ativo"></canvas>
      </div>
    </div>

  );
}