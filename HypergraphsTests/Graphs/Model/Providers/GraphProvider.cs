using Hypergraphs.Graphs.Model;

namespace HypergraphsTests.Graphs.Model.Providers;

public class GraphProvider
{
    public static int[,] SimpleMatrix => new int[,]
    {
        { 0, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1 },
        { 0, 0, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1, 1 },
        { 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0 },
    };

    public static int[,] SimpleMatrix_LastRowAndColumnEmpty => new int[,]
    {
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 0, 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0, 1, 0 },
        { 1, 1, 0, 0, 1, 1, 0 },
        { 0, 0, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
    };
    
    public static int[,] SimpleMatrix_AddedEdge => new int[,]
    {
        { 0, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1, 1 },
        { 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0 },
    };

    public static int[,] SimpleMatrix_AddedWeightedEdge => new int[,]
    {
        { 0, 1, 2137, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1 },
        { 2137, 0, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1, 1 },
        { 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0 },
    };
    
    public static int[,] SimpleMatrix_LastVertexDeleted => new int[,]
    {
        { 0, 1, 0, 1, 0, 0 },
        { 1, 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 1, 1, 0, 0, 1, 0 },
        { 0, 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
    };
    
    public static int[,] SimpleMatrix_FirstVertexDeleted => new int[,]
    {
        { 0, 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 1, 0 },
        { 1, 0, 0, 1, 1, 0 },
        { 0, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
    };

    public static int[,] SimpleMatrix_FourthVertexDeleted => new int[,]
    {
        { 0, 1, 0, 0, 1, 0 },
        { 1, 0, 0, 0, 1, 0 },
        { 0, 0, 0, 0, 1, 0 },
        { 0, 0, 0, 0, 1, 0 },
        { 1, 1, 1, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
    };
    
    public static int[,] SimpleMatrix_EdgeDeleted => new int[,]
    {
        { 0, 1, 0, 1, 0, 0 },
        { 1, 0, 0, 1, 0, 1 },
        { 0, 0, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1, 1 },
        { 0, 0, 0, 1, 0, 1 },
        { 0, 1, 1, 1, 1, 0 },
    };
    
    public static Graph SimpleGraph => new Graph()
    {
        Matrix = SimpleMatrix,
        N = 6,
        M = 9
    };

    public static Graph SimpleGraph_MatrixBiggerThanN => new Graph()
    {
        Matrix = SimpleMatrix_LastRowAndColumnEmpty,
        N = 6,
        M = 9
    };
    
}