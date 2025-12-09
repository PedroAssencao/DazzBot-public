import './style.css';

export default function ButtonBase(props) {
    return (
        <div>
            <button
                type={"button"}
                id={props.id}
                className={props.className}
                disabled={props.IsDisabled}
                onClick={props.onClick}
                {...props.AtributoPersonalizado}
            >
                {props.Description}
            </button>
        </div>
    );
}
