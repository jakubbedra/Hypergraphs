using Hypergraphs.Graphs.Algorithms.PCTrees;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

public class PCNodesProvider
{
    public static PCNode FullLeaf => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Full,
    };
    
    public static PCNode FullLeafWithColumn (int column) => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Full,
        Column = column
    };
    
    public static PCNode EmptyLeaf => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Empty,
    };
    
    public static PCNode EmptyLeafWithColumn (int column) => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Empty,
        Column = column
    };

    public static PCNode UnlabeledLeafWithColumn(int column) => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Undefined,
        Column = column
    };

    public static PCNode FullP => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Full,
    };
    
    public static PCNode FullPWithTwoLeaves => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Full,
        Neighbours = new List<PCNode>(){FullLeaf, FullLeaf}
    };
    
    public static PCNode EmptyP => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Empty,
    };
    
    public static PCNode PartialP => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Partial,
    };
    
    public static PCNode FullC => new PCNode()
    {
        Type = NodeType.C,
        Label = NodeLabel.Full,
    };
    
    public static PCNode EmptyC => new PCNode()
    {
        Type = NodeType.C,
        Label = NodeLabel.Empty,
    };
    
    public static PCNode PartialC => new PCNode()
    {
        Type = NodeType.C,
        Label = NodeLabel.Partial,
    };

    public static PCNode EmptyPWithLeaves(List<int> leafColumns) => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Empty,
        Neighbours = leafColumns.Select(x => EmptyLeafWithColumn(x)).ToList()
    };

    public static PCNode FullPWithLeaves(List<int> leafColumns) => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Full,
        Neighbours = leafColumns.Select(x => EmptyLeafWithColumn(x)).ToList()
    };
    
}