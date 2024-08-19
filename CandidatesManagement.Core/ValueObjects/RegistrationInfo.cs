namespace CandidatesManagement.Core.ValueObjects
{
    public abstract record RegistrationInfo;

    public record NotRegisteredYet : RegistrationInfo;
    public record ReviewPending(DateTime ReviewStartedAt): RegistrationInfo;
    public record Approved(DateTime ApprovedAt) : RegistrationInfo;
}
