namespace CandidatesManagement.Infrastructure.Domain
{
    public interface IVersioned
    {
        public byte[] Version { get; init; }
    }
}
