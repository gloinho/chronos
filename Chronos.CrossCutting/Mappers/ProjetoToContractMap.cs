using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.CrossCutting.Mappers
{
    internal class ProjetoToContractMap : Profile
    {
        public ProjetoToContractMap()
        {
            CreateMap<ProjetoRequest, Projeto>().ReverseMap();
            CreateMap<Projeto, ProjetoResponse>().ReverseMap();
        }
    }
}
