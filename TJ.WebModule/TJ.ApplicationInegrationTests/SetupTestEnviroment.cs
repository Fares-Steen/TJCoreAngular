using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using TJ.Persistence;
using TJ.WebModule;

namespace TJ.ApplicationInegrationTests
{
    public class SetupTestEnviroment:WebApplicationFactory<Startup>
    {
        WebApplicationFactory<Startup> appfactory;
        protected readonly HttpClient testClient;
        protected readonly DevicesDbContext dbContext;
        public SetupTestEnviroment()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            dbContext = connectionFactory.CreateContextForSQLite();
            appfactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
              {
                  builder.ConfigureServices((hostContext, serviceCollection) =>
                  {
                      var oldDbContext = serviceCollection.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(DevicesDbContext));
                      serviceCollection.Remove(oldDbContext);
                      serviceCollection.AddSingleton(dbContext);
                  });
              });

            testClient = appfactory.CreateClient();
        }
    }

    public static class ServiceCollectionExtemsion
    {
        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);

            return services;
        }
    }
}
