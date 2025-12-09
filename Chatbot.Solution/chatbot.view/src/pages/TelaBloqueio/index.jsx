import React, { useState } from 'react';
import './style.css';

export default function TelaBloqueio() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [modalAbrir, setModalAbrir] = useState(false);
    const [newContact, setNewContact] = useState('');
    const [selectedBot, setSelectedBot] = useState('');
    const [contacts, setContacts] = useState([
        { number: '7999923879', bot: 'DazzleBot' },
        { number: '799938392', bot: 'DazzleBot' },
    ]);
    const [contactToDelete, setContactToDelete] = useState(null);

    document.querySelector("#bodyFromPageAll").style = "overflow-y: auto;";

    const handleAddContact = () => {
        if (newContact && selectedBot) {
            const updatedContacts = [...contacts, { number: newContact, bot: selectedBot }];
            setContacts(updatedContacts);
            setNewContact('');
            setSelectedBot('');
            setIsModalOpen(false);
        } else {
            alert("Por favor, preencha todos os campos.");
        }
    };

    const handleOpenDeleteModal = (contact) => {
        setContactToDelete(contact);
        setModalAbrir(true);
    };

    const handleDeleteContact = () => {
        if (contactToDelete) {
            const updatedContacts = contacts.filter(contact => contact !== contactToDelete);
            setContacts(updatedContacts);
            setContactToDelete(null);
            setModalAbrir(false);
        }
    };

    return (
        <div className="col">
            <div className="barra-superior">
                <h1>DazzleBot</h1>
            </div>

            <div className="conteudo">
                <div className="parte-superior">
                    <h2>Bloqueados</h2>
                    <button type="button" className="btn btn-success" onClick={() => setIsModalOpen(true)}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="17" height="17" fill="currentColor" className="bi bi-plus-circle" viewBox="0 0 16 16" >
                            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                        </svg>
                        Adicionar Contato
                    </button>
                </div>

                <hr className="linha-horizontal" />

                <table className="table">
                    <thead className="table-primary">
                        <tr>
                            <th scope="col">Contato</th>
                            <th scope="col">Bot</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody className="table-group">
                        {contacts.map((contact, index) => (
                            <tr key={index}>
                                <td>{contact.number}</td>
                                <td>{contact.bot}</td>
                                <td>
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        width="20"
                                        height="20"
                                        fill="currentColor"
                                        className="bi bi-trash3-fill"
                                        viewBox="0 0 16 16"
                                        onClick={() => handleOpenDeleteModal(contact)}
                                    >
                                        <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
                                    </svg>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {isModalOpen && (
                <div className="modalBloqueio" tabIndex="-1">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">Adicionar Contato</h5>
                                <button type="button" className="btn-close" onClick={() => setIsModalOpen(false)}></button>
                            </div>
                            <div className="modal-body">
                                <label>
                                    Bot:
                                    <select className="form-selectBloqueio" value={selectedBot} onChange={(e) => setSelectedBot(e.target.value)}>
                                        <option value="">Selecione um bot</option>
                                        <option value="DazzleBot">DazzleBot</option>
                                    </select>
                                </label>
                                <br />
                                <label>
                                    Contato:
                                    <input type="text" value={newContact} onChange={(e) => setNewContact(e.target.value)} className="form-control" placeholder="Username" />
                                </label>
                                <br />
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-primary" onClick={handleAddContact}>Salvar</button>
                            </div>
                        </div>
                    </div>
                </div>
            )}

            {modalAbrir && (
                <div className="modal" tabIndex="-1">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">Excluir Contato</h5>
                                <button type="button" className="btn-close" onClick={() => setModalAbrir(false)}></button>
                            </div>
                            <div className="modal-body">
                                <p>Tem certeza de que deseja excluir o contato {contactToDelete?.number}?</p>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" onClick={() => setModalAbrir(false)}>Cancelar</button>
                                <button type="button" className="btn btn-primary" onClick={handleDeleteContact}>Excluir</button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
