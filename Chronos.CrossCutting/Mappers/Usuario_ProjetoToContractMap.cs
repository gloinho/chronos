using AutoMapper;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.CrossCutting.Mappers
{
    public class Usuario_ProjetoToContractMap : Profile
    {
        public Usuario_ProjetoToContractMap()
        {
            CreateMap<Usuario_Projeto, Usuario_ProjetoResponse>()
                .ForMember(dst => dst.NomeDoProjeto, map => map.MapFrom(src => src.Projeto.Nome))
                .ReverseMap();
        }
    }
}
