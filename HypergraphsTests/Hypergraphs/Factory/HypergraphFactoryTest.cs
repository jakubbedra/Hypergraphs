using Hypergraphs.Model;

namespace HypergraphsTests.Model;

public class HypergraphFactoryTest
{
    [Test]
    public void FromHyperEdgesList_SmallHypergraph()
    {
        int n = 6;
        int m = 3;
        List<List<int>> hyperEdges = new List<List<int>>
        {
            new List<int> { 1, 2, 3, 4 },
            new List<int> { 4, 0 },
            new List<int> { 3, 2, 1, 5 }
        };
        int[,] expected =
        {
            {0,1,0}, 
            {1,0,1}, 
            {1,0,1}, 
            {1,0,1}, 
            {1,1,0}, 
            {0,0,1} 
        };

        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperEdges);

        Assert.That(h.Matrix, Is.EqualTo(expected));
    }
}