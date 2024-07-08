using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.Repositories;
using CandidatesManagement.Core.ValueObjects;
using CandidatesManagement.Persistence.Sql.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CandidatesManagement.Persistence.Sql.Repositories
{
    internal class CandidatesRepository : ICandidatesRepository
    {
        private readonly JobCandidatesDbContext _dbContext;

        public CandidatesRepository(JobCandidatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateJobCandidate(JobCandidate jobCandidate)
        {
            _dbContext.JobCandidates.Update(jobCandidate);
        }

        public async Task InsertJobCandidateAsync(JobCandidate jobCandidate)
        {
            await _dbContext.JobCandidates.AddAsync(jobCandidate);
        }

        public async Task<JobCandidate?> FindByEmailAddressAsync(EmailAddress emailAddress)
        {
            return await _dbContext.JobCandidates.Where(jc => jc.EmailAddress == emailAddress)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
