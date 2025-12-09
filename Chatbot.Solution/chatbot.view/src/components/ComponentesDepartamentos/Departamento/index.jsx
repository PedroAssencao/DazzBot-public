
import { useEffect } from 'react'
import './style.css'
export default function DepartamentoComponent(props) {
    useEffect(() => {
        console.log(props);
    }, []);

    const teste = (e) => {
        props.setDepartamentoAtivoId(e)
    }
    
    return (

        <div className="DepartamentoCompo row">

            <div className='col dep'>
                <h3 className="TipoDep">{props.date.descricao}</h3>
                <p className="NumberDep">#{props.date.id}</p>
            </div>
            <div className='col dep depicon'>
                <button
                    role="button"
                    onClick={() => {
                        props.MudarStatusModal({
                            Prop: "Att",
                            Model: props?.date?.descricao,
                            OpenModal: true
                        });
                        teste(props.date.id);
                    }}
                    className="btn text-dark border-0"
                    type="button"
                    id="dropdownMenuButton"
                >
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="35"
                        height="35"
                        fill="#263a6d"
                        className="bi bi-arrow-right-circle DepartamentoIcon"
                        viewBox="0 0 16 16"
                    >
                        <path
                            fillRule="evenodd"
                            d="M1 8a7 7 0 1 0 14 0A7 7 0 0 0 1 8m15 0A8 8 0 1 1 0 8a8 8 0 0 1 16 0M4.5 7.5a.5.5 0 0 0 0 1h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 1 0-.708.708L10.293 7.5z"
                        />
                    </svg>
                </button>

            </div>
        </div>

    )
}

