using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Factory;
using Hypergraphs.Graphs.Model;

namespace HypergraphsTests.Graphs.Algorithms;

public class GraphConnectivityCheckTest
{
    [Test]
    public void Apply_IsConnected()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 3 },
            new List<int>() { 0, 1 },
            new List<int>() { 0, 5 },
            new List<int>() { 1, 3 },
            new List<int>() { 1, 4 },
            new List<int>() { 2, 3 },
            new List<int>() { 3, 4 },
            new List<int>() { 4, 5 },
        };
        int n = 6;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        GraphConnectivityCheck check = new GraphConnectivityCheck();
        bool result = check.Apply(graph);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Apply_IsDisconnected_OneIsolatedVertex()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 3 },
            new List<int>() { 0, 1 },
            new List<int>() { 0, 5 },
            new List<int>() { 1, 3 },
            new List<int>() { 1, 4 },
            new List<int>() { 2, 3 },
            new List<int>() { 3, 4 },
            new List<int>() { 4, 5 },
        };
        int n = 7;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        GraphConnectivityCheck check = new GraphConnectivityCheck();
        bool result = check.Apply(graph);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Apply_IsDisconnected_EmptyGraph()
    {
        List<List<int>> edges = new List<List<int>>() { };
        int n = 8;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        GraphConnectivityCheck check = new GraphConnectivityCheck();
        bool result = check.Apply(graph);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Apply_IsDisconnected_TwoConnectedComponents()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 0, 2 },
            new List<int>() { 0, 3 },
            new List<int>() { 1, 2 },
            new List<int>() { 1, 3 },
            new List<int>() { 2, 3 },
            new List<int>() { 4, 5 },
            new List<int>() { 5, 6 },
            new List<int>() { 4, 6 },
        };
        int n = 7;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        GraphConnectivityCheck check = new GraphConnectivityCheck();
        bool result = check.Apply(graph);

        Assert.That(result, Is.False);
    }
    
}