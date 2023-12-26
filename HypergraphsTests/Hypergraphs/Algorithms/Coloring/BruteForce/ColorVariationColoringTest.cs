using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class ColorVariationColoringTest
{
    
    [Test]
    public void ComputeColoring_SmallHyperpath()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        ColorVariationColoring coloring = new ColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
    [Test]
    public void ComputeColoring_SmallHypertree()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int> { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        ColorVariationColoring coloring = new ColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }

}