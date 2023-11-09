namespace Hypergraphs.Common.Algorithms;

public abstract class BaseColoring<T>
{
    protected int[] _validColoring;

    public abstract int[] ComputeColoring(T obj);

}