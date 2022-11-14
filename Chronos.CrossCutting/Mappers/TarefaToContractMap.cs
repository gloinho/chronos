using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.CrossCutting.Mappers
{
    public class TarefaToContractMap : Profile
    {
        public TarefaToContractMap()
        {
            CreateMap<Tarefa, TarefaRequest>().ReverseMap();
            CreateMap<Tarefa, TarefaResponse>().ReverseMap();
        }
    }
}
