using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class OtherGreedyTest : GreedyTestBase
{
      
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new OtherGreedy();
    }
    
    [Test]
    public void HyperstarTest()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2, 3 },
            new List<int> { 0, 1 },
            new List<int> { 0, 2 },
            new List<int> { 0, 3 },
        };
        int expectedColors = 2;
        int n = 4;
        int m = 4;
        int c = 1;

        Hypergraph hypergraph = HypergraphFactory.FromHyperEdgesList(n, edges);
        int[] coloring = _coloringAlgorithm.ComputeColoring(hypergraph);
        int numberOfColors = coloring.Distinct().Count();
        Assert.AreEqual(expectedColors, numberOfColors);
    }
    
}