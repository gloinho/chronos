<div align="center">
<h1>Chronos</h1>
</div>

<div align="center">
<p><i>“Tudo o que temos de decidir é o que fazer com o tempo que nos é dado"</i></p>
</div>




# :hourglass: Índice
- [Apresentação do Projeto](#hourglass-apresentação-do-projeto)
- [Design da Aplicação, Tecnologias e Abordagens utilizadas](#hourglass-design-da-aplicação-tecnologias-e-abordagens-utilizadas)
- [Glossário](#hourglass-glossário)
- [Contextos Delimitados](#hourglass-contextos-delimitados)
- [Modelagens](#hourglass-modelagens)
    - [Diagrama Entidade-Relacionamento](#diagrama-entidade-relacionamento)
    - [Diagrama de Classes](#diagrama-de-classes)
- [Camadas da Aplicação](#hourglass-camadas-da-aplicação)
    - [Chronos.Api](#chronosapi)
    - [Chronos.Crosscutting](#chronoscrosscutting)
    - [Chronos.Data](#chronosdata)
    - [Chronos.Domain](#chronosdomain)
    - [Chronos.IoC](#chronosioc)
    - [Chronos.Services](#chronosservices)
    - [Chronos.Testes](#chronostestes)
- [Controllers](#hourglass-controllers)
- [Features](#hourglass-features)
- [Desafios e Proximos Passos](#hourglass-desafios-e-proximos-passos)
- [Requirements](#hourglass-requirements)
- [Get Started](#hourglass-get-started)
- [Agradecimentos](#hourglass-agradecimentos)
- [Desenvolvedores](#hourglass-desenvolvedores)

# :hourglass: Apresentação do Projeto
O objetivo do presente projeto é atender os critérios do desafio final da turma de .NET da Raro Academy, sintetizando todo o conteúdo visto nas ultimas 10 semanas por meio de aulas síncronas na plataforma Zoom e monitorias diárias.  

Como motivação, nos foi apresentado a proposta de projeto pelo cliente Raro Labs, que pretende gradualmente parar de usar software de terceiros para realizar a gestão de horas que são destinadas a projetos pelos seus colaboradores.  

Para isso, se viu necessário a criação de uma solução que atendesse as expectativas do cliente.   
Essa solução se chama **Chronos** <sub>v1.0</sub>

# :hourglass: Design da Aplicação, Tecnologias e Abordagens utilizadas
O design escolhido para a arquitetura do software foi o Domain-Driven-Design (DDD). Essa modelagem consiste (de uma maneira bem abstrata) em camadas de design de dados, design estratégico e design tático, este ultimo somente funcionando se o design estratégico funcionar de maneira excepcional.  

Desse modo, o primeiro passo da modelagem do nosso sistema foi realizar a montagem do [Glossário](#hourglass-glossário) a ser utilizado ao longo da aplicação para estabelecer uma linguagem ubíqua(universal) que facilitasse a comunicação entre os desenvolvedores e os stakeholders.

Com o glossário em mãos, o próximo passo a ser realizado, ainda no design estratégico, foi traçar os [Contextos Delimitados](#hourglass-contextos-delimitados) da aplicação (Bounded Contexts) para após isso, engatinhar para a a modelagem.  

Para facilitar a construção da aplicação, realizamos inicialmente o [Diagrama de Entidade-Relacionamento](#diagrama-entidade-relacionamento) para começarmos a ter uma idéia do que precisariamos persistir no banco de dados.  

Feito isso, o próximo passo foi começar a pensar nas classes, interfaces, e contratos que provavelmente utilizariamos ao desenvolver o software. Nessa etapa, diversas mudanças foram feitas ao adentrar mais e mais no problema e entender o que realmente precisariamos realizar para alcançar o que nos era solicitado. Para isso, realizamos o [Diagrama de Classes](#diagrama-de-classes) para auxiliar nesse processo.  

O próximo processo foi realmente colocar "as mãos no código". Seguindo a arquitetura do DDD, as [Camadas da Aplicação](#hourglass-camadas-da-aplicação) foram executadas, testadas e documentadas, sempre seguindo também as boas práticas S.O.L.I.D para manter o código limpo, legível e escalável.

O projeto foi realizado utilizando a plataforma de desenvolvimento open source [.NET Core 6](https://learn.microsoft.com/pt-br/dotnet/fundamentals/).

A solução de banco de dados relacional utilizada foi o [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

A comunicação com o serviço Toggl foi feita por meio da integração com o [Toggl Api](https://github.com/toggl/toggl_api_docs/blob/master/reports/detailed.md).  

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
- Miro
- Draw.io

# :hourglass: Glossário
- Usuário: qualquer pessoa cadastrada na solução Chronos. Um usuário pode ser Colaborador e Administrador.
- Projeto: Representação do projeto na solução. Um Usuário participar de nenhum ou vários projetos. Um projeto pode ter nenhum ou vários usuários inseridos.
- Usuario_Projeto: Representação da relação entre projeto e usuário. Obrigatoriamente precisa ter um projeto e um usuário. Uma relação Usuario_Projeto pode ter zero ou várias tarefas.
- Tarefa: Representação das tarefas realizadas pelos usuários cadastrados na solução. Uma tarefa obrigatoriamente precisa ser vinculada a um e somente um Usuario_Projeto.
- ConfirmacaoToken: Token criado para a confirmação do e-mail do usuário.
- CodigoResetSenha: Código criado para o reset da senha de um usuário.
- Relatório de Horas: Representação de uma lista de tarefas, contendo total de horas, descrição, data inicial, data final da tarefa e nome do projeto.
- Integração com Toggl: comunicação com o serviço externo Toggl, que era responsável pelo registro total das horas trabalhadas dos colaboradores Raro.

# :hourglass: Contextos Delimitados
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
- Toggl
    - Um usuário administrador pode solicitar a visualização e importação dos dados da plataforma de terceiros Toggl. Para realizar a importação para o banco de dados, o usuário responsável pela tarefa precisa já estar cadastrado no Chronos. Apenas administradores podem solicitar a importação dos dados para banco de dados.
- Foi realizado uma [Modelagem](https://miro.com/app/board/uXjVP_4nbFs=/) no Miro, para melhor visualização dos contextos e suas interações com outros contextos.
# :hourglass: Modelagens
## Diagrama Entidade-Relacionamento
![DER](/ReadMeAssets/chronos_der.drawio.png)
- Um usuário pode ter zero ou vários (n-n) Projetos
- Um Projeto pode ter zero ou vários(n-n) Usuários
    - Tabela Auxiliar de Many-to-Many: Usuario_Projeto
    - Um Usuario_Projeto tem um e apenas um (1-1) usuário e projeto.
    - Um Usuario_Projeto pode ter zero ou várias (0-n) Tarefas.
- Uma tarefa pertence a um e somente um Usuario_Projeto(1-1) (consequentemente a Tarefa está relacionada ao mesmo tempo com Usuário e Projeto)
## Diagrama de Classes
- Para facilitar a visualização, acesse o [link para Draw.io](https://app.diagrams.net/?libs=general;uml#Agloinho%2Fexercicio-semana-4%2Fmain%2Fchronos_class.drawio)
# :hourglass: Camadas da Aplicação
## Chronos.Api
- Camada responsável por fazer a aplicação se comunicar diretamente com o domínio. Nela, são construídas as classes que serão necessárias para a aplicação. A construção dessas classes, feitas por meio de injeção de dependencias com o intuito de reduzir o acomplamento da aplicação, é realizado com o auxílio da camada de inversão de controle.  

**Handlers**
- AuthorizationHandler.cs &rarr; classe de middleware, responsável por intervir durante o acesso a aplicação e retornar uma excessão personalizada no caso de acesso não permitido. 

**Filters**
- ExceptionFilter.cs &rarr; classe de filtro de excessão personalizada.  

**Controllers**
- BaseController.cs &rarr; classe de base em que outros controllers se especificam.
- [AutenticacaoController.cs](#autenticação-controller) &rarr; definição dos endpoints de autenticação de um usuário.
- [TarefaController.cs](#tarefa-controller) &rarr; definição dos endpoints para interação com as tarefas de um usuário.
- [TogglController.cs](#toggl-controller) &rarr; definição dos endpoints para integração com a API do Toggl.
- [UsuarioController.cs](#usuario-controller)&rarr; definição dos endpoints para interação com o contexto de usuário.

## Chronos.Crosscutting
- Camada que realiza a distribuição de responsabilidades transversais (métodos comuns). Essa camada "cruza" toda a hierarquia de camadas, provendo a configuração dos serviços necessários para as classes funcionarem e estabelecendo o tempo de vida desses serviços.

**DependencyInjection**
- ConfigureMappers.cs &rarr; configuração da injeção de dependência dos serviços de mapeamentos de Entidade para Contracts (entity &harr; request/response)
- ConfigureRepository.cs &rarr; configuração da injeção de dependência do contexto de banco de dados e repositórios.
- ConfigureServices.cs &rarr; configuração da injeção de dependência das classes de services da aplicação.
## Chronos.IoC
- Camada que se comunica diretamente e somente com o CrossCutting, e é utilizada na camada de aplicação, que é responsável por registrar as dependencias necessárias para o funcionamento dos serviços do aplicativo.
- NativeInjectorBootstraper.cs &rarr; classe responsável por registrar as dependencias do aplicativo (mappers, services e repositories).
## Chronos.Data
- Camada responsável por se comunicar diretamente com o banco de dados. Aqui são definidas as entidades da aplicação, que posteriormente irão ser persistidas no banco de dados. É realizado todo o "mapeamento" de configuração dessas entidades para estabelecimento correto de relacionamentos e outros constraints do SQL. Por meio do EF Core Code First, são feitas migrações para o banco de dados e a construção das Tables, com tudo previamente configurado.
Também são construidas as classes que irão interagir com o banco de dados, na qual possuem métodos que realizam o CRUD necessário para a aplicação.

**Context**
- ApplicationDbContext.cs &rarr; classe que define o contexto a ser utilizado para comunicação e transações com o banco de dados.  

**Mappings**
- ProjetoMap.cs &rarr; classe de configuração da entidade Projeto, definindo suas constraints e relacionamentos.
- TarefaMap.cs &rarr; classe de configuração da entidade Tarefa definindo suas constraints e relacionamentos.
- Usuario_ProjetoMap.cs &rarr; classe de configuração da entidade Usuario_Projeto definindo suas constraints e relacionamentos.
- UsuarioMap.cs &rarr; classe de configuração da entidade Usuario definindo suas constraints e relacionamentos.  

**Repositories**
- BaseRepository.cs &rarr; classe que define os métodos base para comunicação com o banco de dados.
- LogRepository.cs &rarr; classe que herda de base repository e interage com a tabela de Log.
- ProjetoRepository.cs &rarr; classe que herda de base repository e interage com a tabela de Projeto.
- TarefaRepository.cs &rarr; classe que herda de base repository e interage com a tabela de Tarefa.
- Usuario_ProjetoRepository.cs &rarr; classe que herda de base repository e interage com a tabela de Usuario_Projeto.
- UsuarioRepository.cs &rarr; classe que herda de base repository e interage com a tabela de Usuario.
## Chronos.Domain
- Camada que delimita o "coração" do nosso negócio. Aqui, são construidos, com auxilio da linguagem ubíqua, contextos e modelagens realizados anteriormente, as interfaces, contratos de transferência de dados e entidades que serão utilizados em toda a aplicação. Além disso, classes auxiliares e utilitárias, excessões personalizadas e configurações são definidas nessa camada.

**Contracts**
- ColaboradoresRequest.cs &rarr; define as informações a serem requisitadas no ProjetoController.cs para adicionar ou remover colaboradores de um projeto.
- LoginRequest.cs &rarr; define as informações a serem requisitadas no AutenticacaoController.cs para realizar o login de um usuário.
- NovaSenhaRequest.cs &rarr; define as informações a serem requisitadas no UsuarioController.cs para realizar o processo de reset de senha de um usuário.
- ProjetoRequest.cs &rarr; define as informações a serem requisitadas no ProjetoController.cs para realizar a interação(criação, alteração) para com a entidade de Projeto.
- ResetSenhaRequest.cs &rarr; define as informações a serem requisitadas no UsuarioController.cs para mudar a senha de um usuário. 
- TarefaRequest.cs &rarr; define as informações a serem requisitadas no TarefaController.cs para realizar a interação(criação, alteração) para com a entidade de Tarefa.
- TarefaStartRequest.cs &rarr; define as informações a serem requisitadas no TarefaController.cs para realizar o inicio de uma tarefa.
- TarefaStopRequest.cs &rarr; define as informações a serem requisitadas no TarefaController.cs para realizar a finalização de uma tarefa.
- TogglDetailedRequest.cs  &rarr; define as informações a serem requisitadas no TogglController.cs para realizar a integração com a API do Toggl e recuperar as informações de projetos e tarefas dos usuários cadastrados na plataforma Toggl.
- UsuarioRequest.cs &rarr; define as informações a serem requisitadas no UsuarioController.cs para realizar a interação(criação, alteração) para com a entidade de Usuario.
- MessageResponse.cs &rarr; classe que define o padrão de mensagem que será entregue quando uma solicitação for feita, com código de status, descrição da mensagem, várias mensagens e detalhes da requisição.
- ProjetoResponse.cs &rarr; define as informações que serão entregues como resposta a uma solicitação que se relacione com a entidade Projeto.
- TarefaResponse.cs &rarr; define as informações que serão entregues como resposta a uma solicitação que se relacione com a entidade Tarefa.
- TogglDetailedResponse.cs &rarr; define as informações que serão entregues como resposta a uma solicitação a API do Toggl.
- Usuario_ProjetoResponse.cs &rarr; define as informações que serão entregues como resposta a uma solicitação que se relacione com a entidade de Tarefa, visto que um usuario_projeto possui diversas tarefas.
- UsuarioResponse.cs &rarr; define as informações que serão entregues como resposta a uma solicitação que se relacione com a entidade Usuario.

**Entities**  
- BaseEntity.cs &rarr; classe base que define as propriedades(colunas) que serão persistidas no banco de dados.
- Log.cs &rarr; classe que define a tabela Log e suas propriedades que serão persistidas no banco de dados. Não herda de BaseEntity.
- Projeto.cs &rarr; classe que define a tabela Projeto e suas propriedades que serão persistidas no banco de dados e suas propriedades de referência.
- Tarefa.cs &rarr; classe que define a tabela Tarefa e suas propriedades que serão persistidas no banco de dados e suas propriedades de referência.
- Usuario.cs &rarr; classe que define a tabela Usuario e suas propriedades que serão persistidas no banco de dados e suas propriedades de referência.
- Usuario_Projeto.cs &rarr; classe que define a tabela Usuario_Projeto e suas propriedades que serão persistidas no banco de dados e suas propriedades de referência.

**Exceptions**
- BaseException.cs &rarr; classe que define a excessão personalizada ao ser utilizada pelo filtro de excessão quando uma requisição não atender os requisitos necessários para ser processada, ou outros erros ocorrerem.
- StatusException.cs &rarr; enum que define o tipo de status a ser entregue por uma MessageResponse.

**Interfaces** 
- Repository &rarr; definem os métodos a serem implementados pelas classes de repository.
- Sevices &rarr; definem os métodos a serem implementados pelas classes de services.  
<sub>para mais informações: [Diagrama De Classes](#diagrama-de-classes)</sub>

**Settings**
- AppSettings.cs &rarr; classe que resgata a securityKey da aplicação para ser utilizada em métodos de services.
- EmailSettings.cs &rarr; classe que resgata informações para serem utilizadas pelo serviço de email na comunicação com o servidor de email externo.
- TogglSettings.cs &rarr; classe que resgata informações sobre a conta toggl para serem utilizadas pelo serviço de integração com a API do toggl.

**Shared**
- Token.cs &rarr; classe que gera JsonWebToken, este que é utilizado nas requisições como bearer para a autenticação na plataforma.

## Chronos.Services
- Camada que aplica as regras de negócio impostas pelo cliente e implementa a lógica dos serviços que a aplicação irá prover. Nela, são definidas validações para a transferência de dados dos contratos e permissões personalizadas que serão utilizadas pelos controllers.

**Validators**
- ColaboradoresRequestValidator.cs &rarr; define regras de validação para as informações a serem recebidas no contract ColaboradoresRequest.
- NovaSenhaRequestValidator.cs &rarr; define regras de validação para as informações a serem recebidas no contract NovaSenhaRequest.
- ProjetoRequestValidator.cs &rarr; define regras de validação para as informações a serem recebidas no contract ProjetoRequest.
- TarefaRequestValidator.cs &rarr; define regras de validação para as informações a serem recebidas no contract TarefaRequest.
- UsuarioRequestValidator.cs &rarr; define regras de validação para as informações a serem recebidas no contract UsuarioRequest.
**Services**
- BaseService.cs &rarr; classe base utilizada apenas para compartilhar, no mesmo contexto HTTP, as claims de uma requisição e de um usuário.
- EmailService.cs &rarr; classe responsável por implementar os métodos contendo a lógica necessária para o funcionamento do serviço de comunicação com o servidor de email.
- LogService.cs &rarr; classe responsável por implementar o método de logar toda ação feita dentro da aplicação e salvar no banco de dados.
- ProjetoService &rarr; classe responsável por implementar todos os métodos e lógica de negócio no que concerne aos Projetos.
- TarefaService &rarr; classe responsável por implementar todos os métodos e lógica de negócio no que concerne as Tarefas.
- TogglService &rarr; classe responsável por implementar o método necessário para a integração com a API externa do Toggl.
- Usuario_ProjetoService.cs &rarr; classe que realiza a conexão entre um usuário e seus projetos e implementa métodos verificadores que auxiliam na lógica do TarefaService e ProjetoService, 
- UsuarioService.cs &rarr; classe que implementa os métodos a serem utilizados no contexto de um Usuario.
## Chronos.Testes
- Camada que realiza testes de todos os componentes da aplicação.

# :hourglass: Controllers

## Autenticação Controller
- [/api/autenticacao](./ReadMeAssets/endpoints.md#apiautenticacao-post) `POST` <sub>Ativa o e-mail do usuário.</sub>
- [/api/autenticacao/login](./ReadMeAssets/endpoints.md#apiautenticacaologin-post) `POST` <sub>Realiza o login do usuário.</sub>

## Projeto Controller
- [/api/projeto](./ReadMeAssets/endpoints.md#apiprojeto-post) `POST`<sub>Cadastra um projeto.</sub>
- [/api/projeto](./ReadMeAssets/endpoints.md#apiprojeto-get) `GET`<sub>Lista todos os projetos.</sub>
- [/api/projeto/{id}/colaboradores](./ReadMeAssets/endpoints.md#apiprojetoidcolaboradores-post) `POST`<sub>Adiciona colaboradores em um projeto.</sub>
- [/api/projeto/{id}/colaboradores](./ReadMeAssets/endpoints.md#apiprojetoidcolaboradores-delete) `POST`<sub>Remove colaboradores de um projeto.</sub>
- [/api/projeto/{id}](./ReadMeAssets/endpoints.md#apiprojetoid-get) `GET`<sub>Retorna um projeto pelo ID.</sub>
- [/api/projeto/{id}](./ReadMeAssets/endpoints.md#apiprojetoid-put) `PUT` <sub>Edita um projeto</sub>
- [/api/projeto/{id}](./ReadMeAssets/endpoints.md#apiprojetoid-delete) `DELETE` <sub>Remove um projeto.</sub>
- [/api/projeto/usuario/{usuarioId}](./ReadMeAssets/endpoints.md#apiprojetousuariousuarioid-get) `GET` <sub>Lista todos os projetos que um usuário participa.</sub>

## Usuario Controller 
- [/api/usuario/{id}](./ReadMeAssets/endpoints.md#apiusuarioid-patch)  `PATCH` <sub>Muda a permissão de um Usuario.</sub>
- [/api/usuario](./ReadMeAssets/endpoints.md#apiusuario-post)  `POST` <sub>Cadastra um usuário.</sub>
- [/api/usuario](./ReadMeAssets/endpoints.md#apiusuario-get) `GET` <sub>Lista todos os usuários.</sub>
- [/api/usuario/{id}](./ReadMeAssets/endpoints.md#apiusuarioid-delete) `DELETE` <sub>Remove um usuário.</sub>
- [/api/usuario/{id}](./ReadMeAssets/endpoints.md#apiusuarioid-get) `GET` <sub>Retorna um usuário pelo ID.</sub>
- [/api/usuario/{id}](./ReadMeAssets/endpoints.md#apiusuarioid-put) `PUT` <sub>Edita as informações de um usuário.</sub>
- [/api/usuario/senha](./ReadMeAssets/endpoints.md#apiusuariosenha-post) `POST` <sub>Envia o código para reset de senha para o e-mail.</sub>
- [/api/usuario/senha](./ReadMeAssets/endpoints.md#apiusuariosenha-put) `PUT` <sub>Reseta a senha de um usuário.</sub>

## Tarefa Controller
- [/api/tarefa](./ReadMeAssets/endpoints.md#apitarefa-post) `POST` <sub>Cadastra uma tarefa.</sub>
- [/api/tarefa](./ReadMeAssets/./ReadMeAssets/endpoints.md#apitarefa-get) `GET` <sub>Lista todas as tarefas.</sub>
- [/api/tarefa/{id}/start](./ReadMeAssets/endpoints.md#apitarefaidstart-patch) `PATCH` <sub>Inicia uma tarefa.</sub>
- [/api/tarefa/{id}/stop](./ReadMeAssets/endpoints.md#apitarefaidstop-patch) `PATCH` <sub>Para uma tarefa.</sub>
- [/api/tarefa/{id}](./ReadMeAssets/endpoints.md#apitarefaid-delete) `DELETE` <sub>Remove uma tarefa.</sub>
- [/api/tarefa/{id}](./ReadMeAssets/endpoints.md#apitarefaid-put) `PUT` <sub>Edita uma tarefa.</sub>
- [/api/tarefa/{id}](./ReadMeAssets/endpoints.md#apitarefa-get) `GET` <sub>Retorna uma tarefa pelo ID.</sub>
- [/api/tarefa/usuario/{usuarioId}](./ReadMeAssets/endpoints.md#apitarefausuariousuarioid-get) `GET` <sub>Retorna todas as tarefas de um usuário.</sub>
- [/api/tarefa/usuario/{usuarioId}/filter_by](./ReadMeAssets/endpoints.md#apitarefausuariousuarioidfilter_by-get) `GET` <sub>Retorna todas as tarefas de um usuário aplicando filtros de data.</sub>
- [/api/tarefa/projeto/{projetoId}](./ReadMeAssets/endpoints.md#apitarefaprojetoprojetoid-get) `GET` <sub>Lista todas as tarefas de um projeto.</sub>

## Toggl Controller  
- [/toggl/relatorio-de-horas](./ReadMeAssets/endpoints.md#togglrelatorio-de-horas-get) `GET` <sub>Endpoint de integração com o toggl.</sub>

# :hourglass: Features
- [x] Cadastro do usuário
- [x] Autenticação do e-mail de um usuário
- [x] Login de um usuário cadastrado
- [x] Resetar a senha de um usuário
- [x] Criar projetos
- [x] Adicionar e remover colaboradores de um projeto
- [x] Editar e remover projetos
- [x] Criar tarefas e associa-las a projetos
- [x] Iniciar e parar tarefas
- [x] Editar e excluir tarefas
- [x] Ver e importar tarefas e projetos a partir dos dados do Toggl

# :hourglass: Desafios e Proximos Passos
- [ ] Possibilitar a total integração da gestão de horas da raro, não dependendo do Toggl.
- [ ] Separação dos Projetos por Clientes
# :hourglass: Requirements
- [.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
# :hourglass: Get Started

```bash
# Clone este repositório:
git clone https://gitlab.com/gloinho/chronos.git
```
```bash 
# Entre na pasta da camada de aplicação do projeto.
cd Chronos.Api
```  
```bash
# Rode a aplicação.
dotnet run
# Observações: 
    # Usuário Administrador necessita ser previamente cadastrado no banco de dados.
``` 

# :hourglass: Agradecimentos
- Gostariamos de agradecer a todo suporte dado pela Raro Academy, aos instrutores que nos aguentaram e auxiliaram e aos monitores que estavam disponíveis para tirar nossas dúvidas.

# :hourglass: Desenvolvedores
![avatar](https://images.weserv.nl/?url=https://avatars.githubusercontent.com/u/99846614?v=4&h=150&w=150&fit=cover&mask=circle&maxage=7d)
![avatar](https://images.weserv.nl/?url=https://avatars.githubusercontent.com/u/58056134?v=4&h=150&w=150&fit=cover&mask=circle&maxage=7d)
![avatar](https://images.weserv.nl/?url=https://avatars.githubusercontent.com/u/119057135?v=4&h=150&w=150&fit=cover&mask=circle&maxage=7d)
![avatar](https://images.weserv.nl/?url=https://avatars.githubusercontent.com/u/113548967?v=4&h=150&w=150&fit=cover&mask=circle&maxage=7d)
![avatar](https://images.weserv.nl/?url=https://avatars.githubusercontent.com/u/119057187?v=4&h=150&w=150&fit=cover&mask=circle&maxage=7d)



