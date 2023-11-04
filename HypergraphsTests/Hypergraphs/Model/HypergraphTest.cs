using Hypergraphs.Model;
using HypergraphsTests.Model.Providers;

namespace HypergraphsTests.Model;

public class HypergraphTest
{
    // ======================== Adding Vertices ========================

    [Test]
    public void AddVertex_MatrixSizeEqualN()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 8;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastRowEmpty;

        h.AddVertex();

        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void AddVertex_MatrixSizeOneGreaterThanN()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeOneGreaterThanN;

        int expectedN = 8;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastRowEmpty;

        h.AddVertex();

        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    // ======================== Weak Deleting Vertices ========================

    [Test]
    public void WeakDeleteVertex_LastVertex()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 6;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedLastVertex;
        int vertexToDelete = h.N - 1;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void WeakDeleteVertex_FirstVertex()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 6;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedFirstVertex;
        int vertexToDelete = 0;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void WeakDeleteVertex_NeitherLastOrFirst()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 6;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedVertexWithIndex4;
        int vertexToDelete = 4;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void WeakDeleteVertex_ValueNegative()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 7;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix;
        int vertexToDelete = -1;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.False);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void WeakDeleteVertex_ValueToLarge_MatrixSizeN()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedN = 7;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix;
        int vertexToDelete = h.N + 100;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.False);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    [Test]
    public void WeakDeleteVertex_ValueToLarge_MatrixSizeOneGreaterThanN()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeOneGreaterThanN;

        int expectedN = 7;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastRowEmpty;
        int vertexToDelete = h.N;

        bool deleted = h.WeakDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.False);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    // ======================== Strong Deleting Vertices ========================

    [Test]
    public void StrongDeleteVertex_NoEdgeAdjacent()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongVertexDeletion;

        int expectedN = 6;
        int expectedM = 5;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongVertexDeletion_DeletedVertexInNoEdge;
        int vertexToDelete = 0;

        bool deleted = h.StrongDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteVertex_OneEdgeAdjacent()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongVertexDeletion;

        int expectedN = 6;
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongVertexDeletion_DeletedVertexInOneEdge;
        int vertexToDelete = 1;

        bool deleted = h.StrongDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteVertex_MultipleEdgesAdjacent()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongVertexDeletion;

        int expectedN = 6;
        int expectedM = 2;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongVertexDeletion_DeletedVertexInSomeEdges;
        int vertexToDelete = 2;

        bool deleted = h.StrongDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteVertex_AllEdgesAdjacent()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongVertexDeletion;

        int expectedN = 6;
        int expectedM = 0;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongVertexDeletion_DeletedVertexInAllEdges;
        int vertexToDelete = 3;

        bool deleted = h.StrongDeleteVertex(vertexToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }

    // ======================== Adding Edges ========================
    
    [Test]
    public void AddEdge_MatrixSizeEqualM()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedM = 5;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastColumnEmpty;

        h.AddEdge();
        
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void AddEdge_MatrixSizeOneGreaterThanM()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeOneGreaterThanN;

        int expectedM = 5;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastColumnEmpty;

        h.AddEdge();
        
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // ======================== Weak Deleting Edges ========================
    
    [Test]
    public void WeakDeleteEdge_LastEdge()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedM = 3;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedLastEdge;
        int edgeToDelete = h.M - 1;
        
        bool deleted = h.WeakDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void WeakDeleteEdge_FirstEdge()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedM = 3;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedFirstEdge;
        int edgeToDelete = 0;
        
        bool deleted = h.WeakDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void WeakDeleteEdge_NeitherLastOrFirst()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;

        int expectedM = 3;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_DeletedEdgeWithIndex2;
        int edgeToDelete = 2;
        
        bool deleted = h.WeakDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void WeakDeleteEdge_ValueNegative()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;
    
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix;
        int edgeToDelete = -1;
    
        bool deleted = h.WeakDeleteEdge(edgeToDelete);
    
        Assert.That(deleted, Is.False);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void WeakDeleteEdge_ValueToLarge_MatrixSizeM()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeEqualNM;
    
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix;
        int edgeToDelete = h.M + 2137;
    
        bool deleted = h.WeakDeleteEdge(edgeToDelete);
    
        Assert.That(deleted, Is.False);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void WeakDeleteEdge_ValueToLarge_MatrixSizeOneGreaterThanM()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_MatrixOfSizeOneGreaterThanM;
    
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrixWithLastColumnEmpty;
        int edgeToDelete = h.M;
    
        bool deleted = h.WeakDeleteEdge(edgeToDelete);
    
        Assert.That(deleted, Is.False);
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    // ======================== Strong Deleting Edges ========================

    [Test]
    public void StrongDeleteEdge_NoVertexInside()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongEdgeDeletion;

        int expectedN = 7;
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithNoVertex;
        int edgeToDelete = 0;

        bool deleted = h.StrongDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteEdge_OneVertexInside()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongEdgeDeletion;

        int expectedN = 6;
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithOneVertex;
        int edgeToDelete = 1;

        bool deleted = h.StrongDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteEdge_SomeVertivesInside()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongEdgeDeletion;

        int expectedN = 4;
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithSomeVertices;
        int edgeToDelete = 2;

        bool deleted = h.StrongDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    [Test]
    public void StrongDeleteEdge_AllVertivesInside()
    {
        Hypergraph h = HypergraphProvider.Hypergraph_StrongEdgeDeletion;

        int expectedN = 0;
        int expectedM = 4;
        int[,] expectedMatrix = HypergraphProvider.SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithAllVertices;
        int edgeToDelete = 3;

        bool deleted = h.StrongDeleteEdge(edgeToDelete);

        Assert.That(deleted, Is.True);
        Assert.That(h.N, Is.EqualTo(expectedN));
        Assert.That(h.M, Is.EqualTo(expectedM));
        Assert.That(h.Matrix, Is.EqualTo(expectedMatrix));
    }
    
    
}