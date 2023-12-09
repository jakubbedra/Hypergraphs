using Hypergraphs.Graphs.Algorithms.PCTrees;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees;

public class PCTreeTest
{
    [Test]
    public void ConstructTest_SampleValue()
    {
        int[,] matrix = new int[,]
        {
            { 0, 1, 1, 0, 1, 0, 0, 0, 1 },
            { 0, 0, 1, 0, 1, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 0 },
            { 1, 0, 0, 0, 0, 1, 1, 0, 0 },
            { 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        };
        int rows = 6;
        int columns = 9;

        PCTree tree = new PCTree(matrix, rows, columns);
        
        tree.Construct();

        Assert.IsNull(tree.GetPermutation());
    }
    
}