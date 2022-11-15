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
            serviceCollection.AddScoped<IAutenticacaoService, AutenticacaoService>();
            serviceCollection.AddScoped<IProjetoService, ProjetoService>();
            serviceCollection.AddScoped<IUsuario_ProjetoService, Usuario_ProjetoService>();
            serviceCollection.AddScoped<ITogglService, TogglService>();
        }
    }
}
