import { urlBase, UsuarioLogado } from '../../appsettings'
var UsuarioLogadoId = null
UsuarioLogado().then(result => {
    UsuarioLogadoId = result.usuarioLogadoId
});

export function VerficarAltura() {
    var larguraJanela = window.innerHeight;

    if (document.querySelector(".ConteudoChat") != null) {
        if (larguraJanela < 559) {
            document.querySelector('.ConteudoChat').style.maxHeight = "65vh";
            document.querySelector('.ConteudoChat').style.height = "65vh";
        } else if (larguraJanela < 670) {
            document.querySelector('.ConteudoChat').style.maxHeight = "67vh";
            document.querySelector('.ConteudoChat').style.height = "67vh";
        } else if (larguraJanela < 768) {
            document.querySelector('.ConteudoChat').style.maxHeight = "70vh";
            document.querySelector('.ConteudoChat').style.height = "70vh";
        } else if (larguraJanela < 874) {
            document.querySelector('.ConteudoChat').style.maxHeight = "74vh";
            document.querySelector('.ConteudoChat').style.height = "74vh";
        } else {
            document.querySelector('.ConteudoChat').style.maxHeight = "77vh";
            document.querySelector('.ConteudoChat').style.height = "77vh";
        }
    }
}

export const FetchChatsData = async () => {
    // try {
    //     const response = await fetch(urlBase + "/v1/Chat/chats/Get/BuscarTodosOsChatsPorLogId/" + UsuarioLogadoId);
    //     const data = await response.json();     
    //     return data;
    // } catch (error) {
    //     console.error('Error fetching contacts:', error);
    // }

    /*const connection = new signalR.HubConnectionBuilder()
        .withUrl(`http://localhost:5058/api/chatHub?logId=${UsuarioLogadoId}`)
        .build();

    connection.on("ReceiveChats", (element) => {
        console.log(element)
        // const index = data.findIndex(item => item.codigo === element.codigo);

        // if (index !== -1) {
        //     // Substitui o objeto existente pelo novo
        //     data[index] = element;
        // } else {
        //     // Adiciona o novo elemento se não houver um com o mesmo código
        //     data.push(element);
        // }

        return element
    });

    connection.start().catch(err => console.error(err.toString()));
*/
};

// Função para obter a data da última mensagem de um objeto de chat
function getUltimaDataMensagem(chat) {
    if (chat.mensagens && chat.mensagens.length > 0) {
        // Pega a última mensagem baseada na data mais recente
        const ultimaMensagem = chat.mensagens.reduce((ultima, mensagemAtual) => {
            return new Date(mensagemAtual.data) > new Date(ultima.data) ? mensagemAtual : ultima;
        });
        return new Date(ultimaMensagem.data);
    }
    return null;
}

// Função para ordenar a lista de chats pela data da última mensagem
function ordenarChatsPorUltimaMensagem(chats) {
    console.log("Chat aqui")
    console.log(chats)
    return chats.sort((chatA, chatB) => {
        const ultimaDataA = getUltimaDataMensagem(chatA);
        const ultimaDataB = getUltimaDataMensagem(chatB);

        // Ordenar do mais recente para o mais antigo
        return ultimaDataB - ultimaDataA;
    });
}


export const FiltrarDataPorStatus = (status, data, departamentoDoAtendente) => {
    console.log("Caiu na function de filtro de por status")
    console.log(departamentoDoAtendente)
    console.log("data que chegou aqui em filtrar data por status")
    console.log(data)
    //deixar isso aqui apenas na fase de teste para deixar por padrao o departamento de suporte
    if (!departamentoDoAtendente?.codigo) {
        departamentoDoAtendente.departamento.codigo = 1
    }
    const statusNormalizado = status.trim().toLowerCase();
    if (statusNormalizado == "ativo") {
        return ordenarChatsPorUltimaMensagem(data.filter(x =>
            x?.atendimento?.atendente !== null &&
            x?.atendimento?.estadoAtendimento == 2 &&
            x?.atendimento?.departamento?.codigo == departamentoDoAtendente?.departamento?.codigo &&
            x?.atendimento?.atendente?.codigo == departamentoDoAtendente?.codigo
        ));
        return []
    }

    if (statusNormalizado == "fila") {
        return ordenarChatsPorUltimaMensagem(data.filter(x =>
            // x?.atendente == null &&
            x?.atendimento?.estadoAtendimento != 2 &&
            x?.atendimento?.estadoAtendimento != 1
        ));
        return []
    }

    if (statusNormalizado == "esperando") {
        return ordenarChatsPorUltimaMensagem(data.filter(x =>
            x?.atendimento?.atendente == null &&
            x?.atendimento?.estadoAtendimento == 2 &&
            x?.atendimento?.departamento?.codigo == departamentoDoAtendente?.departamento?.codigo
        ));
        return []
    }
};

export function entrarChat() {
    var larguraJanela = window.innerWidth;

    if (larguraJanela < 992) {
        document.getElementById('TituloNavbar').style.setProperty('display', 'none', 'important');
        document.getElementById('setarVoltar').style.setProperty('display', 'flex', 'important');
        document.querySelector('#NavbarSearch').style.setProperty('display', 'none', 'important');
        document.getElementById('sidebar').style.setProperty('display', 'none', 'important');
        document.getElementById('containerMensagens').style.setProperty('display', 'none', 'important');
        document.getElementById('containerChats').style.setProperty('display', 'flex', 'important');
    }

    if (larguraJanela > 992) {
        //se quiser fazer uma alteração no código ao entrar o chat em telas grandes, adicione aqui
    }
}

export function voltarChat() {
    document.getElementById('TituloNavbar').style.setProperty('display', 'flex', 'important');
    document.getElementById('setarVoltar').style.setProperty('display', 'none', 'important');
    document.querySelector('#NavbarSearch').style.setProperty('display', 'block', 'important');
    document.getElementById('sidebar').style.setProperty('display', 'flex', 'important');
    document.getElementById('containerMensagens').style.setProperty('display', 'block', 'important');
    document.getElementById('containerChats').style.setProperty('display', 'none', 'important');
}

