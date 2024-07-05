using CandidatesManagement.Application.Contracts.Dtos;

namespace CandidatesManagement.Application.Contracts
{
    public interface ICandidatesService
    {
        Task UpsertJobCandidateAsync(UpsertJobCandidateDto dto);
    }
}
