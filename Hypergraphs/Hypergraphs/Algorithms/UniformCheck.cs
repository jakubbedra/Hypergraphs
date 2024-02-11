using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class UniformCheck : PropertyCheck<Hypergraph>
{
    public bool Apply(Hypergraph hypergraph)
    {
        int n = hypergraph.N;
        int m = hypergraph.M;
        int[,] matrix = hypergraph.Matrix;
        int r = -1;
        for (int e = 0; e < m; e++)
        {
            int edgeSize = 0;
            for (int v = 0; v < n; v++)
            {
                if (matrix[v, e] == 1)
                    edgeSize++;
            }

            if (r == -1) r = edgeSize;
            else if (edgeSize != r) return false;
        }

        return true;
    }
}