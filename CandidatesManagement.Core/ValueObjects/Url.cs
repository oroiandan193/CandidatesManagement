namespace CandidatesManagement.Core.ValueObjects;

public sealed class Url : IEquatable<Url>
{
    public string Value { get; }

    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("URL cannot be empty", nameof(value));
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("Invalid URL format", nameof(value));
        }

        Value = value;
    }

    public override bool Equals(object obj) => obj is Url url && Equals(url);

    public bool Equals(Url other) => other != null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}
