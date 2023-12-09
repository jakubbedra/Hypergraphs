using Hypergraphs.Graphs.Algorithms.PCTrees;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

public class PCNodesProvider
{
    public static PCNode FullLeaf => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Full,
    };
    
    public static PCNode EmptyLeaf => new PCNode()
    {
        Type = NodeType.Leaf,
        Label = NodeLabel.Empty,
    };
    
    public static PCNode FullP => new PCNode()
    {
        Type = NodeType.P,
        Label = NodeLabel.Full,
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
    
}