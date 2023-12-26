using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

public class HyperstarColoring : BaseColoring<Hypergraph>
{
    public override int[] ComputeColoring(Hypergraph h)
    {
        // all vertices get color 0 at first
        _validColoring = new int[h.N];
        for (var v = 0; v < h.N; v++)
            _validColoring[v] = 0;
        
        // find first vertex of the center, and give it a different color
        for (int v = 0; v < h.N; v++)
        {
            bool isCenter = true;
            for (int e = 0; e < h.M && isCenter; e++)
                if (h.Matrix[v, e] == 0)
                    isCenter = false;

            if (isCenter)
            {
                _validColoring[v] = 1;
                return _validColoring;
            }
        }
        
        throw new Exception("Given hypergraph is not a hyperstar.");
    }
}