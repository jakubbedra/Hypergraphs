using Hypergraphs.Graphs.Algorithms.PCTrees;
using HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees;

public class TerminalPathFinderTest
{
    [Test]
    public void LabelNodes_OnePNodeTree()
    {
        int[,] matrix =
        {
            {1,1,1,0,0,1},
            {0,0,1,1,1,0},
            {1,1,0,0,0,0},
            {1,0,0,0,1,1},
            {0,0,0,1,1,1},
            {1,1,0,0,0,1},
        };
        int rows = 6;
        int columns = 6;
        PCNode p1 = new PCNode() {Type = NodeType.P};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p1.AppendNeighbour(l4);
        p1.AppendNeighbour(l5);
        p1.AppendNeighbour(l6);
        PCTree tree = new PCTree(matrix, rows, columns);
        p1.Neighbours.ForEach(n => tree.Leaves.Add(n));
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(p1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
    }

    [Test]
    public void LabelNodes_OneCNodeTree()
    {
        int[,] matrix =
        {
            {1,1,1,0,0,1},
            {0,0,1,1,1,0},
            {1,1,0,0,0,0},
            {1,0,0,0,1,1},
            {0,0,0,1,1,1},
            {1,1,0,0,0,1},
        };
        int rows = 6;
        int columns = 6;
        PCNode c1 = new PCNode() {Type = NodeType.C};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        c1.AppendNeighbour(l1);
        c1.AppendNeighbour(l2);
        c1.AppendNeighbour(l3);
        c1.AppendNeighbour(l4);
        c1.AppendNeighbour(l5);
        c1.AppendNeighbour(l6);
        PCTree tree = new PCTree(matrix, rows, columns);
        c1.Neighbours.ForEach(n => tree.Leaves.Add(n));
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(c1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
    }

    [Test]
    public void LabelNodes_TwoPNodeTree()
    {
        int[,] matrix =
        {
            {0,1,1,0,0,1},
            {0,0,1,1,1,0},
            {1,1,0,0,0,0},
            {1,0,0,0,1,1},
            {0,0,0,1,1,1},
            {1,1,0,0,0,1},
        };
        int rows = 6;
        int columns = 6;
        PCNode p1 = new PCNode() {Type = NodeType.P};
        PCNode p2 = new PCNode() {Type = NodeType.P};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p2.AppendNeighbour(l4);
        p2.AppendNeighbour(l5);
        p2.AppendNeighbour(l6);
        p1.AppendNeighbour(p2);
        p2.AppendNeighbour(p1);
        p1.Parent = p2;
        p2.Parent = p1;
        PCTree tree = new PCTree(matrix, rows, columns);
        tree.Leaves.Add(l1);
        tree.Leaves.Add(l2);
        tree.Leaves.Add(l3);
        tree.Leaves.Add(l4);
        tree.Leaves.Add(l5);
        tree.Leaves.Add(l6);
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(p1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(p2.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
    }
    
    [Test]
    public void LabelNodes_TwoCNodeTree()
    {
        int[,] matrix =
        {
            {0,1,1,0,0,1},
            {0,0,1,1,1,0},
            {1,1,0,0,0,0},
            {1,0,0,0,1,1},
            {0,0,0,1,1,1},
            {1,1,0,0,0,1},
        };
        int rows = 6;
        int columns = 6;
        PCNode p1 = new PCNode() {Type = NodeType.C};
        PCNode p2 = new PCNode() {Type = NodeType.C};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p2.AppendNeighbour(l4);
        p2.AppendNeighbour(l5);
        p2.AppendNeighbour(l6);
        p1.AppendNeighbour(p2);
        p2.AppendNeighbour(p1);
        PCTree tree = new PCTree(matrix, rows, columns);
        tree.Leaves.Add(l1);
        tree.Leaves.Add(l2);
        tree.Leaves.Add(l3);
        tree.Leaves.Add(l4);
        tree.Leaves.Add(l5);
        tree.Leaves.Add(l6);
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(p1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(p2.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
    }
    
    [Test]
    public void LabelNodes_OnePNodeOneCNodeTree()
    {
        int[,] matrix =
        {
            {0,1,1,0,0,1},
            {0,0,1,1,1,0},
            {1,1,0,0,0,0},
            {1,0,0,0,1,1},
            {0,0,0,1,1,1},
            {1,1,0,0,0,1},
        };
        int rows = 6;
        int columns = 6;
        PCNode p1 = new PCNode() {Type = NodeType.P};
        PCNode p2 = new PCNode() {Type = NodeType.C};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p2.AppendNeighbour(l4);
        p2.AppendNeighbour(l5);
        p2.AppendNeighbour(l6);
        p1.AppendNeighbour(p2);
        p2.AppendNeighbour(p1);
        PCTree tree = new PCTree(matrix, rows, columns);
        tree.Leaves.Add(l1);
        tree.Leaves.Add(l2);
        tree.Leaves.Add(l3);
        tree.Leaves.Add(l4);
        tree.Leaves.Add(l5);
        tree.Leaves.Add(l6);
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(p1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(p2.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
    }

    [Test]
    public void LabelNodes_MoreComplexCase()
    {
        int[,] matrix =
        {
            {1,0,1,0,0,0,1,0,1},
            {1,0,1,0,0,0,1,0,1},
            {1,0,1,0,0,0,1,0,1},
            {1,0,1,0,0,0,1,0,1},
            {1,0,1,0,0,0,1,0,1},
            {1,0,1,0,0,0,1,0,1},
        };
        int rows = 6;
        int columns = 9;
        PCNode p1 = new PCNode() {Type = NodeType.C};
        PCNode p2 = new PCNode() {Type = NodeType.P};
        PCNode p3 = new PCNode() {Type = NodeType.P};
        PCNode c1 = new PCNode() {Type = NodeType.P};
        PCNode l0 = new PCNode() {Type = NodeType.Leaf, Column = 0, Parent = p2, Neighbours = new List<PCNode>(){p2}};
        PCNode l1 = new PCNode() {Type = NodeType.Leaf, Column = 1, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l2 = new PCNode() {Type = NodeType.Leaf, Column = 2, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l3 = new PCNode() {Type = NodeType.Leaf, Column = 3, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l4 = new PCNode() {Type = NodeType.Leaf, Column = 4, Parent = p1, Neighbours = new List<PCNode>(){p1}};
        PCNode l5 = new PCNode() {Type = NodeType.Leaf, Column = 5, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l6 = new PCNode() {Type = NodeType.Leaf, Column = 6, Parent = p2 ,Neighbours = new List<PCNode>(){p2}};
        PCNode l7 = new PCNode() {Type = NodeType.Leaf, Column = 7, Parent = c1, Neighbours = new List<PCNode>(){c1}};
        PCNode l8 = new PCNode() {Type = NodeType.Leaf, Column = 8, Parent = p3, Neighbours = new List<PCNode>(){p3}};
        p1.AppendNeighbour(p3);
        p1.AppendNeighbour(l4);
        p1.AppendNeighbour(l2);
        
        p2.AppendNeighbour(c1);
        p2.AppendNeighbour(l6);
        p2.AppendNeighbour(l0);
        
        p3.AppendNeighbour(c1);
        p3.AppendNeighbour(p1);
        p3.AppendNeighbour(l8);
        
        c1.AppendNeighbour(p2);
        c1.AppendNeighbour(p3);
        c1.AppendNeighbour(l1);
        c1.AppendNeighbour(l3);
        c1.AppendNeighbour(l7);
        c1.AppendNeighbour(l5);
        
        p1.Parent = p3;
        p3.Parent = c1;
        p2.Parent = c1;
        
        PCTree tree = new PCTree(matrix, rows, columns);
        tree.Leaves.Add(l0);
        tree.Leaves.Add(l1);
        tree.Leaves.Add(l2);
        tree.Leaves.Add(l3);
        tree.Leaves.Add(l4);
        tree.Leaves.Add(l5);
        tree.Leaves.Add(l6);
        tree.Leaves.Add(l7);
        tree.Leaves.Add(l8);
        TerminalPathFinder finder = new TerminalPathFinder(tree);

        finder.LabelNodes();

        Assert.That(p1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(p2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(p3.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(c1.Label, Is.EqualTo(NodeLabel.Partial));
        Assert.That(l0.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l1.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l2.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l3.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l4.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l5.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l6.Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(l7.Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(l8.Label, Is.EqualTo(NodeLabel.Full));
    }

    [Test]
    public void Find_NoTerminalVertex()
    {
        
    }

    [Test]
    public void Find_OneTerminalVertex()
    {
        
    }

    [Test]
    public void Find_TwoTerminalVertices()
    {
        
    }
    
}