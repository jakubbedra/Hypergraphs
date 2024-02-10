namespace Hypergraphs.Common.Comparator;

public class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
{
    public bool Equals(HashSet<T> x, HashSet<T> y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x is null || y is null || x.Count != y.Count)
            return false;

        return x.SetEquals(y);
    }

    public int GetHashCode(HashSet<T> obj)
    {
        unchecked
        {
            int hash = 17;
            foreach (T item in obj)
                hash = hash * 23 + item.GetHashCode();
            return hash;
        }
    }
}
