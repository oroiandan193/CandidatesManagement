using CandidatesManagement.Application.Contracts;
using CandidatesManagement.Application.Contracts.Dtos;
using CandidatesManagement.Core.Repositories;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CandidatesManagement.Application.Services
{
    internal class CandidatesService : ICandidatesService
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesService(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        public async Task UpsertJobCandidateAsync(UpsertJobCandidateDto dto)
        {
            var candidate = dto.ToEntity();

            var existing = await _repository.FindByEmailAddressAsync(candidate.EmailAddress);

            if (existing != null)
            {
                existing.UpdateFrom(candidate);

                await _repository.UpdateJobCandidateAsync(existing);
            }
            else
            {
                await _repository.InsertJobCandidateAsync(candidate);
            }
        }
    }
}
