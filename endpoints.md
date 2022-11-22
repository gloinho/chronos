# /api/autenticacao `POST`
> Ativa o e-mail do usuário.
> Permissões: Allow Anonymous.

- Response:  
`Mensagem de confirmação.`

# /api/autenticacao/login `POST`
> Realiza o login do usuário. 
> Permissões: Allow Anonymous.

- Response:   
`Jwt token com claims.` 

# /api/projeto `POST`
> Cadastra um projeto.
> Permissão: Administrador.

- Response
`Projeto Response` 

# /api/projeto `GET`
> Lista todos os projetos.
> Permissão : Administrador.

# /api/projeto/{id}/colaboradores `POST`
> Adiciona colaboradores em um projeto.
> Permissão: Administradores.

# /api/projeto/{id} `GET`
> Retorna um projeto pelo ID.
> Permissão: Administradores e Colaboradores**
- **Colaboradores somente poderão ver projetos em que estão participando.

# /api/projeto/{id} `PUT`
> Edita um projeto
> Permissão: Administradores.

# /api/projeto/{id} `DELETE`
> Exclui um projeto.  
> Permissões: Administradores.

# /api/projeto/usuario/{usuarioId} `GET`
> Lista todos os projetos que um usuário participa.
> Permissões: Administradores, Colaboradores**
- **Colaboradores podem ver apenas seus projetos.

# /api/usuario `POST` 
> Cadastra um usuário.
> Permissões: Allow Anonymous.
- Response:  
`Confirme sua conta clicando no link enviado para seu e-mail.`


# /api/usuario `GET`
> Lista todos os usuários. 
> Permissões: Administradores.

- Response:  
`List<UsuarioResponse>`

# /api/usuario/senha `POST`
> Envia o código para reset de senha para o e-mail.
> Permissões: Allow Anonymous.

- Response:   
`Mensagem dizendo que foi enviado para o email o código para resetar senha.`

# /api/usuario/senha `PUT`
> Reseta a senha de um usuário. 
> Permissões: Authorize com o token enviado para o email contendo o código e email.

- Response:  
`Mensagem de confirmação se alterou a senha.`

# /api/usuario/{id} `GET`
> Retorna um usuário pelo ID.  
> Permissões: Administradores e Colaboradores**.
- Administrador pode ver todos os usuarios.
- **Colaborador só pode ver ele mesmo.
- Response:  
`UsuarioResponse`

# /api/usuario/{id} `PUT`
> Edita as informações de um usuário.  
> Permissões: Administradores e Colaboradores**.
- Administrador pode modificar todos os usuarios.
- Colaborador pode modificar somente ele.
- Response:  
`UsuarioResponse`

# /api/usuario/{id} `DELETE`
> Remove um usuário. 
> Permissões: Administradores.
- Admin pode deletar todos os usuários.
- Ao deletar um usuário, todas as suas tarefas também são excluidas.

- Response:  
`Mensagem de sucesso ou erro`

# /api/tarefa/ `POST`
> Cadastra uma tarefa. 
> Permissões: Administradores e Colaboradores.
- Usuario que está cadastrando a tarefa deve estar associado ao projeto.
- Response:  
`Mensagem para dizer que tarefa foi cadastrada`

# /api/tarefa `GET`
> Lista todas as tarefas.
> Permissões: Administradores.

# /api/tarefa/{id}/start `PATCH`
> Inicia uma tarefa. 
> Permissões: Administradores e Colaboradores**.

- **Checa se a tarefa está vinculada ao usuario logado.

- Response:
`TarefaResponse`

# /api/tarefa/{id}/stop `PATCH`
> Para uma tarefa.  
> Permissões: Administradores e Colaboradores**.

- **Checar se a tarefa está vinculada ao usuario logado

- Response:
`Tarefa response`

# /api/tarefa/{id} `DELETE`
> Remove uma tarefa.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem excluir apenas suas tarefas.
- Response:  
`Mensagem de sucesso`

# /api/tarefa/{id} `PUT`
> Edita uma tarefa.
> Permissões: Administradores e Colaboradores**.
- **Checar se o usuário logado é o mesmo dono da tarefa
- **Checar se o usuário logado faz parte do projeto em que ele pretende colocar a tarefa (se a edição for mudar o projeto da tarefa)
- **Checar se já passou dois dias (48 horas) que a tarefa foi cadastrada, senão não pode editar.

- Response:  
`TarefaResponse`

# /api/tarefa/{id} `GET`
> Retorna uma tarefa por ID.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem ver apenas suas tarefas.
- Response:  
`TarefaResponse`

# /api/tarefa/usuario/{usuarioId} `GET`
> Retorna todas as tarefas de um usuário.
> Permissões: Administradores e Colaboradores**
- **Colaboradores podem ver apenas suas tarefas.
- Response:  
`List<TarefaResponse>`

# /api/tarefa/{usuarioId}/dia `GET`
> Lista todas as tarefas que iniciaram e terminaram no dia.
> Permissões: Administradores e Colaboradores**.

- Retorna apenas as tarefas do dia vinculadas ao usuario
- Response:
`List TarefaResponse`

# /api/tarefa/{usuarioId}/mes `GET`
> Lista todas as tarefas que iniciaram e terminaram no mês.
> Permissões: Administradores e Colaboradores**.
- Retorna apenas as tarefas do mes vinculadas ao usuario

- Response:
`List TarefaResponse`

# /api/tarefa/{usuarioId}/semana `GET`
> Lista todas as tarefas que iniciaram e terminaram na semana.
> Permissões: Administradores e Colaboradores**.

- Retorna apenas as tarefas do dia vinculadas ao usuario
- Response:
`List TarefaResponse`




# /api/tarefa/projeto/{projetoId} `GET`
> Lista todas as tarefas de um projeto.  
> Permissões: Administradores.
- Response:  
`List<TarefaResponse>`

# /api/tarefa/{usuarioId}/{datainicial}{datafinal}
//TODO
> Retorna todas as tarefas entre as datas.  
> Permissões: Administradores e Colaboradores**.

- Retorna apenas as tarefas vinculadas ao usuario

- Response:
`List TarefaResponse`