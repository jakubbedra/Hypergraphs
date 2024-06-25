using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Factory;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Hypergraphs.Factory.Valdator;

public class HostGraphValidator
{
    public bool IsValid(Hypergraph h, Graph hostGraph)
    {
        for (int e = 0; e < h.M; e++)
        {
            List<int> vertices = h.GetEdgeVertices(e);
            Dictionary<int, List<int>> subgraph = new Dictionary<int, List<int>>();
            foreach (int vertex in vertices)
            {
                subgraph.Add(vertex, new List<int>());
            }

            foreach (int v in vertices)
            {
                hostGraph.Neighbours(v)
                    .Where(u => vertices.Contains(u))
                    .ToList()
                    .ForEach(u => subgraph[v].Add(u));
            }

            if (!IsConnected(subgraph, vertices[0]))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsConnected(Dictionary<int, List<int>> graph, int startNode)
    {
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(startNode);
        visited.Add(startNode);

        while (queue.Count > 0)
        {
            // Dequeue a node from the queue
            int currentNode = queue.Dequeue();
            // Console.WriteLine(currentNode);

            // Get all neighbors of the current node
            if (!graph.ContainsKey(currentNode)) continue;
            foreach (int neighbor in graph[currentNode])
            {
                // If the neighbor hasn't been visited yet, enqueue it and mark it as visited
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }

        return visited.Count == graph.Count;
    }
    
}