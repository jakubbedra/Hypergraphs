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
        PCNode p1 = new PCNode() {Type = NodeType.P};
        PCNode p2 = new PCNode() {Type = NodeType.P};
        PCNode p3 = new PCNode() {Type = NodeType.P};
        PCNode c1 = new PCNode() {Type = NodeType.C};
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
    public void Find_NoTerminalNode()
    {
        PCNode p1 = new PCNode() {Type = NodeType.P,  Label = NodeLabel.Full};
        PCNode l0 = PCNodesProvider.FullLeafWithColumn(0);
        l0.Parent = p1;
        l0.AppendNeighbour(p1);
        PCNode l1 = PCNodesProvider.FullLeafWithColumn(1);
        l1.Parent = p1;
        l1.AppendNeighbour(p1);
        PCNode l2 = PCNodesProvider.FullLeafWithColumn(2);
        l2.Parent = p1;
        l2.AppendNeighbour(p1);
        PCNode l3 = PCNodesProvider.FullLeafWithColumn(3);
        l3.Parent = p1;
        l3.AppendNeighbour(p1);
        PCNode l4 = PCNodesProvider.FullLeafWithColumn(4);
        l4.Parent = p1;
        l4.AppendNeighbour(p1);
        PCNode l5 = PCNodesProvider.FullLeafWithColumn(5);
        l5.Parent = p1;
        l5.AppendNeighbour(p1);
        
        p1.AppendNeighbour(l0);
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p1.AppendNeighbour(l4);
        p1.AppendNeighbour(l5);

        TerminalPathFinder finder = new TerminalPathFinder(new List<PCNode>(){});

        List<PCNode>? terminalPath = finder.FindTerminalPath();
        
        Assert.That(terminalPath!.Count, Is.EqualTo(0));
    }

    [Test]
    public void Find_OneTerminalNode()
    {
        PCNode p1 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode l0 = PCNodesProvider.FullLeafWithColumn(0);
        l0.Parent = p1;
        l0.AppendNeighbour(p1);
        PCNode l1 = PCNodesProvider.FullLeafWithColumn(1);
        l1.Parent = p1;
        l1.AppendNeighbour(p1);
        PCNode l2 = PCNodesProvider.FullLeafWithColumn(2);
        l2.Parent = p1;
        l2.AppendNeighbour(p1);
        PCNode l3 = PCNodesProvider.EmptyLeafWithColumn(3);
        l3.Parent = p1;
        l3.AppendNeighbour(p1);
        PCNode l4 = PCNodesProvider.FullLeafWithColumn(4);
        l4.Parent = p1;
        l4.AppendNeighbour(p1);
        PCNode l5 = PCNodesProvider.EmptyLeafWithColumn(5);
        l5.Parent = p1;
        l5.AppendNeighbour(p1);
        
        p1.AppendNeighbour(l0);
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p1.AppendNeighbour(l4);
        p1.AppendNeighbour(l5);

        TerminalPathFinder finder = new TerminalPathFinder(new List<PCNode>(){p1});

        List<PCNode>? terminalPath = finder.FindTerminalPath();
        
        Assert.That(terminalPath!.Count, Is.EqualTo(1));
        Assert.That(terminalPath, Contains.Item(p1));
    }

    [Test]
    public void Find_TwoPartialNodes()
    {
        PCNode p1 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p2 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode l0 = PCNodesProvider.FullLeafWithColumn(0);
        l0.Parent = p1;
        l0.AppendNeighbour(p1);
        PCNode l1 = PCNodesProvider.FullLeafWithColumn(1);
        l1.Parent = p1;
        l1.AppendNeighbour(p1);
        PCNode l2 = PCNodesProvider.FullLeafWithColumn(2);
        l2.Parent = p1;
        l2.AppendNeighbour(p1);
        PCNode l3 = PCNodesProvider.EmptyLeafWithColumn(3);
        l3.Parent = p1;
        l3.AppendNeighbour(p1);
        PCNode l4 = PCNodesProvider.FullLeafWithColumn(4);
        l4.Parent = p2;
        l4.AppendNeighbour(p2);
        PCNode l5 = PCNodesProvider.EmptyLeafWithColumn(5);
        l5.Parent = p2;
        l5.AppendNeighbour(p2);
        
        p1.AppendNeighbour(l0);
        p1.AppendNeighbour(l1);
        p1.AppendNeighbour(l2);
        p1.AppendNeighbour(l3);
        p2.AppendNeighbour(l4);
        p2.AppendNeighbour(l5);

        TerminalPathFinder finder = new TerminalPathFinder(new List<PCNode>(){p1, p2});

        List<PCNode>? terminalPath = finder.FindTerminalPath();
        
        Assert.That(terminalPath!.Count, Is.EqualTo(2));
        Assert.That(terminalPath, Contains.Item(p1));
        Assert.That(terminalPath, Contains.Item(p2));
    }

    [Test]
    public void Find_TwoTerminalNodes()
    {
        PCNode p1 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p2 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Full};
        PCNode p3 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode c1 = new PCNode() {Type = NodeType.C, Label = NodeLabel.Partial};
        PCNode l0 = PCNodesProvider.FullLeafWithColumn(0);
        PCNode l1 = PCNodesProvider.EmptyLeafWithColumn(1);
        PCNode l2 = PCNodesProvider.FullLeafWithColumn(2);
        PCNode l3 = PCNodesProvider.EmptyLeafWithColumn(3);
        PCNode l4 = PCNodesProvider.EmptyLeafWithColumn(4);
        PCNode l5 = PCNodesProvider.EmptyLeafWithColumn(5);
        PCNode l6 = PCNodesProvider.FullLeafWithColumn(6);
        PCNode l7 = PCNodesProvider.EmptyLeafWithColumn(7);
        PCNode l8 = PCNodesProvider.FullLeafWithColumn(8);
        
        p1.AppendNeighbour(p3);
        p1.AppendNeighbour(l4);
        p1.AppendNeighbour(l2);
        p1.Parent = p3;
        
        p3.AppendNeighbour(c1);
        p3.AppendNeighbour(p1);
        p3.AppendNeighbour(l8);
        p3.Parent = c1;
        
        p2.AppendNeighbour(c1);
        p2.AppendNeighbour(l6);
        p2.AppendNeighbour(l0);
        p2.Parent = c1;
        
        c1.AppendNeighbour(p3);
        c1.AppendNeighbour(l1);
        c1.AppendNeighbour(l3);
        c1.AppendNeighbour(l7);
        c1.AppendNeighbour(l5);
        c1.AppendNeighbour(p2);
        
        
        TerminalPathFinder finder = new TerminalPathFinder(new List<PCNode>(){p1, c1, p3});

        List<PCNode>? terminalPath = finder.FindTerminalPath();
        
        Assert.That(terminalPath, Is.EqualTo(new List<PCNode>(){p1, p3, c1}));
    }

    [Test]//todo: zrobic jeszcze test labelowania dla tego przypadku
    public void Find_TwoTerminalNodes_MoreComplexCase()
    {
        // todo: tutaj moge wziac to z artykulu https://drops.dagstuhl.de/storage/00lipics/lipics-vol204-esa2021/LIPIcs.ESA.2021.43/LIPIcs.ESA.2021.43.pdf
        PCNode p1 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p2 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p3 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p4 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Full};
        PCNode p5 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Full};
        PCNode p6 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Partial};
        PCNode p7 = new PCNode() {Type = NodeType.P, Label = NodeLabel.Full};
        PCNode c1 = new PCNode() {Type = NodeType.C, Label = NodeLabel.Partial};
        PCNode c2 = new PCNode() {Type = NodeType.C, Label = NodeLabel.Empty};
        PCNode l1 = PCNodesProvider.FullLeafWithColumn(1);
        PCNode l2 = PCNodesProvider.FullLeafWithColumn(2);
        PCNode l3 = PCNodesProvider.FullLeafWithColumn(3);
        PCNode l4 = PCNodesProvider.EmptyLeafWithColumn(4);
        PCNode l5 = PCNodesProvider.FullLeafWithColumn(5);
        PCNode l6 = PCNodesProvider.FullLeafWithColumn(6);
        PCNode l7 = PCNodesProvider.FullLeafWithColumn(7);
        PCNode l8 = PCNodesProvider.EmptyLeafWithColumn(8);
        PCNode l9 = PCNodesProvider.FullLeafWithColumn(9);
        PCNode l10 = PCNodesProvider.EmptyLeafWithColumn(10);
        PCNode l11 = PCNodesProvider.EmptyLeafWithColumn(11);
        PCNode l12 = PCNodesProvider.EmptyLeafWithColumn(12);
        PCNode l13 = PCNodesProvider.FullLeafWithColumn(13);
        PCNode l14 = PCNodesProvider.FullLeafWithColumn(14);
        PCNode l15 = PCNodesProvider.EmptyLeafWithColumn(15);
        
        p1.AppendNeighbour(p2);
        p1.Parent = p2;
        p1.AppendNeighbour(l9);
        l9.Parent = p1;
        l9.AppendNeighbour(p1);
        p1.AppendNeighbour(l8);
        l8.Parent = p1;
        l8.AppendNeighbour(p1);
        p1.AppendNeighbour(l7);
        l7.Parent = p1;
        l7.AppendNeighbour(p1);
        
        p5.AppendNeighbour(p2);
        p5.Parent = p2;
        p5.AppendNeighbour(l6);
        l6.Parent = p5;
        l6.AppendNeighbour(p5);
        p5.AppendNeighbour(l5);
        l5.Parent = p5;
        l5.AppendNeighbour(p5);

        p2.AppendNeighbour(p1);
        p2.AppendNeighbour(p5);
        p2.AppendNeighbour(p3);
        p2.Parent = p3;
        p2.AppendNeighbour(l4);
        l4.Parent = p2;
        l4.AppendNeighbour(p2);

        p3.AppendNeighbour(p2);
        p3.AppendNeighbour(c1);
        p3.AppendNeighbour(c2);
        
        c1.AppendNeighbour(p3);
        p3.Parent = c1;
        c1.AppendNeighbour(l3);
        l3.Parent = c1;
        l3.AppendNeighbour(c1);
        c1.AppendNeighbour(p4);
        p4.Parent = c1;
        p4.AppendNeighbour(c1);
        c1.AppendNeighbour(p6);
        p6.Parent = c1;
        p6.AppendNeighbour(c1);
        
        p4.AppendNeighbour(l2);
        l2.Parent = p4;
        l2.AppendNeighbour(p4);
        p4.AppendNeighbour(l1);
        l1.Parent = p4;
        l1.AppendNeighbour(p4);

        p6.AppendNeighbour(l15);
        l15.Parent = p6;
        l15.AppendNeighbour(p6);
        p6.AppendNeighbour(p7);
        p7.Parent = p6;
        p7.AppendNeighbour(p6);

        l14.Parent = p7;
        l14.AppendNeighbour(p7);
        p7.AppendNeighbour(l14);
        l13.Parent = p7;
        l13.AppendNeighbour(p7);
        p7.AppendNeighbour(l13);
        
        c2.AppendNeighbour(p3);
        c2.AppendNeighbour(l12);
        l12.Parent = c2;
        l12.AppendNeighbour(c2);
        c2.AppendNeighbour(l11);
        l11.Parent = c2;
        l11.AppendNeighbour(c2);
        c2.AppendNeighbour(l10);
        l10.Parent = c2;
        l10.AppendNeighbour(c2);
        
        TerminalPathFinder finder = new TerminalPathFinder(new List<PCNode>(){p2, c1, p3, p6});

        List<PCNode>? terminalPath = finder.FindTerminalPath();
        
        Assert.That(terminalPath, Is.EqualTo(new List<PCNode>(){p2, p3, c1, p6}));
    }
    
    [Test]
    public void Find_TerminalTree()
    {
        
    }
    
}