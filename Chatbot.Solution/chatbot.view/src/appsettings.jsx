export const urlBase = "http://localhost:5058/api"
export const UsuarioLogado = () => {
    return fetch(urlBase + '/v1/Login/login/GetClaimsInfo', {
        credentials: 'include'
    })
        .then(response => response.json()) // Supondo que a resposta seja em JSON
        .then(data => {
            return data; // Retorna os dados recebidos
        })
        .catch(error => {
            console.error('Erro ao buscar as claims:', error);
            return null;
        });
};