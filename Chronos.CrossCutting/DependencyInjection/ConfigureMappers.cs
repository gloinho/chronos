using Chronos.CrossCutting.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.CrossCutting.DependencyInjection
{
    public class ConfigureMappers
    {
        public static void ConfigureDependenciesMappers(IServiceCollection serviceCollection)
        {
            var configuration = new AutoMapper.MapperConfiguration(conf =>
            {
                conf.AddProfile(new Usuario_ProjetoToContractMap());
                conf.AddProfile(new UsuarioToContractMap());
                conf.AddProfile(new ProjetoToContractMap());
                conf.AddProfile(new TarefaToContractMap());
            });
            var mapConfiguration = configuration.CreateMapper();
            serviceCollection.AddSingleton(mapConfiguration);
        }
    }
}
