import ConteudoChat from "../conteudoChat"
import '../ContainerChats/style.css'
export default function ContainerChats(props) {
    return (
        <div className="col p-0 d-lg-flex containerChat" id="containerChats"
            style={{ display: "none", flexDirection: "column", backgroundColor: "#EBEFF9" }}>
            <ConteudoChat connectionDateChild={props.connectionDateChild} ChatDates={props.ChatDates} chatActiveStatus={props.chatActiveStatus}/>
        </div>
    )
}