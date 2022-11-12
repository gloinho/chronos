using Chronos.Domain.Interfaces.Services;
using Chronos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.CrossCutting.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
