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
            var tarefa = await CheckSeIdExiste(id);
            CheckDataDeInclusao(tarefa);
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

        public async Task<TarefaResponse> ObterPorIdAsync(int id)
        {
            var tarefa = await CheckSeIdExiste(id);
            await _usuario_ProjetoService.CheckPermissao(tarefa.Usuario_ProjetoId);
            var response = ObterHorasTotais(tarefa);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterPorUsuarioId(int usuarioId)
        {
            await CheckPermissao(usuarioId);
            var tarefas = await _tarefaRepository.ObterPorUsuarioIdAsync(usuarioId);
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterTodosAsync()
        {
            var tarefas = await _tarefaRepository.ObterTodosAsync();
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterTarefasDoDia(int usuarioId)
        {
            await CheckPermissao(usuarioId);
            var tarefas = await _tarefaRepository.GetTarefasDia(usuarioId);
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterTarefasDoMes(int usuarioId)
        {
            await CheckPermissao(usuarioId);
            var tarefas = await _tarefaRepository.GetTarefasMes(usuarioId);
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterTarefasDaSemana(int usuarioId)
        {
            await CheckPermissao(usuarioId);
            var tarefas = await _tarefaRepository.GetTarefasSemana(usuarioId);
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<List<TarefaResponse>> ObterTarefasDoProjeto(int projetoId)
        {
            await _usuario_ProjetoService.CheckSeProjetoExiste(projetoId);
            if (UsuarioPermissao == PermissaoUtil.PermissaoColaborador)
            {
                await _usuario_ProjetoService.CheckSeUsuarioFazParteDoProjeto(projetoId);
            }
            var tarefas = await _tarefaRepository.GetTarefasProjeto(projetoId);
            var response = ObterHorasTotais(tarefas);
            return response;
        }

        public async Task<TarefaResponse> StartTarefa(int id)
        {
            var tarefa = await CheckSeIdExiste(id);
            await _usuario_ProjetoService.CheckPermissao(tarefa.Usuario_ProjetoId);
            if (tarefa.DataFinal != null || tarefa.DataInicial != null)
            {
                throw new BaseException(
                    StatusException.NaoProcessado,
                    $"Tarefa de id {id} já foi iniciada ou finalizada."
                );
            }
            tarefa.DataInicial = DateTime.Now;
            tarefa.DataAlteracao = DateTime.Now;
            await _tarefaRepository.AlterarAsync(tarefa);
            return _mapper.Map<TarefaResponse>(tarefa);
        }

        public async Task<TarefaResponse> StopTarefa(int id)
        {
            var tarefa = await CheckSeIdExiste(id);
            await _usuario_ProjetoService.CheckPermissao(tarefa.Usuario_ProjetoId);

            if (tarefa.DataInicial == null)
            {
                throw new BaseException(
                    StatusException.NaoProcessado,
                    $"Tarefa de id {id} ainda não foi iniciada"
                );
            }
            if (tarefa.DataFinal != null)
            {
                throw new BaseException(
                    StatusException.NaoProcessado,
                    $"Tarefa de id {id} já foi finalizada."
                );
            }
            tarefa.DataFinal = DateTime.Now;
            tarefa.DataAlteracao = DateTime.Now;
            await _tarefaRepository.AlterarAsync(tarefa);
            var response = ObterHorasTotais(tarefa);
            return response;
        }

        private TarefaResponse ObterHorasTotais(Tarefa tarefa)
        {
            var response = _mapper.Map<TarefaResponse>(tarefa);
            if (tarefa.DataInicial != null && tarefa.DataFinal != null)
            {
                response.TotalHoras =
                    tarefa.DataFinal.Value.TimeOfDay - tarefa.DataInicial.Value.TimeOfDay;
                return response;
            }
            if (tarefa.DataInicial != null)
            {
                response.TotalHoras = DateTime.Now.TimeOfDay - tarefa.DataInicial.Value.TimeOfDay;
                return response;
            }
            return response;
        }

        private List<TarefaResponse> ObterHorasTotais(ICollection<Tarefa> tarefas)
        {
            var responses = new List<TarefaResponse>();
            foreach (var tarefa in tarefas)
            {
                var response = ObterHorasTotais(tarefa);
                responses.Add(response);
            }
            return responses;
        }

        private async Task CheckPermissao(int usuarioId)
        {
            await _usuario_ProjetoService.CheckSeUsuarioExiste(usuarioId);
            if (UsuarioPermissao == PermissaoUtil.PermissaoColaborador && UsuarioId != usuarioId)
            {
                throw new BaseException(
                    StatusException.NaoAutorizado,
                    "Colaborador não pode interagir com tarefas de outros colaboradores."
                );
            }
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
