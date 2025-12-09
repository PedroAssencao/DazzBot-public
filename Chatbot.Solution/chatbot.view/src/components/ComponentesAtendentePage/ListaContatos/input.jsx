import React, { useEffect, useState } from 'react';
import ChatCard from "../ChatCard";
import SmallLoadScreen from '../../BaseComponents/smallLoading';
import { entrarChat } from '../../../Repository/AtendenteRepository';
import { UsuarioLogado, urlBase } from '../../../appsettings';

export default function ListaContato(props) {
    var AtendenteLogado = null;
    const [mensagemVazia, setMensagemVazia] = useState(false);
    const [IsLoading, SetLoading] = useState(true)
    const [date, setDate] = useState([])

    useEffect(() => {
        SetLoading(true)
        const teste = [...props.date]
        setDate(teste)
        if (date.length < 0) {
            SetLoading(false);
            setMensagemVazia(true);
        } else {
            SetLoading(false);
            setMensagemVazia(false);
        }
    }, [props.date]);

    useEffect(() => {
        UsuarioLogado().then(result => {
            AtendenteLogado = result.usuarioLogadoId
        });
    }, []);

    const setMessagesRead = async (models) => {
        let qtdMensagensNaoLidas = 0
        const mensagens = models.mensagens || [];

        if (mensagens.length > 0) {
            mensagens.forEach((element) => {
                if (element.statusDaMensagen != 3 && element.contato) {
                    qtdMensagensNaoLidas++
                }
            });
        }

        if (qtdMensagensNaoLidas > 0) {
            try {
                const response = await fetch(`${urlBase}/v1/Mensagem/mensagens/MarcarMensagensComoLida`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(models)
                });

                if (!response.ok) {
                    throw new Error('Erro ao enviar os dados');
                }

                const obj = await response.json();

                props.SetChatDatesFromChild(prevChatsDate => {
                    console.log("Esses são os dados antes")
                    const index = prevChatsDate.findIndex(chat => chat.codigo === obj.codigo);

                    if (index !== -1) {
                        const updatedChats = [...prevChatsDate];
                        updatedChats[index] = obj;
                        return updatedChats;
                    } else {
                        return [...prevChatsDate, obj];
                    }
                });
            } catch (error) {
                console.error('Erro:', error);
            }
        }
    };

    const teste = props.chatActiveStatus
    return (
        <div className="ListaContatos overflow-y-auto overflow-x-hidden" style={{ maxHeight: "63vh" }}>
            {/* Percorrer a lista de chats para exibir dependendo do status */}
            {date.map(x => (
                <ChatCard
                    key={x.codigo}
                    ChatDate={x}
                    chatActiveStatus={teste}
                    setMessagesRead={setMessagesRead}
                    onClick={() => {
                        entrarChat();
                        setMessagesRead(x)
                        props.setChatActive({
                            Codigo: x.codigo,
                            AtendenteLogado: AtendenteLogado,
                            chatActiveStatus: "Ativado",
                        });

                        const offcanvasElement = document.getElementById('offcanvasExample');
                        const offcanvasInstance = bootstrap.Offcanvas.getInstance(offcanvasElement);
                        if (offcanvasElement.classList.contains('show') && offcanvasInstance) {
                            offcanvasInstance.hide();
                        }
                    }}
                    className={"mt-4 justify-content-center align-items-center row mx-auto p-2 unactiveChat"}
                />
            ))}
            {/* Se Nenhum Chat For Reenderizado exibir a mensagem de error */}
            {mensagemVazia && (
                <div className='d-flex justify-content-center align-items-center'>
                    <strong className='h4 mt-5 text-center' style={{ color: "rgb(38, 58, 109)" }}>
                        Nenhum contato para o estado selecionado foi encontrado.
                    </strong>
                </div>
            )}
            {/* Deixar um loading se ainda não tiver reenderizado toda a lista de chats */}
            {IsLoading && (
                <SmallLoadScreen />
            )}
            {/* Apenas para cunho de espaçamento */}
            <div style={{ minHeight: "10rem" }}></div>
        </div>
    );
}
