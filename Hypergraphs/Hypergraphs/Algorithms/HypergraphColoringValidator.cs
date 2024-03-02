using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HypergraphColoringValidator
{
    public bool IsValid(Hypergraph h, int[] coloring)
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
                        color1 = coloring[j];
                    }
                    else if (coloring[j] != color1)
                    {
                        color2 = coloring[j];
                        break;
                    }
                }
            }
            
            if (color2 == -1) return false;
        }

        return true;
    }

}