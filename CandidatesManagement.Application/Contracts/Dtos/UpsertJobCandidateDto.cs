using CandidatesManagement.Core.Entities;
using System.Globalization;
using System.Text.Json.Serialization;

namespace CandidatesManagement.Application.Contracts.Dtos
{
    public class UpsertJobCandidateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public TimeSpan? IntervalStart { get; set; }

        public TimeSpan? IntervalEnd { get; set; }

        public string LinkedInProfile { get; set; }

        public string GitHubProfile { get; set; }

        public string Comment { get; set; }

        public JobCandidate ToEntity() 
        {
            return new JobCandidate(
                FirstName,
                LastName,
                string.IsNullOrEmpty(PhoneNumber) ? new Core.ValueObjects.PhoneNumber(PhoneNumber) : null,
                new Core.ValueObjects.EmailAddress(EmailAddress),
                (IntervalStart.HasValue && IntervalEnd.HasValue ) ? new Core.ValueObjects.TimeInterval(TimeOnly.FromTimeSpan(IntervalStart.Value), TimeOnly.FromTimeSpan(IntervalEnd.Value)) : null,
                new Core.ValueObjects.Url(LinkedInProfile),
                new Core.ValueObjects.Url(GitHubProfile),
                Comment
                );
        }
    }
}
