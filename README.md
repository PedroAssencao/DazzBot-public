# DazzleBot 🤖 — Plataforma de Chatbot para Atendimento (TCC)

O **DazzleBot** é uma plataforma completa de atendimento via **WhatsApp (Meta)**, **unificando atendentes em um único número**, permitindo automação com **IA**, **envio de mensagens em massa**, **criação de fluxos de bot**, **dashboards** e controle total das conversas entre **bot** e **humano** — tudo rodando sobre **SQL Server**, **ASP.NET com C#**, **React com JavaScript** e **Entity Framework Core**. Desenvolvido como projeto de TCC, integra automação e atendimento humano de forma profissional e escalável.

---

# 🎥 Demonstração

https://github.com/user-attachments/assets/c602f801-3c41-457b-83c8-98eb518db143
<img width="1919" height="967" alt="image" src="https://github.com/user-attachments/assets/d25d7243-2f49-4aff-bfa1-f6277e842a95" />
<img width="1919" height="965" alt="image" src="https://github.com/user-attachments/assets/99b86618-aa98-419a-9825-e9def17fd739" />
<img width="1919" height="969" alt="image" src="https://github.com/user-attachments/assets/1c4a8318-a59b-4e95-a0c1-a3d815fd3444" />
<img width="1919" height="962" alt="image" src="https://github.com/user-attachments/assets/6476b3f1-2d53-455d-9782-98799c16c9b4" />
<img width="1919" height="964" alt="image" src="https://github.com/user-attachments/assets/340501be-c84a-4fa3-8496-8b1ac05d908e" />
<img width="1919" height="965" alt="image" src="https://github.com/user-attachments/assets/d1c861f0-2ece-43c0-86cf-923b5c92c2dd" />
<img width="1919" height="967" alt="image" src="https://github.com/user-attachments/assets/d194c20e-3bc3-473e-9111-e0229f875834" />




# 🚀 Inicialização

Abaixo estão todos os passos para executar o projeto corretamente — incluindo a parte do webhook do Meta.

---

## 🔧 Pré-requisitos

Instale antes de iniciar:

- **.NET SDK 8.0**
- **SQL Server**
- **Node.js + npm**
- **ngrok** (para expor o backend a Meta)
- Uma conta no **Meta for Developers**

---

# 🗄️ 1. Banco de Dados

1. Crie um novo banco no SQL Server (ex: `chatbot`).
2. Acesse a pasta `DbAtualizado`.
3. Execute o script mais recente (ex.: `BancoDeDadosAtualizado20_11_24.sql`) para criar tabelas e dados iniciais.

---

# ⚙️ 2. Configurando o Backend (`Chatbot.API`)

1. Entre em:
   ```
   Chatbot.Solution/Chatbot.API
   ```
2. Crie o arquivo **appsettings.json** baseado em `appsettings.example.json`.
3. Configure:
   - **ConnectionString** → chave `Chinook`
   - Chaves da **Meta** (WhatsApp)
   - Chave do **OpenAI**
4. Rode a API:
   ```bash
   dotnet run
   ```
   A API ficará disponível, por exemplo, em:
   - `http://localhost:5058/`
   - `https://localhost:7261/`

---

# 🌎 3. Configurando o Webhook do Meta (WhatsApp)

1. Com a API **rodando**, inicie o **ngrok**:
   ```bash
   ngrok http 5058
   ```
2. Pegue a URL gerada (ex.: `https://f0a2ab243a9b.ngrok-free.app`).
3. Vá até **Meta for Developers** → Webhooks → Configure:

```
{URL_DO_NGROK}/api/v1/Meta/hook
```

Exemplo:
```
https://f0a2ab243a9b.ngrok-free.app/api/v1/Meta/hook
```

4. Na configuração do Webhook:
   - Ative **"messages"**
   - Use versão **v19.0 ou superior**

Se tudo estiver correto, o webhook será validado automaticamente e o bot ficará ativo com o fluxo padrão criado pelo SQL.

---

# 💻 4. Configurando o Frontend (`chatbot.view`)

1. Acesse:
   ```
   Chatbot.Solution/chatbot.view
   ```
2. Instale dependências:
   ```bash
   npm install
   ```
3. Execute:
   ```bash
   npm run dev
   ```

Se o backend estiver funcionando e configurado, tudo estará integrado automaticamente.

---

# 🔐 Usuários padrão

## 👑 Master (Administrador)
```
email: master.123@123
senha: senai.123
```

## 👨‍💼 Atendente
```
email: emailTeste@gmail.com
senha: atendente@123
```

---

# 🧭 Funcionalidades

## 👑 Modo Master (Administrador)

O usuário Master possui visão completa do sistema:

### Dashboard
- Mensagens recebidas por dia  
- Atendentes online  
- Leads  
- Atendimentos ativos
- configurar comandos para o uso do cliente no chat, como "/finalizar" - usado para finalizar instantaneamente um chat ou o "/reset" para recomeçar o fluxo do chat do zero.
- Atendimentos pendentes  
- Atendimentos por departamento  
- Atendimentos por atendente  

### Gerenciamento
- 👤 **Perfil** → edição de dados pessoais  
- 🏢 **Departamentos** → criar/editar departamentos  
- 👥 **Usuários** → criar atendentes e administradores  
- 📢 **Mensagens em Massa** → enviar para vários leads  
- 🤖 **Fluxo do Bot** → editar o fluxo de conversação atual (menus, respostas simples e respostas com IA)

---

# 🎧 Modo Atendente

O atendente tem acesso ao módulo de atendimento em tempo real.

### Atendimentos
- Conversas divididas por:
  - "Ativo"
  - "Esperando"
  - "Fila"
- Receber e enviar mensagens para leads via WhatsApp
- Ver o que o bot enviou e o que o usuário respondeu
- Atualização instantânea via **SignalR**

### Outras Funções
- 👤 Alterar seu perfil  
- 📢 Enviar mensagens em massa  

---

# 🏗️ Arquitetura e Estrutura do Código

O projeto segue uma arquitetura em camadas para facilitar manutenção e escalabilidade.

## Backend (.NET 8)
- **Domain** → entidades e modelos
- **Services** → regras de negócio, DTOs, validações
- **Infrastructure** → EF Core, repositórios, integrações externas
- **API** → Endpoints, Controllers, autorização, middlewares

## Frontend (React + Vite)
- Componentes reaproveitáveis
- Hooks (`useState`, `useEffect`)
- Pastas organizadas por páginas
- Estilos isolados por componente (`.css`)

---

# 🎓 Projeto desenvolvido como TCC

Este sistema foi desenvolvido como **Trabalho de Conclusão de Curso**, utilizando:

- **SQL Server**
- **ASP.NET / C#**
- **React (JavaScript)**
- **Entity Framework Core**
- **SignalR**
- **Integração oficial com a API do WhatsApp (Meta)**

---
