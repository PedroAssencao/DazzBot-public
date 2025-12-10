import React, { useEffect } from 'react';
import './style.css';

export default function Avaliacoes() {
  useEffect(() => {
    // Verifique se o Chart.js foi carregado
    if (window.Chart) {
      const canvas = document.getElementById('Avaliacoes');
      const ctx = canvas.getContext('2d');
      
      // Se já houver um gráfico, destrua-o
      if (canvas.chart) {
        canvas.chart.destroy();
      }

      // Crie um novo gráfico
      canvas.chart = new window.Chart(ctx, {
        type: 'line',
        data: {
          labels: ['', '', '', '', '', '', ''],
          datasets: [{
            label: '',
            fill: true,
            lineTension: 0.1,
            data: [12, 19, 3, 5, 2, 3, 2],
            borderWidth: 1,
            // backgroundColor: 'rgba(0, 160, 100, 0.2)',
            backgroundColor: (context) =>{
              const bgColor = [
                "rgba(0, 160, 100, 0.7)",
                "rgba(0, 160, 100, 0.2)"
              ];
              if(!context.chart.chartArea){
                return;
              }
              console.log(context.chart.chartArea)
              const {ctx, data, chartArea: {top,bottom}} = context.chart;
              const gradientBg = ctx.createLinearGradient(0,top,0,bottom);
              gradientBg.addColorStop(0,bgColor[0])
              gradientBg.addColorStop(1,bgColor[1])
              return gradientBg
              },
            borderColor: 'rgb(0,160, 100)'
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
      const canvas = document.getElementById('Avaliacoes');
      if (canvas && canvas.chart) {
        canvas.chart.destroy();
      }
    };
  }, []); // O array vazio garante que o efeito só será executado na montagem inicial

  return (
      <div className='Mensagem'>
        <div className='Header'>
          <div className='Info'>
            <div className='MensagemIcon'>
              <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="grey" viewBox="0 0 16 16" className='bi bi-chat-dots Icon'>
              <path d="M8.864.046C7.908-.193 7.02.53 6.956 1.466c-.072 1.051-.23 2.016-.428 2.59-.125.36-.479 1.013-1.04 1.639-.557.623-1.282 1.178-2.131 1.41C2.685 7.288 2 7.87 2 8.72v4.001c0 .845.682 1.464 1.448 1.545 1.07.114 1.564.415 2.068.723l.048.03c.272.165.578.348.97.484.397.136.861.217 1.466.217h3.5c.937 0 1.599-.477 1.934-1.064a1.86 1.86 0 0 0 .254-.912c0-.152-.023-.312-.077-.464.201-.263.38-.578.488-.901.11-.33.172-.762.004-1.149.069-.13.12-.269.159-.403.077-.27.113-.568.113-.857 0-.288-.036-.585-.113-.856a2 2 0 0 0-.138-.362 1.9 1.9 0 0 0 .234-1.734c-.206-.592-.682-1.1-1.2-1.272-.847-.282-1.803-.276-2.516-.211a10 10 0 0 0-.443.05 9.4 9.4 0 0 0-.062-4.509A1.38 1.38 0 0 0 9.125.111zM11.5 14.721H8c-.51 0-.863-.069-1.14-.164-.281-.097-.506-.228-.776-.393l-.04-.024c-.555-.339-1.198-.731-2.49-.868-.333-.036-.554-.29-.554-.55V8.72c0-.254.226-.543.62-.65 1.095-.3 1.977-.996 2.614-1.708.635-.71 1.064-1.475 1.238-1.978.243-.7.407-1.768.482-2.85.025-.362.36-.594.667-.518l.262.066c.16.04.258.143.288.255a8.34 8.34 0 0 1-.145 4.725.5.5 0 0 0 .595.644l.003-.001.014-.003.058-.014a9 9 0 0 1 1.036-.157c.663-.06 1.457-.054 2.11.164.175.058.45.3.57.65.107.308.087.67-.266 1.022l-.353.353.353.354c.043.043.105.141.154.315.048.167.075.37.075.581 0 .212-.027.414-.075.582-.05.174-.111.272-.154.315l-.353.353.353.354c.047.047.109.177.005.488a2.2 2.2 0 0 1-.505.805l-.353.353.353.354c.006.005.041.05.041.17a.9.9 0 0 1-.121.416c-.165.288-.503.56-1.066.56z"/>
              </svg>
              <p className='MensagemTitle'>Avaliaçöes</p>
            </div>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#263a6d" className="bi bi-info-circle" viewBox="0 0 16 16">
              <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
              <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
            </svg>
          </div>
          <p className='Total'>Total: 184</p>
        </div>

        <div className='Grafico'>
          <canvas id="Avaliacoes"></canvas>
        </div>
      </div>
  );
}