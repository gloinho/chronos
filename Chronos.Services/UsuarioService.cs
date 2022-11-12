using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Chronos.Domain.Shared;
using Chronos.Services.Validators;
using FluentValidation;

namespace Chronos.Services
{
    public class UsuarioService : IUsuarioService
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
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<Response> CadastrarUsuario(UsuarioRequest request)
        {
            validator.ValidateAndThrow(request);
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
            return new Response { Mensagem = "Enviamos um e-mail para confirmar sua conta." };
        }

        private async Task CheckSeJaEstaCadastrado(string email)
        {
            var user = await _usuarioRepository.GetPorEmail(email);
            if (user != null)
            {
                throw new Exception("Usuário com esse e-mail já está cadastrado.");
            }
        }
    }
}
