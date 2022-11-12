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
            var user = _mapper.Map<Usuario>(request);
            var token = Token.GenerateTokenRequest(user, _appSettings.SecurityKey);
            user.ConfirmacaoToken = token;
            await _usuarioRepository.Cadastrar(user);
            return new Response { Mensagem = "Enviamos um e-mail para confirmar sua conta." };
        }
    }
}
