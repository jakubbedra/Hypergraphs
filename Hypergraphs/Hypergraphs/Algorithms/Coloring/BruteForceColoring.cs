using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

// depracated, remove
public class BruteForceColoring : BaseColoring<Hypergraph>
{
    private int[]? _validColoring;

    private readonly HypergraphColoringValidator _coloringValidator;
        
    public BruteForceColoring()
    {
        _coloringValidator = new HypergraphColoringValidator();
    }
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        // assuming hypergraph is non-empty
        int numberOfColors = 2;
        _validColoring = new int[h.N];
        for (var i = 0; i < _validColoring.Length; i++)
        {
            _validColoring[i] = -1;
        }

        int[] from = new int[h.N];
        int[] to = new int[h.N];
        for (int i = 0; i < h.N; i++)
        {
            from[i] = 0;
            to[i] = numberOfColors;
        }
        while (!isKColorable(0, h, from, to))
        {
            numberOfColors++;
            for (int i = 0; i < h.N; i++)
            {
                to[i] = numberOfColors;
            }
        }

        return _validColoring;
    }
    private bool isKColorable(int index, Hypergraph h, int[] from, int[] to)
    {
        if (index == h.N)
        {
            return _coloringValidator.IsValid(h, _validColoring);
        }

        for (int i = from[index]; i < to[index]; i++)
        {
            _validColoring[index] = i;
            bool result = isKColorable(index + 1, h, from, to);
            _validColoring[index] = -1;
            if (result) return true;
        }

        return false;
    }
    
}
