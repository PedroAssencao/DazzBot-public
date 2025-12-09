export function MensagemGraph() {

    const ctx = document.getElementById('myChart');


    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['', '', '', '', '', '', '',],
            datasets: [{
                label: '',
                fill: true,
                lineTension: 0.1,
                data: [12, 19, 3, 5, 2, 3, 2],
                borderWidth: 1,
                backgroundColor: [
                    'rgba(75, 192, 192, 0.2)',
                ],
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
                        display: false // Hides the grid lines on the Y axis
                    },
                    ticks: {
                        display: false // Hides the Y axis labels
                    }
                }
            }
        }
    });

}