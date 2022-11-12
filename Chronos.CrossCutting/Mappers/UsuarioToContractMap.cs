using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.CrossCutting.Mappers
{
    public class UsuarioToContractMap : Profile
    {
        public UsuarioToContractMap()
        {
            CreateMap<Usuario, UsuarioRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();
        }
    }
}
