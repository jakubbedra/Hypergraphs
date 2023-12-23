using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class GreedyColoringTest
{
    [Test]
    public void ComputeColoring_2ColorableHypergraph()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int>() { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        GreedyColoring coloring = new GreedyColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
    [Test]
    public void ComputeColoring_RandomUniformGraph()
    {
        int n = 5;
        int m = 10;
        int r = 2;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph h = generator.GenerateSimple(n, m, r);
        GreedyColoring coloring = new GreedyColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));//TODO: FIX
    }
    
    // todo: do for all generators when they will be implemented
    
}