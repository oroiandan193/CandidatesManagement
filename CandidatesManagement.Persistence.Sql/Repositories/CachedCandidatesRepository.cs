using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.Repositories;
using CandidatesManagement.Core.ValueObjects;
using CandidatesManagement.Infrastructure.Cache;

namespace CandidatesManagement.Persistence.Sql.Repositories
{
    internal class CachedCandidatesRepository : ICandidatesRepository
    {
        private readonly CandidatesRepository _decorated;
        private readonly ICacheService _cacheService;

        public CachedCandidatesRepository(CandidatesRepository decorated, ICacheService cacheService)
        {
            _decorated = decorated;
            _cacheService = cacheService;
        }

        public async Task<JobCandidate?> FindByEmailAddressAsync(EmailAddress emailAddress) =>
            await _cacheService.GetOrAddAsync<JobCandidate>(emailAddress.Value, async () =>
            {
                return await _decorated.FindByEmailAddressAsync(emailAddress);
            });

        public async Task InsertJobCandidateAsync(JobCandidate jobCandidate)
        {
            await _decorated.InsertJobCandidateAsync(jobCandidate);

            await _cacheService.SetAsync(jobCandidate.EmailAddress.Value, jobCandidate);
        }

        public Task SaveChangesAsync()
        {
            return _decorated.SaveChangesAsync();
        }

        public void UpdateJobCandidate(JobCandidate jobCandidate)
        {
            _decorated.UpdateJobCandidate(jobCandidate);

            _cacheService.SetAsync(jobCandidate.EmailAddress.Value, jobCandidate).GetAwaiter().GetResult();
        }
    }
}
