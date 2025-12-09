import { UsuarioLogado } from "../../appsettings";
import { useEffect, useState } from "react";
import LoadScreen from "../../components/BaseComponents/loadingScreen";
export default function Home() {
    const [IsLoading, setIsLoading] = useState(true);
    useEffect(() => {
        console.log("Carregou use effect")
        UsuarioLogado().then(result => {
            console.log("Entrou no usuario logado funciton")

            //aqui podemos redirecionar para qualquer tela dependendo se o usuario esta logado, se e um usuario cliente se e master se e atendete etc...

            console.log(result)
            if (result.usuarioLogadoId == null) {
                console.log("Usuario Redirecionado")
                alert("Usuario precisa estar logado")
                location.replace(location.origin + "/login");
            }

            if (result.tipoUsuario == "Atendente") {
                console.log("Usuario Redirecionado")
                alert("Usuario Não tem permissão para acessar essa tela")
                location.replace(location.origin + "/Atendimento");
            }
            setIsLoading(false)
        });
    }, []);

    return (
        <>
            {IsLoading ? (
                <LoadScreen />
            ) : (
                <div className="col">
                    <h1>Home</h1>
                </div>
            )}
        </>

    );
}
