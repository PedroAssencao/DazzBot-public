import SearchBar from "../../BaseComponents/searchBar"
import { useState } from 'react';
import StatusAtendimento from "../StatusAtendimento"
import ListaContato from "../ListaContatos/input"
import '../ContainerMensagens/style.css'
export default function ContainerMensagen(props) {
    // console.log("Oque esta chegando no container mensagens e isso aqui ")
    // console.log(props.ContatosDate)
    return (
        <div className="col bg-light containerMensagens border-end border-1 border-dark" id="containerMensagens">
            <SearchBar searchbarFunction={props.searchbarFunction} className={"d-none d-lg-flex justify-content-center ms-4 gap-3 align-items-center mt-2"}/>
            <StatusAtendimento SetStatusActive={props.StatusFuncion} />
            <ListaContato chatActiveStatus={props.chatActiveStatus} SetChatDatesFromChild={props.SetChatDatesFromChild} setChatActive={props.setChatActive} date={props.ContatosDate} />
        </div>
    )
}