use master
go
create database chatbot
go
use chatbot
go
-- Tabelas
create table login (
    log_id int identity(1,1) primary key,
    log_email varchar(255),
    log_senha varchar(255),
    log_img varchar(max),
    log_plano varchar(255),
    log_user varchar(255),
	log_tipo int,
    log_waid varchar(255)
)
go

create table departamento (
    dep_id int identity(1,1) primary key,
    dep_descricao varchar(255),
    log_id int,
    constraint fk_login_departamento foreign key (log_id) references login(log_id) on delete set null
)
go
create table atendentes (
    ate_id int identity(1,1) primary key,
    ate_email varchar(255),
    ate_Nome varchar(255),
    ate_img varchar(max),
    ate_senha varchar(255),
    ate_estado bit,
    log_id int,
    dep_id int,
    constraint fk_login foreign key (log_id) references login(log_id) on delete set null,
    constraint fk_departamento foreign key (dep_id) references departamento(dep_id) on delete set null
)
go

create table contatos (
    con_id int identity(1,1) primary key,
    con_WaId varchar(255),
    con_nome varchar(255),	
    con_DataCadastro datetime,
    con_BloqueadoStatus bit,
    log_id int,
    constraint fk_login_contatos foreign key (log_id) references login(log_id) on delete set null
)
go

create table Atendimento (
    aten_id int identity(1,1) primary key,
    aten_estado int,
    aten_data datetime,
    ate_id int,
    dep_id int,
    con_id int,
    log_id int,
    constraint fk_atendentes foreign key (ate_id) references atendentes(ate_id) on delete set null,
    constraint fk_departamento_atendimento foreign key (dep_id) references departamento(dep_id) on delete set null,
    constraint fk_contatos foreign key (con_id) references contatos(con_id) on delete set null,
    constraint fk_login_atendimento foreign key (log_id) references login(log_id) on delete set null
)
go

create table chat (
    cha_id int identity(1,1) primary key,
    ate_id int,
    log_id int,
    con_id int,
    aten_id int,
    constraint fk_atendentes_chat foreign key (ate_id) references atendentes(ate_id) on delete set null,
    constraint fk_login_chat foreign key (log_id) references login(log_id) on delete set null,
    constraint fk_contatos_chat foreign key (con_id) references contatos(con_id) on delete set null,
    constraint fk_atendimento_chat foreign key (aten_id) references Atendimento(aten_id) on delete set null
)

go

create table Mensagens (
    mens_id int identity(1,1) primary key,
    mens_data datetime,
    mens_descricao varchar(max),
    men_tipo int,
	men_WaId varchar(max),
	mens_status int,
    con_id int,
    log_id int,
    cha_id int,
    constraint fk_contatos_mensagens foreign key (con_id) references contatos(con_id) on delete set null,
    constraint fk_login_mensagens foreign key (log_id) references login(log_id) on delete set null,
    constraint fk_chat_mensagens foreign key (cha_id) references chat(cha_id) on delete set null
)

ALTER TABLE Mensagens
ALTER COLUMN men_tipo int;

ALTER TABLE Mensagens
ALTER COLUMN mens_status int;



go

create table menus (
    men_id int identity(1,1) primary key,
    men_header varchar(255),
    men_footer varchar(255),
    men_body varchar(255),
    log_id int,
    men_tipo int,
    men_title varchar(255),
    constraint fk_login_menus foreign key (log_id) references login(log_id) on delete set null
)
go

create table options (
    opt_id int identity(1,1) primary key,
    men_id int,
    log_id int,
    opt_data datetime,
    opt_descricao varchar(500),
    opt_finalizar bit,
    opt_resposta varchar(500),
    opt_tipo int,
    opt_title varchar(24),
    constraint fk_menus_options foreign key (men_id) references menus(men_id) on delete set null,
    constraint fk_login_options foreign key (log_id) references login(log_id) on delete set null
)
go

-- Inserções

--se desejar usar o bot de teste precisa alterar o log_waid para 15550882003, se o bot escolhido for o de produção mudar para 557999411293
insert into login (log_email, log_senha, log_img, log_plano, log_user, log_waid, log_tipo) 
values ('master.123@123', 'c2VuYWkuMTIz', 'img-placeholder', 'master', 'Master', '557999411293',1)
go

update login set log_waid = '15550882003' where log_id = 1

insert into departamento (dep_descricao, log_id) 
values ('Suporte', 1)
go


insert into atendentes (ate_email, ate_Nome, ate_img, ate_senha, ate_estado, log_id, dep_id) 
values ('emailTeste@gmail.com', 'AtendenteTeste', 'placeholder.img', 'atendente@123', 1, 1, 1)
go


insert into contatos (con_WaId, con_nome, con_DataCadastro, con_BloqueadoStatus, log_id) 
values ('123456789', 'ContatoTeste', '2024-07-23T22:31:35.673', 0, 1)
go

