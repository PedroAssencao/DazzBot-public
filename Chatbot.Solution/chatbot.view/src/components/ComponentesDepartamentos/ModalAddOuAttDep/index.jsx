import Input from '../../BaseComponents/input';
import Select from '../../BaseComponents/select';
import { useState, useEffect } from "react";
import './style.css';

export default function ModalAddOuAttDep(props) {
    const [DescricaoDepartamento, setDescricaoDepartamento] = useState(props?.departamento?.descricao || "");
    // const selecionarDepartamentoAtivoNoSelect = (param) => {
    //     const selectElement = document.querySelector("#SelectDepartamentoUsuarioModal");
    //     if (selectElement && param) {
    //         selectElement.value = param;
    //     } else {
    //         selectElement.value = 1;
    //     }
    // };

    // useEffect(() => {
    //     selecionarDepartamentoAtivoNoSelect(DepartamentoUsuario);
    // }, [DepartamentoUsuario]);

    useEffect(() => {
        setDescricaoDepartamento(props?.departamento?.descricao || "");
    }, [props.departamento]);

    return (
        <div className="modal fade" id="staticBackdrop" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered modal-mg">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="staticBackdropLabel">{props.title}</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body modalUsuarioBody">
                        <strong className='w-100 d-flex justify-content-start h6'>
                            {props.descricao}
                        </strong>
                        <div className='row gap-2'>
                     <p>Departamento</p>
                                <Input
                                    value={DescricaoDepartamento}
                                    onChange={(e) => setDescricaoDepartamento(e.target.value)}
                                    id={"DescricaoInputDepartamento"}
                                    placeholder={"Descrição Departamento"}
                            />
                         
                        </div>
                    </div>
                    <div className="modal-footer">
                        {props.isButtondelete ? (
                            <button data-bs-toggle="modal" data-bs-target="#ModalExcluirDepartamento" type="button" className="btn btn-danger">Deletar</button>
                        ) : null}
                        <button type="button" onClick={props.onClick} className="btn btn-primary btnHoverUsuarios">Salvar</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
