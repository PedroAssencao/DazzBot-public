import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home'
import { urlBase, UsuarioLogado } from './appsettings';
import { useEffect, useState } from "react";
import NoPage from './pages/NoPage';
import './App.css';
import Login from './pages/Login'
import Atendimento from './pages/AtendentePage'
import FluxoBot from './pages/FluxoBot'
import DashBoard from './pages/DashBoard'
import Usuario from './pages/Usuario'
import Perfil from './pages/Perfil'
import Departamento from './pages/Departamento'
import Funil from './pages/Funil'
import Sidebar from './components/BaseComponents/sideBar'
import TelaBloqueio from './pages/TelaBloqueio'
import MensagemEmMassa from './pages/MensagemEmMassa'
export default function App() {
  const [usuarioLogadoObj, setUsuarioLogadoObj] = useState({});
  const searchLocation = window.location.pathname.toLocaleLowerCase();

  const urlQueteramSideBarENavBar = ['/', '/home', '/atendimento', '/fluxobot', '/dashboard', '/Usuario', '/Perfil', '/Departamento', '/Funil', '/TelaBloqueio','/MensagemEmMassa'].map(url => url.toLowerCase());

  function isNoPage(urlIndex, LocationIndex) {
    for (let i = 0; i < urlIndex.length; i++) {
      if (urlIndex[i] === LocationIndex) {
        return true;
      }
    }
    return false;
  }

  useEffect(() => {
    console.log("Carregou use effect")
    UsuarioLogado().then(result => {
      console.log("Entrou no usuario logado funciton app.jsx")
      console.log(result)
      setUsuarioLogadoObj(result)
    });
  }, []);

  const IsTrue = isNoPage(urlQueteramSideBarENavBar, searchLocation);
  const versionString = `Versão: 0.01 alpha - Tipo Usuário: ${usuarioLogadoObj.tipoUsuario} - Usuário: ${usuarioLogadoObj.userName}`

  return (
    <BrowserRouter>
      <div className="container-fluid bg-warning min-vh-100">
        <div className="row bg-light min-vh-100">
          <div style={{ fontSize: "10pt", position: "Absolute" }} className='d-flex justify-content-center align-items-center mx-auto'>
            {versionString}
          </div>
          {IsTrue ? <Sidebar /> : null}
          <Routes>
            {/* <Route index element={<Home />} />
            <Route path="/home" element={<Home />} /> */}
            <Route index element={<DashBoard />} />
            <Route path="/home" element={<DashBoard />} />
            <Route path="/Atendimento" element={<Atendimento />} />
            <Route path="/FluxoBot" element={<FluxoBot />} />
            <Route path="/Login" element={<Login />} />
            {/* <Route path="/DashBoard" element={<DashBoard />} /> */}
            <Route path="/Usuario" element={<Usuario />} />
            <Route path="/TelaBloqueio" element={<TelaBloqueio />} />
            <Route path="/MensagemEmMassa" element={<MensagemEmMassa />} />
            <Route path="/Perfil" element={<Perfil />} />
            <Route path="/Departamento" element={<Departamento />} />
            <Route path="/Funil" element={<Funil />} />
            <Route path="*" element={<NoPage />} />
          </Routes>
        </div>
      </div>
    </BrowserRouter>
  )

}

