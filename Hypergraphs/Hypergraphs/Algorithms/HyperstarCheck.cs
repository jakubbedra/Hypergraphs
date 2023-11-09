using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HyperstarCheck : PropertyCheck<Hypergraph>
{
    public bool Apply(Hypergraph h)
    {
        for (int v = 0; v < h.N; v++)
        {
            bool isCenter = true;
            for (int e = 0; e < h.M && isCenter; e++)
                if (h.Matrix[v, e] == 0)
                    isCenter = false;

            if (isCenter) return true;
        }

        return false;
    }
}