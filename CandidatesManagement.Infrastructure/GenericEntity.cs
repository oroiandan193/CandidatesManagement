namespace CandidatesManagement.Infrastructure
{
    public class GenericEntity<T>
    {
        public T Id { get; init; }

        public byte[] Version { get; init; }
    }
}
