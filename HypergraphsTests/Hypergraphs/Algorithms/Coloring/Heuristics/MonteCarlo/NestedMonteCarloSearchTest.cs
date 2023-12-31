using System.Diagnostics;
using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.MonteCarlo;

public class NestedMonteCarloSearchTest
{
    
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
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        Stopwatch stopwatch = new Stopwatch();
        NMCS2 nmcs = new NMCS2(h, 2, 10, 3, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
    [Test]
    public void ComputeColoring_RandomConnectedUniform_Medium()
    {
        int n = 30;
        int m = 30;
        int r = 4;
        int[] vertexOrder = new int[n];
        for (var i = 0; i < vertexOrder.Length; i++)
            vertexOrder[i] = i;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph h = generator.GenerateConnected(n,m,r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        Stopwatch stopwatch = new Stopwatch();
        NMCS2 nmcs = new NMCS2(h, 2, 10, 3, vertexOrder);

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
    [Test]
    public void ComputeColoring_RandomUniform_Large()
    {
        int n = 69;
        int m = 420;
        int r = 4;
        int[] vertexOrder = new int[n];
        for (var i = 0; i < vertexOrder.Length; i++)
            vertexOrder[i] = i;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph h = generator.GenerateSimple(n,m,r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        Stopwatch stopwatch = new Stopwatch();
        NMCS2 nmcs = new NMCS2(h, 2, 10, 3, vertexOrder);

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
}