import Input from '../../BaseComponents/input';
import Select from '../../BaseComponents/select';
import { useState, useEffect } from "react";
import './style.css';

export default function ModalAddOuAttUsuario(props) {

    console.log("Props Atendente aqui")
    console.log(props.Usuario.estadoAtendente)

    const [nomeUsuario, setNomeUsuario] = useState(props?.Usuario?.nome || "");
    const [emailUsuario, setEmailUsuario] = useState(props?.Usuario?.email || "");
    const [senhaUsuario, setSenhaUsuario] = useState(props?.Usuario?.senha || "");
    const [EstadoAtendente, setEstadoAtendente] = useState(props?.Usuario?.estadoAtendente || "");
    const DepartamentoUsuario = props?.Usuario?.departamento?.codigo;

    const selecionarDepartamentoAtivoNoSelect = (param) => {
        if (props.tipoUsuario == "Atendente") {
            const selectElement = document.querySelector("#SelectDepartamentoUsuarioModal");
            if (selectElement && param) {
                selectElement.value = param;
            } else {
                selectElement.value = 1;
            }
        }
    };

    useEffect(() => {
        selecionarDepartamentoAtivoNoSelect(DepartamentoUsuario);
    }, [DepartamentoUsuario]);

    useEffect(() => {

        if (props.tipoUsuario != "Atendente") {
            setNomeUsuario(props?.Usuario?.usuario || "");
            setEmailUsuario(props?.Usuario?.email || "");
            setSenhaUsuario(props?.Usuario?.senha || "");
            return
        }

        setNomeUsuario(props?.Usuario?.nome || "");
        setEmailUsuario(props?.Usuario?.email || "");
        setSenhaUsuario(props?.Usuario?.senha || "");
        setEstadoAtendente(props?.Usuario?.estadoAtendente == true ? true : false || props?.Usuario?.estadoAtendente == "true" ? true : false || false)


    }, [props.Usuario]);

    console.log("Estado atendente aqui")
    console.log(EstadoAtendente)

    return (
        <div className="modal fade" id="staticBackdrop" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered modal-lg">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="staticBackdropLabel">{props.title}</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body modalUsuarioBody">
                        <strong className='w-100 d-flex justify-content-start flex-column h6'>
                            {props.descricao}
                            {props.tipoUsuario == "Atendente" &&
                                <div className='d-flex justify-content-start mt-2'>
                                    <div class="form-check d-flex justify-content-center align-items-center gap-2">
                                        <input className="form-check-input" type="checkbox" value="" id="EstadoAtendentePerfil" onChange={(e) => setEstadoAtendente(e.target.checked == "true" ? true : false || e.target.checked == true ? true : false)} checked={EstadoAtendente} />
                                        <label style={{ marginTop: "5px" }} className="form-check-label" for="flexCheckChecked">
                                            Estado Atendente
                                        </label>
                                    </div>
                                </div>
                            }

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
                            {props.tipoUsuario == "Atendente" &&
                                <div className='widthRowUsuarioModal'>
                                    <Select
                                        onChange={props.SetDepartamentoAtivoId}
                                        id={"SelectDepartamentoUsuarioModal"}
                                        placeholder={"Departamentos"}
                                        optionsList={props.optionsList}
                                    />
                                </div>
                            }

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
                        <button type="button" onClick={props.onClick} data-bs-dismiss="modal" aria-label="Close" className="btn btn-primary btnHoverUsuarios">Salvar</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
