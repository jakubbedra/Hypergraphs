using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HypertreeCheckTest
{
    [Test]
    public void HypertreeCheck_IsHypertree_Hypertree()
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
        HypertreeCheck hypertreeCheck = new HypertreeCheck();

        bool result = hypertreeCheck.Apply(h);

        Assert.That(result, Is.True);
    }

    [Test]
    public void HypertreeCheck_IsHypertree_LargerHypertree()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 5, 6, 7 },
            new List<int> { 5, 6 },
            new List<int> { 8, 9 },
            new List<int> { 6, 7, 8, 9 },
            new List<int> { 9, 10, 11, 12 },
            new List<int> { 11, 12 },
        };
        int n = 13;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        HypertreeCheck hypertreeCheck = new HypertreeCheck();

        bool result = hypertreeCheck.Apply(h);

        Assert.That(result, Is.True);
    }
    
    [Test]
    public void HypertreeCheck_IsNotHypertree_Random()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 5, 6, 7 },
            new List<int> { 5, 6 },
            new List<int> { 8, 9 },
            new List<int> { 6, 7, 8, 9 },
            new List<int> { 9, 10, 11, 12 },
            new List<int> { 11, 12 },
            new List<int> { 0, 10 },
        };
        int n = 13;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        HypertreeCheck hypertreeCheck = new HypertreeCheck();

        bool result = hypertreeCheck.Apply(h);

        Assert.That(result, Is.False);
    }
    
}
