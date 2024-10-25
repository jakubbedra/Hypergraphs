using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class TreeCheck : PropertyCheck<Graph>
{
    public bool Apply(Graph g)
    {
        return g.N - g.M - 1 == 0;
    }
}