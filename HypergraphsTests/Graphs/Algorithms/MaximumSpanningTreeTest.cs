using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Factory;
using Hypergraphs.Graphs.Model;

namespace HypergraphsTests.Graphs.Algorithms;

public class MaximumSpanningTreeTest
{
    [Test]
    public void MaximumSpanningTree_Path()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 3 },
            new List<int>() { 1, 3 },
            new List<int>() { 1, 2 },
            new List<int>() { 2, 4 },
            new List<int>() { 4, 5 },
        };
        List<int> weights = new List<int>() { 6, 9, 4, 2, 2 };
        int n = 6;
        Graph graph = GraphFactory.FromEdgesList(n, edges, weights);
        MaximumSpanningTree mst = new MaximumSpanningTree();
        TreeCheck check = new TreeCheck();

        Graph t = mst.Create(graph);
        bool result = check.Apply(t);

        Assert.That(result, Is.True);
        Assert.That(t.Matrix, Is.EqualTo(graph.Matrix));
    }

    [Test]
    public void MaximumSpanningTree_C4()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 1, 2 },
            new List<int>() { 2, 3 },
            new List<int>() { 3, 0 },
        };
        List<int> weights = new List<int>() { 2, 1, 3, 7 };
        int n = 4;
        Graph graph = GraphFactory.FromEdgesList(n, edges, weights);
        MaximumSpanningTree mst = new MaximumSpanningTree();
        TreeCheck check = new TreeCheck();
        int[,] expectedMatrix =
        {
            { 0, 2, 0, 7 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 3 },
            { 7, 0, 3, 0 },
        };

        Graph t = mst.Create(graph);
        bool result = check.Apply(t);

        Assert.That(result, Is.True);
        Assert.That(t.Matrix, Is.EqualTo(expectedMatrix));
    }
}