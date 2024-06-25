using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class BreadthFirstSearch
{
    public List<int> GetOrder(Graph g, int startVertex = 0)
    {
        List<int> bfsOrder = new List<int>();

        bool[] visited = new bool[g.N];
        for (var i = 0; i < g.N; i++)
            visited[i] = false;

        int v = startVertex;
        Queue<int> q = new Queue<int>();
        q.Enqueue(v);

        while (q.Count != 0)
        {
            v = q.Dequeue();
            if (!visited[v])
            {
                bfsOrder.Add(v);
                visited[v] = true;
                var neighbours = g.Neighbours(v);
                foreach (int neighbour in neighbours)
                    if (!visited[neighbour])
                        q.Enqueue(neighbour);
            }
        }

        return bfsOrder;
    }
    
    public List<int> GetOrder(Graph g, int startVertex, HashSet<int> visited)
    {
        List<int> bfsOrder = new List<int>();

        int v = startVertex;
        Queue<int> q = new Queue<int>();
        q.Enqueue(v);

        while (q.Count != 0)
        {
            v = q.Dequeue();
            if (!visited.Contains(v))
            {
                bfsOrder.Add(v);
                visited.Add(v);
                var neighbours = g.Neighbours(v);
                foreach (int neighbour in neighbours)
                    if (!visited.Contains(neighbour))
                        q.Enqueue(neighbour);
            }
        }

        return bfsOrder;
    }
}