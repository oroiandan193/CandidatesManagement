namespace CandidatesManagement.Core.ValueObjects;

using System;
using System.Text.RegularExpressions;

public sealed class EmailAddress : IEquatable<EmailAddress>
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email address cannot be empty", nameof(value));
        }

        if (!EmailRegex.IsMatch(value))
        {
            throw new ArgumentException("Invalid email address format", nameof(value));
        }

        Value = value;
    }

    public override bool Equals(object obj) => obj is EmailAddress emailAddress && Equals(emailAddress);

    public bool Equals(EmailAddress other) => other != null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}
