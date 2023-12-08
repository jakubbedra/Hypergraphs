using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class GraphConnectivityCheck : PropertyCheck<Graph>
{
    private BreadthFirstSearch _bfs = new();

    public bool Apply(Graph g)
    {
        return _bfs.GetOrder(g).Count == g.N;
    }
    
}
