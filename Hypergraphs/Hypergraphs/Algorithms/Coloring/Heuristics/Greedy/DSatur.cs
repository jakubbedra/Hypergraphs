using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class DSatur : BaseGreedy
{
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 0
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = -1;

        _vertexOrder = new int[h.N];

        List<int> unvisitedVertices = new List<int>();
        for (int i = 0; i < h.N; i++)
            unvisitedVertices.Add(i);

        for (var i = 0; i < h.N; i++)
        {
            // choose next vertex
            int vertexWithLeastPossibleColors = unvisitedVertices.MaxBy(v => GetConflictingColors(h, v, coloring).Count);
            int count = GetConflictingColors(h, vertexWithLeastPossibleColors, coloring).Count;
            List<int> candidates = unvisitedVertices.Where(v => GetConflictingColors(h, v, coloring).Count == count)
                .ToList();

            int chosenVertex = candidates.MaxBy(h.VertexDegree);
            unvisitedVertices.Remove(chosenVertex);

            coloring[chosenVertex] = GetMinNonConflictingColor2(h, chosenVertex, coloring);//todo: keep in mind this change
            
            _vertexOrder[i] = chosenVertex;
        }

        return coloring;
    }
    
}