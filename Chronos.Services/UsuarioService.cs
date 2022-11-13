using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Domain.Shared;
using Chronos.Domain.Utils;
using Chronos.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;
        private readonly UsuarioRequestValidator validator = new UsuarioRequestValidator();
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IEmailService emailService,
            AppSettings appSettings,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<MensagemResponse> CadastrarAsync(UsuarioRequest request)
        {
            await validator.ValidateAndThrowAsync(request);
            await CheckSeJaEstaCadastrado(request.Email);
            var user = _mapper.Map<Usuario>(request);
            var token = Token.GenerateTokenRequest(user, _appSettings.SecurityKey);
            user.ConfirmacaoToken = token;
            user.Senha = BCrypt.Net.BCrypt.HashPassword(
                user.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            await _usuarioRepository.Cadastrar(user);
            await _emailService.Send(
                request.Email,
                "Confirmação de Email Chronos",
                $"Conclua a configuração da sua nova Conta Chronos com o token: {token}"
            );
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Enviamos um token para seu email. Por favor, faça a confirmação." }
            };
        }

        public async Task<MensagemResponse> AlterarAsync(int id, UsuarioRequest request)
        {
            var usuario = await CheckSeIdExiste(id);
            CheckPermissao(id);
            await validator.ValidateAndThrowAsync(request);
            usuario.DataAlteracao = DateTime.Now;
            await _usuarioRepository.Editar(_mapper.Map(request, usuario));
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Alterado com Sucesso" }
            };
        }

        public async Task<MensagemResponse> DeletarAsync(int id)
        {
            var usuario = await CheckSeIdExiste(id);
            await _usuarioRepository.Excluir(usuario);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Deletado com Sucesso" }
            };
        }

        public async Task<UsuarioResponse> ObterPorIdAsync(int id)
        {
            var usuario = await CheckSeIdExiste(id);
            CheckPermissao(id);
            var result = _mapper.Map<UsuarioResponse>(usuario);
            return result;
        }

        public async Task<List<UsuarioResponse>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.Listar();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        private async Task CheckSeJaEstaCadastrado(string email)
        {
            var user = await _usuarioRepository.GetPorEmail(email);
            if (user != null)
            {
                throw new BaseException(StatusException.Erro, "E-mail já cadastrado");
            }
        }

        private async Task<Usuario> CheckSeIdExiste(int id)
        {
            var usuario = await _usuarioRepository.GetPorId(id);
            if (usuario == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, $"Usuário com o id {id} não cadastrado.");
            }
            return usuario;
        }

        private void CheckPermissao(int id)
        {
            if (UsuarioPermissao == PermissaoUtil.PermissaoColaborador && id != UsuarioId)
            {
                throw new BaseException(StatusException.NaoAutorizado, $"Colaborador não pode acessar.");
            }
        }
    }
}
