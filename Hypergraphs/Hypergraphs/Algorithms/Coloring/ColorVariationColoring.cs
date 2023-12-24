using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class ColorVariationColoring : BaseColoring<Hypergraph>
{
    private Hypergraph _h;
    private HypergraphColoringValidator _validator;

    public ColorVariationColoring(Hypergraph h)
    {
        _h = h;
        _validator = new HypergraphColoringValidator();
    }

    public ColorVariationColoring()
    {
        _validator = new HypergraphColoringValidator();
    }
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        _h = h;
        List<int> colors = new List<int>();
        for (int i = 2; i < h.N; i++)
        {
            List<int> coloring = VariationColoring(i, colors);
            if (coloring.Count > 0)
            {
                return colors.ToArray();
            }
        }

        return colors.ToArray();
    }

    List<int> VariationColoring(int maxNumberOfColors, List<int> vertexColors)
    {
        if (vertexColors.Count == _h.N)
        {
            if (_validator.IsValid(_h, vertexColors.ToArray())) 
                return vertexColors;
            return new List<int>();
        }
        else
        {
            for (int i = 0; i < maxNumberOfColors; i++)
            {
                vertexColors.Add(i);
                List<int> coloring = VariationColoring(maxNumberOfColors, vertexColors);
                if (coloring.Count > 0)
                {
                    return coloring;
                }

                vertexColors.Remove(i);
            }
        }

        return new List<int>();
    }
    
}
