using CandidatesManagement.Application.Contracts.Dtos;

namespace CandidatesManagement.UnitTests;

public class UpsertJobCandidateDtoBuilder
{
    private string _firstName;
    private string _lastName;
    private string _emailAddress;
    private string _phoneNumber;
    private TimeSpan? _intervalStart;
    private TimeSpan? _intervalEnd;
    private string _linkedInProfile;
    private string _gitHubProfile;
    private string _comment;

    public UpsertJobCandidateDtoBuilder()
    {
        // Set default values if any
        _firstName = string.Empty;
        _lastName = string.Empty;
        _emailAddress = string.Empty;
        _phoneNumber = null;
        _intervalStart = null;
        _intervalEnd = null;
        _linkedInProfile = string.Empty;
        _gitHubProfile = string.Empty;
        _comment = string.Empty;
    }

    public UpsertJobCandidateDtoBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithEmailAddress(string emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithIntervalStart(TimeSpan? intervalStart)
    {
        _intervalStart = intervalStart;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithIntervalEnd(TimeSpan? intervalEnd)
    {
        _intervalEnd = intervalEnd;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithLinkedInProfile(string linkedInProfile)
    {
        _linkedInProfile = linkedInProfile;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithGitHubProfile(string gitHubProfile)
    {
        _gitHubProfile = gitHubProfile;
        return this;
    }

    public UpsertJobCandidateDtoBuilder WithComment(string comment)
    {
        _comment = comment;
        return this;
    }

    public UpsertJobCandidateDto Build()
    {
        return new UpsertJobCandidateDto
        {
            FirstName = _firstName,
            LastName = _lastName,
            EmailAddress = _emailAddress,
            PhoneNumber = _phoneNumber,
            IntervalStart = _intervalStart,
            IntervalEnd = _intervalEnd,
            LinkedInProfile = _linkedInProfile,
            GitHubProfile = _gitHubProfile,
            Comment = _comment
        };
    }
}

