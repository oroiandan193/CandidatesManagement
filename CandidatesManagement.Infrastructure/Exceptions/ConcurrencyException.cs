namespace CandidatesManagement.Infrastructure.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException() { }

        public ConcurrencyException(string message) : base(message) { }
    }
}
