namespace CandidatesManagement.Core.ValueObjects;

public sealed class TimeInterval : IEquatable<TimeInterval>
{
    public TimeOnly? Start { get; }
    public TimeOnly? End { get; }

    public TimeInterval(TimeOnly? start, TimeOnly? end)
    {
        if (end <= start)
        {
            throw new ArgumentException("End time must be greater than start time.");
        }

        Start = start;
        End = end;
    }

    public override bool Equals(object obj) => obj is TimeInterval interval && Equals(interval);

    public bool Equals(TimeInterval other) => other != null && Start == other.Start && End == other.End;

    public override int GetHashCode() => HashCode.Combine(Start, End);

    public override string ToString() => $"{Start} - {End}";
}
