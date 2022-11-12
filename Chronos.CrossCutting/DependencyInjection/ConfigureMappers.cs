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
            });
            var mapConfiguration = configuration.CreateMapper();
            serviceCollection.AddSingleton(mapConfiguration);
        }
    }
}