insert into menus (men_header, men_footer, men_body, log_id, men_tipo, men_title) 
values 
('Empresas Senai', 'Todos direitos reservados', 'Seja Bem Vindo ao Nosso Robo de Atendimento, Antes de Falar Com Nossos Atendentes Por Favor Resposnda as Perguntas Abaixo Para Sabermos o Seu Problema, Tentaremos Resolver Sem Intervenção Humana Se Possivel!', 1, 1, 'MenuInicial'),
('Empresas Senai', 'Todos direitos reservados', 'Por Favor Escolha Qual Parte das Finança Voce Esta Tendo Problemas', 1, 2, 'Finanças'),
('Empresas Senai', 'Todos direitos reservados', 'Por Favor Escolha Qual Setor de Suporte Que Voce Deseja Ser Atendido', 1, 2, 'Suporte'),
('Empresas Senai', 'Todos direitos reservados', 'Escolha Quais Das Opções Abaixo Descreve Melhor o Seu Problema', 1, 2, 'Menu de Dificuldades ao Acessar o Sistema'),
('Empresas Senai', 'Todos direitos reservados', 'Escolha Quais Das Opções Abaixo Descreve Melhor o Seu Problema de Pagamento', 1, 2, 'DificuldadePagar'),
('Empresas Senai', 'Todos direitos reservados', 'Escolha Quais Das Opções Abaixo e a Sua Vontade Se Tiver Mais Alguma Pergunta Apenas Pergunte!', 1, 3, 'Menu IA')
go

--quando a option for tipo RedirecinamentoHumano lembrar que a optResposta tem que ser igual ao id do departamento que deve ser redirecionado para o codigo funcionar
--quando a option for tipo MensagemDeRespostaInterativa lembrar que a optResposta tem que ser igual ao id do menu que deve ser usado como resposta para o codigo funcionar
--quando a option for tipo MensagemPorIA lembrar que a optResposta pode ser null por que não vai ser usada ja que e a ia que ira responder e por enquanto quando esse tipo de mensagem e utilizada ele ja entra automaticamente no modo de interação com ia talvez futuramente quando o cliente queira apenas uma resposta com ia possa colocar a optresposta para ser o id do proximo menu que ela deve responder
--quando a option for tipo MensagemDeResposta lembrar que a optResposta e oque vai ser respondido no chat ja que o texto enviado e de um tipo simples

INSERT INTO options (men_id, log_id, opt_data, opt_descricao, opt_finalizar, opt_resposta, opt_tipo, opt_title) 
VALUES 
    (1, 1, '2024-07-23T22:31:35.673', 'Referente a Financeiro', 0, '2', 3, 'Financeiro'),
    (1, 1, '2024-07-23T22:31:35.673', 'Referente a Suporte', 0, '3', 3, 'Suporte'),
    (1, 1, '2024-07-23T22:31:35.673', 'História do Senai Contada Pela IA e Interação Geral Com IA', 0, NULL, 4, 'História Senai'),
    (5, 1, '2024-07-23T22:31:35.673', 'Pagamento Não Disponível', 1, 'Sua forma de pagamento não está disponível no sistema. Use esse QR code para pagar diretamente: (exemploQRcode)', 1, 'Pagamento Indisponível'),
    (5, 1, '2024-07-23T22:31:35.673', 'Pagamento Não Autorizado', 1, 'Sinto muito pelo transtorno. Se possível, verifique seu saldo para conferir se houve uma transação errônea.', 1, 'Pagamento Não Autorizado'),
    (5, 1, '2024-07-23T22:31:35.673', 'Finalizar Atendimento', 1, 'Muito obrigado por interagir', 1, 'Finalizar'),
    (4, 1, '2024-07-23T22:31:35.673', 'Esqueci Minha Senha', 1, 'Aqui está um link para preencher as informações para o reset da sua senha: (linkExemplo). Espero que fique bem.', 1, 'Esquecimento da Senha'),
    (4, 1, '2024-07-23T22:31:35.673', 'Instabilidade no Geral', 1, 'Lamentamos se o sistema está lento hoje. Estamos em período de manutenção e voltaremos ao normal em breve.', 1, 'Dificuldades Sistemas'),
    (4, 1, '2024-07-23T22:31:35.673', 'Finalizar Atendimento', 1, 'Obrigado por interagir. Volte sempre!', 1, 'Finalizar'),
    (2, 1, '2024-07-23T22:31:35.673', 'Dificuldades no Pagamento', 0, '5', 3, 'Pagamento'),
    (2, 1, '2024-07-23T22:31:35.673', 'Finalizar Atendimento', 1, 'Obrigado por interagir. Espero que tenha conseguido resolver seu problema.', 1, 'Finalizar'),
    (3, 1, '2024-07-23T22:31:35.673', 'Dificuldades com o Sistema', 0, '4', 3, 'Sistema'),
    (3, 1, '2024-07-23T22:31:35.673', 'Falar com Atendente do Setor de Suporte', 0, '1', 6, 'Suporte Humano'),
    (3, 1, '2024-07-23T22:31:35.673', 'Finalizar Atendimento', 1, 'Obrigado por interagir. Espero que tenha conseguido resolver seu problema.', 1, 'Finalizar'),
    (6, 1, '2024-07-23T22:31:35.673', 'Voltar ao Fluxo de Atendimento Normal', 0, 'Sim', 4, 'Sim'),
    (6, 1, '2024-07-23T22:31:35.673', 'Finalizar o Atendimento', 1, 'Obrigado por interagir com o sistema!', 4, 'Finalizar');

select * from login
select * from atendentes
select * from Atendimento
select * from departamento
select * from options
select * from menus
select * from chat
select * from Mensagens
select * from contatos
select * from chat



select * from atendentes

update atendentes set ate_estado = 1 where ate_id = 2

select * from Atendimento

select * from contatos

select * from Mensagens 

UPDATE Atendimento
SET aten_data = GETDATE() + 1
WHERE aten_id = 1