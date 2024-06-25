using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HyperpathGreedyColoring : BaseColoring<Hypergraph>
{

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        int[] coloring = new int[hypergraph.N];
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = -1;
        List<List<int>> twoEdgeSequences = TwoEdgesFinder.FindTwoEdgeSequences(hypergraph);
        foreach (List<int> twoEdgeSequence in twoEdgeSequences)
        {
            twoEdgeSequence.ForEach(v => coloring[v] = GetMinNonConflictingColor(hypergraph, v, coloring));
        }

        for (int v = 0; v < hypergraph.N; v++)
        {
            if (coloring[v] == -1)
            {
                coloring[v] = GetMinNonConflictingColor(hypergraph, v, coloring);
            }
        }
        return coloring;
    }
    
    
    protected int GetMinNonConflictingColor(Hypergraph h, int vertex, int[] coloring)
    {
        HashSet<int> conflictingColors = GetConflictingColors(h, vertex, coloring);
        int currentMinColor = 0;

        while (conflictingColors.Contains(currentMinColor))
            currentMinColor++;
        return currentMinColor;
    }

    protected HashSet<int> GetConflictingColors(Hypergraph h, int vertex, int[] coloring)
    {
        List<int> vertexEdges = h.GetVertexEdges(vertex);
        HashSet<int> conflictingColors = new HashSet<int>();

        foreach (int e in vertexEdges)
        {
            List<Tuple<int, int>> edgeColors = h.GetEdgeVertices(e)
                .Where(u => u != vertex)
                .Where(u => coloring[u] != -1)
                .Select(u => new Tuple<int, int>(u, coloring[u]))
                .ToList();
            if (edgeColors.Count == h.EdgeCardinality(e) - 1)
            {
                int color = edgeColors[0].Item2;
                if (edgeColors.All(c => c.Item2 == color))
                    conflictingColors.Add(color);
            }
        }

        return conflictingColors;
    }
    
}