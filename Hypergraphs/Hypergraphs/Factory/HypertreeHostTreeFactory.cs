using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Hypergraphs.Factory;

public class HypertreeHostTreeFactory
{
    public static Graph FromHypertree(Hypergraph h)
    {
        Graph maximumHostGraph = new Graph(h.N);
        for (int e = 0; e < h.M; e++)
        {
            int[] vertices = h.GetEdgeVertices(e).ToArray();
            for (var v = 0; v < vertices.Length; v++)
            for (var u = v + 1; u < vertices.Length; u++)
            {
                if (!maximumHostGraph.EdgeExists(vertices[v], vertices[u])) 
                    maximumHostGraph.AddEdge(vertices[v], vertices[u]);
                else 
                    maximumHostGraph.SetWeight(vertices[v], vertices[u], maximumHostGraph.Weight(vertices[v], vertices[u]) + 1);
            }
        }

        MaximumSpanningTree mst = new MaximumSpanningTree();
        return mst.Create(maximumHostGraph);
    }
    
}