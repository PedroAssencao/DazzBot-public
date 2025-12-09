import './style.css';
import { verificarRegrasSalvarOptionAtt, SelectTipoHandEventAtualizacao } from '../../../Repository/FluxoBoxRepository'
import { urlBase, UsuarioLogado } from "../../../appsettings";
import { useEffect, useState } from "react";
import Select from '../../BaseComponents/select';
import ButtonBase from '../../BaseComponents/button';
import Input from '../../BaseComponents/input';
export default function ModalDeAtualizacaoFluxobot(props) {
    const [resultDep, setResultDep] = useState(null);
    const [ResultOption, setResultOption] = useState(null);
    const [ResultMenu, setResultMenu] = useState(null);
    const [DepartamentoAtivoId, setDepartamentoAtivoId] = useState(0);
    const [IdUsuarioLogado, setIdUsuarioLogado] = useState(0);

    const [tituloOpcao, settituloOpcao] = useState(props?.Dados?.tituloOpcao || "");
    const [descricaoopcao, setdescricaoopcao] = useState(props?.Dados?.descricaoOpcao || "");
    const [respostaopcao, setrespostaopcao] = useState(props?.Dados?.respostaopcao || "");
    const [finalizarAtendimento, setfinalizarAtendimento] = useState(props?.Dados?.finalizarAtendimento || "");
    const [tituloMenu, settituloMenu] = useState(props?.Dados?.tituloMenu || "");
    const [descricaomenu, setdescricaomenu] = useState(props?.Dados?.descricaoMenu || "");
    const [cabecalhomenu, setcabecalhomenu] = useState(props?.Dados?.cabecalhomenu || "");
    const [rodapemenu, setrodapemenu] = useState(props?.Dados?.rodapemenu || "");
    const [corpomenu, setcorpomenu] = useState(props?.Dados?.corpomenu || "");

    console.log("obj aqui")
    console.log(props?.Dados)

    const fetchData = async (param) => {
        let url = `${urlBase}/v1/Departamaneto/Departamento/BuscarTodosDepartamentoPorLogId?id=${param}`;

        try {
            const response = await fetch(url);

            if (!response.ok) {
                throw new Error('Erro na requisição');
            }

            const data = await response.json();
            var listTratada = []
            data.map(x => {
                listTratada.push({
                    id: x.codigo,
                    descricao: x.nomeDepartamento,
                    value: x.codigo
                })
            })
            setResultDep(listTratada);
            console.log("Aqui esta a lista tratada")
            console.log(listTratada)
            setDepartamentoAtivoId(listTratada[0].id)
        } catch (error) {
            console.log(error)
        }

        // if (localStorage.getItem("OptId") != null) {
        //     url = `${urlBase}/v1/Option/Option/${localStorage.getItem("OptId")}`;
        //     console.log("bateu aqui no fech option")
        //     console.log(localStorage.getItem("OptId"))
        //     try {
        //         const response = await fetch(url);

        //         if (!response.ok) {
        //             throw new Error('Erro na requisição');
        //         }

        //         const data = await response.json();
        //         setResultOption(data);
        //         document.querySelector("#DescricaoMenuInputRedirecionamentoAtt").value = data.descricao
        //         document.querySelector("#TituloOpcaoMenuInputRedirecionamentoAtt").value = data.titulo
        //         document.querySelector("#DescricaoMenuInputAtt").value = data.descricao
        //         document.querySelector("#TituloOpcaoMenuInputAtt").value = data.titulo
        //         document.querySelector("#DescricaoSimplesInputAtt").value = data.descricao
        //         document.querySelector("#TituloSimplesInputAtt").value = data.titulo
        //     } catch (error) {
        //         console.log(error)
        //     }
        // }


        // if (localStorage.getItem("MenId") != null) {
        //     console.log("bateu aqui no fech menu")
        //     url = `${urlBase}/v1/Menus/Menus/${localStorage.getItem("MenId")}`;
        //     console.log("bateu aqui no fech Menu")
        //     console.log(localStorage.getItem("MenId"))
        //     try {
        //         const response = await fetch(url);

        //         if (!response.ok) {
        //             throw new Error('Erro na requisição');
        //         }

        //         const data = await response.json();
        //         setResultMenu(data)
        //         document.querySelector("#rodapeMenuInputAtt").value = data.footer
        //         document.querySelector("#corpoMenuInputAtt").value = data.body
        //         document.querySelector("#cabecalhoMenuInputAtt").value = data.header
        //         document.querySelector("#TituloMenuInputAtt").value = data.titulo
        //     } catch (error) {
        //         console.log(error)
        //     }
        // }

    };

    console.log("teste reendirzação")

    const DepartamentoUsuario = 0;

    const selecionarDepartamentoAtivoNoSelect = (param) => {
        try {
            const selectElement = document.querySelector("#selectDepartamentoAtt");
            if (selectElement && param) {
                selectElement.value = param;
            } else {
                selectElement.value = 1;
            }
        } catch (error) {

        }
    };

    useEffect(() => {
        selecionarDepartamentoAtivoNoSelect(DepartamentoUsuario);
    }, [DepartamentoUsuario]);

    const teste = async (e) => {
        console.log("Clickado")
        await verificarRegrasSalvarOptionAtt(e)
        await props.inicialise()
    }


    useEffect(() => {
        UsuarioLogado().then(result => {
            if (result.usuarioLogadoId == null) {
                location.replace(location.origin + "/login");
            } else {
                fetchData(result.usuarioLogadoId);
                setIdUsuarioLogado(result.usuarioLogadoId)
            }

            const containerPai = document.querySelector("#modalAttFluxoBot");

            const inputs = containerPai.querySelectorAll("input");

            inputs.forEach(input => {
                input.addEventListener("focus", (e) => {
                    console.log(e)
                    if (e.target.classList.contains("errorParaCampos")) {
                        e.target.classList.remove("errorParaCampos")
                    }
                });
            });
        });
    }, []);

    useEffect(() => {
        settituloOpcao(props?.Dados?.tituloOpcao || "");
        setdescricaoopcao(props?.Dados?.descricaoOpcao || "");
        setrespostaopcao(props?.Dados?.respostaopcao || "");
        setfinalizarAtendimento(props?.Dados?.finalizarAtendimento || "");
        settituloMenu(props?.Dados?.tituloMenu || "");
        setdescricaomenu(props?.Dados?.descricaoMenu || "");
        setcabecalhomenu(props?.Dados?.cabecalhomenu || "");
        setrodapemenu(props?.Dados?.rodapemenu || "");
        setcorpomenu(props?.Dados?.corpomenu || "");
    }, [props.Dados]);

    return (
        <div className="modal fade" id="exampleModalAtualizacao" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered modal-xl">
                <div className="modal-content">
                    <div className="modal-body" id='modalAttFluxoBot'>
                        <div className="row mt-3 justify-content-between">
                            <div className="containerTipo col-6">
                                <label className="mb-2"><strong>Tipo</strong></label>
                                {/* Puxar Aqui Do Codigo Todos os Tipo Para Inserção */}
                                <select onChange={(e) => SelectTipoHandEventAtualizacao(e)} className="form-select" id="SelectTipoAtt">
                                    <option value="0" defaultValue={true}>Selecione um Tipo</option>
                                    <option value="6">Redirecionamento</option>
                                    <option value="1">Mensagem Simples</option>
                                    <option value="3">Mensagem de Multipla Escolhas</option>
                                    <option value="4">Mensagem Gerada por IA</option>
                                </select>
                            </div>

                            <div className="containerTipo col-6" style={{ display: "none" }} id="DepartamentoSelectAtt">
                                <label className="mb-2"><strong>Departamento</strong></label>
                                {/* Puxar Aqui Do Codigo Todos os Departamentos Para Inserção
                                            Lembrar que id vai ser o id do departamento selecionado em questao */}
                                {resultDep != null &&
                                    <Select
                                        // onChange={props.SetDepartamentoAtivoId}
                                        id={"selectDepartamentoAtt"}
                                        className={"DepartamentoModalAddFluxoBot form-select p-3"}
                                        placeholder={"Departamentos"}
                                        optionsList={resultDep}
                                    />
                                }
                                {/* <select id="selectDepartamento" className="form-select">
                                    <option defaultValue={true}>Selecione um Departamento</option>
                                    <option id="1" value="1">Suporte</option>
                                    <option id="2" value="2">Financeiro</option>
                                    <option id="3" value="3">Técnico</option>
                                </select> */}
                            </div>

                        </div>

                        {/* Menu Simples para opcao de Redirecionamento Departamento */}
                        <div className="row mt-4" style={{ display: "none" }} id="RedirecionamentoInputsSectionsAtt">
                            <div className="col-6 mb-3">
                                <label className="mb-2"><strong>Titulo da Opção</strong></label>
                                {/* <input maxLength={24} id="TituloOpcaoMenuInputRedirecionamentoAtt" placeholder="Obrigatorio"
                                    className="form-control" /> */}
                                <Input
                                    lenght={"24"}
                                    value={tituloOpcao}
                                    onChange={(e) => settituloOpcao(e.target.value)}
                                    id={"TituloOpcaoMenuInputRedirecionamentoAtt"}
                                    placeholder={"Obrigatorio"}
                                />
                            </div>
                            <div className="col-6 mb-3">
                                <label className="mb-2"><strong>Descrição</strong></label>
                                {/* <input id="DescricaoMenuInputRedirecionamentoAtt" placeholder="Opcional"
                                    className="form-control" /> */}
                                <Input
                                    value={descricaoopcao}
                                    onChange={(e) => setdescricaoopcao(e.target.value)}
                                    id={"DescricaoMenuInputRedirecionamentoAtt"}
                                    placeholder={"Opcional"}
                                />
                            </div>
                        </div>

                        <div className="gap-3 justify-content-between mt-4" style={{ display: "none" }}
                            id="FinalizarAtendimentoSelectAtt">
                            <div className="form-check">
                                <Input
                                    value={finalizarAtendimento}
                                    checked={finalizarAtendimento}
                                    type={"checkbox"}
                                    className={"form-check-input"}
                                    onChange={(e) => setfinalizarAtendimento(e.target.checked)}
                                    id={"FinalizarCheckedAtt"}
                                // placeholder={"Opcional"}
                                />
                                {/* <input className="form-check-input" type="checkbox" value="" id="FinalizarCheckedAtt" /> */}
                                <label className="form-check-label" htmlFor="flexCheckDefault">
                                    Finalizar atendimento ápos a resposta?
                                </label>
                            </div>
                        </div>

                        {/* Caso A mensagem Seja do Tipo de Resposta Simples */}
                        <div className="gap-3 justify-content-between mt-4 flex-column" style={{ display: "none" }}
                            id="TextareaSelectAtt">
                            <div className="row">
                                <div className="col-6 mb-3">
                                    <label className="mb-2"><strong>Titulo</strong></label>
                                    <Input
                                        lenght={"24"}
                                        value={tituloOpcao}
                                        onChange={(e) => settituloOpcao(e.target.value)}
                                        id={"TituloSimplesInputAtt"}
                                        placeholder={"Obrigatorio"}
                                    />
                                    {/* <input id="TituloSimplesInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                                </div>
                                <div className="col-6 mb-3">
                                    <label className="mb-2"><strong>Descricao</strong></label>
                                    {/* <input id="DescricaoSimplesInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                                    <Input
                                        value={descricaoopcao}
                                        onChange={(e) => setdescricaoopcao(e.target.value)}
                                        id={"DescricaoSimplesInputAtt"}
                                        placeholder={"Obrigatorio"}
                                    />
                                </div>
                            </div>
                            <div className="w-100">
                                <label className="mb-2"><strong>Resposta</strong></label>
                                <textarea
                                    value={respostaopcao}
                                    style={{ minHeight: "10rem", resize: "none" }}
                                    onChange={(e) => setrespostaopcao(e.target.value)}
                                    id="textAreaContentAtt"
                                    className="form-control">
                                </textarea>

                            </div>
                        </div>

                        {/* Caso A mensagem Seja do Tipo de Resposta Gerada Por IA*/}
                        <div className="gap-3 justify-content-between mt-4 flex-column" style={{ display: "none" }}
                            id="TextareaSelectIAAtt">
                            <div className="row">
                                <div className="col-6 mb-3">
                                    <label className="mb-2"><strong>Titulo</strong></label>
                                    <Input
                                        lenght={"24"}
                                        value={tituloOpcao}
                                        onChange={(e) => settituloOpcao(e.target.value)}
                                        id={"TituloSimplesInputIAAtt"}
                                        placeholder={"Obrigatorio"}
                                    />
                                    {/* <input id="TituloSimplesInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                                </div>
                                {/* <div className="col-6 mb-3">
                                    <label className="mb-2"><strong>Descricao</strong></label>
                                    <Input
                                        value={descricaoopcao}
                                        onChange={(e) => setdescricaoopcao(e.target.value)}
                                        id={"DescricaoSimplesInputAtt"}
                                        placeholder={"Obrigatorio"}
                                    />
                                </div> */}
                            </div>
                            <div className="w-100">
                                <label className="mb-2"><strong>Comando</strong></label>
                                <textarea
                                    value={descricaoopcao}
                                    style={{ minHeight: "10rem", resize: "none" }}
                                    onChange={(e) => setdescricaoopcao(e.target.value)}
                                    id="textAreaContentIAAtt"
                                    className="form-control">
                                </textarea>

                            </div>
                        </div>

                        {/* Caso A mensagem Seja do Tipo de Multiplas Escolhas */}
                        <div className="row mt-4" style={{ display: "none" }} id="MultiplaEscolhaAtt">
                            <div className="col-6 mb-3" id='TituloOpcaoMenuInputAttContainer'>
                                <label className="mb-2"><strong>Titulo da Opção</strong></label>
                                <Input
                                    lenght={"24"}
                                    value={tituloOpcao}
                                    onChange={(e) => settituloOpcao(e.target.value)}
                                    id={"TituloOpcaoMenuInputAtt"}
                                    placeholder={"Obrigatorio"}
                                />
                                {/* <input maxLength={24} id="TituloOpcaoMenuInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                            </div>
                            <div className="col-6 mb-3" id='DescricaoMenuInputAttContainer'>
                                <label className="mb-2"><strong>Descrição</strong></label>
                                {/* <input id="DescricaoMenuInputAtt" placeholder="Opcional" className="form-control" /> */}
                                <Input
                                    value={descricaoopcao}
                                    onChange={(e) => setdescricaoopcao(e.target.value)}
                                    id={"DescricaoMenuInputAtt"}
                                    placeholder={"Opcional"}
                                />
                            </div>
                            <div className="col-6 mb-3" id='TituloMenuInputAttContainer'>
                                <label className="mb-2"><strong>Titulo do Menu</strong></label>
                                {/* <input id="TituloMenuInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                                <Input
                                    value={tituloMenu}
                                    onChange={(e) => settituloMenu(e.target.value)}
                                    id={"TituloMenuInputAtt"}
                                    placeholder={"Obrigatorio"}
                                />
                            </div>
                            <div className="col-6 mb-3" id='cabecalhoMenuInputAttContainer'>
                                <label className="mb-2"><strong>Cabeçalho</strong></label>
                                {/* <input id="cabecalhoMenuInputAtt" placeholder="Opcional" className="form-control" /> */}
                                <Input
                                    value={cabecalhomenu}
                                    onChange={(e) => setcabecalhomenu(e.target.value)}
                                    id={"cabecalhoMenuInputAtt"}
                                    placeholder={"Opcional"}
                                />
                            </div>
                            <div className="col-6 mb-3" id='corpoMenuInputAttContainer'>
                                <label className="mb-2"><strong>Corpo</strong></label>
                                {/* <input id="corpoMenuInputAtt" placeholder="Obrigatorio" className="form-control" /> */}
                                <Input
                                    value={corpomenu}
                                    onChange={(e) => setcorpomenu(e.target.value)}
                                    id={"corpoMenuInputAtt"}
                                    placeholder={"Opcional"}
                                />
                            </div>
                            <div className="col-6 mb-3" id='rodapeMenuInputAttContainer'>
                                <label className="mb-2"><strong>rodapé</strong></label>
                                {/* <input id="rodapeMenuInputAtt" placeholder="Opcional" className="form-control" /> */}
                                <Input
                                    value={rodapemenu}
                                    onChange={(e) => setrodapemenu(e.target.value)}
                                    id={"rodapeMenuInputAtt"}
                                    placeholder={"Opcional"}
                                />
                            </div>
                        </div>


                    </div>
                    <div className="modal-footer border-top-0">
                        <ButtonBase
                            AtributoPersonalizado={{ 'data-bs-dismiss': "modal" }}
                            className={"btn buttonCancelarFromModal"}
                            Description={"Cancelar"}
                        />
                        <ButtonBase
                            // AtributoPersonalizado={{ 'data-bs-dismiss': "modal" }}
                            className={"btn buttonSalvarFromHome"}
                            Description={"Salvar"}
                            onClick={teste}
                        />
                    </div>
                </div>
            </div>
        </div>

    )
}