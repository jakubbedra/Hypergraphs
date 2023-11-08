using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class ChordalityCheck : PropertyCheck<Graph>
{
    public bool Apply(Graph g)
    {
        int[] ordering = MaximumCardinalitySearch(g);
        return IsSimplicialEliminationOrdering(g, ordering);
    }

    private bool IsSimplicialEliminationOrdering(Graph g, int[] ordering)
    {
        for (int i = 0; i < g.N; i++)
        {
            int[] vertices = g.Neighbours(ordering[i])
                .Where(v => Array.IndexOf(ordering, v) < i)
                .Append(ordering[i])
                .ToArray();
            if (!IsCompleteSubGraph(g, vertices))
                return false;
        }

        return true;
    }

    private bool IsCompleteSubGraph(Graph g, int[] vertices)
    {
        for (int i = 0; i < vertices.Length; i++)
        for (var j = i+1; j < vertices.Length; j++)
            if (!g.EdgeExists(vertices[i], vertices[j]))
                return false;
        return true;
    }

    private int[] MaximumCardinalitySearch(Graph g)
    {
        int[] weight = new int[g.N];
        List<int> vertices = new List<int>();
        int[] ordering = new int[g.N];
        for (int i = 0; i < g.N; i++)
        {
            weight[i] = 0;
            vertices.Add(i);
        }

        for (int i = 0; i < g.N; i++)
        {
            int u = vertices.Aggregate((max, cur) => weight[cur] > weight[max] ? cur : max);
            ordering[i] = u;
            g.Neighbours(u).ForEach(v => weight[v]++);
            vertices.Remove(u);
        }

        return ordering;
    }
    
}
