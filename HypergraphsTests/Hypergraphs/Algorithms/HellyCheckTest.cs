using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HellyCheckTest
{
    [Test]
    public void HellyCheck_HypergraphIsHelly_Interval()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);

        HellyCheck check = new HellyCheck();
        bool result = check.IsHelly(h);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void HellyCheck_HypergraphIsHelly_Hypertree()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);

        HellyCheck check = new HellyCheck();
        bool result = check.IsHelly(h);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void HellyCheck_HypergraphIsNotHelly()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1 },
            new List<int> { 0, 2, 3 },
            new List<int> { 1, 3 },
            new List<int> { 5, 4, 3 },
            new List<int> { 5, 6, 7 },
        };
        int n = 8;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);

        HellyCheck check = new HellyCheck();
        bool result = check.IsHelly(h);
        
        Assert.That(result, Is.False);
    }
    
}