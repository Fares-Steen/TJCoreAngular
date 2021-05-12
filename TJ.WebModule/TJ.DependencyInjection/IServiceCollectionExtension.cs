using Microsoft.Extensions.DependencyInjection;
using TJ.Interfaces.DbInterfaces;
using TJ.Persistence;
using TJ.Persistence.Initialize;
using TJ.Persistence.Repositories;

namespace TJ.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddPersistenceLibrary(this IServiceCollection services)
        {
            services.AddDbContext<DevicesDbContext>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
