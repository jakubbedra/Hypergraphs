namespace Hypergraphs.Common.Algorithms;

public interface PropertyCheck<T>
{
    bool Apply(T structure);
}