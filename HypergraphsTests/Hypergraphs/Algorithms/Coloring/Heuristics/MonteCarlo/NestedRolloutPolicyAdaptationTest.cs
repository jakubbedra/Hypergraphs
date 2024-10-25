using System.Diagnostics;
using Hypergraphs.Algorithms;
using Hypergraphs.Extensions;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.MonteCarlo;

public class NestedRolloutPolicyAdaptationTest
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
        NRPA nrpa = new NRPA(h, 10, 10, 3, vertices.ToArray());

        stopwatch.Start();
        int[]? coloring = nrpa.ComputeColoring();
        stopwatch.Stop();

        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";

        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));
    }
    
    [Test]
    public void ComputeColoring()
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

        // NestedRolloutPolicyAdaptation nrpa = new NestedRolloutPolicyAdaptation(h, 2, 10);
        int[] vertexOrder = new int[n];
        for (int i = 0; i < n; i++)
            vertexOrder[i] = i;
        NRPA nrpa = new NRPA(h, 2, 10, 3, vertexOrder, 0.01);
        
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        int[]? coloring = nrpa.ComputeColoring();
        stopwatch.Stop();
        
        TimeSpan elapsedTime = stopwatch.Elapsed;
        string time = $"Elapsed time: {elapsedTime.TotalMilliseconds} milliseconds";
        
        Assert.NotNull(coloring);
        Assert.True(validator.IsValid(h, coloring));

    }
    
}