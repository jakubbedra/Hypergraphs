namespace Hypergraphs.Generators.Model;

public class CircularOnesColumn
{
    public int StartIndex { get; set; }
    public int Size { get; set; }

    public int EndIndex => StartIndex + Size - 1;

    // columns do overlap, but are not equal
    public bool Overlaps(CircularOnesColumn other, int n)
    {
        List<ConsecutiveOnesColumn> thisFragments = new List<ConsecutiveOnesColumn>()
        {
            new ConsecutiveOnesColumn()
            {
                StartIndex = StartIndex,
                Size = EndIndex < n ? Size : n - StartIndex
            }
        };
        List<ConsecutiveOnesColumn> otherFragments = new List<ConsecutiveOnesColumn>()
        {
            new ConsecutiveOnesColumn()
            {
                StartIndex = other.StartIndex,
                Size = other.EndIndex < n ? other.Size : n - other.StartIndex
            }
        };
        if (this.EndIndex >= n)
        {
            thisFragments.Add(new ConsecutiveOnesColumn()
            {
                StartIndex = 0,
                Size = EndIndex - n
            });
        }

        if (other.EndIndex >= n)
        {
            otherFragments.Add(new ConsecutiveOnesColumn()
            {
                StartIndex = 0,
                Size = other.EndIndex - n
            });
        }

        return thisFragments.Any(c1 => otherFragments.Any(c2 => c2.Overlaps(c1)));
    }

    // Overload the == operator
    public static bool operator ==(CircularOnesColumn a, CircularOnesColumn b)
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
    public static bool operator !=(CircularOnesColumn a, CircularOnesColumn b)
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
        if (obj is CircularOnesColumn other)
            return this == other;

        return false;
    }

    public bool ContainsVertex(int vertex, int n)
    {
        
        List<ConsecutiveOnesColumn> thisFragments = new List<ConsecutiveOnesColumn>()
        {
            new ConsecutiveOnesColumn()
            {
                StartIndex = StartIndex,
                Size = EndIndex < n ? Size : n - StartIndex
            }
        };
        if (this.EndIndex >= n)
        {
            thisFragments.Add(new ConsecutiveOnesColumn()
            {
                StartIndex = 0,
                Size = EndIndex - n
            });
        }

        return thisFragments.Any(f => f.StartIndex <= vertex && f.EndIndex >= vertex);
    }
}