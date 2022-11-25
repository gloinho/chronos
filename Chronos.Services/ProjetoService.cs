using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class ProjetoService : BaseService<Projeto>, IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly ILogService _logService;
        private readonly IUsuario_ProjetoService _usuario_projetoService;
        private readonly ProjetoRequestValidator _validator = new();
        private readonly ColaboradoresRequestValidator _validatorColab = new();
        private readonly IMapper _mapper;

        public ProjetoService(
            IHttpContextAccessor httpContextAccessor,
            IProjetoRepository projetoRepository,
            ILogService logService,
            IMapper mapper,
            IUsuario_ProjetoService usuario_ProjetoService
        ) : base(httpContextAccessor, projetoRepository)
        {
            _projetoRepository = projetoRepository;
            _logService = logService;
            _mapper = mapper;
            _usuario_projetoService = usuario_ProjetoService;
        }

        public async Task<MensagemResponse> AdicionarColaboradores(
            int projetoId,
            ColaboradoresRequest request
        )
        {
            await CheckSeIdExiste(projetoId);
            await _validatorColab.ValidateAndThrowAsync(request);
            foreach (int usuarioId in request.Usuarios)
            {
                await _logService.LogAsync(nameof(ProjetoService), nameof(AdicionarColaboradores) + $" Id: {usuarioId}", projetoId);
                await _usuario_projetoService.CadastrarAsync(projetoId, usuarioId);
            }
            ;
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>
                {
                    $"O(s) usuario(s) foram adicionados/ativados no projeto de ID {projetoId} com sucesso"
                }
            };
        }

        public async Task<MensagemResponse> InativarColaboradores(
            int projetoId,
            ColaboradoresRequest request
        )
        {
            await CheckSeIdExiste(projetoId);
            await _validatorColab.ValidateAndThrowAsync(request);
            foreach (int usuarioId in request.Usuarios)
            {
                await _logService.LogAsync(nameof(ProjetoService), nameof(InativarColaboradores), projetoId);
                await _usuario_projetoService.InativarColaborador(projetoId, usuarioId);
            }
            return new MensagemResponse()
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>
                {
                    $"O(s) usuario(s) foram inativados no projeto de ID {projetoId} com sucesso"
                }
            };
        }

        public async Task<MensagemResponse> AlterarAsync(int id, ProjetoRequest request)
        {
            var projeto = await CheckSeIdExiste(id);
            await _validator.ValidateAndThrowAsync(request);

            await _logService.LogAsync(nameof(ProjetoService), nameof(AlterarAsync), id, UsuarioId);

            await _projetoRepository.AlterarAsync(_mapper.Map(request, projeto));
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Alterado com Sucesso" }
            };
        }

        public async Task<MensagemResponse> CadastrarAsync(ProjetoRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
            var projeto = _mapper.Map<Projeto>(request);

            await _logService.LogAsync(nameof(ProjetoService), nameof(CadastrarAsync), projeto.Id);

            await _projetoRepository.CadastrarAsync(projeto);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Cadastrado com Sucesso" },
                Detalhe = $"Id: {projeto.Id}"
            };
        }

        public async Task<MensagemResponse> DeletarAsync(int id)
        {
            var projeto = await CheckSeIdExiste(id);

            await _logService.LogAsync(nameof(TarefaService), nameof(DeletarAsync), id, UsuarioId);

            await _projetoRepository.DeletarAsync(projeto);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Deletado com Sucesso" }
            };
        }

        public async Task<ProjetoResponse> ObterPorIdAsync(int id)
        {
            var projeto = await CheckSeIdExiste(id);
            var result = _mapper.Map<ProjetoResponse>(projeto);
            return result;
        }

        public async Task<List<ProjetoResponse>> ObterPorUsuarioId(int usuarioId)
        {
            await _usuario_projetoService.CheckPermissao(usuarioId);
            var projetos = await _projetoRepository.ObterPorUsuarioIdAsync(usuarioId);

            return _mapper.Map<List<ProjetoResponse>>(projetos);
        }

        public async Task<List<ProjetoResponse>> ObterTodosAsync()
        {
            var projetos = await _projetoRepository.ObterTodosAsync();
            var projetosOrdenados = _mapper
                .Map<List<ProjetoResponse>>(projetos)
                .OrderBy(u => u.Nome)
                .ToList();
            return projetosOrdenados;
        }
    }
}
