import Input from '../../BaseComponents/input';
import Select from '../../BaseComponents/select';
import { useState, useEffect } from "react";
import './style.css';

export default function ModalAddOuAttUsuario(props) {
    const [nomeUsuario, setNomeUsuario] = useState(props?.Usuario?.nome || "");
    const [emailUsuario, setEmailUsuario] = useState(props?.Usuario?.email || "");
    const [senhaUsuario, setSenhaUsuario] = useState(props?.Usuario?.senha || "");
    const DepartamentoUsuario = props?.Usuario?.departamento?.codigo;

    const selecionarDepartamentoAtivoNoSelect = (param) => {
        const selectElement = document.querySelector("#SelectDepartamentoUsuarioModal");
        if (selectElement && param) {
            selectElement.value = param;
        } else {
            selectElement.value = 1;
        }
    };

    function removeModal() {
        console.log('goze');
        const modalElement = document.getElementById('staticBackdrop');
        modalElement.hide();
        
        // Atualiza o atributo aria-hidden
        modalElement.setAttribute('aria-hidden', 'true');
    }

    useEffect(() => {
        selecionarDepartamentoAtivoNoSelect(DepartamentoUsuario);
    }, [DepartamentoUsuario]);

    useEffect(() => {
        setNomeUsuario(props?.Usuario?.nome || "");
        setEmailUsuario(props?.Usuario?.email || "");
        setSenhaUsuario(props?.Usuario?.senha || "");
    }, [props.Usuario]);

    return (
        <div className="modal fade" id="staticBackdrop" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true" >
            <div className="modal-dialog modal-dialog-centered modal-lg">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="staticBackdropLabel">{props.title}</h1>
                        <button onClick={removeModal} type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body modalUsuarioBody">
                        <strong className='w-100 d-flex justify-content-start h6'>
                            {props.descricao}
                        </strong>
                        <div className='row gap-2'>
                            <div className='widthRowUsuarioModal'>
                                <Input
                                    value={nomeUsuario}
                                    onChange={(e) => setNomeUsuario(e.target.value)}
                                    id={"NomeUsuarioInputUsuarios"}
                                    placeholder={"Nome Usuario"}
                                />
                            </div>
                            <div className='widthRowUsuarioModal'>
                                <Select
                                    onChange={props.SetDepartamentoAtivoId}
                                    id={"SelectDepartamentoUsuarioModal"}
                                    placeholder={"Departamentos"}
                                    optionsList={props.optionsList}
                                />
                            </div>
                            <div className='widthRowUsuarioModal'>
                                <Input
                                    value={senhaUsuario}
                                    onChange={(e) => setSenhaUsuario(e.target.value)}
                                    id={"SenhaInputUsuarios"}
                                    placeholder={"Senha"}
                                />
                            </div>
                            <div className='widthRowUsuarioModal'>
                                <Input
                                    value={emailUsuario}
                                    onChange={(e) => setEmailUsuario(e.target.value)}
                                    id={"EmailInputUsuarios"}
                                    placeholder={"Email"}
                                />
                            </div>
                        </div>
                    </div>
                    <div className="modal-footer">
                        {props.isButtondelete ? (
                            <button data-bs-toggle="modal" data-bs-target="#ModalExcluirAtendenteTelaUsuario" type="button" className="btn btn-danger">Deletar</button>
                        ) : null}
                        <button type="button" onClick={props.onClick} className="btn btn-primary btnHoverUsuarios">Salvar</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
