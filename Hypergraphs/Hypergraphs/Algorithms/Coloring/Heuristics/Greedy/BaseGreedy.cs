using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public abstract class BaseGreedy : BaseColoring<Hypergraph>
{
    protected int[] _vertexOrder;

    public int[] GetVertexOrder()
    {
        return _vertexOrder;
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
                .Select(u => new Tuple<int, int>(u, coloring[u]))
                .ToList();
            if (edgeColors.Count > 0)
            {
                int color = edgeColors[0].Item2;
                if (edgeColors.All(c => c.Item2 == color))
                    conflictingColors.Add(color);
            }
        }

        return conflictingColors;
    }
}