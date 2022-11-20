using AutoMapper;
using Chronos.CrossCutting.Mappers;

namespace Chronos.Testes.Settings
{
    public static class MapperConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Usuario_ProjetoToContractMap());
                cfg.AddProfile(new UsuarioToContractMap());
                cfg.AddProfile(new ProjetoToContractMap());
                cfg.AddProfile(new TarefaToContractMap());
            });
            return mockMapper.CreateMapper();
        }
    }
}
