# Endpoints:

## Autenticação Controller
- [/api/auth](#apiauth-post) `POST` <sub>loga um usuario</sub>
- [/api/auth/{token}](#apiauthtoken-put) `PUT` <sub>ativa um usuario</sub>

## Usuario Controller 
- [/api/usuarios](#apiusuarios-post)  `POST` <sub>criar um usuario</sub>
- [/api/usuarios](#apiusuarios-get) `GET` <sub>lista todos os usuarios</sub>
- [/api/usuarios/senhareset](#apiusuariossenhareset-post) `POST` <sub>envia um código para alteração da senha por email</sub>
- [/api/usuarios/senhareset/reset](#apiusuariossenharesetreset-post) `POST` <sub>envia o código e altera senha</sub>
- [/api/usuarios/{id}](#apiusuariosid-get) `GET` <sub>retorna um usuario</sub>
- [/api/usuarios/{id}](#apiusuariosid-put) `PUT` <sub>edita um usuario</sub>
- [/api/usuarios/{id}](#apiusuariosid-delete) `DELETE` <sub>deleta um usuario</sub>

## Tarefa Controller
- [/api/tarefas](#apitarefas-post) `POST` <sub>criar uma tarefa</sub>
- [/api/tarefas/{id}](#apitarefasid-put) `PUT` <sub>edita uma tarefa</sub>
- [/api/tarefas/{id}/start](#apitarefasidstart-post) `PUT` <sub>iniciar uma tarefa</sub>
- [/api/tarefas/{id}/stop](#apitarefasidstop-put) `PUT` <sub>parar uma tarefa</sub>
- [/api/tarefas/{usuarioId}/dia](#apitarefasusuarioiddia-get) `GET` <sub>lista tarefas do dia do usuario</sub>
- [/api/tarefas/{usuarioId}/semana](#apitarefasusuarioidsemana-get) `GET` <sub>lista tarefas da semana do usuario</sub>
- [/api/tarefas/{usuarioId}/mes](#apitarefasusuarioidmes-get) `GET` <sub>lista tarefas do mes do usuario</sub>
- [/api/tarefas/projetos/{projetoId}](#apitarefasprojetosprojetoid-get) `GET` <sub>lista as tarefas de um projeto</sub>

## Projeto Controller
- [/api/projeto](#apiprojeto-post) `POST`<sub>criar um projeto</sub>
- [/api/projeto/{id}](#apiprojetoid-patch) `PATCH` <sub>edita um projeto</sub>
- [/api/projeto/{id}](#apiprojetoid-delete) `DELETE` <sub>exclui um projeto</sub>

# /api/auth `POST`
> Loga um usuário.  
> Permissões: Allow Anonymous.
```csharp
public class LoginRequest
{
    [Required]
    public string Email {get; set;}
    [Required]
    public string Senha{get; set;}
}
```
- Confirmações:
    - Usuario cadastrado.
    - Email confirmado.

- Response:  
`JWT token com claims: id, permissao`

# api/auth/{token} `PUT`
> Confirmação da conta do usuário  
> Permissões: Allow Anonymous.
- Nesse endpoint, será realizada a busca do usuario a confirmar por meio do token

```csharp
public Task<bool> Confirmar(string token)
{
    var user = _context.Usuarios.FirstOrDefaultAsync(u => u.TokenConfirmacao == token)
    if(user == null)
    {
        throw new Exception()

    }
    user.Confirmado = true;
    _context.Update(user);
    _context.SaveChangesAsync();
    return true;
}
```
- Response:   
`Usuario confirmado com sucesso` ou algum erro pra pensar depois.

# api/usuarios `POST` 
> Cadastro de usuário.  
> Permissões: Allow Anonymous.
```csharp
public class UsuarioRequest
{
    [Required]
    public string Nome {get; set;}
    [Required]
    public string Email {get; set;}
    [Required]
    public string Senha{get; set;}
}
```
- Confirmações:
    - Nenhum null
    - Email unico
    - Senha 8+ caracteres, letra minuscula, maiuscula e simbolo
    - Nome 3+ caracteres
- Após confirmações será feito um token encriptado (pode seguir o estilo JWT token ou qualquer hash mesmo) que será colocado no path da url de confirmação
    - Exemplo: `https://localhost:5001/api/auth/{token}`
- Será feito a adição do token no usuário para a confirmação do mesmo.
    - // TODO Pensar em um meio de não pedir o token no `UsuarioRequest` 
        - // TODO pedir pra ignorar a prop no swagger ?
- // TODO seed de um primeiro usuário como ADMIN
- // TODO endpoint para mudar permissoes.

- Todos os usuarios cadastrados serão Colaboradores.
- Response:  
`Confirme sua conta clicando no link enviado para seu e-mail.`
- Dentro do email, terá um link para o endpoint [api/auth/{token}](#apiauthtoken-put)

# api/usuarios `GET`
> Retorna todos os usuários cadastrados na plataforma ORDEM ALFABETICA  
> Permissões: Administradores.

- Response:  
`List<UsuarioResponse>`

# /api/usuarios/senhareset `POST`
> Envia um código para o email do usuário para resetar a senha.  
> Permissões: Allow Anonymous.
```csharp
public class CodigoRecuperarSenhaRequest
{
    [Required]
    public string Email {get; set;}
}
```
- Provavelmente um token com um numero de 6 digitos ou um hash simples
- Response:   
`Mensagem para dizer que tem que acessar o email e botar o codigo`

# /api/usuarios/senhareset/reset `POST`
> Recebe o código, e altera a senha do usuário.  
> Permissões: Allow Anonymous.
```csharp
public class RecuperarSenhaRequest
{
    [Required]
    public int Codigo {get; set;}
    [Required]
    public string Senha {get; set;}
    [Required]
    public string ConfirmacaoSenha {get; set;}
}
```
- Checa se o código bate com alguem na database
- Confirma se nova senha == confirmação
- Altera a senha
- Response:  
`Mensagem se alterou senha`

# /api/usuarios/{id} `GET`
> Retorna um usuário por ID.  
> Permissões: Administradores e Colaboradores**.
- Admin pode ver todos os usuarios.
- **Colaborador só pode ver ele mesmo.
- Response:  
`UsuarioResponse`

# /api/usuarios/{id} `PUT`
> Modifica um usuario.  
> Permissões: Administradores e Colaboradores**.
- Admin pode modificar todos os usuarios.
- Colaborador pode modificar somente ele.
- Response:  
`UsuarioResponse`

# /api/usuarios/{id} `DELETE`
> Deleta um usuário.  
> Permissões: Administradores e Colaboradores**.
- Admin pode deletar todos os usuários.
- Colaborador só pode se deletar.
- Verificar se um colaborador está associado a uma tarefa? se deletar o usuario, setar como null? verificar isso.
- Response:  
`Mensagem de sucesso ou erro`

# /api/tarefas/ `POST`
> Cria uma nova tarefa.  
> Permissões: Administradores e Colaboradores.

```csharp
public class TarefaRequest
{
    [Required] 
    public string Descricao {get; set;}
    [Required]
    public int ProjetoId {get; set;}
}
```
- Confirmações
    - Descrição não nula
    - Projeto existe
    - Usuario que está startando a tarefa está associado ao projeto.
- No service, fazer associação de tarefa com Usuario_Projeto:
```csharp
var usuario_projeto = _usuario_projetoRepository.Where(up => up.UserId == UserId && up.ProjetoId == ProjetoId);
var tarefa = _mapper.Map<Tarefa>(request);
tarefa.Usuario_Projeto = usuario_projeto.Id;
_tarefarepository.AddAsync(tarefa)
```
# /api/tarefas/{id} `PUT`
> Edita uma tarefa.  
> Permissões: Administradores e Colaboradores**.
- **Checar se o usuário logado é o mesmo dono da tarefa
- **Checar se o usuário logado faz parte do projeto em que ele pretende colocar a tarefa (se a edição for mudar o projeto da tarefa)
- **Checar se já passou dois dias (48 horas) que a tarefa foi cadastrada, senão não pode editar.

- Response:  
`TarefaResponse`

# /api/tarefas/{id}/start `POST`
> Inicia o cronometro de uma tarefa.  
> Permissões: Administradores e Colaboradores**.

```csharp
public class TarefaStartRequest
{
    [Required] 
    public int TarefaId {get; set;}
}
```
- Confirmações
    - Checar se a tarefa existe
    - Checar se a tarefa ja está finalizada
    - **Checar se a tarefa está vinculada ao usuario logado

```csharp
var tarefa = _tarefaRepository.Find(id);
tarefa.DataInicial = DateTime.Now
_tarefaRepo.save(tarefa);
```

- Response:
`TarefaResponse`

# api/tarefas/{id}/stop `PUT`
> Para o cronometro de uma tarefa.  
> Permissões: Administradores e Colaboradores**.

```csharp
public class TarefaStopResquest
{
    [Required] 
    public int TarefaId {get; set;}
}
```
- Confirmações
    - Checar se a tarefa existe
    - Checar se a tarefa ja está finalizada
    - **Checar se a tarefa está vinculada ao usuario logado

```csharp
var tarefa = _tarefaRepository.Find(id);
tarefa.DataFinal = DateTime.Now
_tarefaRepo.save(tarefa);
```

- Response:
`Tarefa response`

# api/tarefas/{usuarioId}/{datainicial}{datafinal}
> Retorna todas as tarefas entre as datas.  
> Permissões: Administradores e Colaboradores**.
- Chama o service e aplica o filtro de dias
- **Retorna apenas as tarefas vinculadas ao usuario
```csharp
// NO REPOSITORIO
public Task<Tarefa> GetTarefasDoDia()
{
    _context.Tarefa.Where(p => datainicial < p.DataInclusao && p.DataInclusao < datafinal)
    .Include(p => p.Usuario_Projeto)
    .Include(p => p.Projeto)
    .Select(p => p.Nome)
    .ToListAsync();
}
```
- Response:
`List TarefaResponse`

# api/tarefas/{usuarioId}/dia `GET`
> Retorna todas as tarefas do dia.  
> Permissões: Administradores e Colaboradores**.
- **Retorna apenas as tarefas do dia vinculadas ao usuario

```csharp
// NO REPOSITORIO
public Task<Tarefa> GetTarefasDoDia()
{
    _context.Tarefa.Where(p => p.DataDeInclusao == DateTime.Today)
    .Include(p => p.Usuario_Projeto)
    .Include(p => p.Projeto)
    .Select(p => p.Nome)
    .ToListAsync();
}
```

- Response:
`List TarefaResponse`

# api/tarefas/{usuarioId}/semana `GET`
> Retorna todas as tarefas da semana.  
> Permissões: Administradores e Colaboradores**.
- **Retorna apenas as tarefas do dia vinculadas ao usuario
```csharp
// NO REPOSITORIO
public Task<Tarefa> GetTarefasDaSemana()
{
    //TODO como fazer?
    _context.Tarefa.Where(p => DateTime.Today < p.DataDeInclusao )
    .Include(p => p.Usuario_Projeto)
    .Include(p => p.Projeto)
    .Select(p => p.Nome)
    .ToListAsync();
}
```

- Response:
`List TarefaResponse`


# api/tarefas/{usuarioId}/mes `GET`
> Retorna todas as tarefas do mes.  
> Permissões: Administradores e Colaboradores**.
- **Retorna apenas as tarefas do mes vinculadas ao usuario
```csharp
// NO REPOSITORIO
public Task<Tarefa> GetTarefasDoMes()
{
    _context.Tarefa.Where(p => p.DataDeInclusao.Month == DateTime.Today.Month)
    .Include(p => p.Usuario_Projeto)
    .Include(p => p.Projeto)
    .Select(p => p.Nome)
    .ToListAsync();
}
```

- Response:
`List TarefaResponse`

# api/tarefas/projetos/{projetoId} `GET`
> Retorna as tarefas de um projeto.  
> Permissões: Administradores.
- Response:  
`List<TarefaResponse>`

# api/projeto `POST`
> Cria um novo projeto
- Apenas administradores poderão criar projetos.

```csharp
public class ProjetoRequest
{
    [Required]
    public string Nome {get; set;}
    public ICollection<int> UsuariosId {get; set;}
}

```

- Confirmações:
    - Usuarios existem
    - Nome não nulo
    - Usuarios podem ser nulos, porque ai bota uma endpoint para adicionar usuarios no projeto
- Associa many to many no service:
```csharp
foreach(var user in UsuariosID)
{
    _repository.Usuario_Projeto.AddAsync(new Usuario_Projeto(idProjeto, user));
}

```
- Response
`Projeto Response` 

# api/projeto/{id} `PATCH`
> Edita um projeto.  
> Permissões: Administradores.

# api/projeto/{id} `DELETE`
> Exclui um projeto.  
> Permissões: Administradores.

