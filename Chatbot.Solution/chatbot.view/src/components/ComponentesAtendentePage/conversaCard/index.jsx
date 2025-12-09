export default function conversaCard(props) {
    const IsRecaiveMessage = props.IsRecaive
    
    return(
        <div className={`rounded-3 p-3 mt-3 ${IsRecaiveMessage ? 'align-self-end' : 'bg-light'}`}  style={{maxWidth: '20rem',backgroundColor: IsRecaiveMessage ? '#a3adc4' : '#fff' ,}}>
            {props.descricao}
        </div>
        
        // <div class="rounded-3 p-3 mt-3 align-self-end" style="max-width: 20rem; background-color: #a3adc4;">
        //     Lorem ipsum dolor sit amet consectetur adipisicing elit. Officia ut porro cum excepturi, ipsum officiis est minima dignissimos? Ea neque est maiores architecto itaque minus nesciunt nulla enim suscipit ab.
        // </div>
    )
}