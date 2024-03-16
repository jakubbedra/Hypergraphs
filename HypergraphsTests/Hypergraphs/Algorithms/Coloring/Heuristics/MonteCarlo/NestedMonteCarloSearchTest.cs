using System.Diagnostics;
using Hypergraphs.Algorithms;
using Hypergraphs.Extensions;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.MonteCarlo;

public class NestedMonteCarloSearchTest
{
    [Test]
    public void ComputeColoring_SmallHyperstar()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 0, 3, 5},
            new List<int> { 0, 8 },
            new List<int> { 0, 4, 6, 7 },
            new List<int> { 0, 9 },
            new List<int> { 0, 7, 6 },
            new List<int> { 0, 2, 3, 4, 5 },
            new List<int> { 0, 1, 4, 5 },
            new List<int> { 0, 5, 9 },
            new List<int> { 0, 2, 1, 3, 7 },
        };
        int n = 10;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        List<int> vertices = new List<int>();
        for (int v = 0; v < h.N; v++)
            vertices.Add(v);
        vertices.Shuffle();
        
        Stopwatch stopwatch = new Stopwatch();
        NMCS nmcs = new NMCS(h, 2, 10, 3, vertices.ToArray());

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
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
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        Stopwatch stopwatch = new Stopwatch();
        NMCS nmcs = new NMCS(h, 2, 10, 3, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }

    [Test]
    public void ComputeColoring_MediumHypertree()
    {
        int n = 20;
        int m = 20;
        HypertreeGenerator generator = new HypertreeGenerator();
        Hypergraph h = generator.Generate(n, m);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        List<int> vertices = new List<int>();
        for (int v = 0; v < h.N; v++)
            vertices.Add(v);
        vertices.Shuffle();

        Stopwatch stopwatch = new Stopwatch();
        NMCS nmcs = new NMCS(h, 2, 10, 3, vertices.ToArray());
        NMCSColoring coloringAlgorithm = new NMCSColoring();

        stopwatch.Start();
        int[]? coloring = coloringAlgorithm.ComputeColoring(h);
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }

    [Test]
    public void ComputeColoring_RandomConnectedUniform_Small()
    {
        int n = 20;
        int m = 100;
        int r = 3;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph h = generator.GenerateConnected(n,m,r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        List<int> vertices = new List<int>();
        for (int v = 0; v < h.N; v++)
            vertices.Add(v);
        vertices.Shuffle();

        NMCSColoring coloringAlgorithm = new NMCSColoring();
        Stopwatch stopwatch = new Stopwatch();
        NMCS nmcs = new NMCS(h, 2, 10, 3, vertices.ToArray());

        stopwatch.Start();
        int[]? coloring = coloringAlgorithm.ComputeColoring(h);
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
        NMCS nmcs = new NMCS(h, 2, 10, 3, vertexOrder);

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
        NMCS nmcs = new NMCS(h, 2, 10, 3, vertexOrder);

        stopwatch.Start();
        int[]? coloring = nmcs.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
}