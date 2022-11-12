using Chronos.CrossCutting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterAppDependencies(this IServiceCollection services)
        {
            ConfigureServices.ConfigureDependenciesService(services);
        }
        public static void RegisterAppDependenciesContext(this IServiceCollection services, string connectionString)
        {
            ConfigureRepository.ConfigureDependenciesRepository(services, connectionString);
        }
    }
}
