using Hypergraphs.Model;

namespace HypergraphsTests.Model.Providers;

public class HypergraphProvider
{
    public static int[,] SimpleMatrix => new int[,]
    {
        { 1, 0, 0, 1 },
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 }
    };

    public static int[,] SimpleMatrix_DeletedLastVertex => new int[,]
    {
        { 1, 0, 0, 1 },
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_DeletedFirstVertex => new int[,]
    {
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_DeletedVertexWithIndex4 => new int[,]
    {
        { 1, 0, 0, 1 },
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 1 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrixWithLastRowEmpty => new int[,]
    {
        { 1, 0, 0, 1 },
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrixWithLastColumnEmpty => new int[,]
    {
        { 1, 0, 0, 1, 0 },
        { 0, 1, 1, 0, 0 },
        { 0, 1, 1, 0, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 0, 1, 0, 0 },
        { 0, 1, 0, 0, 0 },
        { 0, 0, 1, 0, 0 }
    };

    public static int[,] SimpleMatrix_DeletedLastEdge => new int[,]
    {
        { 1, 0, 0, 0 },
        { 0, 1, 1, 0 },
        { 0, 1, 1, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 }
    };

    public static int[,] SimpleMatrix_DeletedFirstEdge => new int[,]
    {
        { 0, 0, 1, 0 },
        { 1, 1, 0, 0 },
        { 1, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 }
    };

    public static int[,] SimpleMatrix_DeletedEdgeWithIndex2 => new int[,]
    {
        { 1, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 0, 1, 0, 0 },
        { 1, 0, 1, 0 },
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongVertexDeletion => new int[,]
    {
        { 0, 0, 0, 0, 0 }, // none
        { 0, 1, 0, 0, 0 }, // one
        { 1, 0, 1, 1, 0 }, // some
        { 1, 1, 1, 1, 1 }, // all
        { 0, 0, 0, 1, 1 },
        { 0, 1, 1, 1, 0 },
        { 0, 1, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongVertexDeletion_DeletedVertexInNoEdge => new int[,]
    {
        { 0, 1, 0, 0, 0 }, // one
        { 1, 0, 1, 1, 0 }, // some
        { 1, 1, 1, 1, 1 }, // all
        { 0, 0, 0, 1, 1 },
        { 0, 1, 1, 1, 0 },
        { 0, 1, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongVertexDeletion_DeletedVertexInOneEdge => new int[,]
    {
        { 0, 0, 0, 0, 0 }, // none
        { 1, 1, 1, 0, 0 }, // some
        { 1, 1, 1, 1, 0 }, // all
        { 0, 0, 1, 1, 0 },
        { 0, 1, 1, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongVertexDeletion_DeletedVertexInSomeEdges => new int[,]
    {
        { 0, 0, 0, 0, 0 }, // none
        { 1, 0, 0, 0, 0 }, // one
        { 1, 1, 0, 0, 0 }, // all
        { 0, 1, 0, 0, 0 },
        { 1, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongVertexDeletion_DeletedVertexInAllEdges => new int[,]
    {
        { 0, 0, 0, 0, 0 }, // none
        { 0, 0, 0, 0, 0 }, // one
        { 0, 0, 0, 0, 0 }, // some
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };
    
    
    public static int[,] SimpleMatrix_StrongEdgeDeletion => new int[,]
    {
        { 0, 0, 0, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 1, 1 },
        { 0, 0, 1, 1, 1 },
        { 0, 0, 0, 1, 1 },
        { 0, 0, 1, 1, 1 },
        { 0, 0, 0, 1, 0 }
    };

    public static int[,] SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithNoVertex => new int[,]
    {
        { 0, 0, 1, 0, 0 },
        { 1, 0, 1, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 1, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithOneVertex => new int[,]
    {
        { 0, 0, 1, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 1, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithSomeVertices => new int[,]
    {
        { 0, 0, 1, 0, 0 },
        { 0, 1, 1, 0, 0 },
        { 0, 0, 1, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };

    public static int[,] SimpleMatrix_StrongEdgeDeletion_DeletedEdgeWithAllVertices => new int[,]
    {
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
    };


    public static Hypergraph Hypergraph_MatrixOfSizeEqualNM => new Hypergraph
    {
        N = 7,
        M = 4,
        Matrix = SimpleMatrix
    };

    public static Hypergraph Hypergraph_MatrixOfSizeOneGreaterThanN => new Hypergraph
    {
        N = 7,
        M = 4,
        Matrix = SimpleMatrixWithLastRowEmpty
    };

    public static Hypergraph Hypergraph_MatrixOfSizeOneGreaterThanM => new Hypergraph
    {
        N = 7,
        M = 4,
        Matrix = SimpleMatrixWithLastColumnEmpty
    };

    public static Hypergraph Hypergraph_StrongVertexDeletion => new Hypergraph
    {
        N = 7,
        M = 5,
        Matrix = SimpleMatrix_StrongVertexDeletion
    };
    
    public static Hypergraph Hypergraph_StrongEdgeDeletion => new Hypergraph
    {
        N = 7,
        M = 5,
        Matrix = SimpleMatrix_StrongEdgeDeletion
    };
}