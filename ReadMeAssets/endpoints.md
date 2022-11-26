# /api/autenticacao <span style="color:green">POST</span>
> Ativa o e-mail do usuário.
> Permissões: Allow Anonymous.

- Response:  
`MensagemResponse` com mensagem de sucesso de confirmação do e-mail.

# /api/autenticacao/login <span style="color:green">POST</span>
> Realiza o login do usuário. 
> Permissões: Allow Anonymous.

- Response:   
`MensagemResponse` contendo o JWT do usuário para autenticação.

# /api/projeto <span style="color:green">POST</span>
> Cadastra um projeto.
> Permissões: Administrador.

- Response:  
`MensagemResponse` com detalhe contendo o ID do projeto. 

# /api/projeto <span style="color:cyan">GET</span>
> Lista todos os projetos.
> Permissões : Administrador.

- Response:  
`List<ProjetoResponse>` 


# /api/projeto/{id}/colaboradores <span style="color:green">POST</span>
> Adiciona colaboradores em um projeto.
> Permissões: Administradores.

- Response:
`MensagemResponse`.

# /api/projeto/{id}/colaboradores <span style="color:red">DELETE</span>
> Inativa colaboradores em um projeto.
> Permissões: Administradores.

- Response:
`MensagemResponse`.
# /api/projeto/{id} <span style="color:cyan">GET</span>
> Retorna um projeto pelo ID.
> Permissões: Administradores e Colaboradores**
- **Colaboradores somente poderão ver projetos em que estão participando.

- Response:
`ProjetoResponse`

# /api/projeto/{id} <span style="color:coral">PUT</span>
> Edita um projeto
> Permissões: Administradores.

- Response:
`MensagemResponse`.

# /api/projeto/{id} <span style="color:red">DELETE</span>
> Exclui um projeto.  
> Permissões: Administradores.

- Response:
`MensagemResponse`.

# /api/projeto/usuario/{usuarioId} <span style="color:cyan">GET</span>
> Lista todos os projetos que um usuário participa.
> Permissões: Administradores, Colaboradores**
- **Colaboradores podem ver apenas seus projetos.

- Response:  
`List<ProjetoResponse>` 

# /api/usuario <span style="color:green">POST</span> 
> Cadastra um usuário.
> Permissões: Allow Anonymous.
 
- Response:
`MensagemResponse` com instruções para acesso da plataforma.


# /api/usuario <span style="color:cyan">GET</span>
> Lista todos os usuários. 
> Permissões: Administradores.

- Response:  
`List<UsuarioResponse>`

# /api/usuario/senha <span style="color:green">POST</span>
> Envia o código para reset de senha para o e-mail.
> Permissões: Allow Anonymous.

- Response:   
`MensagemResponse` com instruções para reset de senha.

# /api/usuario/senha <span style="color:coral">PUT</span>
> Reseta a senha de um usuário. 
> Permissões: Authorize com o token enviado para o email contendo o código e email.

- Response:   
`MensagemResponse`.

# /api/usuario/{id} <span style="color:cyan">GET</span>
> Retorna um usuário pelo ID.  
> Permissões: Administradores e Colaboradores**.
- Administrador pode ver todos os usuarios.
- **Colaborador só pode ver ele mesmo.

- Response:  
`UsuarioResponse`

# /api/usuario/{id} <span style="color:coral">PUT</span>
> Edita as informações de um usuário.  
> Permissões: Administradores e Colaboradores**.
- Administrador pode modificar todos os usuarios.
- Colaborador pode modificar somente ele.

- Response:  
`UsuarioResponse`

# /api/usuario/{id} <span style="color:red">DELETE</span>
> Remove um usuário. 
> Permissões: Administradores.
- Admin pode deletar todos os usuários.
- Ao deletar um usuário, todas as suas tarefas também são excluidas.

- Response:   
`MensagemResponse`.

# /api/tarefa/ <span style="color:green">POST</span>
> Cadastra uma tarefa. 
> Permissões: Administradores e Colaboradores.
- Usuario que está cadastrando a tarefa deve estar associado ao projeto.

- Response:   
`MensagemResponse`.

# /api/tarefa <span style="color:cyan">GET</span>
> Lista todas as tarefas.
> Permissões: Administradores.

- Response:   
`List<TarefaResponse>`.

# /api/tarefa/{id}/start <span style="color:aquamarine">PATCH</span>
> Inicia uma tarefa. 
> Permissões: Administradores e Colaboradores**.

- **Checa se a tarefa está vinculada ao usuario logado.

- Response:   
`MensagemResponse`.

# /api/tarefa/{id}/stop <span style="color:aquamarine">PATCH</span>
> Para uma tarefa.  
> Permissões: Administradores e Colaboradores**.

- **Checar se a tarefa está vinculada ao usuario logado

- Response:   
`MensagemResponse`.

# /api/tarefa/{id} <span style="color:red">DELETE</span>
> Remove uma tarefa.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem excluir apenas suas tarefas.

- Response:   
`MensagemResponse`.

# /api/tarefa/{id} <span style="color:coral">PUT</span>
> Edita uma tarefa.
> Permissões: Administradores e Colaboradores**.
- **Checar se o usuário logado é o mesmo dono da tarefa
- **Checar se o usuário logado faz parte do projeto em que ele pretende colocar a tarefa (se a edição for mudar o projeto da tarefa)
- **Checar se já passou dois dias (48 horas) que a tarefa foi cadastrada, senão não pode editar.

- Response:   
`MensagemResponse`.

# /api/tarefa/{id} <span style="color:cyan">GET</span>
> Retorna uma tarefa por ID.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem ver apenas suas tarefas.

- Response:  
`TarefaResponse`

# /api/tarefa/usuario/{usuarioId} <span style="color:cyan">GET</span>
> Retorna todas as tarefas de um usuário.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem ver apenas suas tarefas.

- Response:  
`List<TarefaResponse>`

# /api/tarefa/usuario/{usuarioId}/filter_by <span style="color:cyan">GET</span>
> Lista todas as tarefas de um usuário, aplicando filtros de datas
> Permissões: Administradores e Colaboradores**.
- **Colaboradores podem ver apenas suas tarefas.

- Response:
`List<TarefaResponse>`

# /api/tarefa/projeto/{projetoId} <span style="color:cyan">GET</span>
> Lista todas as tarefas de um projeto.  
> Permissões: Administradores.

- Response:  
`List<TarefaResponse>`

# /toggl/relatorio-de-horas <span style="color:cyan">GET</span>
> Endpoint de integração com o toggl. Lista todas os reports detalhados de acordo com o Id do Workspace, Toggl Token do usuário, filtros de dados e importa os dados que ainda não estão no banco de dados.
> Permissões: Administradores.

- Response:
`TogglDetailedResponse`
