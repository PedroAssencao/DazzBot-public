import SearchBar from "../../BaseComponents/searchBar"
import ListaContato from "../ListaContatos/input"
export default function offCanvasBuscaMobile(props) {
    return (
        // Off Canvas para Busca em mobile mode
        <div className="offcanvas offcanvas-end w-100" tabIndex="-1" id="offcanvasExample"
            aria-labelledby="offcanvasExampleLabel">
            <div className="offcanvas-header">
                <h1 className="offcanvas-title" id="offcanvasExampleLabel">Buscar</h1>
                <button type="button" className="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>

            <div className="offcanvas-body">
                <SearchBar searchbarFunction={props.searchbarFunction} className={"d-flex justify-content-center ms-4 gap-3 align-items-center mt-2 mx-auto"}/>                
                <ListaContato chatActiveStatus={props.chatActiveStatus} SetChatDatesFromChild={props.SetChatDatesFromChild} setChatActive={props.setChatActive} date={props.ContatosDate} />
            </div>
        </div>
    )
}