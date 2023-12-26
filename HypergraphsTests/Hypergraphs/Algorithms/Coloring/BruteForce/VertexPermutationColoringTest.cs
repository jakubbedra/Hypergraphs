using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class VertexPermutationColoringTest
{
    
    [Test]
    public void ComputeColoring_2ColorableHypergraph()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        VertexPermutationColoring coloring = new VertexPermutationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
}