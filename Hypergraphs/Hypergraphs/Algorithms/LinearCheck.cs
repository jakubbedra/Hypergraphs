using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class LinearCheck : PropertyCheck<Hypergraph>
{
    public bool Apply(Hypergraph hypergraph)
    {
        int n = hypergraph.N;
        int m = hypergraph.M;
        for (int e1 = 0; e1 < m; e1++)
        {
            for (int e2 = e1+1; e2 < m; e2++)
            {
                if (hypergraph.EdgeIntersection(e1, e2).Count > 1) return false;
            }
        }

        return true;
    }
}