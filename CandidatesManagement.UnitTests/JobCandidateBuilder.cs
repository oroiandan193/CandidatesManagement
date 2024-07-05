using CandidatesManagement.Application.Contracts.Dtos;
using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.ValueObjects;

namespace CandidatesManagement.UnitTests;

public class JobCandidateBuilder
{
    private string _firstName = "John";
    private string _lastName = "Doe";
    private PhoneNumber _phoneNumber = new PhoneNumber("+1234567890");
    private EmailAddress _emailAddress = new EmailAddress("johndoe@example.com");
    private TimeInterval _timeInterval = new TimeInterval(new TimeOnly(9, 0), new TimeOnly(17, 0));
    private Url _linkedInProfile = new Url("https://www.linkedin.com/in/johndoe");
    private Url _githubProfile = new Url("https://github.com/johndoe");
    private string _comment = "Test comment";

    public JobCandidate Build()
    {
        return new JobCandidate(
            _firstName,
            _lastName,
            _phoneNumber,
            _emailAddress,
            _timeInterval,
            _linkedInProfile,
            _githubProfile,
            _comment
        );
    }

    public UpsertJobCandidateDto BuildDto()
    {
        return new UpsertJobCandidateDto
        {
            FirstName = _firstName,
            LastName = _lastName,
            Comment = _comment,
            EmailAddress = _emailAddress.Value,
            GitHubProfile = _githubProfile.Value,
            IntervalEnd = _timeInterval.End.Value.ToTimeSpan(),
            IntervalStart = _timeInterval.Start.Value.ToTimeSpan(),
            LinkedInProfile = _linkedInProfile.Value,
            PhoneNumber = _phoneNumber.Value
        };
    }

    public JobCandidateBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public JobCandidateBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public JobCandidateBuilder WithPhoneNumber(PhoneNumber phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public JobCandidateBuilder WithEmailAddress(EmailAddress emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }

    public JobCandidateBuilder WithTimeInterval(TimeInterval timeInterval)
    {
        _timeInterval = timeInterval;
        return this;
    }

    public JobCandidateBuilder WithLinkedInProfile(Url linkedInProfile)
    {
        _linkedInProfile = linkedInProfile;
        return this;
    }

    public JobCandidateBuilder WithGithubProfile(Url githubProfile)
    {
        _githubProfile = githubProfile;
        return this;
    }

    public JobCandidateBuilder WithComment(string comment)
    {
        _comment = comment;
        return this;
    }
}

