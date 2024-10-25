using Hypergraphs.Algorithms;
using Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.Coloring.Heuristics.LovaszLocalLemma;

public class UniformHypergraphColoringTest
{
    [Test]
    public void ComputeColoringTest_SmallHypergraph()
    {
        int n = 10;
        int m = 5;
        int r = 4;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph hypergraph = generator.GenerateConnected(n, m, r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        UniformHypergraphColoring coloring = new UniformHypergraphColoring();

        int[] colors = coloring.ComputeColoring(hypergraph);
        bool result = validator.IsValid(hypergraph, colors);
        
        Assert.IsTrue(result);
    }
    
    [Test]
    public void ComputeColoringTest_MediumHypergraph()
    {
        int n = 40;
        int m = 24;
        int r = 5;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph hypergraph = generator.GenerateConnected(n, m, r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        UniformHypergraphColoring coloring = new UniformHypergraphColoring();

        int[] colors = coloring.ComputeColoring(hypergraph);
        bool result = validator.IsValid(hypergraph, colors);
        
        Assert.IsTrue(result);
    }
    
    [Test]
    public void ComputeColoringTest_BigHypergraph()
    {
        int n = 100;
        int m = 69;
        int r = 6;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph hypergraph = generator.GenerateConnected(n, m, r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        UniformHypergraphColoring coloring = new UniformHypergraphColoring();

        int[] colors = coloring.ComputeColoring(hypergraph);
        bool result = validator.IsValid(hypergraph, colors);
        
        Assert.IsTrue(result);
    }
}