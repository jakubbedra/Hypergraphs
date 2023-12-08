namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCNode
{
    public NodeType Type { get; set; }

    // null for C - nodes
    public PCEdge? ParentEdge { get; set; }

    // only for leaves
    public int? Column { get; set; }
    
}