using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Hypergraphs.Factory;
using Hypergraphs.Model;

public class HypergraphConnectivityCheck : PropertyCheck<Hypergraph>
{
    private readonly GraphConnectivityCheck _graphConnectivityCheck;

    public HypergraphConnectivityCheck()
    {
        _graphConnectivityCheck = new GraphConnectivityCheck();
    }
    
    public bool Apply(Hypergraph h)
    {
        Graph lineGraph = LineGraphFactory.FromHypergraph(h);
        
        return _graphConnectivityCheck.Apply(lineGraph) && !ContainsIsolatedVertices(h);
    }

    private bool ContainsIsolatedVertices(Hypergraph h)
    {
        for (int v = 0; v < h.N; v++)
            if (h.VertexDegree(v) == 0)
                return true;
        return false;
    }
    
}
