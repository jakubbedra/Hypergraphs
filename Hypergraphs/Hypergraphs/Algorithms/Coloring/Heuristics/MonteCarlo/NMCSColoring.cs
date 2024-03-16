using Hypergraphs.Common.Algorithms;
using Hypergraphs.Extensions;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NMCSColoring : BaseColoring<Hypergraph>
{
    private readonly int NumberOfEpochs = 10;
    private readonly int MaxDepth = 3;
    
    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        List<int> vertices = new List<int>();
        for (int v = 0; v < hypergraph.N; v++)
            vertices.Add(v);
        vertices.Shuffle();

        for (int c = 2; c < hypergraph.N; c++)
        {
            NMCS nmcs = new NMCS(hypergraph, c, NumberOfEpochs, MaxDepth, vertices.ToArray());
            int[]? colors = nmcs.ComputeColoring();
            if (colors != null) return colors;
        }

        return vertices.ToArray();// return n coloring
    }
    
}
