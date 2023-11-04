using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Hypergraphs.Factory;

public class LineGraphFactory
{
    public Graph FromHypergraph(Hypergraph h)
    {
        Graph g = new Graph(h.M);
        for (int i = 0; i < h.M; i++)
        for (int j = i + 1; j < h.M; j++)
            if (EdgesIntersect(h, i, j))
                g.AddEdge(i, j);
        return g;
    }

    private bool EdgesIntersect(Hypergraph h, int e1, int e2)
    {
        for (int i = 0; i < h.N; i++)
            if (h.Matrix[i, e1] > 0 && h.Matrix[i, e2] > 0)
                return true;
        return false;
    }
    
}