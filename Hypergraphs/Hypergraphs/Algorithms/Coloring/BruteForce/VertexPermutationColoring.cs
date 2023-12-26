using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class VertexPermutationColoring : BaseColoring<Hypergraph>
{

    private int[]? _bestColoring;
    
    private GreedyColoring _greedyColoring;

    public VertexPermutationColoring()
    {
        _greedyColoring = new GreedyColoring();
    }
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        _greedyColoring = new GreedyColoring();
        List<int> availableVertices = new List<int>();
        for (int i = 0; i < h.N; i++)
            availableVertices.Add(i);
        return PermutationColoring(h, new List<int>(), availableVertices);
    }

    private int[]? PermutationColoring(Hypergraph h, List<int> permutation, List<int> availableVertices)
    {
        if (availableVertices.Count == 0)
        {
            _greedyColoring.StartPermutation = permutation.ToArray();
            return _greedyColoring.ComputeColoring(h);
        }

        List<int> availableVerticesCopy = new List<int>(availableVertices);
        foreach (int v in availableVertices)
        {
            availableVerticesCopy.Remove(v);
            permutation.Add(v);
            int[] colors = PermutationColoring(h, permutation, availableVerticesCopy);
            int maxColor = colors.Max();
            if (_bestColoring != null && _bestColoring.Max() > maxColor)
            {
                _bestColoring = colors;
            }
            else if (_bestColoring == null)
            {
                _bestColoring = colors;
            }
            
            permutation.Remove(v);
            availableVerticesCopy.Add(v);
        }

        return _bestColoring;
    }
    
}