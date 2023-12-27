using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.MonteCarlo;

public class NestedMonteCarloSearchTest
{
    [Test]
    public void ComputeColoring_Sample()
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
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        // todo: zrobic wersje z hashmapa
        NestedMonteCarloSearch nmcs = new NestedMonteCarloSearch(h, 2, 10);

        int[]? coloring = nmcs.ComputeColoring();
     
        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
}