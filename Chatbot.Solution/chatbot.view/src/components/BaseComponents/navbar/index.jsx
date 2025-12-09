import './style.css'
import A from '../a'
import image from '../../../img/file.jpg'
import { voltarChat } from '../../../Repository/AtendenteRepository'
export default function Navbar(props) {

    const chatSelecionadoIndice = props.ChatDates.findIndex(chat => chat.codigo == props.chatActiveStatus.Codigo);
    const chatSelecionado = props.ChatDates[chatSelecionadoIndice]
    console.log("Dados aqui")
    console.log(chatSelecionado)
    return (
        // navbar

        //colocar os ifs para atender condições de renderização aqui depois
        <div className="col-12 p-0 bg-light" id="navbar">
            <div className="row justify-content-between">

                {/* Header aqui fica o contato e a navbar */}
                <div id='TituloNavbar' className="d-flex align-items-center bg-light BordarParaSerAplicadaEmTelasMaiores border-bottom border-1 border-dark" style={{ maxWidth: "28.77rem" }}>

                    <div className="d-flex gap-3 ms-4">

                        <h6 className="d-block" style={{ color: "#263a6d", fontWeight: "bold", fontSize: "2rem" }}>
                            Chatbot
                        </h6>

                    </div>

                    {/* <button className="btn btnAdicionar">
                            <p className="buttonplus">+</p>
                        </button> */}

                    <div id='NavbarSearch' className='d-lg-none'>
                        <A className={"col d-flex d-lg-none justify-content-end align-items-center"} bootsrapAction={"offcanvas"} href={"#offcanvasExample"} icon={
                            <svg xmlns="http://www.w3.org/2000/svg" style={{ color: "#182c5f", display: "flex" }} width="36" height="36"
                                fill="currentColor" className="bi bi-search" viewBox="0 0 16 16">
                                <path
                                    d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
                            </svg>
                        } />
                    </div>
                </div>

                {/* Contato, Ver Como Vai ficar isso aqui depois, acho massa fazer algo como whastapp que so aparece os chat quando clicka aqui */}
                {props.chatActiveStatus.chatActiveStatus == "Desativado" ? (
                    <div className="col d-none d-lg-block p-3" style={{ minHeight: "5.7rem", backgroundColor: "rgb(235, 239, 249)" }} id='setarVoltar'>

                    </div>
                ) : (
                    <div className="col d-none d-lg-block p-3 border-bottom border-1 border-dark" style={{ minHeight: "5.7rem" }} id='setarVoltar'>

                        <div className="p-3 d-lg-none" onClick={voltarChat}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                                fill="currentColor" className="bi bi-arrow-left" viewBox="0 0 16 16">
                                <path fillRule="evenodd"
                                    d="M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8" />
                            </svg>
                        </div>

                        <div className="d-flex gap-3">
                            <div className="d-flex justify-content-center align-items-center gap-3">
                                <img src={image} className="leadImage rounded-circle" />
                                <strong style={{ fontFamily: "Arial, Helvetica, sans-serif", height: "20px" }}>{chatSelecionado.contato.nome}</strong>
                            </div>
                        </div>

                    </div>
                )}


            </div>
        </div>
    )
}