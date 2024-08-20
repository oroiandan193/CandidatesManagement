using CandidatesManagement.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace CandidatesManagement.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, bool isDevelopment = true)
        {
            //
            if (isDevelopment) 
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                //redis will go here.
            }

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
