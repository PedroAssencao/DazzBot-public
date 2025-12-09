import './style.css'

export default function LoadScreen() {
    return (
        <div className='d-flex justify-content-center align-items-center w-100 h-100 bg-light LoadingDivPai'>
            <div className="spinner-border LoadingDivFilho" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
        </div>
    )
}