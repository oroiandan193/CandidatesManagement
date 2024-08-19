using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CandidatesManagement.Persistence.Sql.Persistence
{
    internal class JobCandidatesDbContextFactory : IDesignTimeDbContextFactory<JobCandidatesDbContext>
    {
        public JobCandidatesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<JobCandidatesDbContext>();

            var connectionString = configuration.GetConnectionString("JobCandidatesDbContext");

            builder.UseSqlServer(connectionString);

            return new JobCandidatesDbContext(builder.Options);
        }
    }
}
