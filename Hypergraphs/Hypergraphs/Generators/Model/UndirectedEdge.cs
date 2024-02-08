public class UndirectedEdge
{
    public int V { get; set; }
    public int U { get; set; }

    // Overload the == operator
    public static bool operator ==(UndirectedEdge a, UndirectedEdge b)
    {
        // Check for null on either side
        if (ReferenceEquals(a, b))
            return true;

        // Check if either side is null
        if (a is null || b is null)
            return false;

        // Compare the contents of the edges
        return (a.V == b.V && a.U == b.U) || (a.V == b.U && a.U == b.V);
    }

    // Overload the != operator (optional, but recommended)
    public static bool operator !=(UndirectedEdge a, UndirectedEdge b)
    {
        return !(a == b);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(V + U);
    }
    
}
