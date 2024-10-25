using Hypergraphs.Extensions;
using Hypergraphs.Graphs.Algorithms.PCTrees;
using HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees;

public class TerminalPathRearrangementUtilsTest
{
    [Test]
    public void OrderPNodeTest_PartialOnLeft()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        PCNode leftPartial = PCNodesProvider.PartialP;
        node.AppendNeighbour(leftPartial);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyP);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        
        TerminalPathRearrangementUtils.OrderPNode(node, leftPartial, null);
        
        Assert.That(node.Neighbours[0], Is.EqualTo(leftPartial));
        Assert.That(node.Neighbours[1].Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(node.Neighbours[^1].Label, Is.EqualTo(NodeLabel.Empty));
    }
    
    [Test]
    public void OrderPNodeTest_PartialOnRight()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        node.AppendNeighbour(rightPartial);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyP);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        
        TerminalPathRearrangementUtils.OrderPNode(node, null, rightPartial);
        
        Assert.That(node.Neighbours[0], Is.EqualTo(rightPartial));
        Assert.That(node.Neighbours[1].Label, Is.EqualTo(NodeLabel.Empty));
        Assert.That(node.Neighbours[^1].Label, Is.EqualTo(NodeLabel.Full));
    }
    
    [Test]
    public void OrderPNodeTest_TwoPartials()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        PCNode leftPartial = PCNodesProvider.PartialP;
        node.AppendNeighbour(rightPartial);
        node.AppendNeighbour(leftPartial);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyP);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);

        int emptyNeighboursCount = 4;
        int fullNeighboursCount = 3;
        
        TerminalPathRearrangementUtils.OrderPNode(node, leftPartial, rightPartial);

        Assert.That(node.Neighbours[0], Is.EqualTo(leftPartial));
        for (int i = 0; i < fullNeighboursCount; i++)
            Assert.That(node.Neighbours[1+i].Label, Is.EqualTo(NodeLabel.Full));
        
        Assert.That(node.Neighbours[1 + fullNeighboursCount], Is.EqualTo(rightPartial));
        for (int i = 0; i < emptyNeighboursCount; i++)
            Assert.That(node.Neighbours[2 + fullNeighboursCount + i].Label, Is.EqualTo(NodeLabel.Empty));
    }

    [Test]
    public void OrderCNodeTest_PartialOnLeft_IsOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            leftPartial,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyP,  
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, null);
        
        neighbours.RotateLeft(neighbours.IndexOf(leftPartial));
        
        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(neighbours[i] == node.Neighbours[i]);
    }
    
    [Test]
    public void OrderCNodeTest_PartialOnLeft_MustBeFlipped()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.FullLeaf,
            leftPartial,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyP,  
        };
        List<PCNode> expectedNeighbours = new List<PCNode>()
        {
            neighbours[5],
            neighbours[4],
            neighbours[3],
            neighbours[2],
            neighbours[1],
            neighbours[0],
            neighbours[7],
            neighbours[6],
        };
        
        neighbours.ForEach(n => node.AppendNeighbour(n));
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, null);

        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(expectedNeighbours[i] == node.Neighbours[i]);
    }

    [Test]
    public void OrderCNodeTest_PartialOnLeft_CannotBeOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.FullLeaf,
            leftPartial,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyP,  
        };
        
        neighbours.ForEach(n => node.AppendNeighbour(n));
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, null);

        Assert.IsFalse(result);
    }
    
    [Test]
    public void OrderCNodeTest_PartialOnRight_CanBeOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,  // 0
            PCNodesProvider.EmptyLeaf,  // 1
            PCNodesProvider.FullLeaf,   // 2
            PCNodesProvider.FullP,      // 3
            PCNodesProvider.FullLeaf,   // 4
            rightPartial,               // 5
            PCNodesProvider.EmptyLeaf,  // 6
            PCNodesProvider.EmptyP,     // 7
        };
        List<PCNode> expectedNeighbours = new List<PCNode>()
        {
            neighbours[5],
            neighbours[6],
            neighbours[7],
            neighbours[0],
            neighbours[1],
            neighbours[2],
            neighbours[3],
            neighbours[4],
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, null, rightPartial);

        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(expectedNeighbours[i] == node.Neighbours[i]);
    }

    [Test]
    public void OrderCNodeTest_PartialOnRight_MustBeFlipped()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,  // 0
            PCNodesProvider.EmptyLeaf,  // 1
            rightPartial,               // 2
            PCNodesProvider.FullLeaf,   // 3
            PCNodesProvider.FullP,      // 4
            PCNodesProvider.FullLeaf,   // 5
            PCNodesProvider.EmptyLeaf,  // 6
            PCNodesProvider.EmptyP,     // 7
        };
        List<PCNode> expectedNeighbours = new List<PCNode>()
        {// 2 1 0 7 6 5 4 3
            neighbours[2],
            neighbours[1],
            neighbours[0],
            neighbours[7],
            neighbours[6],
            neighbours[5],
            neighbours[4],
            neighbours[3],
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, null, rightPartial);
        
        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(expectedNeighbours[i] == node.Neighbours[i]);
    }
    
    [Test]
    public void OrderCNodeTest_PartialOnRight_CannotBeOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.FullLeaf,
            rightPartial,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyP,  
        };
        
        neighbours.ForEach(n => node.AppendNeighbour(n));
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, null, rightPartial);

        Assert.IsFalse(result);
    }

    [Test]
    public void OrderCNodeTest_TwoPartials_CanBeOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,  // 0
            PCNodesProvider.EmptyLeaf,  // 1
            leftPartial,                // 2
            PCNodesProvider.FullLeaf,   // 3
            PCNodesProvider.FullP,      // 4
            PCNodesProvider.FullLeaf,   // 5
            rightPartial,               // 6
            PCNodesProvider.EmptyLeaf,  // 7
            PCNodesProvider.EmptyP,     // 8
        };
        List<PCNode> expectedNeighbours = new List<PCNode>()
        {
            neighbours[2],
            neighbours[3],
            neighbours[4],
            neighbours[5],
            neighbours[6],
            neighbours[7],
            neighbours[8],
            neighbours[0],
            neighbours[1],
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, rightPartial);
        
        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(expectedNeighbours[i] == node.Neighbours[i]);
    }
    
    [Test]
    public void OrderCNodeTest_TwoPartials_MustBeFlipped()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyC,     // 0
            PCNodesProvider.EmptyLeaf,  // 1
            rightPartial,               // 2
            PCNodesProvider.FullC,      // 3
            PCNodesProvider.FullLeaf,   // 4
            PCNodesProvider.FullP,      // 5
            leftPartial,                // 6
            PCNodesProvider.EmptyLeaf,  // 7
            PCNodesProvider.EmptyP,     // 8
        };
        List<PCNode> expectedNeighbours = new List<PCNode>()
        {// 6 5 4 3 2 1 0 7 8
            neighbours[6],
            neighbours[5],
            neighbours[4],
            neighbours[3],
            neighbours[2],
            neighbours[1],
            neighbours[0],
            neighbours[8],
            neighbours[7],
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, rightPartial);
        
        Assert.IsTrue(result);
        for (var i = 0; i < node.Neighbours.Count; i++)
            Assert.True(expectedNeighbours[i] == node.Neighbours[i]);
    }
    
    [Test]
    public void OrderCNodeTest_TwoPartials_CannotBeOrdered()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        PCNode rightPartial = PCNodesProvider.PartialP;
        PCNode leftPartial = PCNodesProvider.PartialP;
        List<PCNode> neighbours = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            leftPartial,              
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf, 
            PCNodesProvider.FullP,    
            PCNodesProvider.FullLeaf, 
            rightPartial,             
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyP,   
        };
        neighbours.ForEach(n => node.AppendNeighbour(n));
        
        bool result = TerminalPathRearrangementUtils.OrderCNode(node, leftPartial, rightPartial);
        
        Assert.IsFalse(result);
    }

    [Test]
    public void RearrangePath_OnePNodePath()
    {
        // case with only one P-node and leaves
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);

        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node});

        Assert.IsTrue(result);
    }

    [Test]
    public void RearrangePath_TwoPNodePath()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
        };
        neighbours1.ForEach(node => node1.AppendNeighbour(node));

        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
        };
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node1.AppendNeighbour(node2);
        node2.PrependNeighbour(node1);
            
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2});

        Assert.True(result);
        Assert.True(node1.Neighbours[0] == node2);
        Assert.True(node1.Neighbours[1].Label == NodeLabel.Empty);
        Assert.True(node1.Neighbours[3].Label == NodeLabel.Full);
        
        Assert.True(node2.Neighbours[0] == node1);
        Assert.True(node2.Neighbours[1].Label == NodeLabel.Full);
        Assert.True(node2.Neighbours[3].Label == NodeLabel.Empty);
    }

    [Test]
    public void RearrangePath_MoreComplexCase()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
        };
        PCNode node3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
        };
        node1.AppendNeighbour(node2);
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        node2.AppendNeighbour(node1);
        node2.AppendNeighbour(node3);
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node3.AppendNeighbour(node2);
        neighbours3.ForEach(node => node3.AppendNeighbour(node));
        
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2, node3});

        Assert.True(result);
        Assert.True(node1.Neighbours[0] == node2);
        Assert.True(node1.Neighbours[1] == neighbours1[0]);
        Assert.True(node1.Neighbours[2] == neighbours1[1]);
        Assert.True(node1.Neighbours[3] == neighbours1[2]);
        Assert.True(node1.Neighbours[4] == neighbours1[3]);
        Assert.True(node1.Neighbours[5] == neighbours1[4]);
        
        Assert.True(node2.Neighbours[0] == node1);
        Assert.True(node2.Neighbours[2] == node3);
        Assert.True(node2.Neighbours[1] == neighbours2[0]);
        
        Assert.True(node3.Neighbours[0] == node2);
        Assert.True(node3.Neighbours[1] == neighbours3[1]);
        Assert.True(node3.Neighbours[2] == neighbours3[0]);
    }
    
    [Test]
    public void RearrangePath_OnePNodeCannotBeRearranged_ForbiddenAdditionalPartialInTheMiddle()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.PartialC,
        };
        PCNode node3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
        };
        node1.AppendNeighbour(node2);
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        node2.AppendNeighbour(node1);
        node2.AppendNeighbour(node3);
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node3.AppendNeighbour(node2);
        neighbours3.ForEach(node => node3.AppendNeighbour(node));
        
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2, node3});

        Assert.False(result);
    }
    
    [Test]
    public void RearrangePath_OneCNodeCannotBeRearranged()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
        };
        PCNode node3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
        };
        node1.AppendNeighbour(node2);
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        node2.AppendNeighbour(node1);
        node2.AppendNeighbour(node3);
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node3.AppendNeighbour(node2);
        neighbours3.ForEach(node => node3.AppendNeighbour(node));
        
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2, node3});

        Assert.False(result);
    }
    
    [Test]
    public void RearrangePath_MultipleNodesCannotBeRearranged()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullP,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.PartialP,
        };
        PCNode node3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.PartialC,
        };
        node1.AppendNeighbour(node2);
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        node2.AppendNeighbour(node1);
        node2.AppendNeighbour(node3);
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node3.AppendNeighbour(node2);
        neighbours3.ForEach(node => node3.AppendNeighbour(node));
        
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2, node3});

        Assert.False(result);
    }
    
    [Test]
    public void RearrangePath_CannotBeRearranged_OnePNodePath_OneNeighbouringPartialNode()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.PartialC);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);

        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node});

        Assert.IsFalse(result);
    }
    
    [Test]
    public void RearrangePath_CannotBeRearranged_OnePNodePath_MultipleNeighbouringPartialNodes()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.PartialC);
        node.AppendNeighbour(PCNodesProvider.PartialP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.PartialP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);

        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node});

        Assert.IsFalse(result);
    }
    
    [Test]
    public void RearrangePath_CannotBeRearranged_OneCNodePath_MultipleNeighbouringPartialNodes()
    {
        PCNode node = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.PartialC);
        node.AppendNeighbour(PCNodesProvider.PartialP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.PartialP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);

        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node});

        Assert.IsFalse(result);
    }
    
    
    [Test]
    public void RearrangePath_PNodeAndCNOdePath_NoneCanBeOrdered()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.FullLeaf,
            PCNodesProvider.EmptyLeaf,
        };
        neighbours1.ForEach(node => node1.AppendNeighbour(node));

        PCNode node2 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeaf,
            PCNodesProvider.PartialC,
            PCNodesProvider.PartialP,
            PCNodesProvider.EmptyLeaf,
            PCNodesProvider.EmptyLeaf,
        };
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        node1.AppendNeighbour(node2);
        node2.PrependNeighbour(node1);
            
        bool result = TerminalPathRearrangementUtils.RearrangePath(new List<PCNode>(){node1, node2});

        Assert.False(result);
    }

    [Test]
    public void SplitAndMergePath_OnePNodePath()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(1),
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.FullLeafWithColumn(4),
            PCNodesProvider.FullLeafWithColumn(8),
            PCNodesProvider.EmptyLeafWithColumn(7),
            PCNodesProvider.EmptyLeafWithColumn(6),
            PCNodesProvider.EmptyLeafWithColumn(5),
            PCNodesProvider.EmptyLeafWithColumn(3),
            PCNodesProvider.EmptyLeafWithColumn(0),
        };
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours1.ForEach(node => node.Parent = node1);

        PCNode neighbour2 = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1 });
        PCNode neighbour1 = neighbour2.Parent!;
        
        Assert.That(neighbour1.Neighbours.Count, Is.EqualTo(5));
        Assert.That(neighbour2.Neighbours.Count, Is.EqualTo(6));
        
        Assert.True(neighbour1.Neighbours[4] == neighbour2);
        Assert.True(neighbour1.Neighbours[0].Column == 1);
        Assert.True(neighbour1.Neighbours[1].Column == 2);
        Assert.True(neighbour1.Neighbours[2].Column == 4);
        Assert.True(neighbour1.Neighbours[3].Column == 8);
        
        Assert.True(neighbour2.Neighbours[5] == neighbour1);
        Assert.True(neighbour2.Neighbours[0].Column == 7);
        Assert.True(neighbour2.Neighbours[1].Column == 6);
        Assert.True(neighbour2.Neighbours[2].Column == 5);
        Assert.True(neighbour2.Neighbours[3].Column == 3);
        Assert.True(neighbour2.Neighbours[4].Column == 0);
    }
    
    [Test]
    public void SplitAndMergePath_OnePNodePath_TwoPNodesInTotal()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyPWithLeaves(new List<int>() {7,6,5,3,0}),
            PCNodesProvider.EmptyLeafWithColumn(8),
            PCNodesProvider.EmptyLeafWithColumn(1),
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.FullLeafWithColumn(4),
        };
        
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours1.ForEach(node => node.Parent = node1);
        
        PCNode neighbour2 = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1 });
        PCNode neighbour1 = neighbour2.Parent!;


        Assert.That(neighbour1.Neighbours.Count, Is.EqualTo(3));
        Assert.True(neighbour1.Neighbours[0] == neighbours1[3]);
        Assert.True(neighbour1.Neighbours[1] == neighbours1[4]);
        
        Assert.That(neighbour2.Neighbours.Count, Is.EqualTo(4));
        Assert.True(neighbour2.Neighbours[0] == neighbours1[0]);
        Assert.True(neighbour2.Neighbours[1] == neighbours1[1]);
        Assert.True(neighbour2.Neighbours[2] == neighbours1[2]);
    }

    [Test]
    public void SplitAndMergePath_TwoPNodePath()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(5),
            PCNodesProvider.FullPWithLeaves(new List<int>() {0,1}),
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.EmptyPWithLeaves(new List<int>() {3,4}),
        };
        
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours1.ForEach(node => node.Parent=node1);
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        neighbours2.ForEach(node => node.AppendNeighbour(node2));
        neighbours2.ForEach(node => node.Parent=node2);
        
        node1.PrependNeighbour(node2);
        node2.PrependNeighbour(node1);
        
        PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1, node2 });

        Assert.That(centralCNode.Neighbours.Count, Is.EqualTo(4));
        Assert.True(centralCNode.Neighbours[0] == neighbours2[1]);
        Assert.True(centralCNode.Neighbours[1] == neighbours1[0]);
        Assert.True(centralCNode.Neighbours[2] == neighbours1[1]);
        Assert.True(centralCNode.Neighbours[3] == neighbours2[0]);
    }

    [Test]
    public void SplitAndMergePath_OnePNodeOneCNodePath()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(5),
            PCNodesProvider.FullLeafWithColumn(0),
            PCNodesProvider.FullLeafWithColumn(1),
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.EmptyLeafWithColumn(3),
            PCNodesProvider.EmptyLeafWithColumn(4),
        };

        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.Parent = node1);
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        neighbours2.ForEach(node => node.Parent = node2);
        neighbours2.ForEach(node => node.AppendNeighbour(node2));

        node1.PrependNeighbour(node2);
        node1.Parent = node2;
        node2.PrependNeighbour(node1);

        PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1, node2 });

        Assert.That(centralCNode.Neighbours.Count, Is.EqualTo(5));
        Assert.True(centralCNode.Neighbours[0] == neighbours2[1]);
        Assert.True(centralCNode.Neighbours[1] == neighbours2[2]);
        Assert.True(centralCNode.Neighbours[2] == neighbours1[0]);
        Assert.True(centralCNode.Neighbours[3].Label == NodeLabel.Full);
        Assert.True(centralCNode.Neighbours[4] == neighbours2[0]);
    }

    [Test]
    public void SplitAndMergePath_TwoPNodeTree()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.FullLeafWithColumn(4),
            PCNodesProvider.EmptyLeafWithColumn(1),
            PCNodesProvider.EmptyLeafWithColumn(8),
        };
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Empty
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(0),
            PCNodesProvider.EmptyLeafWithColumn(3),
            PCNodesProvider.EmptyLeafWithColumn(5),
            PCNodesProvider.EmptyLeafWithColumn(6),
            PCNodesProvider.EmptyLeafWithColumn(7),
        };

        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.Parent = node1);
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        neighbours2.ForEach(node => node.Parent = node2);
        neighbours2.ForEach(node => node.AppendNeighbour(node2));
        
        node1.PrependNeighbour(node2);
        node1.Parent = node2;
        node2.PrependNeighbour(node1);
        node2.Parent = node1;

        PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1 });

        Assert.That(centralCNode.Neighbours.Count, Is.EqualTo(4));
    }
    
    [Test]
    public void SplitAndMergePath_SampleTestIEqualTo3()
    {
        PCNode p1 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighboursP1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(3),
            PCNodesProvider.FullLeafWithColumn(7),
        };

        PCNode p2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(5),
            PCNodesProvider.EmptyLeafWithColumn(6),
            PCNodesProvider.EmptyLeafWithColumn(0),
        };

        PCNode p3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Empty
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(8),
        };
        
        PCNode p4 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Empty
        };
        List<PCNode> neighbours4 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(2),
            PCNodesProvider.EmptyLeafWithColumn(4),
        };
        
        PCNode c1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighboursC1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(1),
        };

        p1.Parent = c1;
        p1.AppendNeighbour(c1);
        c1.AppendNeighbour(p1);
        neighboursP1.ForEach(n => p1.AppendNeighbour(n));
        neighboursP1.ForEach(n => n.AppendNeighbour(p1));
        neighboursP1.ForEach(n => n.Parent = p1);

        p2.Parent = c1;
        p2.AppendNeighbour(c1);
        c1.AppendNeighbour(p2);
        neighbours2.ForEach(n => p2.AppendNeighbour(n));
        neighbours2.ForEach(n => n.AppendNeighbour(p2));
        neighbours2.ForEach(n => n.Parent = p2);

        p4.Parent = p3;
        p4.AppendNeighbour(p3);
        p3.AppendNeighbour(p4);
        neighbours4.ForEach(n => p4.AppendNeighbour(n));
        neighbours4.ForEach(n => n.AppendNeighbour(p4));
        neighbours4.ForEach(n => n.Parent = p4);
        
        p3.Parent = c1;
        neighbours3.ForEach(n => p3.AppendNeighbour(n));
        neighbours3.ForEach(n => n.AppendNeighbour(p3));
        neighbours3.ForEach(n => n.Parent = p3);
        p3.AppendNeighbour(c1);
        c1.AppendNeighbour(p3);

        neighboursC1.ForEach(n => c1.AppendNeighbour(n));
        neighboursC1.ForEach(n => n.AppendNeighbour(c1));
        neighboursC1.ForEach(n => n.Parent = c1);
        
        // c1.Flip();
        // c1.Neighbours.RotateLeft(c1.Neighbours.IndexOf(p2)); // take the left one to 0th slot
        // List<PCNode> terminalPath = new List<PCNode>() { p2, c1, p1 };
        List<PCNode> terminalPath = new List<PCNode>() { p1, c1, p2 };
        TerminalPathRearrangementUtils.RearrangePath(terminalPath);
        PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePathV2(terminalPath);
    }

    [Test]
    public void SplitAndMergePath_MoreComplexCase()
    {
        PCNode node1 = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours1 = new List<PCNode>()
        {
            PCNodesProvider.EmptyLeafWithColumn(1),
            PCNodesProvider.EmptyLeafWithColumn(3),
            PCNodesProvider.EmptyLeafWithColumn(7),
            PCNodesProvider.EmptyLeafWithColumn(5),
            PCNodesProvider.FullPWithTwoLeaves,
        };//prepend 2
        
        PCNode node2 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours2 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(8),
        };//prepend1, append 3
        
        PCNode node3 = new PCNode()
        {
            Type = NodeType.P,
            Label = NodeLabel.Partial
        };
        List<PCNode> neighbours3 = new List<PCNode>()
        {
            PCNodesProvider.FullLeafWithColumn(2),
            PCNodesProvider.EmptyLeafWithColumn(4),
        };//prepend 2
        
        neighbours1.ForEach(node => node1.AppendNeighbour(node));
        neighbours1.ForEach(node => node.Parent = node1);
        neighbours1.ForEach(node => node.AppendNeighbour(node1));
        neighbours2.ForEach(node => node2.AppendNeighbour(node));
        neighbours2.ForEach(node => node.Parent = node2);
        neighbours2.ForEach(node => node.AppendNeighbour(node2));
        neighbours3.ForEach(node => node3.AppendNeighbour(node));
        neighbours3.ForEach(node => node.Parent = node3);
        neighbours3.ForEach(node => node.AppendNeighbour(node3));

        node1.PrependNeighbour(node2);
        node2.PrependNeighbour(node1);
        node2.AppendNeighbour(node3);
        node3.PrependNeighbour(node2);

        // PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePath(new List<PCNode>() { node1, node2, node3 });
        PCNode centralCNode = TerminalPathRearrangementUtils.SplitAndMergePathV2(new List<PCNode>() { node1, node2, node3 });
        
        Assert.That(centralCNode.Neighbours.Count, Is.EqualTo(8));
        
        Assert.True(centralCNode.Neighbours[0] == neighbours3[1]);
        Assert.True(centralCNode.Neighbours[1] == neighbours1[0]);
        Assert.True(centralCNode.Neighbours[2] == neighbours1[1]);
        Assert.True(centralCNode.Neighbours[3] == neighbours1[2]);
        Assert.True(centralCNode.Neighbours[4] == neighbours1[3]);
        Assert.True(centralCNode.Neighbours[5] == neighbours1[4]);
        Assert.True(centralCNode.Neighbours[6] == neighbours2[0]);
        Assert.True(centralCNode.Neighbours[7] == neighbours3[0]);
    }
    
}