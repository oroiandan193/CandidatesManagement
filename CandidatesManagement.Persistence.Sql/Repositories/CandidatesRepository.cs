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
        public async Task UpdateJobCandidateAsync(JobCandidate jobCandidate)
        {
            _dbContext.JobCandidates.Update(jobCandidate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertJobCandidateAsync(JobCandidate jobCandidate)
        {
            await _dbContext.JobCandidates.AddAsync(jobCandidate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<JobCandidate?> FindByEmailAddressAsync(EmailAddress emailAddress)
        {
            return await _dbContext.JobCandidates.Where(jc => jc.EmailAddress == emailAddress)
                .FirstOrDefaultAsync();
        }
    }
}
