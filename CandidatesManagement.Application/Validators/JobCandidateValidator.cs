using CandidatesManagement.Application.Contracts.Dtos;
using FluentValidation;

namespace CandidatesManagement.Application.Validators
{
    public class JobCandidateValidator : AbstractValidator<UpsertJobCandidateDto>
    {
        public JobCandidateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Missing FirstName");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Missing LastName");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+\d{1,3}\d{1,14}$").WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.LinkedInProfile)
                .Must(IsValidUrl).WithMessage("Invalid LinkedIn profile URL format.")
                .When(x => !string.IsNullOrEmpty(x.LinkedInProfile));

            RuleFor(x => x.GitHubProfile)
                .Must(IsValidUrl).WithMessage("Invalid GitHub profile URL format.")
                .When(x => !string.IsNullOrEmpty(x.GitHubProfile));

            RuleFor(x => x.IntervalStart)
                .LessThan(x => x.IntervalEnd).WithMessage("Interval start time must be before end time.")
                .When(x => x.IntervalStart.HasValue && x.IntervalEnd.HasValue);

            RuleFor(x => x.IntervalEnd)
                .GreaterThan(x => x.IntervalStart).WithMessage("Interval end time must be after start time.")
                .When(x => x.IntervalEnd.HasValue && x.IntervalStart.HasValue);

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Missing comment");
        }
        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
