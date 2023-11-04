using Hypergraphs.Graphs.Model;
using HypergraphsTests.Graphs.Model.Providers;

namespace HypergraphsTests.Graphs.Model;

public class GraphTest
{
    // vertex addition
    
    [Test]
    public void AddVertex_MatrixSizeN()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 7;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_LastRowAndColumnEmpty;

        g.AddVertex();

        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void AddVertex_MatrixSizeGreaterThanN()
    {
        Graph g = GraphProvider.SimpleGraph_MatrixBiggerThanN;

        int expectedN = 7;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_LastRowAndColumnEmpty;

        g.AddVertex();

        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // edge addition
    
    [Test]
    public void AddEdge_EdgeDoesNotExist()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 10;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_AddedEdge;

        g.AddEdge(0, 2);

        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void AddEdge_EdgeExists()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix;

        g.AddEdge(0, 1);

        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void AddEdge_WithWeight()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 10;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_AddedWeightedEdge;

        g.AddEdge(0, 2, 2137);

        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // vertex deletion
    
    [Test]
    public void DeleteVertex_LastVertex()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 5;
        int expectedM = 4;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_LastVertexDeleted;

        bool result = g.DeleteVertex(5);

        Assert.That(result, Is.True);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void DeleteVertex_FirstVertex()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 5;
        int expectedM = 6;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_FirstVertexDeleted;

        bool result = g.DeleteVertex(0);

        Assert.That(result, Is.True);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void DeleteVertex_NeitherFirstOrLastVertex()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 5;
        int expectedM = 5;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_FourthVertexDeleted;

        bool result = g.DeleteVertex(3);

        Assert.That(result, Is.True);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void DeleteVertex_VertexOutOfBounds_ToLargeN()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix;

        bool result = g.DeleteVertex(69);

        Assert.That(result, Is.False);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void DeleteVertex_VertexOutOfBounds_ToSmallN()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix;

        bool result = g.DeleteVertex(-10);

        Assert.That(result, Is.False);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // edge deletion

    [Test]
    public void DeleteEdge_EdgeExists()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 8;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix_EdgeDeleted;

        bool result = g.DeleteEdge(0, 5);

        Assert.That(result, Is.True);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void DeleteEdge_EdgeDoesNotExist()
    {
        Graph g = GraphProvider.SimpleGraph;

        int expectedN = 6;
        int expectedM = 9;
        int[,] expectedMatrix = GraphProvider.SimpleMatrix;

        bool result = g.DeleteEdge(0, 2);

        Assert.That(result, Is.False);
        Assert.That(g.N, Is.EqualTo(expectedN));
        Assert.That(g.M, Is.EqualTo(expectedM));
        Assert.That(g.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // weight change
    
     
}