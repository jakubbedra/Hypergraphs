using Hypergraphs.Graphs.Algorithms.PCTrees;
using HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees;

public class PCTreeTest
{
  
    [Test]
    public void ConstructTest_SampleValue_HasCircularOnes()
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

        bool result = tree.Construct();

        Assert.True(result);
        int[]? permutation = tree.GetPermutation();
        Assert.NotNull(permutation);
        Assert.AreEqual(new int[] { 2, 8, 0, 6, 5, 7, 3, 1, 4 }, permutation);
    }

    [Test]
    public void ConstructTest_SampleValue2()
    {
        int[,] matrix = new int[,]
        {
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 0, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
        };
        int rows = 14;
        int columns = 10;

        PCTree tree = new PCTree(matrix, rows, columns);

        bool result = tree.Construct();

        Assert.True(result);
        int[]? permutation = tree.GetPermutation();
        Assert.NotNull(permutation);
        Assert.AreEqual(new int[] { 5, 9, 8, 7, 6, 1, 2, 0, 3, 4 }, permutation);
    }

    [Test]
    public void ConstructTest_SampleValue2_ReorderedColumns()
    {
        int[,] matrix = new int[,]
        { //  5, 6, 1, 3, 4, 2, 7, 8, 0, 9, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, },
            { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, },
            { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, },
            { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, },
            { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, },
            { 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, },
            { 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, },
            { 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, },
            { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, },
            { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, },
            { 1, 1, 0, 0, 0, 0, 1, 1, 0, 1, },
        };
        
        //int[,] matrix2 = new int[,]
        //{ //  9, 7, 6, 1, 0, 4, 3, 8, 5, 2, 
        //    { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, },
        //    { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, },
        //    { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, },
        //    { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, },
        //    { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, },
        //    { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, },
        //    { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, },
        //    { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, },
        //    { 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, },
        //    { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, },
        //    { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, },
        //    { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, },
        //    { 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, },
        //    { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, },
        //};
        int rows = 14;
        int columns = 10;

        PCTree tree = new PCTree(matrix, rows, columns);

        bool result = tree.Construct();

        Assert.True(result);
        int[]? permutation = tree.GetPermutation();
        Assert.NotNull(permutation);
        Assert.AreEqual(new int[] { 9, 7, 6, 1, 0, 4, 3, 8, 5, 2 }, permutation);
    }

    [Test]
    public void GetPermutation_OnePNodeTree()
    {
        // todo:  to jeszcze zrobic testy na puste przypadki
        //todo i wymieszac permutacje kolumn w tym drugim 
    }

    [Test]
    public void GetPermutation_BasicTree()
    {
        PCNode p1 = new PCNode() { Type = NodeType.P };
        PCNode c1 = new PCNode() { Type = NodeType.C };

        PCNode l0 = PCNodesProvider.UnlabeledLeafWithColumn(0);
        PCNode l1 = PCNodesProvider.UnlabeledLeafWithColumn(1);
        PCNode l2 = PCNodesProvider.UnlabeledLeafWithColumn(2);
        PCNode l3 = PCNodesProvider.UnlabeledLeafWithColumn(3);
        PCNode l4 = PCNodesProvider.UnlabeledLeafWithColumn(4);
        PCNode l5 = PCNodesProvider.UnlabeledLeafWithColumn(5);
        PCNode l6 = PCNodesProvider.UnlabeledLeafWithColumn(6);
        PCNode l7 = PCNodesProvider.UnlabeledLeafWithColumn(7);
        PCNode l8 = PCNodesProvider.UnlabeledLeafWithColumn(8);

        p1.AppendNeighbour(l0);
        l0.AppendNeighbour(p1);
        l0.Parent = p1;
        p1.AppendNeighbour(l6);
        l6.AppendNeighbour(p1);
        l6.Parent = p1;
        p1.AppendNeighbour(c1);
        p1.Parent = c1;

        c1.AppendNeighbour(p1);
        c1.AppendNeighbour(l8);
        l8.AppendNeighbour(c1);
        l8.Parent = c1;
        c1.AppendNeighbour(l2);
        l2.AppendNeighbour(c1);
        l2.Parent = c1;
        c1.AppendNeighbour(l4);
        l4.AppendNeighbour(c1);
        l4.Parent = c1;
        c1.AppendNeighbour(l1);
        l1.AppendNeighbour(c1);
        l1.Parent = c1;
        c1.AppendNeighbour(l3);
        l3.AppendNeighbour(c1);
        l3.Parent = c1;
        c1.AppendNeighbour(l7);
        l7.AppendNeighbour(c1);
        l7.Parent = c1;
        c1.AppendNeighbour(l5);
        l5.AppendNeighbour(c1);
        l5.Parent = c1;

        int[,] matrix = new int[,]
        {
            { 1, 0, 1, 0, 0, 0, 1, 0, 1 }
        };
        int rows = 1;
        int columns = 9;
        List<PCNode> leaves = new List<PCNode>() { l0, l1, l2, l3, l4, l5, l6, l7, l8 };

        PCTree tree = PCTree.TestInstance(leaves, matrix, rows, columns);

        int[]? permutation = tree.GetPermutation();

        Assert.That(permutation, Is.EqualTo(new int[] { 0, 6, 8, 2, 4, 1, 3, 7, 5 }));
    }

    [Test]
    public void GetPermutation_MoreComplexTree()
    {
        PCNode p1 = new PCNode() { Type = NodeType.P };
        PCNode p2 = new PCNode() { Type = NodeType.P };
        PCNode p3 = new PCNode() { Type = NodeType.P };
        PCNode c1 = new PCNode() { Type = NodeType.C };

        PCNode l0 = PCNodesProvider.UnlabeledLeafWithColumn(0);
        PCNode l1 = PCNodesProvider.UnlabeledLeafWithColumn(1);
        PCNode l2 = PCNodesProvider.UnlabeledLeafWithColumn(2);
        PCNode l3 = PCNodesProvider.UnlabeledLeafWithColumn(3);
        PCNode l4 = PCNodesProvider.UnlabeledLeafWithColumn(4);
        PCNode l5 = PCNodesProvider.UnlabeledLeafWithColumn(5);
        PCNode l6 = PCNodesProvider.UnlabeledLeafWithColumn(6);
        PCNode l7 = PCNodesProvider.UnlabeledLeafWithColumn(7);
        PCNode l8 = PCNodesProvider.UnlabeledLeafWithColumn(8);

        p1.AppendNeighbour(l0);
        l0.Parent = p1;
        l0.AppendNeighbour(p1);
        p1.AppendNeighbour(l6);
        l6.Parent = p1;
        l6.AppendNeighbour(p1);
        p1.AppendNeighbour(c1);

        c1.AppendNeighbour(p1);
        c1.AppendNeighbour(p2);
        c1.AppendNeighbour(l1);
        l1.Parent = c1;
        l1.AppendNeighbour(c1);
        c1.AppendNeighbour(l3);
        l3.Parent = c1;
        l3.AppendNeighbour(c1);
        c1.AppendNeighbour(l7);
        l7.Parent = c1;
        l7.AppendNeighbour(c1);
        c1.AppendNeighbour(l5);
        l5.Parent = c1;
        l5.AppendNeighbour(c1);

        p2.AppendNeighbour(l8);
        l8.Parent = p2;
        l8.AppendNeighbour(p2);
        p2.AppendNeighbour(p3);
        p2.AppendNeighbour(c1);

        p3.AppendNeighbour(p2);
        p3.AppendNeighbour(l2);
        l2.Parent = p3;
        l2.AppendNeighbour(p3);
        p3.AppendNeighbour(l4);
        l4.Parent = p3;
        l4.AppendNeighbour(p3);

        int[,] matrix = new int[,]
        {
            { 1, 0, 1, 0, 0, 0, 1, 0, 1 }
        };
        int rows = 1;
        int columns = 9;
        List<PCNode> leaves = new List<PCNode>() { l0, l1, l2, l3, l4, l5, l6, l7, l8 };

        PCTree tree = PCTree.TestInstance(leaves, matrix, rows, columns);

        int[]? permutation = tree.GetPermutation();

        Assert.That(permutation, Is.EqualTo(new int[] { 1, 3, 7, 5, 0, 6, 8, 2, 4 }));
    }

    [Test]
    public void GetPermutation_EvenMoreComplexTree()
    {
        PCTree tree = PCTreesProvider.VeryComplexCaseLeavesUnlabeled();

        int[]? permutation = tree.GetPermutation();

        Assert.That(permutation, Is.EqualTo(new int[] { 7, 6, 8, 5, 4, 2, 1, 0, 13, 12, 14, 9, 10, 11, 3 }));
    }
}