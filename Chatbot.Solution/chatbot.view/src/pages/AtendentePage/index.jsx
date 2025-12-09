import { useEffect, useState, useRef } from 'react';
import LoadScreen from '../../components/BaseComponents/loadingScreen';
import Navbar from '../../components/BaseComponents/navbar';
import ContainerMensagen from '../../components/ComponentesAtendentePage/ContainerMensagens';
import ContainerChats from '../../components/ComponentesAtendentePage/ContainerChats';
import { VerficarAltura, FetchChatsData, FiltrarDataPorStatus } from '../../Repository/AtendenteRepository/index'
import OffCanvasBuscaMobile from '../../components/ComponentesAtendentePage/offCanvasBuscaParaMobile';
import { urlBase, UsuarioLogado } from '../../appsettings'
export default function Atendente() {
    const [UsuarioLogadoId, setUsuarioLogadoId] = useState(null);
    const [AtendenteLogadoObj, setAtendenteLogadoObj] = useState(null);
    const [isReady, setIsReady] = useState(false);
    const [ChatsDate, setChatsDate] = useState([]);
    const [IsDataLoad, SetLoadDate] = useState(false);
    const [StatusActive, setStatusActive] = useState("Ativo");
    const [IsChatActive, setChatActive] = useState({ chatActiveStatus: "Desativado" });
    const [connectionDateChild, setconnectionDateChild] = useState()
    const [ChatDatesFiltered, setChatDatesFiltered] = useState([])
    const isChatActiveRef = useRef(IsChatActive);
    let prevMsg = ""
    useEffect(() => {
        isChatActiveRef.current = IsChatActive;
    }, [IsChatActive]);

    // function showNotification(title, options) {
    //     if (options.body == prevMsg) {
    //         console.log("Caiu dentro do if de retorno")
    //         return
    //     } else {
    //         prevMsg = options.body
    //     }
    //     console.log("Paramtro passados para show notification")
    //     console.log(title)
    //     console.log(options)
    //     console.log("Chamou function show Notification")
    //     // Verifica se a API de Notificações é suportada
    //     if (!("Notification" in window)) {
    //         console.log("Este navegador não suporta notificações.");
    //         return;
    //     }

    //     // Solicita permissão para enviar notificações
    //     Notification.requestPermission().then(permission => {
    //         if (permission === "granted") {
    //             // Cria e exibe a notificação
    //             console.log("Criação de notification")
    //             const notification = new Notification(title, options);

    //             // Adiciona um evento para quando a notificação é clicada se necessario executar uma ação posteriormente
    //             // notification.onclick = () => {
    //             //     console.log("Notificação clicada!");
    //             // };
    //         } else if (permission === "denied") {
    //             console.log("Permissão de notificações negada.");
    //         }
    //     });
    // }

    useEffect(() => {
        const verificarUsuario = async () => {
            const result = await UsuarioLogado();
            if (result.usuarioLogadoId == null) {
                // alert("Usuario precisa estar logado")
                return location.replace(location.origin + "/login");
            }

            if (result.tipoUsuario !== "Atendente") {
                alert("Usuario não tem permissão para acessar a tela de atendimento")
                return location.replace(location.origin + "/Home");
            }

            const data = await fetch(urlBase + '/v1/Atendente/Atendente/' + result.usuarioLogadoId).then(res => res.json());

            if (data.estadoAtendente == false) {
                alert("Atendente esta offline, fique online antes de começar os atendimentos!")
                return location.replace(location.origin + "/Perfil");
            }

            console.log(result)
            setUsuarioLogadoId(result.idUsuarioCliente);
            setAtendenteLogadoObj(data)
            setIsReady(true);
        };

        verificarUsuario(); // Chama a função ao carregar

    }, []); // Apenas no carregamento inicial

    useEffect(() => {

        if (!isReady || !UsuarioLogadoId) return;
        window.addEventListener('resize', VerficarAltura);

        const fetchdata = (firstConnection) => {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl(`${urlBase}/chatHub?logId=${UsuarioLogadoId}`)
                .build();

            setconnectionDateChild(connection);

            connection.on("ReceiveChats", (element) => {

                const mensagens = element.mensagens || [];
                const IsLeadMessage = mensagens.length > 0 ? mensagens[mensagens.length - 1].contato : false;
                console.log("Is chat active reference aqui")
                console.log(isChatActiveRef)
                console.log("First Concetion")
                console.log(firstConnection)
                if (firstConnection == false && IsLeadMessage && isChatActiveRef.current.Codigo !== element.codigo &&
                    element?.atendimento?.estadoAtendimento == 2 && element?.atendimento?.atendente
                    && element?.atendimento?.atendente.estadoAtendente) {
                    console.log("Esta entrando dentro do if")
                    const ultimaMensagem = mensagens.length > 0
                        ? mensagens[mensagens.length - 1].descricao
                        : 'Aguardando mensagem';

                    // if (document.visibilityState == "hidden" || isChatActiveRef.current.Codigo != element.codigo) {
                    // showNotification("Nova Mensagem", {
                    //     body: ultimaMensagem,
                    // });
                    // }

                }

                setChatsDate(prevChatsDate => {
                    const index = prevChatsDate.findIndex(chat => chat.codigo === element.codigo);

                    if (index !== -1) {
                        const updatedChats = [...prevChatsDate];
                        updatedChats[index] = element;
                        return updatedChats;
                    } else {
                        return [...prevChatsDate, element];
                    }
                });
            });

            connection.on("CompleteLoading", () => {
                SetLoadDate(true);
                firstConnection = false;
            });

            connection.start().catch(err => console.error(err.toString()));
        };

        fetchdata(true);
        VerficarAltura();
    }, [isReady, UsuarioLogadoId]);

    const handleDataFromChild = (data) => {
        setStatusActive(data);
    };

    const handleChatInFromChild = (data) => {
        setChatActive(data)
    }

    const SetChatDatesFromChild = (data) => {
        setChatsDate(data)
    }

    const BuscarContato = (query) => {
        if (query == "" || query == null || query == '') {
            return setChatDatesFiltered([])
        }
        const resultado = ChatsDate.filter((chat) => {
            return chat.contato.nome.toLowerCase().includes(query.toLowerCase()) ||
                chat.contato.codigoWhatsapp.toLowerCase().includes(query.toLowerCase());
        });
        setChatDatesFiltered(resultado)
    }

    document.querySelector("#bodyFromPageAll").style.overflowX = "hidden"

    return (
        <div className="col bg-warning containerPai p-0 container-fluid d-flex flex-column">
            {IsDataLoad ? <>
                <Navbar chatActiveStatus={IsChatActive} ChatDates={ChatsDate} />
                <div className='flex-grow-1 d-flex bg-dark p-0'>
                    <ContainerMensagen searchbarFunction={BuscarContato} SetChatDatesFromChild={SetChatDatesFromChild} chatActiveStatus={IsChatActive} StatusActive={StatusActive} setChatActive={handleChatInFromChild} StatusFuncion={handleDataFromChild} ContatosDate={ChatDatesFiltered.length > 0 ? ChatDatesFiltered : FiltrarDataPorStatus(StatusActive, ChatsDate, AtendenteLogadoObj)} />
                    <ContainerChats connectionDateChild={connectionDateChild} ChatDates={ChatsDate} chatActiveStatus={IsChatActive} />
                </div>
                <OffCanvasBuscaMobile searchbarFunction={BuscarContato} SetChatDatesFromChild={SetChatDatesFromChild} chatActiveStatus={IsChatActive} StatusActive={StatusActive} setChatActive={handleChatInFromChild} StatusFuncion={handleDataFromChild} ContatosDate={ChatDatesFiltered.length > 0 ? ChatDatesFiltered : ChatsDate} />
            </> : <LoadScreen />}
        </div>
    );
}
