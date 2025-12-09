import '../StatusAtendimento/style.css'
export default function StatusAtendimento(props) {
    const handleSubmit = (e) => {
        if (!e.target.classList.contains("activeOption")) {
            document.querySelectorAll(".activeOption").forEach(x => {
                x.classList.remove("activeOption");
            });
            e.target.classList.add("activeOption");
        }
        props.SetStatusActive(e.target.textContent);
    };

    return (
        <div
            className="d-flex justify-content-center align-items-center align-items-lg-start MarginMedia flex-column gap-3">

            <h1 style={{ color: "#263a6d", fontWeight: "bold" }}>Conversas</h1>

            <div className="mt-2 d-flex  justify-content-center align-items-center rounded-3"
                style={{ width: "97%", padding: "0.6rem", backgroundColor: "#b0dea5" }}>
                <div className="row gap-2 justify-content-center align-items-center ">
                    <div onClick={handleSubmit} role='button' className="col conversaStatus activeOption rounded-2 p-1 text-center"
                        style={{ minWidth: "7rem" }}>
                        Ativo</div>
                    <div role='button' onClick={handleSubmit} className="col conversaStatus rounded-2 p-1 text-center" style={{ minWidth: "7rem" }}>
                        Esperando</div>
                    <div onClick={handleSubmit} role='button' className="col conversaStatus rounded-2 p-1 text-center"
                        style={{ minWidth: "7rem" }}>
                        Fila</div>
                    {/* <div role='button' className="col menuButtonStatus">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                            fill="currentColor" className="bi bi-three-dots-vertical" viewBox="0 0 16 16">
                            <path
                                d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0m0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0m0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0" />
                        </svg>
                    </div> */}
                </div>
            </div>


        </div>
    )
}