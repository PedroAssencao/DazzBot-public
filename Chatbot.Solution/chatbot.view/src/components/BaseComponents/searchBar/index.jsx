import A from '../a'
import React, { useState } from 'react';
import Input from '../input';
export default function searchBar(props) {
    const [inputValue, setInputValue] = useState('');

    const classVariable = props.className
    const handleInputChange = (e) => {
        setInputValue(e.target.value);
    };
    return (
        <div className={classVariable} style={{ maxWidth: "90%" }}>
            <Input onChange={handleInputChange} id={"testeasdas"} placeholder={"Buscar por usuário ou telefone"} />
            <A onClick={() => props.searchbarFunction(inputValue)} bootsrapAction={props.bootsrapAction} href={props.href} className={"btn b0dea5"} icon={
                <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor"
                    className="bi bi-search" viewBox="0 0 16 16">
                    <path
                        d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
                </svg>
            } />
        </div>

    )
}