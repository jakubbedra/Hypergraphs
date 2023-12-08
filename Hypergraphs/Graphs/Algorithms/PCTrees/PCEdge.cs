namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCEdge
{
    public PCNode NodeA { get; set; }
    public PCNode NodeB { get; set; }
    public bool IsParentA { get; set; }
    
    public PCEdge Left { get; set; }
    public PCEdge Right { get; set; }

    public void Contract()
    {
        
    }

}