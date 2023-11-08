using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Factory;

public class GraphFactory
{
    public static Graph FromEdgesList(int n, List<List<int>> edges)
    {
        Graph graph = new Graph(n);
        graph.N = n;

        foreach (List<int> edge in edges)
        {
            if (edge.Count != 2)
            {
                throw new ArgumentException("Each edge of a graph should be have size equal to 2.");
            }
            graph.AddEdge(edge[0], edge[1]);
        }

        return graph;
    }
    
    public static Graph FromEdgesList(int n, List<List<int>> edges, List<int> weights)
    {
        Graph graph = new Graph(n);
        graph.N = n;

        int e = 0;
        foreach (List<int> edge in edges)
        {
            if (edge.Count != 2)
            {
                throw new ArgumentException("Each edge of a graph should be have size equal to 2.");
            }
            graph.AddEdge(edge[0], edge[1], weights[e]);
            e++;
        }

        return graph;
    }
}