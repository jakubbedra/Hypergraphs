using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class MaximumSpanningTree
{
    public Graph Create(Graph g)
    {
        Graph t = new Graph(g.N);

        bool[] visited = new bool[g.N];
        int[] weights = new int[g.N];
        int[] parent = new int[g.N];

        for (int i = 0; i < g.N; i++)
        {
            visited[i] = false;
            weights[i] = int.MinValue;
        }

        weights[0] = int.MaxValue;
        parent[0] = -1;

        for (int i = 0; i < g.N - 1; i++)
        {
            int maxVertexIndex = MaxVertex(visited, weights, g.N);

            visited[maxVertexIndex] = true;

            for (int j = 0; j < g.N; j++)
            {
                if (g.EdgeExists(j, maxVertexIndex) && visited[j] == false)
                {
                    if (g.Weight(j, maxVertexIndex) > weights[j])
                    {
                        weights[j] = g.Weight(j, maxVertexIndex);
                        parent[j] = maxVertexIndex;
                    }
                }
            }
        }
        for (int i = 1; i < g.N; i++) 
        {
            t.AddEdge(i, parent[i], g.Weight(i, parent[i]));
        }
        return t;
    }

    private int MaxVertex(bool[] visited, int[] weights, int n)
    {
        int index = -1;
        int maxW = int.MinValue;
        for (int i = 0; i < n; i++)
        {
            if (visited[i] == false && weights[i] > maxW) 
            {
                maxW = weights[i];
                index = i;
            }
        }
        return index;
    }
    
}