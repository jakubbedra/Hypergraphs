using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class TwoEdgesFinder
{
    public static List<List<int>> FindTwoEdgeSequences(Hypergraph hypergraph)
    {
        // find all 2-edges
        // build a graph of them
        // return connected components
        int n = hypergraph.N;
        int[,] matrix = new int[n, n];
        for (var u = 0; u < n; u++)
        for (int v = 0; v < n; v++)
            matrix[u, v] = 0;
        int m = 0;
        for (int e = 0; e < hypergraph.M; e++)
        {
            List<int> edgeVertices = hypergraph.GetEdgeVertices(e);
            if (edgeVertices.Count == 2)
            {
                int u = edgeVertices[0];
                int v = edgeVertices[1];
                matrix[u, v] = 1;
                matrix[v, u] = 1;
                m++;
            }
        }

        Graph g = new Graph()
        {
            Matrix = matrix,
            N = n,
            M = m
        };

        BreadthFirstSearch bfs = new BreadthFirstSearch();
        HashSet<int> visitedVertices = new HashSet<int>();
        List<List<int>> twoEdgeSequences = new List<List<int>>();
        for (int v = 0; v < n; v++)
        {
            if (!visitedVertices.Contains(v) && g.VertexDegree(v) != 0)
            {
                List<int> order = bfs.GetOrder(g, v, visitedVertices);
                twoEdgeSequences.Add(order);
            }
        }

        return twoEdgeSequences;
    }

}