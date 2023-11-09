using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HypergraphConnectivityCheckTest
{
    [Test]
    public void Apply_Connected()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int> { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphConnectivityCheck check = new HypergraphConnectivityCheck();
        
        bool result = check.Apply(h);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void Apply_IsolatedVertex()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int> { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 12;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphConnectivityCheck check = new HypergraphConnectivityCheck();
        
        bool result = check.Apply(h);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void Apply_MultipleConnectedComponents()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4, 5, 6 },
            new List<int> { 6, 7, 8 },
            new List<int> { 10, 11, 12 },
            new List<int> { 14, 15, 16 },
            new List<int> { 14, 13, 4, 9, 10 },
            new List<int> { 17, 18, 19 },
            new List<int> { 20, 21, 22, 23 },
            new List<int> { 24, 25, 26 },
            new List<int> { 27, 28, 25 },
        };
        int n = 29;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphConnectivityCheck check = new HypergraphConnectivityCheck();
        
        bool result = check.Apply(h);
        
        Assert.That(result, Is.False);
    }
    
}