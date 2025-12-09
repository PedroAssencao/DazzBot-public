import './style.css'

export default function SmallLoadScreen() {
    return (
        <div className='d-flex justify-content-center align-items-center w-100 h-100 bg-light'>
            <div className="spinner-border text-dark" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
        </div>
    )
}