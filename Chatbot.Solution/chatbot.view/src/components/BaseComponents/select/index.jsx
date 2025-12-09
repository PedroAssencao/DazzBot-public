export default function Select(props) {
    console.log("Dentro do select")
    console.log("optionsList aqui")
    console.log(props.optionsList)
    const { optionsList, onChange, id, className, placeholder } = props;    


    const selectClassName = className || "form-select p-3 backgroudVerderInput";

    const handleChange = (event) => {
        const selectedValue = event.target.value;
        onChange(selectedValue);
    };

    return (
        <select
            id={id}
            onChange={handleChange}
            className={selectClassName}
            placeholder={placeholder}
        >
            {optionsList.map((x) => (
                <option value={x.value} key={x.id}>
                    {x.descricao}
                </option>
            ))}
            
        </select>
    );
}
