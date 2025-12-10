import '../MobileNavbar/style.css'
export default function MobileNavbar(props) {
    return (
        //Navbar
        <div className="col-12 p-2 d-lg-none" style="background-color: #EBEFF9;" id="navbar">
            <div className="row justify-content-between">

                {/* Contato, Ver Como Vai ficar isso aqui depois, acho massa fazer algo como whastapp que so aparece os chat quando clicka aqui */}
                <div className="col mt-3 d-flex ">
                    {/* Icone para voltar na versão mobile */}
                    <div className="p-3 d-lg-none">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                            fill="currentColor" className="bi bi-arrow-left" viewBox="0 0 16 16">
                            <path fill-rule="evenodd"
                                d="M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8" />
                        </svg>
                    </div>
                    <div className="d-flex gap-3">
                        <div className="d-flex align-items-end gap-3">
                            <img className="leadImage rounded-circle" src="./img/file.jpg" />
                            <p style="font-family: Arial, Helvetica, sans-serif;">Pedro Assenção</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    )
}