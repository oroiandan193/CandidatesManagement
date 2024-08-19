namespace CandidatesManagement.Infrastructure.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public string? ObjectIdentifier { get; init; }

        public ConcurrencyException() { }

        public ConcurrencyException(string objectIdentifier, string message) : base(message) 
        {
            ObjectIdentifier = objectIdentifier;
        }
    }
}
