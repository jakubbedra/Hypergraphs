namespace Hypergraphs.Generators.Model;

public class ConsecutiveOnesColumn
{
    public int StartIndex { get; set; }
    public int Size { get; set; }

    public int EndIndex => StartIndex + Size - 1;

    // columns do overlap, but are not equal
    public bool Overlaps(ConsecutiveOnesColumn other)
    {
        return !(other.EndIndex < this.StartIndex || this.EndIndex < other.StartIndex);
    }
    
    // Overload the == operator
    public static bool operator ==(ConsecutiveOnesColumn a, ConsecutiveOnesColumn b)
    {
        // Check for null on either side
        if (ReferenceEquals(a, b))
            return true;
        
        // Check if either side is null
        if (a is null || b is null)
            return false;

        // Compare the contents of the columns
        return a.StartIndex == b.StartIndex && a.Size == b.Size;
    }

    // Overload the != operator (optional, but recommended)
    public static bool operator !=(ConsecutiveOnesColumn a, ConsecutiveOnesColumn b)
    {
        return !(a == b);
    }

    // Implement GetHashCode (required when overloading equality operators)
    public override int GetHashCode()
    {
        return HashCode.Combine(StartIndex, Size);
    }

    // Implement Equals (required when overloading equality operators)
    public override bool Equals(object obj)
    {
        if (obj is ConsecutiveOnesColumn other)
            return this == other;

        return false;
    }

    public bool ContainsVertex(int vertex)
    {
        return StartIndex <= vertex && EndIndex >= vertex;
    }
}