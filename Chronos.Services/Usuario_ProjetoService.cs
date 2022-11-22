﻿using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class Usuario_ProjetoService : BaseService, IUsuario_ProjetoService
    {
        private readonly IUsuario_ProjetoRepository _usuario_ProjetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;

        public Usuario_ProjetoService(
            IUsuario_ProjetoRepository usuario_ProjetoRepository,
            IUsuarioRepository usuarioRepository,
            IProjetoRepository projetoRepository,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor)
        {
            _usuario_ProjetoRepository = usuario_ProjetoRepository;
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
        }

        public async Task CadastrarAsync(Usuario_Projeto relacao)
        {
            await CheckSeUsuarioExiste(relacao.UsuarioId);
            await CheckSeRelacaoJaExiste(relacao);
            await _usuario_ProjetoRepository.CadastrarAsync(relacao);
        }

        public async Task<Usuario_Projeto> CheckSeUsuarioFazParteDoProjeto(
            int projetoId,
            int? usuarioId
        )
        {
            var relacao = await _usuario_ProjetoRepository.ObterAsync(
                p => p.ProjetoId == projetoId && p.UsuarioId == usuarioId
            );
            if (relacao == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    "Usuário não faz parte do projeto."
                );
            }
            return relacao;
        }

        public async Task CheckPermissao(int usuario_projetoId)
        {
            var relacao = await _usuario_ProjetoRepository.ObterPorIdAsync(usuario_projetoId);
            if (
                relacao.UsuarioId != UsuarioId
                && PermissaoUtil.PermissaoColaborador == UsuarioPermissao
            )
            {
                throw new BaseException(
                    StatusException.NaoAutorizado,
                    "Colaborador não pode interagir com tarefas de outros colaboradores."
                );
            }
        }

        public async Task<Usuario_Projeto> CheckSePodeAlterarTarefa(int projetoId, Tarefa tarefa)
        {
            // Primeiro checar se o projeto existe:
            await CheckSeProjetoExiste(projetoId);

            // Depois obter a relação pela tarefa:
            var usuario_projeto = await _usuario_ProjetoRepository.ObterPorIdAsync(
                tarefa.Usuario_ProjetoId
            );

            // Se o usuário logado for admin, precisa checar se o dono da tarefa em que ele quer mudar faz parte do projeto em que ele quer colocar a tarefa
            if (UsuarioPermissao == PermissaoUtil.PermissaoAdministrador)
            {
                var relacao = await CheckSeUsuarioFazParteDoProjeto(
                    projetoId,
                    usuario_projeto.UsuarioId
                );
                return relacao;
            }
            // Se o usuário logado for colaborador preciso checar SE o ID do usuário LOGADO é igual ao ID do usuário que existe no Usuario_Projeto da TAREFA.

            if (UsuarioId != usuario_projeto.UsuarioId)
            {
                throw new BaseException(
                    StatusException.NaoAutorizado,
                    "Não é possivel alterar tarefas de outros usuários."
                );
            }
            return usuario_projeto;
        }

        public async Task CheckSeUsuarioExiste(int usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"O usuario de ID {usuarioId} não foi encontrado."
                );
            }
            ;
        }

        public async Task CheckSeProjetoExiste(int projetoId)
        {
            var projeto = await _projetoRepository.ObterPorIdAsync(projetoId);
            if (projeto == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"O projeto de ID {projetoId} não foi encontrado."
                );
            }
        }

        private async Task CheckSeRelacaoJaExiste(Usuario_Projeto relacao)
        {
            var find = await _usuario_ProjetoRepository.ObterAsync(
                p => p.ProjetoId == relacao.ProjetoId && p.UsuarioId == relacao.UsuarioId
            );
            if (find != null)
            {
                throw new BaseException(
                    StatusException.Erro,
                    $"O usuario de id {relacao.UsuarioId} já faz parte do projeto {relacao.ProjetoId}"
                );
            }
        }
    }
}
