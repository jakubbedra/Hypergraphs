using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class VertexPermutationColoring : BaseColoring<Hypergraph>
{

    private int[]? _bestColoring;
    
    private MonochromeRepair _monochromeRepair;

    public VertexPermutationColoring()
    {
        _monochromeRepair = new MonochromeRepair();
    }
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        _monochromeRepair = new MonochromeRepair();
        List<int> availableVertices = new List<int>();
        for (int i = 0; i < h.N; i++)
            availableVertices.Add(i);
        return PermutationColoring(h, new List<int>(), availableVertices);
    }

    private int[]? PermutationColoring(Hypergraph h, List<int> permutation, List<int> availableVertices)
    {
        if (availableVertices.Count == 0)
        {
            _monochromeRepair.StartPermutation = permutation.ToArray();
            return _monochromeRepair.ComputeColoring(h);
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