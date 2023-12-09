using Hypergraphs.Extensions;
using Hypergraphs.Graphs.Algorithms.PCTrees;
using HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees;

public class TerminalPathUtilsTest
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
        
        TerminalPathUtils.OrderPNode(node, leftPartial, null);
        
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
        
        TerminalPathUtils.OrderPNode(node, null, rightPartial);
        
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
        
        TerminalPathUtils.OrderPNode(node, leftPartial, rightPartial);

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
        TerminalPathUtils.OrderCNode(node, leftPartial, null);
        
        neighbours.RotateLeft(neighbours.IndexOf(leftPartial));
     
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
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(PCNodesProvider.FullP);
        node.AppendNeighbour(PCNodesProvider.FullLeaf);
        node.AppendNeighbour(leftPartial);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyP);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        node.AppendNeighbour(PCNodesProvider.EmptyLeaf);
        
        TerminalPathUtils.OrderPNode(node, leftPartial, null);
        
        Assert.That(node.Neighbours[0], Is.EqualTo(leftPartial));
        Assert.That(node.Neighbours[1].Label, Is.EqualTo(NodeLabel.Full));
        Assert.That(node.Neighbours[^1].Label, Is.EqualTo(NodeLabel.Empty));
    }

    [Test]
    public void OrderCNodeTest_PartialOnLeft_CannotBeOrdered()
    {
    }

    [Test]
    public void OrderCNodeTest_PartialOnRight_CanBeOrdered()
    {
        
    }
    
    [Test]
    public void OrderCNodeTest_TwoPartials_CanBeOrdered()
    {
        
    }
    
}