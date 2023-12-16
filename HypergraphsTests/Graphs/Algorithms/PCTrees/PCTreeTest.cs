using Hypergraphs.Graphs.Algorithms.PCTrees;
using HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

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

    [Test]
    public void GetPermutation_OnePNodeTree()
    {
        
    }

    [Test]
    public void GetPermutation_OneCNodeTree()
    {
        
    }

    [Test]
    public void GetPermutation_TwoPNodeTree()
    {
        
    }
    
    [Test]
    public void GetPermutation_TwoCNodeTree()
    {
        
    }
    
    [Test]
    public void GetPermutation_OnePNodeOneCNodeTree()
    {
        
    }

    [Test]
    public void GetPermutation_MoreComplexTree()
    {
        
    }

    [Test]
    public void GetPermutation_EvenMoreComplexTree()
    {
        // todo: tutej to z artykulu dac
        PCTree tree = PCTreesProvider.VeryComplexCaseLeavesLabeled();

        int[]? permutation = tree.GetPermutation();

        Assert.That(permutation, Is.EqualTo(new int[] {1,2,3,4}));
    }
    
}
