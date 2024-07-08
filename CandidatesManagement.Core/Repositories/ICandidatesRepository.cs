using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.ValueObjects;

namespace CandidatesManagement.Core.Repositories
{
    public interface ICandidatesRepository
    {
        Task<JobCandidate?> FindByEmailAddressAsync(EmailAddress emailAddress);

        void UpdateJobCandidate(JobCandidate jobCandidate);

        Task InsertJobCandidateAsync(JobCandidate jobCandidate);

        Task SaveChangesAsync();
    }
}
