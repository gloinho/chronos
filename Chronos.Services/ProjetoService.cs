using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Chronos.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Chronos.Services
{
    public class ProjetoService : BaseService, IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUsuario_ProjetoService _usuario_projetoService;
        private readonly ProjetoRequestValidator _validator = new ProjetoRequestValidator();
        private readonly AdicionarColaboradorRequestValidator _validatorColab =
            new AdicionarColaboradorRequestValidator();
        private readonly IMapper _mapper;

        public ProjetoService(
            IHttpContextAccessor httpContextAccessor,
            IProjetoRepository projetoRepository,
            IMapper mapper,
            IUsuario_ProjetoService usuario_ProjetoService
        ) : base(httpContextAccessor)
        {
            _projetoRepository = projetoRepository;
            _mapper = mapper;
            _usuario_projetoService = usuario_ProjetoService;
        }

        public async Task<MensagemResponse> AdicionarColaboradores(
            int projetoId,
            AdicionarColaboradoresRequest request
        )
        {
            await CheckSeIdExiste(projetoId);
            await _validatorColab.ValidateAndThrowAsync(request);
            foreach (int usuarioId in request.Usuarios)
            {
                await _usuario_projetoService.CadastrarAsync(
                    new Usuario_Projeto { ProjetoId = projetoId, UsuarioId = usuarioId }
                );
            }
            ;
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>
                {
                    $"O(s) usuario(s) foram adicionados ao projeto de ID {projetoId} com sucesso"
                }
            };
        }

        public async Task<MensagemResponse> AlterarAsync(int id, ProjetoRequest request)
        {
            var projeto = await CheckSeIdExiste(id);
            await _validator.ValidateAndThrowAsync(request);
            projeto.DataAlteracao = DateTime.Now;
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
            await CheckPermissao(usuarioId);
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

        private async Task<Projeto> CheckSeIdExiste(int id)
        {
            var projeto = await _projetoRepository.ObterPorIdAsync(id);
            if (projeto == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"Projeto com o id {id} não cadastrado."
                );
            }
            return projeto;
        }

        private async Task CheckPermissao(int usuarioId)
        {
            await _usuario_projetoService.CheckSeUsuarioExiste(usuarioId);
            if (UsuarioPermissao == PermissaoUtil.PermissaoColaborador && UsuarioId != usuarioId)
            {
                throw new BaseException(
                    StatusException.NaoAutorizado,
                    "Colaborador não pode interagir com projetos de outros colaboradores."
                );
            }
        }
    }
}
