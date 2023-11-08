using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Factory;
using Hypergraphs.Graphs.Model;

namespace HypergraphsTests.Graphs.Algorithms;

public class ChordalityCheckTest
{
    [Test]
    public void ChordalityCheck_IsChordal_Tree()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 3 },
            new List<int>() { 1, 3 },
            new List<int>() { 2, 3 },
            new List<int>() { 3, 4 },
            new List<int>() { 4, 5 },
        };
        int n = 6;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        ChordalityCheck check = new ChordalityCheck();

        bool result = check.Apply(graph);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void ChordalityCheck_IsChordal_Triangle()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 1, 2 },
            new List<int>() { 2, 0 },
        };
        int n = 3;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        ChordalityCheck check = new ChordalityCheck();

        bool result = check.Apply(graph);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void ChordalityCheck_IsChordal_Random()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 3 },
            new List<int>() { 0, 4 },
            new List<int>() { 0, 5 },
            new List<int>() { 0, 6 },
            new List<int>() { 3, 1 },
            new List<int>() { 3, 2 },
            new List<int>() { 3, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4, 5 },
            new List<int>() { 6, 7 },
        };
        int n = 8;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        ChordalityCheck check = new ChordalityCheck();

        bool result = check.Apply(graph);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void ChordalityCheck_IsNotChordal_C4()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 1, 2 },
            new List<int>() { 2, 3 },
            new List<int>() { 3, 0 },
        };
        int n = 4;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        ChordalityCheck check = new ChordalityCheck();

        bool result = check.Apply(graph);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void ChordalityCheck_IsNotChordal_Random()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 0, 2 },
            new List<int>() { 0, 6 },
            new List<int>() { 1, 4 },
            new List<int>() { 1, 3 },
            new List<int>() { 1, 5 },
            new List<int>() { 1, 4 },
            new List<int>() { 2, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 3, 9 },
            new List<int>() { 6, 7 },
            new List<int>() { 7, 8 },
            new List<int>() { 5, 8 },
        };
        int n = 10;
        Graph graph = GraphFactory.FromEdgesList(n, edges);
        ChordalityCheck check = new ChordalityCheck();

        bool result = check.Apply(graph);
        
        Assert.That(result, Is.False);
    }
    
}
