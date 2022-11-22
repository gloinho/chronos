<div align="center">
<h1>Chronos</h1>
</div>
<p align="center"><i>“Tudo o que temos de decidir é o que fazer com o tempo que nos é dado"</i></p>

# Índice
- [Apresentação do Projeto](#apresentação-do-projeto)
- [Design da Aplicação, Tecnologias e Abordagens utilizadas](#design-da-aplicação-tecnologias-e-abordagens-utilizadas)
- [Glossário](#glossário)
- [Contextos Delimitados]()
- [Modelagens](#modelagens)
    - [Diagrama Entidade-Relacionamento](#diagrama-entidade-relacionamento)
    - [Diagrama de Classes](#diagrama-de-classes)
- [Camadas da Aplicação](#camadas-da-aplicação)
    - [Chronos.Api](#chronosapi)
    - [Chronos.Crosscutting](#chronoscrosscutting)
    - [Chronos.Data](#chronosdata)
    - [Chronos.Domain](#chronosdomain)
    - [Chronos.IoC](#chronosioc)
    - [Chronos.Services](#chronosservices)
    - [Chronos.Testes](#chronostestes)
- [Controllers](#controllers)
- [Desafios e Proximos Passos](#desafios-e-proximos-passos)
- [Requirements](#requirements)
- [Agradecimentos](#agradecimentos)
- [Desenvolvedores](#desenvolvedores)

# Apresentação do Projeto
O objetivo do presente projeto é atender os critérios do desafio final da turma de .NET da Raro Academy, sintetizando todo o conteúdo visto nas ultimas 10 semanas por meio de aulas síncronas na plataforma Zoom e monitorias diárias.  

Como motivação, nos foi apresentado a proposta de projeto pelo cliente Raro Labs, que pretende gradualmente parar de usar software de terceiros para realizar a gestão de horas que são destinadas a projetos pelos seus colaboradores.  

Para isso, se viu necessário a criação de uma solução que atendesse as expectativas do cliente.   
Essa solução se chama **Chronos** <sub>v1.0</sub>

# Design da Aplicação, Tecnologias e Abordagens utilizadas
O design escolhido para a arquitetura do software foi o Domain-Driven-Design (DDD). Essa modelagem consiste (de uma maneira bem abstrata) em camadas de design de dados, design estratégico e design tático, este ultimo somente funcionando se o design estratégico funcionar de maneira excepcional.  

Desse modo, o primeiro passo da modelagem do nosso sistema foi realizar a montagem do [Glossário](#glossário) a ser utilizado ao longo da aplicação para estabelecer uma linguagem ubíqua(universal) que facilitasse a comunicação entre os desenvolvedores e os stakeholders.

Com o glossário em mãos, o próximo passo a ser realizado, ainda no design estratégico, foi traçar os [Contextos Delimitados](#contextos-delimitados) da aplicação (Bounded Contexts) para após isso, engatinhar para a a modelagem.  

Para facilitar a construção da aplicação, realizamos inicialmente o [Diagrama de Entidade-Relacionamento](#diagrama-entidade-relacionamento) para começarmos a ter uma idéia do que precisariamos persistir no banco de dados.  

Feito isso, o próximo passo foi começar a pensar nas classes, interfaces, e contratos que provavelmente utilizariamos ao desenvolver o software. Nessa etapa, diversas mudanças foram feitas ao adentrar mais e mais no problema e entender o que realmente precisariamos realizar para alcançar o que nos era solicitado. Para isso, realizamos o [Diagrama de Classes](#diagrama-de-classes) para auxiliar nesse processo.  

O próximo processo foi realmente colocar "as mãos no código". Seguindo a arquitetura do DDD, as [Camadas da Aplicação](#camadas-da-aplicação) foram executadas, testadas e documentadas.  

O projeto foi realizado utilizando a plataforma de desenvolvimento open source [.NET Core 6](https://learn.microsoft.com/pt-br/dotnet/fundamentals/).

A solução de banco de dados relacional utilizada foi o [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

Para facilitar a persistência dos dados e comunicação com o banco de dados, utilizamos a biblioteca O/RM [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

Algumas outras bibliotecas externas utilizadas ao longo do desenvolvimento: 
- Autofixture v4.17.0
- Automapper v12.0.0
- Bogus v34.0.2
- FluentValidation v11.3.0
- Newtonsoft.Json v9.0.1
- Bcrypt v
- Moq v4.18.2
- Moq.EntityFrameworkCore v6.0.1.4
- NetCORE.MailKit v2.1.0

# Modelagens
## Diagrama Entidade-Relacionamento
## Diagrama de Classes
# Camadas da Aplicação
## Chronos.Api
## Chronos.Crosscutting
## Chronos.Data
## Chronos.Domain
## Chronos.IoC
## Chronos.Services
## Chronos.Testes
# Desafios e Proximos Passos
# Requirements
# Get Started
# Agradecimentos
# Desenvolvedores

# Glossário

- Usuário: qualquer pessoa cadastrada na solução Chronos. Um usuário pode ser Colaborador e Administrador.
- Projeto: Representação do projeto na solução. Um Usuário participar de nenhum ou vários projetos. Um projeto pode ter nenhum ou vários usuários inseridos.
- Usuario_Projeto: Representação da relação entre projeto e usuário. Obrigatoriamente precisa ter um projeto e um usuário. Uma relação Usuario_Projeto pode ter zero ou várias tarefas.
- Tarefa: Representação das tarefas realizadas pelos usuários cadastrados na solução. Uma tarefa obrigatoriamente precisa ser vinculada a um e somente um Usuario_Projeto.
- ConfirmacaoToken: Token criado para a confirmação do e-mail do usuário.
- ResetSenhaToken: Token criado para o reset da senha de um usuário.
- Relatório de Horas: Representação de uma lista de tarefas, contendo total de horas, descrição, data inicial, data final da tarefa e nome do projeto.

# Contextos Delimitados
- Usuario
    - Todo usuário deverá ser cadastrado e para acessar a solução, precisa autenticar seu e-mail.
    - Um usuário pode ser administrador ou colaborador, mas mantem outras caracteristicas iguais.
    - Um colaborador somente pode alterar e ver suas informações.
    - Um administrador pode alterar, ver e deletar todos os usuários.
- Autenticação
    - Um usuário precisa ter seu e-mail autenticado para acesso total a solução Chronos.
    - Um usuário pode efetuar login no sistema usando seu e-mail confirmado e senha.
- Projetos
    - Apenas usuários administradores podem ver, deletar, criar, editar e adicionar outros usuários em todos os projetos.
    - Usuários colaboradores podem apenas ver os projetos em que estão inseridos.
- Tarefas
    - Qualquer usuário pode efetuar cadastro de uma tarefa na solução, desde que esteja participando de um projeto.
    - Um usuário colaborador só pode ver o seu relatório de horas, editar, ver, iniciar, parar e excluir suas tarefas.
    - Um usuário administrador pode ver todos os relatório de horas, editar, ver, iniciar, parar e excluir as tarefas de todos os usuários.
    - Uma tarefa só pode ser iniciada se não tiver sido iniciada ou finalizada.
    - Uma tarefa só pode ser finalizada se não tiver sido finalizada e já tiver sido iniciada.

# Controllers

## Autenticação Controller
- [/api/autenticacao](./Endpoints.md#apiautenticacao-post) `POST` <sub>Ativa o e-mail do usuário.</sub>
- [/api/autenticacao/login](./Endpoints.md#apiautenticacaologin-post) `POST` <sub>Realiza o login do usuário.</sub>

## Projeto Controller
- [/api/projeto](./Endpoints.md#apiprojeto-post) `POST`<sub>Cadastra um projeto.</sub>
- [/api/projeto](./Endpoints.md#apiprojeto-get) `GET`<sub>Lista todos os projetos.</sub>
- [/api/projeto/{id}/colaboradores](./Endpoints.md#apiprojetoidcolaboradores-post) `POST`<sub>Adiciona colaboradores em um projeto.</sub>
- [/api/projeto/{id}](./Endpoints.md#apiprojetoid-get) `GET`<sub>Retorna um projeto pelo ID.</sub>
- [/api/projeto/{id}](./Endpoints.md#apiprojetoid-put) `PUT` <sub>Edita um projeto</sub>
- [/api/projeto/{id}](./Endpoints.md#apiprojetoid-delete) `DELETE` <sub>Remove um projeto.</sub>
- [/api/projeto/usuario/{usuarioId}](./Endpoints.md#apiprojetousuariousuarioid-get) `GET` <sub>Lista todos os projetos que um usuário participa.</sub>

## Usuario Controller 
- [/api/usuario](./Endpoints.md#apiusuario-post)  `POST` <sub>Cadastra um usuário.</sub>
- [/api/usuario](./Endpoints.md#apiusuario-get) `GET` <sub>Lista todos os usuários.</sub>
- [/api/usuario/{id}](./Endpoints.md#apiusuarioid-delete) `DELETE` <sub>Remove um usuário.</sub>
- [/api/usuario/{id}](./Endpoints.md#apiusuarioid-get) `GET` <sub>Retorna um usuário pelo ID.</sub>
- [/api/usuario/{id}](./Endpoints.md#apiusuarioid-put) `PUT` <sub>Edita as informações de um usuário.</sub>
- [/api/usuario/senha](./Endpoints.md#apiusuariosenha-post) `POST` <sub>Envia o código para reset de senha para o e-mail.</sub>
- [/api/usuario/senha](./Endpoints.md#apiusuariosenha-put) `PUT` <sub>Reseta a senha de um usuário.</sub>

## Tarefa Controller
- [/api/tarefa](./Endpoints.md#apitarefa-post) `POST` <sub>Cadastra uma tarefa.</sub>
- [/api/tarefa](./Endpoints.md#apitarefa-get) `GET` <sub>Lista todas as tarefas.</sub>
- [/api/tarefa/{id}/start](./Endpoints.md#apitarefaidstart-patch) `PATCH` <sub>Inicia uma tarefa.</sub>
- [/api/tarefa/{id}/stop](./Endpoints.md#apitarefaidstop-patch) `PATCH` <sub>Para uma tarefa.</sub>
- [/api/tarefa/{id}](./Endpoints.md#apitarefaid-delete) `DELETE` <sub>Remove uma tarefa.</sub>
- [/api/tarefa/{id}](./Endpoints.md#apitarefaid-put) `PUT` <sub>Edita uma tarefa.</sub>
- [/api/tarefa/{id}](./Endpoints.md#apitarefa-get) `GET` <sub>Retorna uma tarefa pelo ID.</sub>
- [/api/tarefa/usuario/{usuarioId}](./Endpoints.md#apitarefausuariousuarioid-get) `GET` <sub>Retorna todas as tarefas de um usuário.</sub>
- [/api/tarefa/{usuarioId}/dia](./Endpoints.md#apitarefausuarioiddia-get) `GET` <sub>Lista todas as tarefas que iniciaram e terminaram no dia.</sub>
- [/api/tarefa/{usuarioId}/semana](./Endpoints.md#apitarefausuarioidsemana-get) `GET` <sub>Lista todas as tarefas que iniciaram e terminaram na semana.</sub>
- [/api/tarefa/{usuarioId}/mes](./Endpoints.md#apitarefausuarioidmes-get) `GET` <sub>Lista todas as tarefas que iniciaram e terminaram no mês.</sub>
- [/api/tarefa/projeto/{projetoId}](./Endpoints.md#apitarefaprojetoprojetoid-get) `GET` <sub>Lista todas as tarefas de um projeto.</sub>

## Toggl Controller  
- [/toggl/relatorio-de-horas]() `GET` <sub>Endpoint de integração com o toggl.</sub>
