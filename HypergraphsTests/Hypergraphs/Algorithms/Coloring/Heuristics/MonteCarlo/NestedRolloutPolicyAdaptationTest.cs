using System.Diagnostics;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms.MonteCarlo;

public class NestedRolloutPolicyAdaptationTest
{

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

        // todo: zrobic wersje z hashmapa
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