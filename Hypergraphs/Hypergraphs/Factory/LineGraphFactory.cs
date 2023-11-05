using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Hypergraphs.Factory;

public class LineGraphFactory
{
    public static Graph FromHypergraph(Hypergraph h)
    {
        Graph g = new Graph(h.M);
        for (int i = 0; i < h.M; i++)
        for (int j = i + 1; j < h.M; j++)
            if (h.EdgesIntersect(i, j))
                g.AddEdge(i, j);
        return g;
    }
    
}