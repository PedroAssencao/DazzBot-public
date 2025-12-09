import React, { useEffect } from 'react';
import './style.css';

export default function Mensagem(props) {
  const data = props.data.mensagensPorDia
  console.log("Dados que chegam em lead 2")
  console.log(data)
  useEffect(() => {
    // Verifique se o Chart.js foi carregado
    if (window.Chart) {
      const canvas = document.getElementById('Mensagem');
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
            data: data.map(x => x.totalMensagens),
            borderWidth: 1,
            // backgroundColor: 'rgba(75, 192, 192, 0.2)',
            backgroundColor: (context) => {
              const bgColor = [
                "rgba(75, 192, 192, 0.7)",
                "rgba(75, 192, 192, 0.1)"
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
            borderColor: 'rgb(75, 192, 192)'
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
      const canvas = document.getElementById('Mensagem');
      if (canvas && canvas.chart) {
        canvas.chart.destroy();
      }
    };
  }, []); // O array vazio garante que o efeito só será executado na montagem inicial

  return (
    <div className='Mensagem secondCardsClass flexColumnForDash'>
      <div className='MensagemHeader'>

        <div className='MensagemIcon'>
          <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="grey" viewBox="0 0 16 16" className='bi bi-chat-dots Icon'>
            <path d="M5 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0m4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0m3 1a1 1 0 1 0 0-2 1 1 0 0 0 0 2" />
            <path d="m2.165 15.803.02-.004c1.83-.363 2.948-.842 3.468-1.105A9 9 0 0 0 8 15c4.418 0 8-3.134 8-7s-3.582-7-8-7-8 3.134-8 7c0 1.76.743 3.37 1.97 4.6a10.4 10.4 0 0 1-.524 2.318l-.003.011a11 11 0 0 1-.244.637c-.079.186.074.394.273.362a22 22 0 0 0 .693-.125m.8-3.108a1 1 0 0 0-.287-.801C1.618 10.83 1 9.468 1 8c0-3.192 3.004-6 7-6s7 2.808 7 6-3.004 6-7 6a8 8 0 0 1-2.088-.272 1 1 0 0 0-.711.074c-.387.196-1.24.57-2.634.893a11 11 0 0 0 .398-2" />
          </svg>
          <p className='MensagemTitle'>Mensagens</p>
        </div>
        <div className='MensagemInfo'>
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#263a6d" className="bi bi-info-circle" viewBox="0 0 16 16"
            data-bs-toggle="tooltip"
            data-bs-title="Quantidade de Mensagens Recebidas Por Dia"
          >
            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
            <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
          </svg>
        </div>


        <br></br>

      </div>
      <div className='MensagemFinal'>
        <p className='Total'>Total: {props.qtdTotalMensagens}</p>
      </div>



      <canvas className='Grafico' id="Mensagem"></canvas>

    </div>
  );
}