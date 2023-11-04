using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class BruteForceColoring
{
    private int[] _validColoring;
    
    public int[] GetColoring(Hypergraph h)
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
// todo: trzeba jakos wstrzyknac stan poczatkowy w postaci tablicy, np: [0,0,0,0,0] albo [0,0,1,0,0,0]
// todo: oraz stan koncowy, w sensie to ma byc od jakiego do jakiego pokolorowania sprawdzamy
// dodac na podstawie tego TaskBruteForceColoring  i GpuBruteForceColoring
    private bool isKColorable(int index, Hypergraph h, int[] from, int[] to)
    {
        if (index == h.N)
        {
            return IsValidColoring(h);
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
    
    private bool IsValidColoring(Hypergraph h)
    {
        for (int i = 0; i < h.M; i++)
        {
            bool isMonochromatic = false;
            // we only need 2 colors, because it is sufficient for telling that an edge is not monochromatic
            int color1 = -1;
            int color2 = -1;
            
            for (var j = 0; j < h.N; j++)
            {
                if (h.Matrix[j, i] > 0)
                {
                    if (color1 == -1)
                    {
                        color1 = _validColoring[j];
                    }
                    else if (_validColoring[j] != color1)
                    {
                        color2 = _validColoring[j];
                        break;
                    }
                }
            }
            
            if (color2 == -1)
            {
                return false;
            }
        }

        return true;
    }
    
}