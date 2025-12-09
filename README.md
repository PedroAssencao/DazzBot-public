# DazzleBot 🤖 - Plataforma de Chatbot para Atendimento

O **DazzleBot** é uma plataforma completa de **chatbot para atendimento ao cliente**, projetada para otimizar a comunicação e o suporte. O projeto é construído em uma arquitetura moderna, utilizando **.NET 8.0 (C#)** para o backend, integrando com a **API do WhatsApp (Meta)** e **Inteligência Artificial (OpenAI)** para automação, e uma interface de usuário dinâmica em **React/Vite** para a gestão e o atendimento humano em tempo real via **SignalR**.

---

## 🛠️ Instalação

Siga os passos abaixo para configurar e rodar o projeto localmente.

### Pré-requisitos

Certifique-se de ter instalado:

* **SDK do .NET 8.0**
* **SQL Server** (ou configure para usar outro banco de dados compatível com Entity Framework Core).
* **Node.js e npm** (para o frontend React).

### 1. Configuração do Banco de Dados

1.  Crie um novo banco de dados no seu SQL Server (por exemplo, `chatbot`).
2.  Execute os scripts SQL disponíveis na pasta `DbAtualizado` (recomenda-se usar o mais recente, como `DbAtualizado/BancoDeDadosAtualizado20_11_24.sql`) para criar o esquema e popular as tabelas iniciais.

### 2. Configuração do Backend (`Chatbot.API`)

1.  Navegue até a pasta `Chatbot.Solution/Chatbot.API`.
2.  Crie um arquivo `appsettings.json` (baseado em `appsettings.example.json`) e configure:
    * A **ConnectionString** para o seu banco de dados (chave `Chinook`).
    * As chaves de API para Meta (WhatsApp) e OpenAI, se aplicável.
3.  Execute a API:
    ```bash
    dotnet run
    ```
    A API será iniciada (por padrão em `http://localhost:5058` ou `https://localhost:7261`).

### 3. Configuração do Frontend (`chatbot.view`)

1.  Navegue até a pasta `Chatbot.Solution/chatbot.view`.
2.  Instale as dependências:
    ```bash
    npm install
    ```
3.  Verifique o arquivo `src/appsettings.jsx` e confirme se a `urlBase` aponta para o endereço correto da sua API (ex: `http://localhost:5058/api`).
4.  Execute o frontend:
    ```bash
    npm run dev
    ```

---

## 🚀 Uso

O DazzleBot oferece uma interface de gestão e atendimento completa.

### Módulos Principais

* **Login (`/Login`)**: Página inicial para autenticação de usuários (Master, Usuário e Atendente).
* **Dashboard (`/Home` ou `/DashBoard`)**: Visão gerencial com gráficos de atendimentos ativos por atendente e departamento, leads e volume de mensagens por dia.
* **Atendimento (`/Atendimento`)**: Interface do atendente para conversas em tempo real com clientes do WhatsApp, com categorização de conversas (Ativo, Esperando, Fila) e uso de **SignalR** para atualizações instantâneas.
* **Fluxo do Bot (`/FluxoBot`)**: Permite a criação e edição visual do fluxo de conversação do chatbot, incluindo mensagens de resposta simples, menus de múltipla escolha e respostas geradas por IA.
* **Usuários/Departamentos (`/Usuario` e `/Departamento`)**: Gerenciamento de contas de atendentes e dos departamentos para roteamento de conversas.

---

## 🎨 Estilo de Codificação

O projeto segue um padrão de **arquitetura em camadas** claro, separando as responsabilidades para facilitar a manutenção e o desenvolvimento:

### Backend (.NET)

* **Estrutura de Projetos (Solution):** Utiliza projetos separados para `Domain`, `Infrastructure`, `Services` e `API`.
* **Domain**: Contém os modelos de domínio (ex: `Atendimento.cs`, `Menu.cs` e Enums).
* **Services**: Contém a lógica de negócio (Business Logic) e a manipulação de DTOs (Data Transfer Objects), garantindo a separação das preocupações (ex: `AtendimentoServices.cs`).
* **Infrastructure**: Lida com a persistência de dados (Entity Framework Core e Repositórios - ex: `BaseRepository.cs`) e integrações externas (OpenAI, Meta).

### Frontend (React/Vite)

* **Componentização**: A interface é dividida em componentes reutilizáveis (ex: `conversaCard`, `ModalAddOuAttUsuario`).
* **Hooks**: Uso de `useState` e `useEffect` para gerenciar estado e ciclo de vida nos componentes de página (ex: `AtendentePage/index.jsx`, `Usuario/index.jsx`).
* **Estilização**: Cada componente ou página tem seu próprio arquivo `.css` para estilos localizados (ex: `pages/Perfil/style.css`, `components/ComponentesDepartamentos/ModalAddOuAttDep/style.css`).

---
