import { useEffect, useRef } from 'react';
import ConversaCard from '../conversaCard'
import MensagemSend from '../MensagenSend'
import '../conteudoChat/style.css'
export default function conteudoChat(props) {
    const chatSelecionadoIndice = props.ChatDates.findIndex(chat => chat.codigo == props.chatActiveStatus.Codigo);
    const chatSelecionado = props.ChatDates[chatSelecionadoIndice]
     // Referência à div do chat para controle de scroll
     const chatEndRef = useRef(null);

     // Função para fazer scroll até o final da lista de mensagens
     const scrollToBottom = () => {
         chatEndRef.current?.scrollIntoView({ behavior: "smooth" });
     };
 
     // Executa o scroll sempre que o chatSelecionado mudar (ou seja, sempre que uma nova mensagem for adicionada)
     useEffect(() => {
         scrollToBottom();
     }, [chatSelecionado?.mensagens]);
 
    return (
        <>
            {props.chatActiveStatus.chatActiveStatus == "Desativado" ? (
                <div className='d-flex justify-content-center align-items-center w-100 h-100'>
                    <strong className='h5' style={{ color: "rgb(38, 58, 109)" }}>
                        Entre em algum Chat, As suas conversas irão aparecer aqui!
                    </strong>
                </div>
            ) : (
                <>
                    <div className="ConteudoChat d-flex flex-column">
                        {chatSelecionado !== null ? (
                            chatSelecionado.mensagens.map((x) => (
                                <ConversaCard
                                    key={x.codigo}
                                    IsRecaive={x.contato != null}
                                    descricao={x.descricao}
                                />
                            ))
                        ) : null}
                        {/* Esse elemento oculto serve para garantir que o scroll chegue até o final */}
                        <div ref={chatEndRef}></div>
                    </div>
                    <MensagemSend connectionDateChild={props.connectionDateChild} chatDates={props.chatActiveStatus} />
                </>
            )}
        </>

    )
}