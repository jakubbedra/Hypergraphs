using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HyperstarCheckTest
{
    [Test]
    public void IsHyperstar_BasicHyperstar_True()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1, 2, 3 },
            new List<int>() { 0, 4 },
            new List<int>() { 0, 5 },
            new List<int>() { 0, 6 },
        };
        int n = 7;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HyperstarCheck check = new HyperstarCheck();
        bool result = check.Apply(h);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHyperstar_Hyperstar_True()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 2, 1, 0, 3 },
            new List<int>() { 2, 0, 5, 4 },
            new List<int>() { 2, 3, 4 },
            new List<int>() { 2, 1, 6 },
            new List<int>() { 2, 7, 4 },
            new List<int>() { 2, 5, 3, 4, 1, 0, 6 },
        };
        int n = 8;

        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HyperstarCheck check = new HyperstarCheck();
        bool result = check.Apply(h);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHyperstar_IntervalHypergraph_True()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1, 2, 3, 4 },
            new List<int>() { 3, 4, 5, 6, 7 },
        };
        int n = 8;

        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HyperstarCheck check = new HyperstarCheck();
        bool result = check.Apply(h);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHyperstar_Hypertree_False()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1, 2, 3 },
            new List<int>() { 3, 4, 5 },
            new List<int>() { 2, 6, 7 },
        };
        int n = 8;

        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HyperstarCheck check = new HyperstarCheck();
        bool result = check.Apply(h);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHyperstar_Random_False()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1, 2, 4 },
            new List<int>() { 7, 1, 6, 4, 8 },
            new List<int>() { 3, 4, 5 },
            new List<int>() { 2, 6, 7 },
            new List<int>() { 9, 6, 8, 3 },
            new List<int>() { 2, 1, 3, 6, 4 },
        };
        int n = 10;

        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HyperstarCheck check = new HyperstarCheck();
        bool result = check.Apply(h);

        Assert.That(result, Is.False);
    }

}