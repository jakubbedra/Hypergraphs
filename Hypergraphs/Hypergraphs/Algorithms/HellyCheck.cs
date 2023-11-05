using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HellyCheck
{
    public bool IsHelly(Hypergraph h)
    {
        for (int i = 0; i < h.N; i++)
        {
            for (int j = i + 1; j < h.N; j++)
            {
                for (int k = j + 1; k < h.N; k++)
                {
                    // edges containing at least 2 of the vertices
                    List<int> edges = new List<int>();
                    for (int e = 0; e < h.M; e++)
                    {
                        if (h.VertexInEdge(i, e) && h.VertexInEdge(j, e) ||
                            h.VertexInEdge(i, e) && h.VertexInEdge(k, e) ||
                            h.VertexInEdge(k, e) && h.VertexInEdge(j, e))
                            edges.Add(e);
                    }

                    if (!EdgesHaveCore(h, edges))
                        return false;
                }
            }
        }
        return true;
    }

    private bool EdgesHaveCore(Hypergraph h, List<int> edges)
    {
        if (edges.Count < 2) return true;

        List<int> core = h.EdgeIntersection(edges[0], edges[1]);
        
        for (var i = 2; i < edges.Count; i++)
            core = h.EdgeIntersection(core, edges[i]);
        
        return core.Count != 0;
    }
}