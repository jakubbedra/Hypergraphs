﻿namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCNode
{
    public NodeType Type { get; set; }
    public NodeLabel Label { get; set; }

    // null for C - nodes
    private PCNode? _parent;
    public PCNode? Parent
    {
        get => _parent;
        set
        {
            if (Type != NodeType.C) _parent = value;
        }
    }

    // only for leaves
    public int? Column { get; set; }

    // neighbours ordered clockwise
    private List<PCNode> _neighbours;
    public List<PCNode> Neighbours
    {
        get { return _neighbours; }
        init => _neighbours = value;
    }

    public PCNode()
    {
        _neighbours = new List<PCNode>();
        Label = NodeLabel.Undefined;
    }
    
    public void AppendNeighbour(PCNode node)
    {
        _neighbours.Add(node);
    }

    public void PrependNeighbour(PCNode node)
    {
        _neighbours.Insert(0, node);
    }
    
    public void Flip()
    {
        _neighbours.Reverse();
    }

    public bool ModifyOrder(List<PCNode> order)
    {
        if (Type == NodeType.C)
            return false;
        if (Neighbours.Any(c => !order.Contains(c)))
            return false;
        _neighbours = order;
        return true;
    }
    
}
