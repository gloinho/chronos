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
    public class TarefaService : BaseService, ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuario_ProjetoService _usuario_ProjetoService;
        private readonly IMapper _mapper;
        private readonly TarefaRequestValidator _validator = new TarefaRequestValidator();

        public TarefaService(
            IHttpContextAccessor httpContextAccessor,
            IUsuario_ProjetoService usuario_ProjetoService,
            ITarefaRepository tarefaRepository,
            IMapper mapper
        ) : base(httpContextAccessor)
        {
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
            _usuario_ProjetoService = usuario_ProjetoService;
        }

        public async Task<MensagemResponse> AlterarAsync(int id, TarefaRequest request)
        {
            var tarefa = await CheckSeIdExiste(id); // checar se tarefa existe
            CheckDataDeInclusao(tarefa); // verificação tanto pra admin quanto pra colab se tarefa tiver +2 dias inclusa.
            // se o usuario logador for colaborador:
            /// só pode alterar tarefa em que é dono
            /// só pode alterar projeto em que faz parte
            var usuario_projeto = await _usuario_ProjetoService.CheckSePodeAlterarTarefa(
                request.ProjetoId,
                tarefa
            );
            var editada = _mapper.Map(request, tarefa);
            editada.Usuario_ProjetoId = usuario_projeto.Id;
            editada.DataAlteracao = DateTime.Now;
            await _tarefaRepository.AlterarAsync(editada);
            return new MensagemResponse
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Alterado com Sucesso" }
            };
        }

        public async Task<MensagemResponse> CadastrarAsync(TarefaRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
            var usuario_projeto = await _usuario_ProjetoService.CheckSeUsuarioFazParteDoProjeto(
                request.ProjetoId
            );
            var tarefa = _mapper.Map<Tarefa>(request);
            tarefa.Usuario_ProjetoId = usuario_projeto.Id;
            await _tarefaRepository.CadastrarAsync(tarefa);
            return new MensagemResponse()
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string> { "Tarefa cadastrada com sucesso." }
            };
        }

        public async Task<MensagemResponse> DeletarAsync(int id)
        {
            var tarefa = await CheckSeIdExiste(id);
            await _usuario_ProjetoService.CheckPermissao(tarefa.Usuario_ProjetoId);
            await _tarefaRepository.DeletarAsync(tarefa);
            return new MensagemResponse()
            {
                Codigo = StatusException.Nenhum,
                Mensagens = new List<string>() { "Tarefa deletada com sucesso." }
            };
        }

        public Task<TarefaResponse> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TarefaResponse>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Tarefa> CheckSeIdExiste(int id)
        {
            var tarefa = await _tarefaRepository.ObterPorIdAsync(id);
            if (tarefa == null)
            {
                throw new BaseException(
                    StatusException.NaoEncontrado,
                    $"Tarefa com o id {id} não cadastrado."
                );
            }
            return tarefa;
        }

        private void CheckDataDeInclusao(Tarefa tarefa)
        {
            if (tarefa.DataInclusao.AddDays(2) < DateTime.Now)
            {
                throw new BaseException(
                    StatusException.NaoProcessado,
                    $"O tempo de editar a tarefa expirou em {tarefa.DataInclusao.AddDays(2)}"
                );
            }
        }
    }
}
