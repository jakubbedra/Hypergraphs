using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class GreedyColoring : BaseColoring<Hypergraph>
{
    private static Random _r = new Random();
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 1
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = 1;

        int[] vertices = new int[h.N];
        for (var i = 0; i < vertices.Length; i++)
            vertices[i] = i;
        
        // randomize vertices
        double[] x = new double[h.N];
        for (var i = 0; i < x.Length; i++)
            x[i] = _r.NextDouble();
        
        Array.Sort(x, vertices);

        HashSet<int> visitedEdges = new HashSet<int>();

        for (var i = 0; i < vertices.Length; i++)
        {
            int currentMinColor = 1;
            for (int j = 0; j < h.M; j++)
            {
                if (h.Matrix[vertices[i], j] != 0 && !visitedEdges.Contains(j))
                {
                    visitedEdges.Add(j);
                    // recolor the vertex so that edge j is not monochromatic
                    currentMinColor = GetMinNonConflictingColor(currentMinColor, h, vertices[i], coloring);
                }
            }
            coloring[vertices[i]] = currentMinColor;
        }
        
        return coloring;
    }

    private int GetMinNonConflictingColor(int color, Hypergraph h, int v, int[] coloring)
    {
        List<int> vertexEdges = h.GetVertexEdges(v);

        while (true)
        {
            bool monochromaticEdgeFound = false;
            foreach (int e in vertexEdges)
            {
                HashSet<Tuple<int, int>> edgeColors = h.GetEdgeVertices(e)
                    .Select(u => new Tuple<int, int>(u, coloring[u]))
                    .ToHashSet();
                // check if monochromatic when color will be applied
                if (edgeColors.All(c => c.Item2 == color || c.Item1 == v))
                {
                    monochromaticEdgeFound = true;
                    color++;
                    break;
                } 
            }
            if (!monochromaticEdgeFound)
            {
                return color;
            }
        }
    }
    
}
