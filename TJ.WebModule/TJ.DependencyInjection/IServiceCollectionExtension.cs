using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TJ.Application.DeviceActions;
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
            services.AddDbContext<DevicesDbContext>(opt=>opt.UseSqlite("DataSource=DeviceDb.db"));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IDeviceAction, DeviceAction>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
