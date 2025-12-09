import React, { useEffect } from 'react';
import './style.css';

export default function Atendimentos(props) {
  const data = props.data.atendimentosPorDia
  console.log("Dados que chegam em lead 3")
  console.log(data)
  useEffect(() => {
    // Verifique se o Chart.js foi carregado
    if (window.Chart) {
      const canvas = document.getElementById('Atendimentos');
      const ctx = canvas.getContext('2d');

      // Se já houver um gráfico, destrua-o
      if (canvas.chart) {
        canvas.chart.destroy();
      }

      const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
      const tooltipList = [...tooltipTriggerList].map(
        (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
      );

      // Crie um novo gráfico
      canvas.chart = new window.Chart(ctx, {
        type: 'line',
        data: {
          labels: data.map(x => x.nome),
          datasets: [{
            label: '',
            fill: true,
            lineTension: 0.1,
            data: data.map(x => x.totalAtendimentos),
            borderWidth: 1,
            // backgroundColor: 'rgba(0, 0, 80, 0.2)',
            backgroundColor: (context) => {
              const bgColor = [
                "rgba(0, 0, 80, 0.5)",
                "rgba(0, 0, 80, 0.2)"
              ];
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
            borderColor: 'rgb(0,0, 80)'
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
              beginAtZero: true,
              ticks: {
                display: false // Hides the Y axis labels
              },
              grid: {
                display: false // Hides the grid lines on the Y axis
              }
            },
            x: {
              grid: {
                display: false // Hides the grid lines on the X axis
              },
              ticks: {
                display: false // Hides the X axis labels
              }
            }
          }
        }
      });
    }

    // Limpeza ao desmontar o componente
    return () => {
      tooltipList.forEach((tooltip) => tooltip.dispose());
      const canvas = document.getElementById('Atendimentos');
      if (canvas && canvas.chart) {
        canvas.chart.destroy();
      }
    };
  }, []); // O array vazio garante que o efeito só será executado na montagem inicial

  return (
    <div className='Mensagem secondCardsClass flexColumnForDash'>
      <div className='AtendimentosHeader'>

        <div className='MensagemIcon'>
          <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="grey" viewBox="0 0 16 16" className='bi bi-chat-dots Icon'>
            <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10s-3.516.68-4.168 1.332c-.678.678-.83 1.418-.832 1.664z" />
          </svg>
          <p className='MensagemTitle'>Atendimentos</p>
        </div>
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#263a6d" className="bi bi-info-circle" viewBox="0 0 16 16"
          data-bs-toggle="tooltip"
          data-bs-title="Quantidade de Atendimentos Por Dia">
          <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
          <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
        </svg>


      </div>
      <div className='AtendimentosFinal'>
        <p className='Total'>Total: {props.qtdTotalAtendimentos}</p>
      </div>


      <canvas className='Grafico' id="Atendimentos"></canvas>

    </div>
  );
}