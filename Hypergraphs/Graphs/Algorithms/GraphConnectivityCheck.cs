using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class GraphConnectivityCheck : PropertyCheck<Graph>
{
    public bool Apply(Graph g)
    {
        bool[] visited = new bool[g.N];
        for (var i = 0; i < g.N; i++)
            visited[i] = false;

        int v = 0;
        Queue<int> q = new Queue<int>();
        q.Enqueue(v);

        while (q.Count != 0)
        {
            v = q.Dequeue();
            visited[v] = true;
            var neighbours = g.Neighbours(v);
            foreach (int neighbour in neighbours)
                if (!visited[neighbour])
                    q.Enqueue(neighbour);
        }
        return !visited.Contains(false);
    }
}