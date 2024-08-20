using CandidatesManagement.Core.Repositories;
using CandidatesManagement.Persistence.Sql.Persistence;
using CandidatesManagement.Persistence.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidatesManagement.Persistence.Sql
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            //register DB Context.
            services.AddDbContext<JobCandidatesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("JobCandidatesDbContext")));

            //register repos.
            services.AddScoped<CandidatesRepository>();
            services.AddScoped<ICandidatesRepository, CachedCandidatesRepository>();

            return services;
        }
    }
}
