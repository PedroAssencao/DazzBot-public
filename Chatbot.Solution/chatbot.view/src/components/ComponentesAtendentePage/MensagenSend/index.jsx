import '../MensagenSend/style.css'
import { useEffect, useState } from 'react';
import { UsuarioLogado } from '../../../appsettings';
export default function MensagemSend(props) {
    const [AtendeteLogado, setAtendeteLogado] = useState({});
    useEffect(() => {
        UsuarioLogado().then(result => {
            setAtendeteLogado(result)
        });
    }, []);

    const sendMessage = (AtenId, ChatId, Message) => {
        console.log(props.chatDates)
        console.log("Codigo Atedente aqui " + AtenId)
        console.log("Codigo Chat aqui " + ChatId)
        console.log("message aqui " + Message)
        console.log("Oque esta chegando aqui no send message")

        if (Message.trim() !== "") {
            console.log("Atendente Logado Aqui")
            console.log(AtendeteLogado)
            props.connectionDateChild.invoke("SendMessage", parseInt(AtendeteLogado.usuarioLogadoId, 10), ChatId, Message)
                .catch(err => console.error(err.toString()));
        }
    }

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            console.log("Chat dates aqui")
            console.log(props.chatDates)
            sendMessage(props.chatDates.AtendenteLogado, props.chatDates.Codigo, event.target.value);
            event.target.value = '';
        }
    }


    return (
        <div className="flex-grow-1 d-flex align-items-end">

            <div className="d-flex w-100 gap-3 p-2 mensagens mt-3 justify-content-center align-items-center">

                <div className="inputMensagen w-100" style={{ maxWidth: "90%" }}>
                    <input
                        className="form-control p-3 rounded-4"
                        id='InputMessageChat'
                        style={{ backgroundColor: "#c7cfe4" }}
                        placeholder="Mensagem"
                        onKeyDown={handleKeyDown}  // Evento de tecla pressionada
                    />
                </div>

                <a
                    className="btn p-3 rounded-4"
                    onClick={() => {
                        sendMessage(props.chatDates.AtendenteLogado, props.chatDates.Codigo, document.querySelector("#InputMessageChat").value)
                    }}
                    style={{ backgroundColor: "#6276A8" }}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor"
                        className="bi bi-send" viewBox="0 0 16 16">
                        <path
                            d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576zm6.787-8.201L1.591 6.602l4.339 2.76z" />
                    </svg>
                </a>

            </div>
        </div>
    )
}