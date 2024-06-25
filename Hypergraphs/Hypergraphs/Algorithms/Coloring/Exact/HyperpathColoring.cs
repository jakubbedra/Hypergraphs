using Hypergraphs.Common.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HyperpathColoring : BaseColoring<Hypergraph>
{
    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        ConsecutiveOnes consecutiveOnes = new ConsecutiveOnes(
            hypergraph.Matrix, hypergraph.N, hypergraph.M
        );
        int[]? permutation = consecutiveOnes.GetPermutation();
        while (permutation == null || !IsPathHost(permutation, hypergraph))
        {
            var hyperpathGenerator = new HyperpathGenerator();
            hypergraph = hyperpathGenerator.Generate(hypergraph.N, hypergraph.M);
            consecutiveOnes = new ConsecutiveOnes(
                hypergraph.Matrix, hypergraph.N, hypergraph.M
            );
            permutation = consecutiveOnes.GetPermutation();
        }
        int[] colors = new int[hypergraph.N];
        for (var i = 0; i < colors.Length; i++)
            colors[i] = -1;
        for (var i = 0; i < colors.Length; i++)
            colors[permutation[i]] = i % 2;

        return colors;
    }
    
    private bool IsPathHost(int[] permutation, Hypergraph hypergraph)
    {
        for (var i = 0; i < permutation.Length - 1; i++)
        {
            List<int> e1 = hypergraph.GetVertexEdges(permutation[i]);
            List<int> e2 = hypergraph.GetVertexEdges(permutation[i+1]);
            if (!e1.Intersect(e2).Any()) return false;
        }

        return true;
    }
}