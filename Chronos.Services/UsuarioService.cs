using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Entities.Enums;
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
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuario_ProjetoService _usuario_ProjetoService;
        private readonly ILogService _logService;
        private readonly IEmailService _emailService;
        private readonly UsuarioRequestValidator _validator = new();
        private readonly NovaSenhaRequestValidator _validatorNovaSenha = new();
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsuarioService(
            IUsuario_ProjetoService usuario_ProjetoService,
            IUsuarioRepository usuarioRepository,
            ILogService logService,
            IEmailService emailService,
            AppSettings appSettings,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor, usuarioRepository)
        {
            _usuario_ProjetoService = usuario_ProjetoService;
            _usuarioRepository = usuarioRepository;
            _logService = logService;
            _emailService = emailService;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<MensagemResponse> MudarPermissao(int id, Permissao permissao)
        {
            var usuario = await CheckSeIdExiste(id);

            switch(usuario.Permissao)
            {
                case Permissao.Administrador:
                    {
                        if(permissao == Permissao.Administrador)
                        {
                            throw new BaseException(StatusException.NaoProcessado, new List<string> { "Usuario ja é Administrador." });
                        }
                        usuario.Permissao = permissao;
                        break;
                    }
                case Permissao.Colaborador:
                    {
                        if (permissao == Permissao.Colaborador)
                        {
                            throw new BaseException(StatusException.NaoProcessado, new List<string> { "Usuario ja é colaborador." });
                        }
                        usuario.Permissao = permissao;
                        break;
                    }
            }

            await _usuarioRepository.AlterarAsync(usuario);

            return new MensagemResponse()
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Permissão alterada com sucesso"}
            };

        }

        public async Task<MensagemResponse> CadastrarAsync(UsuarioRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
            await CheckSeJaEstaCadastrado(request.Email);
            var user = _mapper.Map<Usuario>(request);
            var token = Token.GenerateTokenRequest(user, _appSettings.SecurityKey);
            user.ConfirmacaoToken = token;
            user.Senha = BCrypt.Net.BCrypt.HashPassword(
                user.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            var emailToken = GenerateCustomHtmlString.CreateEnvioTokenHtml(request.Nome, token);
            await _emailService.Send(
                request.Email,
                "Confirmação de Email Chronos",
                emailToken
            );
            await _usuarioRepository.CadastrarAsync(user);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>
                {
                    "Enviamos um token para seu email. Por favor, faça a confirmação."
                },
                Detalhe = $"Id: {user.Id}"
            };
        }

        public async Task<MensagemResponse> AlterarAsync(int id, UsuarioRequest request)
        {
            await _usuario_ProjetoService.CheckPermissao(id);
            var usuario = await CheckSeIdExiste(id);
            await _validator.ValidateAndThrowAsync(request);

            await _logService.LogAsync(nameof(UsuarioService), nameof(AlterarAsync), id, UsuarioId);

            request.Senha = BCrypt.Net.BCrypt.HashPassword(
                request.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );
            await _usuarioRepository.AlterarAsync(_mapper.Map(request, usuario));
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Alterado com Sucesso" }
            };
        }

        public async Task<MensagemResponse> DeletarAsync(int id)
        {
            var usuario = await CheckSeIdExiste(id);

            await _logService.LogAsync(nameof(UsuarioService), nameof(DeletarAsync), id, UsuarioId);

            await _usuarioRepository.DeletarAsync(usuario);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Deletado com Sucesso" }
            };
        }

        public async Task<UsuarioResponse> ObterPorIdAsync(int id)
        {
            await _usuario_ProjetoService.CheckPermissao(id);
            var usuario = await CheckSeIdExiste(id);
            var result = _mapper.Map<UsuarioResponse>(usuario);
            return result;
        }

        public async Task<List<UsuarioResponse>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            var usuariosOrdenados = _mapper
                .Map<List<UsuarioResponse>>(usuarios)
                .OrderBy(u => u.Nome)
                .ToList();
            return usuariosOrdenados;
        }

        public async Task<MensagemResponse> AlterarSenha(NovaSenhaRequest request)
        {
            var user = await _usuarioRepository.ObterAsync(u => u.Email == UsuarioEmail);

            await _validatorNovaSenha.ValidateAndThrowAsync(request);

            if (!BCrypt.Net.BCrypt.Verify(request.Codigo, user.CodigoSenhaToken))
            {
                throw new BaseException(StatusException.Erro, "Código incorreto.");
            }

            if (request.Senha != request.ConfirmacaoSenha)
            {
                throw new BaseException(
                    StatusException.FormatoIncorreto,
                    $"Senhas digitadas são diferentes."
                );
            }

            if (BCrypt.Net.BCrypt.Verify(request.Senha, user.Senha))
            {
                throw new BaseException(
                    StatusException.Erro,
                    "Nova senha deve ser diferente da senha atual"
                );
            }

            user.Senha = BCrypt.Net.BCrypt.HashPassword(
                request.Senha,
                BCrypt.Net.BCrypt.GenerateSalt()
            );

            await _usuarioRepository.AlterarAsync(user);

            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Senha alterada com sucesso." }
            };
        }

        public async Task<MensagemResponse> EnviarCodigoResetSenha(ResetSenhaRequest request)
        {
            var user = await CheckSeEmailExiste(request.Email);
            var codigo = Token.GenerateCodigo();
            var token = Token.GenerateTokenRequest(user, _appSettings.SecurityKey, codigo);

            user.CodigoSenhaToken = BCrypt.Net.BCrypt.HashPassword(
                codigo,
                BCrypt.Net.BCrypt.GenerateSalt()
            );

            await _usuarioRepository.AlterarAsync(user);

            var emailRecuperacao = GenerateCustomHtmlString.CreateEnvioResetarSenhaHtml(user.Nome, codigo, token);

            await _emailService.Send(
                request.Email,
                "Recuperação de Senha Chronos",
                emailRecuperacao
            );

            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>
                {
                    "Enviamos o código de alteração de senha para seu e-mail. Este código será válido por apenas 2 horas."
                }
            };
        }

        private async Task CheckSeJaEstaCadastrado(string email)
        {
            var user = await _usuarioRepository.ObterAsync(u => u.Email == email);
            if (user != null)
            {
                throw new BaseException(StatusException.Erro, "E-mail já cadastrado");
            }
        }

        private async Task<Usuario> CheckSeEmailExiste(string email)
        {
            var usuario = await _usuarioRepository.ObterAsync(u => u.Email == email);
            if (usuario == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"Usuário com o email {email} não cadastrado."
                );
            }
            return usuario;
        }


    }
}
