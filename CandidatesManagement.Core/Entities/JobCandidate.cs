using CandidatesManagement.Core.ValueObjects;
using CandidatesManagement.Infrastructure.Domain;

namespace CandidatesManagement.Core.Entities
{
    public class JobCandidate : GenericEntity<int>, IVersioned
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public TimeInterval? TimeInterval { get; private set; }
        public Url? LinkedInProfile { get; private set; }
        public Url? GithubProfile { get; private set; }
        public string Comment { get; private set; }
        public byte[] Version { get; init; }

        //EF Core
        protected JobCandidate() { }

        public JobCandidate(string firstName, string lastName, PhoneNumber? phoneNumber, EmailAddress emailAddress, TimeInterval? timeInterval, Url? linkedInProfileLink, Url? githubProfileLink, string comment, RegistrationInfo registrationInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            TimeInterval = timeInterval;
            LinkedInProfile = linkedInProfileLink;
            GithubProfile = githubProfileLink;
            Comment = comment;
        }

        public void UpdateFrom(JobCandidate candidate)
        {
            ArgumentNullException.ThrowIfNull(candidate);

            FirstName = candidate.FirstName;
            LastName = candidate.LastName;
            PhoneNumber = candidate.PhoneNumber;
            EmailAddress = candidate.EmailAddress;
            TimeInterval = candidate.TimeInterval;
            LinkedInProfile = candidate.LinkedInProfile;
            GithubProfile = candidate.GithubProfile;
            Comment = candidate.Comment;
        }

        //Factory method.
        public static JobCandidate CreateNewCandidateRegistration(string firstName, string lastName, PhoneNumber? phoneNumber, EmailAddress emailAddress, TimeInterval? timeInterval, Url? linkedInProfileLink, Url? githubProfileLink, string comment = null)
        {
            RegistrationInfo registrationInfo = new NotRegisteredYet();

            return new JobCandidate(firstName, lastName, phoneNumber, emailAddress, timeInterval, linkedInProfileLink, githubProfileLink, comment, registrationInfo);
        }
    }
}
