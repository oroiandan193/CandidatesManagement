namespace CandidatesManagement.Core.ValueObjects;

using System;
using System.Text.RegularExpressions;

public sealed class PhoneNumber : IEquatable<PhoneNumber>
{
    private static readonly Regex PhoneNumberRegex = new Regex(@"^\+\d{1,3}\d{1,14}$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Phone number cannot be empty", nameof(value));
        }

        if (!PhoneNumberRegex.IsMatch(value))
        {
            throw new ArgumentException("Invalid phone number format", nameof(value));
        }

        Value = value;
    }

    public override bool Equals(object obj) => obj is PhoneNumber phoneNumber && Equals(phoneNumber);

    public bool Equals(PhoneNumber other) => other != null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}
