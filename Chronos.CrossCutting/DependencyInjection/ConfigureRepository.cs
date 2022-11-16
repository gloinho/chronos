using Chronos.Data.Context;
using Chronos.Data.Repositories;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(
            IServiceCollection serviceCollection,
            string connectionString
        )
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                //options.UseLazyLoadingProxies();
            });
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddScoped<IProjetoRepository, ProjetoRepository>();
            serviceCollection.AddScoped<IUsuario_ProjetoRepository, Usuario_ProjetoRepository>();
            serviceCollection.AddScoped<ITarefaRepository, TarefaRepository>();
        }
    }
}
