import ButtonBase from '../button';
import './style.css';

export default function ModalBase(props) {
    const HasHeader = props.HasHeader || false;
    const ModalSizeClass = `modal-dialog modal-dialog-centered ${props.ModalSize || null}`;

    return (
        <div className="modal fade" id={props.id} tabIndex="-1" aria-hidden="true">
            <div className={ModalSizeClass}>
                <div className="modal-content">

                    {HasHeader && (
                        <div className="modal-header">
                            <h5 className="modal-title">{props.title}</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    )}

                    <div className="modal-body">
                        <div className="d-flex gap-3 mt-3 justify-content-between">
                            <h6 className="h6">
                                <strong>{props.Description}</strong>
                            </h6>
                        </div>
                    </div>

                    <div className="modal-footer border-top-0">
                        <ButtonBase
                            AtributoPersonalizado={{ 'data-bs-dismiss': "modal" }}
                            className="btn buttonCancelarFromModal"
                            Description="Cancelar"
                        />
                        <ButtonBase                            
                            className={props.ButtonClassName}
                            Description={props.ButtonDescription}
                            onClick={props.ButtonOnclick}
                        />
                    </div>

                </div>
            </div>
        </div>
    );
}
